using System;
using System.Collections.Generic;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics.Dynamics.Joints;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;
using Edit2DEngine.Trigger;
using Edit2DEngine.Action;
using Edit2DEngine.Particles;
using System.Drawing;

namespace Edit2DEngine
{
    public class Entite : ICloneable, IActionHandler, ITriggerHandler
    {
        private String _name;
        private Size _size;
        Vector2 _center = Vector2.Zero;
        public Geom geom;
        protected Body body;
        private Vertices originalVerts;

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
        public List<Script> ListScript { get; set; }
        [Browsable(false)]
        public List<TriggerBase> ListTrigger { get; set; }
        [Browsable(false)]
        public Dictionary<String, Object> ListCustomProperties { get; set; }
        [Browsable(false)]
        public List<ParticleSystem> ListParticleSystem { get; set; }

        [Browsable(false)]
        public int UniqueId { get; set; }

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

        [Browsable(false)]
        public Boolean Selected { get; set; }

        [Browsable(true)]
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [Browsable(true), Category("Spring")]
        public String TextureName { get; set; }

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
        public Size NativeImageSize { get; set; }

        [Browsable(true), AttributeAction]
        public Size Size
        {
            get { return _size; }
            set
            {
                if (value != _size)
                    ChangeSize(value.Width, value.Height, true);
            }
        }

        [Browsable(false)]
        public Microsoft.Xna.Framework.Vector2 SizeVector
        {
            get { return new Microsoft.Xna.Framework.Vector2(this.Size.Width, this.Size.Height); }
        }

        [Browsable(false)]
        public Microsoft.Xna.Framework.Rectangle Rectangle
        {
            get
            {
                Microsoft.Xna.Framework.Rectangle rec = new Microsoft.Xna.Framework.Rectangle((int)(this.Position.X), (int)(this.Position.Y), this.Size.Width, this.Size.Height);
                return rec;
            }
        }

        private int _layer;
        [Browsable(true)]
        public int Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;

