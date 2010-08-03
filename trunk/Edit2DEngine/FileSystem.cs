using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Xml;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics.Dynamics.Joints;
using Microsoft.Xna.Framework;
using Edit2DEngine.Action;
using Edit2DEngine.Trigger;
using Edit2DEngine.Particles;
using System.Drawing;
using FarseerGames.FarseerPhysics.Collisions;

namespace Edit2DEngine
{
    public static class FileSystem
    {
        private static void OpenEntity(Entite entite, bool isParticle, XmlTextReader reader, Repository repository)
        {
            //entite.AngularSpeed = float.Parse(reader["AngularSpeed"]);
            entite.IsColisionable = bool.Parse(reader["IsColisionable"]);
            entite.IsStatic = bool.Parse(reader["IsStatic"]);
            entite.Name = reader["Name"];

            int pos = reader["Position"].IndexOf(';');
            entite.SetPosition(ReadVector2(reader["Position"]));

            entite.Rotation = float.Parse(reader["Rotation"]);

            pos = reader["Size"].IndexOf(';');
            entite.Size = new Size(int.Parse(reader["Size"].Substring(0, pos)), int.Parse(reader["Size"].Substring(pos + 1, reader["Size"].Length - pos - 1)));

            if (reader["Color"] != null)
            {
                //string[] strColors = reader["Color"].Split(new char[] { ' ', '{', '}', 'R', 'G', 'B', 'A', ':' }, StringSplitOptions.RemoveEmptyEntries);
                //entite.Color = new Microsoft.Xna.Framework.Graphics.Color(byte.Parse(strColors[0]), byte.Parse(strColors[1]), byte.Parse(strColors[2]), byte.Parse(strColors[3]));
                entite.Color = reader["Color"].ToColor();
            }

            if (reader["IsInBackground"] != null)
                entite.IsInBackground = bool.Parse(reader["IsInBackground"]);

            if (reader["BlurFactor"] != null)
                entite.BlurFactor = float.Parse(reader["BlurFactor"]);

            if (reader["FrictionCoefficient"] != null)
                entite.FrictionCoefficient = float.Parse(reader["FrictionCoefficient"]);

            if (reader["RestitutionCoefficient"] != null)
                entite.RestitutionCoefficient = float.Parse(reader["RestitutionCoefficient"]);

            if (reader["Layer"] != null)
                entite.Layer = int.Parse(reader["Layer"]);

            Texture2D texture = TextureManager.LoadTexture2D(entite.TextureName);

            //if(!isParticle)
            //    repository.ChangeEntitySize(entite, new Size(texture.Width, texture.Height));

            entite.TextureName = reader["TextureName"];
            
            bool ist  =entite.geom.Body == entite.Body;
        }

