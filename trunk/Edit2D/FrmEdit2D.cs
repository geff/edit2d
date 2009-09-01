using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using WinFormsContentLoading;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerGames.GettingStarted;
using System.IO;
using Xna.Tools;
using Edit2DEngine.Action;
using Edit2DEngine.Trigger;
using Edit2DEngine.Particles;
using Edit2DEngine;
using Edit2DEngine.Render;
using Edit2D.Properties;
using System.Drawing.Imaging;
using FarseerGames.FarseerPhysics.Collisions;

namespace Edit2D
{
    public partial class FrmEdit2D : Form
    {
        #region Attributs
        Random rnd;
        Pointer pointer = new Pointer();
        Pointer pointerCamera = new Pointer();

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public Repository repository = new Repository();
        private Render render;
        //private static string path = @"D:\Geff\Log\Edit2D\Edit2D\Data\";
        //private static string path = @"D:\Log\Edit2D\Edit2D\Data\";

        ContentBuilder contentBuilder;
        ContentManager contentManager;

        private Vector2 prevPosCamera = Vector2.Zero;
        private Vector2 vecFocal = Vector2.Zero;
        private Vector2 vecOldCorner = Vector2.Zero;
        private float oldZoom = 1f;

        Dictionary<MouseMode, Cursor> dicCursors = new Dictionary<MouseMode, Cursor>();

        #endregion

        #region Initialize
        public FrmEdit2D()
        {
            InitializeComponent();
            modelViewerControl.MouseWheel += new MouseEventHandler(modelViewerControl_MouseWheel);
            this.Load += new EventHandler(Form1_Load);
            listView.DrawItem += new DrawListViewItemEventHandler(listView_DrawItem);
        }

        private void Init()
        {
            rnd = new Random();

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1;
            timer.Enabled = true;

            repository = new Repository();

            //--- Création des curseurs
            dicCursors.Add(MouseMode.Move, CreateCursor(Resources.MouseMove));
            dicCursors.Add(MouseMode.Resize, CreateCursor(Resources.MouseScale));
            dicCursors.Add(MouseMode.Rotate, CreateCursor(Resources.MouseRotate));
            dicCursors.Add(MouseMode.Select, CreateCursor(Resources.MouseSelect));
            //---

            InitRender();

            repository.CurrentTextureName = "BigRec";
            repository.CurrentPointer2.WorldPosition = new Vector2(100, 10);
            repository.CurrentPointer.WorldPosition = new Vector2(200, 200);
            repository.FrmEdit2D = this;
            repository.Pause = true;

            AddEntity();
            repository.CurrentEntite = repository.listEntite[0];

            InitListViewImage();
            RefreshTreeView();

            triggerControl.repository = repository;
            scriptControl.repository = repository;
            particleControl.repository = repository;

            particleControl.InitParticleControl();

            btnTriggerModeBar.PerformClick();
        }

        private Cursor CreateCursor(Icon graphicCursor)
        {
            string fileName = Path.GetTempFileName();

            FileStream stream = new FileStream(fileName, FileMode.Create);
            graphicCursor.Save(stream);
            stream.Close();

            Cursor cursor = new Cursor(fileName);

            return cursor;
        }

        private void InitListViewImage()
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(50, 50);
            imageList.ColorDepth = ColorDepth.Depth32Bit;

            foreach (String textureName in TextureManager.ListTexture2D.Keys)
            {
                Texture2D texture = TextureManager.ListTexture2D[textureName];

                //------
                Bitmap bmp = TextureManager.GetBitmapFromTexture2D(texture);
                //------

                imageList.Images.Add(textureName, (Image)bmp);

                listView.Items.Add(new ListViewItem(textureName, textureName));
                listView.LargeImageList = imageList;
            }
        }

        private void InitRender()
        {
            string directory = Environment.CurrentDirectory;

            contentBuilder = new ContentBuilder();

            contentManager = new ContentManager(modelViewerControl.Services,
                                                directory);

            modelViewerControl.Initialize(repository, contentManager, contentBuilder);
            //TextureManager.InitTextureManager(modelViewerControl.GraphicsDevice);
            TextureManager.InitTextureManager(modelViewerControl.GraphicsDevice, @"..\..\..");
            render = new Render(modelViewerControl.spriteBatch, modelViewerControl.GraphicsDevice, repository, null);

            InitPhysicSimulatorView();
        }

        private void InitPhysicSimulatorView()
        {
            repository.PhysicsSimulatorView = new PhysicsSimulatorView(Repository.physicSimulator);
            //repository.PhysicsSimulatorView.EnableGridView = false;
            repository.PhysicsSimulatorView.EnablePerformancePanelView = false;
            //repository.PhysicsSimulatorView.EnablePerformancePanelBodyCount = false;
            repository.PhysicsSimulatorView.EnableAABBView = false;
            repository.PhysicsSimulatorView.EnableEdgeView = true;
            repository.PhysicsSimulatorView.EnablePinJointView = true;
            repository.PhysicsSimulatorView.EnableRevoluteJointView = true;
            repository.PhysicsSimulatorView.EnableVerticeView = true;
            //repository.PhysicsSimulatorView.EnableGridView = true;
            //repository.PhysicsSimulatorView.EnableCoordinateAxisView = false;
        }

        #endregion

        #region Entity
        private void AddEntity()
        {
            if (!String.IsNullOrEmpty(repository.CurrentTextureName))
            {
                //--- Calcul le nom de l'entité
                string name = repository.FoundNewName(repository.CurrentTextureName);
                //---

                Entite entite = new Entite(true, false, repository.CurrentTextureName.ToString(), name);

                entite.IsStatic = true;
                entite.SetPosition(repository.CurrentPointer.WorldPosition);
                repository.listEntite.Add(entite);

                EntiteSelectionChange(repository.CurrentEntite, entite);

                Repository.physicSimulator.Update(0.0000001f);

                entite.geom.CollisionEnabled = true;
            }
        }

        private void DeleteEntity()
        {
            if (repository.CurrentEntite != null)
            {
                repository.CurrentEntite.geom.Dispose();
                repository.CurrentEntite.Body.Dispose();
                repository.listEntite.Remove(repository.CurrentEntite);

                Repository.physicSimulator.Update(0.0000001f);

                EntiteSelectionChange(repository.CurrentEntite, null);
                //RefreshTreeView();
            }
        }
        #endregion

