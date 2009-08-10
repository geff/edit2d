using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using Edit2DEngine.Action;
using Microsoft.Xna.Framework;
using Edit2DEngine.Trigger;

namespace Edit2DEngine.Particles
{
    public class Particle : Entite
    {
        [Category("Particle")]
        public int LifeTime { get; set; }

        [Browsable(false), Category("Particle")]
        public bool IsAlive { get; set; }
        
        [Browsable(false)]
        public ParticleSystem ParticleSystem { get; set; }
        
        [Browsable(false)]
        public TimeSpan TimeEmitting { get; set; }

        public Particle(bool addToPhysicSimulator, string textureName, string name, ParticleSystem particleSystem) : base(addToPhysicSimulator, textureName, name)
        {
            this.ParticleSystem = particleSystem;
            this.LifeTime = 20000;
            this.IsAlive = true;
            //base.Constructor(false, textureName, name);
        }

        protected override Texture2D GetTexture()
        {
            return TextureManager.LoadParticleTexture2D(TextureName);
        }

        //public override void ChangeTexture(string textureName, bool addToPhysicSimulator)
        //{
        //    this.TextureName = textureName;
        //    Texture2D texture = GetTexture();

        //    this.NativeImageSize = new Size(texture.Width, texture.Height);
        //    //this.Size = new Size(texture.Width, texture.Height); 
            
        //    ChangeSize(texture.Width,
        //               texture.Height,
        //               addToPhysicSimulator);
        //}

        new public Particle Clone(bool addToPhysicSimulator)
        {
            Particle clone = new Particle(false, this.TextureName, this.Name, this.ParticleSystem);

            //clone.Size = new Size(this.Size.Width, this.Size.Height);
            clone.ChangeSize(this.Size.Width, this.Size.Height, addToPhysicSimulator);

            if (clone.body != null && this.body != null)
            {
                clone.body.Rotation = this.body.Rotation;
                clone.body.Position = this.body.Position;
                clone.body.IsStatic = this.body.IsStatic;
                clone.IsStatic = this.body.IsStatic;
            }
            clone.IsColisionable = this.IsColisionable;

            clone.Name = this.Name;
            clone.TextureName = this.TextureName;
            clone.NativeImageSize = this.NativeImageSize;

            clone.BlurFactor = this.BlurFactor;
            clone.IsInBackground = this.IsInBackground;
            clone.Color = this.Color;
            clone.FrictionCoefficient = this.FrictionCoefficient;
            clone.RestitutionCoefficient = this.RestitutionCoefficient;

            //--- Scripts & Curves
            for (int j = 0; j < this.ListScript.Count; j++)
            {
                Script script = this.ListScript[j];

                Script scriptClone = new Script(script.ScriptName, clone);
                //scriptClone.Duration = script.Duration;
                clone.ListScript.Add(scriptClone);

                for (int k = 0; k < script.ListAction.Count; k++)
                {
                    ActionBase action = script.ListAction[k];

                    if (action is ActionCurve)
                    {
                        ActionCurve curve = (ActionCurve)action;
                        ActionCurve curveClone = new ActionCurve(scriptClone, curve.ActionName, curve.IsRelative, curve.IsLoop, typeof(Particle), curve.PropertyName);

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

            //--- Trigger
            for (int j = 0; j < ListTrigger.Count; j++)
            {
                TriggerBase trigger = ListTrigger[j];

                if (trigger is TriggerCollision)
                {
                    TriggerCollision triggerCol = (TriggerCollision)trigger;
                    TriggerCollision cloneTrigger = new TriggerCollision(triggerCol.TriggerName, clone, triggerCol.TargetEntite);

                    cloneTrigger.ListScript = triggerCol.ListScript;

                    clone.ListTrigger.Add(cloneTrigger);
                }
                else if (trigger is TriggerValueChanged)
                {
                    TriggerValueChanged triggerVal = (TriggerValueChanged)trigger;
                    TriggerValueChanged cloneTrigger = new TriggerValueChanged(triggerVal.TriggerName, clone, triggerVal.PropertyName, triggerVal.Sens, triggerVal.Value, triggerVal.IsCustomProperty);

                    cloneTrigger.ListScript = triggerVal.ListScript;

                    clone.ListTrigger.Add(cloneTrigger);
                }
                else if (trigger is TriggerLoad)
                {
                    TriggerLoad triggerLoad = (TriggerLoad)trigger;
                    TriggerLoad cloneTrigger = new TriggerLoad(triggerLoad.TriggerName, clone);

                    cloneTrigger.ListScript = triggerLoad.ListScript;

                    clone.ListTrigger.Add(cloneTrigger);
                }
                else if (trigger is TriggerMouse)
                {
                    TriggerMouse triggerMouse = (TriggerMouse)trigger;
                    TriggerMouse cloneTrigger = new TriggerMouse(triggerMouse.TriggerName, clone, triggerMouse.TriggerMouseType);

                    cloneTrigger.ListScript = triggerMouse.ListScript;

                    clone.ListTrigger.Add(cloneTrigger);
                }
            }
            //---

            //--- ParticleSystem
            for (int j = 0; j < ListParticleSystem.Count; j++)
            {
                ParticleSystem pSystem = ListParticleSystem[j];
                ParticleSystem clonePSystem = new ParticleSystem(clone);

                clonePSystem.EmmittingAngle = pSystem.EmmittingAngle;
                clonePSystem.EmmittingFromAllSurface = pSystem.EmmittingFromAllSurface;
                clonePSystem.FieldAngle = pSystem.FieldAngle;
                clonePSystem.ParticleSystemName = pSystem.ParticleSystemName;
                clonePSystem.Rate = pSystem.Rate;
                clonePSystem.Velocity = pSystem.Velocity;

                clone.ListParticleSystem.Add(clonePSystem);

                for (int k = 0; k < pSystem.ListParticleTemplate.Count; k++)
                {
                    Particle particle = pSystem.ListParticleTemplate[k];
                    Particle cloneParticle = particle.Clone(true);

                    clonePSystem.ListParticleTemplate.Add(cloneParticle);
                }
            }
            //---

            clone.IsAlive = true;
            clone.LifeTime = this.LifeTime;
            clone.ParticleSystem = this.ParticleSystem;

            return clone;
        }
    }
}
