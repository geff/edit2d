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
        public float OldZoom { get; set; }
        public Vector2 Focal { get; set; }
        public Vector2 OldCorner { get; set; }
        public Vector2 NewCorner { get; set; }

        public Camera()
        {
            this.Position = Vector2.Zero;
            this.Zoom = 1f;
            this.OldZoom = 1f;
            this.Focal = Vector2.Zero;
            this.OldCorner = Vector2.Zero;
            this.NewCorner = Vector2.Zero;
        }

        public Matrix MatrixTransformation
        {
            get
            {
                Matrix mtx = Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(Position.X + NewCorner.X, Position.Y + NewCorner.Y, 0f);
                return mtx;
            }
        }

        public Matrix MatrixScale
        {
            get
            {
                Matrix mtx = Matrix.CreateScale(Zoom);
                return mtx;
            }
        }

        public Matrix MatrixTranslation
        {
            get
            {
                Matrix mtx = Matrix.CreateTranslation(Position.X + NewCorner.X, Position.Y + NewCorner.Y, 0f);
                return mtx;
            }
        }
    }
}
