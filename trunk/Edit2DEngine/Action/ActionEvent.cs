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
        public int[] Speeds { get; set; }
        [Browsable(false)]
        public bool[] IsRelative { get; set; }
        [Browsable(false)]
        private TimeSpan startTime;
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

        public ActionEvent(Script script, string actionName, string propertyName)
        {
            this.Script = script;
            this.ActionName = actionName;

            this.PropertyName = propertyName;
            this.ActionProperty = script.ActionHandler.GetType().GetProperty(propertyName);
            this.PropertyType = this.ActionProperty.PropertyType;

            if (
                this.PropertyType.Name == "Single" ||
                this.PropertyType.Name == "Int32" ||
                this.PropertyType.Name == "Boolean")
            {
                InitVar(1);
            }
            else if (
                this.PropertyType.Name == "Vector2" ||
                this.PropertyType.Name == "Size")
            {
                InitVar(2);
            }
            else if (
                this.PropertyType.Name == "Color")
            {
                InitVar(3);
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
            this.startTime =  TimeSpan.Zero;
            this.initialized = new bool[count];
            this.rndStartValue = new float[count];
            this.deltaMs = new int[count];
            this.pct = new float[count];
        }

        public override void InitAction()
        {
            startTime = TimeSpan.Zero;

            for (int i = 0; i < this.ActionEventTypes.Length; i++)
            {
                deltaMs[i] = 0;
                pct[i] = 0f;
                initialized[i] = false;
                Playing = false;
            }
        }

        ////--- Start values
        //Vector2 vecStartValue = Vector2.Zero;
        //float floatStartValue = 0f;
        //Size sizeStartValue = Size.Empty;
        //Microsoft.Xna.Framework.Graphics.Color colorStartValue = Microsoft.Xna.Framework.Graphics.Color.Black;
        //bool boolStartValue = false;
        float[] rndStartValue;
        float rndStartValueBoolean;
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
        float distance = 0f;

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
            {
                if (this.PropertyType.Name == "Boolean")
                {
                    if (repository.GetRandomValue(0f, 10f) <= 5f)
                        rndStartValue[i] = Convert.ToSingle(true);
                    else
                        rndStartValue[i] = Convert.ToSingle(false);
                }
                else
                {
                    rndStartValue[i] = repository.GetRandomValue(RndMinValues[i], RndMaxValues[i]);
                }
            }
            //---

            initialized[i] = true;
        }

        private void StartActionEvent(TimeSpan time, Repository repository)
        {
            //--- Initialisation du timer
            startTime = time;
            //---

            for (int i = 0; i < this.ActionEventTypes.Length; i++)
            {
                //---
                deltaMs[i] = 0;
                pct[i] = 0f;
                //---

                if (repository != null)
                    GetPropertyValue(repository, i);

                if (repository != null && ActionEventTypes[i] == ActionEventType.MouseX)
                {
                    this.FloatValues[i] = repository.GetMousePosition().X - (float)startValues[i];
                }
                else if (repository != null && ActionEventTypes[i] == ActionEventType.MouseY)
                {
                    this.FloatValues[i] = repository.GetMousePosition().Y - (float)startValues[i];
                }
            }

            //--- Calcul de la distance
            //---
        }

        public void UpdateValue(Repository repository)
        {
            TimeSpan timeNow = DateTime.Now.TimeOfDay;

            bool playing = true;

            //---
            if (startTime == TimeSpan.Zero)
            {
                StartActionEvent(timeNow, repository);
            }
            //---

            for (int i = 0; i < this.ActionEventTypes.Length; i++)
            {
                updateValues[i] = 0;

                //--- Durée
                if (Durations[i] != 0 || Speeds[i] != 0)
                {
                    //if (startTimes[i] == TimeSpan.Zero)
                    //{
                    //    //StartActionEvent(i, timeNow, repository);
                    //    //initialized[i] = true;
                    //}
                    //else
                    //{
                        deltaMs[i] = (int)timeNow.Subtract(startTime).TotalMilliseconds;

                        if (Durations[i] != 0)
                            pct[i] = (float)deltaMs[i] / (float)Durations[i];
                        else if (Speeds[i] != 0)
                            pct[i] = (float)Speeds[i] * deltaMs[i] / 1000f / this.FloatValues[i];

                        if (pct[i] > 1f)
                            pct[i] = 1f;
                    //}
                }
                else
                {
                    deltaMs[i] = 0;
                    pct[i] = 0f;

                    initialized[i] = true;
                }
                //---

                float calcValue = 0f;

                //--- Calcul de la valeur
                switch (this.ActionEventTypes[i])
                {
                    case ActionEventType.Deactivated:
                        calcValue = startValues[i];
                        break;
                    case ActionEventType.FixedValue:
                        calcValue = this.FloatValues[i];
                        break;
                    case ActionEventType.MouseX:
                        calcValue = this.FloatValues[i];// repository.GetMousePosition().X - startValues[i];
                        break;
                    case ActionEventType.MouseY:
                        calcValue = this.FloatValues[i];// repository.GetMousePosition().Y - startValues[i];
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
                //---

                //--- Relatives & Durations
                if (this.IsRelative[i])
                {
                    updateValues[i] = startValues[i];
                }

                if (Durations[i] != 0 || Speeds[i] != 0)
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
                    //if (startTimes[i] != TimeSpan.Zero)
                    {
                        //deltaMs[i] = (int)timeNow.Subtract(startTimes[i]).TotalMilliseconds;

                        //--- Si le temps d'animation est écoulé, quitter la procédure de mise à jour
                        if (deltaMs[i] >= Durations[i])
                        {
                            startTime = TimeSpan.Zero;
                            initialized[i] = false;
                        }
                        //---
                    }
                }
                else if(Speeds[i] != 0)
                {
                    if(pct[i] > 0.95f)
                    {
                        startTime = TimeSpan.Zero;
                        initialized[i] = false;
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
                Boolean boolValue = Convert.ToBoolean(updateValues[0]);
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
