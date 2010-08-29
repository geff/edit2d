using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Edit2DEngine.Action;
using Microsoft.Xna.Framework;
using Xna.Tools;
using System.Reflection;
using Edit2DEngine.Entities.Particles;
using Edit2DEngine;
using Edit2D.UC;
using Edit2DEngine.Entities;

namespace Edit2D.ScriptControl
{
    public partial class ScriptControl : UserControlLocal
    {
        #region Constantes
        int ID_ACTION_CURVE = 0;
        int ID_ACTION_EVENT = 1;
        int ID_ACTION_SOUND = 2;
        #endregion

        #region Attributs
        public Repository Repository { get; set; }
        int currentAction = -1;
        int currentSubAction = -1;

        private int timeLineValue;
        public int TimeLineValue
        {
            get
            {
                return timeLineValue;
            }
            set
            {
                timeLineValue = value;
                this.curveControl.TimeLine = value;
            }
        }
        #endregion

        public ScriptControl()
        {
            InitializeComponent();

            InitScriptControl();
        }

        #region Script events
        private void txtScriptName_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                txtScriptName.ResetText();
                txtScriptName.Focus();
                txtScriptName.Update();
            }
        }

        private void btnAddScript_Click(object sender, EventArgs e)
        {
            AddScriptToCurrentEntity();
        }

        private void btnAddScript_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                txtScriptName.Text = String.Empty;
                AddScriptToCurrentEntity();
            }
        }

        private void btnDelScript_Click(object sender, EventArgs e)
        {
            DeleteCurrentScript();
        }

        private void btnDelSrcipt_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                DeleteCurrentScript();
            }
        }

        private void btnChangeScriptName_Click(object sender, EventArgs e)
        {
            ChangeCurrentScriptName();
        }

        private void btnChangeScriptName_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                ChangeCurrentScriptName();
            }
        }

        private void btnPlayScript_Click(object sender, EventArgs e)
        {
            if (Repository.CurrentScript != null)
            {
                foreach (ActionBase action in Repository.CurrentScript.ListAction)
                {
                    if (action is ActionCurve)
                    {
                        if (((ActionCurve)action).playAnimationState != PlayAnimationState.Stop)
                            ((ActionCurve)action).playAnimationState = PlayAnimationState.Stop;
                        else
                        {
                            ((ActionCurve)action).StartAnimation(PlayAnimationState.PlayInEditor);
                        }
                    }
                }
            }
        }

        private void listboxScript_SelectedIndexChanged(object sender, EventArgs e)
        {
            Repository.CurrentScript = null;

            if (listboxScript.SelectedIndex != -1)
                Repository.CurrentScript = Repository.CurrentActionHandler.ListScript[listboxScript.SelectedIndex];

            RefreshActionView(true);
            CheckNodeGlobalTreeView<Script>(Repository.CurrentScript);

            if (Repository.CurrentScript != null)
            {
                txtScriptName.Text = Repository.CurrentScript.ScriptName;
            }
        }

        private void btnAddAction_Click(object sender, EventArgs e)
        {
            if (Repository.CurrentScript != null &&
                cmbActionType.SelectedIndex != -1 &&
                (cmbActionProperties.SelectedIndex != -1 || !cmbActionProperties.Visible))
            {
                int curveCount = Repository.CurrentScript.ListAction.Count(action => action is ActionCurve) - 1;
                int eventCount = Repository.CurrentScript.ListAction.Count(action => action is ActionEvent) - 1;
                int soundCount = Repository.CurrentScript.ListAction.Count(action => action is ActionSound) - 1;

                ActionBase act = null;

                //--- Création de l'action
                if (cmbActionType.SelectedIndex == ID_ACTION_CURVE)
                {
                    act = new ActionCurve(Repository.CurrentScript, String.Format("Curve{0}", curveCount + 1), true, false, cmbActionProperties.SelectedItem.ToString());
                    currentAction = curveCount + 1;
                }
                else if (cmbActionType.SelectedIndex == ID_ACTION_EVENT)
                {
                    act = new ActionEvent(Repository.CurrentScript, String.Format("Event{0}", eventCount + 1), cmbActionProperties.SelectedItem.ToString());
                    currentAction = eventCount + 1;
                }
                else if (cmbActionType.SelectedIndex == ID_ACTION_SOUND)
                {
                    act = new ActionSound(Repository.CurrentScript, String.Format("Sound{0}", soundCount + 1), String.Empty, false);
                    currentAction = soundCount + 1;
                }
                //---

                Repository.CurrentScript.ListAction.Add(act);

                RefreshScriptView(true);
                RefreshActionView(true);

                //treeViewAction.SelectedNode = treeViewAction.Nodes[treeViewAction.Nodes.Count - 1];
            }
        }

        private void btnDelAction_Click(object sender, EventArgs e)
        {
            if (Repository.CurrentActionHandler != null && Repository.CurrentScript != null && currentAction != -1)
            {
                Repository.CurrentScript.ListAction.RemoveAt(currentAction);
                currentAction = -1;
                currentSubAction = -1;

                RefreshScriptView(true);
                RefreshActionView(true);
            }
        }

        private void btnActionUp_Click(object sender, EventArgs e)
        {
            /*if (currentScript != 1 && currentCurve != -1)
            {
                repository.currentEntity.ListScript[currentScript].ListCurve.mo [currentCurve]
            }*/
        }

        private void btnActionDown_Click(object sender, EventArgs e)
        {

        }

        private void cmbActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitComboProperties();
        }

        private void btnPlayScriptAction_Click(object sender, EventArgs e)
        {
            ActionCurve actionCurve = GetCurrentActionCurve();

            if (actionCurve == null)
                return;

            //---> Arrête l'animation si elle est en route
            if (actionCurve.playAnimationState != PlayAnimationState.Stop)
                actionCurve.playAnimationState = PlayAnimationState.Stop;
            else
            {
                actionCurve.StartAnimation(PlayAnimationState.PlayInEditor);
            }
        }

        private void treeViewAction_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ActionBase action = null;

            if (e.Node.Parent != null)
            {
                currentAction = e.Node.Parent.Index;
                currentSubAction = e.Node.Index;
            }
            else
            {
                currentAction = e.Node.Index;
                currentSubAction = -1;
            }

            if (currentAction != -1)
            {
                if (Repository.CurrentActionHandler != null)
                    action = Repository.CurrentScript.ListAction[currentAction];

                propAction.PropertyGrid.SelectedObject = action;
            }

            if (action is ActionCurve)
            {
                btnPlayScriptAction.Visible = true;
                ViewActionCurve((ActionCurve)action);
            }
            else if (action is ActionEvent)
            {
                btnPlayScriptAction.Visible = false;
                ViewActionEvent((ActionEvent)action);
            }
            else if (action is ActionSound)
            {
                btnPlayScriptAction.Visible = true;
                ViewActionSound((ActionSound)action);
            }
        }

        private void curveControl_TimeLineChange(object sender, int value)
        {
            ActionCurve actionCurve = GetCurrentActionCurve();
            this.timeLineValue = value;

            if (actionCurve != null)
            {
                if (value == -1) // StartAnimation
                {
                    actionCurve.StartAnimation(PlayAnimationState.PlayManually);
                }
                else
                {
                    actionCurve.UpdateAnimation(value);
                }
            }
        }

        private void curveControl_CurveChange(object sender, EventArgs e)
        {
            ActionCurve curve = GetCurrentActionCurve();

            if (curve != null)
            {
                UpdateCurve(curve);
            }
            else
                return;
        }

        private void ScriptControl_Resize(object sender, EventArgs e)
        {
            //TODO : ajouter une condition sur l'action event visible
            //foreach (Control ctrl in pnlActionEventLines.Controls)
            //{
            //    ctrl.Width = pnlActionEventLines.Width;
            //}
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            RefreshActionView(false);
        }
        #endregion

        #region Script private methods
        private void InitScriptControl()
        {
            propAction.PropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(PropertyGrid_PropertyValueChanged);

            cmbActionType.SelectedIndex = 0;
            listboxScript.SelectedIndex = -1;
        }

        private void RefreshScriptView(bool selectScript)
        {
            listboxScript.Items.Clear();
            txtScriptName.Clear();

            if (Repository.CurrentActionHandler == null)
                return;


            for (int i = 0; i < Repository.CurrentActionHandler.ListScript.Count; i++)
            {
                listboxScript.Items.Add(Repository.CurrentActionHandler.ListScript[i].ScriptName);
            }


            if (selectScript)
            {
                if (Repository.CurrentScript != null)
                {
                    listboxScript.SelectedIndex = listboxScript.FindString(Repository.CurrentScript.ScriptName);
                }
                else if (Repository.CurrentActionHandler.ListScript.Count > 0)
                {
                    RefreshGlobalTreeView();
                    listboxScript.SelectedIndex = 0;
                }
                else
                {
                    RefreshActionView(true);

                    RefreshGlobalTreeView<IActionHandler>(Repository.CurrentActionHandler);
                }
            }
        }

        private void HideActionView()
        {
            pnlAction.Enabled = false;
            lblActionType.Enabled = false;
            lblActionProperty.Enabled = false;
            cmbActionProperties.Enabled = false;
            cmbActionType.Enabled = false;
            lblAction.Enabled = false;
        }

        private void ShowActionView()
        {
            pnlAction.Enabled = true;
            lblActionType.Enabled = true;
            lblActionProperty.Enabled = true;
            cmbActionProperties.Enabled = true;
            cmbActionType.Enabled = true;
            lblAction.Enabled = true;
        }

        private void RefreshActionView(bool selectAction )
        {
            treeViewAction.Nodes.Clear();

            pnlCurve.Visible = false;
            pnlActionEvent.Visible = false;
            actionSoundControl.Visible = false;
            propAction.Visible = false;

            if (Repository.CurrentActionHandler != null && Repository.CurrentScript != null)
            {
                ShowActionView();
                InitComboProperties();

                for (int i = 0; i < Repository.CurrentScript.ListAction.Count; i++)
                {
                    ActionBase action = Repository.CurrentScript.ListAction[i];

                    if (action is ActionCurve)
                    {
                        ActionCurve actionCurve = (ActionCurve)action;

                        switch (actionCurve.PropertyType.Name)
                        {
                            case "Size":
                                TreeNode nodeSize = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})({3})", action.ActionName, actionCurve.PropertyName, "Curve" + (actionCurve.IsLoop ? "(loop)" : ""), actionCurve.IsRelative ? "R" : "A"));
                                nodeSize.Nodes.Add("Width", "Width");
                                nodeSize.Nodes.Add("Height", "Height");
                                break;
                            case "Vector2":
                                TreeNode nodeVector2 = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})({3})", action.ActionName, actionCurve.PropertyName, "Curve" + (actionCurve.IsLoop ? "(loop)" : ""), actionCurve.IsRelative ? "R" : "A"));
                                nodeVector2.Nodes.Add("X", "X");
                                nodeVector2.Nodes.Add("Y", "Y");
                                break;
                            case "Single":
                                TreeNode nodeAngle = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})({3})", action.ActionName, actionCurve.PropertyName, "Curve" + (actionCurve.IsLoop ? "(loop)" : ""), actionCurve.IsRelative ? "R" : "A"));
                                break;
                            case "Int32":
                                TreeNode nodeInt32 = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})({3})", action.ActionName, actionCurve.PropertyName, "Curve" + (actionCurve.IsLoop ? "(loop)" : ""), actionCurve.IsRelative ? "R" : "A"));
                                break;
                            case "Color":
                                TreeNode nodeColor = treeViewAction.Nodes.Add(String.Format("{0}-Color ({1})({2})", action.ActionName, actionCurve.PropertyName, "Curve" + (actionCurve.IsLoop ? "(loop)" : ""), actionCurve.IsRelative ? "R" : "A"));
                                nodeColor.Nodes.Add("R", "R");
                                nodeColor.Nodes.Add("G", "G");
                                nodeColor.Nodes.Add("B", "B");
                                nodeColor.Nodes.Add("A", "A");
                                break;
                            case "Boolean":
                                TreeNode nodeBool = treeViewAction.Nodes.Add(String.Format("{0}-{1})", action.ActionName, actionCurve.PropertyName, "Curve" + (actionCurve.IsLoop ? "(loop)" : ""), actionCurve.IsRelative ? "R" : "A"));
                                break;
                            default:
                                break;
                        }
                    }
                    else if (action is ActionEvent)
                    {
                        ActionEvent actionEvent = (ActionEvent)action;

                        switch (actionEvent.PropertyType.Name)
                        {
                            case "Size":
                                TreeNode nodeSize = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})", action.ActionName, actionEvent.PropertyName, "Event"));
                                break;
                            case "Vector2":
                                TreeNode nodeVector2 = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})", action.ActionName, actionEvent.PropertyName, "Event"));
                                break;
                            case "Single":
                                TreeNode nodeAngle = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})", action.ActionName, actionEvent.PropertyName, "Event"));
                                break;
                            case "Int32":
                                TreeNode nodeInt = treeViewAction.Nodes.Add(String.Format("{0}-{1} ({2})", action.ActionName, actionEvent.PropertyName, "Event"));
                                break;
                            case "Color":
                                TreeNode nodeColor = treeViewAction.Nodes.Add(String.Format("{0}-Color ({1})", action.ActionName, actionEvent.PropertyName, "Event"));
                                break;
                            case "Boolean":
                                TreeNode nodeBool = treeViewAction.Nodes.Add(String.Format("{0}", action.ActionName, actionEvent.PropertyName, "Event"));
                                break;
                            default:
                                break;
                        }
                    }
                    else if (action is ActionSound)
                    {
                        ActionSound actionSound = (ActionSound)action;

                        TreeNode nodeSize = treeViewAction.Nodes.Add(String.Format("{0}-({1}))", action.ActionName, "Sound" + (actionSound.Loop ? "(Loop)" : "")));
                    }
                }
            }
            else
            {
                HideActionView();
            }

            if (selectAction)
            {
                if (currentAction > -1)
                {
                    treeViewAction.SelectedNode = treeViewAction.Nodes[currentAction];
                    currentSubAction = 0;
                    propAction.Visible = true;

                }
                if (treeViewAction.Nodes.Count > 0)
                {
                    treeViewAction.SelectedNode = treeViewAction.Nodes[0];
                    currentSubAction = 0;
                    propAction.Visible = true;
                }
                else
                {
                    treeViewAction.SelectedNode = null;
                    currentSubAction = -1;
                }
            }
        }

        private void ViewActionCurve(ActionCurve actionCurve)
        {
            //--- Affiche le contrôle de courbe
            pnlCurve.Visible = true;
            pnlActionEvent.Visible = false;
            actionSoundControl.Visible = false;
            //---

            curveControl.Curves.Clear();

            if (actionCurve == null)
            {
                curveControl.TimeLine = 0;
                curveControl.Frame(0, -100, 1000, 100);
            }
            else
            {
                //--- Affiche les caractéristiques de l'action
                cmbActionType.SelectedText = actionCurve.PropertyName;
                //---

                if (currentAction != -1)
                {
                    if (currentSubAction != -1)
                    {
                        ViewActionCurve(actionCurve, currentSubAction);
                    }
                    else
                    {
                        for (int i = 0; i < actionCurve.ListCurve.Count; i++)
                        {
                            ViewActionCurve(actionCurve, i);
                        }
                    }
                }

                //--- Change la repère selon le type de courbe
                float minX = 0f;
                float minY = 0f;
                float maxX = 0f;
                float maxY = 0f;

                maxX = Math.Max(actionCurve.Duration, 1000f); ;

                switch (actionCurve.PropertyType.Name)
                {
                    case "Size":
                        minY = -150f;
                        maxY = 150f;
                        break;
                    case "Vector2":
                        minY = -150f;
                        maxY = 150f;
                        break;
                    case "Single":
                        minY = -MathHelper.TwoPi - 0.1f;
                        maxY = MathHelper.TwoPi + 0.1f;
                        break;
                    case "Color":
                        minY = 0f;
                        maxY = 256f;
                        break;
                    default:
                        break;
                }

                curveControl.Frame(minX, minY, maxX, maxY);
                curveControl.TimeLine = actionCurve.timeLineAnimation;
                //---
            }
        }

        private void ViewActionCurve(ActionCurve actionCurve, int subCurve)
        {
            //--- Calcul la couleur de la courbe à afficher
            System.Drawing.Color color = System.Drawing.Color.Blue;

            switch (actionCurve.PropertyType.Name)
            {
                case "Size":
                    if (subCurve == 0)
                        color = System.Drawing.Color.Red;
                    else
                        color = System.Drawing.Color.Blue;
                    break;
                case "Vector2":
                    if (subCurve == 0)
                        color = System.Drawing.Color.Red;
                    else
                        color = System.Drawing.Color.Blue;
                    break;
                case "Single":
                    color = System.Drawing.Color.Blue;
                    break;
                case "Color":
                    if (subCurve == 0)
                        color = System.Drawing.Color.Red;
                    else if (subCurve == 1)
                        color = System.Drawing.Color.Green;
                    else if (subCurve == 2)
                        color = System.Drawing.Color.Blue;
                    else
                        color = System.Drawing.Color.Violet;
                    break;
                default:
                    break;
            }
            //---

            //--- Curve
            curveControl.Curves.Add(new EditCurve(actionCurve.ActionName + subCurve.ToString(), color, actionCurve.ListCurve[subCurve], null));
            //---
        }

        private void CreateActionEventLines(ActionEvent actionEvent, string nameTemplate, string[] propNames, float fixedValue, float fixedMinValue, float fixedMaxValue, float rndMinValue, float rndMinMinValue, float rndMinMaxValue, float rndMaxValue, float rndMaxMinValue, float rndMaxMaxValue)
        {
            pnlActionEventLines.Controls.Clear();
            pnlActionEventLines.Height = 0;
            pnlActionEventLines.Visible = false;

            for (int i = 0; i < propNames.Length; i++)
            {
                ActionEventLineControl actionEventLine = new ActionEventLineControl(
                    actionEvent.PropertyType, Repository, String.Format(nameTemplate, propNames[i]), actionEvent, i,
                    fixedValue, fixedMinValue, fixedMaxValue, rndMinValue, rndMinMinValue, rndMinMaxValue, rndMaxValue, rndMaxMinValue, rndMaxMaxValue);

                pnlActionEventLines.Controls.Add(actionEventLine);
                actionEventLine.Top = i * (actionEventLine.Height - 1);
                pnlActionEventLines.Width = actionEventLine.Width;
                pnlActionEventLines.Height = actionEventLine.Bottom;
            }


            WinformVisualStyle.ApplyStyle(pnlActionEventLines);

            pnlActionEventLines.Visible = true;
        }

        private void ViewActionEvent(ActionEvent actionEvent)
        {
            //--- Affiche le panneau ActionEvent
            pnlCurve.Visible = false;
            pnlActionEvent.Visible = true;
            actionSoundControl.Visible = false;
            //---

            if (actionEvent.PropertyType.Name == "Color")
            {
                CreateActionEventLines(actionEvent, actionEvent.PropertyName + ".{0}", new String[] { "R", "G", "B" }, 0f, -255f, 255f, 0f, -255f, 255f, 0f, -255f, 255f);
            }
            else if (actionEvent.PropertyType.Name == "Single")
            {
                CreateActionEventLines(actionEvent, actionEvent.PropertyName, new String[] { "" }, 0f, -255f, 255f, 0f, -255f, 255f, 0f, -255f, 255f);
            }
            else if (actionEvent.PropertyType.Name == "Int32")
            {
                CreateActionEventLines(actionEvent, actionEvent.PropertyName, new String[] { "" }, 0f, 0f, 1000f, 0f, 0f, 1000f, 0f, 0f, 1000f);
            }
            else if (actionEvent.PropertyType.Name == "Vector2")
            {
                CreateActionEventLines(actionEvent, actionEvent.PropertyName + ".{0}", new String[] { "X", "Y" }, 0f, -1000f, 1000f, 0f, -1000f, 1000f, 0f, -1000f, 1000f);
            }
            else if (actionEvent.PropertyType.Name == "Size")
            {
                CreateActionEventLines(actionEvent, actionEvent.PropertyName + ".{0}", new String[] { "Width", "Height" }, 0f, -1000f, 1000f, 0f, -1000f, 1000f, 0f, -1000f, 1000f);
            }
            else if (actionEvent.PropertyType.Name == "Boolean")
            {
                CreateActionEventLines(actionEvent, actionEvent.PropertyName, new String[] { "" }, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            }

            //--- Mise à jour du composant graphique
            for (int i = 0; i < actionEvent.ActionEventTypes.Length; i++)
            {
                ((ActionEventLineControl)pnlActionEventLines.Controls[i]).RefreshActionEvent();
            }
            //---
        }

        private void ViewActionSound(ActionSound actionSound)
        {
            //--- Affiche le panneau ActionEvent
            pnlCurve.Visible = false;
            pnlActionEvent.Visible = false;
            actionSoundControl.Visible = true;
            //---
        }

        private ActionCurve GetCurrentActionCurve()
        {
            if (Repository.CurrentActionHandler != null &&
                Repository.CurrentScript != null &&
                currentAction != -1 &&
                Repository.CurrentScript.ListAction.Count > currentAction &&
                Repository.CurrentScript.ListAction[currentAction] is ActionCurve)
                return (ActionCurve)Repository.CurrentScript.ListAction[currentAction];
            else
                return null;
        }

        private ActionEvent GetCurrentActionEvent()
        {
            if (Repository.CurrentScript != null && currentAction != -1)
                return (ActionEvent)Repository.CurrentScript.ListAction[currentAction];
            else
                return null;
        }

        private void UpdateCurve(ActionCurve actionCurve)
        {
            //--- Met à jour les points
            if (currentSubAction == -1)
                currentSubAction = 0;

            {
                if (curveControl.Curves.Count > 0)
                {
                    actionCurve.ListCurve[currentSubAction] = curveControl.Curves[0].OriginalCurve.Clone();
                }
            }
            //---

            //--- Calcul et met à jour la durée de l'animation pour chaque courbe
            actionCurve.CalcDuration();
            List<ActionBase> listActionCurve = actionCurve.Script.ListAction.FindAll(action => action is ActionCurve);
            for (int j = 0; j < listActionCurve.Count; j++)
            {
                float maxDuration = 0;

                ActionCurve actionCrv = (ActionCurve)listActionCurve[j];

                for (int i = 0; i < actionCrv.ListCurve.Count; i++)
                {
                    if (actionCrv.ListCurve[i].Keys.Count > 0)
                        maxDuration = Math.Max(actionCrv.ListCurve[i].Keys.Max<CurveKey>(key => key.Position), maxDuration);
                }

                actionCrv.Duration = (int)maxDuration;
            }
            //---
        }

        private void StopAllScripts()
        {
            for (int i = 0; i < Repository.listEntity.Count; i++)
            {
                Entity entity = Repository.listEntity[i];

                StopScript(entity);

                for (int k = 0; k < entity.ListParticleSystem.Count; k++)
                {
                    StopScript(entity.ListParticleSystem[k]);

                    for (int l = 0; l < entity.ListParticleSystem[k].ListParticleTemplate.Count; k++)
                    {
                        StopScript(entity.ListParticleSystem[k].ListParticleTemplate[l]);
                    }
                }
            }
        }

        private void StopScript(IActionHandler actionHandler)
        {
            for (int j = 0; j < actionHandler.ListScript.Count; j++)
            {
                for (int k = 0; k < actionHandler.ListScript[j].ListAction.Count; k++)
                {
                    ActionBase action = actionHandler.ListScript[j].ListAction[k];

                    if (action is ActionCurve)
                    {
                        ((ActionCurve)action).StopAnimation();
                    }
                }
            }
        }

        private void InitComboProperties()
        {
            cmbActionProperties.Visible = (cmbActionType.SelectedIndex != ID_ACTION_SOUND);
            cmbActionProperties.Items.Clear();
            PropertyInfo[] properties = null;

            if (Repository!=null && Repository.CurrentActionHandler != null)
                properties = Repository.CurrentActionHandler.GetType().GetProperties();

            if (properties != null)
            {
                List<String> propertiesName = new List<String>();
                for (int i = 0; i < properties.Length; i++)
                {
                    if (properties[i].GetCustomAttributes(typeof(AttributeAction), true).Length > 0)
                        propertiesName.Add(properties[i].Name);
                }

                cmbActionProperties.Items.AddRange(propertiesName.ToArray());
            }
        }
        #endregion

        #region Public methods
        public Script AddScriptToCurrentEntity()
        {
            if (Repository.CurrentActionHandler == null)
                return null;

            //--- Calcul du nom du script
            string scriptName = String.Empty;

            if (String.IsNullOrEmpty(txtScriptName.Text))
            {
                scriptName = Common.CreateNewName<Script>(Repository.CurrentActionHandler.ListScript, "ScriptName", "Script{0}");
            }
            else
            {
                scriptName = txtScriptName.Text;
            }

            if (Repository.CurrentActionHandler.ListScript.Exists(s => s.ScriptName == scriptName))
            {
                MessageBox.Show(String.Format("Le nom de script '{0}' existe déja", scriptName), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            //---

            //--- Création du nouveau script
            Script script = new Script(scriptName, Repository.CurrentActionHandler);

            Repository.CurrentActionHandler.ListScript.Add(script);
            //---

            //--- Rafraichissement de la liste des scripts et de l'arborescence
            currentAction = -1;
            RefreshScriptView(false);
            RefreshGlobalTreeView();
            //---

            //--- Sélectionne le nouveau script
            listboxScript.SelectedIndex = listboxScript.Items.Count - 1;
            //---

            return script;
        }

        public void DeleteCurrentScript()
        {
            if (Repository.CurrentActionHandler != null && Repository.CurrentScript != null)
            {
                Repository.CurrentActionHandler.ListScript.Remove(Repository.CurrentScript);

                Repository.CurrentScript = null;

                RefreshScriptView(true);
            }
        }

        public void ChangeCurrentScriptName()
        {
            if (Repository.CurrentScript != null &&
                !String.IsNullOrEmpty(txtScriptName.Text) &&
                txtScriptName.Text != Repository.CurrentScript.ScriptName
                )
            {
                if (Repository.CurrentActionHandler.ListScript.Exists(s => s.ScriptName == txtScriptName.Text))
                {
                    MessageBox.Show(String.Format("Le nom de script '{0}' existe déja", txtScriptName.Text), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Repository.CurrentScript.ScriptName = txtScriptName.Text;

                    RefreshScriptView(true);
                    RefreshGlobalTreeView();
                }
            }
        }

        public void RefreshScriptControl(bool selectScript)
        {
            currentAction = -1;
            currentSubAction = -1;

            RefreshScriptView(selectScript);

            this.Visible = (Repository.CurrentActionHandler != null);
        }

        public void UpdateEntityActionPlayer(ActionCurve actionCurve)
        {
            ActionCurve currentActionCurve = GetCurrentActionCurve();

            if (currentActionCurve == null)
                return;

            if (actionCurve == currentActionCurve)
            {
                curveControl.TimeLine = actionCurve.timeLineAnimation;
                curveControl.Refresh();
            }
        }
        #endregion
    }
}
