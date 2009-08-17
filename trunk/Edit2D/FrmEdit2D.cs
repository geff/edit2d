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

namespace Edit2D
{
    public partial class FrmEdit2D : Form
    {
        #region Attributs
        Random rnd;

        Vector2 prevPos;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public Repository repository = new Repository();
        private Render render;
        //private static string path = @"D:\Geff\Log\Edit2D\Edit2D\Data\";
        //private static string path = @"D:\Log\Edit2D\Edit2D\Data\";

        //--- 3D
        ContentBuilder contentBuilder;
        ContentManager contentManager;
        //---

        //int currentScript = -1;
        //int currentAction = -1;
        //int currentSubAction = -1;


        private Vector2 prevPosCamera = Vector2.Zero;
        private Vector2 vecFocal = Vector2.Zero;
        private Vector2 vecOldCorner = Vector2.Zero;
        private float oldZoom = 1f;
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

            //Repository.physicSimulator = new FarseerGames.FarseerPhysics.PhysicsSimulator(new Vector2(0, 9.81f));

            repository = new Repository();

            InitRender();

            repository.CurrentTextureName = "BigRec";
            repository.pointer2 = new Vector2(100, 10);
            repository.pointer = new Vector2(200, 200);
            repository.FrmEdit2D = this;
            repository.pause = true;

            AddEntity();
            repository.CurrentEntite = repository.listEntite[0];

            InitListViewImage();
            RefreshTreeView();

            triggerControl.repository = repository;
            scriptControl.repository = repository;
            particleControl.repository = repository;

            particleControl.InitParticleControl();

            //btnTriggerModeBar.PerformClick();
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
            render = new Render(modelViewerControl.spriteBatch, modelViewerControl.GraphicsDevice, repository);

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

                entite.SetPosition(repository.pointer);
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
                Vector2 vec1 = repository.CurrentEntite.Body.GetLocalPosition(repository.pointer);
                Vector2 vec2 = repository.currentEntite2.Body.GetLocalPosition(repository.pointer2);

                repository.CurrentEntite.AddLinearSpring(repository.currentEntite2, vec1, vec2);

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddFixedLinearSpring()
        {
            if (repository.CurrentEntite != null)
            {
                Vector2 vec1 = repository.CurrentEntite.Body.GetLocalPosition(repository.pointer);
                Vector2 vec2 = repository.pointer2;

                repository.CurrentEntite.AddFixedLinearSpring(vec1, vec2);

                Repository.physicSimulator.Update(0.0001f);
            }
        }
        #endregion

        #region Joint
        private void AddPinJoint()
        {
            if (repository.CurrentEntite != null && repository.currentEntite2 != null)
            {
                Vector2 vec1 = repository.CurrentEntite.Body.GetLocalPosition(repository.pointer);
                Vector2 vec2 = repository.currentEntite2.Body.GetLocalPosition(repository.pointer2);

                repository.CurrentEntite.AddPinJoint(repository.currentEntite2, vec1, vec2);

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddRevoluteJoint()
        {
            if (repository.CurrentEntite != null && repository.currentEntite2 != null)
            {
                Vector2 vec1 = repository.CurrentEntite.Body.GetWorldPosition(repository.pointer);
                Vector2 vec2 = repository.currentEntite2.Body.GetLocalPosition(repository.pointer2);

                repository.CurrentEntite.AddRevoluteJoint(repository.currentEntite2, vec1);

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddFixedRevoluteJoint()
        {
            if (repository.CurrentEntite != null)
            {
                Vector2 vec1 = repository.CurrentEntite.Body.GetLocalPosition(repository.pointer);

                repository.CurrentEntite.AddFixedRevoluteJoint(repository.pointer);

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
            bool pause = repository.pause;
            string textureName = repository.CurrentTextureName;

            repository = new Repository();
            repository.mouseMode = mouseMode;

            modelViewerControl.Initialize(repository, contentManager, contentBuilder);

            InitPhysicSimulatorView();

            repository.PhysicsSimulatorView.LoadContent(modelViewerControl.GraphicsDevice, contentManager);

            repository.pointer = new Vector2(100, 80);

            repository.pause = pause;
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
            repository.pause = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            this.btnPause.Enabled = false;
            this.btnPlay.Enabled = true;

            repository.pause = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.btnPause.Enabled = false;
            this.btnPlay.Enabled = true;

            repository.pause = true;

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
                            actionCurve.ListCurve[0].Keys.Add(new CurveKey(scriptControl.TimeLineValue, repository.CurrentEntite.Position.X));
                            actionCurve.ListCurve[1].Keys.Add(new CurveKey(scriptControl.TimeLineValue, repository.CurrentEntite.Position.Y));
                        }

                        if (actionCurve.ActionName == "Rotation")
                        {
                            actionCurve.ListCurve[0].Keys.Add(new CurveKey(scriptControl.TimeLineValue, repository.CurrentEntite.Rotation));
                        }

                        if (actionCurve.ActionName == "Size")
                        {
                            actionCurve.ListCurve[0].Keys.Add(new CurveKey(scriptControl.TimeLineValue, repository.CurrentEntite.SizeVector.X));
                            actionCurve.ListCurve[1].Keys.Add(new CurveKey(scriptControl.TimeLineValue, repository.CurrentEntite.SizeVector.Y));

                        }
                        actionCurve.CalcDuration();
                    }
                }

                scriptControl.TimeLineValue += 200;
                scriptControl.RefreshScriptControl();
            }
        }

        private void btnShowPhysic_Click(object sender, EventArgs e)
        {
            repository.showPhysic = !repository.showPhysic;

            this.btnShowPhysic.Checked = repository.showPhysic;
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
            if (repository.CurrentEntite != null)
            {
                repository.CurrentEntite.IsStatic = !repository.CurrentEntite.IsStatic;

                if (repository.tempEntite != null)
                    repository.tempEntite.IsStatic = repository.CurrentEntite.IsStatic;

                btnPinStatic.Checked = repository.CurrentEntite.IsStatic;
            }
        }

        private void btnColisionable_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null)
            {
                repository.CurrentEntite.IsColisionable = !repository.CurrentEntite.IsColisionable;

                if (repository.tempEntite != null)
                    repository.tempEntite.IsColisionable = repository.CurrentEntite.IsColisionable;

                btnColisionable.Checked = repository.CurrentEntite.IsColisionable;
            }
        }

