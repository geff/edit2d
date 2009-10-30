#region File Description
//-----------------------------------------------------------------------------
// ModelViewerControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Edit2D;
using FarseerGames.GettingStarted.DrawingSystem;
using Edit2DEngine.Particles;
using Edit2DEngine.Particles;
using Edit2DEngine;
using Edit2D.Properties;
using System.Threading;
using Edit2DEngine.Tools;
#endregion

namespace WinFormsContentLoading
{
    public struct VPPT
    {
        public Vector3 Position0;
        public Vector3 Position1;
        public Vector2 TextureCoordinate;

        public static int SizeInBytes = (3 + 3 + 2) * sizeof(float);
        public static readonly VertexElement[] VertexElements = new VertexElement[] { 
                new VertexElement(0,0,VertexElementFormat.Vector3,
                                             VertexElementMethod.Default,VertexElementUsage.Position,0),
                new VertexElement(0,sizeof(float)*3,VertexElementFormat.Vector3,
                                             VertexElementMethod.Default,VertexElementUsage.Position,1),
                new VertexElement(0,sizeof(float)*6,VertexElementFormat.Vector2,
                                             VertexElementMethod.Default,VertexElementUsage.TextureCoordinate,0),
            };

    }

    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, and displays
    /// a spinning 3D model. The main form class is responsible for loading
    /// the model: this control just displays it.
    /// </summary>
    public class ModelViewerControl : GraphicsDeviceControl
    {
        LineBrush lineBrush;
        RectangleBrush rectangleBrush;

        public bool ChangeViewPortSize = false;

        //FrameRateCounter frameRateCounter;

        // Timer controls the rotation speed.
        Stopwatch timer;

        public ContentManager Content;
        ContentBuilder contentBuilder;

        public SpriteBatch spriteBatch;
        Edit2D.Repository repository;
        Effect effect;
        BasicEffect basicEffect;
        EffectPool effectPool;
        CompiledEffect compiledEffect;
        SpriteFont spriteFont;
        private bool loadNewShader = false;

        //string effectPath = @"D:\Log\Edit2D\Blip\Content\Shader";
        string effectPath = @"D:\GDD\Log\Log\Edit2D\Blip\Content\Shader";
        string effectFileName = "SpriteBatch.fx";

        /// <summary>
        /// Initializes the control.
        /// </summary>
        public void Initialize(Edit2D.Repository repository, ContentManager content, ContentBuilder contentBuilder)
        {
            this.Content = content;
            this.contentBuilder = contentBuilder;
            this.repository = repository;
            this.lineBrush = new LineBrush(1, Color.Red);
            this.rectangleBrush = new RectangleBrush(10, 10, Color.DarkGreen, Color.DarkKhaki);

            // Start the animation timer.
            timer = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };

            //--- Chargement de l'effet
            effectPool = new EffectPool();
            //effect = content.Load<Effect>("SpriteBatch");

            LoadShader();

            basicEffect = new BasicEffect(GraphicsDevice, effectPool);

            FileSystemWatcher watcher = new FileSystemWatcher(effectPath, effectFileName);
            watcher.EnableRaisingEvents = true;
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);

            ViewPortSizeChanged();
            //---

            spriteFont = content.Load<SpriteFont>("spriteFont");

            lineBrush.Load(GraphicsDevice);
            rectangleBrush.Load(GraphicsDevice);

