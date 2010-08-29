using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Edit2DEngine.Action;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using Edit2DEngine.Entities.Particles;
using System.Drawing;
using Edit2DEngine.Entities;

namespace Edit2DEngine.Action
{
    public class ActionCurve : ActionBase
    {
        [Browsable(false)]
        public String PropertyName { get; set; }
        [Browsable(false)]
        public Type PropertyType { get; set; }
        [Browsable(true)]
        public Boolean IsRelative { get; set; }
        [Browsable(true)]
        private PropertyInfo ActionProperty { get; set; }
        [Browsable(false)]
        public List<Curve> ListCurve { get; set; }
        [Browsable(false)]
        public int Duration { get; set; }
        [Browsable(true)]
        public Boolean IsLoop { get; set; }

        public ActionCurve(Script script, string actionName, bool isRelative, bool isLoop, string propertyName)
        {
            this.Script = script;
            this.ActionName = actionName;
            this.IsRelative = isRelative;
            this.IsLoop = isLoop;

            this.ListCurve = new List<Microsoft.Xna.Framework.Curve>();

            this.PropertyName = propertyName;
            this.ActionProperty = script.ActionHandler.GetType().GetProperty(propertyName);
            this.PropertyType = this.ActionProperty.PropertyType;

            this.ListCurve.Add(new Curve());

            switch (this.PropertyType.Name)
            {
                case "Vector2":
                case "Size" :
                    this.ListCurve.Add(new Curve());
                    break;
                case "Single":
                    break;
                case "Color":
                    this.ListCurve.Add(new Curve());
                    this.ListCurve.Add(new Curve());
                    this.ListCurve.Add(new Curve());
                    break;
                default:
                    break;
            }
        }

        public override void InitAction()
        {
            StopAnimation();
        }

        #region CurvePlayer
        TimeSpan startAnimation;
        TimeSpan durationAnimation;
        public PlayAnimationState playAnimationState = PlayAnimationState.Stop;
        //Entity entityCloneAnimation;
        Object entityCloneAnimation;
        public int timeLineAnimation;