        #region Spring
        private void AddLinearSpring()
        {
            if (repository.CurrentEntite != null && repository.currentEntite2 != null)
            {
                Vector2 vec1 = repository.CurrentEntite.Body.GetLocalPosition(repository.CurrentPointer.WorldPosition);
                Vector2 vec2 = repository.currentEntite2.Body.GetLocalPosition(repository.CurrentPointer2.WorldPosition);

                repository.CurrentEntite.AddLinearSpring(repository.currentEntite2, vec1, vec2);

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddFixedLinearSpring()
        {
            if (repository.CurrentEntite != null)
            {
                Vector2 vec1 = repository.CurrentEntite.Body.GetLocalPosition(repository.CurrentPointer.WorldPosition);
                Vector2 vec2 = repository.CurrentPointer2.WorldPosition;

                repository.CurrentEntite.AddFixedLinearSpring(vec1, vec2);

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddFixedAngleSpring()
        {
            if (repository.CurrentEntite != null)
            {
                repository.CurrentEntite.AddFixedAngleSpring();

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddAngleSpring()
        {
            if (repository.CurrentEntite != null && repository.ListSelection.Count > 0)
            {
                repository.CurrentEntite.AddAngleSpring(repository.ListSelection[0].Entite);

                Repository.physicSimulator.Update(0.0001f);
            }
        }
        #endregion

        #region Joint
        private void AddPinJoint()
        {
            if (repository.CurrentEntite != null && repository.currentEntite2 != null)
            {
                Vector2 vec1 = repository.CurrentEntite.Body.GetLocalPosition(repository.CurrentPointer.WorldPosition);
                Vector2 vec2 = repository.currentEntite2.Body.GetLocalPosition(repository.CurrentPointer2.WorldPosition);

                repository.CurrentEntite.AddPinJoint(repository.currentEntite2, vec1, vec2);

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddRevoluteJoint()
        {
            if (repository.CurrentEntite != null && repository.currentEntite2 != null)
            {
                Vector2 vec1 = repository.CurrentEntite.Body.GetWorldPosition(repository.CurrentPointer.WorldPosition);
                Vector2 vec2 = repository.currentEntite2.Body.GetLocalPosition(repository.CurrentPointer2.WorldPosition);

                repository.CurrentEntite.AddRevoluteJoint(repository.currentEntite2, vec1);

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddFixedRevoluteJoint()
        {
            if (repository.CurrentEntite != null)
            {
                repository.CurrentEntite.AddFixedRevoluteJoint(repository.CurrentPointer.WorldPosition);

                Repository.physicSimulator.Update(0.0001f);
            }
        }
        #endregion

        #region Private methods
        private bool ChangeEntityName(Entite entite, string newName, string oldName)
        {
            bool nameChanged = false;
            int countFound = repository.listEntite.FindAll(ent => ent.Name == newName.ToString() && ent != entite).Count;

            string nameFound = repository.FoundNewName(entite.TextureName);

            if (countFound > 0)
            {
                if (MessageBox.Show(String.Format("Le nom {0} est déja utilisé pour une entité, voulez-vous utiliser le nom {1} ?", newName, nameFound), "Avertissement", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    nameChanged = true;
                    entite.Name = nameFound;
                }
                else
                {
                    nameChanged = false;
                    entite.Name = oldName;
                }
            }
            else
            {
                nameChanged = true;
                entite.Name = newName;
            }

            return nameChanged;
        }

        private void EntiteSelectionChange(Entite oldEntite, Entite newEntite)
        {
            EntiteSelectionChange(true, oldEntite, newEntite);
        }

        private void EntiteSelectionChange(bool refreshTreeView, Entite oldEntite, Object newSelection)
        {
            if (newSelection == null)
            {
                repository.CurrentEntite = null;
                repository.CurrentObject = null;
            }

            if (newSelection is Entite)
                repository.CurrentEntite = (Entite)newSelection;
            else
                repository.CurrentObject = newSelection;

            if (refreshTreeView)
                RefreshTreeView();

            if (repository.CurrentEntite != null)
            {
                prop.SelectedObject = repository.CurrentEntite;
                btnPinStatic.Checked = repository.CurrentEntite.IsStatic;
                btnColisionable.Checked = repository.CurrentEntite.IsColisionable;

                //--- Affiche le nom de l'entité courante dans la statusStrip
                toolStripTextBoxEntityName.Text = repository.CurrentEntite.Name;
                //---
            }
            else if (repository.CurrentParticleSystem != null)
            {
                prop.SelectedObject = repository.CurrentParticleSystem;
            }
            else if (newSelection is World)
            {
                prop.SelectedObject = (World)newSelection;
            }

            //--- Rafraichi le contrôle du mode courant
            if (btnTriggerModeBar.Checked)
            {
                triggerControl.RefreshTriggerList();
            }
            else if (btnScriptModeBar.Checked)
            {
                scriptControl.RefreshScriptControl();
            }
            else if (btnParticleSystemModeBar.Checked)
            {
                particleControl.RefreshParticleControl();
            }
            //---

            if (repository.CurrentEntite != null)
            {
                String nodeKey = String.Empty;

                if (repository.CurrentEntite is Particle)
                {
                    ParticleSystem pSystem = ((Particle)repository.CurrentEntite).ParticleSystem;
                    string pSystemKey = String.Format("{0}-{1}", pSystem.Entite.Name, pSystem.ParticleSystemName);

                    nodeKey = String.Format("{0}-{1}", pSystemKey, repository.CurrentEntite.Name);
                }
                else
                {
                    nodeKey = repository.CurrentEntite.Name;
                }

                TreeNode node = treeView.Nodes[0].Nodes.Find(nodeKey, true)[0];
                treeView.SelectedNode = node;
            }
            else if (repository.CurrentParticleSystem != null)
            {
                String nodeKey = String.Format("{0}-{1}", repository.CurrentParticleSystem.Entite.Name, repository.CurrentParticleSystem.ParticleSystemName);

                TreeNode node = treeView.Nodes[0].Nodes.Find(nodeKey, true)[0];
                treeView.SelectedNode = node;
            }
            else
            {
                treeView.SelectedNode = null;
            }
        }

        private void RefreshTreeView()
        {
            treeView.Nodes.Clear();
            TreeNode nodeRoot = treeView.Nodes.Add("World");
            nodeRoot.Tag = repository.World;

            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];

                TreeNode nodeEntite = nodeRoot.Nodes.Add(entite.Name, entite.Name);
                nodeEntite.Tag = entite;

                //--- Spring
                if (entite.ListFixedLinearSpring.Count > 0 || entite.ListLinearSpring.Count > 0)
                {
                    TreeNode nodeSpring = nodeEntite.Nodes.Add("Spring");

                    //--- FixedLinearSpring
                    if (entite.ListFixedLinearSpring.Count > 0)
                    {
                        for (int j = 0; j < entite.ListFixedLinearSpring.Count; j++)
                        {
                            TreeNode node = nodeSpring.Nodes.Add("FixedLinearSpring " + j);
                            node.Tag = entite.ListFixedLinearSpring[j];
                        }
                    }
                    //---

                    //--- LinearSpring
                    if (entite.ListLinearSpring.Count > 0)
                    {
                        for (int j = 0; j < entite.ListLinearSpring.Count; j++)
                        {
                            TreeNode node = nodeSpring.Nodes.Add(String.Format("LinearSpring {0} ({1})", j, ""));
                            node.Tag = entite.ListLinearSpring[j];
                        }
                    }
                    //---
                }
                //---

                //--- ParticleSystem
                if (entite.ListParticleSystem.Count > 0)
                {
                    for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                    {
                        string particleSystemKey = String.Format("{0}-{1}", entite.Name, entite.ListParticleSystem[j].ParticleSystemName);
                        TreeNode nodeParticleSystem = nodeEntite.Nodes.Add(particleSystemKey, entite.ListParticleSystem[j].ParticleSystemName);
                        nodeParticleSystem.Tag = entite.ListParticleSystem[j];

                        for (int k = 0; k < entite.ListParticleSystem[j].ListParticleTemplate.Count; k++)
                        {
                            TreeNode node = nodeParticleSystem.Nodes.Add(String.Format("{0}-{1}", particleSystemKey, entite.ListParticleSystem[j].ListParticleTemplate[k].Name), entite.ListParticleSystem[j].ListParticleTemplate[k].Name);
                            node.Tag = entite.ListParticleSystem[j].ListParticleTemplate[k];
                        }
                    }
                }
                //---
            }

            nodeRoot.ExpandAll();
        }

        private void New()
        {
            Repository.physicSimulator = new FarseerGames.FarseerPhysics.PhysicsSimulator(new Vector2(0, 9.81f));

            MouseMode mouseMode = repository.mouseMode;
            bool pause = repository.Pause;
            string textureName = repository.CurrentTextureName;

            repository = new Repository();
            repository.mouseMode = mouseMode;

            modelViewerControl.Initialize(repository, contentManager, contentBuilder);

            btnShowDebugMode.Checked = true;
            InitPhysicSimulatorView();

            repository.PhysicsSimulatorView.LoadContent(modelViewerControl.GraphicsDevice, contentManager);

            repository.CurrentPointer.WorldPosition = new Vector2(100, 80);

            repository.Camera.Zoom = 1f;
            repository.Camera.Position = new Vector2(0, 0);
            repository.Camera.Focal = new Vector2(0, 0);

            repository.Pause = pause;
            repository.mouseMode = mouseMode;
            repository.CurrentTextureName = textureName;
            repository.FrmEdit2D = this;

            render.UpdatePhysic();
            RefreshTreeView();

            triggerControl.repository = repository;
            scriptControl.repository = repository;
            particleControl.repository = repository;
        }

        private void Open()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Niveau *.lvl |*.lvl";

            Repository.physicSimulator.Enabled = false;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                New();

                FileSystem.Open(dlg.FileName, this.repository);

                triggerControl.repository = repository;
                render.repository = repository;
            }

            Repository.physicSimulator.Enabled = true;

            RefreshTreeView();
            render.UpdatePhysic();
        }

        private void Save()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Niveau *.lvl |*.lvl";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileSystem.Save(dlg.FileName, repository);
            }
        }

        #endregion

        #region Toolbar events
        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.btnPause.Enabled = true;
            this.btnPlay.Enabled = false;
            repository.Pause = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            this.btnPause.Enabled = false;
            this.btnPlay.Enabled = true;

            repository.Pause = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.btnPause.Enabled = false;
            this.btnPlay.Enabled = true;

            repository.Pause = true;

            foreach (Entite entite in repository.listEntite)
            {
                foreach (TriggerBase trigger in entite.ListTrigger)
                {
                    trigger.InitTrigger(repository);
                }

                foreach (Script script in entite.ListScript)
                {
                    foreach (ActionBase action in script.ListAction)
                    {
                        action.InitAction();
                    }
                }
            }

            //--- World
            foreach (TriggerBase trigger in repository.World.ListTrigger)
            {
                trigger.InitTrigger(repository);
            }
            //---
        }

        private void btnRecObjectStatus_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null)
            {
                AddScriptObjectStatus(repository.CurrentEntite.Position, repository.CurrentEntite.Rotation, repository.CurrentEntite.SizeVector);
            }
        }

        private void btnRecObjectStatusLoop_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null)
            {
                Script script = null;

                if (btnScriptModeBar.Checked && (script = scriptControl.GetSelectedScript()) != null)
                {
                    if (script.ListAction.Count > 0)
                    {
                        ActionCurve actionCurve = null;

                        if ((actionCurve = (ActionCurve)script.ListAction.Find(a => a.ActionName == "Position")) != null)
                        {
                            actionCurve.ListCurve[0].Keys.Add(new CurveKey(scriptControl.TimeLineValue, actionCurve.ListCurve[0].Keys[0].Value));
                            actionCurve.ListCurve[1].Keys.Add(new CurveKey(scriptControl.TimeLineValue, actionCurve.ListCurve[1].Keys[0].Value));

                            actionCurve.ListCurve[0].ComputeTangents(CurveTangent.Smooth);
                            actionCurve.ListCurve[1].ComputeTangents(CurveTangent.Smooth);

                            actionCurve.CalcDuration();
                        }
                        if ((actionCurve = (ActionCurve)script.ListAction.Find(a => a.ActionName == "Rotation")) != null)
                        {
                            actionCurve.ListCurve[0].Keys.Add(new CurveKey(scriptControl.TimeLineValue, actionCurve.ListCurve[0].Keys[0].Value));

                            actionCurve.ListCurve[0].ComputeTangents(CurveTangent.Smooth);

                            actionCurve.CalcDuration();
                        }
                        if ((actionCurve = (ActionCurve)script.ListAction.Find(a => a.ActionName == "Size")) != null)
                        {
                            actionCurve.ListCurve[0].Keys.Add(new CurveKey(scriptControl.TimeLineValue, actionCurve.ListCurve[0].Keys[0].Value));
                            actionCurve.ListCurve[1].Keys.Add(new CurveKey(scriptControl.TimeLineValue, actionCurve.ListCurve[1].Keys[0].Value));

                            actionCurve.ListCurve[0].ComputeTangents(CurveTangent.Smooth);
                            actionCurve.ListCurve[1].ComputeTangents(CurveTangent.Smooth);

                            actionCurve.CalcDuration();
                        }
                    }

                    short timeLineValueIncrement = 1000;
                    if (!Int16.TryParse(txtRecTime.Text, out timeLineValueIncrement))
                        timeLineValueIncrement = 1000;

                    scriptControl.TimeLineValue += timeLineValueIncrement;
                    scriptControl.RefreshScriptControl();
                }
            }
        }

