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
using Edit2DEngine.Entities.Particles;
using Edit2DEngine;
using Edit2DEngine.Render;
using Edit2D.Properties;
using System.Drawing.Imaging;
using FarseerGames.FarseerPhysics.Collisions;
using Edit2DEngine.Tools;
using Microsoft.Xna.Framework.Input;
using Edit2D.UC;
using Edit2DEngine.Entities;

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

        //List<InputHandler> listInputHandler;
        #endregion

        #region Initialize
        public FrmEdit2D()
        {
            InitializeComponent();
            modelViewerControl.MouseWheel += new MouseEventHandler(modelViewerControl_MouseWheel);
            this.Load += new EventHandler(Form1_Load);
            listView.DrawItem += new DrawListViewItemEventHandler(listView_DrawItem);
            propertyGrid.PropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(PropertyGrid_PropertyValueChanged);
            propertyGrid.PropertyGrid.PropertySort = PropertySort.CategorizedAlphabetical;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void Init()
        {
            rnd = new Random();

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
            repository.CurrentPointer.WorldPosition = new Vector2(2000, 200);
            repository.FrmEdit2D = this;
            repository.Pause = true;

            repository.World.GradientColor1 = Microsoft.Xna.Framework.Graphics.Color.White;
            repository.World.GradientColor2 = Microsoft.Xna.Framework.Graphics.Color.White;
            
            //this.WindowState = FormWindowState.Maximized;

            //--- Mode simplifié
            if (repository.IsSimpleMode)
            {
                pnlViewerModes.Panel2Collapsed = false;
                pnlMain.Panel2Collapsed = false;

                pnlMain.SplitterDistance = 650;
                pnlRight.SplitterDistance = 100;

                this.Size = new Size(1000, 600);
                this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Size.Width;
                this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Size.Height;
                this.ShowIcon = false;
                modelViewerControl.ChangeViewPortSize = true;
                repository.CurrentPointer2.WorldPosition = new Vector2(10, 10);
                repository.CurrentPointer.WorldPosition = new Vector2(100, 100);
                
                //toolStripMenu.Visible = false;

                //foreach (ToolStripItem item in toolStripMenu.Items)
                //{
                //    item.Image = null;

                //    if (item.Name != "btnSetCenterEntity")
                //        item.Visible = false;
                //}
            }
            //---

            InitListViewImage();

            triggerControl.Repository = repository;
            scriptControl.Repository = repository;
            particleControl.Repository = repository;

            //--- Initialise l'arborescence de sélection
            treeView.ItemTypeShowed =
                TreeViewLocalItemType.Entity |
                TreeViewLocalItemType.CustomProperties |
                TreeViewLocalItemType.Script |
                TreeViewLocalItemType.Trigger |
                TreeViewLocalItemType.ParticleSystem |
                TreeViewLocalItemType.SubEntity;
            treeView.ItemTypeCheckBoxed =
                TreeViewLocalItemType.World |
                TreeViewLocalItemType.Entity |
                TreeViewLocalItemType.CustomProperties |
                TreeViewLocalItemType.Script |
                TreeViewLocalItemType.Trigger |
                TreeViewLocalItemType.ParticleSystem |
                TreeViewLocalItemType.SubEntity;
            treeView.AllowMultipleItemChecked = false;
            treeView.AllowUncheckedNode = false;
            treeView.Repository = repository;
            //---

            particleControl.InitParticleControl();

            btnScriptModeBar.PerformClick();

            WinformVisualStyle.ApplyStyle(this, "LightGray");
            //WinformVisualStyle.ApplyStyle(this, "AlmostDarkGrayBlue");

            AddEntity();
            repository.CurrentEntity = repository.listEntity[0];

            RefreshTreeView();

            //InitInputHandler();
        }

        /*
        #region Input Handler

        private void InitInputHandler()
        {
            //--- Input Handler
            listInputHandler = new List<InputHandler>();
            //---

            //--- Sélection des entités
            //InputHandler ihSelectEntity = new InputHandler("SelectEntity");
            //ihSelectEntity.LeftMouseButtonPressed = true;
            //ihSelectEntity.ContextCondition = new InputHandlerDelegate(IHContextCondition_SelectEntity);
            //ihSelectEntity.Actions.Add(new InputHandlerDelegate(IHActions_SelectEntity));

            //listInputHandler.Add(ihSelectEntity);
            //---

            //--- Déplacement de la caméra des entités
            InputHandler ihMoveCamera = new InputHandler("MoveCamera");
            ihMoveCamera.MiddleMouseButtonPressed = true;
            ihMoveCamera.KeysNotPressed.Add(Microsoft.Xna.Framework.Input.Keys.LeftControl);
            //ihMoveCamera.ContextCondition = new InputHandlerDelegate(IHContextCondition_MoveCamera);
            ihMoveCamera.Actions.Add(new InputHandlerDelegate(IHActions_MoveCamera));

            listInputHandler.Add(ihMoveCamera);
            //---
        }

        private bool IHContextCondition_SelectEntity(KeyboardState keyboarState, MouseState mouseState, GameTime gameTime)
        {
            return false;
        }

        private bool IHActions_SelectEntity(KeyboardState keyboarState, MouseState mouseState, GameTime gameTime)
        {
            return false;
        }

        private bool IHContextCondition_MoveCamera(KeyboardState keyboarState, MouseState mouseState, GameTime gameTime)
        {
            return true;
        }

        private bool IHActions_MoveCamera(KeyboardState keyboarState, MouseState mouseState, GameTime gameTime)
        {
            System.Drawing.Point mouseLocation = new System.Drawing.Point(mouseState.X, mouseState.Y);

            pointerCamera.CalcMousePointerLocation(mouseLocation, repository.Camera);
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

            return true;
        }

        private void EvaluateInput()
        {
            KeyboardState keyboardState = new KeyboardState();

            MouseState mouseState = Mouse.GetState();
            GameTime gameTime = new GameTime(DateTime.Now.TimeOfDay, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, false);

            for (int i = 0; i < listInputHandler.Count; i++)
            {
                listInputHandler[i].Evaluate(keyboardState, mouseState, gameTime);
            }
        }

        #endregion
        */

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
            //TextureManager.InitTextureManager(modelViewerControl.GraphicsDevice, @"..\..\..", "*.PNG");
            TextureManager.InitTextureManager(modelViewerControl.GraphicsDevice, "", "*.PNG");
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
                string name = Common.CreateNewName<Entity>(repository.listEntity, "Name", repository.CurrentTextureName + "{0}");
                //---

                Entity entity = new Entity(true, false, repository.CurrentTextureName.ToString(), name);

                entity.IsStatic = true;
                entity.SetPosition(repository.CurrentPointer.WorldPosition);
                repository.listEntity.Add(entity);

                EntitySelectionChange(repository.CurrentEntity, entity);

                Repository.physicSimulator.Update(0.0000001f);

                entity.geom.CollisionEnabled = true;
            }
        }

        private void DeleteEntity()
        {
            if (repository.CurrentEntity != null)
            {
                repository.CurrentEntity.geom.Dispose();
                repository.CurrentEntity.Body.Dispose();
                repository.listEntity.Remove(repository.CurrentEntity);

                Repository.physicSimulator.Update(0.0000001f);

                EntitySelectionChange(repository.CurrentEntity, null);
                
                RefreshTreeView();
            }
        }
        #endregion

        #region Spring
        private void AddLinearSpring()
        {
            if (repository.CurrentEntity != null && repository.currentEntity2 != null)
            {
                Vector2 vec1 = repository.CurrentEntity.Body.GetLocalPosition(repository.CurrentPointer.WorldPosition);
                Vector2 vec2 = repository.currentEntity2.Body.GetLocalPosition(repository.CurrentPointer2.WorldPosition);

                repository.CurrentEntity.AddLinearSpring(repository.currentEntity2, vec1, vec2);

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddFixedLinearSpring()
        {
            if (repository.CurrentEntity != null)
            {
                Vector2 vec1 = repository.CurrentEntity.Body.GetLocalPosition(repository.CurrentPointer.WorldPosition);
                Vector2 vec2 = repository.CurrentPointer2.WorldPosition;

                repository.CurrentEntity.AddFixedLinearSpring(vec1, vec2);

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddFixedAngleSpring()
        {
            if (repository.CurrentEntity != null)
            {
                repository.CurrentEntity.AddFixedAngleSpring();

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddAngleSpring()
        {
            if (repository.CurrentEntity != null && repository.ListSelection.Count > 0)
            {
                repository.CurrentEntity.AddAngleSpring(repository.ListSelection[0].Entity);

                Repository.physicSimulator.Update(0.0001f);
            }
        }
        #endregion

        #region Joint
        private void AddPinJoint()
        {
            if (repository.CurrentEntity != null && repository.currentEntity2 != null)
            {
                Vector2 vec1 = repository.CurrentEntity.Body.GetLocalPosition(repository.CurrentPointer.WorldPosition);
                Vector2 vec2 = repository.currentEntity2.Body.GetLocalPosition(repository.CurrentPointer2.WorldPosition);

                repository.CurrentEntity.AddPinJoint(repository.currentEntity2, vec1, vec2);

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddRevoluteJoint()
        {
            if (repository.CurrentEntity != null && repository.currentEntity2 != null)
            {
                Vector2 vec1 = repository.CurrentEntity.Body.GetWorldPosition(repository.CurrentPointer.WorldPosition);
                Vector2 vec2 = repository.currentEntity2.Body.GetLocalPosition(repository.CurrentPointer2.WorldPosition);

                repository.CurrentEntity.AddRevoluteJoint(repository.currentEntity2, vec1);

                Repository.physicSimulator.Update(0.0001f);
            }
        }

        private void AddFixedRevoluteJoint()
        {
            if (repository.CurrentEntity != null)
            {
                repository.CurrentEntity.AddFixedRevoluteJoint(repository.CurrentPointer.WorldPosition);

                Repository.physicSimulator.Update(0.0001f);
            }
        }
        #endregion

        #region Private methods
        private bool ChangeEntityName(Entity entity, string newName, string oldName)
        {
            bool nameChanged = false;
            int countFound = repository.listEntity.FindAll(ent => ent.Name == newName.ToString() && ent != entity).Count;

            string nameFound = Common.CreateNewName<Entity>(repository.listEntity, "Name", entity.TextureName + "{0}");

            if (countFound > 0)
            {
                if (MessageBox.Show(String.Format("Le nom {0} est déja utilisé pour une entité, voulez-vous utiliser le nom {1} ?", newName, nameFound), "Avertissement", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    nameChanged = true;
                    entity.Name = nameFound;
                }
                else
                {
                    nameChanged = false;
                    entity.Name = oldName;
                }
            }
            else
            {
                nameChanged = true;
                entity.Name = newName;
            }

            return nameChanged;
        }

        private void EntitySelectionChange(Entity oldEntity, Entity newEntity)
        {
            EntitySelectionChange(true, oldEntity, newEntity);
        }

        private void EntitySelectionChange(bool refreshTreeView, Entity oldEntity, Object newSelection)
        {
            repository.CurrentObject = newSelection;

            repository.CurrentEntity = null;
            repository.CurrentScript = null;
            repository.CurrentTrigger = null;
            repository.CurrentParticleSystem = null;
            repository.CurrentActionHandler = null;
            repository.CurrentTriggerHandler = null;

            if (newSelection is Particle)
            {
                repository.CurrentEntity = ((Particle)newSelection).ParticleSystem.Entity;
                repository.CurrentParticleSystem = ((Particle)newSelection).ParticleSystem;
                repository.CurrentTriggerHandler = (ITriggerHandler)newSelection;
                repository.CurrentActionHandler = (IActionHandler)newSelection;

                propertyGrid.PropertyGrid.SelectedObject = repository.CurrentParticleSystem.Entity;

                ShowParticleSystemMode();
            }
            else if (newSelection is Entity)
            {
                repository.CurrentEntity = (Entity)newSelection;
                repository.CurrentTriggerHandler = (ITriggerHandler)newSelection;
                repository.CurrentActionHandler = (IActionHandler)newSelection;

                propertyGrid.PropertyGrid.SelectedObject = repository.CurrentEntity;

                switch (repository.ViewingMode)
                {
                    case ViewingMode.Nothing :
                        break;
                    case ViewingMode.Script:
                        scriptControl.RefreshScriptControl(true);
                        break;
                    case ViewingMode.Trigger:
                        triggerControl.RefreshTriggerList(true);
                        break;
                    case ViewingMode.ParticleSystem:
                        particleControl.RefreshParticleControl(true);
                        break;
                    default:
                        break;
                }
            }
            else if (newSelection is Script)
            {
                repository.CurrentScript = (Script)newSelection;
                repository.CurrentActionHandler = ((Script)newSelection).ActionHandler;

                if (repository.CurrentActionHandler is ITriggerHandler)
                    repository.CurrentTriggerHandler = (ITriggerHandler)repository.CurrentActionHandler;

                if(repository.CurrentActionHandler is Entity &&
                    !repository.CurrentActionHandler.GetType().IsSubclassOf(typeof(Entity)))
                    propertyGrid.PropertyGrid.SelectedObject = repository.CurrentActionHandler;

                if (repository.CurrentTriggerHandler is Entity)
                    repository.CurrentEntity = (Entity)repository.CurrentTriggerHandler;

                ShowScriptMode();
            }
            else if (newSelection is TriggerBase)
            {
                repository.CurrentTrigger = (TriggerBase)newSelection;
                repository.CurrentTriggerHandler = ((TriggerBase)newSelection).TriggerHandler;

                if (repository.CurrentTriggerHandler is IActionHandler)
                    repository.CurrentActionHandler = (IActionHandler)repository.CurrentTriggerHandler;

                if ((repository.CurrentTriggerHandler is Entity &&
                    !repository.CurrentTriggerHandler.GetType().IsSubclassOf(typeof(Entity))) ||
                    repository.CurrentTriggerHandler is World)
                    propertyGrid.PropertyGrid.SelectedObject = repository.CurrentTriggerHandler;

                if (repository.CurrentTriggerHandler is Entity)
                    repository.CurrentEntity = (Entity)repository.CurrentTriggerHandler;

                ShowTriggerMode();
            }
            else if (newSelection is ParticleSystem)
            {
                repository.CurrentEntity = ((ParticleSystem)newSelection).Entity;
                repository.CurrentParticleSystem = (ParticleSystem)newSelection;
                repository.CurrentActionHandler = (IActionHandler)newSelection;

                propertyGrid.PropertyGrid.SelectedObject = repository.CurrentParticleSystem.Entity;

                ShowParticleSystemMode();
            }
            else if (newSelection is World)
            {
                repository.CurrentTriggerHandler = (ITriggerHandler)newSelection;

                propertyGrid.PropertyGrid.SelectedObject = repository.World;

                ShowTriggerMode();
            }

            EnableMode(
                repository.CurrentActionHandler != null, 
                repository.CurrentTriggerHandler != null, 
                repository.CurrentEntity!=null);

            VisibleMode(
                repository.CurrentActionHandler != null,
                repository.CurrentTriggerHandler != null,
                repository.CurrentEntity != null);
        }

        private void EnableMode(bool enabledModeScript, bool enabledModeTrigger, bool enabledModeParticleSystem)
        {
            btnScriptModeBar.Enabled = enabledModeScript;
            btnTriggerModeBar.Enabled = enabledModeTrigger;
            btnParticleSystemModeBar.Enabled = enabledModeParticleSystem;
        }

        private void VisibleMode(bool visibleModeScript, bool visibleModeTrigger, bool visibleModeParticleSystem)
        {
            scriptControl.Visible = visibleModeScript & repository.ViewingMode == ViewingMode.Script;
            triggerControl.Visible = visibleModeTrigger & repository.ViewingMode == ViewingMode.Trigger;
            particleControl.Visible = visibleModeParticleSystem & repository.ViewingMode == ViewingMode.ParticleSystem;
        }

        private void RefreshTreeView()
        {
            treeView.RefreshView();
        }

        private void SetRepository()
        {
            render.Repository = repository;
            scriptControl.Repository = repository;
            triggerControl.Repository = repository;
            particleControl.Repository = repository;
            treeView.Repository = repository;
        }

        private void New()
        {
            Repository.physicSimulator = new FarseerGames.FarseerPhysics.PhysicsSimulator(new Vector2(0, 9.81f));

            MouseMode mouseMode = repository.MouseMode;
            bool pause = repository.Pause;
            string textureName = repository.CurrentTextureName;

            repository = new Repository();
            repository.MouseMode = mouseMode;

            modelViewerControl.Initialize(repository, contentManager, contentBuilder);

            btnShowDebugMode.Checked = true;
            InitPhysicSimulatorView();

            repository.PhysicsSimulatorView.LoadContent(modelViewerControl.GraphicsDevice, contentManager);

            repository.CurrentPointer.WorldPosition = new Vector2(100, 80);

            repository.Camera.Zoom = 1f;
            repository.Camera.Position = new Vector2(0, 0);
            repository.Camera.Focal = new Vector2(0, 0);

            repository.Pause = pause;
            repository.MouseMode = mouseMode;
            repository.CurrentTextureName = textureName;
            repository.FrmEdit2D = this;

            render.UpdatePhysic();

            SetRepository();

            RefreshTreeView();
        }

        private void Open()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Niveau *.lvl |*.lvl";

            Repository.physicSimulator.Enabled = false;
            bool showDebugMode = repository.ShowDebugMode;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                New();

                FileSystem.Open(dlg.FileName, this.repository);

                SetRepository();

                repository.ShowDebugMode = showDebugMode;
            }

            Repository.physicSimulator.Enabled = true;

            repository.WatchLoading.Start();

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

            foreach (Entity entity in repository.listEntity)
            {
                foreach (TriggerBase trigger in entity.ListTrigger)
                {
                    trigger.InitTrigger(repository);
                }

                foreach (Script script in entity.ListScript)
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
            if (repository.CurrentEntity != null)
            {
                AddScriptObjectStatus(repository.CurrentEntity.Position, repository.CurrentEntity.Rotation, repository.CurrentEntity.SizeVector);
            }
        }

        private void btnRecObjectStatusLoop_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntity != null)
            {
                //Script script = null;

                if (btnScriptModeBar.Checked && repository.CurrentScript != null)
                {
                    if (repository.CurrentScript.ListAction.Count > 0)
                    {
                        ActionCurve actionCurve = null;

                        if ((actionCurve = (ActionCurve)repository.CurrentScript.ListAction.Find(a => a.ActionName == "Position")) != null)
                        {
                            actionCurve.ListCurve[0].Keys.Add(new CurveKey(scriptControl.TimeLineValue, actionCurve.ListCurve[0].Keys[0].Value));
                            actionCurve.ListCurve[1].Keys.Add(new CurveKey(scriptControl.TimeLineValue, actionCurve.ListCurve[1].Keys[0].Value));



                            actionCurve.ListCurve[0].ComputeTangents(CurveTangent.Smooth);
                            actionCurve.ListCurve[1].ComputeTangents(CurveTangent.Smooth);


                            LoopTangentCurve(actionCurve.ListCurve[0]);
                            LoopTangentCurve(actionCurve.ListCurve[1]);
                            //actionCurve.ListCurve[0].Keys.Last().TangentOut = actionCurve.ListCurve[0].Keys[0].TangentOut;
                            //actionCurve.ListCurve[0].Keys[0].TangentIn = actionCurve.ListCurve[0].Keys.Last().TangentIn;

                            //actionCurve.ListCurve[1].Keys.Last().TangentOut = actionCurve.ListCurve[1].Keys[0].TangentOut;
                            //actionCurve.ListCurve[1].Keys[0].TangentIn = actionCurve.ListCurve[1].Keys.Last().TangentIn;

                            actionCurve.CalcDuration();
                        }
                        if ((actionCurve = (ActionCurve)repository.CurrentScript.ListAction.Find(a => a.ActionName == "Rotation")) != null)
                        {
                            actionCurve.ListCurve[0].Keys.Add(new CurveKey(scriptControl.TimeLineValue, actionCurve.ListCurve[0].Keys[0].Value));

                            actionCurve.ListCurve[0].ComputeTangents(CurveTangent.Smooth);

                            actionCurve.CalcDuration();
                        }
                        if ((actionCurve = (ActionCurve)repository.CurrentScript.ListAction.Find(a => a.ActionName == "Size")) != null)
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
                    scriptControl.RefreshScriptControl(true);
                }
            }
        }

        private void LoopTangentCurve(Curve curve)
        {
            CurveKey keyA = curve.Keys[0];
            CurveKey keyN = curve.Keys[1];
            CurveKey keyP = curve.Keys[curve.Keys.Count - 2];
            CurveKey keyB = curve.Keys[curve.Keys.Count - 1];

            float tangentIn = 0f;
            float tangentOut = 0f;


            float dt = keyB.Position + keyN.Position - keyA.Position - keyP.Position;
            float dv = keyN.Value - keyP.Value;
            if (Math.Abs(dv) < float.Epsilon)
            {
                tangentIn = 0;
                tangentOut = 0;
            }
            else
            {
                tangentIn = dv * (keyB.Position - keyP.Position) / dt;
                tangentOut = dv * (keyN.Position - keyA.Position) / dt;
            }

            keyA.TangentIn = tangentIn;
            keyA.TangentOut = tangentOut;

            keyB.TangentIn = tangentIn;
            keyB.TangentOut = tangentOut;
        }

        static void SetCurveKeyTangent(ref CurveKey prev, ref CurveKey cur, ref CurveKey next)
        {
            float dt = next.Position - prev.Position;
            float dv = next.Value - prev.Value;
            if (Math.Abs(dv) < float.Epsilon)
            {
                cur.TangentIn = 0;
                cur.TangentOut = 0;
            }
            else
            {
                cur.TangentIn = dv * (cur.Position - prev.Position) / dt;
                cur.TangentOut = dv * (next.Position - cur.Position) / dt;
            }
        }

        private void AddScriptObjectStatus(Vector2 position, float rotation, Vector2 size)
        {
            if (repository.CurrentEntity != null)
            {
                if (!(btnScriptModeBar.Checked && repository.CurrentScript != null))
                {
                    ShowScriptMode();
                    repository.CurrentScript = this.scriptControl.AddScriptToCurrentEntity();
                    if (repository.CurrentScript == null)
                        return;
                }

                if (repository.CurrentScript.ListAction.Count == 0)
                {
                    //--- Position
                    ActionCurve actionPosition = new ActionCurve(repository.CurrentScript, "Position", false, false, "Position");
                    repository.CurrentScript.ListAction.Add(actionPosition);
                    //---

                    //--- Rotation
                    ActionCurve actionRotation = new ActionCurve(repository.CurrentScript, "Rotation", false, false, "Rotation");
                    repository.CurrentScript.ListAction.Add(actionRotation);
                    //---

                    //--- Size
                    ActionCurve actionSize = new ActionCurve(repository.CurrentScript, "Size", false, false, "Size");
                    repository.CurrentScript.ListAction.Add(actionSize);
                    //---
                }

                foreach (ActionBase action in repository.CurrentScript.ListAction)
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
                scriptControl.RefreshScriptControl(true);
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

            repository.MouseMode = MouseMode.Move;
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            btnMove.CheckState = CheckState.Unchecked;
            btnResize.CheckState = CheckState.Checked;
            btnRotate.CheckState = CheckState.Unchecked;

            repository.MouseMode = MouseMode.Resize;
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            btnMove.CheckState = CheckState.Unchecked;
            btnResize.CheckState = CheckState.Unchecked;
            btnRotate.CheckState = CheckState.Checked;

            repository.MouseMode = MouseMode.Rotate;
        }

        private void btnPinStatic_Click(object sender, EventArgs e)
        {
            btnPinStatic.Checked = !btnPinStatic.Checked;

            if (repository.CurrentEntity != null)
            {
                repository.CurrentEntity.IsStatic = btnPinStatic.Checked;

                if (repository.tempEntity != null)
                    repository.tempEntity.IsStatic = btnPinStatic.Checked;
            }

            for (int i = 0; i < repository.ListSelection.Count; i++)
            {
                repository.ListSelection[i].Entity.IsStatic = btnPinStatic.Checked;

                if (repository.ListSelection[i].TempEntity != null)
                    repository.ListSelection[i].TempEntity.IsStatic = btnPinStatic.Checked;
            }
        }

        private void btnColisionable_Click(object sender, EventArgs e)
        {
            btnColisionable.Checked = !btnColisionable.Checked;

            if (repository.CurrentEntity != null)
            {
                repository.CurrentEntity.IsColisionable = btnColisionable.Checked;

                if (repository.tempEntity != null)
                    repository.tempEntity.IsColisionable = btnColisionable.Checked;
            }

            for (int i = 0; i < repository.ListSelection.Count; i++)
            {
                repository.ListSelection[i].Entity.IsColisionable = btnColisionable.Checked;

                if (repository.ListSelection[i].TempEntity != null)
                    repository.ListSelection[i].TempEntity.IsColisionable = btnColisionable.Checked;
            }
        }

        private void btnResetEntityPhysic_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntity != null)
            {
                Vector2 position = repository.CurrentEntity.Position;
                float rotation = repository.CurrentEntity.Rotation;

                repository.CurrentEntity.Body.ResetDynamics();
                repository.CurrentEntity.SetPosition(position);
                repository.CurrentEntity.Rotation = rotation;
            }
        }

        private void btnCursorToEntityCenter_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntity != null)
            {
                Vector2 position = repository.CurrentEntity.Position;
                repository.CurrentPointer.WorldPosition = position;
                repository.CurrentPointer.CalcScreenPositionFromWorldPosition(repository.Camera);
            }
        }

        private void btnSetCenterEntity_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntity != null)
            {
                repository.CurrentEntity.SetCenterFromWorldPosition(repository.CurrentPointer.WorldPosition, true);

                repository.tempEntity = (Entity)repository.CurrentEntity.Clone(false);

                Repository.physicSimulator.Update(0.000002f);
            }
        }

        private void btnOrderUp_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntity != null && repository.ListSelection.Count > 0)
            {
                int entityIndex = repository.listEntity.IndexOf(repository.CurrentEntity);
                int minIndex = repository.listEntity.Count - 1;

                for (int i = 0; i < repository.ListSelection.Count; i++)
                {
                    int index = repository.listEntity.IndexOf(repository.ListSelection[i].Entity);

                    if (index < minIndex)
                        minIndex = index;
                }

                if (entityIndex < minIndex)
                {
                    for (int j = entityIndex; j < minIndex - 1; j++)
                    {
                        repository.listEntity[j] = repository.listEntity[j + 1];
                    }

                    repository.listEntity[minIndex - 1] = repository.CurrentEntity;
                }
                else if (entityIndex > minIndex)
                {
                    for (int j = entityIndex; j >= minIndex; j--)
                    {
                        repository.listEntity[j] = repository.listEntity[j - 1];
                    }

                    repository.listEntity[minIndex] = repository.CurrentEntity;
                }

                RefreshTreeView();
            }
        }

        private void btnOrderDown_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntity != null && repository.ListSelection.Count > 0)
            {
                int entityIndex = repository.listEntity.IndexOf(repository.CurrentEntity);
                int maxIndex = 0;

                for (int i = 0; i < repository.ListSelection.Count; i++)
                {
                    int index = repository.listEntity.IndexOf(repository.ListSelection[i].Entity);

                    if (index > maxIndex)
                        maxIndex = index;
                }

                if (entityIndex < maxIndex)
                {
                    for (int j = entityIndex; j < maxIndex + 1; j++)
                    {
                        repository.listEntity[j] = repository.listEntity[j + 1];
                    }

                    repository.listEntity[maxIndex] = repository.CurrentEntity;
                }
                else if (entityIndex > maxIndex)
                {
                    for (int j = entityIndex; j > maxIndex; j--)
                    {
                        repository.listEntity[j] = repository.listEntity[j - 1];
                    }

                    repository.listEntity[maxIndex + 1] = repository.CurrentEntity;
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
            if (repository.CurrentEntity != null)
            {
                if (ChangeEntityName(repository.CurrentEntity, toolStripTextBoxEntityName.Text, repository.CurrentEntity.Name))
                {
                    this.propertyGrid.PropertyGrid.Refresh();
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

            if (pnlViewerModes.Panel2Collapsed)
            {
                repository.ViewingMode = ViewingMode.Nothing;
            }
            else
            {
                ShowScriptMode();
            }
        }

        private void btnPanelRight_Click(object sender, EventArgs e)
        {
            pnlMain.Panel2Collapsed = !btnPanelRight.Checked;
        }
        #endregion

        #region RightBar Events
        private void optRightBarEntities_CheckedChanged(object sender, EventArgs e)
        {
            if (optRightBarEntities.Checked)
            {
                pnlEntitys.Visible = true;
                pnlSpring.Visible = false;
                pnlJoint.Visible = false;
                propertyGrid.Visible = false;
            }
        }

        private void optRightBarSpring_CheckedChanged(object sender, EventArgs e)
        {
            if (optRightBarSpring.Checked)
            {
                pnlEntitys.Visible = false;
                pnlSpring.Visible = true;
                pnlJoint.Visible = false;
                propertyGrid.Visible = false;
            }
        }

        private void optRightBarJoint_CheckedChanged(object sender, EventArgs e)
        {
            if (optRightBarJoint.Checked)
            {
                pnlEntitys.Visible = false;
                pnlSpring.Visible = false;
                pnlJoint.Visible = true;
                propertyGrid.Visible = false;
            }
        }

        private void optRightBarProperties_CheckedChanged(object sender, EventArgs e)
        {
            if (optRightBarProperties.Checked)
            {
                pnlEntitys.Visible = false;
                pnlSpring.Visible = false;
                pnlJoint.Visible = false;
                propertyGrid.Visible = true;
            }
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
                e.Graphics.FillRectangle(WinformVisualStyle.BrushSelectedColor, recBackground);
            }
            else if (e.State == ListViewItemStates.Hot)// || (listView.SelectedItems.Count > 0 && e.Item == listView.SelectedItems[0]))
            {
                e.Graphics.FillRectangle(WinformVisualStyle.BrushMouseOverColor, recBackground);
            }
            else
            {
                e.Graphics.FillRectangle(WinformVisualStyle.BrushBackColorDark, recBackground);
            }

            e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageKey], rec);
            e.Graphics.DrawString(e.Item.Text, listView.Font, WinformVisualStyle.BrushForeColor1, loc);
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
            if (render != null)
                render.Update();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //---> Si la touche Alt (MouseMode) vient d'être préssée
            //     les entités sélectionnées sont clonées
            //if (e.Alt && !repository.keyAltPressed)
            //{
            //    CloneSelectedEntity();
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
                modelViewerControl.Cursor = dicCursors[repository.MouseMode];
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
                    modelViewerControl.Cursor = dicCursors[repository.MouseMode];
                }
                else
                {
                    modelViewerControl.Cursor = dicCursors[MouseMode.Select];
                }
                //---

                if (e.KeyCode == System.Windows.Forms.Keys.S && repository.CurrentEntity != null)
                {
                    btnPinStatic.PerformClick();
                }

                if (e.KeyCode == System.Windows.Forms.Keys.Oem7)
                {
                    if (repository.MouseMode == MouseMode.Move)
                    {
                        btnRotate.PerformClick();
                    }
                    else if (repository.MouseMode == MouseMode.Resize)
                    {
                        btnMove.PerformClick();
                    }
                    else if (repository.MouseMode == MouseMode.Rotate)
                    {
                        btnResize.PerformClick();
                    }
                }

                if (e.KeyCode == System.Windows.Forms.Keys.D1)
                {
                    if (repository.MouseMode == MouseMode.Move)
                    {
                        btnResize.PerformClick();
                    }
                    else if (repository.MouseMode == MouseMode.Resize)
                    {
                        btnRotate.PerformClick();
                    }
                    else if (repository.MouseMode == MouseMode.Rotate)
                    {
                        btnMove.PerformClick();
                    }
                }

                if (e.KeyCode == System.Windows.Forms.Keys.A)
                {
                    btnAddEntity.PerformClick();
                }

                if (e.KeyCode == System.Windows.Forms.Keys.Delete)
                {
                    DeleteEntity();
                }

                if (e.KeyCode == System.Windows.Forms.Keys.Space)
                {
                    if (repository.Pause)
                        btnPlay.PerformClick();
                    else
                        btnPause.PerformClick();
                }

                //--- Incrémente le layer des entités sélectionnées
                if (e.KeyCode == System.Windows.Forms.Keys.Add)
                {
                    repository.GetSelectedEntity().ForEach(ent => ent.Layer += 1);
                    repository.OrderEntity();
                    RefreshTreeView();
                }

                //---> Décrémente le layer des entités sélectionnées
                if (e.KeyCode == System.Windows.Forms.Keys.Subtract)
                {
                    repository.GetSelectedEntity().ForEach(ent => ent.Layer -= 1);
                    repository.OrderEntity();
                    RefreshTreeView();
                }

                if (e.KeyCode == System.Windows.Forms.Keys.P)
                {
                    repository.Screenshot = true;
                }
            }
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.propertyGrid.PropertyGrid.SelectedObject != null && this.propertyGrid.PropertyGrid.SelectedObject is Entity)
            {
                if (e.ChangedItem.Label == "Name")
                {
                    if (!ChangeEntityName(((Entity)this.propertyGrid.PropertyGrid.SelectedObject), e.ChangedItem.Value.ToString(), e.OldValue.ToString()))
                    {
                        this.propertyGrid.PropertyGrid.Refresh();
                    }
                }

            }

            RefreshTreeView();
        }

        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (treeView.IsCheckedByMouse && e.Node.Checked)
            {
                Object item = treeView.GetItemFromNode(e.Node);

                EntitySelectionChange(false, repository.CurrentEntity, item);
            }
        }

        private void btnUpEntity_Click(object sender, EventArgs e)
        {
            int index = repository.listEntity.IndexOf(repository.CurrentEntity);

            if (index > 0)
            {
                Entity entity = repository.listEntity[index - 1];
                repository.listEntity[index - 1] = repository.CurrentEntity;
                repository.listEntity[index] = entity;

                RefreshTreeView();
            }
        }

        private void btnDownEntity_Click(object sender, EventArgs e)
        {
            int index = repository.listEntity.IndexOf(repository.CurrentEntity);

            if (index < repository.listEntity.Count - 1)
            {
                Entity entity = repository.listEntity[index + 1];
                repository.listEntity[index + 1] = repository.CurrentEntity;
                repository.listEntity[index] = entity;

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
            else if (repository.keyShiftPressed && btnParticleSystemModeBar.Checked && repository.CurrentEntity != null && repository.CurrentEntity.ListParticleSystem.Count > 0)
            {
                CloneSelectedEntity(false);
                repository.CurrentPointer2.CalcMousePointerLocation(e.Location, repository.Camera);
                repository.CurrentPointer2.SaveState();
            }
            else
            {
                //--- Si la touche MouseMode est pressée et que des entités sont sélectionnées
                if (repository.keyAltPressed && (repository.CurrentEntity != null || repository.ListSelection.Count > 0))
                {
                    //---> Clone les entités avant la redéfinition de leur centrer
                    CloneSelectedEntity(true);

                    //---> Si le MouseMode courant est Rotate ou scale et qu'il y'a sélection multiple
                    //     Placer le centre des entités sur le curseur
                    if ((repository.MouseMode == MouseMode.Rotate || repository.MouseMode == MouseMode.Resize) && repository.ListSelection.Count > 0)
                    {
                        repository.GetSelectedEntity().ForEach(ent => ent.SetCenterFromWorldPosition(repository.CurrentPointer2.WorldPosition, true));
                    }

                    CloneSelectedEntity(false);
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

            //EvaluateInput();

            if (!repository.Pause && !btnGameClickableOnPlay.Checked)
                return;

            if (e.Button == MouseButtons.Middle)
                return;

            if (repository.keyCtrlPressed)
            {
                Pointer pointer = new Pointer();
                pointer.ScreenPosition = Vector2.Zero;
                pointer.CalcMousePointerLocation(e.Location, repository.Camera);

                Entity selectedEntity = repository.GetSelectedEntityFromLocation(pointer.WorldPosition);

                //---> La multisélection est possible uniquement sur les entités
                if (selectedEntity == null)
                    return;

                //---> Supprime la sélection si la position cliquée est à moins de 10 pixels
                Selection existSelection = repository.ListSelection.Find(s => Vector2.Distance(s.Pointer.ScreenPosition, pointer.ScreenPosition) <= 20);
                //---> Repositionne le pointeur si l'entité est déja sélectionnée
                Selection entitySelection = repository.ListSelection.Find(s => s.Entity == selectedEntity);

                if (existSelection != null)
                {
                    if (existSelection.Entity != null)
                        existSelection.Entity.Selected = false;

                    repository.ListSelection.Remove(existSelection);
                }
                else if (entitySelection != null)
                {
                    entitySelection.Pointer.WorldPosition = pointer.WorldPosition;
                    entitySelection.Pointer.ScreenPosition = pointer.ScreenPosition;
                }
                else
                {
                    if (selectedEntity != null)
                        selectedEntity.Selected = true;

                    Selection newSelection = new Selection(selectedEntity, pointer.WorldPosition, pointer.ScreenPosition);
                    repository.ListSelection.Add(newSelection);
                }

                //--- Affecte les entités sélectionnées au property grid
                propertyGrid.PropertyGrid.SelectedObjects = repository.GetSelectedEntity().ToArray();
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
                if (repository.CurrentEntity != null)
                {
                    if (repository.tempEntity != null)
                    {
                        repository.CurrentEntity.Body.IsStatic = repository.tempEntity.IsStatic;
                    }
                }
                //---

                //--- Si il y'a sélection multiple et que le MouseMode est Resize ou Rotate
                //    Redéfinir le centre des entités
                if (repository.ListSelection.Count > 0 && (repository.MouseMode == MouseMode.Resize || repository.MouseMode == MouseMode.Rotate) && clonedSelectedEntity.Count > 0)
                {
                    for (int i = 0; i < repository.ListSelection.Count; i++)
                    {
                        repository.ListSelection[i].Entity.SetNewCenter(clonedSelectedEntity[i].Center - repository.ListSelection[i].Entity.Center, true);
                    }

                    if (repository.CurrentEntity != null)
                    {
                        repository.CurrentEntity.SetNewCenter(clonedSelectedEntity.Last().Center - repository.CurrentEntity.Center, true);
                    }

                    clonedSelectedEntity = new List<Entity>();
                }
                //---

                //--- Si la touche MouseMode n'est pas pressée, change l'entité courante
                if (!repository.keyAltPressed)
                {
                    //--- Si la touche Ctrl n'est pas pressée et que la touche MouseMode (Alt) ne
                    //    l'est pas non plus, vide la liste de multisélection
                    repository.ListSelection.ForEach(s => s.Entity.Selected = false);
                    repository.ListSelection = new List<Selection>();
                    //---

                    Entity selectedEntity = repository.GetSelectedEntityFromLocation(repository.CurrentPointer.WorldPosition);

                    if (selectedEntity != null)
                        EntitySelectionChange(repository.CurrentEntity, repository.GetSelectedEntityFromLocation(repository.CurrentPointer.WorldPosition));
                }
                //--- Si la touche MouseMode est pressée, réinjecte les nouvelles valeurs des propriétés (Size, Rotation)
                else
                {
                    //TODO : mettrer cela en place pour la mutli sélection
                    if (repository.CurrentEntity != null && repository.tempEntity != null)
                    {
                        if (repository.MouseMode == MouseMode.Move)
                        {
                            for (int i = 0; i < repository.CurrentEntity.ListFixedRevoluteJoint.Count; i++)
                            {
                                repository.CurrentEntity.ListFixedRevoluteJoint[i].Anchor = repository.CurrentEntity.ListFixedRevoluteJoint[i].Anchor + repository.CurrentEntity.Position - repository.tempEntity.Position;
                            }
                        }

                        if (repository.MouseMode == MouseMode.Resize)
                        {
                            Entity newEntity = repository.ChangeEntitySize(repository.CurrentEntity, repository.tempEntity.Size);
                            EntitySelectionChange(repository.CurrentEntity, newEntity);

                            Repository.physicSimulator.Update(0.0000001f);
                        }

                        repository.tempEntity = null;
                    }

                    //---> Si la touche Alt (MouseMode) est préssée alors que la souris est relâchée
                    //     alors il faut clôner les entités sélectionnées
                    CloneSelectedEntity(false);
                    //prevPos = repository.CurrentPointer;
                    repository.CurrentPointer.SaveState();
                }
                //---
            }
        }

        private void modelViewerControl_MouseMove(object sender, MouseEventArgs e)
        {
            //EvaluateInput();

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
                        if (repository.CurrentEntity != null && repository.CurrentEntity.ListParticleSystem.Count > 0)
                        {
                            Vector2 vec1 = repository.CurrentPointer2.PrevWorldPosition - repository.CurrentEntity.Position;
                            Vector2 vec2 = repository.CurrentPointer2.WorldPosition - repository.CurrentEntity.Position;

                            vec1.Normalize();
                            vec2.Normalize();
                            float angle = vec1.GetAngle(vec2);

                            //TODO : ajouter un prevpos2 lors du MouseDown pour le pointer2
                            repository.CurrentEntity.ListParticleSystem[0].EmmittingAngle = repository.tempEntity.ListParticleSystem[0].EmmittingAngle + angle;
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
                    if ((repository.CurrentEntity != null || repository.ListSelection.Count > 0) &&
                         repository.keyAltPressed &&
                         repository.MouseMode == MouseMode.Move)
                    {
                        Vector2 deltaPosition = repository.CurrentPointer.WorldPosition - repository.CurrentPointer.PrevWorldPosition;

                        if (repository.CurrentEntity != null)
                            repository.CurrentEntity.FixPosition(repository.tempEntity.Position + deltaPosition);

                        for (int i = 0; i < repository.ListSelection.Count; i++)
                        {
                            repository.ListSelection[i].Entity.FixPosition(repository.ListSelection[i].TempEntity.Position + deltaPosition);
                            repository.ListSelection[i].Pointer.WorldPosition = repository.ListSelection[i].Pointer.PrevWorldPosition + deltaPosition;

                            repository.ListSelection[i].Pointer.CalcScreenPositionFromWorldPosition(repository.Camera);
                        }
                    }
                    //---

                    //--- MouseMode.Resize
                    #region Resize
                    if (repository.CurrentEntity != null && repository.tempEntity != null && repository.keyAltPressed && repository.MouseMode == MouseMode.Resize)
                    {
                        float width = 0;
                        float height = 0;

                        //--- Calcul des positions absolues des 4 points du rectangle
                        float w = (float)repository.tempEntity.Size.Width / 2f;
                        float h = (float)repository.tempEntity.Size.Height / 2f;
                        float r = (float)Math.Sqrt(w * w + h * h);

                        float angle = 0f;
                        float angleFinalPoint = 0f;

                        angle = (float)Math.Acos(w / r);

                        angleFinalPoint = repository.tempEntity.Rotation + angle + MathHelper.Pi;
                        Vector2 vec11 = repository.tempEntity.Position - 0 * repository.tempEntity.Center + new Vector2(r * (float)Math.Cos(angleFinalPoint), r * (float)Math.Sin(angleFinalPoint));

                        angleFinalPoint = repository.tempEntity.Body.Rotation - angle;
                        Vector2 vec12 = repository.tempEntity.Position - 0 * repository.tempEntity.Center + new Vector2(r * (float)Math.Cos(angleFinalPoint), r * (float)Math.Sin(angleFinalPoint));

                        angleFinalPoint = repository.tempEntity.Body.Rotation + angle;
                        Vector2 vec21 = repository.tempEntity.Position - 0 * repository.tempEntity.Center + new Vector2(r * (float)Math.Cos(angleFinalPoint), r * (float)Math.Sin(angleFinalPoint));

                        angleFinalPoint = repository.tempEntity.Body.Rotation - angle + MathHelper.Pi;
                        Vector2 vec22 = repository.tempEntity.Position - 0 * repository.tempEntity.Center + new Vector2(r * (float)Math.Cos(angleFinalPoint), r * (float)Math.Sin(angleFinalPoint));

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

                        //repository.currentEntity.SetSize((int)width, (int)height);
                        repository.CurrentEntity.ChangeSize((int)width, (int)height, false);

                        repository.CurrentEntity.Position = repository.tempEntity.Position;
                        repository.CurrentEntity.Rotation = repository.tempEntity.Rotation;
                        //repository.currentEntity.ChangeSize((int)width, (int)height, repository.currentEntity.IsColisionable);


                        //Debug.Print(string.Format("Width : {0:0.00} - Height : {1:0.00}", width, height));
                        //this.Text = String.Format("pctX : {0:0.00} pctY : {1:0.00}", pctX, pctY);

                        Repository.physicSimulator.Update(0.0000001f);
                    }
                    #endregion
                    //---

                    //--- MouseMode.Rotate
                    if ((repository.CurrentEntity != null || repository.ListSelection.Count > 0) && repository.keyAltPressed && repository.MouseMode == MouseMode.Rotate)
                    {
                        List<Entity> listSelectedEntity = repository.GetSelectedEntity();

                        Vector2 vecA = Vector2.Zero;
                        Vector2 vecB = Vector2.Zero;
                        float angle = 0f;

                        for (int i = 0; i < repository.ListSelection.Count; i++)
                        {
                            vecA = repository.CurrentPointer.PrevWorldPosition - repository.ListSelection[i].Entity.Position;
                            vecB = repository.CurrentPointer.WorldPosition - repository.ListSelection[i].Entity.Position;

                            vecA.Normalize();
                            vecB.Normalize();

                            angle = vecA.GetAngle(vecB);

                            repository.ListSelection[i].Entity.Rotation = repository.ListSelection[i].TempEntity.Rotation + angle;
                        }

                        if (repository.CurrentEntity != null)
                        {
                            vecA = repository.CurrentPointer.PrevWorldPosition - repository.CurrentEntity.Position;
                            vecB = repository.CurrentPointer.WorldPosition - repository.CurrentEntity.Position;

                            vecA.Normalize();
                            vecB.Normalize();

                            angle = vecA.GetAngle(vecB);
                            this.Text = angle.ToString();
                            repository.CurrentEntity.Rotation = repository.tempEntity.Rotation + angle;
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

            if (btnParticleSystemModeBar.Checked && repository.keyShiftPressed && repository.CurrentParticleSystem != null)
            {
                repository.CurrentParticleSystem.FieldAngle += (float)e.Delta / 960f;
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

        List<Entity> clonedSelectedEntity = new List<Entity>();

        private void CloneSelectedEntity(bool cloneLocal)
        {
            if (cloneLocal)
                clonedSelectedEntity = new List<Entity>();

            //---> Création des clones lorsque la touche de modification est pressée
            if (repository.CurrentEntity != null || repository.ListSelection.Count > 0)
            {
                for (int i = 0; i < repository.ListSelection.Count; i++)
                {
                    //---> Le body courant devient statique pour tous les MouseMode
                    if (cloneLocal)
                        clonedSelectedEntity.Add((Entity)repository.ListSelection[i].Entity.Clone(false));
                    else
                        repository.ListSelection[i].TempEntity = (Entity)repository.ListSelection[i].Entity.Clone(false);

                    repository.ListSelection[i].Pointer.SaveState();
                    repository.ListSelection[i].Entity.IsStatic = true;

                    //--- Suppression du body courant si on est en mode Resize
                    if (repository.MouseMode == MouseMode.Resize)
                    {
                        Repository.physicSimulator.Remove(repository.ListSelection[i].Entity.Body);
                        Repository.physicSimulator.Remove(repository.ListSelection[i].Entity.geom);
                    }
                    //---
                }

                //TODO : unifier la mutlisélection avec la sélection simple
                if (repository.CurrentEntity != null)
                {
                    //---> Le body courant devient statique pour tous les MouseMode
                    if (cloneLocal)
                        clonedSelectedEntity.Add((Entity)repository.CurrentEntity.Clone(false));
                    else
                        repository.tempEntity = (Entity)repository.CurrentEntity.Clone(false);

                    repository.CurrentEntity.Body.IsStatic = true;

                    //--- Suppression du body courant si on est en mode Resize
                    if (repository.MouseMode == MouseMode.Resize)
                    {
                        Repository.physicSimulator.Remove(repository.CurrentEntity.Body);
                        Repository.physicSimulator.Remove(repository.CurrentEntity.geom);
                    }
                    //---
                }
            }
        }

        private void modelViewerControl_Resize(object sender, EventArgs e)
        {
            modelViewerControl.ChangeViewPortSize = true;
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

            repository.ViewingMode = ViewingMode.Script;

            scriptControl.RefreshScriptControl(true);
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

            repository.ViewingMode = ViewingMode.Trigger;

            triggerControl.RefreshTriggerList(true);
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

            repository.ViewingMode = ViewingMode.ParticleSystem;

            particleControl.RefreshParticleControl(true);
        }
        #endregion
    }
}