using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using System.Drawing;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using FarseerGames.FarseerPhysics.Dynamics.Joints;

using Edit2DEngine.Entities.Particles;
using Microsoft.Xna.Framework.Graphics;
using Edit2DEngine.Tools;
using FarseerGames.FarseerPhysics.Factories;
using Edit2DEngine.Triggers;

namespace Edit2DEngine.Entities
{
    public class EntitySprite : EntityPhysicObject
    {
        [Browsable(false)]
        public Size NativeImageSize { get; set; }

        [Browsable(true)]
        public String TextureName { get; set; }

        [Browsable(false)]
        public override string TreeViewPath
        {
            get
            {
                return "Monde\\Entités\\" + this.EntityParent.Name + "\\Sprite\\" + this.Name;
            }
        }

        public EntitySprite(bool linkToPhysiSimulator, string textureName, string name, Entity entityParent) :
            this(linkToPhysiSimulator, true, textureName, name, entityParent)
        {
        }

        public EntitySprite(bool addToPhysicSimulator, bool isCollisionable, string textureName, string name, Entity entityParent)
        {
            ListFixedLinearSpring = new List<FixedLinearSpring>();
            ListLinearSpring = new List<LinearSpring>();
            ListFixedAngleSpring = new List<FixedAngleSpring>();
            ListAngleSpring = new List<AngleSpring>();
            ListPinJoint = new List<PinJoint>();
            ListRevoluteJointJoint = new List<RevoluteJoint>();
            ListFixedRevoluteJoint = new List<FixedRevoluteJoint>();

            this.EntityParent = entityParent;
            this.ListScript = new List<Script>();
            this.ListTrigger = new List<TriggerBase>();


            this.Color = Microsoft.Xna.Framework.Graphics.Color.White;
            this.Name = name;
            this.TextureName = textureName;
            _addedToPhysicSimulator = addToPhysicSimulator;

            this.UniqueId = ++Repository.EntityComponentCount;

            Init(addToPhysicSimulator, isCollisionable);
        }

        protected override void Init(bool addToPhysicSimulator, bool isCollisionable)
        {
            ChangeTexture(this.TextureName, addToPhysicSimulator, isCollisionable);

            if (addToPhysicSimulator)
            {
                Geom.CollisionEnabled = isCollisionable;
                Geom.CollisionCategories = (CollisionCategory)Math.Pow(2, this.EntityParent.UniqueId);
                Geom.CollidesWith = CollisionCategory.All & ~Geom.CollisionCategories;
                Geom.CollisionResponseEnabled = true;
                Geom.FrictionCoefficient = 0.5f;
                Geom.RestitutionCoefficient = 0.5f;
            }
        }


        public override void ChangeSize(int width, int height, bool addToPhysicSimulator, bool isCollisionable)
        {
            _size = new Vector2(width, height);

            Texture2D texture = GetTexture();
            CreateBodyFromVertices(addToPhysicSimulator, isCollisionable, width, height);
        }

        public virtual void ChangeTexture(string textureName, bool addToPhysicSimulator, bool isCollisionable)
        {
            this.TextureName = textureName;

            Texture2D texture = GetTexture();

            this.NativeImageSize = new Size(texture.Width, texture.Height);
            _size = new Vector2(texture.Width, texture.Height);

            CalcVerticesFromTexture(texture);
            CreateBodyFromVertices(addToPhysicSimulator, isCollisionable, texture.Width, texture.Height);
        }

        private void CalcVerticesFromTexture(Texture2D polygonTexture)
        {
            //Create an array to hold the data from the texture
            uint[] data = new uint[polygonTexture.Width * polygonTexture.Height];

            //Transfer the texture data to the array
            polygonTexture.GetData(data);

            //--- Calcul des vertices originaux
            _originalVerts = Vertices.CreatePolygon(data, polygonTexture.Width, polygonTexture.Height);//, 1f, 127, true, true);// 2f, out vec);
            //---
        }

