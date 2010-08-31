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
using Edit2DEngine.Actions;

using Edit2DEngine.Entities.Particles;
using System.Drawing;
using FarseerGames.FarseerPhysics.Collisions;
using Edit2DEngine.Entities;
using Edit2DEngine.Triggers;

namespace Edit2DEngine.Tools
{
    public static class FileSystem
    {
        private static void OpenEntity(Entity entity, bool isParticle, XmlTextReader reader, Repository repository)
        {
            //entity.IsStatic = bool.Parse(reader["IsStatic"]);
            entity.Name = reader["Name"];
            entity.Position = ReadVector2(reader["Position"]);
            entity.Rotation = float.Parse(reader["Rotation"]);

            if (reader["Layer"] != null)
                entity.Layer = int.Parse(reader["Layer"]);
        }

        private static void OpenEntitySprite(EntitySprite entitySprite, XmlTextReader reader, Repository repository)
        {
            entitySprite.Name = reader["Name"];
            entitySprite.SetPosition(ReadVector2(reader["Position"]));
            entitySprite.Rotation = float.Parse(reader["Rotation"]);

            int pos = reader["Size"].IndexOf(';');
            //entitySprite.Size = new Size(int.Parse(reader["Size"].Substring(0, pos)), int.Parse(reader["Size"].Substring(pos + 1, reader["Size"].Length - pos - 1)));

            if (reader["Color"] != null)
            {
                //string[] strColors = reader["Color"].Split(new char[] { ' ', '{', '}', 'R', 'G', 'B', 'A', ':' }, StringSplitOptions.RemoveEmptyEntries);
                //entitySprite.Color = new Microsoft.Xna.Framework.Graphics.Color(byte.Parse(strColors[0]), byte.Parse(strColors[1]), byte.Parse(strColors[2]), byte.Parse(strColors[3]));
                entitySprite.Color = reader["Color"].ToColor();
            }

            if (reader["IsInBackground"] != null)
                entitySprite.IsInBackground = bool.Parse(reader["IsInBackground"]);

            if (reader["BlurFactor"] != null)
                entitySprite.BlurFactor = float.Parse(reader["BlurFactor"]);

            if (reader["FrictionCoefficient"] != null)
                entitySprite.FrictionCoefficient = float.Parse(reader["FrictionCoefficient"]);

            if (reader["RestitutionCoefficient"] != null)
                entitySprite.RestitutionCoefficient = float.Parse(reader["RestitutionCoefficient"]);

            Texture2D texture = TextureManager.LoadTexture2D(entitySprite.TextureName);

            entitySprite.TextureName = reader["TextureName"];
        }

