using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Collisions;

namespace Edit2DEngine.Trigger
{
    public class TriggerCollision : TriggerBase
    {
        public Entite TargetEntite { get; set; }
        public String TargetCollisionEntiteName { get; set; }

        public TriggerCollision(String triggerName, ITriggerHandler triggerHandler, Entite targetEntite)
        {
            this.TriggerName = triggerName;
            this.TriggerHandler = triggerHandler;

            this.ListScript = new List<Script>();
            this.ListTargetActionHandlerName = new List<string>();
            this.ListTargetScriptName = new List<string>();

            this.TargetEntite = targetEntite;
        }

        public override void InitTrigger(Repository repository)
        {
        }

        public override void CheckTrigger(Repository repository)
        {
            if (this.TargetEntite != null && this.TargetEntite.geom != null)
            {
                //if (this.Entite.geom.Collide(this.TargetEntite.geom))
                if (this.TargetEntite.geom.Collide(((Entite)this.TriggerHandler).geom))
                {
                    LaunchScript(repository);
                }
            }
        }
    }
}
