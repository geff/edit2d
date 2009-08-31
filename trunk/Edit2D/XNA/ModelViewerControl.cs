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

        FrameRateCounter frameRateCounter;

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


        //VPPT[] quad = new VPPT[10];
        VertexPositionColorTexture[] quad = new VertexPositionColorTexture[10];
        VertexDeclaration vdec;

        /// <summary>
        /// Draw texture to given position and angle, and informs shader of lastPosition and lastAngle
        /// </summary>
        /// <param name="texture">Texture to draw</param>
        /// <param name="position">Current position</param>
        /// <param name="lastPosition">Previous position</param>
        /// <param name="angle">Current rotation angle</param>
        /// <param name="lastAngle">Previous rotatino angle</param>
        public void DrawTexture(Texture2D texture, Vector2 position, Vector2 center, Vector2 size, Vector2 lastPosition, float angle, float lastAngle)
        {
            //vdec = new VertexDeclaration(GraphicsDevice, VPPT.VertexElements);
            vdec = new VertexDeclaration(GraphicsDevice, VertexPositionColorTexture.VertexElements);

            #region Setup quadrilater with the size of the texture, and centered at (0,0)
            //float halfW = texture.Width / 2;
            //float halfH = texture.Height / 2;

            float halfW = size.X / 2f;
            float halfH = size.Y / 2f;

            float left = -halfW;
            float right = +halfW;
            float top = -halfH;
            float bottom = +halfH;

            center.X = 0; //(float)Math.Round((double)center.X, MidpointRounding.AwayFromZero);
            center.Y = 0;// (float)Math.Round((double)center.Y, MidpointRounding.AwayFromZero);

            //float left = -halfW - center.X + halfW;
            //float right = +halfW + center.X - halfW;
            //float top = -halfH - center.Y + halfH;
            //float bottom = +halfH + center.Y - halfH;

            //--- VPPT
            //quad[0].Position0 = new Vector3(0, 0, 0);

            //quad[1].Position0 = new Vector3(left, top, 0);
            //quad[2].Position0 = new Vector3(0, top, 0);
            //quad[3].Position0 = new Vector3(right, top, 0);

            //quad[4].Position0 = new Vector3(right, 0, 0);
            //quad[5].Position0 = new Vector3(right, bottom, 0);
            //quad[6].Position0 = new Vector3(0, bottom, 0);

            //quad[7].Position0 = new Vector3(left, bottom, 0);
            //quad[8].Position0 = new Vector3(left, 0, 0);
            //quad[9].Position0 = new Vector3(left, top, 0);

            //quad[0].TextureCoordinate = new Vector2(.5f, .5f);

            //quad[1].TextureCoordinate = new Vector2(0f, 0f);
            //quad[2].TextureCoordinate = new Vector2(.5f, 0f);
            //quad[3].TextureCoordinate = new Vector2(1f, 0f);

            //quad[4].TextureCoordinate = new Vector2(1f, .5f);
            //quad[5].TextureCoordinate = new Vector2(1f, 1f);
            //quad[6].TextureCoordinate = new Vector2(.5f, 1f);

            //quad[7].TextureCoordinate = new Vector2(0f, 1f);
            //quad[8].TextureCoordinate = new Vector2(0f, .5f);
            //quad[9].TextureCoordinate = new Vector2(0f, 0f);
            //---

            quad[0].Position = new Vector3(0, 0, 0);

            quad[1].Position = new Vector3(left, top, 0);
            quad[2].Position = new Vector3(0, top, 0);
            quad[3].Position = new Vector3(right, top, 0);

            quad[4].Position = new Vector3(right, 0, 0);
            quad[5].Position = new Vector3(right, bottom, 0);
            quad[6].Position = new Vector3(0, bottom, 0);

            quad[7].Position = new Vector3(left, bottom, 0);
            quad[8].Position = new Vector3(left, 0, 0);
            quad[9].Position = new Vector3(left, top, 0);

            quad[0].TextureCoordinate = new Vector2(.5f, .5f);

            quad[1].TextureCoordinate = new Vector2(0f, 0f);
            quad[2].TextureCoordinate = new Vector2(.5f, 0f);
            quad[3].TextureCoordinate = new Vector2(1f, 0f);

            quad[4].TextureCoordinate = new Vector2(1f, .5f);
            quad[5].TextureCoordinate = new Vector2(1f, 1f);
            quad[6].TextureCoordinate = new Vector2(.5f, 1f);

            quad[7].TextureCoordinate = new Vector2(0f, 1f);
            quad[8].TextureCoordinate = new Vector2(0f, .5f);
            quad[9].TextureCoordinate = new Vector2(0f, 0f);

            #endregion

            //Translate and Rotate quadrilater

            float sin = (float)Math.Sin(angle);
            float cos = (float)Math.Cos(angle);
            float sin2 = (float)Math.Sin(lastAngle);
            float cos2 = (float)Math.Cos(lastAngle);

            for (int ii = 0; ii < quad.Length; ++ii)
            {
                //The following code is the equivalent of inlining the follwing 2 lines of code(exepept the rotation matrix is only created once)
                //quad[ii].Position0 = new Vector3(Vector2.Transform(quad[ii].Position0, Matrix.CreateRotationZ(angle) * Matrix.CreateTranslation(new Vector3(position,0));
                //and 
                //quad[ii].Position0 = new Vector3(Vector2.Transform(quad[ii].Position0, Matrix.CreateRotationZ(lastAngle) * Matrix.CreateTranslation(new Vector3(lastPosition,0)); 

                //float tmpX = quad[ii].Position.X + center.X;
                //float tmpY = quad[ii].Position.Y + center.Y;
                //quad[ii].Position.X = (tmpX * cos) + (tmpY * -sin) + position.X + center.X;
                //quad[ii].Position.Y = (tmpX * sin) + (tmpY * cos) + position.Y + center.Y;

                //quad[ii].Position.X = (tmpX * cos2) + (tmpY * -sin2) + lastPosition.X + center.X;
                //quad[ii].Position.Y = (tmpX * sin2) + (tmpY * cos2) + lastPosition.Y + center.Y;

            }

            GraphicsDevice.Textures[0] = texture;

            GraphicsDevice.VertexDeclaration = vdec;

            //GraphicsDevice.DrawUserPrimitives<VPPT>(PrimitiveType.TriangleFan, quad, 0, 8);
            GraphicsDevice.DrawUserPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleFan, quad, 0, 8);
        }

        private void DrawSprite(Texture2D texture, Vector2 position, Vector2 center, Vector2 size, Vector2 lastPosition, float angle, float lastAngle)
        {
            effect.Begin();

            //GraphicsDevice.SetRenderTarget(0, mbColor);
            GraphicsDevice.SetRenderTarget(0, null);


            effect.CurrentTechnique.Passes[0].Begin();

            //A compléter

            //--- DrawBlurred
            Viewport port = GraphicsDevice.Viewport;
            //Vector2 viewport = new Vector2(port.Width / 2, port.Height / 2);
            Vector2 viewport = new Vector2(port.Width, port.Height);

            GraphicsDevice.SetVertexShaderConstant(0, viewport);
            GraphicsDevice.SetVertexShaderConstant(1, size);
            GraphicsDevice.SetVertexShaderConstant(2, repository.Camera.MatrixTransformation);



            //--- DrawHelper
            //draw.Draw(color, position, lastp, angle, lastangle);
            DrawTexture(texture, position, center, size, lastPosition, angle, lastAngle);
            //---

            effect.CurrentTechnique.Passes[0].End();

            effect.End();
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

            EffectPass pass = null;

            //--- Texture de l'entité
            if (entite.BlurFactor != 0f)
            {
                effect.CurrentTechnique = effect.Techniques["Blur"];
                pass = effect.Techniques["Blur"].Passes[0];
            }
            else
            {
                effect.CurrentTechnique = effect.Techniques["SpriteBatch"];
                pass = effect.Techniques["SpriteBatch"].Passes[0];
            }
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
            effect.End();
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
                    //(int)(10f / repository.Camera.Zoom),
                    //(int)(16f / repository.Camera.Zoom));

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

                //--- Cadre de sélection
                //if (entite.Selected)
                //{
                //    float ratioX = (float)entite.Size.Width / (float)entite.NativeImageSize.Width;
                //    float ratioY = (float)entite.Size.Height / (float)entite.NativeImageSize.Height;

                //    Vector2 vecCenter = Vector2.Zero;
                //    vecCenter.X = 5f * entite.Center.X / (float)entite.Size.Width * ratioX;
                //    vecCenter.Y = 5f * entite.Center.Y / (float)entite.Size.Height * ratioY;

                //    this.spriteBatch.Draw(TextureManager.LoadTexture2D("Anchor"), entite.Rectangle, null, new Color(0, 150, 250, 100), entite.Body.Rotation, vecCenter, SpriteEffects.None, 0f);
                //}
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

            //--- Frame rate
            

            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, String.Format("{0:00.0} FPS", 1000f / stopWatch.ElapsedMilliseconds), new Vector2(20,20), Color.White);
            spriteBatch.End();

            stopWatch.Reset();
            stopWatch.Start();
            //---


            //this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, repository.Camera.MatrixTransformation);
            //---> Begin pour le shader
            //this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState);//, repository.Camera.MatrixTransformation);

            //--- Réinitialisation du renderstate - Le SpriteBatch modifie le renderstate
            //this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);
            //effect.Begin();

            //            Rectangle recScreen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];
                DrawEntite(entite);
            }

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
