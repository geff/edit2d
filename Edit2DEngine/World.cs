using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using Edit2DEngine.Triggers;

namespace Edit2DEngine
{
    public class World : ITriggerMouseHandler
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
        public bool SupportTriggerChangedValue
        {
            get
            {
                return false;
            }
        }

        [Browsable(false)]
        public bool SupportTriggerCollision
        {
            get
            {
                return false;
            }
        }

        public bool ContainsLocation(Microsoft.Xna.Framework.Vector2 pos)
        {
            return false;
        }
    }
}
