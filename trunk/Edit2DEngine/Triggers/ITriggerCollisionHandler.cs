using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Collisions;
using Edit2DEngine.Entities;

namespace Edit2DEngine.Triggers
{
    public interface ITriggerCollisionHandler : ITriggerHandler
    {
        Geom Geom { get; set; }
        String Name { get; set; }
    }
}
