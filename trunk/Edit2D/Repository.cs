using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FarseerGames.GettingStarted;
using Edit2DEngine;
using Edit2DEngine.Actions;

using Edit2DEngine.Entities;
using Edit2DEngine.Triggers;
using Edit2DEngine.CustomProperties;
using Edit2DEngine.Entities.Particles;

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

        public ICustomPropertyHandler CurrentCustomPropertyHandler
        {
            get
            {
                if (this.ListSelection.Count != 1)
                    return null;

                return this.ListSelection[0].CustomPropertyHandler;
            }
        }

        public IActionHandler CurrentActionHandler
        {
            get
            {
                if (this.ListSelection.Count != 1)
                    return null;

                return this.ListSelection[0].ActionHandler;
            }
        }

        public ITriggerHandler CurrentTriggerHandler
        {
            get
            {
                if (this.ListSelection.Count != 1)
                    return null;

                return this.ListSelection[0].TriggerHandler;
            }
        }

        public ParticleSystem CurrentParticleSystem
        {
            get
            {
                if (this.ListSelection.Count != 1)
                    return null;

                return this.ListSelection[0].ParticleSystem;
            }
        }

        public EntityPhysicObject CurrentEntityPhysic
        {
            get
            {
                EntityPhysicObject currentEntityPhysicObject = null;

                for (int i = 0; i < this.ListSelection.Count; i++)
                {
                    if (this.ListSelection[i].EntityPhysicObject != null)
                        currentEntityPhysicObject = this.ListSelection[i].EntityPhysicObject;

                    if (currentEntityPhysicObject != null)
                        return currentEntityPhysicObject;
                }

                return currentEntityPhysicObject;
            }
        }

        public EntityPhysicObject CurrentEntityPhysic2
        {
            get
            {
                EntityPhysicObject currentEntityPhysicObject = null;
                int count = 0;

                for (int i = 0; i < this.ListSelection.Count; i++)
                {
                    if (this.ListSelection[i].EntityPhysicObject != null)
                    {
                        currentEntityPhysicObject = this.ListSelection[i].EntityPhysicObject;
                        count++;
                    }

                    if (currentEntityPhysicObject != null && count == 2)
                        return currentEntityPhysicObject;
                }

                return null;
            }
        }

        public Object CurrentObject
        {
            get
            {
                if (this.ListSelection.Count != 1)
                    return null;
                else
                    return this.ListSelection[0].Object;
            }
        }

        public Entity CurrentEntity
        {
            get
            {
                Entity currentEntity = null;

                if (this.ListSelection.Count != 1)
                    return null;

                for (int i = 0; i < this.ListSelection.Count; i++)
                {
                    if (this.ListSelection[i].Entity != null)
                        currentEntity = this.ListSelection[i].Entity;
                    if (this.ListSelection[i].EntityComponent != null)
                        currentEntity = this.ListSelection[i].EntityComponent.EntityParent;

                    if (currentEntity != null)
                        return currentEntity;
                }

                return currentEntity;
            }
        }

        public Script CurrentScript
        {
            get
            {
                Script currentScript = null;

                if (this.ListSelection.Count != 1)
                    return null;

                for (int i = 0; i < this.ListSelection.Count; i++)
                {
                    if (this.ListSelection[i].Script != null)
                        currentScript = this.ListSelection[i].Script;

                    if (currentScript != null)
                        return currentScript;
                }

                for (int i = 0; i < this.ListSelection.Count; i++)
                {
                    if (this.ListSelection[i].ActionHandler != null && this.ListSelection[i].ActionHandler.ListScript.Count > 0)
                        currentScript = this.ListSelection[i].ActionHandler.ListScript[0];

                    if (currentScript != null)
                        return currentScript;
                }

                return currentScript;
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

        public List<Entity> GetSelectedEntity()
        {
            List<Entity> listSelectedEntity = new List<Entity>();

            listSelectedEntity.AddRange(ListSelection.Select<Selection, Entity>(s => s.EntityComponent.EntityParent));

            if (CurrentEntity != null)
                listSelectedEntity.Add(CurrentEntity);

            return listSelectedEntity.Distinct().ToList();
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
                            for (int l = 0; l <= actionCurve.Duration; l += durationLine, nbLine++)
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
                                if (ListCurveLine.Count <= nbLine + 1)
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
