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

namespace Edit2DEngine.Entities
{
    public class Entity : IActionHandler, ITriggerHandler, IMoveableObject, IResizeableObject
    {
        public Microsoft.Xna.Framework.Rectangle Rectangle { get; set; }

        public String Name { get; set; }
        [Browsable(false)]
        public Boolean Selected { get; set; }

        [Browsable(true), AttributeAction]
        public Microsoft.Xna.Framework.Vector2 Center { get; set; }

        [Browsable(true), AttributeAction]
        public float Rotation { get; set; }

        [Browsable(true), AttributeAction]
        public Microsoft.Xna.Framework.Vector2 Position { get; set; }

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
            this.ListEntityComponent = new List<EntityComponent>();
            this.ListParticleSystem = new List<ParticleSystem>();
            this.ListCustomProperties = new Dictionary<string, object>();
            this.ListScript = new List<Script>();
            this.ListTrigger = new List<TriggerBase>();
        }

        public Entity()
        {
        }

        #region ICloneable Members

        public Entity Clone()
        {
            //TODO : gérer la méthode de clone de Entity
            return null;
        }

        #endregion

        #region IResizeableObject Membres

        public Vector2 Size
        {
            get;
            set;
        }

        #endregion
    }
}