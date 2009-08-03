using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit2D.Trigger
{
    public interface ITriggerHandler
    {
        List<TriggerBase> ListTrigger { get; set; }
    }
}