        public static void Open(string fileName, Repository repository)
        {
            {
                XmlTextReader reader = new XmlTextReader(fileName);

                Entity entity = null;
                EntitySprite entitySprite = null;
                int indexPoints = 0;
                int curActionEventIndex = 0;

                repository.listEntity = new List<Entity>();
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
                        if (reader.Name == "Entity")
                        {
                            string name = reader["Name"].ToString();

                            entity = new Entity(reader["Name"]);

                            OpenEntity(entity, false, reader, repository);

                            currentTriggerHandler = entity;
                            currentActionHandler = entity;

                            dicActionHandler.Add(currentActionHandler.Name, currentActionHandler);
                            repository.listEntity.Add(entity);
                        }
                        else if (reader.Name == "EntitySprite")
                        {
                            entitySprite = new EntitySprite(true, reader["TextureName"], reader["Name"]);
                            OpenEntitySprite(entitySprite, reader, repository);

                            entity.ListEntityComponent.Add(entitySprite);
                        }
                        else if (reader.Name == "FixedLinearSpring")
                        {
                            FixedLinearSpring spring = new FixedLinearSpring();
                            entitySprite.ListFixedLinearSpring.Add(spring);

                            spring.Body = entitySprite.Body;
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
                            LinearSpring spring = new LinearSpring();
                            entitySprite.ListLinearSpring.Add(spring);

                            //SpringFactory.Instance.CreateLinearSpring(Repository.physicSimulator, body, vec1, entity.body, vec2, 10f, 10f);

                            spring.Body1 = entitySprite.Body;
                            spring.Body2 = entitySprite.Body;
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
                            FixedRevoluteJoint joint = new FixedRevoluteJoint();
                            entitySprite.ListFixedRevoluteJoint.Add(joint);

                            joint.Body = entitySprite.Body;
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
                            Script scr = currentActionHandler.ListScript.Last();

                            ActionEvent actionEvent = new ActionEvent(scr, reader["ActionName"], reader["PropertyName"]);

                            scr.ListAction.Add(actionEvent);
                        }
                        else if (reader.Name == "EventDetail")
                        {
                            Script scr = currentActionHandler.ListScript.Last();

                            ActionEvent actionEvent = (ActionEvent)scr.ListAction.Last();

                            curActionEventIndex = int.Parse(reader["EventIndex"]);

                            actionEvent.IsRelative[curActionEventIndex] = bool.Parse(reader["IsRelative"]);
                            actionEvent.Durations[curActionEventIndex] = int.Parse(reader["Duration"]);
                            actionEvent.Speeds[curActionEventIndex] = int.Parse(reader["Speed"]);
                        }
                        else if (reader.Name == "Deactivated")
                        {
                            Script scr = currentActionHandler.ListScript.Last();
                            ActionEvent actionEvent = (ActionEvent)scr.ListAction.Last();

                            actionEvent.ActionEventTypes[curActionEventIndex] = ActionEventType.Deactivated;
                        }
                        else if (reader.Name == "FixedValue")
                        {
                            Script scr = currentActionHandler.ListScript.Last();
                            ActionEvent actionEvent = (ActionEvent)scr.ListAction.Last();

                            actionEvent.ActionEventTypes[curActionEventIndex] = ActionEventType.FixedValue;
                            actionEvent.FloatValues[curActionEventIndex] = float.Parse(reader["Value"]);
                        }
                        else if (reader.Name == "MouseX")
                        {
                            Script scr = currentActionHandler.ListScript.Last();
                            ActionEvent actionEvent = (ActionEvent)scr.ListAction.Last();

                            actionEvent.ActionEventTypes[curActionEventIndex] = ActionEventType.MouseX;
                        }
                        else if (reader.Name == "MouseY")
                        {
                            Script scr = currentActionHandler.ListScript.Last();
                            ActionEvent actionEvent = (ActionEvent)scr.ListAction.Last();

                            actionEvent.ActionEventTypes[curActionEventIndex] = ActionEventType.MouseY;
                        }
                        else if (reader.Name == "EntityBinding")
                        {
                            Script scr = currentActionHandler.ListScript.Last();
                            ActionEvent actionEvent = (ActionEvent)scr.ListAction.Last();

                            actionEvent.ActionEventTypes[curActionEventIndex] = ActionEventType.EntityBinding;
                            actionEvent.EntityBindingNames[curActionEventIndex] = reader["EntityName"];
                            actionEvent.EntityBindingPropertyNames[curActionEventIndex] = reader["PropertyName"];
                            actionEvent.EntityBindingPropertyId[curActionEventIndex] = int.Parse(reader["PropertyIndex"]);
                        }
                        else if (reader.Name == "Random")
                        {
                            Script scr = currentActionHandler.ListScript.Last();
                            ActionEvent actionEvent = (ActionEvent)scr.ListAction.Last();

                            actionEvent.ActionEventTypes[curActionEventIndex] = ActionEventType.Random;
                            actionEvent.RndMinValues[curActionEventIndex] = int.Parse(reader["RandomMinValue"]);
                            actionEvent.RndMaxValues[curActionEventIndex] = int.Parse(reader["RandomMaxValue"]);
                        }
                        else if (reader.Name == "TriggerCollision")
                        {
                            TriggerCollision triggerCollision = new TriggerCollision(reader["TriggerName"], (ITriggerCollisionHandler)currentTriggerHandler, null);

                            triggerCollision.TargetCollisionEntityName = reader["TargetEntityName"];

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
                            TriggerMouse triggerMouse = new TriggerMouse(reader["TriggerName"], (ITriggerMouseHandler)currentTriggerHandler, (TriggerMouseType)int.Parse(reader["TriggerMouseType"]));

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
                            Entity ent = repository.listEntity.Last();

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
                            Entity ent = repository.listEntity.Last();
                            ParticleSystem psSystem = ent.ListParticleSystem.Last();
                            IParticle particleTemplate = null;

                            if (reader["ParticleType"] == "Sprite")
                            {
                                particleTemplate = new ParticleSprite(false, true, reader["TextureName"], reader["Name"]);
                            }
                            else if(reader["ParticleType"] == "Text")
                            {
                            }
                            else if (reader["ParticleType"] == "3DModel")
                            {
                            }

                            //IParticle particleTemplate = new IParticle(false, reader["TextureName"], reader["Name"], psSystem);

                            //OpenEntity(particleTemplate, true, reader, repository);

                            particleTemplate.LifeTime = int.Parse(reader["LifeTime"]);

                            psSystem.ListParticleTemplate.Add(particleTemplate);
                        }
                    }
                }

