using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FarseerGames.GettingStarted;
using Edit2DEngine;
using Edit2DEngine.Action;
using Edit2DEngine.Trigger;

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

        public TriggerBase CurrentTrigger { get; set; }

        public MouseMode MouseMode = MouseMode.Move;
        public ViewingMode ViewingMode = ViewingMode.Nothing;

        //--- Visualsation de la courbe courante
        public List<Vector2> ListCurveLine { get; set; }
        public List<Vector2> ListCurvePoint { get; set; }

        private Script _currentScript = null;
        public Script CurrentScript
        {
            get
            {
                return _currentScript;
            }
            set
            {
                _currentScript = value;
                CalcDrawingCurve();
            }
        }
        //---

        public bool Screenshot = false;

        public Repository()
        {
            this.ListSelection = new List<Selection>();
            this.CurrentPointer = new Pointer();
            this.CurrentPointer2 = new Pointer();

            this.ListCurveLine = new List<Vector2>();
            this.ListCurvePoint = new List<Vector2>();
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

        public bool IsSimpleMode
        {
            get
            {
                return System.Environment.MachineName == "P64L03BIB09";
                //return true;
            }
        }

        private void CalcDrawingCurve()
        {
            if (this.CurrentScript == null)
                return;

            this.ListCurveLine = new List<Vector2>();
            this.ListCurvePoint = new List<Vector2>();

            for (int j = 0; j < this.CurrentScript.ListAction.Count; j++)
            {
                ActionBase actionBase = this.CurrentScript.ListAction[j];

                if (actionBase != null && actionBase is ActionCurve)
                {
                    ActionCurve actionCurve = (ActionCurve)actionBase;

                    if (actionCurve.ActionName.StartsWith("Position"))
                    {
                        this.ListCurvePoint = new List<Vector2>();

                        for (int i = 0; i < actionCurve.ListCurve.Count; i++)
                        {
                            Curve curve = actionCurve.ListCurve[i];

                            for (int k = 0; k < curve.Keys.Count; k++)
                            {
                                CurveKey curveKey = curve.Keys[k];

                                //--- Point
                                if (ListCurvePoint.Count <= k)
                                {
                                    ListCurvePoint.Add(new Vector2(curveKey.Value, 0));
                                }
                                else
                                {
                                    ListCurvePoint[k] = new Vector2(ListCurvePoint[k].X, curveKey.Value);
                                }
                                //---
                            }

                            //---> Durée pour une ligne affichée (100 ms)
                            int durationLine = 100;
                            int nbLine = 0;
                            for (int l = 0; l <= actionCurve.Duration; l+=durationLine, nbLine++)
                            {
                                if (ListCurveLine.Count <= nbLine)
                                {
                                    ListCurveLine.Add(new Vector2(curve.Evaluate((float)l), 0));
                                }
                                else
                                {
                                    ListCurveLine[nbLine] = new Vector2(ListCurveLine[nbLine].X, curve.Evaluate((float)l));
                                }
                            }

                            if (actionCurve.Duration % durationLine > 0)
                            {
                                if (ListCurveLine.Count <= nbLine+1)
                                {
                                    ListCurveLine.Add(new Vector2(curve.Evaluate((float)actionCurve.Duration), 0));
                                }
                                else
                                {
                                    ListCurveLine[nbLine] = new Vector2(ListCurveLine[nbLine].X, curve.Evaluate((float)actionCurve.Duration));
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public enum MouseMode : int
    {
        Select = 0,
        Move = 1,
        Resize = 2,
        Rotate = 3
    }

    public enum ViewingMode : int
    {
        Nothing = 0,
        Script = 1,
        Trigger = 2,
        ParticleSystem = 3
    }
}
