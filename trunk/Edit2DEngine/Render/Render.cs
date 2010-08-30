using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Edit2DEngine.Entities.Particles;
using Microsoft.Xna.Framework;
using Edit2DEngine.Actions;
using Microsoft.Xna.Framework.Content;
using Edit2DEngine.Entities;
using Edit2DEngine.Tools;

namespace Edit2DEngine.Render
{
    public class Render
    {
        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }
        public Repository Repository { get; set; }

        private Effect effect;

        public Render(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Repository repository, ContentManager contentManager)
        {
            this.SpriteBatch = spriteBatch;
            this.GraphicsDevice = graphicsDevice;
            this.Repository = repository;

            if(contentManager != null)
                effect = contentManager.Load<Effect>(@"Content\Shader\SpriteBatch");
        }

        #region Update
        public void Update()
        {
            UpdateEntityActionPlayer();

            if (!Repository.Pause)
            {
                UpdateEntityTrigger();
                UpdateEntityParticleSystem();

                UpdatePhysic();
            }
        }

        public void UpdatePhysic()
        {
            Repository.physicSimulator.Update(0.2f);

            //bool ist = repository.listEntity[5].geom == Repository.physicSimulator.GeomList[5];
        }

        private void UpdateEntityActionPlayer()
        {
            for (int i = 0; i < Repository.listEntity.Count; i++)
            {
                Entity entity = Repository.listEntity[i];

                UpdateActionHandlerPlayer(entity);

                for (int j = 0; j < entity.ListParticleSystem.Count; j++)
                {
                    UpdateActionHandlerPlayer(entity.ListParticleSystem[j]);

                    for (int k = 0; k < entity.ListParticleSystem[j].ListParticleTemplate.Count; k++)
                    {
                        UpdateActionHandlerPlayer(entity.ListParticleSystem[j].ListParticleTemplate[k]);
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

                        if ((Repository.Pause && (actionCurve.playAnimationState == PlayAnimationState.PlayInEditor) ||
                           (!Repository.Pause && (actionCurve.playAnimationState == PlayAnimationState.Play))))
                        {
                            //--- Lit la courbe d'animation
                            ((ActionCurve)action).UpdateAnimation();
                            //---

                            //--- Si l'entité courante est animée, met à jour le contrôle de courbe
                            //scriptControl.UpdateEntityActionPlayer(((ActionCurve)action));
                            //---
                        }
                    }
                    else if (action is ActionEvent && !Repository.Pause && ((ActionEvent)action).Playing)
                    {
                        ((ActionEvent)action).UpdateValue(Repository);
                    }
                }
            }
            //---
        }

        private void UpdateEntityTrigger()
        {
            for (int i = 0; i < Repository.listEntity.Count; i++)
            {
                Entity entity = Repository.listEntity[i];

                //--- Update Trigger
                for (int j = 0; j < entity.ListTrigger.Count; j++)
                {
                    //TODO : appeler LaunchTrigger uniquement à la fin de la boucle
                    entity.ListTrigger[j].CheckTrigger(Repository);
                }
                //---

                //--- Update Trigger des particules
                for (int j = 0; j < entity.ListParticleSystem.Count; j++)
                {
                    for (int k = 0; k < entity.ListParticleSystem[j].ListParticle.Count; k++)
                    {
                        for (int l = 0; l < entity.ListParticleSystem[j].ListParticle[k].ListTrigger.Count; l++)
                        {
                            entity.ListParticleSystem[j].ListParticle[k].ListTrigger[l].CheckTrigger(Repository);
                        }
                    }
                }
                //---
            }

            //--- Update Trigger
            for (int j = 0; j < Repository.World.ListTrigger.Count; j++)
            {
                Repository.World.ListTrigger[j].CheckTrigger(Repository);
            }
            //---
        }

        private void UpdateEntityParticleSystem()
        {
            for (int i = 0; i < Repository.listEntity.Count; i++)
            {
                Entity entity = Repository.listEntity[i];

                //--- Update Trigger
                for (int j = 0; j < entity.ListParticleSystem.Count; j++)
                {
                    //TODO : appeler LaunchTrigger uniquement à la fin de la boucle
                    entity.ListParticleSystem[j].Update();
                }
                //---
            }
        } 
        #endregion

        public void Draw()
        {
            for (int i = 0; i < Repository.listEntity.Count; i++)
            {
                DrawEntity(Repository.listEntity[i]);
            }
        }