        public static void Open(string fileName, Repository repository)
        {
            {
                XmlTextReader reader = new XmlTextReader(fileName);

                Entite entite = null;
                int indexPoints = 0;

                repository.listEntite = new List<Entite>();
                Repository.physicSimulator.Clear();
                ITriggerHandler currentTriggerHandler = null;
                IActionHandler currentActionHandler = null;

                Dictionary<String, IActionHandler> dicActionHandler = new Dictionary<string, IActionHandler>();

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "World")
                        {
                            repository.World = new World();
                            currentTriggerHandler = repository.World;
                        }
                        if (reader.Name == "Entite")
                        {
                            //string name = repository.FoundNewName(reader["Name"]);
                            string name = reader["Name"].ToString();

                            entite = new Entite(true, reader["TextureName"], name);

                            OpenEntity(entite, false, reader, repository);

                            currentTriggerHandler = entite;
                            currentActionHandler = entite;

                            dicActionHandler.Add(currentActionHandler.Name, currentActionHandler);
                            repository.listEntite.Add(entite);
                        }
                        else if (reader.Name == "FixedLinearSpring")
                        {
                            Entite ent = repository.listEntite.Last();

                            FixedLinearSpring spring = new FixedLinearSpring();
                            ent.ListFixedLinearSpring.Add(spring);

                            spring.Body = entite.Body;
                            spring.BodyAttachPoint = ReadVector2(reader["BodyAttachPoint"]);
                            //spring..Position = ReadVector2(reader["Position"]);
                            spring.WorldAttachPoint = ReadVector2(reader["WorldAttachPoint"]);
                            spring.RestLength = float.Parse(reader["RestLength"]);
                            spring.DampingConstant = 10f;
                            spring.SpringConstant = 10f;

                            Repository.physicSimulator.SpringList.Add(spring);
                        }
                        else if (reader.Name == "LinearSpring")
                        {
                            Entite ent = repository.listEntite.Last();

                            //ent.AddLinearSpring

                            LinearSpring spring = new LinearSpring();
                            ent.ListLinearSpring.Add(spring);

                            //SpringFactory.Instance.CreateLinearSpring(Repository.physicSimulator, body, vec1, entite.body, vec2, 10f, 10f);

                            spring.Body1 = ent.Body;
                            spring.Body2 = ent.Body;
                            spring.Tag = reader["Body2"];
                            spring.AttachPoint1 = ReadVector2(reader["AttachPoint1"]);
                            spring.AttachPoint2 = ReadVector2(reader["AttachPoint2"]);
                            spring.RestLength = float.Parse(reader["RestLength"]);
                            spring.DampingConstant = 10f;
                            spring.SpringConstant = 10f;

                            Repository.physicSimulator.SpringList.Add(spring);
                        }
                        else if (reader.Name == "FixedRevoluteJoint")
                        {
                            Entite ent = repository.listEntite.Last();

                            FixedRevoluteJoint joint = new FixedRevoluteJoint();
                            ent.ListFixedRevoluteJoint.Add(joint);

                            joint.Body = entite.Body;
                            joint.Anchor = ReadVector2(reader["Anchor"]);
                            joint.MaxImpulse = float.Parse(reader["MaxImpulse"]);

                            Repository.physicSimulator.JointList.Add(joint);
                        }
                        else if (reader.Name == "Script")
                        {
                            Script script = new Script(reader["ScriptName"], currentActionHandler);

                            currentActionHandler.ListScript.Add(script);
                        }
                        else if (reader.Name == "Curve")
                        {
                            Script scr = currentActionHandler.ListScript.Last();

                            ActionCurve curve = new ActionCurve(scr, reader["ActionName"], bool.Parse(reader["IsRelative"]), bool.Parse(reader["IsLoop"]), reader["PropertyName"]);

                            scr.ListAction.Add(curve);

                            indexPoints = -1;
                        }
                        else if (reader.Name == "Points")
                        {
                            indexPoints++;
                        }
                        else if (reader.Name == "Point")
                        {
                            CurveKey point = new CurveKey(float.Parse(reader["X"]), float.Parse(reader["Y"]));

                            Script scr = currentActionHandler.ListScript.Last();
                            Curve crv = ((ActionCurve)scr.ListAction.Last(action => action is ActionCurve)).ListCurve[indexPoints];

                            crv.Keys.Add(point);

                            ((ActionCurve)scr.ListAction.Last()).CalcDuration();
                        }
                        else if (reader.Name == "Event")
                        {
                            //Entite ent = repository.listEntite.Last();
                            //Script scr = ent.ListScript.Last();

                            //ActionEvent actionEvent = new ActionEvent(scr, reader["ActionName"], bool.Parse(reader["IsRelative"]), reader["PropertyName"]);

                            //if (actionEvent.PropertyType.Name == "Color")
                            //{
                            //    actionEvent.Value = reader["Color"].ToColor();
                            //}
                            //else if (actionEvent.PropertyType.Name == "Single")
                            //{
                            //    actionEvent.Value = float.Parse(reader["Value"]);
                            //}
                            //else if (actionEvent.PropertyType.Name == "Vector2")
                            //{
                            //    actionEvent.Value = reader["Value"].ToVector2();
                            //}

                            ////if (reader["ChangeValue"] != null)
                            ////{
                            ////    actionEvent.ChangeValue = reader["ChangeValue"].ToBools();
                            ////}

                            //scr.ListAction.Add(actionEvent);

                            //indexPoints = -1;
                        }
                        else if (reader.Name == "TriggerCollision")
                        {
                            TriggerCollision triggerCollision = new TriggerCollision(reader["TriggerName"], currentTriggerHandler, null);

                            triggerCollision.TargetCollisionEntiteName = reader["TargetEntiteName"];

                            currentTriggerHandler.ListTrigger.Add(triggerCollision);
                        }
                        else if (reader.Name == "TriggerScript")
                        {
                            TriggerBase trigger = currentTriggerHandler.ListTrigger.Last();

                            trigger.ListTargetActionHandlerName.Add(reader["ScriptActionHandlerName"]);
                            trigger.ListTargetScriptName.Add(reader["ScriptName"]);
                        }
                        else if (reader.Name == "TriggerValueChanged")
                        {
                            TriggerValueChanged triggerValueChanged = new TriggerValueChanged(reader["TriggerName"], currentTriggerHandler, reader["PropertyName"], null, null, bool.Parse(reader["IsCustomProperty"]));

                            string[] sens = reader["Sens"].Split(new char[] { ' ', '{', '}', ';' }, StringSplitOptions.RemoveEmptyEntries);
                            TriggerValueChangedSens[] sensTrigger = new TriggerValueChangedSens[sens.Length];

                            for (int i = 0; i < sens.Length; i++)
                            {
                                sensTrigger[i] = (TriggerValueChangedSens)int.Parse(sens[i]);
                            }

                            triggerValueChanged.Sens = sensTrigger;


                            if (triggerValueChanged.PropertyType.Name == "Vector2")
                            {

                                triggerValueChanged.Value = reader["Value"].ToVector2();
                            }
                            else if (triggerValueChanged.PropertyType.Name == "Single")
                            {
                                triggerValueChanged.Value = float.Parse(reader["Value"]);
                            }
                            else if (triggerValueChanged.PropertyType.Name == "Color")
                            {
                                triggerValueChanged.Value = reader["Value"].ToColor();
                            }

                            currentTriggerHandler.ListTrigger.Add(triggerValueChanged);
                        }
                        else if (reader.Name == "TriggerMouse")
                        {
                            TriggerMouse triggerMouse = new TriggerMouse(reader["TriggerName"], currentTriggerHandler, (TriggerMouseType)int.Parse(reader["TriggerMouseType"]));

                            currentTriggerHandler.ListTrigger.Add(triggerMouse);
                        }
                        else if (reader.Name == "TriggerLoad")
                        {
                            TriggerLoad triggerLoad = new TriggerLoad(reader["TriggerName"], currentTriggerHandler);
                            currentTriggerHandler.ListTrigger.Add(triggerLoad);
                        }
                        else if (reader.Name == "TriggerTime")
                        {
                            TriggerTime triggerTime = new TriggerTime(reader["TriggerName"], currentTriggerHandler);
                            triggerTime.TimeLoop = (int)float.Parse(reader["TimeLoop"]);

                            currentTriggerHandler.ListTrigger.Add(triggerTime);
                        }
                        else if (reader.Name == "ParticleSystem")
                        {
                            Entite ent = repository.listEntite.Last();

                            ParticleSystem pSystem = new ParticleSystem(ent);
                            pSystem.EmmittingAngle = float.Parse(reader["EmmittingAngle"]);
                            pSystem.EmmittingFromAllSurface = bool.Parse(reader["EmmittingFromAllSurface"]);
                            pSystem.FieldAngle = float.Parse(reader["FieldAngle"]);
                            pSystem.Name = reader["Name"];//ParticleSystemName
                            pSystem.Rate = int.Parse(reader["Rate"]);
                            pSystem.Velocity = float.Parse(reader["Velocity"]);

                            currentActionHandler = pSystem;
                            dicActionHandler.Add(currentActionHandler.Name, currentActionHandler);

                            ent.ListParticleSystem.Add(pSystem);
                        }
                        else if (reader.Name == "ParticleTemplate")
                        {
                            Entite ent = repository.listEntite.Last();
                            ParticleSystem psSystem = ent.ListParticleSystem.Last();

                            Particle particleTemplate = new Particle(false, reader["TextureName"], reader["Name"], psSystem);
                            OpenEntity(particleTemplate, true, reader, repository);

                            particleTemplate.LifeTime = int.Parse(reader["LifeTime"]);

                            psSystem.ListParticleTemplate.Add(particleTemplate);
                        }
                    }
                }

