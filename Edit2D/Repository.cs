using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FarseerGames.GettingStarted;
using Edit2DEngine;

namespace Edit2D
{
    public class Repository : Edit2DEngine.Repository
    {
        public FrmEdit2D FrmEdit2D { get; set; }
        public PhysicsSimulatorView PhysicsSimulatorView;
        public List<Selection> ListSelection { get; set; }
        public bool ShowDebugMode = true;
        public bool keyCtrlPressed = false;
        public bool keyShiftPressed = false;
        public bool keyAltPressed = false;
        public bool IsEntityClickableOnPlay = false;

        public Pointer CurrentPointer { get; set; }
        public Pointer CurrentPointer2 { get; set; }

        public MouseMode mouseMode = MouseMode.Move;

        public bool Screenshot = false;

        public Repository()
        {
            this.ListSelection = new List<Selection>();
            this.CurrentPointer = new Pointer();
            this.CurrentPointer2 = new Pointer();
        }

        private Vector2 GetModelViewControlPosition()
        {
            Vector2 pos = Vector2.Zero;

            pos = new Vector2(FrmEdit2D.Location.X, FrmEdit2D.Location.Y) + new Vector2(FrmEdit2D.modelViewerControl.Location.X, FrmEdit2D.modelViewerControl.Location.Y);

            return pos;
        }

        public override Vector2 GetMousePosition()
        {
            MouseState mouseState = Mouse.GetState();

            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y) - GetModelViewControlPosition();

            return mousePosition;
        }

        public List<Entite> GetSelectedEntite()
        {
            List<Entite> listSelectedEntite = new List<Entite>();

            listSelectedEntite.AddRange(ListSelection.Select<Selection, Entite>(s => s.Entite));

            if (CurrentEntite != null)
                listSelectedEntite.Add(CurrentEntite);

            return listSelectedEntite;
        }
    }

    public enum MouseMode : int
    {
        Select = 0,
        Move = 1,
        Resize = 2,
        Rotate = 3
    }
}
