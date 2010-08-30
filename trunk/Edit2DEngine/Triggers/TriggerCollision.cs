using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Collisions;
using Edit2DEngine.Entities;

namespace Edit2DEngine.Triggers
{
    public class TriggerCollision : TriggerBase
    {
        public ITriggerCollisionHandler TargetEntity { get; set; }
        public String TargetCollisionEntityName { get; set; }

        public TriggerCollision(String triggerName, ITriggerCollisionHandler triggerHandler, ITriggerCollisionHandler targetEntity)
        {
            this.TriggerName = triggerName;
            this.TriggerHandler = triggerHandler;

            this.ListScript = new List<Script>();
            this.ListTargetActionHandlerName = new List<string>();
            this.ListTargetScriptName = new List<string>();

            this.TargetEntity = targetEntity;
        }

        public override void InitTrigger(Repository repository)
        {
        }

        public override void CheckTrigger(Repository repository)
        {
            if (this.TargetEntity != null && this.TargetEntity.geom != null)
            {
                //if (this.Entity.geom.Collide(this.TargetEntity.geom))
                if (this.TargetEntity.geom.Collide(((ITriggerCollisionHandler)this.TriggerHandler).geom))
                {
                    LaunchScript(repository);
                }
            }
        }
    }
}