        private void AddScriptObjectStatus(Vector2 position, float rotation, Vector2 size)
        {
            if (repository.CurrentEntite != null)
            {
                Script script = null;

                if (!(btnScriptModeBar.Checked && (script = scriptControl.GetSelectedScript()) != null))
                {
                    ShowScriptMode();
                    script = this.scriptControl.AddScriptToCurrentEntity();
                }

                if (script.ListAction.Count == 0)
                {
                    //--- Position
                    ActionCurve actionPosition = new ActionCurve(script, "Position", false, false, typeof(Entite), "Position");
                    script.ListAction.Add(actionPosition);
                    //---

                    //--- Rotation
                    ActionCurve actionRotation = new ActionCurve(script, "Rotation", false, false, typeof(Entite), "Rotation");
                    script.ListAction.Add(actionRotation);
                    //---

                    //--- Size
                    ActionCurve actionSize = new ActionCurve(script, "Size", false, false, typeof(Entite), "Size");
                    script.ListAction.Add(actionSize);
                    //---
                }

                foreach (ActionBase action in script.ListAction)
                {
                    if (action is ActionCurve)
                    {
                        ActionCurve actionCurve = (ActionCurve)action;

                        if (actionCurve.ActionName == "Position")
                        {
                            actionCurve.ListCurve[0].Keys.Add(new CurveKey(scriptControl.TimeLineValue, position.X));
                            actionCurve.ListCurve[1].Keys.Add(new CurveKey(scriptControl.TimeLineValue, position.Y));

                            actionCurve.ListCurve[0].ComputeTangents(CurveTangent.Smooth);
                            actionCurve.ListCurve[1].ComputeTangents(CurveTangent.Smooth);
                        }

                        if (actionCurve.ActionName == "Rotation")
                        {
                            actionCurve.ListCurve[0].Keys.Add(new CurveKey(scriptControl.TimeLineValue, rotation));

                            actionCurve.ListCurve[0].ComputeTangents(CurveTangent.Smooth);
                        }

                        if (actionCurve.ActionName == "Size")
                        {
                            actionCurve.ListCurve[0].Keys.Add(new CurveKey(scriptControl.TimeLineValue, size.X));
                            actionCurve.ListCurve[1].Keys.Add(new CurveKey(scriptControl.TimeLineValue, size.Y));
                            
                            actionCurve.ListCurve[0].ComputeTangents(CurveTangent.Smooth);
                            actionCurve.ListCurve[1].ComputeTangents(CurveTangent.Smooth);
                        }
                        actionCurve.CalcDuration();
                    }
                }

                short timeLineValueIncrement = 1000;
                if (!Int16.TryParse(txtRecTime.Text, out timeLineValueIncrement))
                    timeLineValueIncrement = 1000;

                scriptControl.TimeLineValue += timeLineValueIncrement;
                scriptControl.RefreshScriptControl();
            }
        }

