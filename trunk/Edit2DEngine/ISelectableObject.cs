using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Edit2DEngine
{
    public interface ISelectableObject
    {
        Boolean Selected { get; set; }

        bool ContainsLocation(Vector2 pos);
    }
}
