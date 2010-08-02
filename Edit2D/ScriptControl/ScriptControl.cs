﻿using System;
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
using Edit2DEngine.Particles;
using Edit2DEngine;
using Edit2D.UC;

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
        int currentScript = -1;
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
            IActionHandler actionHandler = GetCurrentActionHandler();

            if (actionHandler != null && currentScript != -1)
            {
                actionHandler.ListScript.RemoveAt(listboxScript.SelectedIndex);
                //currentScript = -1;

                RefreshScriptView();

                if (actionHandler.ListScript.Count > 0)
                {
                    RefreshGlobalTreeView();
                    listboxScript.SelectedIndex = 0;
                }
                else
                {
                    currentScript = -1;
                    RefreshActionView();

                    RefreshGlobalTreeView<IActionHandler>(actionHandler);
                }
            }
        }

        private void btnChangeScriptName_Click(object sender, EventArgs e)
        {
            if (Repository.CurrentScript != null &&
                !String.IsNullOrEmpty(txtScriptName.Text) &&
                txtScriptName.Text != Repository.CurrentScript.ScriptName
                )
            {
                IActionHandler actionHandler = GetCurrentActionHandler();

                if (actionHandler.ListScript.Exists(s => s.ScriptName == txtScriptName.Text))
                {
                    MessageBox.Show(String.Format("Le nom de script '{0}' existe déja", txtScriptName.Text), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int previousSelectedIndex = listboxScript.SelectedIndex;
                    Repository.CurrentScript.ScriptName = txtScriptName.Text;

                    RefreshScriptView();
                    RefreshGlobalTreeView();

                    listboxScript.SelectedIndex = previousSelectedIndex;
                }
            }
        }

        private void btnPlayScript_Click(object sender, EventArgs e)
        {
            if (currentScript != -1)
            {
                IActionHandler actionHandler = GetCurrentActionHandler();
                Script script = actionHandler.ListScript[currentScript];

                foreach (ActionBase action in script.ListAction)
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
            currentScript = listboxScript.SelectedIndex;
            Repository.CurrentScript = GetSelectedScript();

            RefreshActionView();
            CheckNodeGlobalTreeView<Script>(Repository.CurrentScript);

            if (Repository.CurrentScript != null)
            {
                txtScriptName.Text = Repository.CurrentScript.ScriptName;

                if (treeViewAction.Nodes.Count > 0)
                {
                    treeViewAction.SelectedNode = treeViewAction.Nodes[0];
                }
            }
        }

        private void btnAddAction_Click(object sender, EventArgs e)
        {
            if (Repository.CurrentScript != null &&
                cmbActionType.SelectedIndex != -1 &&
                (cmbActionProperties.SelectedIndex != -1 || !cmbActionProperties.Visible))
            {
                //--- Déterminaison du type de l'entité porteuse de l'action
                //Type typeEntite = null;
                //Script script = null;

                //if (Repository.CurrentEntite != null)
                //{
                //    typeEntite = typeof(Entite);
                //    script = Repository.CurrentEntite.ListScript[currentScript];
                //}
                //else if (Repository.CurrentParticleSystem != null)
                //{
                //    typeEntite = typeof(ParticleSystem);
                //    script = Repository.CurrentParticleSystem.ListScript[currentScript];
                //}
                //---

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

                RefreshScriptView();
                RefreshActionView();

                currentSubAction = 0;
                treeViewAction.SelectedNode = treeViewAction.Nodes[treeViewAction.Nodes.Count - 1];
            }
        }

        private void btnDelAction_Click(object sender, EventArgs e)
        {
            IActionHandler actionHandler = GetCurrentActionHandler();

            if (actionHandler != null && currentScript != -1 && currentAction != -1)
            {
                Script script = actionHandler.ListScript[currentScript];

                script.ListAction.RemoveAt(currentAction);
                currentAction = -1;
                currentSubAction = -1;

                RefreshScriptView();
                RefreshActionView();
            }
        }

        private void btnActionUp_Click(object sender, EventArgs e)
        {
            /*if (currentScript != 1 && currentCurve != -1)
            {
                repository.currentEntite.ListScript[currentScript].ListCurve.mo [currentCurve]
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
                IActionHandler actionHandler = GetCurrentActionHandler();

                if (actionHandler != null)
                    action = actionHandler.ListScript[currentScript].ListAction[currentAction];

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
            RefreshActionView();
        }
        #endregion

        #region Script private methods
        private void InitScriptControl()
        {
            propAction.PropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(PropertyGrid_PropertyValueChanged);

            cmbActionType.SelectedIndex = 0;
            listboxScript.SelectedIndex = -1;
        }

        private void RefreshScriptView()
        {
            listboxScript.Items.Clear();
            txtScriptName.Clear();

            IActionHandler actionHandler = GetCurrentActionHandler();

            if (actionHandler != null)
            {
                for (int i = 0; i < actionHandler.ListScript.Count; i++)
                {
                    listboxScript.Items.Add(actionHandler.ListScript[i].ScriptName);
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

        private void RefreshActionView()
        {
            treeViewAction.Nodes.Clear();
            IActionHandler actionHandler = GetCurrentActionHandler();

            pnlCurve.Visible = false;
            pnlActionEvent.Visible = false;
            actionSoundControl.Visible = false;

            if (actionHandler != null && currentScript != -1)
            {
                ShowActionView();
                InitComboProperties();

                Script script = actionHandler.ListScript[currentScript];

                for (int i = 0; i < script.ListAction.Count; i++)
                {
                    ActionBase action = script.ListAction[i];

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
        }

        private void ViewActionCurve(ActionCurve actionCurve)
        {
            //--- Affiche le contrôle de courbe
            //pnlMain.ColumnStyles[1 + ID_ACTION_CURVE].SizeType = SizeType.AutoSize;
            //pnlMain.ColumnStyles[1 + ID_ACTION_EVENT].SizeType = SizeType.Percent;
            //pnlMain.ColumnStyles[1 + ID_ACTION_SOUND].SizeType = SizeType.Percent;

            //pnlMain.ColumnStyles[1 + ID_ACTION_CURVE].Width = 100;
            //pnlMain.ColumnStyles[1 + ID_ACTION_EVENT].Width = 0;
            //pnlMain.ColumnStyles[1 + ID_ACTION_SOUND].Width = 0;

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
                //chkCurve.Checked = true;
                //chkLoop.Checked = actionCurve.IsLoop;
                //optRelative.Checked = actionCurve.IsRelative;
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
                    //case ActionType.BlurFactor:
                    //    minY = 0f;
                    //    maxY = 0.1f;
                    //    break;
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
            //pnlMain.ColumnStyles[1 + ID_ACTION_CURVE].SizeType = SizeType.Percent;
            //pnlMain.ColumnStyles[1 + ID_ACTION_EVENT].SizeType = SizeType.Percent;
            //pnlMain.ColumnStyles[1 + ID_ACTION_SOUND].SizeType = SizeType.Percent;

            //pnlMain.ColumnStyles[1 + ID_ACTION_CURVE].Width = 0;
            //pnlMain.ColumnStyles[1 + ID_ACTION_EVENT].Width = 100;
            //pnlMain.ColumnStyles[1 + ID_ACTION_SOUND].Width = 0;

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
            //pnlMain.ColumnStyles[1 + ID_ACTION_CURVE].SizeType = SizeType.Percent;
            //pnlMain.ColumnStyles[1 + ID_ACTION_EVENT].SizeType = SizeType.Percent;
            //pnlMain.ColumnStyles[1 + ID_ACTION_SOUND].SizeType = SizeType.AutoSize;

            //pnlMain.ColumnStyles[1 + ID_ACTION_CURVE].SizeType = 0;
            //pnlMain.ColumnStyles[1 + ID_ACTION_EVENT].SizeType = 0;

            pnlCurve.Visible = false;
            pnlActionEvent.Visible = false;
            actionSoundControl.Visible = true;
            //---
        }

        private IActionHandler GetCurrentActionHandler()
        {
            IActionHandler actionHandler = null;

            if (Repository == null)
                return null;

            if (Repository.CurrentEntite != null)
                actionHandler = Repository.CurrentEntite;
            else if (Repository.CurrentParticleSystem != null)
                actionHandler = Repository.CurrentParticleSystem;
            else if (Repository.CurrentScript != null)
                actionHandler = Repository.CurrentScript.ActionHandler;

            return actionHandler;
        }

        private ActionCurve GetCurrentActionCurve()
        {
            IActionHandler actionHandler = GetCurrentActionHandler();

            if (actionHandler != null &&
                currentScript != -1 &&
                currentAction != -1 &&
                actionHandler.ListScript.Count > currentScript &&
                actionHandler.ListScript[currentScript].ListAction.Count > currentAction &&
                actionHandler.ListScript[currentScript].ListAction[currentAction] is ActionCurve)
                return (ActionCurve)actionHandler.ListScript[currentScript].ListAction[currentAction];
            else
                return null;
        }

        private ActionEvent GetCurrentActionEvent()
        {
            IActionHandler actionHandler = GetCurrentActionHandler();

            if (currentScript != -1 && currentAction != -1)
                return (ActionEvent)actionHandler.ListScript[currentScript].ListAction[currentAction];
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
            for (int i = 0; i < Repository.listEntite.Count; i++)
            {
                Entite entite = Repository.listEntite[i];

                StopScript(entite);

                for (int k = 0; k < entite.ListParticleSystem.Count; k++)
                {
                    StopScript(entite.ListParticleSystem[k]);

                    for (int l = 0; l < entite.ListParticleSystem[k].ListParticleTemplate.Count; k++)
                    {
                        StopScript(entite.ListParticleSystem[k].ListParticleTemplate[l]);
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

            IActionHandler actionHandler = GetCurrentActionHandler();

            //if (Repository != null && Repository.CurrentEntite != null && cmbActionType.SelectedIndex != ID_ACTION_SOUND)
            //{
            //    properties = Repository.CurrentEntite.GetType().GetProperties();
            //}
            //else if (Repository != null && Repository.CurrentParticleSystem != null)
            //{
            //    properties = Repository.CurrentObject.GetType().GetProperties();
            //}

            if (actionHandler != null)
                properties = actionHandler.GetType().GetProperties();


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
            IActionHandler actionHandler = GetCurrentActionHandler();

            if (actionHandler == null)
                return null;

            //--- Calcul du nom du script
            string scriptName = String.Empty;

            if (String.IsNullOrEmpty(txtScriptName.Text))
            {
                scriptName = String.Format("Script{0}", actionHandler.ListScript.Count + 1);
            }
            else
            {
                scriptName = txtScriptName.Text;
            }

            if (actionHandler.ListScript.Exists(s => s.ScriptName == scriptName))
            {
                MessageBox.Show(String.Format("Le nom de script '{0}' existe déja", scriptName), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            //---

            //--- Création du nouveau script
            Script script = new Script(scriptName, actionHandler);

            actionHandler.ListScript.Add(script);
            //---

            //--- Rafraichissement de la liste des scripts et de l'arborescence
            RefreshScriptView();
            RefreshGlobalTreeView();
            //---

            //--- Sélectionne le nouveau script
            listboxScript.SelectedIndex = listboxScript.Items.Count - 1;
            //---

            return script;
        }

        public Script GetSelectedScript()
        {
            Script script = null;

            IActionHandler actionHandler = GetCurrentActionHandler();

            if (actionHandler != null && currentScript != -1)
            {
                script = actionHandler.ListScript[currentScript];
            }

            return script;
        }

        public void RefreshScriptControl(bool selectScript)
        {
            currentScript = -1;
            currentAction = -1;
            currentSubAction = -1;

            ActionBase action = null;
            RefreshScriptView();

            IActionHandler actionHandler = GetCurrentActionHandler();

            if (actionHandler == null)
            {
                this.Visible = false;
                return;
            }
            else
            {
                this.Visible = true;
            }

            if (selectScript)
            {
                if (Repository.CurrentScript != null)
                {
                    listboxScript.SelectedIndex = listboxScript.FindString(Repository.CurrentScript.ScriptName);
                }
                else if (actionHandler.ListScript.Count > 0)
                {
                    listboxScript.SelectedIndex = 0;
                }
                else
                {
                    RefreshActionView();
                    RefreshGlobalTreeView<IActionHandler>(actionHandler);
                }
            }

            //if (actionHandler != null && actionHandler.ListScript.Count > 0)// && repository.currentEntite.ListScript[0].ListCurve.Count > 0)
            //{
            //    currentScript = 0;
            //    listboxScript.SelectedIndex = 0;
            //}

            //RefreshActionView();
            //if (actionHandler != null && actionHandler.ListScript.Count > 0 && actionHandler.ListScript[0].ListAction.Count > 0)
            //{
            //    currentAction = 0;
            //    action = actionHandler.ListScript[0].ListAction[0];
            //    treeViewAction.SelectedNode = treeViewAction.Nodes[0];
            //}
            //else
            //{
            //    InitComboProperties();
            //}

            //if (action is ActionCurve)
            //    ViewActionCurve((ActionCurve)action);
            //else if (action is ActionEvent)
            //    ViewActionEvent((ActionEvent)action);
            //else if (action is ActionSound)
            //    ViewActionSound((ActionSound)action);
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