        private void btnShowPhysic_Click(object sender, EventArgs e)
        {
            repository.ShowDebugMode = !repository.ShowDebugMode;

            this.btnShowDebugMode.Checked = repository.ShowDebugMode;
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            btnMove.CheckState = CheckState.Checked;
            btnResize.CheckState = CheckState.Unchecked;
            btnRotate.CheckState = CheckState.Unchecked;

            repository.mouseMode = MouseMode.Move;
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            btnMove.CheckState = CheckState.Unchecked;
            btnResize.CheckState = CheckState.Checked;
            btnRotate.CheckState = CheckState.Unchecked;

            repository.mouseMode = MouseMode.Resize;
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            btnMove.CheckState = CheckState.Unchecked;
            btnResize.CheckState = CheckState.Unchecked;
            btnRotate.CheckState = CheckState.Checked;

            repository.mouseMode = MouseMode.Rotate;
        }

        private void btnPinStatic_Click(object sender, EventArgs e)
        {
            btnPinStatic.Checked = !btnPinStatic.Checked;

            if (repository.CurrentEntite != null)
            {
                repository.CurrentEntite.IsStatic = btnPinStatic.Checked;

                if (repository.tempEntite != null)
                    repository.tempEntite.IsStatic = btnPinStatic.Checked;
            }

            for (int i = 0; i < repository.ListSelection.Count; i++)
            {
                repository.ListSelection[i].Entite.IsStatic = btnPinStatic.Checked;

                if (repository.ListSelection[i].TempEntite != null)
                    repository.ListSelection[i].TempEntite.IsStatic = btnPinStatic.Checked;
            }
        }

        private void btnColisionable_Click(object sender, EventArgs e)
        {
            btnColisionable.Checked = !btnColisionable.Checked;

            if (repository.CurrentEntite != null)
            {
                repository.CurrentEntite.IsColisionable = btnColisionable.Checked;

                if (repository.tempEntite != null)
                    repository.tempEntite.IsColisionable = btnColisionable.Checked;
            }

            for (int i = 0; i < repository.ListSelection.Count; i++)
            {
                repository.ListSelection[i].Entite.IsColisionable = btnColisionable.Checked;

                if (repository.ListSelection[i].TempEntite != null)
                    repository.ListSelection[i].TempEntite.IsColisionable = btnColisionable.Checked;
            }
        }

