using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using Edit2DEngine.Entities;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics.Dynamics.Joints;
using FarseerGames.FarseerPhysics.Factories;

namespace Edit2DEngine.Tools
{
    public class PhysicManager
    {
        #region Springs, Joints
        public static void AddLinearSpring(EntityPhysicObject entity, EntityPhysicObject entity2, Microsoft.Xna.Framework.Vector2 vec1, Microsoft.Xna.Framework.Vector2 vec2)
        {
            LinearSpring linearSpring = SpringFactory.Instance.CreateLinearSpring(Repository.physicSimulator, entity.Body, vec1, entity2.Body, vec2, 10f, 10f);

            entity.ListLinearSpring.Add(linearSpring);
        }

        public static void AddFixedLinearSpring(EntityPhysicObject entity, Microsoft.Xna.Framework.Vector2 vec1, Microsoft.Xna.Framework.Vector2 vec2)
        {
            FixedLinearSpring fixedLinearSpring = SpringFactory.Instance.CreateFixedLinearSpring(Repository.physicSimulator, entity.Body, new Vector2(vec1.X, vec1.Y), new Vector2(vec2.X, vec2.Y), 10f, 10f);

            entity.ListFixedLinearSpring.Add(fixedLinearSpring);
        }

        public static void AddAngleSpring(EntityPhysicObject entity, EntityPhysicObject entity2)
        {
            AngleSpring angleSpring = SpringFactory.Instance.CreateAngleSpring(Repository.physicSimulator, entity.Body, entity2.Body, 1000f, 500f);

            entity.ListAngleSpring.Add(angleSpring);
        }

        public static void AddFixedAngleSpring(EntityPhysicObject entity)
        {
            FixedAngleSpring fixedAngleSpring = SpringFactory.Instance.CreateFixedAngleSpring(Repository.physicSimulator, entity.Body, 100000f, 50000f);

            entity.ListFixedAngleSpring.Add(fixedAngleSpring);
        }

        public static void AddPinJoint(EntityPhysicObject entity, EntityPhysicObject entity2, Microsoft.Xna.Framework.Vector2 vec1, Microsoft.Xna.Framework.Vector2 vec2)
        {
            PinJoint pinJoint = JointFactory.Instance.CreatePinJoint(Repository.physicSimulator, entity.Body, vec1, entity2.Body, vec2);

            entity.ListPinJoint.Add(pinJoint);
        }

        public static void AddRevoluteJoint(EntityPhysicObject entity, EntityPhysicObject entity2, Vector2 vec1)
        {
            RevoluteJoint revoluteJoint = JointFactory.Instance.CreateRevoluteJoint(Repository.physicSimulator, entity.Body, entity2.Body, vec1);

            entity.ListRevoluteJointJoint.Add(revoluteJoint);
        }

        public static void AddFixedRevoluteJoint(EntityPhysicObject entity, Vector2 vec1)
        {
            FixedRevoluteJoint fixedRevoluteJoint = JointFactory.Instance.CreateFixedRevoluteJoint(Repository.physicSimulator, entity.Body, vec1);

            entity.ListFixedRevoluteJoint.Add(fixedRevoluteJoint);
        }
        #endregion
    }
}
