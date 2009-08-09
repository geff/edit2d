using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FarseerGames.GettingStarted;

namespace Edit2D
{
    public class Repository : Edit2DEngine.Repository
    {
        public FrmEdit2D FrmEdit2D { get; set; }
        public PhysicsSimulatorView PhysicsSimulatorView;

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
    }
}