        public void StopAnimation()
        {
            if (playAnimationState > 0)
            {
                switch (this.PropertyType.Name)
                {
                    case "Size":
                        this.ActionProperty.SetValue(this.Script.ActionHandler, (Size)this.ActionProperty.GetValue(entityCloneAnimation, null), null);
                        break;
                    case "Vector2":
                        this.ActionProperty.SetValue(this.Script.ActionHandler, (Vector2)this.ActionProperty.GetValue(entityCloneAnimation, null), null);
                        break;
                    case "Single":
                        this.ActionProperty.SetValue(this.Script.ActionHandler, (float)this.ActionProperty.GetValue(entityCloneAnimation, null), null);
                        break;
                    case "Color":
                        this.ActionProperty.SetValue(this.Script.ActionHandler, (Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(entityCloneAnimation, null), null);
                        break;
                    default:
                        break;
                }
            }
        }

        public void StartAnimation(PlayAnimationState state)
        {
            startAnimation = DateTime.Now.TimeOfDay;

            if (this.Script.ActionHandler is Entity)
                entityCloneAnimation = ((Entity)this.Script.ActionHandler).Clone();
            else if (this.Script.ActionHandler is ParticleSystem)
                entityCloneAnimation = ((ParticleSystem)this.Script.ActionHandler).Clone();

            //--- playAnimation est utilisé pour l'animation automatique
            //if (controlByTimeline)
            //    playAnimation = 0;
            //else
            //    playAnimation = 2;
            //if (controlByTimeline)
            //    playAnimationState = PlayAnimationState.PlayManually;
            //else
            //    playAnimationState = PlayAnimationState.PlayInEditor;
            playAnimationState = state;
            //---
        }

        public void UpdateAnimation()
        {
            durationAnimation = DateTime.Now.TimeOfDay.Subtract(startAnimation);

            if (durationAnimation.TotalMilliseconds > this.Duration)
            {
                if (IsLoop)
                {
                    startAnimation = DateTime.Now.TimeOfDay;
                    durationAnimation = DateTime.Now.TimeOfDay.Subtract(startAnimation);
                }
                else
                {
                    playAnimationState = PlayAnimationState.Stop;
                }
            }

            UpdateAnimation((int)durationAnimation.TotalMilliseconds);
        }

        public void UpdateAnimation(int timeLine)
        {
            this.timeLineAnimation = timeLine;

            //---
            Vector2 vector2Value = new Vector2();
            byte[] tabColor = new byte[4];
            Microsoft.Xna.Framework.Graphics.Color colorValue = Microsoft.Xna.Framework.Graphics.Color.White;
            float floatValue = 0f;
            Size sizeValue = new Size();
            //---

            for (int i = 0; i < ListCurve.Count; i++)
            {
                float value = ListCurve[i].Evaluate(timeLine);

                switch (this.PropertyType.Name)
                {
                    case "Size":
                        //value = MathHelper.Clamp(value, 0f, 5000f);
                        if (i == 0)
                            sizeValue.Width = Math.Max((int)value, 1);
                        else
                            sizeValue.Height = Math.Max((int)value, 1);
                        break;
                    case "Vector2":
                        if (i == 0)
                            vector2Value.X = value;
                        else
                            vector2Value.Y = value;
                        break;
                    case "Single":
                        floatValue = value;
                        break;
                    case "Int32":
                        floatValue = value;
                        break;
                    case "Color":
                        value = MathHelper.Clamp(value, 0f, 255f);
                        tabColor[i] = (byte)value;
                        break;
                    default:
                        break;
                }
            }

            //---
            if (this.PropertyType.Name == "Color")
                colorValue = new Microsoft.Xna.Framework.Graphics.Color(tabColor[0], tabColor[1], tabColor[2], tabColor[3]);
            //---

            //---
            switch (this.PropertyType.Name)
            {
                case "Size":
                    if (this.IsRelative)
                        this.ActionProperty.SetValue(this.Script.ActionHandler, (Size)this.ActionProperty.GetValue(entityCloneAnimation, null) + sizeValue, null);
                    else
                        this.ActionProperty.SetValue(this.Script.ActionHandler, sizeValue, null);
                    break;
                case "Vector2":
                    if (this.IsRelative)
                        this.ActionProperty.SetValue(this.Script.ActionHandler, (Vector2)this.ActionProperty.GetValue(entityCloneAnimation, null) + vector2Value, null);
                    else
                        this.ActionProperty.SetValue(this.Script.ActionHandler, vector2Value, null);
                    break;
                case "Single":
                    if (this.IsRelative)
                        this.ActionProperty.SetValue(this.Script.ActionHandler, (float)this.ActionProperty.GetValue(entityCloneAnimation, null) + floatValue, null);
                    else
                        this.ActionProperty.SetValue(this.Script.ActionHandler, floatValue, null);
                    break;
                case "Int32":
                    if (this.IsRelative)
                        this.ActionProperty.SetValue(this.Script.ActionHandler, (int)this.ActionProperty.GetValue(entityCloneAnimation, null) + (int)floatValue, null);
                    else
                        this.ActionProperty.SetValue(this.Script.ActionHandler, (int)floatValue, null);
                    break;
                case "Color":
                    //TODO : ajouter la couleur relative
                    //if (this.IsRelative)
                    //    this.ActionProperty.SetValue(this.Script.Entity, (Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(entityCloneAnimation, null) + colorValue, null);
                    //else
                    this.ActionProperty.SetValue(this.Script.ActionHandler, colorValue, null);
                    break;
                default:
                    break;
            }
            //---
        }
        #endregion

        public void CalcDuration()
        {
            float maxDuration = 0;

            for (int i = 0; i < this.ListCurve.Count; i++)
            {
                if (this.ListCurve[i].Keys.Count > 0)
                    maxDuration = Math.Max(this.ListCurve[i].Keys.Max<CurveKey>(key => key.Position), maxDuration);
            }

            this.Duration = (int)maxDuration;
        }
    }

    public enum PlayAnimationState : int
    {
        Stop = 0,
        Play = 1,
        PlayInEditor = 2,
        PlayManually =3
    }
}


