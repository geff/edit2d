﻿using System;
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
        protected Body _body;
        protected Vertices _originalVerts;
        protected Vector2 _size;
        protected Vector2 _center = Vector2.Zero;
        protected Vector2 _relativePosition = Vector2.Zero;
        protected Boolean _addedToPhysicSimulator = false;
        protected Boolean _isClone;

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
            get
            {
                return _center;
            }
            set
            {
                this.SetCenterFromWorldPosition(value, true);
            }
        }

        public Boolean IsStatic
        {
            get
            {
                return _body.IsStatic;
            }
            set
            {
                _body.IsStatic = value;
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
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        [Browsable(true), AttributeAction]
        public override Microsoft.Xna.Framework.Vector2 Position
        {
            get
            {
                return new Microsoft.Xna.Framework.Vector2(_body.Position.X, _body.Position.Y);
            }
            set
            {
                RelativePosition = value - EntityParent.Position;
            }
        }

        [Browsable(true), AttributeAction]
        public Microsoft.Xna.Framework.Vector2 RelativePosition
        {
            get
            {
                return _relativePosition;
            }
            set
            {
                _relativePosition = value;
                SetPosition(_relativePosition, EntityParent.Position);
            }
        }

        [Browsable(true), AttributeAction]
        public override float Rotation
        {
            get
            {
                return _body.Rotation;
            }
            set
            {
                _body.Rotation = value;
                this.EntityParent.UpdateRectangle();
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

        public abstract void ChangeSize(int width, int height, bool addToPhysicSimulator, bool isCollisionable);

        public void SetPosition(Microsoft.Xna.Framework.Vector2 relativePosition, Microsoft.Xna.Framework.Vector2 parentPosition)
        {
            float prevRotation = _body.Rotation;
            _body.ResetDynamics();
            _body.Position = new Vector2(relativePosition.X + parentPosition.X, relativePosition.Y + parentPosition.Y);
            _body.Rotation = prevRotation;

            //CreateVerticesForRendering();

            this.EntityParent.UpdateRectangle();
        }

        public void SetNewCenter(Vector2 deltaPosition, bool addToPhysicSimulator)
        {
            float rotation = this.Rotation;

            deltaPosition = -deltaPosition;

            Geom.LocalVertices.Translate(ref deltaPosition);

            DistanceGrid.Instance.RemoveDistanceGrid(this.Geom);
            DistanceGrid.Instance.CreateDistanceGrid(this.Geom);

            deltaPosition = -deltaPosition;

            Vector2 worldPosition = _body.GetWorldPosition(deltaPosition);

            _center += deltaPosition;
            this.SetPosition(worldPosition, Vector2.Zero);
            this.Rotation = rotation;
        }

        public void SetCenterFromWorldPosition(Vector2 worldPosition, bool addToPhysicSimulator)
        {
            this.SetNewCenter(_body.GetLocalPosition(worldPosition), addToPhysicSimulator);
        }

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

        public override Microsoft.Xna.Framework.Rectangle Rectangle
        {
            get
            {
                Microsoft.Xna.Framework.Rectangle rectangle = Microsoft.Xna.Framework.Rectangle.Empty;

                rectangle.Location = new Microsoft.Xna.Framework.Point((int)Math.Round((double)_geom.AABB.Min.X), (int)Math.Round((double)_geom.AABB.Min.Y));
                rectangle.Width = (int)Math.Round((double)(_geom.AABB.Max.X - _geom.AABB.Min.X));
                rectangle.Height = (int)Math.Round((double)(_geom.AABB.Max.Y - _geom.AABB.Min.Y));

                return rectangle;
            }
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
                ChangeSize((int)value.X, (int)value.Y, _addedToPhysicSimulator, _addedToPhysicSimulator);
            }
        }
    }
}