                if (geom != null)
                {
                    //---> 17 car il s'agit du milieu de l'ensemble des valeurs possible pour l'enum CollisionCategory
                    geom.CollisionCategories = (CollisionCategory)(2 ^ (17 + _layer));
                    geom.CollidesWith = geom.CollisionCategories;
                }
            }
        }

        [Browsable(true), AttributeAction]
        public Microsoft.Xna.Framework.Vector2 Position
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

        [Browsable(true), AttributeAction]
        public float Rotation
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
        public Boolean IsStatic
        {
            get
            {
                if (this.body == null)
                    return false;

                return this.body.IsStatic;
            }
            set
            {
                if (body != null)
                    this.body.IsStatic = value;
            }
        }

        [Browsable(true), AttributeAction, Category("Physic")]
        public Boolean IsColisionable
        {
            get
            {
                if (this.geom == null)
                    return true;

                return this.geom.CollisionEnabled;
            }
            set
            {
                if (geom != null)
                    this.geom.CollisionEnabled = value;
            }
        }

        [Browsable(true), AttributeAction, Category("Physic")]
        public float FrictionCoefficient
        {
            get
            {
                if (this.geom == null)
                    return 0.1f;

                return geom.FrictionCoefficient;
            }
            set
            {
                if (geom != null)
                    geom.FrictionCoefficient = value;
            }
        }

        [Browsable(true), AttributeAction, Category("Physic")]
        public float RestitutionCoefficient
        {
            get
            {
                if (this.geom == null)
                    return 0.1f;

                return geom.RestitutionCoefficient;
            }
            set
            {
                if (geom != null)
                    geom.RestitutionCoefficient = value;
            }
        }

        [Browsable(true), AttributeAction, Category("Physic")]
        public float Mass
        {
            get
            {
                if (this.body == null)
                    return 5f;

                return body.Mass;
            }
            set
            {
                if (body != null)
                    body.Mass = value;
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

        public String TreeViewPath
        {
            get
            {
                return "Monde\\Entités\\" + this.Name;
            }
        }

        public Entite(bool linkToPhysiSimulator, bool isCollisionable, string textureName, string name)
        {
            Constructor(linkToPhysiSimulator, isCollisionable, textureName, name);
        }

        public Entite(bool linkToPhysiSimulator, string textureName, string name)
        {
            Constructor(linkToPhysiSimulator, true, textureName, name);
        }

        protected void Constructor(bool addToPhysicSimulator, bool isCollisionable, string textureName, string name)
        {
            ListFixedLinearSpring = new List<FixedLinearSpring>();
            ListLinearSpring = new List<LinearSpring>();
            ListFixedAngleSpring = new List<FixedAngleSpring>();
            ListAngleSpring = new List<AngleSpring>();
            ListPinJoint = new List<PinJoint>();
            ListRevoluteJointJoint = new List<RevoluteJoint>();
            ListFixedRevoluteJoint = new List<FixedRevoluteJoint>();
            ListScript = new List<Script>();
            ListTrigger = new List<TriggerBase>();
            ListCustomProperties = new Dictionary<string, object>();
            ListParticleSystem = new List<ParticleSystem>();

            this.Color = Microsoft.Xna.Framework.Graphics.Color.White;
            this.Name = name;
            this.TextureName = textureName;

            if (!(this is Particle))
                UniqueId = ++Repository.EntityCount;

            Init(addToPhysicSimulator, isCollisionable);

            this.Layer = 0;
        }

        public void Init(bool addToPhysicSimulator, bool isCollisionable)
        {
            ChangeTexture(this.TextureName, addToPhysicSimulator, isCollisionable);

            if (geom != null)
            {
                geom.CollisionEnabled = isCollisionable;
                geom.CollidesWith = CollisionCategory.All;
                geom.CollisionResponseEnabled = true;
                geom.FrictionCoefficient = 0.5f;
                geom.RestitutionCoefficient = 0.5f;
            }
        }

        public virtual void ChangeTexture(string textureName, bool addToPhysicSimulator, bool isCollisionable)
        {
            this.TextureName = textureName;

            Texture2D texture = GetTexture();

            this.NativeImageSize = new Size(texture.Width, texture.Height);
            this._size = new Size(texture.Width, texture.Height);

            CalcVerticesFromTexture(texture);
            CreateBodyFromVertices(addToPhysicSimulator, isCollisionable, ref body, ref geom, texture.Width, texture.Height);
        }

        public void ChangeSize(int width, int height, bool addToPhysicSimulator)
        {
            this._size = new Size(width, height);

            Texture2D texture = GetTexture();
            CreateBodyFromVertices(addToPhysicSimulator, true, ref body, ref geom, width, height);
        }

        private void CalcVerticesFromTexture(Texture2D polygonTexture)
        {
            //Create an array to hold the data from the texture
            uint[] data = new uint[polygonTexture.Width * polygonTexture.Height];

            //Transfer the texture data to the array
            polygonTexture.GetData(data);

            //--- Calcul des vertices originaux
            originalVerts = Vertices.CreatePolygon(data, polygonTexture.Width, polygonTexture.Height);//, 1f, 127, true, true);// 2f, out vec);
            //---
        }

        private void CreateBodyFromVertices(Boolean addToPhysicSimulator, bool isCollisionable, ref Body polygonBody, ref Geom polygonGeom, int width, int height)
        {
            float widthFactor = (float)width / (float)NativeImageSize.Width;
            float heightFactor = (float)height / (float)NativeImageSize.Height;

            //--- Copie des vertices originaux puis transformation
            Vertices verts = new Vertices(originalVerts);
            Vector2 vecScale = new Vector2(widthFactor, heightFactor);
            verts.Scale(ref vecScale);
            //---

            //--- Calcul du centre du polygone
            this._center = originalVerts.GetCentroid();
            //---

            //--- Calcul des Vertices pour l'affichage
            CreateVerticesForRendering(originalVerts);
            //---

            //Use the body factory to create the physics body
            if (addToPhysicSimulator)
            {
                //--- Suppression du body et du geom
                if (polygonBody != null)
                    Repository.physicSimulator.Remove(polygonBody);
                if (polygonGeom != null)
                    Repository.physicSimulator.Remove(polygonGeom);
                //---

                Vector2 prevBodyPosition = Vector2.Zero;
                float prevBodyRotation = 0f;
                bool prevStatic = false;
                bool prevCollisionable = false;
                float prevMass = 5f;

                if (polygonBody != null)
                {
                    prevBodyPosition = polygonBody.Position;
                    prevBodyRotation = polygonBody.Rotation;
                    prevStatic = polygonBody.IsStatic;
                    prevCollisionable = polygonGeom.CollisionEnabled;
                    prevMass = polygonBody.Mass;
                }


                polygonBody = BodyFactory.Instance.CreatePolygonBody(Repository.physicSimulator, verts, 5);

                polygonGeom = GeomFactory.Instance.CreatePolygonGeom(Repository.physicSimulator, polygonBody, verts, 0f);

                polygonGeom.SetBody(polygonBody);

                if (polygonBody != null)
                {
                    polygonBody.Position = prevBodyPosition;
                    polygonBody.Rotation = prevBodyRotation;
                    polygonBody.IsStatic = prevStatic;

                    if (isCollisionable)
                    {
                        polygonGeom.CollisionEnabled = prevCollisionable;
                    }
                    else
                    {
                        polygonGeom.CollisionEnabled = false;
                    }
                    polygonBody.Mass = prevMass;
                }
            }
            else
            {
                polygonBody = BodyFactory.Instance.CreatePolygonBody(verts, 5);
                polygonGeom = GeomFactory.Instance.CreatePolygonGeom(polygonBody, verts, 0f);

                //--- Si l'entité n'est pas ajouté au moteur physique, ne pas créer le Body et le Geom
                //polygonBody = null;
                //polygonGeom = null;
                //---
            }
        }

        //private void CreateVerticesForRendering()
        //{
        //    CreateVerticesForRendering(this.Center);
        //}

        private void CreateVerticesForRendering(Vertices vertices)
        {
            if (TexVertices == null)
            {
                TexVertices = new VertexPositionTexture[4];
                this.NumberTriangles = 2;
                this.TexIndices = new short[6];

                this.TexIndices[0] = 0;
                this.TexIndices[1] = 1;
                this.TexIndices[2] = 2;

                this.TexIndices[3] = 1;
                this.TexIndices[4] = 2;
                this.TexIndices[5] = 3;
            }

            //float midWidth = this.SizeVector.X / 2f;
            //float midHeight = this.SizeVector.Y / 2f;

            AABB boundingBox = new AABB(vertices);

            float midWidth = boundingBox.Width / 2f;
            float midHeight = boundingBox.Height / 2f;

            TexVertices[0].Position.X = this.Center.X - midWidth;
            TexVertices[0].Position.Y = this.Center.Y - midHeight;
            TexVertices[0].TextureCoordinate = new Vector2(0, 0);

            TexVertices[1].Position.X = this.Center.X + midWidth;
            TexVertices[1].Position.Y = this.Center.Y - midHeight;
            TexVertices[1].TextureCoordinate = new Vector2(1, 0);

            TexVertices[2].Position.X = this.Center.X - midWidth;
            TexVertices[2].Position.Y = this.Center.Y + midHeight;
            TexVertices[2].TextureCoordinate = new Vector2(0, 1);

            TexVertices[3].Position.X = this.Center.X + midWidth;
            TexVertices[3].Position.Y = this.Center.Y + midHeight;
            TexVertices[3].TextureCoordinate = new Vector2(1, 1);

            //TexVertices[0].Position += new Vector3(boundingBox.Min, 0f);
            //TexVertices[1].Position += new Vector3(boundingBox.Min, 0f);
            //TexVertices[2].Position += new Vector3(boundingBox.Min, 0f);
            //TexVertices[3].Position += new Vector3(boundingBox.Min, 0f);
        }

        protected virtual Texture2D GetTexture()
        {
            return TextureManager.LoadTexture2D(TextureName);
        }

        public void SetPosition(Microsoft.Xna.Framework.Vector2 position)
        {
            if (body != null)
            {
                body.ResetDynamics();
                body.Position = new Vector2(position.X, position.Y);
                //CreateVerticesForRendering();
            }
        }

        #region Springs, Joints
        public void AddLinearSpring(Entite entite, Microsoft.Xna.Framework.Vector2 vec1, Microsoft.Xna.Framework.Vector2 vec2)
        {
            LinearSpring linearSpring = SpringFactory.Instance.CreateLinearSpring(Repository.physicSimulator, body, vec1, entite.body, vec2, 10f, 10f);

            ListLinearSpring.Add(linearSpring);
        }

        public void AddFixedLinearSpring(Microsoft.Xna.Framework.Vector2 vec1, Microsoft.Xna.Framework.Vector2 vec2)
        {
            FixedLinearSpring fixedLinearSpring = SpringFactory.Instance.CreateFixedLinearSpring(Repository.physicSimulator, body, new Vector2(vec1.X, vec1.Y), new Vector2(vec2.X, vec2.Y), 10f, 10f);

            ListFixedLinearSpring.Add(fixedLinearSpring);
        }


        public void AddAngleSpring(Entite entite)
        {
            AngleSpring angleSpring = SpringFactory.Instance.CreateAngleSpring(Repository.physicSimulator, body, entite.body, 1000f, 500f);

            ListAngleSpring.Add(angleSpring);
        }

        public void AddFixedAngleSpring()
        {
            FixedAngleSpring fixedAngleSpring = SpringFactory.Instance.CreateFixedAngleSpring(Repository.physicSimulator, body, 100000f, 50000f);

            ListFixedAngleSpring.Add(fixedAngleSpring);
        }

        public void AddPinJoint(Entite entite, Microsoft.Xna.Framework.Vector2 vec1, Microsoft.Xna.Framework.Vector2 vec2)
        {
            PinJoint pinJoint = JointFactory.Instance.CreatePinJoint(Repository.physicSimulator, this.body, vec1, entite.body, vec2);

            ListPinJoint.Add(pinJoint);
        }

        public void AddRevoluteJoint(Entite entite, Vector2 vec1)
        {
            RevoluteJoint revoluteJoint = JointFactory.Instance.CreateRevoluteJoint(Repository.physicSimulator, this.body, entite.body, vec1);

            ListRevoluteJointJoint.Add(revoluteJoint);
        }

        public void AddFixedRevoluteJoint(Vector2 vec1)
        {
            FixedRevoluteJoint fixedRevoluteJoint = JointFactory.Instance.CreateFixedRevoluteJoint(Repository.physicSimulator, this.body, vec1);

            ListFixedRevoluteJoint.Add(fixedRevoluteJoint);
        }
        #endregion

        public void FixPosition(Vector2 position)
        {
            body.Position = position;
        }

        public void SetNewCenter(Vector2 deltaPosition, bool addToPhysicSimulator)
        {
            float rotation = this.Rotation;

            deltaPosition = -deltaPosition;

            this.geom.LocalVertices.Translate(ref deltaPosition);

            ////---
            //TexVertices[0].Position += new Vector3(deltaPosition+new Vector2(50f), 0f);
            //TexVertices[1].Position += new Vector3(deltaPosition + new Vector2(50f), 0f);
            //TexVertices[2].Position += new Vector3(deltaPosition + new Vector2(50f), 0f);
            //TexVertices[3].Position += new Vector3(deltaPosition + new Vector2(50f), 0f);
            ////---



            DistanceGrid.Instance.RemoveDistanceGrid(this.geom);
            DistanceGrid.Instance.CreateDistanceGrid(this.geom);

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

        #region ICloneable Membres

        public object Clone()
        {
            return Clone(false);
        }

        public object Clone(bool addToPhysicSimulator)
        {
            Entite clone = new Entite(addToPhysicSimulator, this.TextureName, this.Name);

            clone.ChangeSize(this.Size.Width, this.Size.Height, addToPhysicSimulator);

            if (clone.body != null && this.body != null)
            {
                clone.body.Rotation = this.body.Rotation;
                clone.body.Position = this.body.Position;
                clone.body.IsStatic = this.body.IsStatic;
                clone.IsStatic = this.body.IsStatic;
            }

            clone.Name = this.Name;
            clone.TextureName = this.TextureName;
            clone.NativeImageSize = this.NativeImageSize;

            clone.BlurFactor = this.BlurFactor;
            clone.IsInBackground = this.IsInBackground;
            clone.Color = this.Color;
            clone.FrictionCoefficient = this.FrictionCoefficient;
            clone.RestitutionCoefficient = this.RestitutionCoefficient;

            //--- Centre de l'entité
            Vector2 deltaPosition = this.Center - clone.Center;
            if (deltaPosition != Vector2.Zero)
                clone.SetNewCenter(deltaPosition, false);
            //---

            //--- Scripts & Curves
            for (int j = 0; j < this.ListScript.Count; j++)
            {
                Script script = this.ListScript[j];

                Script scriptClone = new Script(script.ScriptName, clone);
                //scriptClone.Duration = script.Duration;
                clone.ListScript.Add(scriptClone);

                for (int k = 0; k < script.ListAction.Count; k++)
                {
                    ActionBase action = script.ListAction[k];

                    if (action is ActionCurve)
                    {
                        ActionCurve curve = (ActionCurve)action;
                        ActionCurve curveClone = new ActionCurve(scriptClone, curve.ActionName, curve.IsRelative, curve.IsLoop, curve.PropertyName);

                        scriptClone.ListAction.Add(curveClone);

                        for (int l = 0; l < curve.ListCurve.Count; l++)
                        {
                            Curve newCurve = new Curve();
                            curveClone.ListCurve.Add(newCurve);

                            for (int m = 0; m < curve.ListCurve[l].Keys.Count; m++)
                            {
                                newCurve.Keys.Add(curve.ListCurve[l].Keys[0].Clone());
                            }
                        }
                    }
                    else if (action is ActionEvent)
                    {
                        //ActionEvent actionEvent = (ActionEvent)action;
                        //ActionEvent actionEventClone = new ActionEvent(actionEvent.Script, actionEvent.ActionName, actionEvent.IsRelative, actionEvent.PropertyName);
                        //actionEventClone.Value = actionEvent.Value;
                        //actionEventClone.ChangeValue = actionEvent.ChangeValue;

                        //scriptClone.ListAction.Add(actionEventClone);
                    }
                }
            }
            //---

            //--- Trigger
            for (int j = 0; j < ListTrigger.Count; j++)
            {
                TriggerBase trigger = ListTrigger[j];

                if (trigger is TriggerCollision)
                {
                    TriggerCollision triggerCol = (TriggerCollision)trigger;
                    TriggerCollision cloneTrigger = new TriggerCollision(triggerCol.TriggerName, clone, triggerCol.TargetEntite);

                    cloneTrigger.ListScript = triggerCol.ListScript;

                    clone.ListTrigger.Add(cloneTrigger);
                }
                else if (trigger is TriggerValueChanged)
                {
                    TriggerValueChanged triggerVal = (TriggerValueChanged)trigger;
                    TriggerValueChanged cloneTrigger = new TriggerValueChanged(triggerVal.TriggerName, clone, triggerVal.PropertyName, triggerVal.Sens, triggerVal.Value, triggerVal.IsCustomProperty);

                    cloneTrigger.ListScript = triggerVal.ListScript;

                    clone.ListTrigger.Add(cloneTrigger);
                }
                else if (trigger is TriggerLoad)
                {
                    TriggerLoad triggerLoad = (TriggerLoad)trigger;
                    TriggerLoad cloneTrigger = new TriggerLoad(triggerLoad.TriggerName, clone);

                    cloneTrigger.ListScript = triggerLoad.ListScript;

                    clone.ListTrigger.Add(cloneTrigger);
                }
                else if (trigger is TriggerMouse)
                {
                    TriggerMouse triggerMouse = (TriggerMouse)trigger;
                    TriggerMouse cloneTrigger = new TriggerMouse(triggerMouse.TriggerName, clone, triggerMouse.TriggerMouseType);

                    cloneTrigger.ListScript = triggerMouse.ListScript;

                    clone.ListTrigger.Add(cloneTrigger);
                }
            }
            //---

            //--- ParticleSystem
            for (int j = 0; j < ListParticleSystem.Count; j++)
            {
                ParticleSystem pSystem = ListParticleSystem[j];
                ParticleSystem clonePSystem = new ParticleSystem(clone);

                clonePSystem.EmmittingAngle = pSystem.EmmittingAngle;
                clonePSystem.EmmittingFromAllSurface = pSystem.EmmittingFromAllSurface;
                clonePSystem.FieldAngle = pSystem.FieldAngle;
                clonePSystem.ParticleSystemName = pSystem.ParticleSystemName;
                clonePSystem.Rate = pSystem.Rate;
                clonePSystem.Velocity = pSystem.Velocity;

                clone.ListParticleSystem.Add(clonePSystem);
                for (int k = 0; k < pSystem.ListParticleTemplate.Count; k++)
                {
                    Particle particle = pSystem.ListParticleTemplate[k];
                    Particle cloneParticle = particle.Clone(true);

                    clonePSystem.ListParticleTemplate.Add(cloneParticle);
                }
            }
            //---

            return clone;
        }

        #endregion
    }
}