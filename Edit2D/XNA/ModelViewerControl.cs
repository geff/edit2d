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
#endregion

namespace WinFormsContentLoading
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, and displays
    /// a spinning 3D model. The main form class is responsible for loading
    /// the model: this control just displays it.
    /// </summary>
    public class ModelViewerControl : GraphicsDeviceControl
    {
        LineBrush line;


        // Timer controls the rotation speed.
        Stopwatch timer;

        public ContentManager Content;
        ContentBuilder contentBuilder;

        public SpriteBatch spriteBatch;
        Edit2D.Repository repository;
        Effect effect;

        //public Vector2 scenePosition;
        //public int scenePositionX;
        //public int scenePositionY;

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
            //effect = content.Load<Effect>(@"Content\\" + "SpriteBatch");
            ChangeViewPortSize();
            //---

            line.Load(GraphicsDevice);

            InitListModels();
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

        private void DrawEntite(Entite entite)
        {
            if (entite.ListParticleSystem.Count > 0)
            {
                //--- Rendu du système de particule
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    ParticleSystem pSystem = entite.ListParticleSystem[j];

                    //--- Rendu de l'angle d'émission
                    if (entite.Selected)
                    {
                        //this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

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

                        //this.spriteBatch.End();
                    }

                    for (int k = 0; k < pSystem.ListParticle.Count; k++)
                    {
                        DrawEntite(pSystem.ListParticle[k]);
                    }
                }
                //---
            }

            //--- Texture de l'entité
            //effect.Begin();
            //effect.Parameters["isInBackground"].SetValue(entite.IsInBackground);
            //effect.Parameters["blurFactor"].SetValue(entite.BlurFactor);

            //int idTechnique = 0;

            //if (entite.BlurFactor > 0f)
            //    idTechnique = 1;
            //---

            //---

            //this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, mtx);

            //effect.Techniques[idTechnique].Passes[0].Begin();

            Texture2D texture = null;

            if (entite is Particle)
            {
                texture = TextureManager.LoadParticleTexture2D(entite.TextureName);
            }
            else
            {
                texture = TextureManager.LoadTexture2D(entite.TextureName);
            }

            this.spriteBatch.Draw(texture, entite.Rectangle, null, entite.Color, entite.Body.Rotation, entite.Center, SpriteEffects.None, 0f);
            //this.spriteBatch.Draw(TextureManager.LoadTexture2D(entite.TextureName), entite.Position, null, entite.Color, entite.Body.Rotation, entite.Center, 1f, SpriteEffects.None, 1f);

            //effect.Techniques[idTechnique].Passes[0].End();

            //this.spriteBatch.End();
            //effect.End();
            //---

            if (((!repository.Pause && repository.IsEntityClickableOnPlay) || repository.Pause) && ((repository.showPhysic && entite.IsStatic) || entite.Selected))
            {
                //this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

                //--- Pin static
                if (repository.showPhysic && entite.IsStatic)
                {
                    Rectangle recPin = new Rectangle((int)entite.Position.X,
                                                     (int)entite.Position.Y,
                                                     (int)(10f),
                                                     (int)(16f));
                    //(int)(10f / repository.Camera.Zoom),
                    //(int)(16f / repository.Camera.Zoom));

                    this.spriteBatch.Draw(TextureManager.LoadTexture2D("Pin"), recPin, null, Color.White, entite.Body.Rotation,
                        entite.SizeVector / 2f + new Vector2(5f, 8f), SpriteEffects.None, 0f);
                }
                //---

                //--- Cadre de sélection
                if (entite.Selected)
                {
                    float ratioX = (float)entite.Size.Width / (float)entite.NativeImageSize.Width;
                    float ratioY = (float)entite.Size.Height / (float)entite.NativeImageSize.Height;

                    Vector2 vecCenter = Vector2.Zero;
                    vecCenter.X = 5f * entite.Center.X / (float)entite.Size.Width * ratioX;
                    vecCenter.Y = 5f * entite.Center.Y / (float)entite.Size.Height * ratioY;

                    this.spriteBatch.Draw(TextureManager.LoadTexture2D("Anchor"), entite.Rectangle, null, new Color(0, 150, 250, 100), entite.Body.Rotation, vecCenter, SpriteEffects.None, 0f);
                }
                //---

                //this.spriteBatch.End();
            }

            if (entite.Selected && entite.ListParticleSystem.Count > 0)
            {
                //--- Rendu du système de particule
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    ParticleSystem pSystem = entite.ListParticleSystem[j];

                    //this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

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

                    //this.spriteBatch.End();
                }
                //---
            }
        }

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

            GraphicsDevice.Clear(Color.White);

            this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, repository.Camera.MatrixTransformation);

            //--- Réinitialisation du renderstate - Le SpriteBatch modifie le renderstate
            //this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);
            //effect.Begin();

            //            Rectangle recScreen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];
                DrawEntite(entite);
            }

            //--- Cadre physique
            if (repository.showPhysic)
                repository.PhysicsSimulatorView.Draw(spriteBatch);
            //---

            this.spriteBatch.End();

            this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState);

            //--- Pointeur de la souris
            this.spriteBatch.Draw(TextureManager.LoadTexture2D("Pointer"), repository.pointerDraw, null, Color.Red);
            //---

            //--- Pointeur secondaire de la souris
            this.spriteBatch.Draw(TextureManager.LoadTexture2D("Pointer"), repository.pointerDraw2, null, Color.Green);
            //---

            //--- Pointeurs multi
            for (int i = 0; i < repository.ListSelection.Count; i++)
            {
                this.spriteBatch.Draw(TextureManager.LoadTexture2D("Pointer"), repository.ListSelection[i].PointerDraw, null, Color.Blue);
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
