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

        public EntitySprite(bool linkToPhysiSimulator, bool isCollisionable, string textureName, string name, Entity entityParent)
        {
            Constructor(linkToPhysiSimulator, isCollisionable, textureName, name, entityParent);
        }

        public EntitySprite(bool linkToPhysiSimulator, string textureName, string name, Entity entityParent)
        {
            Constructor(linkToPhysiSimulator, true, textureName, name, entityParent);
        }

        private void Constructor(bool addToPhysicSimulator, bool isCollisionable, string textureName, string name, Entity entityParent)
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

            //ListParticleSystem = new List<ParticleSystem>();

            this.Color = Microsoft.Xna.Framework.Graphics.Color.White;
            this.Name = name;
            this.TextureName = textureName;

            //if (!(this is Particle))
            UniqueId = ++Repository.EntityCount;

            Init(addToPhysicSimulator, isCollisionable);
        }

        protected override void Init(bool addToPhysicSimulator, bool isCollisionable)
        {
            ChangeTexture(this.TextureName, addToPhysicSimulator, isCollisionable);

            if (Geom != null)
            {
                Geom.CollisionEnabled = isCollisionable;
                Geom.CollidesWith = CollisionCategory.All;
                Geom.CollisionResponseEnabled = true;
                Geom.FrictionCoefficient = 0.5f;
                Geom.RestitutionCoefficient = 0.5f;
            }
        }


        public override void ChangeSize(int width, int height, bool addToPhysicSimulator)
        {
            this._size = new Vector2(width, height);

            Texture2D texture = GetTexture();
            CreateBodyFromVertices(addToPhysicSimulator, true, ref body, ref _geom, width, height);
        }

        public virtual void ChangeTexture(string textureName, bool addToPhysicSimulator, bool isCollisionable)
        {
            this.TextureName = textureName;

            Texture2D texture = GetTexture();

            this.NativeImageSize = new Size(texture.Width, texture.Height);
            _size = new Vector2(texture.Width, texture.Height);

            CalcVerticesFromTexture(texture);
            CreateBodyFromVertices(addToPhysicSimulator, isCollisionable, ref body, ref _geom, texture.Width, texture.Height);
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

            //float midWidth = this.Size.X / 2f;
            //float midHeight = this.Size.Y / 2f;

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

        public override object Clone()
        {
            return null;// Clone(false);
        }
    }
}