                reader.Close();

                //--- Associe les scripts aux trigger de l'obet World
                LinkTriggerToScript(repository.World, dicActionHandler);
                //---

                foreach (Entite ent in repository.listEntite)
                {
                    foreach (LinearSpring spring in ent.ListLinearSpring)
                    {
                        spring.Body2 = repository.listEntite.Find(e => e.Name == spring.Tag.ToString()).Body;
                    }

                    //--- Associe les scripts aux triggers de l'entité
                    LinkTriggerToScript(ent, dicActionHandler);
                    //---
                }

                //Repository.physicSimulator.Update(0.00002f);

                //for (int i = 0; i < Repository.physicSimulator.GeomList.Count; i++)
                //{
                //    //Repository.physicSimulator.GeomList[i].SetBody(repository.listEntite[i].Body);
                //    //repository.listEntite[i].geom = Repository.physicSimulator.GeomList[i];
                //}

                repository.OrderEntite();
            }
        }

        private static void LinkTriggerToScript(ITriggerHandler triggerHandler, Dictionary<String, IActionHandler> dicActionHandler)
        {
            foreach (TriggerBase trigger in triggerHandler.ListTrigger)
            {
                for (int i = 0; i < trigger.ListTargetActionHandlerName.Count; i++)
                {
                    string scriptName = trigger.ListTargetScriptName[i];
                    string actionHandlerName = trigger.ListTargetActionHandlerName[i];

                    //Script script = repository.listEntite.Find(ent2 => ent2.Name == entiteName).ListScript.Find(scr => scr.ScriptName == scriptName);
                    Script script = dicActionHandler[actionHandlerName].ListScript.Find(scr => scr.ScriptName == scriptName);

                    if (script != null)
                        trigger.ListScript.Add(script);
                }

                if (trigger is TriggerCollision)
                {
                    TriggerCollision triggerCol = (TriggerCollision)trigger;

                    triggerCol.TargetEntite = (Entite)dicActionHandler.First(ent2 => ent2.Value is Entite && ent2.Value.Name == triggerCol.TargetCollisionEntiteName).Value;
                }
            }
        }

        private static void SaveEntity(Entite entite, bool isParticle, XmlTextWriter writer, Repository repository)
        {
            if (isParticle)
            {
                writer.WriteStartElement("ParticleTemplate");

                Particle particle = (Particle)entite;

                writer.WriteAttributeString("LifeTime", particle.LifeTime.ToString());
            }
            else
                writer.WriteStartElement("Entite");

            writer.WriteAttributeString("Position", WriteVector2(entite.Position));
            writer.WriteAttributeString("Name", entite.Name);
            writer.WriteAttributeString("TextureName", entite.TextureName);
            writer.WriteAttributeString("IsColisionable", entite.IsColisionable.ToString());
            writer.WriteAttributeString("IsStatic", entite.IsStatic.ToString());
            writer.WriteAttributeString("Size", String.Format("{0};{1}", entite.Size.Width, entite.Size.Height));
            writer.WriteAttributeString("Rotation", String.Format("{0:0.00}", entite.Rotation));

            writer.WriteAttributeString("Color", entite.Color.ToString());
            writer.WriteAttributeString("BlurFactor", String.Format("{0:0.00}", entite.BlurFactor));
            writer.WriteAttributeString("IsInBackground", entite.IsInBackground.ToString());

            writer.WriteAttributeString("FrictionCoefficient", WriteFloat(entite.FrictionCoefficient));
            writer.WriteAttributeString("RestitutionCoefficient", WriteFloat(entite.RestitutionCoefficient));
            writer.WriteAttributeString("Layer", entite.Layer.ToString());

            #region Springs
            for (int j = 0; j < entite.ListFixedLinearSpring.Count; j++)
            {
                writer.WriteStartElement("FixedLinearSpring");

                FixedLinearSpring spring = entite.ListFixedLinearSpring[j];

                writer.WriteAttributeString("BodyAttachPoint", WriteVector2(spring.BodyAttachPoint));
                //writer.WriteAttributeString("Position", WriteVector2(spring.Position));
                writer.WriteAttributeString("RestLength", WriteFloat(spring.RestLength));
                writer.WriteAttributeString("WorldAttachPoint", WriteVector2(spring.WorldAttachPoint));
                writer.WriteEndElement();
            }

            for (int j = 0; j < entite.ListLinearSpring.Count; j++)
            {
                writer.WriteStartElement("LinearSpring");

                LinearSpring spring = entite.ListLinearSpring[j];

                Entite entite2 = repository.GetEntiteFromBody(spring.Body2);

                writer.WriteAttributeString("Body2", entite2.Name);

                writer.WriteAttributeString("AttachPoint1", WriteVector2(spring.AttachPoint1));
                writer.WriteAttributeString("AttachPoint2", WriteVector2(spring.AttachPoint2));
                writer.WriteAttributeString("RestLength", WriteFloat(spring.RestLength));

                writer.WriteEndElement();
            }
            #endregion

            #region Joints
            for (int j = 0; j < entite.ListFixedRevoluteJoint.Count; j++)
            {
                writer.WriteStartElement("FixedRevoluteJoint");

                FixedRevoluteJoint joint = entite.ListFixedRevoluteJoint[j];

                writer.WriteAttributeString("Anchor", WriteVector2(joint.Anchor));
                writer.WriteAttributeString("MaxImpulse", WriteFloat(joint.MaxImpulse));
                writer.WriteEndElement();
            }
            #endregion

            #region Scripts & Actions
            for (int j = 0; j < entite.ListScript.Count; j++)
            {
                writer.WriteStartElement("Script");

                Script script = entite.ListScript[j];

                writer.WriteAttributeString("ScriptName", script.ScriptName);

                for (int k = 0; k < script.ListAction.Count; k++)
                {
                    ActionBase action = script.ListAction[k];

                    if (action is ActionCurve)
                    {
                        writer.WriteStartElement("Curve");

                        ActionCurve actionCurve = (ActionCurve)action;

                        writer.WriteAttributeString("ActionName", actionCurve.ActionName);
                        writer.WriteAttributeString("PropertyName", actionCurve.PropertyName);
                        writer.WriteAttributeString("IsRelative", actionCurve.IsRelative.ToString());
                        writer.WriteAttributeString("IsLoop", actionCurve.IsLoop.ToString());

                        for (int l = 0; l < actionCurve.ListCurve.Count; l++)
                        {
                            writer.WriteStartElement("Points");

                            Curve curve = actionCurve.ListCurve[l];

                            for (int m = 0; m < curve.Keys.Count; m++)
                            {
                                writer.WriteStartElement("Point");
                                writer.WriteAttributeString("X", WriteFloat(curve.Keys[m].Position));
                                writer.WriteAttributeString("Y", WriteFloat(curve.Keys[m].Value));
                                writer.WriteEndElement();
                            }

                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    else if (action is ActionEvent)
                    {
                        //writer.WriteStartElement("Event");

                        //ActionEvent actionEvent = (ActionEvent)action;

                        //writer.WriteAttributeString("ActionName", actionEvent.ActionName);
                        //writer.WriteAttributeString("PropertyName", actionEvent.PropertyName);
                        //writer.WriteAttributeString("IsRelative", actionEvent.IsRelative.ToString());

                        //writer.WriteAttributeString("ChangeValue", actionEvent.ChangeValue.ArrayToString());

                        //writer.WriteAttributeString("Value", actionEvent.Value.ToString());

                        //writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
            }
            #endregion

            #region Triggers
            for (int j = 0; j < entite.ListTrigger.Count; j++)
            {
                TriggerBase trigger = entite.ListTrigger[j];

                SaveTrigger(writer, trigger);
            }
            #endregion

            #region Particle system
            foreach (ParticleSystem psystem in entite.ListParticleSystem)
            {
                writer.WriteStartElement("ParticleSystem");

                writer.WriteAttributeString("Name", psystem.Name);
                //writer.WriteAttributeString("ParticleSystemName", psystem.ParticleSystemName);
                writer.WriteAttributeString("EmmittingAngle", WriteFloat(psystem.EmmittingAngle));
                writer.WriteAttributeString("FieldAngle", WriteFloat(psystem.FieldAngle));
                writer.WriteAttributeString("EmmittingFromAllSurface", psystem.EmmittingFromAllSurface.ToString());
                writer.WriteAttributeString("Rate", psystem.Rate.ToString());
                writer.WriteAttributeString("Velocity", WriteFloat(psystem.Velocity));

                foreach (Particle particleTemplate in psystem.ListParticleTemplate)
                {
                    SaveEntity(particleTemplate, true, writer, repository);
                }

                writer.WriteEndElement();
            }
            #endregion

            writer.WriteEndElement();
        }

        private static void SaveTrigger(XmlWriter writer, TriggerBase trigger)
        {
            if (trigger is TriggerCollision)
            {
                TriggerCollision triggerCollision = (TriggerCollision)trigger;

                writer.WriteStartElement("TriggerCollision");

                writer.WriteAttributeString("TriggerName", triggerCollision.TriggerName);

                writer.WriteAttributeString("TargetEntiteName", triggerCollision.TargetEntite.Name);
            }
            else if (trigger is TriggerValueChanged)
            {
                TriggerValueChanged triggerValueChanged = (TriggerValueChanged)trigger;

                writer.WriteStartElement("TriggerValueChanged");

                writer.WriteAttributeString("TriggerName", triggerValueChanged.TriggerName);

                string sens = "{";
                for (int k = 0; k < triggerValueChanged.Sens.Length; k++)
                {
                    sens += ";" + ((int)triggerValueChanged.Sens[k]).ToString();
                }
                sens += "}";

                writer.WriteAttributeString("PropertyName", triggerValueChanged.PropertyName);
                writer.WriteAttributeString("Sens", sens);
                writer.WriteAttributeString("Value", triggerValueChanged.Value.ToString());
                writer.WriteAttributeString("IsCustomProperty", triggerValueChanged.IsCustomProperty.ToString());
            }
            else if (trigger is TriggerMouse)
            {
                TriggerMouse triggerMouse = (TriggerMouse)trigger;

                writer.WriteStartElement("TriggerMouse");
                writer.WriteAttributeString("TriggerName", triggerMouse.TriggerName);
                writer.WriteAttributeString("TriggerMouseType", ((int)triggerMouse.TriggerMouseType).ToString());
            }
            else if (trigger is TriggerLoad)
            {
                TriggerLoad triggerLoad = (TriggerLoad)trigger;
                writer.WriteStartElement("TriggerLoad");
                writer.WriteAttributeString("TriggerName", triggerLoad.TriggerName);
            }
            else if (trigger is TriggerTime)
            {
                TriggerTime triggerTime = (TriggerTime)trigger;

                writer.WriteStartElement("TriggerTime");
                writer.WriteAttributeString("TriggerName", triggerTime.TriggerName);
                writer.WriteAttributeString("TimeLoop", WriteFloat(triggerTime.TimeLoop));
            }

            foreach (Script script in trigger.ListScript)
            {
                writer.WriteStartElement("TriggerScript");

                //TODO : gérer correctement selon l'origine de l'entité (Entite ou ParticleSystem)
                writer.WriteAttributeString("ScriptActionHandlerName", script.ActionHandler.Name);
                writer.WriteAttributeString("ScriptName", script.ScriptName);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public static void Save(string fileName, Repository repository)
        {
            XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.Default);

            writer.Formatting = Formatting.Indented;

            writer.WriteStartDocument();

            //--- Root
            writer.WriteStartElement("Root");
            //---

            //--- Header
            writer.WriteStartElement("Header");
            writer.WriteEndElement();
            //---

            //--- World
            writer.WriteStartElement("World");
            for (int i = 0; i < repository.World.ListTrigger.Count; i++)
			{
                SaveTrigger(writer, repository.World.ListTrigger[i]);
			}
            writer.WriteEndElement();
            //---

            //--- Liste des entités
            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];

                SaveEntity(entite, false, writer, repository);
            }
            //---

            //writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }

        private static string WriteFloat(float value)
        {
            return String.Format("{0:0.00}", value);
        }

        private static string WriteVector2(Vector2 vector)
        {
            return String.Format("{0:0.00};{1:0.00}", vector.X, vector.Y);
        }

        private static Vector2 ReadVector2(string vector)
        {
            int pos = vector.IndexOf(';');
            return new Vector2(float.Parse(vector.Substring(0, pos)), float.Parse(vector.Substring(pos + 1, vector.Length - pos - 1)));
        }
    }
}
