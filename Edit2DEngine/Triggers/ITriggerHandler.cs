﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit2DEngine.Triggers
{
    public interface ITriggerHandler
    {
        List<TriggerBase> ListTrigger { get; set; }
        String TreeViewPath { get; }
    }
}
