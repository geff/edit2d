using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Edit2DEngine.Entities;

namespace Edit2DEngine.Trigger
{
    public class TriggerMouse : TriggerBase
    {
        public TriggerMouseType TriggerMouseType { get; set; }
        private bool IsMouseOver = false;

        public TriggerMouse(String triggerName, ITriggerHandler triggerHandler, TriggerMouseType triggerMouseType)
        {
            this.TriggerName = triggerName;
            this.TriggerHandler = triggerHandler;

            this.ListScript = new List<Script>();
            this.ListTargetActionHandlerName = new List<string>();
            this.ListTargetScriptName = new List<string>();

            this.TriggerMouseType = triggerMouseType;
        }

        public override void InitTrigger(Repository repository)
        {
        }

        public override void CheckTrigger(Repository repository)
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 pos = repository.GetMousePosition();
            //new Vector2(mouseState.X, mouseState.Y) - repository.GetModelViewControlPosition();
            bool launchScript = false;

            switch (TriggerMouseType)
            {
                case TriggerMouseType.MouseRightClick:
                    if (this.TriggerHandler is Entity)
                    {
                        if (mouseState.RightButton == ButtonState.Pressed && ((Entity)TriggerHandler).geom.Collide(pos))
                            launchScript = true;
                    }
                    else
                    {
                        if (mouseState.RightButton == ButtonState.Pressed)
                            launchScript = true;
                    }

                    break;
                case TriggerMouseType.MouseLeftClick:
                    //if (mouseState.LeftButton == ButtonState.Pressed && Entity.geom.Collide(pos))
                    //    launchScript = true;
                    if (this.TriggerHandler is Entity)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed && ((Entity)TriggerHandler).geom.Collide(pos))
                            launchScript = true;
                    }
                    else
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                            launchScript = true;
                    }
                    break;
                case TriggerMouseType.MouseEnter:
                    //if (!IsMouseOver && Entity.geom.Collide(pos))
                    //    launchScript = true;
                    if (this.TriggerHandler is Entity)
                    {
                        if (!IsMouseOver && ((Entity)TriggerHandler).geom.Collide(pos))
                            launchScript = true;
                    }
                    else
                    {
                        if (!IsMouseOver)
                            launchScript = true;
                    }
                    break;
                case TriggerMouseType.MouseLeave:
                    //if (IsMouseOver && !Entity.geom.Collide(pos))
                    //    launchScript = true;
                    if (this.TriggerHandler is Entity)
                    {
                        if (IsMouseOver && !((Entity)TriggerHandler).geom.Collide(pos))
                            launchScript = true;
                    }
                    else
                    {
                        if (IsMouseOver)
                            launchScript = true;
                    }
                    break;
                case TriggerMouseType.MouseOver:
                    //if (Entity.geom.Collide(pos))
                    //    launchScript = true;
                    if (this.TriggerHandler is Entity)
                    {
                        if (((Entity)TriggerHandler).geom.Collide(pos))
                            launchScript = true;
                    }
                    else
                    {
                        //if (mouseState.RightButton == ButtonState.Pressed)
                            launchScript = true;
                    }
                    break;
                default:
                    break;
            }

            if (this.TriggerHandler is Entity)
            {
                IsMouseOver = ((Entity)TriggerHandler).geom.Collide(pos);
            }
            else
            {
                IsMouseOver = true;
            }

            if (launchScript)
                base.LaunchScript(repository);
        }
    }

    public enum TriggerMouseType : int
    {
        MouseRightClick = 0,
        MouseLeftClick = 1,
        MouseEnter = 2,
        MouseLeave = 3,
        MouseOver = 4
    }
}
