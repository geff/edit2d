using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Edit2DEngine.Action;

namespace Edit2DEngine.Entities.Particles
{
    public class ParticleSystem : IActionHandler
    {
        [Browsable(false)]
        public Entity Entity { get; set; }

        //public String Name { get { return String.Format("{0}-{1}", Entity.Name, ParticleSystemName); } set{} }
        public String Name { get; set; }
        [Browsable(false)]
        public List<Particle> ListParticle { get; set; }
        [Browsable(false)]
        public List<Particle> ListParticleTemplate { get; set; }
        [Browsable(true), AttributeAction]
        public float FieldAngle { get; set; }
        [Browsable(true), AttributeAction]
        public Boolean EmmittingFromAllSurface { get; set; }
        [Browsable(true), AttributeAction]
        public int Rate { get; set; }
        [Browsable(true), AttributeAction]
        public float Velocity { get; set; }
        [Browsable(true), AttributeAction]
        public float EmmittingAngle { get; set; }
        [Browsable(true), AttributeAction]
        public bool Enabled { get; set; }

        private TimeSpan lastEmitting;
        private Random rnd;

        public ParticleSystem(Entity entity)
        {
            this.ListParticle = new List<Particle>();
            this.ListParticleTemplate = new List<Particle>();

            this.Entity = entity;
            this.EmmittingAngle = -MathHelper.PiOver4;
            this.FieldAngle = MathHelper.TwoPi / 3;
            this.EmmittingFromAllSurface = false;
            this.Rate = 500;
            this.Velocity = 1500f;
            this.Enabled = true;

            //TODO gérer les collisions exlusives
            //this.Entity.geom.CollisionGroup = this.Entity.UniqueId;
            this.lastEmitting = DateTime.Now.TimeOfDay;
            this.rnd = new Random();
            this.ListScript = new List<Script>();
        }

        public void Update()
        {
            if (ListParticleTemplate.Count == 0)
                return;

            TimeSpan time = DateTime.Now.TimeOfDay;
            int elapsedTime = (int)time.Subtract(lastEmitting).TotalMilliseconds;

            if (this.Enabled && elapsedTime >= Rate)
            {
                EmitParticle(time);
            }

            for (int i = 0; i < ListParticle.Count; i++)
            {
                Particle particle = ListParticle[i];

                if (time.Subtract(particle.TimeEmitting).TotalMilliseconds >= particle.LifeTime)
                {
                    particle.IsAlive = false;

                    Repository.physicSimulator.Remove(particle.geom);
                    Repository.physicSimulator.Remove(particle.Body);
                }
            }

            ListParticle.RemoveAll(particle => !particle.IsAlive);
        }

        private void EmitParticle(TimeSpan time)
        {
            this.lastEmitting = time;

            int indexParticle = (int)rnd.Next(0, ListParticleTemplate.Count);
            float angleRnd = (float)rnd.NextDouble() * FieldAngle;
            float angle = EmmittingAngle - FieldAngle / 2f + angleRnd;
            Vector2 vecForce = new Vector2(this.Velocity * (float)Math.Cos(angle), this.Velocity * (float)Math.Sin(angle));
            Vector2 position = new Vector2();

            if (this.EmmittingFromAllSurface)
            {
                position.X = this.Entity.Rectangle.Left + (int)rnd.Next(this.Entity.Rectangle.Width) - this.Entity.Rectangle.Width / 2f;
                position.Y = this.Entity.Rectangle.Top + (int)rnd.Next(this.Entity.Rectangle.Height) - this.Entity.Rectangle.Height / 2f;
            }
            else
            {
                position = this.Entity.Position;
            }

            Particle particleTemplate = ListParticleTemplate[indexParticle];

            Particle particle = particleTemplate.Clone(true);
            particle.Position = position;
            particle.Body.ApplyForce(vecForce);
            particle.Rotation = -MathHelper.PiOver2 + angle;

            particle.FrictionCoefficient = 0.005f;
            particle.RestitutionCoefficient = 0.005f;
            particle.Body.MinimumVelocity = 1f;
            particle.Body.Mass = 5f;
            particle.Body.MomentOfInertia = 2;
            particle.TimeEmitting = this.lastEmitting;
            //particle.Rotation = -angle;

            //particle.geom.CollidesWith = FarseerGames.FarseerPhysics.CollisionCategory.All & ~ Entity.geom.CollisionCategories;
            particle.geom.CollisionGroup = this.Entity.geom.CollisionGroup;

            ListParticle.Add(particle);
        }

        #region IActionHandler Membres
        [Browsable(false)]
        public List<Script> ListScript {get;set;}

        #endregion

        public object Clone()
        {
            ParticleSystem pSystemClone = new ParticleSystem(this.Entity);
            pSystemClone.EmmittingAngle = this.EmmittingAngle;
            pSystemClone.EmmittingFromAllSurface = this.EmmittingFromAllSurface;
            pSystemClone.FieldAngle = this.FieldAngle;
            //pSystemClone.ParticleSystemName = this.ParticleSystemName;
            pSystemClone.Name = this.Name;
            pSystemClone.Rate = this.Rate;
            pSystemClone.Velocity = this.Velocity;
            pSystemClone.Enabled = this.Enabled;

            //for (int i = 0; i < this.ListParticle.Count; i++)
            //{
            //    pSystemClone.ListParticle.Add((Particle)this.ListParticle[i].Clone());
            //}

            for (int i = 0; i < this.ListScript.Count; i++)
            {
                //--- Scripts & Curves
                for (int j = 0; j < this.ListScript.Count; j++)
                {
                    Script script = this.ListScript[j];

                    Script scriptClone = new Script(script.ScriptName, pSystemClone);
                    //scriptClone.Duration = script.Duration;
                    pSystemClone.ListScript.Add(scriptClone);

                    for (int k = 0; k < script.ListAction.Count; k++)
                    {
                        ActionBase action = script.ListAction[k];

                        if (action is ActionCurve)
                        {
                            ActionCurve curve = (ActionCurve)action;
                            ActionCurve curveClone = new ActionCurve(scriptClone, curve.ActionName, curve.IsRelative, curve.IsLoop, curve.PropertyName);

                            scriptClone.ListAction.Add(curveClone);

                            for (int l = 0; l < curve.ListCurve.Count; l++)
                            {
                                Curve newCurve = new Curve();
                                curveClone.ListCurve.Add(newCurve);

                                for (int m = 0; m < curve.ListCurve[l].Keys.Count; m++)
                                {
                                    newCurve.Keys.Add(curve.ListCurve[l].Keys[0].Clone());
                                }
                            }
                        }
                        else if (action is ActionEvent)
                        {
                            //ActionEvent actionEvent = (ActionEvent)action;
                            //ActionEvent actionEventClone = new ActionEvent(actionEvent.Script, actionEvent.ActionName, actionEvent.IsRelative, actionEvent.PropertyName);
                            //actionEventClone.Value = actionEvent.Value;
                            //actionEventClone.ChangeValue = actionEvent.ChangeValue;

                            //scriptClone.ListAction.Add(actionEventClone);
                        }
                    }
                }
                //---
            }

            return pSystemClone;
        }
    }
}