                reader.Close();

                //--- Associe les scripts aux trigger de l'obet World
                LinkTriggerToScript(repository.World, dicActionHandler);
                //---

                //--- Associe le deuxième corps des ressorts
                foreach (Entity ent in repository.listEntity)
                {
                    foreach (EntityComponent entityComponent in ent.ListEntityComponent)
                    {
                        if (entityComponent is EntitySprite)
                        {
                            foreach (LinearSpring spring in ((EntitySprite)entityComponent).ListLinearSpring)
                            {
                                foreach (Entity ent2 in repository.listEntity)
                                {
                                    foreach (EntityComponent entityComponent2 in ent.ListEntityComponent)
                                    {
                                        if (entityComponent2 is EntitySprite &&
                                            spring.Tag.ToString() == ent2.Name + "-" + entityComponent2.Name)
                                        {
                                            spring.Body2 = ((EntitySprite)entityComponent2).Body;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //--- Associe les scripts aux triggers de l'entité
                    LinkTriggerToScript(ent, dicActionHandler);
                    //---
                }
                //---

                //Repository.physicSimulator.Update(0.00002f);

                //for (int i = 0; i < Repository.physicSimulator.GeomList.Count; i++)
                //{
                //    //Repository.physicSimulator.GeomList[i].SetBody(repository.listEntity[i].Body);
                //    //repository.listEntity[i].geom = Repository.physicSimulator.GeomList[i];
                //}

                repository.OrderEntity();
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

                    //Script script = repository.listEntity.Find(ent2 => ent2.Name == entityName).ListScript.Find(scr => scr.ScriptName == scriptName);
                    Script script = dicActionHandler[actionHandlerName].ListScript.Find(scr => scr.ScriptName == scriptName);

                    if (script != null)
                        trigger.ListScript.Add(script);
                }

                if (trigger is TriggerCollision)
                {
                    TriggerCollision triggerCol = (TriggerCollision)trigger;

                    triggerCol.TargetEntity = (ITriggerCollisionHandler)dicActionHandler.First(ent2 => ent2.Value is Entity && ent2.Value.Name == triggerCol.TargetCollisionEntityName).Value;
                }
            }
        }

        private static void SaveEntity(Entity entity, bool isParticle, XmlTextWriter writer, Repository repository)
        {
            if (isParticle)
            {
                writer.WriteStartElement("ParticleTemplate");

                IParticle particle = (IParticle)entity;

                writer.WriteAttributeString("LifeTime", particle.LifeTime.ToString());
            }
            else
                writer.WriteStartElement("Entity");

            writer.WriteAttributeString("Position", WriteVector2(entity.Position));
            writer.WriteAttributeString("Name", entity.Name);
           
            //writer.WriteAttributeString("IsStatic", entity.IsStatic.ToString());
            //writer.WriteAttributeString("Size", String.Format("{0};{1}", entity.Size.Width, entity.Size.Height));
            writer.WriteAttributeString("Rotation", String.Format("{0:0.00}", entity.Rotation));

            //writer.WriteAttributeString("Color", entity.Color.ToString());
            //writer.WriteAttributeString("BlurFactor", String.Format("{0:0.00}", entity.BlurFactor));
            //writer.WriteAttributeString("IsInBackground", entity.IsInBackground.ToString());

            //writer.WriteAttributeString("FrictionCoefficient", WriteFloat(entity.FrictionCoefficient));
            //writer.WriteAttributeString("RestitutionCoefficient", WriteFloat(entity.RestitutionCoefficient));
            writer.WriteAttributeString("Layer", entity.Layer.ToString());


            foreach (EntityComponent entityComponent in entity.ListEntityComponent)
            {
                if (entityComponent is EntitySprite)
                    SaveEntitySprite((EntitySprite)entityComponent, writer, repository);

                //TODO : gérer l'écriture du fichier XML pour les EntityText et Entity3DModel
            }

            /*
            #region Springs
            for (int j = 0; j < entity.ListFixedLinearSpring.Count; j++)
            {
                writer.WriteStartElement("FixedLinearSpring");

                FixedLinearSpring spring = entity.ListFixedLinearSpring[j];

                writer.WriteAttributeString("BodyAttachPoint", WriteVector2(spring.BodyAttachPoint));
                //writer.WriteAttributeString("Position", WriteVector2(spring.Position));
                writer.WriteAttributeString("RestLength", WriteFloat(spring.RestLength));
                writer.WriteAttributeString("WorldAttachPoint", WriteVector2(spring.WorldAttachPoint));
                writer.WriteEndElement();
            }

            for (int j = 0; j < entity.ListLinearSpring.Count; j++)
            {
                writer.WriteStartElement("LinearSpring");

                LinearSpring spring = entity.ListLinearSpring[j];

                Entity entity2 = repository.GetEntityFromBody(spring.Body2);

                writer.WriteAttributeString("Body2", entity2.Name);

                writer.WriteAttributeString("AttachPoint1", WriteVector2(spring.AttachPoint1));
                writer.WriteAttributeString("AttachPoint2", WriteVector2(spring.AttachPoint2));
                writer.WriteAttributeString("RestLength", WriteFloat(spring.RestLength));

                writer.WriteEndElement();
            }
            #endregion

            #region Joints
            for (int j = 0; j < entity.ListFixedRevoluteJoint.Count; j++)
            {
                writer.WriteStartElement("FixedRevoluteJoint");

                FixedRevoluteJoint joint = entity.ListFixedRevoluteJoint[j];

                writer.WriteAttributeString("Anchor", WriteVector2(joint.Anchor));
                writer.WriteAttributeString("MaxImpulse", WriteFloat(joint.MaxImpulse));
                writer.WriteEndElement();
            }
            #endregion
*/
            #region Scripts & Actions
            for (int j = 0; j < entity.ListScript.Count; j++)
            {
                writer.WriteStartElement("Script");

                Script script = entity.ListScript[j];

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
                        writer.WriteStartElement("Event");

                        ActionEvent actionEvent = (ActionEvent)action;

                        writer.WriteAttributeString("ActionName", actionEvent.ActionName);
                        writer.WriteAttributeString("PropertyName", actionEvent.PropertyName);

                        for (int i = 0; i < actionEvent.ActionEventTypes.Length; i++)
                        {
                            writer.WriteStartElement("EventDetail");
                            writer.WriteAttributeString("EventIndex", i.ToString());
                            writer.WriteAttributeString("IsRelative", actionEvent.IsRelative[i].ToString());
                            writer.WriteAttributeString("Duration", actionEvent.Durations[i].ToString());
                            writer.WriteAttributeString("Speed", actionEvent.Speeds[i].ToString());

                            switch (actionEvent.ActionEventTypes[i])
                            {
                                case ActionEventType.Deactivated:
                                    writer.WriteStartElement("Deactivated");
                                    break;
                                case ActionEventType.FixedValue:
                                    writer.WriteStartElement("FixedValue");
                                    writer.WriteAttributeString("Value", WriteFloat(actionEvent.FloatValues[i]));
                                    break;
                                case ActionEventType.MouseX:
                                    writer.WriteStartElement("MouseX");
                                    break;
                                case ActionEventType.MouseY:
                                    writer.WriteStartElement("MouseY");
                                    break;
                                case ActionEventType.EntityBinding:
                                    writer.WriteStartElement("EntityBinding");
                                    writer.WriteAttributeString("EntityName", actionEvent.EntityBindings[i].Name);
                                    writer.WriteAttributeString("PropertyName", actionEvent.EntityBindingProperties[i].Name);
                                    writer.WriteAttributeString("PropertyIndex", actionEvent.EntityBindingPropertyId[i].ToString());
                                    break;
                                case ActionEventType.Random:
                                    writer.WriteStartElement("Random");
                                    writer.WriteAttributeString("RandomMinValue", WriteFloat(actionEvent.RndMinValues[i]));
                                    writer.WriteAttributeString("RandomMaxValue", WriteFloat(actionEvent.RndMaxValues[i]));
                                    break;
                                default:
                                    break;
                            }

                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                    }
                }
                writer.WriteEndElement();
            }
            #endregion

            #region Triggers
            for (int j = 0; j < entity.ListTrigger.Count; j++)
            {
                TriggerBase trigger = entity.ListTrigger[j];

                SaveTrigger(writer, trigger);
            }
            #endregion

            #region Particle system
            foreach (ParticleSystem psystem in entity.ListParticleSystem)
            {
                writer.WriteStartElement("ParticleSystem");

                writer.WriteAttributeString("Name", psystem.Name);
                //writer.WriteAttributeString("ParticleSystemName", psystem.ParticleSystemName);
                writer.WriteAttributeString("EmmittingAngle", WriteFloat(psystem.EmmittingAngle));
                writer.WriteAttributeString("FieldAngle", WriteFloat(psystem.FieldAngle));
                writer.WriteAttributeString("EmmittingFromAllSurface", psystem.EmmittingFromAllSurface.ToString());
                writer.WriteAttributeString("Rate", psystem.Rate.ToString());
                writer.WriteAttributeString("Velocity", WriteFloat(psystem.Velocity));

                foreach (IParticle particleTemplate in psystem.ListParticleTemplate)
                {
                    //SaveEntity(particleTemplate, true, writer, repository);
                }

                writer.WriteEndElement();
            }
            #endregion

            writer.WriteEndElement();
        }

        private static void SaveEntitySprite(EntitySprite entitySprite, XmlTextWriter writer, Repository repository)
        {
            writer.WriteAttributeString("Position", WriteVector2(entitySprite.Position));
            writer.WriteAttributeString("Name", entitySprite.Name);

            writer.WriteAttributeString("Size", String.Format("{0};{1}", entitySprite.Size.X, entitySprite.Size.Y));
            writer.WriteAttributeString("Rotation", String.Format("{0:0.00}", entitySprite.Rotation));

            writer.WriteAttributeString("Color", entitySprite.Color.ToString());
            writer.WriteAttributeString("BlurFactor", String.Format("{0:0.00}", entitySprite.BlurFactor));
            writer.WriteAttributeString("IsInBackground", entitySprite.IsInBackground.ToString());

            SaveEntityPhysic((EntityPhysicObject)entitySprite, writer, repository);

            #region Springs
            for (int j = 0; j < entitySprite.ListFixedLinearSpring.Count; j++)
            {
                writer.WriteStartElement("FixedLinearSpring");

                FixedLinearSpring spring = entitySprite.ListFixedLinearSpring[j];

                writer.WriteAttributeString("BodyAttachPoint", WriteVector2(spring.BodyAttachPoint));
                //writer.WriteAttributeString("Position", WriteVector2(spring.Position));
                writer.WriteAttributeString("RestLength", WriteFloat(spring.RestLength));
                writer.WriteAttributeString("WorldAttachPoint", WriteVector2(spring.WorldAttachPoint));
                writer.WriteEndElement();
            }

            for (int j = 0; j < entitySprite.ListLinearSpring.Count; j++)
            {
                writer.WriteStartElement("LinearSpring");

                LinearSpring spring = entitySprite.ListLinearSpring[j];

                Entity entity2 = repository.GetEntityFromBody(spring.Body2);

                writer.WriteAttributeString("Body2", entity2.Name);

                writer.WriteAttributeString("AttachPoint1", WriteVector2(spring.AttachPoint1));
                writer.WriteAttributeString("AttachPoint2", WriteVector2(spring.AttachPoint2));
                writer.WriteAttributeString("RestLength", WriteFloat(spring.RestLength));

                writer.WriteEndElement();
            }
            #endregion

            #region Joints
            for (int j = 0; j < entitySprite.ListFixedRevoluteJoint.Count; j++)
            {
                writer.WriteStartElement("FixedRevoluteJoint");

                FixedRevoluteJoint joint = entitySprite.ListFixedRevoluteJoint[j];

                writer.WriteAttributeString("Anchor", WriteVector2(joint.Anchor));
                writer.WriteAttributeString("MaxImpulse", WriteFloat(joint.MaxImpulse));
                writer.WriteEndElement();
            }
            #endregion

            #region Scripts & Actions
            for (int j = 0; j < entitySprite.ListScript.Count; j++)
            {
                writer.WriteStartElement("Script");

                Script script = entitySprite.ListScript[j];

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
                        writer.WriteStartElement("Event");

                        ActionEvent actionEvent = (ActionEvent)action;

                        writer.WriteAttributeString("ActionName", actionEvent.ActionName);
                        writer.WriteAttributeString("PropertyName", actionEvent.PropertyName);

                        for (int i = 0; i < actionEvent.ActionEventTypes.Length; i++)
                        {
                            writer.WriteStartElement("EventDetail");
                            writer.WriteAttributeString("EventIndex", i.ToString());
                            writer.WriteAttributeString("IsRelative", actionEvent.IsRelative[i].ToString());
                            writer.WriteAttributeString("Duration", actionEvent.Durations[i].ToString());
                            writer.WriteAttributeString("Speed", actionEvent.Speeds[i].ToString());

                            switch (actionEvent.ActionEventTypes[i])
                            {
                                case ActionEventType.Deactivated:
                                    writer.WriteStartElement("Deactivated");
                                    break;
                                case ActionEventType.FixedValue:
                                    writer.WriteStartElement("FixedValue");
                                    writer.WriteAttributeString("Value", WriteFloat(actionEvent.FloatValues[i]));
                                    break;
                                case ActionEventType.MouseX:
                                    writer.WriteStartElement("MouseX");
                                    break;
                                case ActionEventType.MouseY:
                                    writer.WriteStartElement("MouseY");
                                    break;
                                case ActionEventType.EntityBinding:
                                    writer.WriteStartElement("EntityBinding");
                                    writer.WriteAttributeString("EntityName", actionEvent.EntityBindings[i].Name);
                                    writer.WriteAttributeString("PropertyName", actionEvent.EntityBindingProperties[i].Name);
                                    writer.WriteAttributeString("PropertyIndex", actionEvent.EntityBindingPropertyId[i].ToString());
                                    break;
                                case ActionEventType.Random:
                                    writer.WriteStartElement("Random");
                                    writer.WriteAttributeString("RandomMinValue", WriteFloat(actionEvent.RndMinValues[i]));
                                    writer.WriteAttributeString("RandomMaxValue", WriteFloat(actionEvent.RndMaxValues[i]));
                                    break;
                                default:
                                    break;
                            }

                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                    }
                }
                writer.WriteEndElement();
            }
            #endregion

            #region Triggers
            for (int j = 0; j < entitySprite.ListTrigger.Count; j++)
            {
                TriggerBase trigger = entitySprite.ListTrigger[j];

                SaveTrigger(writer, trigger);
            }
            #endregion
        }

        private static void SaveEntityPhysic(EntityPhysicObject entityPhysic, XmlTextWriter writer, Repository repository)
        {
            writer.WriteAttributeString("IsStatic", entityPhysic.IsStatic.ToString());

            writer.WriteAttributeString("FrictionCoefficient", WriteFloat(entityPhysic.FrictionCoefficient));
            writer.WriteAttributeString("RestitutionCoefficient", WriteFloat(entityPhysic.RestitutionCoefficient));

            #region Springs
            for (int j = 0; j < entityPhysic.ListFixedLinearSpring.Count; j++)
            {
                writer.WriteStartElement("FixedLinearSpring");

                FixedLinearSpring spring = entityPhysic.ListFixedLinearSpring[j];

                writer.WriteAttributeString("BodyAttachPoint", WriteVector2(spring.BodyAttachPoint));
                //writer.WriteAttributeString("Position", WriteVector2(spring.Position));
                writer.WriteAttributeString("RestLength", WriteFloat(spring.RestLength));
                writer.WriteAttributeString("WorldAttachPoint", WriteVector2(spring.WorldAttachPoint));
                writer.WriteEndElement();
            }

            for (int j = 0; j < entityPhysic.ListLinearSpring.Count; j++)
            {
                writer.WriteStartElement("LinearSpring");

                LinearSpring spring = entityPhysic.ListLinearSpring[j];

                Entity entity2 = repository.GetEntityFromBody(spring.Body2);

                writer.WriteAttributeString("Body2", entity2.Name);

                writer.WriteAttributeString("AttachPoint1", WriteVector2(spring.AttachPoint1));
                writer.WriteAttributeString("AttachPoint2", WriteVector2(spring.AttachPoint2));
                writer.WriteAttributeString("RestLength", WriteFloat(spring.RestLength));

                writer.WriteEndElement();
            }
            #endregion

            #region Joints
            for (int j = 0; j < entityPhysic.ListFixedRevoluteJoint.Count; j++)
            {
                writer.WriteStartElement("FixedRevoluteJoint");

                FixedRevoluteJoint joint = entityPhysic.ListFixedRevoluteJoint[j];

                writer.WriteAttributeString("Anchor", WriteVector2(joint.Anchor));
                writer.WriteAttributeString("MaxImpulse", WriteFloat(joint.MaxImpulse));
                writer.WriteEndElement();
            }
            #endregion
        }

        private static void SaveTrigger(XmlWriter writer, TriggerBase trigger)
        {
            if (trigger is TriggerCollision)
            {
                TriggerCollision triggerCollision = (TriggerCollision)trigger;

                writer.WriteStartElement("TriggerCollision");

                writer.WriteAttributeString("TriggerName", triggerCollision.TriggerName);

                writer.WriteAttributeString("TargetEntityName", triggerCollision.TargetEntity.Name);
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

                //TODO : gérer correctement selon l'origine de l'entité (Entity ou ParticleSystem)
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
            for (int i = 0; i < repository.listEntity.Count; i++)
            {
                Entity entity = repository.listEntity[i];

                SaveEntity(entity, false, writer, repository);
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