        private void btnResetEntityPhysic_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null)
            {
                Vector2 position = repository.CurrentEntite.Position;
                float rotation = repository.CurrentEntite.Rotation;

                repository.CurrentEntite.Body.ResetDynamics();
                repository.CurrentEntite.SetPosition(position);
                repository.CurrentEntite.Rotation = rotation;
            }
        }

        private void btnCursorToEntityCenter_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null)
            {
                Vector2 position = repository.CurrentEntite.Position;
                repository.CurrentPointer.WorldPosition = position;
                repository.CurrentPointer.CalcScreenPositionFromWorldPosition(repository.Camera);
            }
        }

        private void btnSetCenterEntity_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null)
            {
                repository.CurrentEntite.SetCenterFromWorldPosition(repository.CurrentPointer.WorldPosition, true);

                repository.tempEntite = (Entite)repository.CurrentEntite.Clone(false);

                Repository.physicSimulator.Update(0.000002f);
            }
        }

        private void btnOrderUp_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null && repository.ListSelection.Count > 0)
            {
                int entiteIndex = repository.listEntite.IndexOf(repository.CurrentEntite);
                int minIndex = repository.listEntite.Count - 1;

                for (int i = 0; i < repository.ListSelection.Count; i++)
                {
                    int index = repository.listEntite.IndexOf(repository.ListSelection[i].Entite);

                    if (index < minIndex)
                        minIndex = index;
                }

                if (entiteIndex < minIndex)
                {
                    for (int j = entiteIndex; j < minIndex - 1; j++)
                    {
                        repository.listEntite[j] = repository.listEntite[j + 1];
                    }

                    repository.listEntite[minIndex - 1] = repository.CurrentEntite;
                }
                else if (entiteIndex > minIndex)
                {
                    for (int j = entiteIndex; j >= minIndex; j--)
                    {
                        repository.listEntite[j] = repository.listEntite[j - 1];
                    }

                    repository.listEntite[minIndex] = repository.CurrentEntite;
                }

                RefreshTreeView();
            }
        }

        private void btnOrderDown_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null && repository.ListSelection.Count > 0)
            {
                int entiteIndex = repository.listEntite.IndexOf(repository.CurrentEntite);
                int maxIndex = 0;

                for (int i = 0; i < repository.ListSelection.Count; i++)
                {
                    int index = repository.listEntite.IndexOf(repository.ListSelection[i].Entite);

                    if (index > maxIndex)
                        maxIndex = index;
                }

                if (entiteIndex < maxIndex)
                {
                    for (int j = entiteIndex; j < maxIndex + 1; j++)
                    {
                        repository.listEntite[j] = repository.listEntite[j + 1];
                    }

                    repository.listEntite[maxIndex] = repository.CurrentEntite;
                }
                else if (entiteIndex > maxIndex)
                {
                    for (int j = entiteIndex; j > maxIndex; j--)
                    {
                        repository.listEntite[j] = repository.listEntite[j - 1];
                    }

                    repository.listEntite[maxIndex + 1] = repository.CurrentEntite;
                }

                RefreshTreeView();
            }
        }

        private void btnScriptModeBar_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                ShowScriptMode();
        }

        private void btnTriggerModeBar_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                ShowTriggerMode();
        }

        private void btnParticleSystemModeBar_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                ShowParticleSystemMode();
        }

        private void toolStripTextBoxEntityName_Validated(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null)
            {
                if (ChangeEntityName(repository.CurrentEntite, toolStripTextBoxEntityName.Text, repository.CurrentEntite.Name))
                {
                    this.prop.Refresh();
                    RefreshTreeView();
                }
            }
        }

        private void btnGameClickableOnPlay_Click(object sender, EventArgs e)
        {
            repository.IsEntityClickableOnPlay = btnGameClickableOnPlay.Checked;
        }

        private void btnPanelBottom_Click(object sender, EventArgs e)
        {
            pnlViewerModes.Panel2Collapsed = !btnPanelBottom.Checked;
        }

        private void btnPanelRight_Click(object sender, EventArgs e)
        {
            pnlMain.Panel2Collapsed = !btnPanelRight.Checked;
        }
        #endregion

        #region Events
        void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            System.Drawing.Rectangle rec = new System.Drawing.Rectangle();
            rec.Location = new System.Drawing.Point(e.Bounds.X + 10, e.Bounds.Y + 10);
            rec.Size = new Size(50, 50);

            System.Drawing.Rectangle recBackground = new System.Drawing.Rectangle();
            recBackground.Location = new System.Drawing.Point(e.Bounds.X + 5, e.Bounds.Y);
            recBackground.Size = new Size(e.Bounds.Width - 20 - listView.Margin.Horizontal, e.Bounds.Height);

            System.Drawing.Point loc = new System.Drawing.Point();

            loc.X = e.Bounds.X + 65;
            loc.Y = e.Bounds.Y + 30;

            if ((int)e.State == 17 || (listView.SelectedItems.Count > 0 && e.Item == listView.SelectedItems[0]))
            {
                e.Graphics.FillRectangle(Brushes.CornflowerBlue, recBackground);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.DarkGray, recBackground);
            }

            e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageKey], rec);
            e.Graphics.DrawString(e.Item.Text, listView.Font, Brushes.Black, loc);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                repository.CurrentTextureName = listView.SelectedItems[0].Text;
                listView.Refresh();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(render != null)
                render.Update();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //---> Si la touche Alt (MouseMode) vient d'être préssée
            //     les entités sélectionnées sont clonées
            //if (e.Alt && !repository.keyAltPressed)
            //{
            //    CloneSelectedEntite();
            //}

            if (e.Control)
                repository.keyCtrlPressed = true;
            if (e.Alt)
                repository.keyAltPressed = true;
            if (e.Shift)
                repository.keyShiftPressed = true;

            //--- Change le curseur selon le MouseMode sélectionné
            if (repository.keyAltPressed)
            {
                modelViewerControl.Cursor = dicCursors[repository.mouseMode];
            }
            else
            {
                modelViewerControl.Cursor = dicCursors[MouseMode.Select];
            }
            //---
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (modelViewerControl.Focused)
            {
                if (!e.Control && repository.keyCtrlPressed)
                    repository.keyCtrlPressed = false;
                if (!e.Alt && repository.keyAltPressed)
                    repository.keyAltPressed = false;
                if (!e.Shift && repository.keyShiftPressed)
                    repository.keyShiftPressed = false;

                //--- Change le curseur selon le MouseMode sélectionné
                if (repository.keyAltPressed)
                {
                    modelViewerControl.Cursor = dicCursors[repository.mouseMode];
                }
                else
                {
                    modelViewerControl.Cursor = dicCursors[MouseMode.Select];
                }
                //---

                if (e.KeyCode == Keys.S && repository.CurrentEntite != null)
                {
                    btnPinStatic.PerformClick();
                }

                if (e.KeyCode == Keys.Oem7)
                {
                    if (repository.mouseMode == MouseMode.Move)
                    {
                        btnRotate.PerformClick();
                    }
                    else if (repository.mouseMode == MouseMode.Resize)
                    {
                        btnMove.PerformClick();
                    }
                    else if (repository.mouseMode == MouseMode.Rotate)
                    {
                        btnResize.PerformClick();
                    }
                }

                if (e.KeyCode == Keys.D1)
                {
                    if (repository.mouseMode == MouseMode.Move)
                    {
                        btnResize.PerformClick();
                    }
                    else if (repository.mouseMode == MouseMode.Resize)
                    {
                        btnRotate.PerformClick();
                    }
                    else if (repository.mouseMode == MouseMode.Rotate)
                    {
                        btnMove.PerformClick();
                    }
                }

                if (e.KeyCode == Keys.A)
                {
                    btnAddEntity.PerformClick();
                }

                if (e.KeyCode == Keys.Delete)
                {
                    DeleteEntity();
                }

                if (e.KeyCode == Keys.Space)
                {
                    if (repository.Pause)
                        btnPlay.PerformClick();
                    else
                        btnPause.PerformClick();
                }

                //--- Incrémente le layer des entités sélectionnées
                if (e.KeyCode == Keys.Add)
                {
                    repository.GetSelectedEntite().ForEach(ent => ent.Layer += 1);
                    repository.OrderEntite();
                    RefreshTreeView();
                }

                //---> Décrémente le layer des entités sélectionnées
                if (e.KeyCode == Keys.Subtract)
                {
                    repository.GetSelectedEntite().ForEach(ent => ent.Layer -= 1);
                    repository.OrderEntite();
                    RefreshTreeView();
                }
            }
        }

        private void prop_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.prop.SelectedObject != null && this.prop.SelectedObject is Entite)
            {
                if (e.ChangedItem.Label == "Name")
                {
                    if (!ChangeEntityName(((Entite)this.prop.SelectedObject), e.ChangedItem.Value.ToString(), e.OldValue.ToString()))
                    {
                        this.prop.Refresh();
                    }
                }

            }

            RefreshTreeView();
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Tag is Entite)
            {
                EntiteSelectionChange(false, repository.CurrentEntite, e.Node.Tag);
            }
            else if (e.Node != null && e.Node.Tag is ParticleSystem)
            {
                EntiteSelectionChange(false, repository.CurrentEntite, e.Node.Tag);
                //repository.CurrentObject = e.Node.Tag;
            }
            else if (e.Node != null && e.Node.Tag is ITriggerHandler)
            {
                EntiteSelectionChange(false, repository.CurrentEntite, e.Node.Tag);
            }
            //else if (e.Node != null && e.Node.Tag is World)
            //{
            //    EntiteSelectionChange(false, repository.CurrentEntite, e.Node.Tag);
            //}
        }

        private void btnUpEntity_Click(object sender, EventArgs e)
        {
            int index = repository.listEntite.IndexOf(repository.CurrentEntite);

            if (index > 0)
            {
                Entite entite = repository.listEntite[index - 1];
                repository.listEntite[index - 1] = repository.CurrentEntite;
                repository.listEntite[index] = entite;

                RefreshTreeView();
            }
        }

        private void btnDownEntity_Click(object sender, EventArgs e)
        {
            int index = repository.listEntite.IndexOf(repository.CurrentEntite);

            if (index < repository.listEntite.Count - 1)
            {
                Entite entite = repository.listEntite[index + 1];
                repository.listEntite[index + 1] = repository.CurrentEntite;
                repository.listEntite[index] = entite;

                RefreshTreeView();
            }
        }

        private void listView_Resize(object sender, EventArgs e)
        {
            listView.TileSize = new Size(listView.Width, 80);
        }
        #endregion

        #region Entity events
        private void btnAddEntity_Click(object sender, EventArgs e)
        {
            AddEntity();

            RefreshTreeView();
        }

        private void btnDelEntity_Click(object sender, EventArgs e)
        {
            DeleteEntity();
        }
        #endregion

        #region Spring events
        private void btnFixedLinearSpring_Click(object sender, EventArgs e)
        {
            AddFixedLinearSpring();

            RefreshTreeView();
        }

        private void btnLinearSpring_Click(object sender, EventArgs e)
        {
            AddLinearSpring();

            RefreshTreeView();
        }

        private void btnFixedAngleSpring_Click(object sender, EventArgs e)
        {
            AddFixedAngleSpring();

            RefreshTreeView();
        }

        private void btnAngleSpring_Click(object sender, EventArgs e)
        {
            AddAngleSpring();

            RefreshTreeView();
        }
        #endregion

        #region Joint events
        private void btnPinJoint_Click(object sender, EventArgs e)
        {
            AddPinJoint();
        }

        private void btnRevoluteJoint_Click(object sender, EventArgs e)
        {
            AddRevoluteJoint();
        }

        private void btnFixedRevoluteJoint_Click(object sender, EventArgs e)
        {
            AddFixedRevoluteJoint();
        }
        #endregion

        private Vector2 prevVecFocal = Vector2.Zero;

        #region ModelViewerControl events
        void modelViewerControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (!repository.Pause && !btnGameClickableOnPlay.Checked)
                return;

            modelViewerControl.Focus();

            //---> Positionne la caméra
            if (e.Button == MouseButtons.Middle)
            {
                //repository.CurrentPointer.CalcMousePointerLocation(e.Location, repository.Camera);
                //repository.CurrentPointer.SaveState();
                pointerCamera.CalcMousePointerLocation(e.Location, repository.Camera);
                pointerCamera.SaveState();

                prevVecFocal = vecFocal;
                prevPosCamera = repository.Camera.Position;
                return;
            }

            if (repository.keyCtrlPressed)
            {
                repository.CurrentPointer2.CalcMousePointerLocation(e.Location, repository.Camera);
            }
            else if (repository.keyShiftPressed && btnParticleSystemModeBar.Checked && repository.CurrentEntite != null && repository.CurrentEntite.ListParticleSystem.Count > 0)
            {
                CloneSelectedEntite(false);
                repository.CurrentPointer2.CalcMousePointerLocation(e.Location, repository.Camera);
                repository.CurrentPointer2.SaveState();
            }
            else
            {
                //--- Si la touche MouseMode est pressée et que des entités sont sélectionnées
                if (repository.keyAltPressed && (repository.CurrentEntite != null || repository.ListSelection.Count > 0))
                {
                    //---> Clone les entités avant la redéfinition de leur centrer
                    CloneSelectedEntite(true);

                    //---> Si le MouseMode courant est Rotate ou scale et qu'il y'a sélection multiple
                    //     Placer le centre des entités sur le curseur
                    if ((repository.mouseMode == MouseMode.Rotate || repository.mouseMode == MouseMode.Resize) && repository.ListSelection.Count > 0)
                    {
                        repository.GetSelectedEntite().ForEach(ent => ent.SetCenterFromWorldPosition(repository.CurrentPointer2.WorldPosition, true));
                    }

                    CloneSelectedEntite(false);
                }
                //---

                repository.CurrentPointer.CalcMousePointerLocation(e.Location, repository.Camera);

                //---> enregistre la position du pointeur clické
                repository.CurrentPointer.SaveState();
                //---
            }
        }

        private void modelViewerControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (!repository.Pause && !btnGameClickableOnPlay.Checked)
                return;

            if (e.Button == MouseButtons.Middle)
                return;

            if (repository.keyCtrlPressed)
            {
                Pointer pointer = new Pointer();
                pointer.ScreenPosition = Vector2.Zero;
                pointer.CalcMousePointerLocation(e.Location, repository.Camera);

                Entite selectedEntite = repository.GetSelectedEntiteFromLocation(pointer.WorldPosition);

                //---> La multisélection est possible uniquement sur les entités
                if (selectedEntite == null)
                    return;

                //---> Supprime la sélection si la position cliquée est à moins de 10 pixels
                Selection existSelection = repository.ListSelection.Find(s => Vector2.Distance(s.Pointer.ScreenPosition, pointer.ScreenPosition) <= 20);
                //---> Repositionne le pointeur si l'entité est déja sélectionnée
                Selection entiteSelection = repository.ListSelection.Find(s => s.Entite == selectedEntite);

                if (existSelection != null)
                {
                    if (existSelection.Entite != null)
                        existSelection.Entite.Selected = false;

                    repository.ListSelection.Remove(existSelection);
                }
                else if (entiteSelection != null)
                {
                    entiteSelection.Pointer.WorldPosition = pointer.WorldPosition;
                    entiteSelection.Pointer.ScreenPosition = pointer.ScreenPosition;
                }
                else
                {
                    if (selectedEntite != null)
                        selectedEntite.Selected = true;

                    Selection newSelection = new Selection(selectedEntite, pointer.WorldPosition, pointer.ScreenPosition);
                    repository.ListSelection.Add(newSelection);
                }

                //--- Affecte les entités sélectionnées au property grid
                prop.SelectedObjects = repository.GetSelectedEntite().ToArray();
                //---
            }
            else
            {
                //--- Repositionne le curseur lors du click gauche
                if (e.Button == MouseButtons.Left)
                {
                    repository.CurrentPointer.CalcMousePointerLocation(e.Location, repository.Camera);

                    vecFocal = new Vector2(e.Location.X, e.Location.Y);
                    vecOldCorner = repository.Camera.NewCorner;
                    oldZoom = repository.Camera.Zoom;
                }
                //---

                //--- Redonne le statut Static à l'entité courante
                if (repository.CurrentEntite != null)
                {
                    if (repository.tempEntite != null)
                    {
                        repository.CurrentEntite.Body.IsStatic = repository.tempEntite.IsStatic;
                    }
                }
                //---

                //--- Si il y'a sélection multiple et que le MouseMode est Resize ou Rotate
                //    Redéfinir le centre des entités
                if (repository.ListSelection.Count > 0 && (repository.mouseMode == MouseMode.Resize || repository.mouseMode == MouseMode.Rotate) && clonedSelectedEntite.Count>0)
                {
                    for (int i = 0; i < repository.ListSelection.Count; i++)
                    {
                        repository.ListSelection[i].Entite.SetNewCenter(clonedSelectedEntite[i].Center - repository.ListSelection[i].Entite.Center, true);
                    }

                    if (repository.CurrentEntite != null)
                    {
                        repository.CurrentEntite.SetNewCenter(clonedSelectedEntite.Last().Center - repository.CurrentEntite.Center, true);
                    }

                    clonedSelectedEntite = new List<Entite>();
                }
                //---

                //--- Si la touche MouseMode n'est pas pressée, change l'entité courante
                if (!repository.keyAltPressed)
                {
                    //--- Si la touche Ctrl n'est pas pressée et que la touche MouseMode (Alt) ne
                    //    l'est pas non plus, vide la liste de multisélection
                    repository.ListSelection.ForEach(s => s.Entite.Selected = false);
                    repository.ListSelection = new List<Selection>();
                    //---

                    Entite selectedEntite = repository.GetSelectedEntiteFromLocation(repository.CurrentPointer.WorldPosition);

                    if (selectedEntite != null)
                        EntiteSelectionChange(repository.CurrentEntite, repository.GetSelectedEntiteFromLocation(repository.CurrentPointer.WorldPosition));
                }
                //--- Si la touche MouseMode est pressée, réinjecte les nouvelles valeurs des propriétés (Size, Rotation)
                else
                {
                    //TODO : mettrer cela en place pour la mutli sélection
                    if (repository.CurrentEntite != null && repository.tempEntite != null)
                    {
                        if (repository.mouseMode == MouseMode.Move)
                        {
                            for (int i = 0; i < repository.CurrentEntite.ListFixedRevoluteJoint.Count; i++)
                            {
                                repository.CurrentEntite.ListFixedRevoluteJoint[i].Anchor = repository.CurrentEntite.ListFixedRevoluteJoint[i].Anchor + repository.CurrentEntite.Position - repository.tempEntite.Position;
                            }
                        }

                        if (repository.mouseMode == MouseMode.Resize)
                        {
                            Entite newEntite = repository.ChangeEntitySize(repository.CurrentEntite, repository.tempEntite.Size);
                            EntiteSelectionChange(repository.CurrentEntite, newEntite);

                            Repository.physicSimulator.Update(0.0000001f);
                        }

                        repository.tempEntite = null;
                    }

                    //---> Si la touche Alt (MouseMode) est préssée alors que la souris est relâchée
                    //     alors il faut clôner les entités sélectionnées
                    CloneSelectedEntite(false);
                    //prevPos = repository.CurrentPointer;
                    repository.CurrentPointer.SaveState();
                }
                //---
            }
        }

        private void modelViewerControl_MouseMove(object sender, MouseEventArgs e)
        {
            //--- Affichage de la position de la souris
            pointer.CalcMousePointerLocation(e.Location, repository.Camera);
            toolStripStatusMouse.Text = String.Format("Mouse.X : {0} - Mouse.Y : {1}", pointer.WorldPosition.X, pointer.WorldPosition.Y);
            //---

            if (!repository.Pause && !btnGameClickableOnPlay.Checked)
                return;

            if (e.Button == MouseButtons.Middle)
            {
                pointerCamera.CalcMousePointerLocation(e.Location, repository.Camera);
                repository.Camera.Position = prevPosCamera + pointerCamera.ScreenPosition - pointerCamera.PrevScreenPosition;

                //--- Calcul de la nouvelle position des pointeurs à l'écran
                for (int i = 0; i < repository.ListSelection.Count; i++)
                {
                    repository.ListSelection[i].Pointer.CalcScreenPositionFromWorldPosition(repository.Camera);
                }

                vecFocal = prevVecFocal + pointerCamera.ScreenPosition - pointerCamera.PrevScreenPosition;
 
                repository.CurrentPointer.CalcScreenPositionFromWorldPosition(repository.Camera);
                repository.CurrentPointer2.CalcScreenPositionFromWorldPosition(repository.Camera);
                //----
            }
            else if (e.Button == MouseButtons.Left)
            {
                //---> Modification des propriétés du système de particules
                if (repository.keyShiftPressed)
                {
                    repository.CurrentPointer2.CalcMousePointerLocation(e.Location, repository.Camera);

                    if (btnParticleSystemModeBar.Checked)
                    {
                        if (repository.CurrentEntite != null && repository.CurrentEntite.ListParticleSystem.Count > 0)
                        {
                            Vector2 vec1 =  repository.CurrentPointer2.PrevWorldPosition - repository.CurrentEntite.Position;
                            Vector2 vec2 = repository.CurrentPointer2.WorldPosition  - repository.CurrentEntite.Position;

                            vec1.Normalize();
                            vec2.Normalize();
                            float angle = vec1.GetAngle(vec2);

                            //TODO : ajouter un prevpos2 lors du MouseDown pour le pointer2
                            repository.CurrentEntite.ListParticleSystem[0].EmmittingAngle = repository.tempEntite.ListParticleSystem[0].EmmittingAngle + angle;
                        }
                    }
                }
                //---> Modification de la position du second pointeur (vert)
                else if (repository.keyCtrlPressed)
                {
                    repository.CurrentPointer2.CalcMousePointerLocation(e.Location, repository.Camera);
                }
                else
                {
                    repository.CurrentPointer.CalcMousePointerLocation(e.Location, repository.Camera);

                    //--- MouseMode.Move
                    if ((repository.CurrentEntite != null || repository.ListSelection.Count > 0) &&
                         repository.keyAltPressed &&
                         repository.mouseMode == MouseMode.Move)
                    {
                        Vector2 deltaPosition = repository.CurrentPointer.WorldPosition - repository.CurrentPointer.PrevWorldPosition;

                        if (repository.CurrentEntite != null)
                            repository.CurrentEntite.FixPosition(repository.tempEntite.Position + deltaPosition);

                        for (int i = 0; i < repository.ListSelection.Count; i++)
                        {
                            repository.ListSelection[i].Entite.FixPosition(repository.ListSelection[i].TempEntite.Position + deltaPosition);
                            repository.ListSelection[i].Pointer.WorldPosition = repository.ListSelection[i].Pointer.PrevWorldPosition + deltaPosition;

                            repository.ListSelection[i].Pointer.CalcScreenPositionFromWorldPosition(repository.Camera);
                        }
                    }
                    //---

                    //--- MouseMode.Resize
                    #region Resize
                    if (repository.CurrentEntite != null && repository.tempEntite != null && repository.keyAltPressed && repository.mouseMode == MouseMode.Resize)
                    {
                        float width = 0;
                        float height = 0;

                        //--- Calcul des positions absolues des 4 points du rectangle
                        float w = (float)repository.tempEntite.Size.Width / 2f;
                        float h = (float)repository.tempEntite.Size.Height / 2f;
                        float r = (float)Math.Sqrt(w * w + h * h);

                        float angle = 0f;
                        float angleFinalPoint = 0f;

                        angle = (float)Math.Acos(w / r);

                        angleFinalPoint = repository.tempEntite.Rotation + angle + MathHelper.Pi;
                        Vector2 vec11 = repository.tempEntite.Position - 0 * repository.tempEntite.Center + new Vector2(r * (float)Math.Cos(angleFinalPoint), r * (float)Math.Sin(angleFinalPoint));

                        angleFinalPoint = repository.tempEntite.Body.Rotation - angle;
                        Vector2 vec12 = repository.tempEntite.Position - 0 * repository.tempEntite.Center + new Vector2(r * (float)Math.Cos(angleFinalPoint), r * (float)Math.Sin(angleFinalPoint));

                        angleFinalPoint = repository.tempEntite.Body.Rotation + angle;
                        Vector2 vec21 = repository.tempEntite.Position - 0 * repository.tempEntite.Center + new Vector2(r * (float)Math.Cos(angleFinalPoint), r * (float)Math.Sin(angleFinalPoint));

                        angleFinalPoint = repository.tempEntite.Body.Rotation - angle + MathHelper.Pi;
                        Vector2 vec22 = repository.tempEntite.Position - 0 * repository.tempEntite.Center + new Vector2(r * (float)Math.Cos(angleFinalPoint), r * (float)Math.Sin(angleFinalPoint));

                        List<Vector2> listVec = new List<Vector2>() { vec11, vec12, vec21, vec22 };
                        //---

                        //--- Détection du coin le plus en bas à gauche
                        float maxY = float.MinValue;
                        float minX = float.MaxValue;

                        int idVec = -1, idNextVec, idNextVec2, idNextVec3;

                        for (int i = 0; i < 4; i++)
                        {
                            if ((int)listVec[i].Y > (int)maxY)
                            {
                                maxY = listVec[i].Y;
                                minX = listVec[i].X;
                                idVec = i;
                            }
                            else if ((int)listVec[i].Y == (int)maxY)
                            {
                                if ((int)listVec[i].X < (int)minX)
                                {
                                    maxY = listVec[i].Y;
                                    minX = listVec[i].X;
                                    idVec = i;
                                }
                            }
                        }

                        // select point from listVec for max(point.Y) -> 
                        //---

                        idNextVec = (idVec == 0 ? 3 : idVec - 1);
                        idNextVec2 = (idNextVec == 0 ? 3 : idNextVec - 1);
                        idNextVec3 = (idNextVec2 == 0 ? 3 : idNextVec2 - 1);

                        float pctX = (float)Math.Round((listVec[idNextVec].X - listVec[idVec].X) / (listVec[idNextVec].X - listVec[idNextVec3].X), 3);
                        float pctY = (float)Math.Round((listVec[idVec].Y - listVec[idNextVec].Y) / (listVec[idVec].Y - listVec[idNextVec2].Y), 3);

                        //float lengthX = repository.CurrentPointer.X - listVec[idNextVec3].X;
                        //float lengthY = repository.CurrentPointer.Y - listVec[idNextVec2].Y;


                        float lengthX = listVec[idNextVec].X - listVec[idNextVec3].X + (repository.CurrentPointer.WorldPosition.X - repository.CurrentPointer.PrevWorldPosition.X);
                        float lengthY = listVec[idVec].Y - listVec[idNextVec2].Y + (repository.CurrentPointer.WorldPosition.Y - repository.CurrentPointer.PrevWorldPosition.Y);

                        float lengthNX = pctX * lengthX;
                        float lengthNY = pctY * lengthY;
                        float lengthNX2 = (1f - pctX) * lengthX;
                        float lengthNY2 = (1f - pctY) * lengthY;

                        float lengthN1 = (float)Math.Sqrt(lengthNX * lengthNX + lengthNY * lengthNY);
                        float lengthN2 = (float)Math.Sqrt(lengthNX2 * lengthNX2 + lengthNY2 * lengthNY2);

                        //if (idVec == 0 || idVec == 2)
                        //{
                        //    height = lengthN1;
                        //    width = lengthN2;
                        //}
                        //else
                        //{
                        //    height = lengthN2;
                        //    width = lengthN1;
                        //}

                        if (idVec == 0 || idVec == 2)
                        {
                            height = lengthN1;
                            width = lengthN2;
                        }
                        else
                        {
                            height = lengthN2;
                            width = lengthN1;
                        }

                        if (width <= 0)
                            width = 1;
                        if (height <= 0)
                            height = 1;

                        //repository.currentEntite.SetSize((int)width, (int)height);
                        repository.CurrentEntite.ChangeSize((int)width, (int)height, false);

                        repository.CurrentEntite.Position = repository.tempEntite.Position;
                        repository.CurrentEntite.Rotation = repository.tempEntite.Rotation;
                        //repository.currentEntite.ChangeSize((int)width, (int)height, repository.currentEntite.IsColisionable);


                        //Debug.Print(string.Format("Width : {0:0.00} - Height : {1:0.00}", width, height));
                        //this.Text = String.Format("pctX : {0:0.00} pctY : {1:0.00}", pctX, pctY);

                        Repository.physicSimulator.Update(0.0000001f);
                    }
                    #endregion
                    //---

                    //--- MouseMode.Rotate
                    if ((repository.CurrentEntite != null || repository.ListSelection.Count > 0) && repository.keyAltPressed && repository.mouseMode == MouseMode.Rotate)
                    {
                        List<Entite> listSelectedEntite = repository.GetSelectedEntite();

                        Vector2 vecA = Vector2.Zero;
                        Vector2 vecB = Vector2.Zero;
                        float angle = 0f;

                        for (int i = 0; i < repository.ListSelection.Count; i++)
                        {
                            vecA = repository.CurrentPointer.PrevWorldPosition - repository.ListSelection[i].Entite.Position;
                            vecB = repository.CurrentPointer.WorldPosition - repository.ListSelection[i].Entite.Position;

                            vecA.Normalize();
                            vecB.Normalize();

                            angle = vecA.GetAngle(vecB);

                            repository.ListSelection[i].Entite.Rotation = repository.ListSelection[i].TempEntite.Rotation + angle;
                        }

                        if (repository.CurrentEntite != null)
                        {
                            vecA = repository.CurrentPointer.PrevWorldPosition - repository.CurrentEntite.Position;
                            vecB = repository.CurrentPointer.WorldPosition - repository.CurrentEntite.Position;

                            vecA.Normalize();
                            vecB.Normalize();

                            angle = vecA.GetAngle(vecB);
                            this.Text = angle.ToString();
                            repository.CurrentEntite.Rotation = repository.tempEntite.Rotation + angle;
                        }
                    }
                    //---
                }
            }
        }

        void modelViewerControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!repository.Pause && !btnGameClickableOnPlay.Checked)
                return;

            if (btnParticleSystemModeBar.Checked && repository.keyShiftPressed && particleControl.GetCurrentParticleSystem() != null)
            {
                ParticleSystem pSystem = particleControl.GetCurrentParticleSystem();

                pSystem.FieldAngle += (float)e.Delta / 960f;
            }
            else
            {
                float deltaZoom = 0f;

                if (repository.Camera.Zoom <= 0.12f)
                    deltaZoom = (float)e.Delta / 10000f;
                else if (repository.Camera.Zoom >= 1.3f)
                    deltaZoom = (float)e.Delta / 1000f;
                else
                    deltaZoom = (float)e.Delta / 2000f;

                repository.Camera.Zoom += deltaZoom;

                if (repository.Camera.Zoom < 0.01f)
                    repository.Camera.Zoom = 0.01f;

                repository.Camera.Focal = vecFocal - repository.Camera.Position;
                repository.Camera.OldCorner = vecOldCorner;
                repository.Camera.OldZoom = oldZoom;

                repository.Camera.NewCorner = repository.Camera.Focal - (repository.Camera.Zoom / repository.Camera.OldZoom) * (repository.Camera.Focal - repository.Camera.OldCorner);

                //--- Calcul de la nouvelle position des pointeurs à l'écran
                for (int i = 0; i < repository.ListSelection.Count; i++)
                {
                    repository.ListSelection[i].Pointer.CalcScreenPositionFromWorldPosition(repository.Camera);
                }

                //Note : Pas besoin de recalculer la position à l'écran du pointeur principal car il est le centre du zoom
                repository.CurrentPointer2.CalcScreenPositionFromWorldPosition(repository.Camera);
                //----
            }
        }

        List<Entite> clonedSelectedEntite = new List<Entite>();

        private void CloneSelectedEntite(bool cloneLocal)
        {
            if (cloneLocal)
                clonedSelectedEntite = new List<Entite>();

            //---> Création des clones lorsque la touche de modification est pressée
            if (repository.CurrentEntite != null || repository.ListSelection.Count > 0)
            {
                for (int i = 0; i < repository.ListSelection.Count; i++)
                {
                    //---> Le body courant devient statique pour tous les MouseMode
                    if (cloneLocal)
                        clonedSelectedEntite.Add((Entite)repository.ListSelection[i].Entite.Clone(false));
                    else
                        repository.ListSelection[i].TempEntite = (Entite)repository.ListSelection[i].Entite.Clone(false);

                    repository.ListSelection[i].Pointer.SaveState();
                    repository.ListSelection[i].Entite.IsStatic = true;

                    //--- Suppression du body courant si on est en mode Resize
                    if (repository.mouseMode == MouseMode.Resize)
                    {
                        Repository.physicSimulator.Remove(repository.ListSelection[i].Entite.Body);
                        Repository.physicSimulator.Remove(repository.ListSelection[i].Entite.geom);
                    }
                    //---
                }

                //TODO : unifier la mutlisélection avec la sélection simple
                if (repository.CurrentEntite != null)
                {
                    //---> Le body courant devient statique pour tous les MouseMode
                    if (cloneLocal)
                        clonedSelectedEntite.Add((Entite)repository.CurrentEntite.Clone(false));
                    else
                        repository.tempEntite = (Entite)repository.CurrentEntite.Clone(false);

                    repository.CurrentEntite.Body.IsStatic = true;

                    //--- Suppression du body courant si on est en mode Resize
                    if (repository.mouseMode == MouseMode.Resize)
                    {
                        Repository.physicSimulator.Remove(repository.CurrentEntite.Body);
                        Repository.physicSimulator.Remove(repository.CurrentEntite.geom);
                    }
                    //---
                }
            }
        }

        private void modelViewerControl_Resize(object sender, EventArgs e)
        {
            modelViewerControl.ChangeViewPortSize();
        }
        #endregion

        #region Trigger private methods
        private void ShowScriptMode()
        {
            scriptControl.Visible = true;
            triggerControl.Visible = false;
            particleControl.Visible = false;

            btnScriptModeBar.Checked = true;
            btnTriggerModeBar.Checked = false;
            btnParticleSystemModeBar.Checked = false;

            pnlModes.SetCellPosition(scriptControl, new TableLayoutPanelCellPosition(0, 1));

            //---
            IActionHandler actionHandler = null;

            if (repository.CurrentEntite != null)
                actionHandler = repository.CurrentEntite;
            else if (repository.CurrentObject != null && repository.CurrentObject is IActionHandler)
                actionHandler = (IActionHandler)repository.CurrentObject;

            EntiteSelectionChange(true, repository.CurrentEntite, actionHandler);
            //---
        }

        private void ShowTriggerMode()
        {
            scriptControl.Visible = false;
            triggerControl.Visible = true;
            particleControl.Visible = false;

            btnScriptModeBar.Checked = false;
            btnTriggerModeBar.Checked = true;
            btnParticleSystemModeBar.Checked = false;

            pnlModes.SetCellPosition(triggerControl, new TableLayoutPanelCellPosition(0, 1));

            //---
            ITriggerHandler triggerHandler = null;

            if (repository.CurrentEntite != null)
                triggerHandler = repository.CurrentEntite;
            else if (repository.CurrentObject != null && repository.CurrentObject is ITriggerHandler)
                triggerHandler = (ITriggerHandler)repository.CurrentObject;

            EntiteSelectionChange(true, repository.CurrentEntite, triggerHandler);
            //---
        }

        private void ShowParticleSystemMode()
        {
            scriptControl.Visible = false;
            triggerControl.Visible = false;
            particleControl.Visible = true;

            btnScriptModeBar.Checked = false;
            btnTriggerModeBar.Checked = false;
            btnParticleSystemModeBar.Checked = true;

            pnlModes.SetCellPosition(particleControl, new TableLayoutPanelCellPosition(0, 1));

            EntiteSelectionChange(true, repository.CurrentEntite, repository.CurrentEntite);
        }
        #endregion
    }
}