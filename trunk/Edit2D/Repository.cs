using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Xna.Framework;
using FarseerGames.GettingStarted;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Input;
using Edit2D.Particles;

namespace Edit2D
{
    public class Repository
    {
        public FrmEdit2D FrmEdit2D { get; set; }

        public Entite tempEntite;
        private Entite _currentEntite;
        private Random rnd;

        public static int EntityCount = 0;

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

        public Vector2 pointer = new Vector2();
        public Vector2 pointerDraw = new Vector2();

        public Vector2 pointer2 = new Vector2();
        public Vector2 pointerDraw2 = new Vector2();

        public bool pause = false;
        public bool showPhysic = true;
        public bool keyCtrlPressed = false;
        public bool keyShiftPressed = false;
        public bool keyAltPressed = false;
        public bool IsEntityClickableOnPlay = false;

        public MouseMode mouseMode = MouseMode.Move;

        public List<Entite> listEntite;

        public PhysicsSimulatorView PhysicsSimulatorView;
        public static PhysicsSimulator physicSimulator;
        public string CurrentTextureName;

        public Repository()
        {
            listEntite = new List<Entite>();
            rnd = new Random();
            World = new World();
        }

        public Entite GetSelectedEntite(Vector2 point)
        {
            List<Geom> listGeom = Repository.physicSimulator.CollideAll(point);

            if (listGeom != null && listGeom.Count > 0)
            {
                Entite entite = listEntite.Find(v => v.geom == listGeom[listGeom.Count - 1]);

                return entite;
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

        public Vector2 GetModelViewControlPosition()
        {
            Vector2 pos = Vector2.Zero;

            pos = new Vector2(FrmEdit2D.Location.X, FrmEdit2D.Location.Y) + new Vector2(FrmEdit2D.modelViewerControl.Location.X, FrmEdit2D.modelViewerControl.Location.Y);

            return pos;
        }

        public Vector2 GetMousePosition()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y) - GetModelViewControlPosition();

            return mousePosition;
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

            //--- Suppression de l'entit� pass�e en param�trer si elle est dans le repository
            //    Puis ajout
            if (this.listEntite.Contains(currentEntite))
            {
                this.listEntite.Remove(currentEntite);
                this.listEntite.Add(entite);
            }
            //---

            //--- Ajoute le Body et le Geom clon�
            Repository.physicSimulator.Add(entite.Body);
            Repository.physicSimulator.Add(entite.geom);
            //---

            //--- Fixe les valeurs
            entite.SetPosition(currentEntite.Position);
            entite.Rotation = currentEntite.Rotation;
            entite.IsColisionable = currentEntite.IsColisionable;
            entite.IsStatic = currentEntite.IsStatic;
            //---

            //--- Change la s�lection si l'entit� courante est la m�me que l'entit� pass�e en param�tre
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
    }

    public enum MouseMode : int
    {
        Move = 1,
        Resize = 2,
        Rotate = 3
    }
}