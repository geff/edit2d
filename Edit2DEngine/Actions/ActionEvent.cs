using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;
using Edit2DEngine.Entities;

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
        public Entity[] EntityBindings { get; set; }

        [Browsable(false)]
        public String[] EntityBindingNames { get; set; }
        [Browsable(false)]
        public String[] EntityBindingPropertyNames { get; set; }

        [Browsable(false)]
        public int[] EntityBindingPropertyId { get; set; }
        [Browsable(false)]
        public PropertyInfo[] EntityBindingProperties { get; set; }
        [Browsable(false)]
        public Boolean Playing { get; set; }

        bool[] initialized;
        int[] deltaMs;
        float[] pct;

        float[] calcFloatValues;
        float[] updateValues;
        float[] startValues;

        public int[] calcDurations;


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
            this.EntityBindings = new Entity[count];
            //this.EntityBindingPropertyNames = new String[count];
            this.EntityBindingPropertyId = new int[count];
            this.EntityBindingProperties = new PropertyInfo[count];
            this.Durations = new int[count];
            this.Speeds = new int[count];
            this.IsRelative = new bool[count];
            this.ActionEventTypes = new ActionEventType[count];
            this.startTime = TimeSpan.Zero;
            this.initialized = new bool[count];
            this.calcFloatValues = new float[count];
            this.deltaMs = new int[count];
            this.pct = new float[count];
            this.calcDurations = new int[count];
        }

        public override void InitAction()
        {
            startTime = TimeSpan.Zero;

            for (int i = 0; i < this.ActionEventTypes.Length; i++)
            {
                deltaMs[i] = 0;
                pct[i] = 0f;
                //initialized[i] = false;
                Playing = false;
            }
        }

        private float GetPropertyValue(int index)
        {
            float value = 0f;

            if (EntityBindingProperties[index].PropertyType.Name == "Vector2")
            {
                if (EntityBindingPropertyId[index] == 1)
                    value = ((Vector2)EntityBindingProperties[index].GetValue(EntityBindings[index], null)).X;
                else if (EntityBindingPropertyId[index] == 2)
                    value = ((Vector2)EntityBindingProperties[index].GetValue(EntityBindings[index], null)).Y;
            }
            else if (EntityBindingProperties[index].PropertyType.Name == "Single")
            {
                value = (Single)EntityBindingProperties[index].GetValue(EntityBindings[index], null);
            }
            else if (EntityBindingProperties[index].PropertyType.Name == "Int32")
            {
                value = (Int32)EntityBindingProperties[index].GetValue(EntityBindings[index], null);
            }
            else if (EntityBindingProperties[index].PropertyType.Name == "Color")
            {
                if (EntityBindingPropertyId[index] == 1)
                    value = ((Microsoft.Xna.Framework.Graphics.Color)EntityBindingProperties[index].GetValue(EntityBindings[index], null)).R;
                else if (EntityBindingPropertyId[index] == 2)
                    value = ((Microsoft.Xna.Framework.Graphics.Color)EntityBindingProperties[index].GetValue(EntityBindings[index], null)).G;
                else if (EntityBindingPropertyId[index] == 3)
                    value = ((Microsoft.Xna.Framework.Graphics.Color)EntityBindingProperties[index].GetValue(EntityBindings[index], null)).B;
                else if (EntityBindingPropertyId[index] == 4)
                    value = ((Microsoft.Xna.Framework.Graphics.Color)EntityBindingProperties[index].GetValue(EntityBindings[index], null)).A;
            }
            else if (EntityBindingProperties[index].PropertyType.Name == "Size")
            {
                if (EntityBindingPropertyId[index] == 1)
                    value = ((Size)EntityBindingProperties[index].GetValue(EntityBindings[index], null)).Width;
                else if (EntityBindingPropertyId[index] == 2)
                    value = ((Size)EntityBindingProperties[index].GetValue(EntityBindings[index], null)).Height;
            }
            return value;
        }

        private void GetStartPropertyValue(Repository repository, int i)
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
                        calcFloatValues[i] = Convert.ToSingle(true);
                    else
                        calcFloatValues[i] = Convert.ToSingle(false);
                }
                else
                {
                    calcFloatValues[i] = repository.GetRandomValue(RndMinValues[i], RndMaxValues[i]);
                }
            }
            //---
        }

        private void CalcActionEvent(Repository repository)
        {
            //--- Initialisation du timer et de la durée calculée
            float maxDuration = 0f;
            //---

            for (int i = 0; i < this.ActionEventTypes.Length; i++)
            {
                if (repository != null)
                {
                    GetStartPropertyValue(repository, i);

                    //--- Calcul la distance à parcourir pendant la transition
                    switch (this.ActionEventTypes[i])
                    {
                        case ActionEventType.Deactivated:
                            calcFloatValues[i] = 0;
                            break;
                        case ActionEventType.FixedValue:
                            calcFloatValues[i] = this.FloatValues[i] - (this.IsRelative[i] ? startValues[i] : 0);
                            break;
                        case ActionEventType.MouseX:
                            calcFloatValues[i] = repository.GetMousePosition().X - (this.IsRelative[i] ? startValues[i] : 0);
                            break;
                        case ActionEventType.MouseY:
                            calcFloatValues[i] = repository.GetMousePosition().Y - (this.IsRelative[i] ? startValues[i] : 0);
                            break;
                        case ActionEventType.EntityBinding:
                            calcFloatValues[i] = GetPropertyValue(i) - (this.IsRelative[i] ? startValues[i] : 0);
                            break;
                        case ActionEventType.Random:
                            calcFloatValues[i] = calcFloatValues[i] - (this.IsRelative[i] ? startValues[i] : 0);
                            break;
                        default:
                            break;
                    }
                    //----

                    //--- Calcul de la durée maximale si la transition 'Vitesse' est activée
                    if (Speeds[i] != 0)
                    {
                        float newMaxDuration = Math.Abs(calcFloatValues[i]) / (float)Speeds[i] * 1000f;

                        if (newMaxDuration > maxDuration)
                            maxDuration = newMaxDuration;
                    }
                    //---

                    //--- Applique la durée si la transition 'Durée' est activée
                    if (this.Durations[i] != 0)
                    {
                        calcDurations[i] = this.Durations[i];
                    }
                    //---
                }
            }

            //--- Applique la durée si la transition 'Vitesse' est activée
            for (int i = 0; i < this.ActionEventTypes.Length; i++)
            {
                if (Speeds[i] != 0)
                {
                    calcDurations[i] = (int)maxDuration;
                }
            }
            //---
        }

        TimeSpan prevTime = TimeSpan.Zero;

        public void UpdateValue(Repository repository)
        {
            TimeSpan timeNow = DateTime.Now.TimeOfDay;
            float calcValue = 0f;

            //--- Démarrage de l'action
            if (startTime == TimeSpan.Zero)
            {
                prevTime = timeNow;
                startTime = timeNow;
            }
            //---

            //--- Met à jour les valeurs à atteindre et la durée
            CalcActionEvent(repository);
            //---

            for (int i = 0; i < this.ActionEventTypes.Length; i++)
            {
                //--- Durée
                if (calcDurations[i] != 0)
                {
                    deltaMs[i] = (int)timeNow.Subtract(prevTime).TotalMilliseconds;

                    pct[i] = (float)deltaMs[i] / (float)calcDurations[i];

                    if (pct[i] > 1f)
                        pct[i] = 1f;

                    //--- Si le temps d'animation est écoulé, l'action va s'arrêter
                    if (deltaMs[i] >= calcDurations[i])
                    {
                        startTime = TimeSpan.Zero;
                        this.Playing = false;
                    }
                    //---
                }
                else
                {
                    deltaMs[i] = 0;
                    pct[i] = 0f;
                    startTime = TimeSpan.Zero;
                    this.Playing = false;
                }
                //---

                //--- Calcul de la valeur
                switch (this.ActionEventTypes[i])
                {
                    case ActionEventType.Deactivated:
                        calcValue = startValues[i];
                        break;
                    case ActionEventType.FixedValue:
                        calcValue = calcFloatValues[i];
                        break;
                    case ActionEventType.MouseX:
                        calcValue = calcFloatValues[i];
                        break;
                    case ActionEventType.MouseY:
                        calcValue = calcFloatValues[i];
                        break;
                    case ActionEventType.EntityBinding:
                        calcValue = calcFloatValues[i];
                        break;
                    case ActionEventType.Random:
                        calcValue = calcFloatValues[i];
                        break;
                    default:
                        break;
                }
                //---

                //--- Relatives & Durations
                updateValues[i] = 0;

                if (this.IsRelative[i])
                {
                    updateValues[i] = startValues[i];
                }

                if (calcDurations[i] != 0)
                {
                    updateValues[i] += MathHelper.Lerp(0f, calcValue, pct[i]);
                }
                else
                {
                    updateValues[i] += calcValue;
                }
                //----
            }

            prevTime = timeNow;

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
