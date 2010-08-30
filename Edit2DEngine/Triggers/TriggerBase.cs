using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit2DEngine.Triggers
{
    public abstract class TriggerBase
    {
        public String TriggerName { get; set; }
        public ITriggerHandler TriggerHandler { get; set; }

        public List<Script> ListScript { get; set; }

        /// <summary>
        /// Collection utilisée lors du chargement des trigger.
        /// Permet d'associer après le chargement l'ActionHandler (Entity, ParticleSystem, World) au trigger
        /// </summary>
        public List<String> ListTargetActionHandlerName { get; set; }
        /// <summary>
        /// Collection utilisée lors du chargement des trigger.
        /// Permet d'associer après le chargement le script de l'ActionHandler (Entity, ParticleSystem, World) au trigger
        /// </summary>
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
