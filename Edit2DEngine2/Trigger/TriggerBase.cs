using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit2DEngine.Trigger
{
    public abstract class TriggerBase
    {
        public String TriggerName { get; set; }
        public ITriggerHandler TriggerHandler { get; set; }

        public List<Script> ListScript { get; set; }
        public List<String> ListTargetScriptEntiteName { get; set; }
        public List<String> ListTargetScriptName { get; set; }

        abstract public void CheckTrigger(Repository repository);

        abstract public void InitTrigger(Repository repository);

        protected void LaunchScript(Repository repository)
        {
            for (int i = 0; i < ListScript.Count; i++)
            {
                ListScript[i].StartScript(repository);
            }
        }
    }
}
