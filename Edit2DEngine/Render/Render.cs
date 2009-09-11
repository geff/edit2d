using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Edit2DEngine.Particles;
using Microsoft.Xna.Framework;
using Edit2DEngine.Action;
using Microsoft.Xna.Framework.Content;

namespace Edit2DEngine.Render
{
    public class Render
    {
        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }
        public Repository repository { get; set; }

        private Effect effect;

        public Render(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Repository repository, ContentManager contentManager)
        {
            this.SpriteBatch = spriteBatch;
            this.GraphicsDevice = graphicsDevice;
            this.repository = repository;

            if(contentManager != null)
                effect = contentManager.Load<Effect>(@"Content\Shader\SpriteBatch");
        }

        #region Update
        public void Update()
        {
            UpdateEntityActionPlayer();

            if (!repository.Pause)
            {
                UpdateEntityTrigger();
                UpdateEntityParticleSystem();

                UpdatePhysic();
            }
        }

        public void UpdatePhysic()
        {
            Repository.physicSimulator.Update(0.2f);

            //bool ist = repository.listEntite[5].geom == Repository.physicSimulator.GeomList[5];
        }

        private void UpdateEntityActionPlayer()
        {
            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];

                UpdateActionHandlerPlayer(entite);

                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    UpdateActionHandlerPlayer(entite.ListParticleSystem[j]);

