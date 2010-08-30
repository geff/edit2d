using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Edit2DEngine.Actions
{
    public abstract class ActionBase
    {
        [Browsable(true)]
        public String ActionName { get; set; }
        [Browsable(false)]
        public Script Script { get; set; }

        public abstract void InitAction();
    }
}
