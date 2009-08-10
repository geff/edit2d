using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace Edit2DEngine.Trigger
{
    public class TriggerValueChanged : TriggerBase
    {
        public String PropertyName { get; set; }
        public Type PropertyType { get; set; }
        public PropertyInfo TriggerProperty { get; set; }
        public TriggerValueChangedSens[] Sens { get; set; }
        public Boolean IsCustomProperty { get; set; }
        public Entite Entite { get; set; }

        public Object Value
        {
            get
            {
                if (this.PropertyType.Name == "Vector2")
                {
                    return vector2Value;
                }
                else if (this.PropertyType.Name == "Single")
                {
                    return floatValue;
                }
                else if (this.PropertyType.Name == "Size")
                {
                    return sizeValue;
                }
                else if (this.PropertyType.Name == "Color")
                {
                    return colorValue;
                }

                return null;
            }
            set
            {
                if (value == null)
                    return;

                if (this.PropertyType.Name == "Vector2")
                {
                    vector2Value = (Vector2)value;
                }
                else if (this.PropertyType.Name == "Single")
                {
                    floatValue = (float)value;
                }
                else if (this.PropertyType.Name == "Size")
                {
                    sizeValue = (Size)value;
                }
                else if (this.PropertyType.Name == "Color")
                {
                    colorValue = (Microsoft.Xna.Framework.Graphics.Color)value;
                }
            }
        }

        private Vector2 vector2Value;
        private Microsoft.Xna.Framework.Graphics.Color colorValue;
        private float floatValue;
        private Size sizeValue;

        public TriggerValueChanged(String triggerName, ITriggerHandler triggerHandler, string propertyName, TriggerValueChangedSens[] sens, Object values, bool isCustomProperty)
        {
            this.TriggerName = triggerName;

            this.TriggerHandler = triggerHandler;
            this.Entite = (Entite)triggerHandler;

            this.ListScript = new List<Script>();
            this.ListTargetScriptEntiteName = new List<string>();
            this.ListTargetScriptName = new List<string>();

            this.PropertyName = propertyName;

            if (!isCustomProperty)
                this.TriggerProperty = typeof(Entite).GetProperty(propertyName);
            else
                this.TriggerProperty = typeof(Entite).GetProperty(propertyName);

            this.PropertyType = this.TriggerProperty.PropertyType;

            this.Sens = sens;
            this.Value = values;
        }

        public override void InitTrigger(Repository repository)
        {
        }

        public override void CheckTrigger(Repository repository)
        {
            bool launchTrigger = true;

            for (int i = 0; i < Sens.Length && launchTrigger; i++)
            {
                switch (this.Sens[i])
                {
                    case TriggerValueChangedSens.Superior:
                        switch (this.PropertyType.Name)
                        {
                            case "Size":
                                if (i == 0)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Width > sizeValue.Width;
                                else if (i == 1)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Height > sizeValue.Height;
                                break;
                            case "Vector2":
                                if (i == 0)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).X > vector2Value.X;
                                else if (i == 1)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).Y > vector2Value.Y;
                                break;
                            case "Single":
                                launchTrigger &= ((float)this.TriggerProperty.GetValue(this.Entite, null) > floatValue);
                                break;
                            case "Color":
                                if (i == 0)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).R > colorValue.R);
                                else if (i == 1)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).G > colorValue.G);
                                else if (i == 2)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).B > colorValue.B);
                                else if (i == 3)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).A > colorValue.A);
                                break;

                            default:
                                break;
                        }
                        break;
                    case TriggerValueChangedSens.Inferior:
                        switch (this.PropertyType.Name)
                        {
                            case "Size":
                                if (i == 0)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Width < sizeValue.Width;
                                else if (i == 1)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Height < sizeValue.Height;
                                break;
                            case "Vector2":
                                if (i == 0)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).X < vector2Value.X;
                                else if (i == 1)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).Y < vector2Value.Y;
                                break;
                            case "Single":
                                launchTrigger &= ((float)this.TriggerProperty.GetValue(this.Entite, null) < floatValue);
                                break;
                            case "Color":
                                if (i == 0)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).R < colorValue.R);
                                else if (i == 1)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).G < colorValue.G);
                                else if (i == 2)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).B < colorValue.B);
                                else if (i == 3)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).A < colorValue.A);
                                break;

                            default:
                                break;
                        }
                        break;
                    case TriggerValueChangedSens.SuperiorOrEqual:
                        switch (this.PropertyType.Name)
                        {
                            case "Size":
                                if (i == 0)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Width >= sizeValue.Width;
                                else if (i == 1)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Height >= sizeValue.Height;
                                break;
                            case "Vector2":
                                if (i == 0)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).X >= vector2Value.X;
                                else if (i == 1)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).Y >= vector2Value.Y;
                                break;
                            case "Single":
                                launchTrigger &= ((float)this.TriggerProperty.GetValue(this.Entite, null) >= floatValue);
                                break;
                            case "Color":
                                if (i == 0)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).R >= colorValue.R);
                                else if (i == 1)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).G >= colorValue.G);
                                else if (i == 2)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).B >= colorValue.B);
                                else if (i == 3)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).A >= colorValue.A);
                                break;

                            default:
                                break;
                        }
                        break;
                    case TriggerValueChangedSens.InferiorOrEqual:
                        switch (this.PropertyType.Name)
                        {
                            case "Size":
                                if (i == 0)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Width <= sizeValue.Width;
                                else if (i == 1)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Height <= sizeValue.Height;
                                break;
                            case "Vector2":
                                if (i == 0)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).X <= vector2Value.X;
                                else if (i == 1)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).Y <= vector2Value.Y;
                                break;
                            case "Single":
                                launchTrigger &= ((float)this.TriggerProperty.GetValue(this.Entite, null) <= floatValue);
                                break;
                            case "Color":
                                if (i == 0)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).R <= colorValue.R);
                                else if (i == 1)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).G <= colorValue.G);
                                else if (i == 2)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).B <= colorValue.B);
                                else if (i == 3)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).A <= colorValue.A);
                                break;

                            default:
                                break;
                        }
                        break;
                    case TriggerValueChangedSens.Equal:
                        switch (this.PropertyType.Name)
                        {
                            case "Size":
                                if (i == 0)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Width == sizeValue.Width;
                                else if (i == 1)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Height == sizeValue.Height;
                                break;
                            case "Vector2":
                                if (i == 0)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).X == vector2Value.X;
                                else if (i == 1)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).Y == vector2Value.Y;
                                break;
                            case "Single":
                                launchTrigger &= ((float)this.TriggerProperty.GetValue(this.Entite, null) == floatValue);
                                break;
                            case "Color":
                                if (i == 0)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).R == colorValue.R);
                                else if (i == 1)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).G == colorValue.G);
                                else if (i == 2)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).B == colorValue.B);
                                else if (i == 3)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).A == colorValue.A);
                                break;

                            default:
                                break;
                        }
                        break;
                    case TriggerValueChangedSens.Different:
                        switch (this.PropertyType.Name)
                        {
                            case "Size":
                                if (i == 0)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Width != sizeValue.Width;
                                else if (i == 1)
                                    launchTrigger &= ((System.Drawing.Size)this.TriggerProperty.GetValue(this.Entite, null)).Height != sizeValue.Height;
                                break;
                            case "Vector2":
                                if (i == 0)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).X != vector2Value.X;
                                else if (i == 1)
                                    launchTrigger &= ((Vector2)this.TriggerProperty.GetValue(this.Entite, null)).Y != vector2Value.Y;
                                break;
                            case "Single":
                                launchTrigger &= ((float)this.TriggerProperty.GetValue(this.Entite, null) != floatValue);
                                break;
                            case "Color":
                                if (i == 0)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).R != colorValue.R);
                                else if (i == 1)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).G != colorValue.G);
                                else if (i == 2)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).B != colorValue.B);
                                else if (i == 3)
                                    launchTrigger &= (((Microsoft.Xna.Framework.Graphics.Color)this.TriggerProperty.GetValue(this.Entite, null)).A != colorValue.A);
                                break;

                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }

            if (launchTrigger)
                LaunchScript(repository);
        }
    }

    public enum TriggerValueChangedSens : int
    {
        None = 0,
        Superior = 1,
        Inferior = 2,
        SuperiorOrEqual = 3,
        InferiorOrEqual = 4,
        Equal = 5,
        Different = 6
    }
}
