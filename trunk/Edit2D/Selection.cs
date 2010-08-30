using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit2DEngine;
using Microsoft.Xna.Framework;
using Edit2DEngine.Actions;
using Edit2DEngine.Entities;

namespace Edit2D
{
    public class Selection
    {
        public Entity TempEntity { get; set; }
        //public Vector2 TempPointer { get; set; }
        //public Vector2 TempPointerDraw { get; set; }

        public Entity Entity { get; set; }
        //public Vector2 Pointer { get; set; }
        //public Vector2 PointerDraw { get; set; }

        public Pointer Pointer { get; set; }

        public Selection(Entity entity, Vector2 worldPointer, Vector2 screePointer)
        {
            this.Entity = entity;
            this.Pointer = new Pointer();
            this.Pointer.WorldPosition = worldPointer;
            this.Pointer.ScreenPosition = screePointer;

        }
    }
}