                    for (int k = 0; k < entite.ListParticleSystem[j].ListParticleTemplate.Count; k++)
                    {
                        UpdateActionHandlerPlayer(entite.ListParticleSystem[j].ListParticleTemplate[k]);
                    }
                }
            }
        }

        private void UpdateActionHandlerPlayer(IActionHandler actionHandler)
        {
            //--- Update Script
            for (int j = 0; j < actionHandler.ListScript.Count; j++)
            {
                for (int k = 0; k < actionHandler.ListScript[j].ListAction.Count; k++)
                {
                    ActionBase action = actionHandler.ListScript[j].ListAction[k];

                    if (action is ActionCurve && ((ActionCurve)action).playAnimationState != PlayAnimationState.Stop)
                    {
                        ActionCurve actionCurve = (ActionCurve)action;

                        if ((repository.Pause && (actionCurve.playAnimationState == PlayAnimationState.PlayInEditor) ||
                           (!repository.Pause && (actionCurve.playAnimationState == PlayAnimationState.Play))))
                        {
                            //--- Lit la courbe d'animation
                            ((ActionCurve)action).UpdateAnimation();
                            //---

                            //--- Si l'entité courante est animée, met à jour le contrôle de courbe
                            //scriptControl.UpdateEntityActionPlayer(((ActionCurve)action));
                            //---
                        }
                    }
                    else if (action is ActionEvent && !repository.Pause && ((ActionEvent)action).Playing)
                    {
                        ((ActionEvent)action).UpdateValue(repository);
                    }
                }
            }
            //---
        }

        private void UpdateEntityTrigger()
        {
            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];

                //--- Update Trigger
                for (int j = 0; j < entite.ListTrigger.Count; j++)
                {
                    //TODO : appeler LaunchTrigger uniquement à la fin de la boucle
                    entite.ListTrigger[j].CheckTrigger(repository);
                }
                //---

                //--- Update Trigger des particules
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    for (int k = 0; k < entite.ListParticleSystem[j].ListParticle.Count; k++)
                    {
                        for (int l = 0; l < entite.ListParticleSystem[j].ListParticle[k].ListTrigger.Count; l++)
                        {
                            entite.ListParticleSystem[j].ListParticle[k].ListTrigger[l].CheckTrigger(repository);
                        }
                    }
                }
                //---
            }

            //--- Update Trigger
            for (int j = 0; j < repository.World.ListTrigger.Count; j++)
            {
                repository.World.ListTrigger[j].CheckTrigger(repository);
            }
            //---
        }

        private void UpdateEntityParticleSystem()
        {
            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];

                //--- Update Trigger
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    //TODO : appeler LaunchTrigger uniquement à la fin de la boucle
                    entite.ListParticleSystem[j].Update();
                }
                //---
            }
        } 
        #endregion

        public void Draw()
        {
            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];

                DrawEntite(entite);
            }
        }

        private void DrawEntite(Entite entite)
        {
            if (entite.ListParticleSystem.Count > 0)
            {

                //--- Rendu du système de particule
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    ParticleSystem pSystem = entite.ListParticleSystem[j];

                    ////--- Rendu de l'angle d'émission
                    //if (entite.Selected)
                    //{
                    //    this.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

                    //    Vector2 vecStart1 = entite.Position;
                    //    Vector2 vecStart2 = entite.Position;

                    //    Vector2 vecEnd1 = new Vector2();
                    //    Vector2 vecEnd2 = new Vector2();

                    //    //float angle = Vector2.UnitX.GetAngle(pSystem.EmittingVector);

                    //    float rayon = 30f;

                    //    vecEnd1 = vecStart1 + new Vector2(rayon * (float)Math.Cos(pSystem.EmmittingAngle + pSystem.FieldAngle / 2f), rayon * (float)Math.Sin(pSystem.EmmittingAngle + pSystem.FieldAngle / 2f));
                    //    vecEnd2 = vecStart2 + new Vector2(rayon * (float)Math.Cos(pSystem.EmmittingAngle - pSystem.FieldAngle / 2f), rayon * (float)Math.Sin(pSystem.EmmittingAngle - pSystem.FieldAngle / 2f));

                    //    line.Draw(SpriteBatch, vecStart1, vecEnd1);
                    //    line.Draw(SpriteBatch, vecStart2, vecEnd2);
                    //    line.Draw(SpriteBatch, vecEnd1, vecEnd2);

                    //    this.SpriteBatch.End();

                    //}

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
            //if(entite.IsInBackground)
            //    effect
            //---

            //---
            //Vector2 vecScale = new Vector2();

            //vecScale.X = entite.Rectangle.Width / recScreen.Width;
            //vecScale.X = entite.Rectangle.Height / recScreen.Height;


            //effect.Techniques[idTechnique].Passes[0].Begin();


            //Texture2D texture = null;

            //if (entite is Particle)
            //{
            //    texture = TextureManager.LoadParticleTexture2D(entite.TextureName);
            //}
            //else
            //{
            //    texture = TextureManager.LoadTexture2D(entite.TextureName);
            //}

            //Rectangle recDraw = new Rectangle(entite.Rectangle.X, entite.Rectangle.Y, entite.Rectangle.Width, entite.Rectangle.Height);

            //float PerspectiveFactor = 20f;
            //Vector2 drawPosition = (entite.Position - repository.Camera.Position) / PerspectiveFactor * (float)entite.Layer;

            ////if (entite.Layer != 0)
            ////{
            ////    int a = 0;
            ////}

            //recDraw.X += (int)drawPosition.X;

            //this.SpriteBatch.Draw(texture, recDraw, null, entite.Color, entite.Body.Rotation, entite.Center, SpriteEffects.None, 1f);
            //this.spriteBatch.Draw(TextureManager.LoadTexture2D(entite.TextureName), entite.Position, null, entite.Color, entite.Body.Rotation, entite.Center, 1f, SpriteEffects.None, 1f);

            //effect.Techniques[idTechnique].Passes[0].End();

            //effect.End();
            //---

            //-------------------------------------------------------
            EffectPass pass = null;

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

            effect.Parameters["isInBackground"].SetValue(entite.IsInBackground);
            effect.Parameters["blurFactor"].SetValue(entite.BlurFactor);
            effect.Parameters["timeMS"].SetValue(DateTime.Now.Millisecond);

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

            
            Rectangle recDraw = new Rectangle(entite.Rectangle.X, entite.Rectangle.Y, entite.Rectangle.Width, entite.Rectangle.Height);
            float PerspectiveFactor = 20f;
            Vector2 drawPosition = (entite.Position - repository.Camera.Position) / PerspectiveFactor * (float)entite.Layer;
            recDraw.X += (int)drawPosition.X;

            this.SpriteBatch.Draw(texture, recDraw, null, entite.Color, entite.Body.Rotation, entite.Center, SpriteEffects.None, 1f);

            pass.End();
            effect.End();
            //---------------------------------------

            //if (((!repository.pause && repository.IsEntityClickableOnPlay) || repository.pause) && ((repository.showPhysic && entite.IsStatic) || entite.Selected))
            //{
            //    this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            //    //--- Pin static
            //    if (repository.showPhysic && entite.IsStatic)
            //    {
            //        this.spriteBatch.Draw(TextureManager.LoadTexture2D("Pin"), new Rectangle((int)entite.Position.X, (int)entite.Position.Y, 10, 16), null, Color.White, entite.Body.Rotation,
            //            entite.SizeVector / 2 + new Vector2(5, 8), SpriteEffects.None, 1f);
            //    }
            //    //---

            //    //--- Cadre de sélection
            //    if (entite.Selected)
            //    {
            //        float ratioX = (float)entite.Size.Width / (float)entite.NativeImageSize.Width;
            //        float ratioY = (float)entite.Size.Height / (float)entite.NativeImageSize.Height;

            //        Vector2 vecCenter = Vector2.Zero;
            //        vecCenter.X = 5f * entite.Center.X / (float)entite.Size.Width * ratioX;
            //        vecCenter.Y = 5f * entite.Center.Y / (float)entite.Size.Height * ratioY;

            //        this.spriteBatch.Draw(TextureManager.LoadTexture2D("Anchor"), entite.Rectangle, null, new Color(0, 150, 250, 100), entite.Body.Rotation, vecCenter, SpriteEffects.None, 1f);
            //    }
            //    //---

            //    this.spriteBatch.End();
            //}

            //if (entite.Selected && entite.ListParticleSystem.Count > 0)
            //{
            //    //--- Rendu du système de particule
            //    for (int j = 0; j < entite.ListParticleSystem.Count; j++)
            //    {
            //        ParticleSystem pSystem = entite.ListParticleSystem[j];

            //        this.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            //        Vector2 vecStart1 = entite.Position;
            //        Vector2 vecStart2 = entite.Position;

            //        Vector2 vecEnd1 = new Vector2();
            //        Vector2 vecEnd2 = new Vector2();

            //        //float angle = Vector2.UnitX.GetAngle(pSystem.EmittingVector);

            //        float rayon = 30f;

            //        vecEnd1 = vecStart1 + new Vector2(rayon * (float)Math.Cos(pSystem.EmmittingAngle + pSystem.FieldAngle / 2f), rayon * (float)Math.Sin(pSystem.EmmittingAngle + pSystem.FieldAngle / 2f));
            //        vecEnd2 = vecStart2 + new Vector2(rayon * (float)Math.Cos(pSystem.EmmittingAngle - pSystem.FieldAngle / 2f), rayon * (float)Math.Sin(pSystem.EmmittingAngle - pSystem.FieldAngle / 2f));

            //        //line.Draw(spriteBatch, vecStart1, vecEnd1);
            //        //line.Draw(spriteBatch, vecStart2, vecEnd2);
            //        //line.Draw(spriteBatch, vecEnd1, vecEnd2);

            //        this.SpriteBatch.End();
            //    }
            //    //---
            //}
        }
    }
}
