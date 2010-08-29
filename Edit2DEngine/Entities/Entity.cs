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
using Edit2DEngine.Trigger;
using Edit2DEngine.Action;
using Edit2DEngine.Entities.Particles;
using System.Drawing;
using Edit2DEngine.Tools;

namespace Edit2DEngine.Entities
{
    public class Entity : ICloneable, IActionHandler, ITriggerHandler
    {
        public String Name { get; set; }
        [Browsable(false)]
        public Boolean Selected { get; set; }

        [Browsable(true), AttributeAction]
        public Microsoft.Xna.Framework.Vector2 Center { get; set; }

        [Browsable(true), AttributeAction]
        public float Rotation { get; set; }

        [Browsable(true), AttributeAction]
        public Microsoft.Xna.Framework.Vector2 Position { get; set; }

        [Browsable(true), AttributeAction, Category("Physic")]
        public Boolean IsStatic { get; set; }

        [Browsable(false)]
        public List<IEntityComponent> ListEntityComponent { get; set; }

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

        [Browsable(false)]
        public bool SupportTriggerChangedValue
        {
            get { throw new NotImplementedException(); }
        }

        [Browsable(false)]
        public bool SupportTriggerCollision
        {
            get { throw new NotImplementedException(); }
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

                foreach (IEntityComponent entityComponent in this.ListEntityComponent)
                {
                    entityComponent.ChangeLayer();
                }
            }
        }

        public Entity(string name)
        {
            this.Name = name;
        }

        #region ICloneable Members

        public object Clone()
        {
            //TODO : gérer la méthode de clone de Enity
            return null;
        }

        #endregion

    }
}