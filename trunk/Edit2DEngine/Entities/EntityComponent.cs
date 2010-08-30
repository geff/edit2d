using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit2DEngine.Triggers;
using Edit2DEngine.Actions;
using Microsoft.Xna.Framework;

namespace Edit2DEngine.Entities
{
    public abstract class EntityComponent : ITriggerHandler, IActionHandler
    {
        public String Name { get; set; }
        public Entity EntityParent { get; set; }
        public abstract String TreeViewPath { get; }

        public abstract void ChangeLayer();

        public abstract EntityComponent Clone();
        public abstract void ApplyForce(Vector2 force);

        public List<Script> ListScript { get; set; }
        public List<TriggerBase> ListTrigger { get; set; }
        //TODO : gérer les cutsom properties
        public Dictionary<string, object> ListCustomProperties { get; set; }
    }
}
