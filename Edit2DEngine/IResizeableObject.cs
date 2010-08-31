using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Edit2DEngine
{
    public interface IResizeableObject
    {
        Vector2 Size { get; set; }
    }
}
