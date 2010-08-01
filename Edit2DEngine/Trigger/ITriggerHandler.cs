using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit2DEngine.Trigger
{
    public interface ITriggerHandler
    {
        List<TriggerBase> ListTrigger { get; set; }

        String TreeViewPath { get;}
    }
}
