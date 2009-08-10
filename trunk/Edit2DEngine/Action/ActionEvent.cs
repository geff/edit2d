using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;

namespace Edit2DEngine.Action
{
    public class ActionEvent : ActionBase
    {
        [Browsable(false)]
        public String PropertyName { get; set; }
        [Browsable(false)]
        public Type PropertyType { get; set; }
        [Browsable(false)]
        private PropertyInfo ActionProperty { get; set; }
        [Browsable(false)]
        public ActionEventType[] ActionEventTypes { get; set; }
        [Browsable(false)]
        public float[] FloatValues { get; set; }
        [Browsable(false)]
        public bool BoolValue { get; set; }
        [Browsable(false)]
        public float[] RndMinValues { get; set; }
        [Browsable(false)]
        public float[] RndMaxValues { get; set; }
        [Browsable(false)]
        public int[] Durations { get; set; }
        [Browsable(false)]
        public bool[] IsRelative { get; set; }
        [Browsable(false)]
        private TimeSpan[] startTimes;
        [Browsable(false)]
        public Entite[] EntiteBindings { get; set; }
        //[Browsable(false)]
        //public String[] EntiteBindingPropertyNames { get; set; }
        [Browsable(false)]
        public int[] EntiteBindingPropertyId { get; set; }
        [Browsable(false)]
        public PropertyInfo[] EntiteBindingProperties { get; set; }
        [Browsable(false)]
        public Boolean Playing { get; set; }

        public ActionEvent(Script script, string actionName, Type typeEntite, string propertyName)
        {
            this.Script = script;
            this.ActionName = actionName;

            this.PropertyName = propertyName;
            this.ActionProperty = typeEntite.GetProperty(propertyName);
            this.PropertyType = this.ActionProperty.PropertyType;

            if (this.PropertyType.Name == "Vector2")
            {
                InitVar(2);
            }
            else if (PropertyType.Name == "Size")
            {
                InitVar(2);
            }
            else if (this.PropertyType.Name == "Single")
            {
                InitVar(1);
            }
            else if (this.PropertyType.Name == "Int32")
            {
                InitVar(1);
            }
            else if (this.PropertyType.Name == "Color")
            {
                InitVar(3);
            }
            else if (this.PropertyType.Name == "Boolean")
            {
                InitVar(1);
            }
        }

        private void InitVar(int count)
        {
            this.FloatValues = new float[count];
            this.RndMinValues = new float[count];
            this.RndMaxValues = new float[count];
            this.EntiteBindings = new Entite[count];
            //this.EntiteBindingPropertyNames = new String[count];
            this.EntiteBindingPropertyId = new int[count];
            this.EntiteBindingProperties = new PropertyInfo[count];
            this.Durations = new int[count];
            this.IsRelative = new bool[count];
            this.ActionEventTypes = new ActionEventType[count];
            this.startTimes = new TimeSpan[count];
            this.initialized = new bool[count];
            this.rndStartValue = new float[count];
            this.deltaMs = new int[count];
            this.pct = new float[count];
        }

        public override void InitAction()
        {
            for (int i = 0; i < this.ActionEventTypes.Length; i++)
            {
                initialized[i] = false;
            }
        }

        Vector2 vecStartValue = Vector2.Zero;
        float floatStartValue = 0f;
        Size sizeStartValue = Size.Empty;
        Microsoft.Xna.Framework.Graphics.Color colorStartValue = Microsoft.Xna.Framework.Graphics.Color.Black;
        bool boolStartValue = false;

        bool[] initialized;
        float[] rndStartValue;
        int[] deltaMs;
        float[] pct;

