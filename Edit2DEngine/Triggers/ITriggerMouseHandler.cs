using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Collisions;
using Edit2DEngine.Entities;
using Microsoft.Xna.Framework;

namespace Edit2DEngine.Triggers
{
    public interface ITriggerMouseHandler : ITriggerHandler
    {
        bool ContainsLocation(Vector2 pos);
    }
}
