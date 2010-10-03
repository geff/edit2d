using System;
using System.Collections.Generic;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics.Dynamics.Joints;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;

using Edit2DEngine.Actions;
using Edit2DEngine.Entities.Particles;
using System.Drawing;
using Edit2DEngine.Tools;
using Edit2DEngine.Triggers;
using Edit2DEngine.CustomProperties;

namespace Edit2DEngine.Entities
{
    public class Entity : IActionHandler, ITriggerHandler, ITriggerMouseHandler, IMoveableObject, IResizeableObject, ISelectableObject, ICustomPropertyHandler, ICloneable, IInnerClone
    {
        private Boolean _isClone = false;
        private Vector2 _position = Vector2.Zero;
        private float _rotation = 0f;
        public Object CloneObject { get; set; }

        public Microsoft.Xna.Framework.Rectangle Rectangle { get; set; }
        public int UniqueId { get; set; }

        public String Name { get; set; }
        [Browsable(false)]
        public Boolean Selected { get; set; }

        [Browsable(true), AttributeAction, Category("Center")]
        public Microsoft.Xna.Framework.Vector2 Center
        {
            get
            {
                //---> L'accesseur Set de la propriété Center se fait en référence locale
                //Vector2 troncatedCenter = new Vector2();
                //troncatedCenter.X = (int)((CenterPercent * this.Size).X);
                //troncatedCenter.Y = (int)((CenterPercent * this.Size).Y);
                //return troncatedCenter;

                return CenterPercent * this.Size;

            }
            set
            {
                //---> L'accesseur Set de la propriété Center se fait en référence World
                CenterPercent = (value - _position + this.Center) / this.Size;

                //---> Change la position de l'objet lorsque le centre est redéfini
                _position = value;
            }
        }

        [Browsable(true), AttributeAction, Category("Center")]
        public Microsoft.Xna.Framework.Vector2 CenterPercent { get; set; }

        [Browsable(true), Category("Center")]
        public Boolean CenterFixed { get; set; }

        [Browsable(true), AttributeAction]
        public float Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                float delta = value - _rotation;
                _rotation = value;

                if (!_isClone)
                {
                    foreach (EntityComponent entityComponent in this.ListEntityComponent)
                    {
                        entityComponent.RotationFromEntityCenter(Rotation - ((Entity)this.CloneObject).Rotation);
                        //---> Détermine le centre de l'entity à chaque rotation d'EntityComponent afin de ne pas avoir une déviation progressive du centre
                        this.Center = ((Entity)this.CloneObject).Position;
                    }
                }
            }
        }

        [Browsable(true), AttributeAction]
        public Microsoft.Xna.Framework.Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                Vector2 delta = value - _position;
                _position = value;

                if (!_isClone)
                {
                    foreach (EntityComponent entityComponent in this.ListEntityComponent)
                    {
                        entityComponent.Position += delta;
                    }
                }
            }
        }

        //[Browsable(true), AttributeAction, Category("Physic")]
        //public Boolean IsStatic { get; set; }

        [Browsable(false)]
        public List<EntityComponent> ListEntityComponent { get; set; }

        [Browsable(false)]
        public List<Script> ListScript { get; set; }

        [Browsable(false)]
        public List<TriggerBase> ListTrigger { get; set; }

        [Browsable(false)]
        public Dictionary<String, Object> ListCustomProperties { get; set; }

        [Browsable(false)]
        public List<ParticleSystem> ListParticleSystem { get; set; }

        [Browsable(false)]
        public string TreeViewPath
        {
            get { return "Monde\\Entités\\" + this.Name; }
        }

        private int _layer;
        [Browsable(true)]
        public int Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;

                foreach (EntityComponent entityComponent in this.ListEntityComponent)
                {
                    entityComponent.ChangeLayer();
                }
            }
        }

        public Entity(string name)
        {
            this.Name = name;
            this.CenterPercent = new Vector2(0.5f);
            this.ListEntityComponent = new List<EntityComponent>();
            this.ListParticleSystem = new List<ParticleSystem>();
            this.ListCustomProperties = new Dictionary<string, object>();
            this.ListScript = new List<Script>();
            this.ListTrigger = new List<TriggerBase>();
            this.UniqueId = ++Repository.EntityCount;
        }

        public Entity()
        {
        }

        public Object Clone()
        {
            //TODO : gérer la méthode de clone de Entity
            Entity clone = new Entity(this.Name);

            clone._isClone = true;
            clone.Name = this.Name;
            clone.Rotation = this.Rotation;
            clone.Position = this.Position;
            clone.Rectangle = this.Rectangle;
            clone.CenterPercent = this.CenterPercent;
            clone.Size = this.Size;
            //---

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

            //--- Trigger
            for (int j = 0; j < ListTrigger.Count; j++)
            {
                TriggerBase trigger = ListTrigger[j];

                //if (trigger is TriggerCollision)
                //{
                //    TriggerCollision triggerCol = (TriggerCollision)trigger;
                //    TriggerCollision cloneTrigger = new TriggerCollision(triggerCol.TriggerName, clone, triggerCol.TargetEntity);

                //    cloneTrigger.ListScript = triggerCol.ListScript;

                //    clone.ListTrigger.Add(cloneTrigger);
                //}
                if (trigger is TriggerValueChanged)
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
                clonePSystem.Name = pSystem.Name;
                //clonePSystem.ParticleSystemName = pSystem.ParticleSystemName;
                clonePSystem.Rate = pSystem.Rate;
                clonePSystem.Velocity = pSystem.Velocity;

                clone.ListParticleSystem.Add(clonePSystem);
                for (int k = 0; k < pSystem.ListParticleTemplate.Count; k++)
                {
                    Particle particle = pSystem.ListParticleTemplate[k];
                    Particle cloneParticle = (Particle)particle.Clone();

                    clonePSystem.ListParticleTemplate.Add(cloneParticle);
                }
            }
            //---

            return clone;
        }

        public void UpdateRectangle()
        {
            if (_isClone)
                return;

            float left = float.MaxValue;
            float right = float.MinValue;
            float top = float.MaxValue;
            float bottom = float.MinValue;

            foreach (EntityComponent entityComponent in this.ListEntityComponent)
            {
                if (entityComponent.Rectangle.Left < left)
                    left = entityComponent.Rectangle.Left;
                if (entityComponent.Rectangle.Right > right)
                    right = entityComponent.Rectangle.Right;

                if (entityComponent.Rectangle.Top < top)
                    top = entityComponent.Rectangle.Top;
                if (entityComponent.Rectangle.Bottom > bottom)
                    bottom = entityComponent.Rectangle.Bottom;
            }

            this.Rectangle = new Microsoft.Xna.Framework.Rectangle((int)left, (int)top, (int)(right - left), (int)(bottom - top));

            _position = new Vector2(left + this.Center.X, top + this.Center.Y);
        }

        #region IResizeableObject Membres

        public Vector2 Size
        {
            get
            {
                return new Vector2(this.Rectangle.Width, this.Rectangle.Height);
            }
            set
            {
                //TODO : ajoute le code qui permet de redimensionner l'ensemble des EntityComponent enfants
            }
        }

        #endregion

        #region ITriggerMouseHandler Membres

        public bool ContainsLocation(Vector2 pos)
        {
            return this.Rectangle.Contains((int)pos.X, (int)pos.Y);
        }

        #endregion


        public void SetRotation(float prevRotation, float delta)
        {

        }
    }
}