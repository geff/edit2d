using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using System.ComponentModel;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics.Joints;
using Microsoft.Xna.Framework;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;
using Edit2DEngine.Triggers;

namespace Edit2DEngine.Entities
{
    public abstract class EntityPhysicObject : EntityComponent, ITriggerCollisionHandler, ITriggerMouseHandler, ITriggerValueChangedHandler
    {
        protected Geom _geom;
        protected Body body;
        protected Vertices originalVerts;
        protected String _name;
        protected Vector2 _size;
        protected Vector2 _center = Vector2.Zero;

        [Category("Spring")]
        public List<LinearSpring> ListLinearSpring { get; set; }
        [Category("Spring")]
        public List<FixedLinearSpring> ListFixedLinearSpring { get; set; }
        [Category("Spring")]
        public List<FixedAngleSpring> ListFixedAngleSpring { get; set; }
        [Category("Spring")]
        public List<AngleSpring> ListAngleSpring { get; set; }

        [Category("Joint")]
        public List<PinJoint> ListPinJoint { get; set; }
        [Category("Joint")]
        public List<RevoluteJoint> ListRevoluteJointJoint { get; set; }
        [Category("Joint")]
        public List<FixedRevoluteJoint> ListFixedRevoluteJoint { get; set; }

        [Browsable(false)]
        public int UniqueId { get; set; }


        public Boolean IsStatic
        {
            get
            {
                return body.IsStatic;
            }
            set
            {
                body.IsStatic = value;
            }
        }

        public Geom Geom
        {
            get
            {
                return _geom;
            }
            set
            {
                _geom = value;
            }
        }

