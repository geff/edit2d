using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit2DEngine.Trigger;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;

namespace Edit2DEngine
{
    public class World : ITriggerHandler
    {
        [Browsable(false)]
        public List<TriggerBase> ListTrigger { get; set; }

        public Color GradientColor1 { get; set; }
        public Color GradientColor2 { get; set; }

        public World()
        {
            this.ListTrigger = new List<TriggerBase>();
            this.GradientColor1 = Color.White;
            this.GradientColor2 = Color.White;
        }

        [Browsable(false)]
        public String TreeViewPath
        {
            get
            {
                return "Monde";
            }
        }

        [Browsable(false)]
        public bool SupportTrigerChangedValue
        {
            get
            {
                return false;
            }
        }

        [Browsable(false)]
        public bool SupportTrigerCollision
        {
            get
            {
                return false;
            }
        }
    }
}
