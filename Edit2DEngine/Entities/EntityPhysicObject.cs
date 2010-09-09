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
        protected Vector2 _size;
        protected Vector2 _center = Vector2.Zero;
        protected Vector2 _relativePosition = Vector2.Zero;

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

        [Browsable(false)]
        public override Microsoft.Xna.Framework.Vector2 Center
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
                    return EntityParent.Position + EntityParent.Center;

                return new Microsoft.Xna.Framework.Vector2(body.Position.X, body.Position.Y);
            }
            set
            {
                RelativePosition = value - EntityParent.Position + EntityParent.Center;
            }
        }

        [Browsable(true), AttributeAction]
        public Microsoft.Xna.Framework.Vector2 RelativePosition
        {
            get
            {
                _relativePosition = body.Position - EntityParent.Position + EntityParent.Center;
                return _relativePosition;
            }
            set
            {
                _relativePosition = value;
                SetPosition(_relativePosition, EntityParent.Position + EntityParent.Center);
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

        public void SetPosition(Microsoft.Xna.Framework.Vector2 relativePosition, Microsoft.Xna.Framework.Vector2 parentPosition)
        {
            if (body != null)
            {
                body.ResetDynamics();
                body.Position = new Vector2(relativePosition.X+parentPosition.X, relativePosition.Y+parentPosition.Y);
                //CreateVerticesForRendering();
            }

            this.EntityParent.UpdateRectangle();
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