        [Browsable(true)]
        public Body Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value;
            }
        }



        [Browsable(true)]
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [Browsable(false)]
        public Microsoft.Xna.Framework.Vector2 Center
        {
            //get { return new Microsoft.Xna.Framework.Vector2(this.Size.Width / 2, this.Size.Height / 2); }
            //get { return new Microsoft.Xna.Framework.Vector2(this.Size.Width / 6, this.Size.Height / 2); }

            //get { return new Microsoft.Xna.Framework.Vector2(0,0); }
            //get { return new Microsoft.Xna.Framework.Vector2(50, 50); }
            //get { return new Microsoft.Xna.Framework.Vector2(NativeImageSize.Width / 2, NativeImageSize.Height / 2); }

            get
            {
                return _center;
            }
            set
            {
                _center = value;
                //CreateVerticesForRendering();
            }
        }

        [Browsable(false)]
        public Microsoft.Xna.Framework.Rectangle Rectangle
        {
            get
            {
                Microsoft.Xna.Framework.Rectangle rec = new Microsoft.Xna.Framework.Rectangle((int)(this.Position.X), (int)(this.Position.Y), (int)this.Size.X, (int)this.Size.Y);
                return rec;
            }
        }

        [Browsable(true), AttributeAction]
        public override Microsoft.Xna.Framework.Vector2 Position
        {
            get
            {
                if (body == null)
                    return Vector2.Zero;

                return new Microsoft.Xna.Framework.Vector2(body.Position.X, body.Position.Y);
            }
            set
            {
                SetPosition(value);
            }
        }

        [Browsable(false)]
        public Microsoft.Xna.Framework.Vector2 AbsolutePosition
        {
            get
            {
                return new Microsoft.Xna.Framework.Vector2(body.Position.X, body.Position.Y) + this.EntityParent.Position;
            }
        }

        [Browsable(true), AttributeAction]
        public override float Rotation
        {
            get
            {
                if (this.body == null)
                    return 0f;

                return body.Rotation;
            }
            set
            {
                if (body != null)
                    body.Rotation = value;
            }
        }

        [Browsable(true), AttributeAction, Category("Graphics")]
        public Microsoft.Xna.Framework.Graphics.Color Color { get; set; }

        [Browsable(true), DefaultValue(false), AttributeAction, Category("Graphics")]
        public Boolean IsInBackground { get; set; }

        [Browsable(true), DefaultValue(0.0f), AttributeAction, Category("Graphics")]
        public float BlurFactor { get; set; }

        [Browsable(true), AttributeAction, Category("Physic")]
        public Boolean IsColisionable
        {
            get
            {
                if (this.Geom == null)
                    return true;

                return this.Geom.CollisionEnabled;
            }
            set
            {
                if (Geom != null)
                    this.Geom.CollisionEnabled = value;
            }
        }

        [Browsable(true), AttributeAction, Category("Physic")]
        public float FrictionCoefficient
        {
            get
            {
                if (this.Geom == null)
                    return 0.1f;

                return Geom.FrictionCoefficient;
            }
            set
            {
                if (Geom != null)
                    Geom.FrictionCoefficient = value;
            }
        }

        [Browsable(true), AttributeAction, Category("Physic")]
        public float RestitutionCoefficient
        {
            get
            {
                if (this.Geom == null)
                    return 0.1f;

                return Geom.RestitutionCoefficient;
            }
            set
            {
                if (Geom != null)
                    Geom.RestitutionCoefficient = value;
            }
        }

        [Browsable(true), AttributeAction, Category("Physic")]
        public float Mass
        {
            get
            {
                if (this.Body == null)
                    return 5f;

                return Body.Mass;
            }
            set
            {
                if (Body != null)
                    Body.Mass = value;
            }
        }

        [Browsable(false)]
        public VertexPositionTexture[] TexVertices
        {
            get;
            set;
        }

        [Browsable(false)]
        public Int16[] TexIndices
        {
            get;
            set;
        }

        [Browsable(false)]
        public Int16 NumberTriangles
        {
            get;
            set;
        }

        protected abstract void Init(bool addToPhysicSimulator, bool isCollisionable);

        public abstract void ChangeSize(int width, int height, bool addToPhysicSimulator);

        public void SetPosition(Microsoft.Xna.Framework.Vector2 position)
        {
            if (body != null)
            {
                body.ResetDynamics();
                body.Position = new Vector2(position.X, position.Y);
                //CreateVerticesForRendering();
            }
        }

        public void FixPosition(Vector2 position)
        {
            body.Position = position;
        }

        public void SetNewCenter(Vector2 deltaPosition, bool addToPhysicSimulator)
        {
            float rotation = this.Rotation;

            deltaPosition = -deltaPosition;

            Geom.LocalVertices.Translate(ref deltaPosition);

            ////---
            //TexVertices[0].Position += new Vector3(deltaPosition+new Vector2(50f), 0f);
            //TexVertices[1].Position += new Vector3(deltaPosition + new Vector2(50f), 0f);
            //TexVertices[2].Position += new Vector3(deltaPosition + new Vector2(50f), 0f);
            //TexVertices[3].Position += new Vector3(deltaPosition + new Vector2(50f), 0f);
            ////---

            DistanceGrid.Instance.RemoveDistanceGrid(this.Geom);
            DistanceGrid.Instance.CreateDistanceGrid(this.Geom);

            deltaPosition = -deltaPosition;

            Vector2 worldPosition = this.body.GetWorldPosition(deltaPosition);

            this.Center += deltaPosition;
            this.Position = worldPosition;
            this.Rotation = rotation;

            //CreateVerticesForRendering(
        }

        public void SetCenterFromWorldPosition(Vector2 worldPosition, bool addToPhysicSimulator)
        {
            this.SetNewCenter(this.body.GetLocalPosition(worldPosition), addToPhysicSimulator);
        }

        //public override object Clone()
        //{
        //    return Clone(false);
        //}

        /*
        public object Clone(bool addToPhysicSimulator)
        {
            EntitySprite clone = new EntitySprite(addToPhysicSimulator, this.TextureName, this.Name);

            //Entity clone = new Entity(addToPhysicSimulator, this.TextureName, this.Name);

            //clone.ChangeSize(this.Size.Width, this.Size.Height, addToPhysicSimulator);

            //if (clone.body != null && this.body != null)
            //{
            //    clone.body.Rotation = this.body.Rotation;
            //    clone.body.Position = this.body.Position;
            //    clone.body.IsStatic = this.body.IsStatic;
            //    clone.IsStatic = this.body.IsStatic;
            //}

            //clone.Name = this.Name;
            //clone.TextureName = this.TextureName;
            //clone.NativeImageSize = this.NativeImageSize;

            //clone.BlurFactor = this.BlurFactor;
            //clone.IsInBackground = this.IsInBackground;
            //clone.Color = this.Color;
            //clone.FrictionCoefficient = this.FrictionCoefficient;
            //clone.RestitutionCoefficient = this.RestitutionCoefficient;

            ////--- Centre de l'entité
            //Vector2 deltaPosition = this.Center - clone.Center;
            //if (deltaPosition != Vector2.Zero)
            //    clone.SetNewCenter(deltaPosition, false);
            ////---

            ////--- Scripts & Curves
            //for (int j = 0; j < this.ListScript.Count; j++)
            //{
            //    Script script = this.ListScript[j];

            //    Script scriptClone = new Script(script.ScriptName, clone);
            //    //scriptClone.Duration = script.Duration;
            //    clone.ListScript.Add(scriptClone);

            //    for (int k = 0; k < script.ListAction.Count; k++)
            //    {
            //        ActionBase action = script.ListAction[k];

            //        if (action is ActionCurve)
            //        {
            //            ActionCurve curve = (ActionCurve)action;
            //            ActionCurve curveClone = new ActionCurve(scriptClone, curve.ActionName, curve.IsRelative, curve.IsLoop, curve.PropertyName);

            //            scriptClone.ListAction.Add(curveClone);

            //            for (int l = 0; l < curve.ListCurve.Count; l++)
            //            {
            //                Curve newCurve = new Curve();
            //                curveClone.ListCurve.Add(newCurve);

            //                for (int m = 0; m < curve.ListCurve[l].Keys.Count; m++)
            //                {
            //                    newCurve.Keys.Add(curve.ListCurve[l].Keys[0].Clone());
            //                }
            //            }
            //        }
            //        else if (action is ActionEvent)
            //        {
            //            //ActionEvent actionEvent = (ActionEvent)action;
            //            //ActionEvent actionEventClone = new ActionEvent(actionEvent.Script, actionEvent.ActionName, actionEvent.IsRelative, actionEvent.PropertyName);
            //            //actionEventClone.Value = actionEvent.Value;
            //            //actionEventClone.ChangeValue = actionEvent.ChangeValue;

            //            //scriptClone.ListAction.Add(actionEventClone);
            //        }
            //    }
            //}
            ////---

            ////--- Trigger
            //for (int j = 0; j < ListTrigger.Count; j++)
            //{
            //    TriggerBase trigger = ListTrigger[j];

            //    if (trigger is TriggerCollision)
            //    {
            //        TriggerCollision triggerCol = (TriggerCollision)trigger;
            //        TriggerCollision cloneTrigger = new TriggerCollision(triggerCol.TriggerName, clone, triggerCol.TargetEntity);

            //        cloneTrigger.ListScript = triggerCol.ListScript;

            //        clone.ListTrigger.Add(cloneTrigger);
            //    }
            //    else if (trigger is TriggerValueChanged)
            //    {
            //        TriggerValueChanged triggerVal = (TriggerValueChanged)trigger;
            //        TriggerValueChanged cloneTrigger = new TriggerValueChanged(triggerVal.TriggerName, clone, triggerVal.PropertyName, triggerVal.Sens, triggerVal.Value, triggerVal.IsCustomProperty);

            //        cloneTrigger.ListScript = triggerVal.ListScript;

            //        clone.ListTrigger.Add(cloneTrigger);
            //    }
            //    else if (trigger is TriggerLoad)
            //    {
            //        TriggerLoad triggerLoad = (TriggerLoad)trigger;
            //        TriggerLoad cloneTrigger = new TriggerLoad(triggerLoad.TriggerName, clone);

            //        cloneTrigger.ListScript = triggerLoad.ListScript;

            //        clone.ListTrigger.Add(cloneTrigger);
            //    }
            //    else if (trigger is TriggerMouse)
            //    {
            //        TriggerMouse triggerMouse = (TriggerMouse)trigger;
            //        TriggerMouse cloneTrigger = new TriggerMouse(triggerMouse.TriggerName, clone, triggerMouse.TriggerMouseType);

            //        cloneTrigger.ListScript = triggerMouse.ListScript;

            //        clone.ListTrigger.Add(cloneTrigger);
            //    }
            //}
            ////---

            ////--- ParticleSystem
            //for (int j = 0; j < ListParticleSystem.Count; j++)
            //{
            //    ParticleSystem pSystem = ListParticleSystem[j];
            //    ParticleSystem clonePSystem = new ParticleSystem(clone);

            //    clonePSystem.EmmittingAngle = pSystem.EmmittingAngle;
            //    clonePSystem.EmmittingFromAllSurface = pSystem.EmmittingFromAllSurface;
            //    clonePSystem.FieldAngle = pSystem.FieldAngle;
            //    clonePSystem.Name = pSystem.Name;
            //    //clonePSystem.ParticleSystemName = pSystem.ParticleSystemName;
            //    clonePSystem.Rate = pSystem.Rate;
            //    clonePSystem.Velocity = pSystem.Velocity;

            //    clone.ListParticleSystem.Add(clonePSystem);
            //    for (int k = 0; k < pSystem.ListParticleTemplate.Count; k++)
            //    {
            //        Particle particle = pSystem.ListParticleTemplate[k];
            //        Particle cloneParticle = particle.Clone(true);

            //        clonePSystem.ListParticleTemplate.Add(cloneParticle);
            //    }
            //}
            ////---

            return clone;
        }
        */
        public override bool ContainsLocation(Vector2 location)
        {
            return Geom.Collide(location);
        }

        public override void ChangeLayer()
        {
            if (Geom != null)
            {
                //---> 17 car il s'agit du milieu de l'ensemble des valeurs possible pour l'enum CollisionCategory
                Geom.CollisionCategories = (CollisionCategory)(2 ^ (17 + this.EntityParent.Layer));
                Geom.CollidesWith = Geom.CollisionCategories;
            }
        }

        public override void ApplyForce(Vector2 force)
        {
            throw new NotImplementedException();
        }

        [Browsable(true), AttributeAction]
        public override Vector2 Size 
        { 
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                ChangeSize((int)value.X, (int)value.Y, true);
            }
        }
    }
}