            InitListModels();
        }

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            loadNewShader = true;
        }

        private void LoadShader()
        {
            compiledEffect = Effect.CompileEffectFromFile(Path.Combine(effectPath, effectFileName), null, null, CompilerOptions.None, TargetPlatform.Windows);

            if (!compiledEffect.Success)
            {
                MessageBox.Show(compiledEffect.ErrorsAndWarnings);
            }
            else
            {
                effect = new Effect(GraphicsDevice, compiledEffect.GetEffectCode(), CompilerOptions.None, effectPool);
            }
        }

        public void RefreshView()
        {
            Draw();
        }

        public void ViewPortSizeChanged()
        {
            //Viewport viewport = GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(ClientSize.Width, ClientSize.Height);
            ChangeViewPortSize = false;
            //effect.Parameters["ViewportSize"].SetValue(viewportSize);
        }

        private void DrawRadialBlur(float radialBlurWidth)
        {
            //--- Initialisation RenderTarget
            ResolveTexture2D textureScene = new ResolveTexture2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 1, SurfaceFormat.Color);
            GraphicsDevice.ResolveBackBuffer(textureScene);
            //---

            //--- Affectionat de la technique graphique
            effect.CurrentTechnique = effect.Techniques["RadialBlur"];
            //---

            //--- Paramètres shader
            effect.Parameters["timeMS"].SetValue((int)DateTime.Now.TimeOfDay.TotalMilliseconds);
            effect.Parameters["BlurWidth"].SetValue(radialBlurWidth);
            //---

            //--- Affichage
            this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            effect.Begin();
            effect.CurrentTechnique.Passes[0].Begin();
            Vector2 center = Vector2.Zero;// new Vector2((float)textureScene.Width / 2f, (float)textureScene.Height / 2f);
            this.spriteBatch.Draw((Texture2D)textureScene, Vector2.Zero, null, Color.Black, 0f, center, 1f, SpriteEffects.None, 0);
            this.spriteBatch.End();

            effect.CurrentTechnique.Passes[0].End();
            effect.End();
            //---
        }

        private void CalcNormalMap(Entite entite)
        {
            //--> 1 : Calcul de la HeightMap
            //--> 2 : Calcul de la normal map

            Texture2D heightMapTexture = null;
            Texture2D normalMapTexture = null;
            Vector3 spriteScale = Vector3.Transform(new Vector3(entite.SizeVector, 0f), repository.Camera.MatrixScale);
            RenderTarget2D renderTarget = new RenderTarget2D(GraphicsDevice, entite.NativeImageSize.Width, entite.NativeImageSize.Height, 1, SurfaceFormat.Color);

            //---------------------------------------
            //-------------- HeightMap --------------
            //---------------------------------------

            //--- [2] Affecte le nouveau RenderTarget
            //GraphicsDevice.DepthStencilBuffer.MultiSampleType = renderTarget.MultiSampleType;
            //GraphicsDevice.DepthStencilBuffer = new DepthStencilBuffer(GraphicsDevice, renderTarget.Width, renderTarget.Height, DepthFormat.Depth16);

            GraphicsDevice.SetRenderTarget(0, null);
            GraphicsDevice.SetRenderTarget(0, renderTarget);
            GraphicsDevice.DepthStencilBuffer = new DepthStencilBuffer(GraphicsDevice, renderTarget.Width, renderTarget.Height, DepthFormat.Depth16);
            GraphicsDevice.Clear(Color.TransparentBlack);
            //---

            //====== [3] Affecte la texture du sprite
            //--- Texture
            Texture2D texture = null;
            if (entite is Particle)
                texture = TextureManager.LoadParticleTexture2D(entite.TextureName);
            else
                texture = TextureManager.LoadTexture2D(entite.TextureName);
            //---

            //--- Paramètres shader
            effect.Parameters["myTextureSize"].SetValue(new Vector2(entite.NativeImageSize.Width, entite.NativeImageSize.Height));
            effect.Parameters["EdgePassTexture"].SetValue(texture);
            //---
            //======

            //--- [4] Affichage du sprite pass
            DrawEntiteBasic(entite, true, "NormalMap", 0);
            //---

            //--- Désaffecte le RenderTarget
            GraphicsDevice.SetRenderTarget(0, null);
            //---

            heightMapTexture = renderTarget.GetTexture();

            //if (repository.Screenshot)
            heightMapTexture.Save(@"c:\scr\HeightMap.png", ImageFileFormat.Png);
            //---------------------------------------
            //---------------------------------------

            //---------------------------------------
            //-------------- NormalMap --------------
            //---------------------------------------

            //--- [2] Affecte le nouveau RenderTarget
            GraphicsDevice.SetRenderTarget(0, renderTarget);
            GraphicsDevice.Clear(Color.TransparentBlack);
            //---

            //GraphicsDevice.SamplerStates[0].MaxAnisotropy = 4;

            //====== [3] Affecte la texture du sprite
            //--- Paramètres shader
            //effect.Parameters["myTextureSize"].SetValue(new Vector2(entite.NativeImageSize.Width, entite.NativeImageSize.Height));
            effect.Parameters["EdgePassTexture"].SetValue(heightMapTexture);
            //---
            //======

            //--- [4] Affichage du sprite pass
            DrawEntiteBasic(entite, true, "NormalMap", 1);
            //---

            //--- Désaffecte le RenderTarget
            GraphicsDevice.SetRenderTarget(0, null);
            //---

            GraphicsDevice.DepthStencilBuffer = new DepthStencilBuffer(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, DepthFormat.Depth16);

            normalMapTexture = renderTarget.GetTexture();

            //if (repository.Screenshot)
            heightMapTexture.Save(@"c:\scr\NormalMap.png", ImageFileFormat.Png);

            //--- Enregistrer la normal Map
            if (entite is Particle)
            {

            }
            else
            {
                if (TextureManager.ListTexture2D.ContainsKey(String.Format("{0}NormalMap", entite.TextureName)))
                    TextureManager.ListTexture2D[String.Format("{0}NormalMap", entite.TextureName)] = normalMapTexture;
                else
                    TextureManager.AddTexture2D(String.Format("{0}NormalMap", entite.TextureName), normalMapTexture);
            }
            //---
        }

        private void DrawEntiteBasic(Entite entite, bool noPosition, string technique)
        {
            DrawEntiteBasic(entite, noPosition, technique, 0);
        }

        private void DrawEntiteBasicVertices(Entite entite, bool noPosition, string technique, int pass)
        {
            Texture2D texture = null;

            if (entite is Particle)
                texture = TextureManager.LoadParticleTexture2D(entite.TextureName);
            else
                texture = TextureManager.LoadTexture2D(entite.TextureName);

            //-- Test drawing element
            GraphicsDevice.VertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionTexture.VertexElements);
            GraphicsDevice.Textures[0] = texture;
            //GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, entite.TexVertices, 0, entite.NumberTriangles);
            
            //--- Pass
            
            effect.CurrentTechnique = effect.Techniques[technique];
            //---
            effect.Begin();
            effect.CurrentTechnique.Passes[pass].Begin();

            GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, entite.TexVertices, 0, entite.TexVertices.Length, entite.TexIndices, 0, entite.NumberTriangles);
            
            effect.CurrentTechnique.Passes[pass].End();
            effect.End();
            //---
        }

        private void DrawEntiteBasic(Entite entite, bool noPosition, string technique, int pass)
        {
            //--- Pass
            effect.CurrentTechnique = effect.Techniques[technique];
            //---

            //---
            if (noPosition)
                this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);
            else
                this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, repository.Camera.MatrixTransformation);

            effect.Begin();
            effect.CurrentTechnique.Passes[pass].Begin();

            Texture2D texture = null;

            if (entite is Particle)
                texture = TextureManager.LoadParticleTexture2D(entite.TextureName);
            else
                texture = TextureManager.LoadTexture2D(entite.TextureName);

            Rectangle rectDst = entite.Rectangle;

            if (noPosition)
            {
                rectDst.Location = new Point((int)entite.Center.X + 0, (int)entite.Center.Y + 0);
                //rectDst.Width += 10;
                //rectDst.Height += 10;
                //GraphicsDevice.DepthStencilBuffer = new DepthStencilBuffer(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, DepthFormat.Depth16);
            }
            else
            {
                //GraphicsDevice.DepthStencilBuffer = new DepthStencilBuffer(GraphicsDevice, rectDst.Width, rectDst.Height, DepthFormat.Depth16);
            }

            this.spriteBatch.Draw(texture, rectDst, null, entite.Color, entite.Body.Rotation, entite.Center, SpriteEffects.None, 0f);
            this.spriteBatch.End();

            effect.CurrentTechnique.Passes[pass].End();
            effect.End();
            //---
        }

        private void DrawEntiteEdge(Entite entite)
        {
            int nbPass = 20;
            Color clearColor = Color.TransparentWhite;

            Texture2D edgeTexture = null;
            Vector3 spriteScale = Vector3.Transform(new Vector3(entite.SizeVector, 0f), repository.Camera.MatrixScale);
            RenderTarget2D edgeRenderTarget1 = new RenderTarget2D(GraphicsDevice, entite.NativeImageSize.Width, entite.NativeImageSize.Height, 1, SurfaceFormat.Color);

            //--- [1] Récupère le RenderTarget principal 
            RenderTarget firstRenderTarget = GraphicsDevice.GetRenderTarget(0);
            //---

            //--- [2] Affecte le nouveau RenderTarget
            //GraphicsDevice.SetRenderTarget(0, null);
            GraphicsDevice.SetRenderTarget(0, edgeRenderTarget1);
            GraphicsDevice.Clear(clearColor);
            //---

            //====== [3] Affecte la texture du sprite
            //--- Texture
            Texture2D texture = null;
            if (entite is Particle)
                texture = TextureManager.LoadParticleTexture2D(entite.TextureName);
            else
                texture = TextureManager.LoadTexture2D(entite.TextureName);
            //---

            //--- Paramètres shader
            effect.Parameters["myTextureSize"].SetValue(new Vector2(entite.NativeImageSize.Width, entite.NativeImageSize.Height));
            effect.Parameters["myEntiteSize"].SetValue(entite.SizeVector);
            effect.Parameters["EdgePassTexture"].SetValue(texture);
            effect.Parameters["initEdgePass"].SetValue(true);
            //---
            //======

            //--- [4] Affichage du sprite pass
            DrawEntiteBasic(entite, true, "EdgePass");
            //---

            //--- Désaffecte le RenderTarget
            GraphicsDevice.SetRenderTarget(0, null);
            //---


            for (int i = 0; i < nbPass; i++)
            {
                edgeTexture = edgeRenderTarget1.GetTexture();
                if (repository.Screenshot)
                    edgeTexture.Save(String.Format(@"c:\scr\testRT{0}.png", i), ImageFileFormat.Png);

                //--- [6] Affecte le nouveau RenderTarget
                GraphicsDevice.SetRenderTarget(0, edgeRenderTarget1);
                GraphicsDevice.Clear(clearColor);
                //---

                //--- [7] Affecte la texture
                effect.Parameters["myTextureSize"].SetValue(new Vector2(entite.NativeImageSize.Width, entite.NativeImageSize.Height));
                effect.Parameters["myEntiteSize"].SetValue(entite.SizeVector);
                effect.Parameters["EdgePassTexture"].SetValue(edgeTexture);
                effect.Parameters["initEdgePass"].SetValue(false);
                //---


                //--- [8] Affichage du sprite pass
                DrawEntiteBasic(entite, true, "EdgePass");
                //---

                //--- Désaffecte le RenderTarget
                GraphicsDevice.SetRenderTarget(0, null);
                //---
            }

            edgeTexture = edgeRenderTarget1.GetTexture();

            if (repository.Screenshot)
                edgeTexture.Save(String.Format(@"c:\scr\testRT{0}.png", nbPass), ImageFileFormat.Png);


            //--- [10] Affecte l'ancien RenderTarget
            //GraphicsDevice.SetRenderTarget(0, firstRenderTarget as RenderTarget2D);
            GraphicsDevice.SetRenderTarget(0, null);
            //---

            //--- [11] Affecte la texture
            effect.Parameters["EdgePassTexture"].SetValue(edgeTexture);
            //---

            //====== Shader Edge
            //--- [12] Affecte les paramètrers du shader
            effect.Parameters["timeMS"].SetValue(DateTime.Now.Millisecond);
            effect.Parameters["isSelected"].SetValue(entite.Selected);
            //---

            //--- [13] Affichage du sprite pass
            DrawEntiteBasic(entite, false, "Edge");
            //---
            //======

            //if (repository.Screenshot)
            //{
            //    firstRenderTarget = GraphicsDevice.GetRenderTarget(1);

            //    edgeTexture = ((RenderTarget2D)firstRenderTarget).GetTexture();
            //    edgeTexture.Save(String.Format(@"c:\scr\final.png", nbPass), ImageFileFormat.Png);

            //}

            GraphicsDevice.SetRenderTarget(0, null);
        }

        private void DrawEntiteNight(Entite entite)
        {
            Texture2D texture = null;
            Texture2D normalMapTexture = null;

            //--- Calcul la normal Map si elle n'existe pas
            if (entite is Particle)
                normalMapTexture = TextureManager.LoadParticleTexture2D(String.Format("{0}NormalMap", entite.TextureName));
            else
                normalMapTexture = TextureManager.LoadTexture2D(String.Format("{0}NormalMap", entite.TextureName));

            if (normalMapTexture == null || repository.Screenshot)
            {
                CalcNormalMap(entite);

                if (entite is Particle)
                    normalMapTexture = TextureManager.LoadParticleTexture2D(String.Format("{0}NormalMap", entite.TextureName));
                else
                    normalMapTexture = TextureManager.LoadTexture2D(String.Format("{0}NormalMap", entite.TextureName));
            }
            //---

            //====== [3] Affecte la texture du sprite
            //--- Texture
            if (entite is Particle)
                texture = TextureManager.LoadParticleTexture2D(entite.TextureName);
            else
                texture = TextureManager.LoadTexture2D(entite.TextureName);
            //---

            //--- Paramètres shader
            effect.Parameters["myTextureSize"].SetValue(new Vector2(entite.NativeImageSize.Width, entite.NativeImageSize.Height));
            //effect.Parameters["myEntiteSize"].SetValue(entite.SizeVector);
            effect.Parameters["myEntitePosition"].SetValue(entite.Position);
            effect.Parameters["timeMS"].SetValue((int)DateTime.Now.TimeOfDay.TotalMilliseconds);
            effect.Parameters["NormalMapTexture"].SetValue(normalMapTexture);
            //---
            //======

            //--- [4] Affichage du sprite pass
            DrawEntiteBasic(entite, false, "Night");
            //---


            if (repository.Screenshot)
            {
                normalMapTexture.Save(@"c:\scr\NormalMap.png", ImageFileFormat.Png);
            }
            //--- Désaffecte le RenderTarget
            //GraphicsDevice.SetRenderTarget(0, null);
            //---
        }

        private void DrawEntiteBlur(Entite entite)
        {
            Texture2D texture = null;

            //====== [3] Affecte la texture du sprite
            //--- Texture
            if (entite is Particle)
                texture = TextureManager.LoadParticleTexture2D(entite.TextureName);
            else
                texture = TextureManager.LoadTexture2D(entite.TextureName);
            //---

            //--- Paramètres shader
            effect.Parameters["myTextureSize"].SetValue(new Vector2(entite.NativeImageSize.Width, entite.NativeImageSize.Height));
            //effect.Parameters["myEntiteSize"].SetValue(entite.SizeVector);
            effect.Parameters["myEntitePosition"].SetValue(entite.Position);
            effect.Parameters["timeMS"].SetValue((int)DateTime.Now.TimeOfDay.TotalMilliseconds);
            //---
            //======

            //--- [4] Affichage du sprite pass
            DrawEntiteBasic(entite, false, "Blur");
            //---

            //--- Désaffecte le RenderTarget
            //GraphicsDevice.SetRenderTarget(0, null);
            //---
        }

        private void DrawEntite(Entite entite)
        {
            //--- Affichage des particules
            if (entite.ListParticleSystem.Count > 0)
            {
                //--- Rendu du système de particule
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    ParticleSystem pSystem = entite.ListParticleSystem[j];

                    for (int k = 0; k < pSystem.ListParticle.Count; k++)
                    {
                        DrawEntite(pSystem.ListParticle[k]);
                    }
                }
                //---
            }
            //---
            
            effect.Parameters["timeMS"].SetValue(DateTime.Now.Millisecond);
            effect.Parameters["isSelected"].SetValue(entite.Selected);
            effect.Parameters["myTextureSize"].SetValue(new Vector2(entite.NativeImageSize.Width, entite.NativeImageSize.Height));

            //DrawEntiteEdge(entite);
            //DrawEntiteBasic(entite, false, "Edge");
            //DrawEntiteBasic(entite, false, "SpriteBatch");
            DrawEntiteBasicVertices(entite, false, "SpriteBatch", 0);
            //--- Night
            //DrawEntiteNight(entite);
            //---

            //--- Blur
            //---


            if (!repository.IsSimpleMode && ((!repository.Pause && repository.IsEntityClickableOnPlay) || repository.Pause) && ((repository.ShowDebugMode && entite.IsStatic) || entite.Selected))
            {
                this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, repository.Camera.MatrixTransformation);

                //--- Pin static
                if (repository.ShowDebugMode && entite.IsStatic)
                {
                    Rectangle recPin = new Rectangle((int)entite.Position.X,
                                                     (int)entite.Position.Y,
                                                     (int)(10f),
                                                     (int)(16f));

                    this.spriteBatch.Draw(TextureManager.LoadTexture2D("Pin"), recPin, null, Color.White, entite.Rotation,
                        entite.SizeVector / 2f + new Vector2(5f, 8f), SpriteEffects.None, 0f);
                }
                //---

                //--- Couche du layer
                if (repository.ShowDebugMode)
                {
                    spriteBatch.DrawString(spriteFont, entite.Layer.ToString(), entite.Position + new Vector2(10f, -10f), Color.Red, entite.Rotation, entite.SizeVector / 2f + new Vector2(5f, 8f), 1f, SpriteEffects.None, 0);
                }
                //---

                this.spriteBatch.End();
            }

            if (entite.Selected && entite.ListParticleSystem.Count > 0)
            {
                lineBrush.Color = Color.Red;
                //--- Rendu du système de particule
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    ParticleSystem pSystem = entite.ListParticleSystem[j];

                    this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, repository.Camera.MatrixTransformation);

                    Vector2 vecStart1 = entite.Position;
                    Vector2 vecStart2 = entite.Position;

                    Vector2 vecEnd1 = new Vector2();
                    Vector2 vecEnd2 = new Vector2();

                    //float angle = Vector2.UnitX.GetAngle(pSystem.EmittingVector);

                    float rayon = 30f;

                    vecEnd1 = vecStart1 + new Vector2(rayon * (float)Math.Cos(pSystem.EmmittingAngle + pSystem.FieldAngle / 2f), rayon * (float)Math.Sin(pSystem.EmmittingAngle + pSystem.FieldAngle / 2f));
                    vecEnd2 = vecStart2 + new Vector2(rayon * (float)Math.Cos(pSystem.EmmittingAngle - pSystem.FieldAngle / 2f), rayon * (float)Math.Sin(pSystem.EmmittingAngle - pSystem.FieldAngle / 2f));

                    lineBrush.Draw(spriteBatch, vecStart1, vecEnd1);
                    lineBrush.Draw(spriteBatch, vecStart2, vecEnd2);
                    lineBrush.Draw(spriteBatch, vecEnd1, vecEnd2);

                    this.spriteBatch.End();
                }
                //---
            }

            //--- Affichage de la courbe active
            if (repository.ShowDebugMode && repository.ViewingMode == ViewingMode.Script && entite.Selected && repository.CurrentEntite == entite)
            {
                lineBrush.Color = Color.Green;
                this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, repository.Camera.MatrixTransformation);
                
                for (int i = 0; i < repository.ListCurveLine.Count-1; i++)
                {
                    Vector2 vecStart = repository.ListCurveLine[i];
                    Vector2 vecEnd = repository.ListCurveLine[i + 1];

                    /*if (i < repository.ListCurveLine.Count - 1)
                    {
                        vecEnd = repository.ListCurveLine[i + 1];
                    }
                    else
                    {
                        vecEnd = repository.ListCurveLine[0];
                    }*/


                    lineBrush.Draw(spriteBatch, vecStart, vecEnd);
                }

                for (int i = 0; i < repository.ListCurvePoint.Count; i++)
                {
                    rectangleBrush.Draw(spriteBatch, repository.ListCurvePoint[i]);
                }

                this.spriteBatch.End();
            }
            //---
        }

        TimeSpan elapsedTime = TimeSpan.Zero;
        Stopwatch stopWatch = new Stopwatch();
        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            if (Program.frm.WindowState == FormWindowState.Minimized || !Program.frm.ContainsFocus)
                return;

            if (this.spriteBatch == null)
            {
                this.spriteBatch = new SpriteBatch(GraphicsDevice);
                repository.PhysicsSimulatorView.LoadContent(GraphicsDevice, Content);
            }

            if (ChangeViewPortSize)
            {
                ViewPortSizeChanged();
            }

            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (loadNewShader)
            {
                LoadShader();
                loadNewShader = false;
            }
            if (!compiledEffect.Success)
                return;

            //--- Fond
            this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState);
            effect.CurrentTechnique = effect.Techniques["Gradient"];
            effect.Parameters["gradientColor1"].SetValue(repository.World.GradientColor1.ToVector4());
            effect.Parameters["gradientColor2"].SetValue(repository.World.GradientColor2.ToVector4());

            effect.Begin();
            effect.CurrentTechnique.Passes[0].Begin();
            GraphicsDevice.SetVertexShaderConstant(0, new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));

            //spriteBatch.Begin();
            spriteBatch.Draw(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.End();

            effect.CurrentTechnique.Passes[0].End();
            effect.End();
            //---

            List<Entite> listEntiteToDraw = new List<Entite>();
            Rectangle recScreen = new Rectangle(GraphicsDevice.Viewport.X + 0, GraphicsDevice.Viewport.Y + 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];

                Rectangle recEntite = Rectangle.Empty;
                Vector3 vecPosEntite = new Vector3(entite.Position - entite.Center, 0f);
                Vector3 vecSizeEntite = new Vector3(entite.geom.AABB.Width, entite.geom.AABB.Height, 0);

                vecPosEntite = Vector3.Transform(vecPosEntite, repository.Camera.MatrixTransformation);
                vecSizeEntite = Vector3.Transform(vecSizeEntite, repository.Camera.MatrixScale);

                recEntite = new Rectangle((int)vecPosEntite.X, (int)vecPosEntite.Y, (int)vecSizeEntite.X, (int)vecSizeEntite.Y);




                if (recScreen.Intersects(recEntite) ||
                    repository.ShowDebugMode && repository.ViewingMode == ViewingMode.Script && entite.Selected && repository.CurrentEntite == entite)
                {
                    listEntiteToDraw.Add(entite);
                }
            }

            for (int i = 0; i < listEntiteToDraw.Count; i++)
            {
                DrawEntite(listEntiteToDraw[i]);
            }

            repository.FrmEdit2D.Text = listEntiteToDraw.Count.ToString();


            if (repository.Screenshot)
            {
                ResolveTexture2D tx = new ResolveTexture2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 1, SurfaceFormat.Color);
                GraphicsDevice.ResolveBackBuffer(tx);
                tx.Save(@"c:\scr\fullscreen.png", ImageFileFormat.Png);
            }

            //--- Affichage de la scène avec le radial blur
            float durationRadialBlurLoading = 1500f;
            float maxRadialBlurWidth = -0.2f;
            if (repository.WatchLoading.IsRunning && repository.WatchLoading.ElapsedMilliseconds < durationRadialBlurLoading)
            {
                float radialBlurWidth = maxRadialBlurWidth * (1f - (float)repository.WatchLoading.ElapsedMilliseconds / durationRadialBlurLoading);
                DrawRadialBlur(radialBlurWidth);
            }
            else if (repository.WatchLoading.IsRunning && repository.WatchLoading.ElapsedMilliseconds >= durationRadialBlurLoading)
            {
                repository.WatchLoading.Stop();
            }
            //---


            this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, repository.Camera.MatrixTransformation);
            //--- Cadre physique
            if (repository.ShowDebugMode)
                repository.PhysicsSimulatorView.Draw(spriteBatch);
            //---
            this.spriteBatch.End();

            this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState);

            Vector2 vecPointerMidSize = new Vector2(5, 5);

            //--- Pointeur de la souris
            this.spriteBatch.Draw(TextureManager.LoadTexture2D("Pointer"), repository.CurrentPointer.ScreenPosition - vecPointerMidSize, null, Color.Red);
            //---

            //--- Pointeur secondaire de la souris
            this.spriteBatch.Draw(TextureManager.LoadTexture2D("Pointer"), repository.CurrentPointer2.ScreenPosition - vecPointerMidSize, null, Color.Green);
            //---

            //--- Pointeurs multi
            for (int i = 0; i < repository.ListSelection.Count; i++)
            {
                this.spriteBatch.Draw(TextureManager.LoadTexture2D("Pointer"), repository.ListSelection[i].Pointer.ScreenPosition - vecPointerMidSize, null, Color.Blue);
            }
            //---

            //--- Frame rate
            //spriteBatch.DrawString(spriteFont, String.Format("{0:00.0} FPS", 1000f / stopWatch.ElapsedMilliseconds), new Vector2(20, 20), Color.White);
            //stopWatch.Reset();
            //stopWatch.Start();
            //---

            repository.Screenshot = false;

            this.spriteBatch.End();
        }

        Vector3 vecLight = new Vector3(2f, 3f, -7f);


        private void InitListModels()
        {
        }

        protected override void Initialize()
        {
        }
    }
}
