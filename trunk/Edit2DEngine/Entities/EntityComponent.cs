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
    public abstract class EntityComponent : ITriggerHandler, IActionHandler, ICloneable, IMoveableObject, IResizeableObject, ISelectableObject, ICustomPropertyHandler
    {
        //[Browsable(false)]
        public abstract Microsoft.Xna.Framework.Vector2 Center{ get; set; }
 
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
        public abstract Vector2 Size { get; set; }
        public abstract Rectangle Rectangle { get; }
    }
}
