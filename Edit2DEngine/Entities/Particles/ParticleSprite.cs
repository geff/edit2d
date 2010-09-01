using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Edit2DEngine.Entities.Particles
{
    public class ParticleSprite : EntitySprite, IParticle
    {
        [Category("Particle")]
        public int LifeTime { get; set; }

        [Browsable(false), Category("Particle")]
        public bool IsAlive { get; set; }

        [Browsable(false)]
        public ParticleSystem ParticleSystem { get; set; }

        [Browsable(false)]
        public TimeSpan TimeEmitting { get; set; }

        public ParticleSprite(bool linkToPhysiSimulator, bool isCollisionable, string textureName, string name, Entity entityParent):
            base(linkToPhysiSimulator, isCollisionable, textureName, name, entityParent)
        {
        }
    }
}
