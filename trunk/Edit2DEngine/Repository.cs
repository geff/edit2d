using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Input;
using Edit2DEngine.Particles;
using System.Drawing;
using System.Linq;
using System.Diagnostics;

namespace Edit2DEngine
{
    public class Repository
    {
        public Entite tempEntite;
        private Entite _currentEntite;
        private Random rnd;
        public static int EntityCount = 0;

        public bool Pause = false;

        public Entite CurrentEntite
        {
            get
            {
                return _currentEntite;
            }
            set
            {
                listEntite.ForEach(ent => ent.Selected = false);

                _currentEntite = value;
                _currentObject = null;

                if (value != null)
                    _currentEntite.Selected = true;
            }
        }

        Object _currentObject = null;
        public Object CurrentObject
        {
            get
            {
                return _currentObject;
            }
            set
            {
                _currentObject = value;
                _currentEntite = null;
            }
        }

        public ParticleSystem CurrentParticleSystem
        {
            get
            {
                if (CurrentObject != null && CurrentObject is ParticleSystem)
                    return (ParticleSystem)CurrentObject;
                else
                    return null;
            }
            set
            {
                CurrentObject = value;
            }
        }

        public World World { get; set; }

        public Entite currentEntite2;

        //public Vector2 pointer = new Vector2();
        //public Vector2 pointerDraw = new Vector2();

        //public Vector2 pointer2 = new Vector2();
        //public Vector2 pointerDraw2 = new Vector2();

        public List<Entite> listEntite;

        public static PhysicsSimulator physicSimulator;
        public string CurrentTextureName;

        public Camera Camera;

        public Stopwatch WatchLoading;

        public Repository()
        {
            listEntite = new List<Entite>();
            rnd = new Random();
            World = new World();
            this.Camera = new Camera();
            this.WatchLoading = new Stopwatch();

            Repository.physicSimulator = new PhysicsSimulator(new Vector2(0, 9.81f));
            //Repository.physicSimulator.NarrowPhaseCollisionTime
            //PhysicsSimulator.NarrowPhaseCollider = NarrowPhaseCollider.SAT;  
        }

        public Entite GetSelectedEntiteFromLocation(Vector2 location)
        {
            List<Geom> listGeom = Repository.physicSimulator.CollideAll(location);

            if (listGeom != null && listGeom.Count > 0)
            {
                Entite selectedEntite = null;
                int minIndex = 0;

                for (int i = 0; i < listGeom.Count; i++)
                {
                    Entite curEntite = listEntite.Find(e => e.geom == listGeom[i]);
                    int index = listEntite.IndexOf(curEntite);

                    if (selectedEntite == null || (index > minIndex))
                    {
                        selectedEntite = curEntite;
                        minIndex = index;
                    }
                }

                return selectedEntite;
            }

            return null;
        }

        public Entite GetEntiteFromBody(FarseerGames.FarseerPhysics.Dynamics.Body body)
        {
            if (this.listEntite != null && listEntite.Count > 0)
            {
                Entite entite = this.listEntite.Find(e => e.Body == body);

                return entite;
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

        public string FoundNewName(string textureName)
        {
            string name = String.Empty;

            bool found = false;
            int count = this.listEntite.FindAll(e => e.TextureName == textureName).Count;
            int number = 0;

            while (!found)
            {
                number++;
                string newName = String.Format("{0}{1}", textureName, number);

                if (this.listEntite.Find(e => e.Name == newName) == null)
                {
                    found = true;
                    name = newName;
                }
            }

            return name;
        }

        public Entite ChangeEntitySize(Entite currentEntite, Size oldSize)
        {
            //--- Suppression du Body et du Geom
            Repository.physicSimulator.BodyList.Remove(currentEntite.Body);
            Repository.physicSimulator.GeomList.Remove(currentEntite.geom);
            //---

            //--- Clone + Size
            Entite entite = (Entite)currentEntite.Clone(false);
            //entite.Size = new Size(currentEntite.Size.Width, currentEntite.Size.Height);
            entite.ChangeSize(currentEntite.Size.Width, currentEntite.Size.Height, false);
            //---

            //--- Suppression du Body et du Geom
            Repository.physicSimulator.Remove(currentEntite.Body);
            Repository.physicSimulator.Remove(currentEntite.geom);
            //---

            //--- Suppression de l'entité passée en paramètrer si elle est dans le repository
            //    Puis ajout
            if (this.listEntite.Contains(currentEntite))
            {
                this.listEntite.Remove(currentEntite);
                this.listEntite.Add(entite);
            }
            //---

            //--- Ajoute le Body et le Geom cloné
            Repository.physicSimulator.Add(entite.Body);
            Repository.physicSimulator.Add(entite.geom);
            //---

            //--- Fixe les valeurs
            entite.SetPosition(currentEntite.Position);
            entite.Rotation = currentEntite.Rotation;
            entite.IsColisionable = currentEntite.IsColisionable;
            entite.IsStatic = currentEntite.IsStatic;
            //---

            //--- Change la sélection si l'entité courante est la même que l'entité passée en paramètre
            //if (this.currentEntite == currentEntite)
            //{
            //    EntiteSelectionChange(repository.currentEntite, entite);
            //}
            //---

            //--- Calcul du ratio de taille
            float ratioX = (float)currentEntite.Size.Width / (float)oldSize.Width;
            float ratioY = (float)currentEntite.Size.Height / (float)oldSize.Height;
            //---

            //--- Clone les Spring
            for (int i = 0; i < currentEntite.ListFixedLinearSpring.Count; i++)
            {
                Vector2 vecBody = new Vector2();
                vecBody.X = currentEntite.ListFixedLinearSpring[i].BodyAttachPoint.X * ratioX;
                vecBody.Y = currentEntite.ListFixedLinearSpring[i].BodyAttachPoint.Y * ratioY;

                entite.AddFixedLinearSpring(vecBody, currentEntite.ListFixedLinearSpring[i].WorldAttachPoint);

                Repository.physicSimulator.Remove(currentEntite.ListFixedLinearSpring[i]);
            }
            //---

            return entite;
        }

        public void OrderEntite()
        {
            Dictionary<int, List<Entite>> dicEntite = new Dictionary<int, List<Entite>>();

            for (int i = 0; i < listEntite.Count; i++)
            {
                Entite entite = listEntite[i];

                if (!dicEntite.ContainsKey(entite.Layer))
                {
                    dicEntite.Add(entite.Layer, new List<Entite>());
                }

                dicEntite[entite.Layer].Add(entite);
            }

            List<Entite> listEntite2 = new List<Entite>();

            foreach (int key in dicEntite.Keys.OrderBy(key => key))
            {
                for (int j = 0; j < dicEntite[key].Count; j++)
                {
                    listEntite2.Add(dicEntite[key][j]);
                }
            }

            listEntite = listEntite2;
        }
    }
}