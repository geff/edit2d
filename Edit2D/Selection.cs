using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit2DEngine;
using Microsoft.Xna.Framework;

namespace Edit2D
{
    public class Selection
    {
        public Entite TempEntite { get; set; }
        //public Vector2 TempPointer { get; set; }
        //public Vector2 TempPointerDraw { get; set; }

        public Entite Entite { get; set; }
        //public Vector2 Pointer { get; set; }
        //public Vector2 PointerDraw { get; set; }

        public Pointer Pointer { get; set; }

        public Selection(Entite entite, Vector2 worldPointer, Vector2 screePointer)
        {
            this.Entite = entite;
            this.Pointer = new Pointer();
            this.Pointer.WorldPosition = worldPointer;
            this.Pointer.ScreenPosition = screePointer;
        }
    }
}