        private float GetPropertyValue(int index)
        {
            float value = 0f;

            if (EntiteBindingProperties[index].PropertyType.Name == "Vector2")
            {
                if (EntiteBindingPropertyId[index] == 1)
                    value = ((Vector2)EntiteBindingProperties[index].GetValue(EntiteBindings[index], null)).X;
                else if (EntiteBindingPropertyId[index] == 2)
                    value = ((Vector2)EntiteBindingProperties[index].GetValue(EntiteBindings[index], null)).Y;
            }
            else if (EntiteBindingProperties[index].PropertyType.Name == "Single")
            {
                value = (Single)EntiteBindingProperties[index].GetValue(EntiteBindings[index], null);
            }
            else if (EntiteBindingProperties[index].PropertyType.Name == "Int32")
            {
                    value = (Int32)EntiteBindingProperties[index].GetValue(EntiteBindings[index], null);
            }
            else if (EntiteBindingProperties[index].PropertyType.Name == "Color")
            {
                if (EntiteBindingPropertyId[index] == 1)
                    value = ((Microsoft.Xna.Framework.Graphics.Color)EntiteBindingProperties[index].GetValue(EntiteBindings[index], null)).R;
                else if (EntiteBindingPropertyId[index] == 2)
                    value = ((Microsoft.Xna.Framework.Graphics.Color)EntiteBindingProperties[index].GetValue(EntiteBindings[index], null)).G;
                else if (EntiteBindingPropertyId[index] == 3)
                    value = ((Microsoft.Xna.Framework.Graphics.Color)EntiteBindingProperties[index].GetValue(EntiteBindings[index], null)).B;
                else if (EntiteBindingPropertyId[index] == 4)
                    value = ((Microsoft.Xna.Framework.Graphics.Color)EntiteBindingProperties[index].GetValue(EntiteBindings[index], null)).A;
            }
            else if (EntiteBindingProperties[index].PropertyType.Name == "Size")
            {
                if (EntiteBindingPropertyId[index] == 1)
                    value = ((Size)EntiteBindingProperties[index].GetValue(EntiteBindings[index], null)).Width;
                else if (EntiteBindingPropertyId[index] == 2)
                    value = ((Size)EntiteBindingProperties[index].GetValue(EntiteBindings[index], null)).Height;
            }
            return value;
        }

        public void UpdateValue(Repository repository)
        {
            Vector2 vecValue = Vector2.Zero;
            float floatValue = 0f;
            Size sizeValue = Size.Empty;
            Microsoft.Xna.Framework.Graphics.Color colorValue = Microsoft.Xna.Framework.Graphics.Color.Black;
            bool boolValue = false;

            TimeSpan timeNow = DateTime.Now.TimeOfDay;

            bool playing = true;

            for (int i = 0; i < this.ActionEventTypes.Length; i++)
            {
                #region Initialization
                if (!initialized[i])
                {
                    if (this.PropertyType.Name == "Vector2")
                    {
                        if (i == 0)
                            vecStartValue.X = ((Vector2)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).X;
                        else if (i == 1)
                            vecStartValue.Y = ((Vector2)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).Y;
                    }
                    else if (this.PropertyType.Name == "Size")
                    {
                        if (i == 0)
                            sizeStartValue.Width = ((Size)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).Width;
                        if (i == 1)
                            sizeStartValue.Height = ((Size)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).Height;
                    }
                    else if (this.PropertyType.Name == "Single")
                    {
                        floatStartValue = (float)this.ActionProperty.GetValue(this.Script.ActionHandler, null);
                    }
                    else if (this.PropertyType.Name == "Int32")
                    {
                        floatStartValue = (float)this.ActionProperty.GetValue(this.Script.ActionHandler, null);
                    }
                    else if (this.PropertyType.Name == "Color")
                    {
                        if (i == 0)
                            colorStartValue.R = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).R;
                        else if (i == 1)
                            colorStartValue.G = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).G;
                        else if (i == 2)
                            colorStartValue.B = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).B;
                        else if (i == 3)
                            colorStartValue.A = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).A;
                    }
                    else if (this.PropertyType.Name == "Boolean")
                    {
                        boolStartValue = (bool)this.ActionProperty.GetValue(this.Script.ActionHandler, null);
                    }

                    //--- Initialisation des valeurs aléatoires
                    if (this.ActionEventTypes[i] == ActionEventType.Random)
                        rndStartValue[i] = repository.GetRandomValue(RndMinValues[i], RndMaxValues[i]);
                    //---

