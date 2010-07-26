﻿using System;
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
        public int[] Speeds { get; set; }
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

        float[] updateValues;
        float[] startValues;

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
            updateValues = new float[count];
            startValues = new float[count];

            this.FloatValues = new float[count];
            this.RndMinValues = new float[count];
            this.RndMaxValues = new float[count];
            this.EntiteBindings = new Entite[count];
            //this.EntiteBindingPropertyNames = new String[count];
            this.EntiteBindingPropertyId = new int[count];
            this.EntiteBindingProperties = new PropertyInfo[count];
            this.Durations = new int[count];
            this.Speeds = new int[count];
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

        ////--- Start values
        //Vector2 vecStartValue = Vector2.Zero;
        //float floatStartValue = 0f;
        //Size sizeStartValue = Size.Empty;
        //Microsoft.Xna.Framework.Graphics.Color colorStartValue = Microsoft.Xna.Framework.Graphics.Color.Black;
        //bool boolStartValue = false;
        float[] rndStartValue;
        ////---

        ////--- Actual values
        //Vector2 vecActualValue = Vector2.Zero;
        //float floatActualValue = 0f;
        //Size sizeActualValue = Size.Empty;
        //Microsoft.Xna.Framework.Graphics.Color colorActualValue = Microsoft.Xna.Framework.Graphics.Color.Black;
        //bool boolActualValue = false;
        //float[] rndActualValue;
        ////---

        bool[] initialized;
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

        private void GetPropertyValue(Repository repository, int i)
        {
            if (this.PropertyType.Name == "Vector2")
            {
                if (i == 0)
                    startValues[i] = ((Vector2)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).X;
                else if (i == 1)
                    startValues[i] = ((Vector2)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).Y;
            }
            else if (this.PropertyType.Name == "Size")
            {
                if (i == 0)
                    startValues[i] = (float)((Size)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).Width;
                if (i == 1)
                    startValues[i] = (float)((Size)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).Height;
            }
            else if (this.PropertyType.Name == "Single")
            {
                startValues[i] = (float)this.ActionProperty.GetValue(this.Script.ActionHandler, null);
            }
            else if (this.PropertyType.Name == "Int32")
            {
                startValues[i] = (float)this.ActionProperty.GetValue(this.Script.ActionHandler, null);
            }
            else if (this.PropertyType.Name == "Color")
            {
                if (i == 0)
                    startValues[i] = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).R;
                else if (i == 1)
                    startValues[i] = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).G;
                else if (i == 2)
                    startValues[i] = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).B;
                else if (i == 3)
                    startValues[i] = ((Microsoft.Xna.Framework.Graphics.Color)this.ActionProperty.GetValue(this.Script.ActionHandler, null)).A;
            }
            else if (this.PropertyType.Name == "Boolean")
            {
                startValues[i] = (float)this.ActionProperty.GetValue(this.Script.ActionHandler, null);
            }

            //--- Initialisation des valeurs aléatoires
            if (this.ActionEventTypes[i] == ActionEventType.Random)
                rndStartValue[i] = repository.GetRandomValue(RndMinValues[i], RndMaxValues[i]);
            //---

            initialized[i] = true;
        }

        public void UpdateValue(Repository repository)
        {
            TimeSpan timeNow = DateTime.Now.TimeOfDay;

            bool playing = true;

            for (int i = 0; i < this.ActionEventTypes.Length; i++)
            {
                updateValues[i] = 0;

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
                    if(Speeds[i] == 0)
                        deltaMs[i] = 0;

                    initialized[i] = true;
                }
                //---

                float calcValue = 0f;

                #region Vector2
                switch (this.ActionEventTypes[i])
                {
                    case ActionEventType.Deactivated:
                        calcValue = startValues[i];
                        break;
                    case ActionEventType.FixedValue:
                        calcValue = this.FloatValues[i];
                        break;
                    case ActionEventType.MouseX:
                        calcValue = repository.GetMousePosition().X - startValues[i];
                        break;
                    case ActionEventType.MouseY:
                        calcValue = repository.GetMousePosition().Y - startValues[i];
                        break;
                    case ActionEventType.EntityBinding:
                        calcValue = GetPropertyValue(i);
                        break;
                    case ActionEventType.Random:
                        calcValue = rndStartValue[i];
                        break;
                    default:
                        break;
                }
                #endregion

                //--- Relatives & Durations
                if (this.IsRelative[i])
                {
                    updateValues[i] = startValues[i];
                }

                if (Durations[i] != 0)
                {
                    updateValues[i] += MathHelper.Lerp(0f, calcValue, pct[i]);
                }
                else
                {
                    updateValues[i] += calcValue;
                }
                //----

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
            {
                Vector2 vecValue = new Vector2(updateValues[0], updateValues[1]);
                this.ActionProperty.SetValue(this.Script.ActionHandler, vecValue, null);
            }
            else if (this.PropertyType.Name == "Size")
            {
                Size sizeValue = new Size((int)updateValues[0], (int)updateValues[1]);
                this.ActionProperty.SetValue(this.Script.ActionHandler, sizeValue, null);
            }
            else if (this.PropertyType.Name == "Single")
            {
                this.ActionProperty.SetValue(this.Script.ActionHandler, updateValues[0], null);
            }
            else if (this.PropertyType.Name == "Int32")
            {
                this.ActionProperty.SetValue(this.Script.ActionHandler, (int)updateValues[0], null);
            }
            else if (this.PropertyType.Name == "Color")
            {
                Microsoft.Xna.Framework.Graphics.Color colorValue = new Microsoft.Xna.Framework.Graphics.Color((byte)updateValues[0], (byte)updateValues[1], (byte)updateValues[2]);//, updateValues[3]);
                this.ActionProperty.SetValue(this.Script.ActionHandler, colorValue, null);
            }
            else if (this.PropertyType.Name == "Boolean")
            {
                Boolean boolValue = false;
                Boolean.TryParse(updateValues[0].ToString(), out boolValue);
                this.ActionProperty.SetValue(this.Script.ActionHandler, boolValue, null);
            }
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