        private void DrawEntity(Entity entity)
        {
            foreach (EntityComponent entityComponent in entity.ListEntityComponent)
            {
                if (entityComponent is EntitySprite)
                    DrawEntitySprite((EntitySprite)entityComponent);
            }

            if (entity.ListParticleSystem.Count > 0)
            {
                //--- Rendu du système de particule
                for (int j = 0; j < entity.ListParticleSystem.Count; j++)
                {
                    ParticleSystem pSystem = entity.ListParticleSystem[j];

                    ////--- Rendu de l'angle d'émission
                    //if (entity.Selected)
                    //{
                    //    this.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

                    //    Vector2 vecStart1 = entity.Position;
                    //    Vector2 vecStart2 = entity.Position;

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
                        DrawEntity(pSystem.ListParticle[k]);
                    }
                }
                //---

            }
        }

        private void DrawEntitySprite(EntitySprite entitySprite)
        {
            //--- Texture de l'entité
            //effect.Begin();
            //effect.Parameters["isInBackground"].SetValue(entity.IsInBackground);
            //effect.Parameters["blurFactor"].SetValue(entity.BlurFactor);

            //int idTechnique = 0;

            //if (entity.BlurFactor > 0f)
            //    idTechnique = 1;
            //if(entity.IsInBackground)
            //    effect
            //---

            //---
            //Vector2 vecScale = new Vector2();

            //vecScale.X = entity.Rectangle.Width / recScreen.Width;
            //vecScale.X = entity.Rectangle.Height / recScreen.Height;


            //effect.Techniques[idTechnique].Passes[0].Begin();


            //Texture2D texture = null;

            //if (entity is Particle)
            //{
            //    texture = TextureManager.LoadParticleTexture2D(entity.TextureName);
            //}
            //else
            //{
            //    texture = TextureManager.LoadTexture2D(entity.TextureName);
            //}

            //Rectangle recDraw = new Rectangle(entity.Rectangle.X, entity.Rectangle.Y, entity.Rectangle.Width, entity.Rectangle.Height);

            //float PerspectiveFactor = 20f;
            //Vector2 drawPosition = (entity.Position - repository.Camera.Position) / PerspectiveFactor * (float)entity.Layer;

            ////if (entity.Layer != 0)
            ////{
            ////    int a = 0;
            ////}

            //recDraw.X += (int)drawPosition.X;

            //this.SpriteBatch.Draw(texture, recDraw, null, entity.Color, entity.Body.Rotation, entity.Center, SpriteEffects.None, 1f);
            //this.spriteBatch.Draw(TextureManager.LoadTexture2D(entity.TextureName), entity.Position, null, entity.Color, entity.Body.Rotation, entity.Center, 1f, SpriteEffects.None, 1f);

            //effect.Techniques[idTechnique].Passes[0].End();

            //effect.End();
            //---

            //-------------------------------------------------------
            EffectPass pass = null;

            if (entitySprite.BlurFactor != 0f)
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

            effect.Parameters["isInBackground"].SetValue(entitySprite.IsInBackground);
            effect.Parameters["blurFactor"].SetValue(entitySprite.BlurFactor);
            effect.Parameters["timeMS"].SetValue(DateTime.Now.Millisecond);

            effect.Begin();
            pass.Begin();

            Texture2D texture = null;

            //--- TODO : section à supprimer en homogénisant les systèmes de particules et les entités sprites
            //if (entitySprite is Particle)
            //{
            //    texture = TextureManager.LoadParticleTexture2D(entitySprite.TextureName);
            //}
            //else
            //---
            {
                texture = TextureManager.LoadTexture2D(entitySprite.TextureName);
            }


            Rectangle recDraw = new Rectangle(entitySprite.Rectangle.X, entitySprite.Rectangle.Y, entitySprite.Rectangle.Width, entitySprite.Rectangle.Height);
            float PerspectiveFactor = 20f;
            Vector2 drawPosition = (entitySprite.AbsolutePosition - Repository.Camera.Position) / PerspectiveFactor * (float)entitySprite.EntityParent.Layer;
            recDraw.X += (int)drawPosition.X;

            this.SpriteBatch.Draw(texture, recDraw, null, entitySprite.Color, entitySprite.Body.Rotation, entitySprite.Center, SpriteEffects.None, 1f);

            pass.End();
            effect.End();
            //---------------------------------------

            //if (((!repository.pause && repository.IsEntityClickableOnPlay) || repository.pause) && ((repository.showPhysic && entity.IsStatic) || entity.Selected))
            //{
            //    this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            //    //--- Pin static
            //    if (repository.showPhysic && entity.IsStatic)
            //    {
            //        this.spriteBatch.Draw(TextureManager.LoadTexture2D("Pin"), new Rectangle((int)entity.Position.X, (int)entity.Position.Y, 10, 16), null, Color.White, entity.Body.Rotation,
            //            entity.SizeVector / 2 + new Vector2(5, 8), SpriteEffects.None, 1f);
            //    }
            //    //---

            //    //--- Cadre de sélection
            //    if (entity.Selected)
            //    {
            //        float ratioX = (float)entity.Size.Width / (float)entity.NativeImageSize.Width;
            //        float ratioY = (float)entity.Size.Height / (float)entity.NativeImageSize.Height;

            //        Vector2 vecCenter = Vector2.Zero;
            //        vecCenter.X = 5f * entity.Center.X / (float)entity.Size.Width * ratioX;
            //        vecCenter.Y = 5f * entity.Center.Y / (float)entity.Size.Height * ratioY;

            //        this.spriteBatch.Draw(TextureManager.LoadTexture2D("Anchor"), entity.Rectangle, null, new Color(0, 150, 250, 100), entity.Body.Rotation, vecCenter, SpriteEffects.None, 1f);
            //    }
            //    //---

            //    this.spriteBatch.End();
            //}

            //if (entity.Selected && entity.ListParticleSystem.Count > 0)
            //{
            //    //--- Rendu du système de particule
            //    for (int j = 0; j < entity.ListParticleSystem.Count; j++)
            //    {
            //        ParticleSystem pSystem = entity.ListParticleSystem[j];

            //        this.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            //        Vector2 vecStart1 = entity.Position;
            //        Vector2 vecStart2 = entity.Position;

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
