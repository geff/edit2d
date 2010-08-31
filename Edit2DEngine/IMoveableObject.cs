using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Edit2DEngine
{
    public interface IMoveableObject
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
    }
}
