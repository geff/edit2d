using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Edit2DEngine.Triggers
{
    public class TriggerTime : TriggerBase
    {
        public int TimeLoop { get; set; }
        private TimeSpan lastExecution = TimeSpan.Zero;

        public TriggerTime(String triggerName, ITriggerHandler triggerHandler)
        {
            this.TriggerName = triggerName;
            this.TriggerHandler = triggerHandler;

            this.ListScript = new List<Script>();
            this.ListTargetActionHandlerName = new List<string>();
            this.ListTargetScriptName = new List<string>();
        }

        public override void InitTrigger(Repository repository)
        {
        }

        public override void CheckTrigger(Repository repository)
        {
            if (TimeLoop ==0)
            {
                base.LaunchScript(repository);
            }
            else
            {
                TimeSpan now = DateTime.Now.TimeOfDay;

                if(lastExecution.TotalMilliseconds +TimeLoop <= now.TotalMilliseconds)
                {
                    lastExecution = now;
                    base.LaunchScript(repository);
                }
            }
        }
    }
}