        private void CreateBodyFromVertices(Boolean addToPhysicSimulator, bool isCollisionable, int width, int height)
        {
            float widthFactor = (float)width / (float)NativeImageSize.Width;
            float heightFactor = (float)height / (float)NativeImageSize.Height;

            //--- Copie des vertices originaux puis transformation
            Vertices verts = new Vertices(_originalVerts);
            Vector2 vecScale = new Vector2(widthFactor, heightFactor);
            verts.Scale(ref vecScale);
            //---

            //--- Calcul du centre du polygone
            _center = _originalVerts.GetCentroid();
            //---

            //--- Calcul des Vertices pour l'affichage
            CreateVerticesForRendering(_originalVerts);
            //---

            //Use the body factory to create the physics body
            if (addToPhysicSimulator)
            {
                //--- Suppression du body et du geom
                if (_body != null)
                    Repository.physicSimulator.Remove(_body);
                if (_geom != null)
                    Repository.physicSimulator.Remove(_geom);
                //---

                Vector2 prevBodyPosition = Vector2.Zero;
                float prevBodyRotation = 0f;
                bool prevStatic = false;
                bool prevCollisionable = false;
                float prevMass = 5f;

                if (_body != null)
                {
                    prevBodyPosition = _body.Position;
                    prevBodyRotation = _body.Rotation;
                    prevStatic = _body.IsStatic;
                    prevCollisionable = _geom.CollisionEnabled;
                    prevMass = _body.Mass;
                }

                _body = BodyFactory.Instance.CreatePolygonBody(Repository.physicSimulator, verts, 5);

                _geom = GeomFactory.Instance.CreatePolygonGeom(Repository.physicSimulator, _body, verts, 0f);

                _geom.SetBody(_body);

                _body.Position = prevBodyPosition;
                _body.Rotation = prevBodyRotation;
                _body.IsStatic = prevStatic;

                if (isCollisionable)
                {
                    _geom.CollisionEnabled = prevCollisionable;
                }
                else
                {
                    _geom.CollisionEnabled = false;
                }
                _body.Mass = prevMass;
            }
            else
            {
                _body = BodyFactory.Instance.CreatePolygonBody(verts, 5);
                _geom = GeomFactory.Instance.CreatePolygonGeom(_body, verts, 0f);
            }
        }

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


            //--- Calculer les vecteurs minimaux et maximaux pour l'AABB
            Vector2 vecMin = new Vector2(float.MaxValue, float.MaxValue);
            Vector2 vecMax = new Vector2(float.MinValue, float.MinValue);

            foreach (Vector2 vector in vertices)
            {
                if (vector.X < vecMin.X)
                    vecMin.X = vector.X;
                if (vector.X > vecMax.X)
                    vecMax.X = vector.X;
                if (vector.Y < vecMin.Y)
                    vecMin.Y = vector.Y;
                if (vector.Y > vecMax.Y)
                    vecMax.Y = vector.Y;
            }
            //---

            AABB boundingBox = new AABB(ref vecMin, ref vecMax);

            //AABB boundingBox = new AABB(vertices);
            
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

        public override object Clone()
        {
            Entity entityParent = (Entity)this.EntityParent.Clone();
            EntitySprite clone = new EntitySprite(false, false, this.TextureName, this.Name, entityParent);

            clone._isClone = true;
            entityParent.ListEntityComponent.Add(clone);

            clone.Size = this.Size;
            
            //clone.Position = this.Position;
            clone.Rotation = this.Rotation;
            clone._relativePosition = this.RelativePosition;
            clone.SetPosition(this.Position, Vector2.Zero);
            //clone.Center = this.Center;

            //clone.EntityParent.Rectangle = this.EntityParent.Rectangle;
            //clone.EntityParent.Position = this.EntityParent.Position;
            //clone._po = this.Position;

            return clone;
        }
    }
}
