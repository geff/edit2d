using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit2DEngine.Trigger;
using Microsoft.Xna.Framework.Graphics;

namespace Edit2DEngine
{
    public class World : ITriggerHandler
    {
        public List<TriggerBase> ListTrigger { get; set; }
        public Color GradientColor1 { get; set; }
        public Color GradientColor2 { get; set; }

        public World()
        {
            this.ListTrigger = new List<TriggerBase>();
            this.GradientColor1 = Color.MidnightBlue;
            this.GradientColor2 = Color.MistyRose;
        }
    }
}
