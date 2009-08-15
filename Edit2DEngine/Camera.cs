using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Edit2DEngine
{
    public class Camera
    {
        public Vector2 Position { get; set; }
        public float Zoom { get; set; }
        public Vector2 Focal { get; set; }
        public Vector2 FocalZ { get; set; }

        public Camera()
        {
            this.Position = Vector2.Zero;
            this.Zoom = 1f;
            this.Focal = Vector2.Zero;
            this.FocalZ = Vector2.Zero;
        }
    }
}
