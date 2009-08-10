using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Edit2DEngine
{
    public static class MathTools
    {
        public static float GetAngle(float Xa, float Ya, float Xb, float Yb)
        {
            return -(float)System.Math.Atan2(Xb - Xa, Yb - Ya) + MathHelper.PiOver2;
        }
    }
}
