using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit2D.Trigger;

namespace Edit2D
{
    public class World : ITriggerHandler
    {
        public List<TriggerBase> ListTrigger { get; set; }

        public World()
        {
            this.ListTrigger = new List<TriggerBase>();
        }
    }
}
