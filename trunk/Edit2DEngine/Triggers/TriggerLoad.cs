using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Edit2DEngine.Trigger
{
    public class TriggerLoad : TriggerBase
    {
        private bool IsLoaded = false;

        public TriggerLoad(String triggerName, ITriggerHandler triggerHandler)
        {
            this.TriggerName = triggerName;
            this.TriggerHandler = triggerHandler;

            this.ListScript = new List<Script>();
            this.ListTargetActionHandlerName = new List<string>();
            this.ListTargetScriptName = new List<string>();
        }

        public override void InitTrigger(Repository repository)
        {
            IsLoaded = false;
        }

        public override void CheckTrigger(Repository repository)
        {
            if (!IsLoaded)
            {
                IsLoaded = true;
                base.LaunchScript(repository);
            }
        }
    }
}