        private void btnCursorToEntityCenter_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null)
            {
                int dimPointer = 10;

                Vector2 position = repository.CurrentEntite.Position;
                repository.pointer = position;
                repository.pointerDraw = new Vector2(repository.pointer.X - dimPointer / 2, repository.pointer.Y - dimPointer / 2);
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

        private void btnSetCenterEntity_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null)
            {
                repository.CurrentEntite.SetCenterFromWorldPosition(repository.pointer2);

                Repository.physicSimulator.Update(0.000002f);
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
                //e.Graphics.FillRectangle(Brushes.CornflowerBlue, recBackground);
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
            //UpdateEntityActionPlayer();

            //if (!repository.pause)
            //{
            //    UpdateEntityTrigger();
            //    UpdateEntityParticleSystem();

            //    UpdatePhysic();
            //}
            render.Update();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                repository.keyCtrlPressed = true;
            if (e.Alt)
                repository.keyAltPressed = true;
            if (e.Shift)
                repository.keyShiftPressed = true;
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
                    if (repository.pause)
                        btnPlay.PerformClick();
                    else
                        btnPause.PerformClick();
                }
            }
        }

        private void prop_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.prop.SelectedObject != null && this.prop.SelectedObject is Entite)
            {
                if (e.ChangedItem.Label == "Name")
                {
                    //int countFound = repository.listEntite.FindAll(ent => ent.Name == e.ChangedItem.Value.ToString()).Count;

                    //string nameFound = repository.FoundNewName(((Entite)this.prop.SelectedObject).TextureName);

                    //if (countFound == 2)// && entiteFound != null && entiteFound != ((Entite)this.prop.SelectedObject))
                    //{
                    //    if (MessageBox.Show(String.Format("Le nom {0} est déja utilisé pour une entité, voulez-vous utiliser le nom {1} ?", e.ChangedItem.Value, nameFound), "Avertissement", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.OK)
                    //    {
                    //        ((Entite)this.prop.SelectedObject).Name = nameFound;
                    //    }
                    //    else
                    //    {
                    //        ((Entite)this.prop.SelectedObject).Name = e.OldValue.ToString();
                    //        this.prop.Refresh();
                    //    }
                    //}

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

        #region ModelViewerControl events
        private Vector2 GetMousePointerLocation(System.Drawing.Point location, ref Vector2 pointerDraw)
        {
            int dimPointer = 10;

            Vector3 vecLocation = new Vector3(location.X, location.Y, 0f);
            Vector3 vecLocationTransformed = Vector3.Transform(vecLocation, Matrix.Invert(repository.Camera.MatrixTransformation));

            pointerDraw = new Vector2(vecLocation.X - dimPointer / 2, vecLocation.Y - dimPointer / 2);

            return new Vector2(vecLocationTransformed.X, vecLocationTransformed.Y);
        }

        void modelViewerControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (!repository.pause && !btnGameClickableOnPlay.Checked)
                return;

            modelViewerControl.Focus();

            if (e.Button == MouseButtons.Middle)
            {
                prevPos = new Vector2(e.Location.X, e.Location.Y);
                prevPosCamera = repository.Camera.Position;
                return;
            }

            if (repository.keyCtrlPressed)
            {
                repository.pointer2 = GetMousePointerLocation(e.Location, ref repository.pointerDraw2);

                repository.currentEntite2 = repository.GetSelectedEntite(repository.pointer2);
            }
            else
            {
                repository.pointer = GetMousePointerLocation(e.Location, ref repository.pointerDraw);

                //--- Si la touche MouseMode n'est pas pressée, change l'entité courante
                if (!repository.keyAltPressed)
                {
                    //Entite selectedEntite = repository.GetSelectedEntite(repository.pointer);

                    //if (selectedEntite != null && repository.currentEntite != selectedEntite && selectedEntite.IsColisionable)
                    //    EntiteSelectionChange(repository.currentEntite, selectedEntite);

                    //if (repository.currentEntite != null)
                    //{
                    //    repository.tempEntite = (Entite)repository.currentEntite.Clone(false);
                    //    this.btnPinStatic.Checked = repository.currentEntite.IsStatic;
                    //    this.btnColisionable.Checked = repository.currentEntite.IsColisionable;


                    //}
                    //else
                    //{
                    //    repository.tempEntite = null;
                    //    this.btnPinStatic.Checked = false;
                    //    this.btnColisionable.Checked = false;
                    //}
                }
                //--- Si la touche MouseMode est pressée
                else if (repository.CurrentEntite != null)
                {
                    //---> Le body courant devient statique pour tous les MouseMode
                    repository.tempEntite = (Entite)repository.CurrentEntite.Clone(false);
                    repository.CurrentEntite.Body.IsStatic = true;

                    //--- Suppression du body courant si on est en mode Resize
                    if (repository.mouseMode == MouseMode.Resize)
                    {
                        Repository.physicSimulator.Remove(repository.CurrentEntite.Body);
                        Repository.physicSimulator.Remove(repository.CurrentEntite.geom);
                    }
                    //---

                    //---> enregistre la position du pointeur clické
                    prevPos = repository.pointer;
                    //---
                }
                //---
            }
        }

        private void modelViewerControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (!repository.pause && !btnGameClickableOnPlay.Checked)
                return;

            if (repository.keyCtrlPressed)
            {
                repository.pointer2 = GetMousePointerLocation(e.Location, ref repository.pointerDraw2);
                repository.currentEntite2 = repository.GetSelectedEntite(repository.pointer2);
            }
            else
            {
                //--- Repositionne le curseur lors du click gauche
                if (e.Button == MouseButtons.Left)
                {
                    repository.pointer = GetMousePointerLocation(e.Location, ref repository.pointerDraw);

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

                //--- Si la touche MouseMode n'est pas pressée, change l'entité courante
                if (!repository.keyAltPressed)
                {
                    //repository.currentEntite = repository.GetSelectedEntite(repository.pointer);
                    //prop.SelectedObject = repository.currentEntite;
                    Entite selectedEntite = repository.GetSelectedEntite(repository.pointer);

                    if (selectedEntite != null)//|| (selectedEntite != null && selectedEntite.IsColisionable))
                        EntiteSelectionChange(repository.CurrentEntite, repository.GetSelectedEntite(repository.pointer));
                }
                //--- Si la touche MouseMode est pressée, réinjecte les nouvelles valeurs des propriétés (Size, Rotation)
                else
                {
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
                }
                //---
            }
        }

        private void modelViewerControl_MouseMove(object sender, MouseEventArgs e)
        {
            //--- Affichage de la position de la souris
            toolStripStatusMouse.Text = String.Format("Mouse.X : {0} - Mouse.Y : {1}", e.X, e.Y);
            //---

            if (!repository.pause && !btnGameClickableOnPlay.Checked)
                return;

            int dimPointer = 10;

            if (e.Button == MouseButtons.Middle)
            {
                //repository.pointer = GetMousePointerLocation(e.Location, ref repository.pointerDraw);
                repository.Camera.Position = prevPosCamera + new Vector2(e.Location.X, e.Location.Y) - prevPos;
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (repository.keyCtrlPressed)
                {
                    repository.pointer2 = GetMousePointerLocation(e.Location, ref repository.pointerDraw2);

                    if (btnParticleSystemModeBar.Checked)
                    {
                        if (repository.CurrentEntite != null && repository.CurrentEntite.ListParticleSystem.Count > 0)
                        {
                            Vector2 vec1 = repository.CurrentEntite.Position - repository.pointer2;
                            float angle = vec1.GetAngle(Vector2.UnitX);

                            repository.CurrentEntite.ListParticleSystem[0].EmmittingAngle = -angle + MathHelper.PiOver2;
                        }
                    }
                }
                else
                {
                    //repository.pointer = new Vector2(e.Location.X, e.Location.Y);
                    //repository.pointerDraw = new Vector2(repository.pointer.X - dimPointer / 2, repository.pointer.Y - dimPointer / 2);

                    repository.pointer = GetMousePointerLocation(e.Location, ref repository.pointerDraw);

                    //--- Si la touche MouseMode n'est pas pressée, change l'entité courante
                    //if (!repository.keyAltPressed)
                    //    EntiteSelectionChange(repository.currentEntite, repository.GetSelectedEntite(repository.pointer));
                    //---

                    //--- MouseMode.Move
                    if (repository.CurrentEntite != null && repository.keyAltPressed && repository.mouseMode == MouseMode.Move && repository.tempEntite != null)
                    {
                        repository.CurrentEntite.FixPosition(repository.tempEntite.Position + repository.pointer - prevPos + new Vector2(dimPointer / 2, dimPointer / 2));

                        //Repository.physicSimulator.Update(0.000002f);
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

                        //float lengthX = repository.pointer.X - listVec[idNextVec3].X;
                        //float lengthY = repository.pointer.Y - listVec[idNextVec2].Y;


                        float lengthX = listVec[idNextVec].X - listVec[idNextVec3].X + (repository.pointer.X - prevPos.X);
                        float lengthY = listVec[idVec].Y - listVec[idNextVec2].Y + (repository.pointer.Y - prevPos.Y);

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
                    if (repository.CurrentEntite != null && repository.keyAltPressed && repository.mouseMode == MouseMode.Rotate)
                    {
                        Vector2 vecA = prevPos - repository.CurrentEntite.Position;
                        Vector2 vecB = repository.pointer - repository.CurrentEntite.Position;

                        vecA.Normalize();
                        vecB.Normalize();

                        float angle = vecA.GetAngle(vecB);

                        repository.CurrentEntite.Rotation = repository.tempEntite.Rotation + angle;
                    }
                    //---
                }
            }
        }

        void modelViewerControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!repository.pause && !btnGameClickableOnPlay.Checked)
                return;

            if (btnParticleSystemModeBar.Checked && repository.keyCtrlPressed && particleControl.GetCurrentParticleSystem() != null)
            {
                ParticleSystem pSystem = particleControl.GetCurrentParticleSystem();

                pSystem.FieldAngle += (float)e.Delta / 960f;
            }
            else
            {
                repository.Camera.Zoom += (float)e.Delta / 2000f;

                repository.Camera.Focal = vecFocal - repository.Camera.Position;
                repository.Camera.OldCorner = vecOldCorner;
                repository.Camera.OldZoom = oldZoom;

                repository.Camera.NewCorner = repository.Camera.Focal - (repository.Camera.Zoom / repository.Camera.OldZoom) * (repository.Camera.Focal - repository.Camera.OldCorner);
            }
        }

        private void modelViewerControl_Resize(object sender, EventArgs e)
        {
            modelViewerControl.ChangeViewPortSize();
        }
        #endregion

        #region Script private methods
        //private void RefreshScriptView()
        //{
        //    listboxScript.Items.Clear();

        //    if (repository.currentEntite != null)
        //    {
        //        for (int i = 0; i < repository.currentEntite.ListScript.Count; i++)
        //        {
        //            listboxScript.Items.Add(repository.currentEntite.ListScript[i].ScriptName);
        //        }
        //    }
        //}

        //private void RefreshActionView()
        //{
        //    treeViewAction.Nodes.Clear();

        //    if (repository.currentEntite != null && currentScript != -1)
        //    {
        //        Script script = repository.currentEntite.ListScript[currentScript];

        //        for (int i = 0; i < script.ListAction.Count; i++)
        //        {
        //            ActionBase action = script.ListAction[i];

        //            if (action is ActionCurve)
        //            {
        //                ActionCurve actionCurve = (ActionCurve)action;

        //                switch (actionCurve.PropertyType.Name)
        //                {
        //                    case "Vector2":
        //                        TreeNode nodeVector2 = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})({3})", action.ActionName, actionCurve.PropertyName, "Curve" + (actionCurve.IsLoop ? "(loop)" : ""), actionCurve.IsRelative ? "R" : "A"));
        //                        nodeVector2.Nodes.Add("X", "X");
        //                        nodeVector2.Nodes.Add("Y", "Y");
        //                        break;
        //                    case "Single":
        //                        TreeNode nodeAngle = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})({3})", action.ActionName, actionCurve.PropertyName, "Curve" + (actionCurve.IsLoop ? "(loop)" : ""), actionCurve.IsRelative ? "R" : "A"));
        //                        break;
        //                    case "Color":
        //                        TreeNode nodeColor = treeViewAction.Nodes.Add(String.Format("{0}-Color ({1})({2})", action.ActionName, actionCurve.PropertyName, "Curve" + (actionCurve.IsLoop ? "(loop)" : ""), actionCurve.IsRelative ? "R" : "A"));
        //                        nodeColor.Nodes.Add("R", "R");
        //                        nodeColor.Nodes.Add("G", "G");
        //                        nodeColor.Nodes.Add("B", "B");
        //                        nodeColor.Nodes.Add("A", "A");
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }
        //            else if (action is ActionEvent)
        //            {
        //                ActionEvent actionEvent = (ActionEvent)action;

        //                switch (actionEvent.PropertyType.Name)
        //                {
        //                    case "Vector2":
        //                        TreeNode nodeVector2 = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})({3})", action.ActionName, actionEvent.PropertyName, "Event", actionEvent.IsRelative ? "R" : "A"));
        //                        break;
        //                    case "Single":
        //                        TreeNode nodeAngle = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})({3})", action.ActionName, actionEvent.PropertyName, "Event", actionEvent.IsRelative ? "R" : "A"));
        //                        break;
        //                    case "Color":
        //                        TreeNode nodeColor = treeViewAction.Nodes.Add(String.Format("{0}-Color ({1})({2})", action.ActionName, actionEvent.PropertyName, "Event", actionEvent.IsRelative ? "R" : "A"));
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }
        //        }
        //    }

        //}

        //private void ViewActionCurve(ActionCurve actionCurve)
        //{
        //    //--- Affiche le contrôle de courbe
        //    pnlMain.ColumnStyles[3].SizeType = SizeType.Percent;
        //    pnlMain.ColumnStyles[2].SizeType = SizeType.Absolute;
        //    pnlMain.ColumnStyles[3].Width = 100;
        //    pnlMain.ColumnStyles[2].Width = 0;

        //    pnlMain.ColumnStyles[1].SizeType = SizeType.Absolute;
        //    pnlMain.ColumnStyles[1].Width = 240;
        //    //---

        //    curveControl.Curves.Clear();

        //    if (actionCurve == null)
        //    {
        //        curveControl.TimeLine = 0;
        //        curveControl.Frame(0, -100, 1000, 100);
        //    }
        //    else
        //    {
        //        //--- Affiche les caractéristiques de l'action
        //        chkCurve.Checked = true;
        //        chkLoop.Checked = actionCurve.IsLoop;
        //        optRelative.Checked = actionCurve.IsRelative;
        //        cmbActionType.SelectedText = actionCurve.PropertyName;
        //        //---

        //        if (currentAction != -1)
        //        {
        //            if (currentSubAction != -1)
        //            {
        //                ViewActionCurve(actionCurve, currentSubAction);
        //            }
        //            else
        //            {
        //                for (int i = 0; i < actionCurve.ListCurve.Count; i++)
        //                {
        //                    ViewActionCurve(actionCurve, i);
        //                }
        //            }
        //        }

        //        //--- Change la repère selon le type de courbe
        //        float minX = 0f;
        //        float minY = 0f;
        //        float maxX = 0f;
        //        float maxY = 0f;

        //        maxX = Math.Max(actionCurve.Duration, 1000f); ;

        //        switch (actionCurve.PropertyType.Name)
        //        {
        //            case "Vector2":
        //                minY = -150f;
        //                maxY = 150f;
        //                break;
        //            case "Single":
        //                minY = -MathHelper.TwoPi - 0.1f;
        //                maxY = MathHelper.TwoPi + 0.1f;
        //                break;
        //            case "Color":
        //                minY = 0f;
        //                maxY = 256f;
        //                break;
        //            //case ActionType.BlurFactor:
        //            //    minY = 0f;
        //            //    maxY = 0.1f;
        //            //    break;
        //            default:
        //                break;
        //        }

        //        curveControl.Frame(minX, minY, maxX, maxY);
        //        curveControl.TimeLine = actionCurve.timeLineAnimation;
        //        //---
        //    }
        //}

        //private void ViewActionCurve(ActionCurve actionCurve, int subCurve)
        //{
        //    //--- Calcul la couleur de la courbe à afficher
        //    System.Drawing.Color color = System.Drawing.Color.Blue;

        //    switch (actionCurve.PropertyType.Name)
        //    {
        //        case "Vector2":
        //            if (subCurve == 0)
        //                color = System.Drawing.Color.Red;
        //            else
        //                color = System.Drawing.Color.Blue;
        //            break;
        //        case "Single":
        //            color = System.Drawing.Color.Blue;
        //            break;
        //        case "Color":
        //            if (subCurve == 0)
        //                color = System.Drawing.Color.Red;
        //            else if (subCurve == 1)
        //                color = System.Drawing.Color.Green;
        //            else if (subCurve == 2)
        //                color = System.Drawing.Color.Blue;
        //            else
        //                color = System.Drawing.Color.Violet;
        //            break;
        //        default:
        //            break;
        //    }
        //    //---

        //    //--- Curve
        //    curveControl.Curves.Add(new EditCurve(actionCurve.ActionName + subCurve.ToString(), color, actionCurve.ListCurve[subCurve], null));
        //    //---
        //}

        //private void ViewActionEvent(ActionEvent actionEvent)
        //{
        //    //--- Affiche le panneau ActionEvent
        //    pnlMain.ColumnStyles[2].SizeType = SizeType.Percent;
        //    pnlMain.ColumnStyles[3].SizeType = SizeType.Absolute;
        //    pnlMain.ColumnStyles[2].Width = 100;
        //    pnlMain.ColumnStyles[3].Width = 0;

        //    pnlMain.ColumnStyles[1].SizeType = SizeType.Absolute;
        //    pnlMain.ColumnStyles[1].Width = 240;
        //    //---

        //    lblActionEventPropertyName.Text = actionEvent.PropertyName;

        //    picActionEventColor.Visible = false;
        //    btnActionEventChooseColor.Visible = false;
        //    txtActionEventValue1.Visible = false;
        //    txtActionEventValue2.Visible = false;
        //    chkActionEvent1.Visible = false;
        //    chkActionEvent2.Visible = false;
        //    txtActionEventValue1.Enabled = true;
        //    txtActionEventValue2.Enabled = true;

        //    if (actionEvent != null)
        //    {
        //        //--- Affiche les caractéristiques de l'action
        //        chkCurve.Checked = false;
        //        optRelative.Checked = actionEvent.IsRelative;
        //        cmbActionType.SelectedText = actionEvent.PropertyName;
        //        //---

        //        if (actionEvent.PropertyType.Name == "Color")
        //        {
        //            picActionEventColor.Visible = true;
        //            btnActionEventChooseColor.Visible = true;

        //            Microsoft.Xna.Framework.Graphics.Color color = (actionEvent.Value == null ? Microsoft.Xna.Framework.Graphics.Color.White : (Microsoft.Xna.Framework.Graphics.Color)actionEvent.Value);
        //            picActionEventColor.BackColor = System.Drawing.Color.FromArgb((int)color.PackedValue);
        //        }
        //        else if (actionEvent.PropertyType.Name == "Single")
        //        {
        //            txtActionEventValue1.Visible = true;

        //            txtActionEventValue1.Mask = "0000.000";
        //            txtActionEventValue1.Text = String.Format("{0:0000.000}", actionEvent.Value);

        //            //chkActionEvent1.Visible = true;
        //            //chkActionEvent1.Checked = actionEvent.ChangeValue[0];
        //        }
        //        else if (actionEvent.PropertyType.Name == "Vector2")
        //        {
        //            txtActionEventValue1.Visible = true;
        //            txtActionEventValue2.Visible = true;

        //            txtActionEventValue1.Mask = "0000.000";
        //            txtActionEventValue1.Text = String.Format("{0:0000.000}", ((Vector2)actionEvent.Value).X);
        //            txtActionEventValue2.Mask = "0000.000";
        //            txtActionEventValue2.Text = String.Format("{0:0000.000}", ((Vector2)actionEvent.Value).Y);

        //            chkActionEvent1.Visible = true;
        //            chkActionEvent1.Checked = actionEvent.ChangeValue[0];
        //            chkActionEvent2.Visible = true;
        //            chkActionEvent2.Checked = actionEvent.ChangeValue[1];

        //            txtActionEventValue1.Enabled = chkActionEvent1.Checked;
        //            txtActionEventValue2.Enabled = chkActionEvent2.Checked;
        //        }
        //    }
        //}

        //private ActionCurve GetCurrentActionCurve()
        //{
        //    if (currentScript != -1 && currentAction != -1)
        //        return (ActionCurve)repository.currentEntite.ListScript[currentScript].ListAction[currentAction];
        //    //GetActionCurveFromScript(repository.currentEntite.ListScript[currentScript], currentCurve);
        //    else
        //        return null;
        //}

        //private ActionEvent GetCurrentActionEvent()
        //{
        //    if (currentScript != -1 && currentAction != -1)
        //        return (ActionEvent)repository.currentEntite.ListScript[currentScript].ListAction[currentAction];
        //    else
        //        return null;
        //}

        //private void UpdateCurve(ActionCurve actionCurve)
        //{
        //    //--- Met à jour les points
        //    if (currentSubAction == -1)
        //        currentSubAction = 0;

        //    {
        //        if (curveControl.Curves.Count > 0)
        //        {
        //            actionCurve.ListCurve[currentSubAction] = curveControl.Curves[0].OriginalCurve.Clone();
        //        }
        //    }
        //    //---

        //    //--- Calcul et met à jour la durée de l'animation pour chaque courbe
        //    actionCurve.CalcDuration();
        //    List<ActionBase> listActionCurve = actionCurve.Script.ListAction.FindAll(action => action is ActionCurve);
        //    for (int j = 0; j < listActionCurve.Count; j++)
        //    {
        //        float maxDuration = 0;

        //        ActionCurve actionCrv = (ActionCurve)listActionCurve[j];

        //        for (int i = 0; i < actionCrv.ListCurve.Count; i++)
        //        {
        //            if (actionCrv.ListCurve[i].Keys.Count > 0)
        //                maxDuration = Math.Max(actionCrv.ListCurve[i].Keys.Max<CurveKey>(key => key.Position), maxDuration);
        //        }

        //        actionCrv.Duration = (int)maxDuration;
        //    }

        //    //actionCurve.Script.Duration = (int)maxDuration;
        //    //---
        //}

        //private void StopAllScripts()
        //{
        //    for (int i = 0; i < repository.listEntite.Count; i++)
        //    {
        //        Entite entite = repository.listEntite[i];

        //        for (int j = 0; j < entite.ListScript.Count; j++)
        //        {
        //            for (int k = 0; k < entite.ListScript[j].ListAction.Count; k++)
        //            {
        //                ActionBase action = entite.ListScript[j].ListAction[k];

        //                if (action is ActionCurve)
        //                {
        //                    ((ActionCurve)action).StopAnimation();
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion

        #region Script events
        //private void btnAddScript_Click(object sender, EventArgs e)
        //{
        //    if (repository.currentEntite != null)
        //    {
        //        Script script = new Script(String.Format("Script{0}", repository.currentEntite.ListScript.Count + 1), repository.currentEntite);
        //        repository.currentEntite.ListScript.Add(script);

        //        RefreshScriptView();

        //        currentScript = repository.currentEntite.ListScript.Count - 1;
        //        listboxScript.SelectedIndex = listboxScript.Items.Count - 1;

        //        RefreshActionView();
        //    }
        //}

        //private void btnDelScript_Click(object sender, EventArgs e)
        //{
        //    if (repository.currentEntite != null && currentScript != -1)
        //    {
        //        repository.currentEntite.ListScript.RemoveAt(listboxScript.SelectedIndex);
        //        currentScript = -1;

        //        RefreshScriptView();
        //        RefreshActionView();
        //    }
        //}

        //private void btnPlayScript_Click(object sender, EventArgs e)
        //{
        //    if (currentScript != -1)
        //    {
        //        Script script = repository.currentEntite.ListScript[currentScript];

        //        foreach (ActionBase action in script.ListAction)
        //        {
        //            if (action is ActionCurve)
        //            {
        //                if (((ActionCurve)action).playAnimation != 0)
        //                    ((ActionCurve)action).playAnimation = 0;
        //                else
        //                {
        //                    ((ActionCurve)action).StartAnimation();
        //                }
        //            }
        //        }
        //    }
        //}

        //private void listboxScript_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    RefreshActionView();
        //    currentScript = listboxScript.SelectedIndex;
        //}

        //private void btnAddAction_Click(object sender, EventArgs e)
        //{
        //    if (repository.currentEntite != null && currentScript != -1 && cmbActionType.SelectedIndex != -1)
        //    {
        //        Script script = repository.currentEntite.ListScript[currentScript];

        //        int curveCount = script.ListAction.Count(action => action is ActionCurve) - 1;
        //        int eventCount = script.ListAction.Count(action => action is ActionEvent) - 1;

        //        //TODO : ne plus calculer le ActionType selon le SelectedIndex mais selon le SelectedValue (fait en partie)
        //        ActionBase act = null;

        //        if (chkCurve.Checked)
        //        {
        //            act = new ActionCurve(script, String.Format("Curve{0}", curveCount + 1), optRelative.Checked, chkLoop.Checked, cmbActionType.SelectedItem.ToString());
        //        }
        //        else
        //        {
        //            act = new ActionEvent(script, String.Format("Event{0}", eventCount + 1), optRelative.Checked, cmbActionType.SelectedItem.ToString());
        //        }

        //        script.ListAction.Add(act);

        //        RefreshScriptView();
        //        RefreshActionView();

        //        currentAction = curveCount;
        //        currentSubAction = 0;
        //        treeViewAction.SelectedNode = treeViewAction.Nodes[treeViewAction.Nodes.Count - 1];
        //    }
        //}

        //private void btnDelAction_Click(object sender, EventArgs e)
        //{
        //    if (repository.currentEntite != null && currentScript != -1 && currentAction != -1)
        //    {
        //        Script script = repository.currentEntite.ListScript[currentScript];

        //        script.ListAction.RemoveAt(currentAction);
        //        currentAction = -1;
        //        currentSubAction = -1;

        //        RefreshScriptView();
        //        RefreshActionView();
        //    }
        //}

        //private void btnActionUp_Click(object sender, EventArgs e)
        //{
        //    /*if (currentScript != 1 && currentCurve != -1)
        //    {
        //        repository.currentEntite.ListScript[currentScript].ListCurve.mo [currentCurve]
        //    }*/
        //}

        //private void btnActionDown_Click(object sender, EventArgs e)
        //{

        //}

        //private void btnPlayScriptAction_Click(object sender, EventArgs e)
        //{
        //    ActionCurve actionCurve = GetCurrentActionCurve();

        //    if (actionCurve == null)
        //        return;

        //    //---> Arrête l'animation si elle est en route
        //    if (actionCurve.playAnimation != 0)
        //        actionCurve.playAnimation = 0;
        //    else
        //    {
        //        actionCurve.StartAnimation();
        //    }
        //}

        //private void treeViewAction_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    ActionBase action = null;

        //    if (e.Node.Parent != null)
        //    {
        //        currentAction = e.Node.Parent.Index;
        //        currentSubAction = e.Node.Index;
        //    }
        //    else
        //    {
        //        currentAction = e.Node.Index;
        //        currentSubAction = -1;
        //    }

        //    if (currentAction != -1)
        //        action = repository.currentEntite.ListScript[currentScript].ListAction[currentAction];

        //    if (action is ActionCurve)
        //    {
        //        btnPlayScriptAction.Visible = true;
        //        ViewActionCurve((ActionCurve)action);
        //    }
        //    else if (action is ActionEvent)
        //    {
        //        btnPlayScriptAction.Visible = false;
        //        ViewActionEvent((ActionEvent)action);
        //    }
        //}

        //private void curveControl_TimeLineChange(object sender, int value)
        //{
        //    ActionCurve actionCurve = GetCurrentActionCurve();

        //    if (actionCurve != null)
        //    {
        //        if (value == -1) // StartAnimation
        //        {
        //            actionCurve.StartAnimation(true);
        //        }
        //        else
        //        {
        //            actionCurve.UpdateAnimation(value);
        //        }
        //    }
        //}

        //private void curveControl_CurveChange(object sender, EventArgs e)
        //{
        //    ActionCurve curve = GetCurrentActionCurve();

        //    if (curve != null)
        //    {
        //        UpdateCurve(curve);
        //    }
        //    else
        //        return;
        //}

        //private void btnValidateActionEventValue_Click(object sender, EventArgs e)
        //{
        //    ActionEvent actionEvent = GetCurrentActionEvent();

        //    if (actionEvent != null)
        //    {
        //        if (actionEvent.PropertyType.Name == "Color")
        //        {
        //            System.Drawing.Color color = picActionEventColor.BackColor;
        //            actionEvent.Value = new Microsoft.Xna.Framework.Graphics.Color(color.R, color.G, color.B, color.A);
        //        }
        //        else if (actionEvent.PropertyType.Name == "Single")
        //        {
        //            actionEvent.Value = float.Parse(txtActionEventValue1.Text);
        //            actionEvent.ChangeValue[0] = true;// chkActionEvent1.Checked;
        //        }
        //        else if (actionEvent.PropertyType.Name == "Vector2")
        //        {
        //            Vector2 vector2 = new Vector2();

        //            vector2.X = float.Parse(txtActionEventValue1.Text);
        //            vector2.Y = float.Parse(txtActionEventValue2.Text);
        //            actionEvent.ChangeValue[0] = chkActionEvent1.Checked;
        //            actionEvent.ChangeValue[1] = chkActionEvent2.Checked;

        //            actionEvent.Value = vector2;
        //        }
        //    }
        //}

        //private void btnActionEventChooseColor_Click(object sender, EventArgs e)
        //{
        //    ColorDialog dlg = new ColorDialog();
        //    dlg.Color = picActionEventColor.BackColor;

        //    if (dlg.ShowDialog() == DialogResult.OK)
        //    {
        //        picActionEventColor.BackColor = dlg.Color;
        //    }
        //}

        //private void chkActionEvent1_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtActionEventValue1.Enabled = chkActionEvent1.Checked;
        //}

        //private void checkBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtActionEventValue2.Enabled = chkActionEvent2.Checked;
        //}

        //private void btnModifAction_Click(object sender, EventArgs e)
        //{
        //    if (currentAction != -1)
        //    {
        //        if (repository.currentEntite.ListScript[currentScript].ListAction[currentAction] is ActionEvent)
        //        {
        //            ActionEvent actionEvent = GetCurrentActionEvent();

        //            //--- Enregistrer les paramètrers ActionEvent typiques
        //            btnValidateActionEventValue.PerformClick();
        //            //---

        //            actionEvent.IsRelative = optRelative.Checked;
        //        }
        //        else
        //        {
        //            ActionCurve actionCurve = GetCurrentActionCurve();

        //            actionCurve.IsLoop = chkLoop.Checked;
        //            actionCurve.IsRelative = optRelative.Checked;
        //        }

        //        RefreshActionView();
        //    }
        //}

        //private void chkCurve_CheckedChanged(object sender, EventArgs e)
        //{
        //    chkLoop.Visible = chkCurve.Checked;
        //}
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