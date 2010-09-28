using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit2DEngine.Triggers;
using Edit2DEngine.Actions;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Edit2DEngine.CustomProperties;

namespace Edit2DEngine.Entities
{
    public abstract class EntityComponent : ITriggerHandler, IActionHandler, ICloneable, IMoveableObject, IResizeableObject, ISelectableObject, ICustomPropertyHandler, IInnerClone
    {
        public Object CloneObject { get; set; }

        public abstract Microsoft.Xna.Framework.Vector2 Center { get; set; }

        public String Name { get; set; }
        public Entity EntityParent { get; set; }
        public abstract String TreeViewPath { get; }

        public abstract void ChangeLayer();

        public abstract Object Clone();
        public abstract void ApplyForce(Vector2 force);

        public List<Script> ListScript { get; set; }
        public List<TriggerBase> ListTrigger { get; set; }
        //TODO : gérer les custom properties
        public Dictionary<string, object> ListCustomProperties { get; set; }

        [Browsable(false)]
        public Boolean Selected { get; set; }

        public abstract Boolean ContainsLocation(Vector2 location);

        public abstract Vector2 Position { get; set; }
        public abstract float Rotation { get; set; }
        public float PrevRotation { get; set; }
        public abstract Vector2 Size { get; set; }
        public abstract Rectangle Rectangle { get; }

        public void RotationFromEntityCenter(float delta)
        {
            Vector3 relativePosition = ((EntityComponent)this.CloneObject).Position.GetVector3() -  ((Entity)this.EntityParent).Position.GetVector3();

            relativePosition = Vector3.Transform(relativePosition, Matrix.CreateRotationZ(delta));

            Vector2 troncatedPosition = new Vector2();

            troncatedPosition.X = (int)(((Entity)this.EntityParent).Position.X + relativePosition.GetVector2().X);
            troncatedPosition.Y = (int)(((Entity)this.EntityParent).Position.Y + relativePosition.GetVector2().Y);

            this.Position = troncatedPosition;

            
            this.Rotation =  ((EntityComponent)this.CloneObject).Rotation+ delta;
        }


        public void SetRotation(float prevRotation, float delta)
        {
            this.Rotation = prevRotation + delta;
        }
    }
}
