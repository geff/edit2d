using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit2DEngine;
using Microsoft.Xna.Framework;
using Edit2DEngine.Actions;
using Edit2DEngine.Entities;
using Edit2DEngine.Triggers;
using Edit2DEngine.Entities.Particles;
using Edit2DEngine.CustomProperties;

namespace Edit2D
{
    public class Selection
    {
        public Object Object { get; set; }
        public Pointer Pointer { get; set; }

        public IMoveableObject MoveableObject { get; set; }
        public IResizeableObject ResizeableObject { get; set; }
        public ISelectableObject SelectableObject { get; set; }


        public Entity Entity { get; set; }
        public EntityComponent EntityComponent { get; set; }
        public EntityPhysicObject EntityPhysicObject { get; set; }
        public Camera Camera { get; set; }
        public Script Script { get; set; }
        public TriggerBase Trigger { get; set; }
        public ParticleSystem ParticleSystem { get; set; }

        public IActionHandler ActionHandler { get; set; }
        public ITriggerHandler TriggerHandler { get; set; }
        public ICustomPropertyHandler CustomPropertyHandler { get; set; }

        public Selection Temp { get; set; }

        public Selection(Object selectedObject, Vector2 worldPointer, Vector2 screenPointer) : this(selectedObject, worldPointer, screenPointer, true)
        {
        }

        private Selection(Object selectedObject, Vector2 worldPointer, Vector2 screenPointer, bool createClone)
        {
            this.Pointer = new Pointer();
            this.Pointer.WorldPosition = worldPointer;
            this.Pointer.ScreenPosition = screenPointer;

            this.Object = selectedObject;

            if (selectedObject is IMoveableObject)
                this.MoveableObject = (IMoveableObject)selectedObject;
            if (selectedObject is IResizeableObject)
                this.ResizeableObject = (IResizeableObject)selectedObject;
            if (selectedObject is ISelectableObject)
                this.SelectableObject = (ISelectableObject)selectedObject;
            if (selectedObject is Entity)
                this.Entity = (Entity)selectedObject;
            if (selectedObject is EntityComponent)
                this.EntityComponent = (EntityComponent)selectedObject;
            if (selectedObject is EntityPhysicObject)
                this.EntityPhysicObject = (EntityPhysicObject)selectedObject;
            if (selectedObject is Camera)
                this.Camera = (Camera)selectedObject;

            if (selectedObject is IActionHandler)
                this.ActionHandler = (IActionHandler)selectedObject;
            if (selectedObject is ITriggerHandler)
                this.TriggerHandler = (ITriggerHandler)selectedObject;
            if (selectedObject is ICustomPropertyHandler)
                this.CustomPropertyHandler = (ICustomPropertyHandler)selectedObject;

            if (selectedObject is ICloneable && createClone)
                this.Temp = new Selection(((ICloneable)selectedObject).Clone(), worldPointer, screenPointer, false);
        }
    }
}