                    initialized[i] = true;
                }
                #endregion

                //--- Durée
                if (Durations[i] != 0)
                {
                    if (startTimes[i] == TimeSpan.Zero)
                    {
                        //--- Initialisation du timer
                        startTimes[i] = timeNow;
                        deltaMs[i] = 0;
                        pct[i] = 0f;
                        //---
                    }
                    else
                    {
                        deltaMs[i] = (int)timeNow.Subtract(startTimes[i]).TotalMilliseconds;

                        pct[i] = (float)deltaMs[i] / (float)Durations[i];
                    }
                }
                else
                {
                    deltaMs[i] = 0;
                    initialized[i] = true;
                }
                //---

                if (this.PropertyType.Name == "Vector2")
                {
                    #region Vector2
                    switch (this.ActionEventTypes[i])
                    {
                        case ActionEventType.Deactivated:
                            if (i == 0)
                                vecValue.X = vecStartValue.X;
                            else if (i == 1)
                                vecValue.Y = vecStartValue.Y;
                            break;
                        case ActionEventType.FixedValue:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    if (i == 0)
                                        vecValue.X = vecStartValue.X + MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                                    else if (i == 1)
                                        vecValue.Y = vecStartValue.Y + MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                                }
                                else
                                {
                                    if (i == 0)
                                        vecValue.X = vecStartValue.X + this.FloatValues[i];
                                    else if (i == 1)
                                        vecValue.Y = vecStartValue.Y + this.FloatValues[i];
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    if (i == 0)
                                        vecValue.X = MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                                    else if (i == 1)
                                        vecValue.Y = MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                                }
                                else
                                {
                                    if (i == 0)
                                        vecValue.X = this.FloatValues[i];
                                    else if (i == 1)
                                        vecValue.Y = this.FloatValues[i];
                                }
                            }
                            break;
                        case ActionEventType.MouseX:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    if (i == 0)
                                        vecValue.X = vecStartValue.X + MathHelper.Lerp(0f, repository.GetMousePosition().X - vecStartValue.X, pct[i]);
                                    else if (i == 1)
                                        vecValue.Y = vecStartValue.Y + MathHelper.Lerp(0f, repository.GetMousePosition().X - vecStartValue.X, pct[i]);
                                }
                                else
                                {
                                    if (i == 0)
                                        vecValue.X = repository.GetMousePosition().X;
                                    else if (i == 1)
                                        vecValue.Y = repository.GetMousePosition().X;
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    if (i == 0)
                                        vecValue.X = MathHelper.Lerp(0f, repository.GetMousePosition().X, pct[i]);
                                    else if (i == 1)
                                        vecValue.Y = MathHelper.Lerp(0f, repository.GetMousePosition().X, pct[i]);
                                }
                                else
                                {
                                    if (i == 0)
                                        vecValue.X = repository.GetMousePosition().X;
                                    else if (i == 1)
                                        vecValue.Y = repository.GetMousePosition().X;
                                }
                            }
                            break;
                        case ActionEventType.MouseY:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    if (i == 0)
                                        vecValue.X = vecStartValue.X + MathHelper.Lerp(0f, repository.GetMousePosition().Y - vecStartValue.Y, pct[i]);
                                    else if (i == 1)
                                        vecValue.Y = vecStartValue.Y + MathHelper.Lerp(0f, repository.GetMousePosition().Y - vecStartValue.Y, pct[i]);
                                }
                                else
                                {
                                    if (i == 0)
                                        vecValue.X = repository.GetMousePosition().Y;
                                    else if (i == 1)
                                        vecValue.Y = repository.GetMousePosition().Y;
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    if (i == 0)
                                        vecValue.X = MathHelper.Lerp(0f, repository.GetMousePosition().Y, pct[i]);
                                    else if (i == 1)
                                        vecValue.Y = MathHelper.Lerp(0f, repository.GetMousePosition().Y, pct[i]);
                                }
                                else
                                {
                                    if (i == 0)
                                        vecValue.X = repository.GetMousePosition().Y;
                                    else if (i == 1)
                                        vecValue.Y = repository.GetMousePosition().Y;
                                }
                            }
                            break;
                        case ActionEventType.EntityBinding:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    if (i == 0)
                                        vecValue.X = vecStartValue.X + MathHelper.Lerp(0f, GetPropertyValue(i), pct[i]);
                                    else if (i == 1)
                                        vecValue.Y = vecStartValue.Y + MathHelper.Lerp(0f, GetPropertyValue(i), pct[i]);
                                }
                                else
                                {
                                    if (i == 0)
                                        vecValue.X = vecStartValue.X + GetPropertyValue(i);
                                    else if (i == 1)
                                        vecValue.Y = vecStartValue.Y + GetPropertyValue(i);
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    if (i == 0)
                                        vecValue.X = MathHelper.Lerp(0f, GetPropertyValue(i), pct[i]);
                                    else if (i == 1)
                                        vecValue.Y = MathHelper.Lerp(0f, GetPropertyValue(i), pct[i]);
                                }
                                else
                                {
                                    if (i == 0)
                                        vecValue.X = GetPropertyValue(i);
                                    else if (i == 1)
                                        vecValue.Y = GetPropertyValue(i);
                                }
                            }
                            break;
                        case ActionEventType.Random:
                            //if (this.IsRelative[i])
                            //{
                            //    if (Durations[i] != 0)
                            //    {
                            //        if (i == 0)
                            //            vecValue.X = vecStartValue.X + MathHelper.Lerp(0f, rndStartValue[i], pct[i]);
                            //        else if (i == 1)
                            //            vecValue.Y = vecStartValue.Y + MathHelper.Lerp(0f, rndStartValue[i], pct[i]);
                            //    }
                            //    else
                            //    {
                            //        if (i == 0)
                            //            vecValue.X = vecStartValue.X + rndStartValue[i];
                            //        else if (i == 1)
                            //            vecValue.Y = vecStartValue.Y + rndStartValue[i];
                            //    }
                            //}
                            //else
                            //{
                            //    if (Durations[i] != 0)
                            //    {
                            //        if (i == 0)
                            //            vecValue.X = MathHelper.Lerp(0f, rndStartValue[i], pct[i]);
                            //        else if (i == 1)
                            //            vecValue.Y = MathHelper.Lerp(0f, rndStartValue[i], pct[i]);
                            //    }
                            //    else
                            //    {
                            //        if (i == 0)
                            //            vecValue.X = rndStartValue[i];
                            //        else if (i == 1)
                            //            vecValue.Y = rndStartValue[i];
                            //    }
                            //}

                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    if (i == 0)
                                        vecValue.X = MathHelper.Lerp(vecStartValue.X, rndStartValue[i], pct[i]);
                                    else if (i == 1)
                                        vecValue.Y = MathHelper.Lerp(vecStartValue.Y, rndStartValue[i], pct[i]);
                                }
                                else
                                {
                                    if (i == 0)
                                        vecValue.X = vecStartValue.X + rndStartValue[i];
                                    else if (i == 1)
                                        vecValue.Y = vecStartValue.Y + rndStartValue[i];
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    if (i == 0)
                                        vecValue.X = MathHelper.Lerp(0, rndStartValue[i], pct[i]);
                                    else if (i == 1)
                                        vecValue.Y = MathHelper.Lerp(0, rndStartValue[i], pct[i]);
                                }
                                else
                                {
                                    if (i == 0)
                                        vecValue.X = rndStartValue[i];
                                    else if (i == 1)
                                        vecValue.Y = rndStartValue[i];
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                else if (this.PropertyType.Name == "Single")
                {
                    #region Float
                    switch (this.ActionEventTypes[i])
                    {
                        case ActionEventType.Deactivated:
                            floatValue = floatStartValue;
                            break;
                        case ActionEventType.FixedValue:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = floatStartValue + MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                                }
                                else
                                {
                                    floatValue = floatStartValue + this.FloatValues[i];
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                                }
                                else
                                {
                                    floatValue = this.FloatValues[i];
                                }
                            }
                            break;
                        case ActionEventType.MouseX:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = floatStartValue + MathHelper.Lerp(0f, repository.GetMousePosition().X - floatStartValue, pct[i]);
                                }
                                else
                                {
                                    floatValue = repository.GetMousePosition().X;
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = MathHelper.Lerp(0f, repository.GetMousePosition().X, pct[i]);
                                }
                                else
                                {
                                    floatValue = repository.GetMousePosition().X;
                                }
                            }
                            break;
                        case ActionEventType.MouseY:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = floatStartValue + MathHelper.Lerp(0f, repository.GetMousePosition().Y - floatStartValue, pct[i]);
                                }
                                else
                                {
                                    floatValue = repository.GetMousePosition().Y;
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = MathHelper.Lerp(0f, repository.GetMousePosition().Y, pct[i]);
                                }
                                else
                                {
                                    floatValue = repository.GetMousePosition().Y;
                                }
                            }
                            break;
                        case ActionEventType.EntityBinding:
                            break;
                        case ActionEventType.Random:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = MathHelper.Lerp(floatStartValue, rndStartValue[i], pct[i]);
                                }
                                else
                                {
                                    floatValue = floatStartValue + rndStartValue[i];
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = MathHelper.Lerp(0f, rndStartValue[i], pct[i]);
                                }
                                else
                                {
                                    floatValue = rndStartValue[i];
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                else if (this.PropertyType.Name == "Int32")
                {
                    #region Int32
                    switch (this.ActionEventTypes[i])
                    {
                        case ActionEventType.Deactivated:
                            floatValue = floatStartValue;
                            break;
                        case ActionEventType.FixedValue:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = floatStartValue + MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                                }
                                else
                                {
                                    floatValue = floatStartValue + this.FloatValues[i];
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                                }
                                else
                                {
                                    floatValue = this.FloatValues[i];
                                }
                            }
                            break;
                        case ActionEventType.MouseX:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = floatStartValue + MathHelper.Lerp(0f, repository.GetMousePosition().X - floatStartValue, pct[i]);
                                }
                                else
                                {
                                    floatValue = repository.GetMousePosition().X;
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = MathHelper.Lerp(0f, repository.GetMousePosition().X, pct[i]);
                                }
                                else
                                {
                                    floatValue = repository.GetMousePosition().X;
                                }
                            }
                            break;
                        case ActionEventType.MouseY:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = floatStartValue + MathHelper.Lerp(0f, repository.GetMousePosition().Y - floatStartValue, pct[i]);
                                }
                                else
                                {
                                    floatValue = repository.GetMousePosition().Y;
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = MathHelper.Lerp(0f, repository.GetMousePosition().Y, pct[i]);
                                }
                                else
                                {
                                    floatValue = repository.GetMousePosition().Y;
                                }
                            }
                            break;
                        case ActionEventType.EntityBinding:
                            break;
                        case ActionEventType.Random:
                            if (this.IsRelative[i])
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = MathHelper.Lerp(floatStartValue, rndStartValue[i], pct[i]);
                                }
                                else
                                {
                                    floatValue = floatStartValue + rndStartValue[i];
                                }
                            }
                            else
                            {
                                if (Durations[i] != 0)
                                {
                                    floatValue = MathHelper.Lerp(0f, rndStartValue[i], pct[i]);
                                }
                                else
                                {
                                    floatValue = rndStartValue[i];
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                else if (this.PropertyType.Name == "Size")
                {
                    #region Size
                    //switch (this.ActionEventTypes[i])
                    //{
                    //    case ActionEventType.Deactivated:
                    //        if (i == 0)
                    //            vecValue.X = vecStartValue.X;
                    //        else if (i == 1)
                    //            vecValue.Y = vecStartValue.Y;
                    //        break;
                    //    case ActionEventType.FixedValue:
                    //        if (this.IsRelative[i])
                    //        {
                    //            if (Durations[i] != 0)
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = vecStartValue.X + MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                    //                else if (i == 1)
                    //                    vecValue.Y = vecStartValue.Y + MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                    //            }
                    //            else
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = vecStartValue.X + this.FloatValues[i];
                    //                else if (i == 1)
                    //                    vecValue.Y = vecStartValue.Y + this.FloatValues[i];
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (Durations[i] != 0)
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                    //                else if (i == 1)
                    //                    vecValue.Y = MathHelper.Lerp(0f, this.FloatValues[i], pct[i]);
                    //            }
                    //            else
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = this.FloatValues[i];
                    //                else if (i == 1)
                    //                    vecValue.Y = this.FloatValues[i];
                    //            }
                    //        }
                    //        break;
                    //    case ActionEventType.MouseX:
                    //        if (this.IsRelative[i])
                    //        {
                    //            if (Durations[i] != 0)
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = vecStartValue.X + MathHelper.Lerp(0f, repository.GetMousePosition().X - vecStartValue.X, pct[i]);
                    //                else if (i == 1)
                    //                    vecValue.Y = vecStartValue.Y + MathHelper.Lerp(0f, repository.GetMousePosition().X - vecStartValue.X, pct[i]);
                    //            }
                    //            else
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = repository.GetMousePosition().X;
                    //                else if (i == 1)
                    //                    vecValue.Y = repository.GetMousePosition().X;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (Durations[i] != 0)
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = MathHelper.Lerp(0f, repository.GetMousePosition().X, pct[i]);
                    //                else if (i == 1)
                    //                    vecValue.Y = MathHelper.Lerp(0f, repository.GetMousePosition().X, pct[i]);
                    //            }
                    //            else
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = repository.GetMousePosition().X;
                    //                else if (i == 1)
                    //                    vecValue.Y = repository.GetMousePosition().X;
                    //            }
                    //        }
                    //        break;
                    //    case ActionEventType.MouseY:
                    //        if (this.IsRelative[i])
                    //        {
                    //            if (Durations[i] != 0)
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = vecStartValue.X + MathHelper.Lerp(0f, repository.GetMousePosition().Y - vecStartValue.Y, pct[i]);
                    //                else if (i == 1)
                    //                    vecValue.Y = vecStartValue.Y + MathHelper.Lerp(0f, repository.GetMousePosition().Y - vecStartValue.Y, pct[i]);
                    //            }
                    //            else
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = repository.GetMousePosition().Y;
                    //                else if (i == 1)
                    //                    vecValue.Y = repository.GetMousePosition().Y;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (Durations[i] != 0)
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = MathHelper.Lerp(0f, repository.GetMousePosition().Y, pct[i]);
                    //                else if (i == 1)
                    //                    vecValue.Y = MathHelper.Lerp(0f, repository.GetMousePosition().Y, pct[i]);
                    //            }
                    //            else
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = repository.GetMousePosition().Y;
                    //                else if (i == 1)
                    //                    vecValue.Y = repository.GetMousePosition().Y;
                    //            }
                    //        }
                    //        break;
                    //    case ActionEventType.EntityBinding:
                    //        break;
                    //    case ActionEventType.Random:
                    //        if (this.IsRelative[i])
                    //        {
                    //            if (Durations[i] != 0)
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = vecStartValue.X + MathHelper.Lerp(0f, rndStartValue[i], pct[i]);
                    //                else if (i == 1)
                    //                    vecValue.Y = vecStartValue.Y + MathHelper.Lerp(0f, rndStartValue[i], pct[i]);
                    //            }
                    //            else
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = vecStartValue.X + rndStartValue[i];
                    //                else if (i == 1)
                    //                    vecValue.Y = vecStartValue.Y + rndStartValue[i];
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (Durations[i] != 0)
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = MathHelper.Lerp(0f, rndStartValue[i], pct[i]);
                    //                else if (i == 1)
                    //                    vecValue.Y = MathHelper.Lerp(0f, rndStartValue[i], pct[i]);
                    //            }
                    //            else
                    //            {
                    //                if (i == 0)
                    //                    vecValue.X = rndStartValue[i];
                    //                else if (i == 1)
                    //                    vecValue.Y = rndStartValue[i];
                    //            }
                    //        }
                    //        break;
                    //    default:
                    //        break;
                    //}
                    #endregion
                }
                else if (this.PropertyType.Name == "Color")
                {
                    #region Color
                    //if (this.IsRelative[i])
                    //{
                    //    if (i == 0)
                    //        colorValue.R = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.Entite, null)).R;
                    //    else if (i == 1)
                    //        colorValue.G = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.Entite, null)).G;
                    //    else if (i == 2)
                    //        colorValue.B = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.Entite, null)).B;
                    //    else if (i == 3)
                    //        colorValue.A = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.Entite, null)).A;
                    //} 
                    #endregion
                }
                else if (this.PropertyType.Name == "Boolean")
                {
                    #region Boolean
                    switch (this.ActionEventTypes[i])
                    {
                        case ActionEventType.Deactivated:
                            boolValue = boolStartValue;
                            break;
                        case ActionEventType.FixedValue:
                            boolValue = this.BoolValue;
                            break;
                        case ActionEventType.EntityBinding:
                            break;
                        default:
                            break;
                    }
                    #endregion
                }

                //--- Durée
                if (Durations[i] != 0)
                {
                    if (startTimes[i] != TimeSpan.Zero)
                    {
                        deltaMs[i] = (int)timeNow.Subtract(startTimes[i]).TotalMilliseconds;

                        //--- Si le temps d'animation est écoulé, quitter la procédure de mise à jour
                        if (deltaMs[i] >= Durations[i])
                        {
                            startTimes[i] = TimeSpan.Zero;
                            initialized[i] = false;
                        }
                        //---
                    }
                }
                //---

                playing &= initialized[i];
            }

            this.Playing = playing;

            if (this.PropertyType.Name == "Vector2")
                this.ActionProperty.SetValue(this.Script.ActionHandler, vecValue, null);
            else if (this.PropertyType.Name == "Size")
                this.ActionProperty.SetValue(this.Script.ActionHandler, sizeValue, null);
            else if (this.PropertyType.Name == "Single")
                this.ActionProperty.SetValue(this.Script.ActionHandler, floatValue, null);
            else if (this.PropertyType.Name == "Int32")
                this.ActionProperty.SetValue(this.Script.ActionHandler, (int)floatValue, null);
            else if (this.PropertyType.Name == "Color")
                this.ActionProperty.SetValue(this.Script.ActionHandler, colorValue, null);
            else if (this.PropertyType.Name == "Boolean")
                this.ActionProperty.SetValue(this.Script.ActionHandler, boolValue, null);
        }
    }

    public enum ActionEventType : int
    {
        Deactivated = 0,
        FixedValue = 1,
        MouseX = 2,
        MouseY = 3,
        EntityBinding = 4,
        Random = 5
    }
}
