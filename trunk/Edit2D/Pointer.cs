using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Edit2DEngine;

namespace Edit2D
{
    public class Pointer
    {
        public Vector2 WorldPosition { get; set; }
        public Vector2 ScreenPosition { get; set; }

        public Vector2 PrevWorldPosition { get; set; }
        public Vector2 PrevScreenPosition { get; set; }

        public Pointer()
        {
            WorldPosition = Vector2.Zero;
            ScreenPosition = Vector2.Zero;

            PrevWorldPosition = Vector2.Zero;
            PrevScreenPosition = Vector2.Zero;
        }

        public void CalcMousePointerLocation(System.Drawing.Point location, Camera camera)
        {
            CalcMousePointerLocation(new Vector2(location.X, location.Y), camera);
        }

        public void CalcMousePointerLocation(Vector2 location, Camera camera)
        {
            Vector3 vecLocation = new Vector3(location.X, location.Y, 0f);
            Vector3 vecLocationTransformed = Vector3.Transform(vecLocation, Matrix.Invert(camera.MatrixTransformation));

            ScreenPosition = location;
            WorldPosition = new Vector2(vecLocationTransformed.X, vecLocationTransformed.Y);
        }

        public void SaveState()
        {
            this.PrevWorldPosition = this.WorldPosition;
            this.PrevScreenPosition = this.ScreenPosition;
        }

        public void CalcScreenPositionFromWorldPosition(Camera camera)
        {
            //TODO : affiner le calcul
            Vector3 vecLocation = Vector3.Transform(new Vector3(WorldPosition, 0f), camera.MatrixTransformation);

            ScreenPosition = new Vector2(vecLocation.X, vecLocation.Y);
        }
    }
}
