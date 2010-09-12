using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Input;
using Edit2DEngine.Entities.Particles;
using Edit2DEngine.Entities;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using Edit2DEngine.Actions;
using Edit2DEngine.Triggers;
using Edit2DEngine.CustomProperties;


namespace Edit2DEngine
{
    public class Repository
    {
        //public Object tempObject;
        private Random rnd;
        public static int EntityCount = 0;
        public bool Pause = false;


        public World World { get; set; }
        //public Entity currentEntity2;
        public List<Entity> listEntity;
        public Camera Camera;
        public static PhysicsSimulator physicSimulator;
        public string CurrentTextureName;
        public Stopwatch WatchLoading;

        public Repository()
        {
            listEntity = new List<Entity>();
            rnd = new Random();
            World = new World();
            this.Camera = new Camera();
            this.WatchLoading = new Stopwatch();

            Repository.physicSimulator = new PhysicsSimulator(new Vector2(0, 9.81f));
        }

        public EntityComponent GetSelectedEntityComponentFromLocation(Vector2 location)
        {
            EntityComponent entityComponentSelected = null;

            foreach (Entity entity in listEntity)
            {
                entityComponentSelected = entity.ListEntityComponent.Find(ec => ec.ContainsLocation(location));
            }

            //TODO : gérer la sélection en tenant compte de la superposition par couche et par ordre

            return entityComponentSelected;
        }

        public Object GetSelectedObectFromLocation(Vector2 location)
        {
            //TODO : gérer la sélection d'objet
            //List<Geom> listGeom = Repository.physicSimulator.CollideAll(location);

            //if (listGeom != null && listGeom.Count > 0)
            //{
            //    Entity selectedEntity = null;
            //    int minIndex = 0;

            //    for (int i = 0; i < listGeom.Count; i++)
            //    {
            //        Entity curEntity = listEntity.Find(e => e.geom == listGeom[i]);
            //        int index = listEntity.IndexOf(curEntity);

            //        if (selectedEntity == null || (index > minIndex))
            //        {
            //            selectedEntity = curEntity;
            //            minIndex = index;
            //        }
            //    }

            //    return selectedEntity;
            //}

            foreach (Entity entity in listEntity)
            {
                foreach (EntityComponent entityComponent in entity.ListEntityComponent)
                {
                    if (entityComponent.ContainsLocation(location))
                    {
                        return entityComponent;
                    }
                }
            }

            return null;
        }

        public Entity GetEntityFromBody(FarseerGames.FarseerPhysics.Dynamics.Body body)
        {
            if (this.listEntity != null && listEntity.Count > 0)
            {
                Entity entity = this.listEntity.Find(e =>
                    e.ListEntityComponent.Find(ec=> ec is EntitySprite &&  ((EntitySprite)ec).Body == body) != null);

                return entity;
            }

            return null;
        }

        //public virtual Vector2 GetModelViewControlPosition()
        //{
        //    Vector2 pos = Vector2.Zero;

        //    //pos = new Vector2(FrmEdit2D.Location.X, FrmEdit2D.Location.Y) + new Vector2(FrmEdit2D.modelViewerControl.Location.X, FrmEdit2D.modelViewerControl.Location.Y);

        //    return pos;
        //}

        public virtual Vector2 GetMousePosition()
        {
            //MouseState mouseState = Mouse.GetState();
            Vector2 pos = Vector2.Zero;

            //Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y) - GetModelViewControlPosition();

            return pos;// mousePosition;
        }

        public float GetRandomValue(float minValue, float maxValue)
        {
            return minValue + (float)rnd.NextDouble() * (maxValue - minValue);
        }

        //public string FoundNewName(string textureName)
        //{
        //    string name = String.Empty;

        //    bool found = false;
        //    int count = this.listEntity.FindAll(e => e.TextureName == textureName).Count;
        //    int number = 0;

        //    while (!found)
        //    {
        //        number++;
        //        string newName = String.Format("{0}{1}", textureName, number);

        //        if (this.listEntity.Find(e => e.Name == newName) == null)
        //        {
        //            found = true;
        //            name = newName;
        //        }
        //    }

        //    return name;
        //}

        public Entity ChangeEntitySize(Entity currentEntity, Size oldSize)
        {
            //TODO : gérer de nouveau le redimenssionnement
            /*
            //--- Suppression du Body et du Geom
            Repository.physicSimulator.BodyList.Remove(currentEntity.Body);
            Repository.physicSimulator.GeomList.Remove(currentEntity.geom);
            //---

            //--- Clone + Size
            Entity entity = (Entity)currentEntity.Clone(false);
            //entity.Size = new Size(currentEntity.Size.Width, currentEntity.Size.Height);
            entity.ChangeSize(currentEntity.Size.Width, currentEntity.Size.Height, false);
            //---

            //--- Suppression du Body et du Geom
            Repository.physicSimulator.Remove(currentEntity.Body);
            Repository.physicSimulator.Remove(currentEntity.geom);
            //---

            //--- Suppression de l'entité passée en paramètrer si elle est dans le repository
            //    Puis ajout
            if (this.listEntity.Contains(currentEntity))
            {
                this.listEntity.Remove(currentEntity);
                this.listEntity.Add(entity);
            }
            //---

            //--- Ajoute le Body et le Geom cloné
            Repository.physicSimulator.Add(entity.Body);
            Repository.physicSimulator.Add(entity.geom);
            //---

            //--- Fixe les valeurs
            entity.SetPosition(currentEntity.Position);
            entity.Rotation = currentEntity.Rotation;
            entity.IsColisionable = currentEntity.IsColisionable;
            entity.IsStatic = currentEntity.IsStatic;
            //---

            //--- Change la sélection si l'entité courante est la même que l'entité passée en paramètre
            //if (this.currentEntity == currentEntity)
            //{
            //    EntitySelectionChange(repository.currentEntity, entity);
            //}
            //---

            //--- Calcul du ratio de taille
            float ratioX = (float)currentEntity.Size.Width / (float)oldSize.Width;
            float ratioY = (float)currentEntity.Size.Height / (float)oldSize.Height;
            //---

            //--- Clone les Spring
            for (int i = 0; i < currentEntity.ListFixedLinearSpring.Count; i++)
            {
                Vector2 vecBody = new Vector2();
                vecBody.X = currentEntity.ListFixedLinearSpring[i].BodyAttachPoint.X * ratioX;
                vecBody.Y = currentEntity.ListFixedLinearSpring[i].BodyAttachPoint.Y * ratioY;

                entity.AddFixedLinearSpring(vecBody, currentEntity.ListFixedLinearSpring[i].WorldAttachPoint);

                Repository.physicSimulator.Remove(currentEntity.ListFixedLinearSpring[i]);
            }
            //---
            */
            //return entity;

            return null;
        }

        public void OrderEntity()
        {
            Dictionary<int, List<Entity>> dicEntity = new Dictionary<int, List<Entity>>();

            for (int i = 0; i < listEntity.Count; i++)
            {
                Entity entity = listEntity[i];

                if (!dicEntity.ContainsKey(entity.Layer))
                {
                    dicEntity.Add(entity.Layer, new List<Entity>());
                }

                dicEntity[entity.Layer].Add(entity);
            }

            List<Entity> listEntity2 = new List<Entity>();

            foreach (int key in dicEntity.Keys.OrderBy(key => key))
            {
                for (int j = 0; j < dicEntity[key].Count; j++)
                {
                    listEntity2.Add(dicEntity[key][j]);
                }
            }

            listEntity = listEntity2;
        }
    }
}