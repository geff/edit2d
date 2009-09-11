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
        LineBrush line;

        //FrameRateCounter frameRateCounter;

        // Timer controls the rotation speed.
        Stopwatch timer;

        public ContentManager Content;
        ContentBuilder contentBuilder;

        public SpriteBatch spriteBatch;
        Edit2D.Repository repository;
        Effect effect;
        EffectPool effectPool;
        CompiledEffect compiledEffect;
        SpriteFont spriteFont;
        private bool loadNewShader = false;

        string effectPath = @"D:\Log\Edit2D\Blip\Content\Shader";
        string effectFileName = "SpriteBatch.fx";

        /// <summary>
        /// Initializes the control.
        /// </summary>
        public void Initialize(Edit2D.Repository repository, ContentManager content, ContentBuilder contentBuilder)
        {
            this.Content = content;
            this.contentBuilder = contentBuilder;
            this.repository = repository;
            this.line = new LineBrush(1, Color.Red);

            // Start the animation timer.
            timer = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };

            //--- Chargement de l'effet
            effectPool = new EffectPool();
            //effect = content.Load<Effect>("SpriteBatch");

            LoadShader();

            FileSystemWatcher watcher = new FileSystemWatcher(effectPath, effectFileName);
            watcher.EnableRaisingEvents = true;
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);

            ChangeViewPortSize();
            //---

            spriteFont = content.Load<SpriteFont>("spriteFont");

            line.Load(GraphicsDevice);

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

        public void ChangeViewPortSize()
        {
            //Viewport viewport = GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(ClientSize.Width, ClientSize.Height);
            //effect.Parameters["ViewportSize"].SetValue(viewportSize);
        }

        private void DrawEntiteBasic(Entite entite, string technique)
        {
            EffectPass pass = null;
            //GraphicsDevice.Clear(Color.White);
            //--- Pass
            effect.CurrentTechnique = effect.Techniques[technique];

            //pass = effect.Techniques[technique].Passes[0];
            //---

            //---
            this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, repository.Camera.MatrixTransformation);

            //effect.Parameters["myTextureSize"].SetValue(new Vector2(entite.NativeImageSize.Width, entite.NativeImageSize.Height));
            //effect.Parameters["myEntiteSize"].SetValue(entite.SizeVector);
            //effect.Parameters["timeMS"].SetValue(DateTime.Now.Millisecond);
            //effect.Parameters["isSelected"].SetValue(entite.Selected);

            effect.Begin();
            effect.CurrentTechnique.Passes[0].Begin();

            Texture2D texture = null;

            if (entite is Particle)
                texture = TextureManager.LoadParticleTexture2D(entite.TextureName);
            else
                texture = TextureManager.LoadTexture2D(entite.TextureName);

            Rectangle rectSrc = entite.Rectangle;
            Rectangle rectDst = entite.Rectangle;

            this.spriteBatch.Draw(texture, rectDst, null, entite.Color, entite.Body.Rotation, entite.Center, SpriteEffects.None, 0f);
            this.spriteBatch.End();

            effect.CurrentTechnique.Passes[0].End();
            effect.End();
            //---
        }

        private void DrawEntiteEdge(Entite entite)
        {
            int nbPass = 5;
            Texture2D edgeTexture = null;
            Vector3 spriteScale = Vector3.Transform(new Vector3(entite.SizeVector, 0f), repository.Camera.MatrixScale);
            //RenderTarget2D edgeRenderTarget1 = new RenderTarget2D(GraphicsDevice, (int)spriteScale.X, (int)spriteScale.Y, 1, SurfaceFormat.Color);
            RenderTarget2D edgeRenderTarget1 = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 1, SurfaceFormat.Color);

            //--- [1] Récupère le RenderTarget principal 
            //RenderTarget firstRenderTarget = GraphicsDevice.GetRenderTarget(0);
            //---
            
            //--- [2] Affecte le nouveau RenderTarget
            GraphicsDevice.SetRenderTarget(0, null);
            GraphicsDevice.SetRenderTarget(0, edgeRenderTarget1);
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
            DrawEntiteBasic(entite, "EdgePass");
            //---

            GraphicsDevice.SetRenderTarget(0, null);


            edgeTexture = edgeRenderTarget1.GetTexture();
            edgeTexture.Save(@"c:\testRT.png", ImageFileFormat.Png);

            

            /*
            for (int i = 0; i < nbPass; i++)
            {
                edgeTexture = edgeRenderTarget1.GetTexture();
                edgeTexture.Save(String.Format(@"c:\testRT{0}.png", i), ImageFileFormat.Png);

                //--- [6] Affecte le nouveau RenderTarget
                //RenderTarget2D edgeRenderTarget2 = new RenderTarget2D(GraphicsDevice, (int)spriteScale.X, (int)spriteScale.Y, 1, SurfaceFormat.Color);
                //ResolveTexture2D tex = new ResolveTexture2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height,1, SurfaceFormat.Color);
                
                GraphicsDevice.SetRenderTarget(0, edgeRenderTarget1);
                //---

                //--- [7] Affecte la texture
                effect.Parameters["myTextureSize"].SetValue(new Vector2(entite.NativeImageSize.Width, entite.NativeImageSize.Height));
                effect.Parameters["myEntiteSize"].SetValue(entite.SizeVector);
                effect.Parameters["EdgePassTexture"].SetValue(edgeTexture);
                //---


                //--- [8] Affichage du sprite pass
                DrawEntiteBasic(entite, "EdgePass");
                //---

                GraphicsDevice.SetRenderTarget(0, null);
            }

            edgeTexture = edgeRenderTarget1.GetTexture();
            edgeTexture.Save(String.Format(@"c:\testRT{0}.png", nbPass), ImageFileFormat.Png);

            //RenderTarget2D prevRT2 = ((RenderTarget2D)GraphicsDevice.GetRenderTarget(0));
            //GraphicsDevice.SetRenderTarget(0, null);

            //--- [10] Affecte l'ancien RenderTarget
            GraphicsDevice.SetRenderTarget(0, firstRenderTarget as RenderTarget2D);
            //---

            //--- [9] Récupère la texture
            //edgeTexture = prevRT2.GetTexture();
            //---

            //edgeTexture.Save(String.Format(@"c:\testRT{0}.png", nbPass), ImageFileFormat.Png);

            //--- [11] Affecte la texture
            effect.Parameters["EdgePassTexture"].SetValue(edgeTexture);
            //---

            //====== Shader Edge
            //--- [12] Affecte les paramètrers du shader
            effect.Parameters["timeMS"].SetValue(DateTime.Now.Millisecond);
            effect.Parameters["isSelected"].SetValue(entite.Selected);
            //---

            //--- [13] Affichage du sprite pass
            DrawEntiteBasic(entite, "Edge");
            //---
            //======

            GraphicsDevice.SetRenderTarget(0, null);
            */
        }

        private void DrawEntite(Entite entite)
        {
            /*
            if (entite.ListParticleSystem.Count > 0)
            {
                //--- Rendu du système de particule
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    ParticleSystem pSystem = entite.ListParticleSystem[j];

                    //--- Rendu de l'angle d'émission
                    if (entite.Selected)
                    {
                        this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, repository.Camera.MatrixTransformation);

                        Vector2 vecStart1 = entite.Position;
                        Vector2 vecStart2 = entite.Position;

                        Vector2 vecEnd1 = new Vector2();
                        Vector2 vecEnd2 = new Vector2();

                        //float angle = Vector2.UnitX.GetAngle(pSystem.EmittingVector);

                        float rayon = 30f;

                        vecEnd1 = vecStart1 + new Vector2(rayon * (float)Math.Cos(pSystem.EmmittingAngle + pSystem.FieldAngle / 2f), rayon * (float)Math.Sin(pSystem.EmmittingAngle + pSystem.FieldAngle / 2f));
                        vecEnd2 = vecStart2 + new Vector2(rayon * (float)Math.Cos(pSystem.EmmittingAngle - pSystem.FieldAngle / 2f), rayon * (float)Math.Sin(pSystem.EmmittingAngle - pSystem.FieldAngle / 2f));

                        line.Draw(spriteBatch, vecStart1, vecEnd1);
                        line.Draw(spriteBatch, vecStart2, vecEnd2);
                        line.Draw(spriteBatch, vecEnd1, vecEnd2);

                        this.spriteBatch.End();
                    }

                    for (int k = 0; k < pSystem.ListParticle.Count; k++)
                    {
                        DrawEntite(pSystem.ListParticle[k]);
                    }
                }
                //---
            }
            */
            //effect.Parameters["timeMS"].SetValue(DateTime.Now.Millisecond);
            //effect.Parameters["isSelected"].SetValue(entite.Selected);

            DrawEntiteEdge(entite);
            //DrawEntiteBasic(entite, "EdgePass");

            /*
            EffectPass pass = null;

            //--- Texture de l'entité
            //if (entite.BlurFactor != 0f)
            //{
            //    effect.CurrentTechnique = effect.Techniques["Blur"];
            //    pass = effect.Techniques["Blur"].Passes[0];
            //}
            //else
            //{
            effect.CurrentTechnique = effect.Techniques["SpriteBatch"];
            pass = effect.Techniques["SpriteBatch"].Passes[0];
            //}
            //---

            //---

            this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, repository.Camera.MatrixTransformation);

            effect.Parameters["isInBackground"].SetValue(entite.IsInBackground);
            effect.Parameters["blurFactor"].SetValue(entite.BlurFactor);
            effect.Parameters["timeMS"].SetValue(DateTime.Now.Millisecond);
            effect.Parameters["isSelected"].SetValue(entite.Selected);

            effect.Parameters["myTextureSize"].SetValue(new Vector2(entite.NativeImageSize.Width, entite.NativeImageSize.Height));
            effect.Parameters["myEntiteSize"].SetValue(entite.SizeVector);

            effect.Begin();
            pass.Begin();

            Texture2D texture = null;

            if (entite is Particle)
            {
                texture = TextureManager.LoadParticleTexture2D(entite.TextureName);
            }
            else
            {
                texture = TextureManager.LoadTexture2D(entite.TextureName);
            }

            Rectangle rectSrc = entite.Rectangle;
            Rectangle rectDst = entite.Rectangle;

            this.spriteBatch.Draw(texture, rectDst, null, entite.Color, entite.Body.Rotation, entite.Center, SpriteEffects.None, 0f);
            this.spriteBatch.End();

            pass.End();
            effect.End();*/
            //---

            if (((!repository.Pause && repository.IsEntityClickableOnPlay) || repository.Pause) && ((repository.ShowDebugMode && entite.IsStatic) || entite.Selected))
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

            //if (entite.Selected && entite.ListParticleSystem.Count > 0)
            //{
            //    //--- Rendu du système de particule
            //    for (int j = 0; j < entite.ListParticleSystem.Count; j++)
            //    {
            //        ParticleSystem pSystem = entite.ListParticleSystem[j];

            //        this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, repository.Camera.MatrixTransformation);

            //        Vector2 vecStart1 = entite.Position;
            //        Vector2 vecStart2 = entite.Position;

            //        Vector2 vecEnd1 = new Vector2();
            //        Vector2 vecEnd2 = new Vector2();

            //        //float angle = Vector2.UnitX.GetAngle(pSystem.EmittingVector);

            //        float rayon = 30f;

            //        vecEnd1 = vecStart1 + new Vector2(rayon * (float)Math.Cos(pSystem.EmmittingAngle + pSystem.FieldAngle / 2f), rayon * (float)Math.Sin(pSystem.EmmittingAngle + pSystem.FieldAngle / 2f));
            //        vecEnd2 = vecStart2 + new Vector2(rayon * (float)Math.Cos(pSystem.EmmittingAngle - pSystem.FieldAngle / 2f), rayon * (float)Math.Sin(pSystem.EmmittingAngle - pSystem.FieldAngle / 2f));

            //        line.Draw(spriteBatch, vecStart1, vecEnd1);
            //        line.Draw(spriteBatch, vecStart2, vecEnd2);
            //        line.Draw(spriteBatch, vecEnd1, vecEnd2);

            //        this.spriteBatch.End();
            //    }
            //    //---
            //}
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

            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (loadNewShader)
            {
                LoadShader();
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

            //this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, repository.Camera.MatrixTransformation);
            //---> Begin pour le shader
            //this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState);//, repository.Camera.MatrixTransformation);

            //--- Réinitialisation du renderstate - Le SpriteBatch modifie le renderstate
            //this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);
            //effect.Begin();

            //            Rectangle recScreen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


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

                if (recScreen.Intersects(recEntite))
                {
                    listEntiteToDraw.Add(entite);
                }
            }

            for (int i = 0; i < listEntiteToDraw.Count; i++)
            {
                DrawEntite(listEntiteToDraw[i]);
            }

            repository.FrmEdit2D.Text = listEntiteToDraw.Count.ToString();

            //for (int i = 0; i < repository.listEntite.Count; i++)
            //{
            //    DrawEntite(repository.listEntite[i]);
            //}


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
            spriteBatch.DrawString(spriteFont, String.Format("{0:00.0} FPS", 1000f / stopWatch.ElapsedMilliseconds), new Vector2(20, 20), Color.White);
            stopWatch.Reset();
            stopWatch.Start();
            //---

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
