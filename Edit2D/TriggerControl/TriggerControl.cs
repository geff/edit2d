using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Edit2DEngine.Trigger;
using System.Reflection;
using Microsoft.Xna.Framework;
using Edit2DEngine.Particles;
using Edit2DEngine;
using Edit2D.UC;

namespace Edit2D.TriggerControl
{
    public partial class TriggerControl : UserControlLocal
    {
        #region Propriétés
        private Repository _repository;
        public Repository Repository
        {
            get
            {
                return _repository;
            }
            set
            {
                _repository = value;

                treeViewScript.Repository = _repository;
                treeViewCollision.Repository = _repository;
                treeViewValueChanged.Repository = _repository;
            }
        } 
        #endregion

        public TriggerControl()
        {
            InitializeComponent();
        }

        #region Évènements
        private void TriggerControl_Load(object sender, EventArgs e)
        {
            treeViewValueChanged.ItemTypeShowed = TreeViewLocalItemType.EntityProperties | TreeViewLocalItemType.CustomProperties;
            treeViewValueChanged.ItemTypeCheckBoxed = TreeViewLocalItemType.EntityProperties | TreeViewLocalItemType.CustomProperties;
            treeViewValueChanged.AllowMultipleItemChecked = false;

            treeViewCollision.ItemTypeShowed = TreeViewLocalItemType.Entity;
            treeViewCollision.ItemTypeCheckBoxed = TreeViewLocalItemType.Entity;
            treeViewScript.AllowMultipleItemChecked = false;

            treeViewScript.ItemTypeShowed = TreeViewLocalItemType.Script;
            treeViewScript.ItemTypeCheckBoxed = TreeViewLocalItemType.Script;
            treeViewScript.AllowMultipleItemChecked = true;
        }

        private void txtTriggerName_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                txtTriggerName.Text = String.Empty;
            }
        }

        private void btnAddTrigger_Click(object sender, EventArgs e)
        {
            AddTriggerToCurrentEntity();
        }

        private void btnAddTrigger_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                txtTriggerName.Text = String.Empty;
                AddTriggerToCurrentEntity();
            }
        }

        private void btnDelTrigger_Click(object sender, EventArgs e)
        {
            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (triggerHandler != null && listboxTrigger.SelectedIndex != -1)
            {
                triggerHandler.ListTrigger.RemoveAt(listboxTrigger.SelectedIndex);

                RefreshTriggerList();
                RefreshGlobalTreeView();

                if (triggerHandler.ListTrigger.Count > 0)
                    listboxTrigger.SelectedIndex = 0;
            }
        }

        private void btnChangeTriggerName_Click(object sender, EventArgs e)
        {
            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (triggerHandler != null &&
                listboxTrigger.SelectedIndex != -1 &&
                !String.IsNullOrEmpty(txtTriggerName.Text) &&
                txtTriggerName.Text != triggerHandler.ListTrigger[listboxTrigger.SelectedIndex].TriggerName
                )
            {

                if (triggerHandler.ListTrigger.Exists(t => t.TriggerName == txtTriggerName.Text))
                {
                    MessageBox.Show(String.Format("Le nom du déclencheur '{0}' existe déja", txtTriggerName.Text), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int previousIndexSelected = listboxTrigger.SelectedIndex;

                    triggerHandler.ListTrigger[listboxTrigger.SelectedIndex].TriggerName = txtTriggerName.Text;
                    RefreshTriggerList();

                    listboxTrigger.SelectedIndex = previousIndexSelected;
                }
            }
        }

        private void listboxTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (triggerHandler == null || listboxTrigger.SelectedIndex == -1)
                return;

            TriggerBase trigger = triggerHandler.ListTrigger[listboxTrigger.SelectedIndex];
            txtTriggerName.Text = trigger.TriggerName;

            SelectTrigger(trigger);
        }

        private void optTypeTriggerCollision_CheckedChanged(object sender, EventArgs e)
        {
            if (optTypeTriggerCollision.Checked)
            {
                pnlEntityCollision.Visible = true;
                pnlValueOverflow.Visible = false;
                pnlMouse.Visible = false;
                pnlTime.Visible = false;

                pnlScript.Left = pnlEntityCollision.Right;

                TriggerBase trigger = GetCurrentTrigger();

                if (trigger != null && trigger is TriggerCollision)
                    treeViewCollision.RefreshView<Entite>(((TriggerCollision)trigger).TargetEntite);
                else
                    treeViewCollision.RefreshView();

                UpdateTrigger();
            }
        }

        private void optTypeTriggerNoCollision_CheckedChanged(object sender, EventArgs e)
        {
            if (optTypeTriggerNoCollision.Checked)
            {
                pnlEntityCollision.Visible = true;
                pnlValueOverflow.Visible = false;
                pnlMouse.Visible = false;
                pnlTime.Visible = false;

                pnlScript.Left = pnlEntityCollision.Right;

                TriggerBase trigger = GetCurrentTrigger();

                if (trigger != null && trigger is TriggerCollision)
                    treeViewCollision.RefreshView<Entite>(((TriggerCollision)trigger).TargetEntite);
                else
                    treeViewCollision.RefreshView();

                UpdateTrigger();
            }
        }

        private void optTypeTriggerValueOverflow_CheckedChanged(object sender, EventArgs e)
        {
            if (optTypeTriggerValueOverflow.Checked)
            {
                pnlEntityCollision.Visible = false;
                pnlValueOverflow.Visible = true;
                pnlMouse.Visible = false;
                pnlTime.Visible = false;

                pnlScript.Left = pnlValueOverflow.Right;

                TriggerBase trigger = GetCurrentTrigger();

                if (trigger != null && trigger is TriggerValueChanged)
                    treeViewValueChanged.RefreshView<PropertyInfo>(((TriggerValueChanged)trigger).TriggerProperty);
                else
                {
                    treeViewValueChanged.RefreshView();
                    UpdateTrigger();
                }
            }
        }

        private void optTypeTriggerMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (optTypeTriggerMouse.Checked)
            {
                pnlEntityCollision.Visible = false;
                pnlValueOverflow.Visible = false;
                pnlMouse.Visible = true;
                pnlTime.Visible = false;

                pnlScript.Left = pnlMouse.Right;

                UpdateTrigger();
            }
        }

        private void optTypeTriggerLoading_CheckedChanged(object sender, EventArgs e)
        {
            if (optTypeTriggerLoading.Checked)
            {
                pnlEntityCollision.Visible = false;
                pnlValueOverflow.Visible = false;
                pnlMouse.Visible = false;
                pnlTime.Visible = false;

                pnlScript.Left = pnlTypeTrigger.Right;

                UpdateTrigger();
            }
        }

        private void optTypeTriggerTime_CheckedChanged(object sender, EventArgs e)
        {
            if (optTypeTriggerTime.Checked)
            {
                pnlEntityCollision.Visible = false;
                pnlValueOverflow.Visible = false;
                pnlMouse.Visible = false;
                pnlTime.Visible = true;

                pnlScript.Left = pnlTime.Right;

                UpdateTrigger();
            }
        }

        private void treeViewCollision_AfterCheck(object sender, TreeViewEventArgs e)
        {
            UpdateTrigger();
        }

        private void treeViewValueChanged_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (treeViewValueChanged.IsCheckedByMouse)
            {
                List<PropertyInfo> listProperties = treeViewValueChanged.GetCheckedNodes<PropertyInfo>();

                if (listProperties.Count > 0)
                    ShowPropertyDetail(listProperties[0]);

                UpdateTrigger();
            }
        }

        private void cmbProp1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void cmbProp2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void cmbProp3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void txtProp1_TextChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void txtProp2_TextChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void txtProp3_TextChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void treeViewProperties_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            //{
            //    if (e.Node.Tag is PropertyInfo)
            //    {
            //        //--- Décoche les custom properties
            //        if (treeViewCustomProperties.Nodes != null & treeViewCustomProperties.Nodes.Count > 0)
            //            ChangeNodeCheck(treeViewCustomProperties.Nodes[0], false);
            //        //---

            //        bool isChecked = e.Node.Checked;
            //        for (int i = 0; i < treeViewProperties.Nodes.Count; i++)
            //        {
            //            ChangeNodeCheck(treeViewProperties, false);
            //        }

            //        ShowPropertyDetail((PropertyInfo)e.Node.Tag);
            //        e.Node.Checked = isChecked;
            //    }
            //    else
            //    {
            //        e.Node.Checked = false;
            //    }

            //    //--- Visibilité du panneau de saisie des valeurs
            //    pnlValueProp.Visible = GetCheckedNodesCount(treeViewProperties) + GetCheckedNodesCount(treeViewCustomProperties) > 0;
            //    //---
            //}

            //UpdateTrigger();
        }

        private void treeViewCustomProperties_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            //{
            //    if (e.Node.Tag is String)
            //    {
            //        //--- Décoche les properties
            //        if (treeViewProperties.Nodes != null & treeViewProperties.Nodes.Count > 0)
            //            ChangeNodeCheck(treeViewProperties.Nodes[0], false);
            //        //---

            //        bool isChecked = e.Node.Checked;
            //        for (int i = 0; i < treeViewCustomProperties.Nodes.Count; i++)
            //        {
            //            ChangeNodeCheck(treeViewCustomProperties.Nodes[i], false);
            //        }

            //        //TODO
            //        //ShowPropertyDetail((PropertyInfo)e.Node.Tag);
            //        e.Node.Checked = isChecked;
            //    }
            //    else
            //    {
            //        e.Node.Checked = false;
            //    }
            //}

            ////--- Visibilité du panneau de saisie des valeurs
            //pnlValueProp.Visible = GetCheckedNodesCount(treeViewProperties) + GetCheckedNodesCount(treeViewCustomProperties) > 0;
            ////---

            //UpdateTrigger();
        }

        private void optMouseRightClick_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void optMouseLeftClick_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void optMouseEnter_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void optMouseLeave_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void optMouseStayOver_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void optTimeLoopAlways_CheckedChanged(object sender, EventArgs e)
        {
            numTimeLoop.Enabled = false;
            UpdateTrigger();
        }

        private void optTimeLoopParam_CheckedChanged(object sender, EventArgs e)
        {
            numTimeLoop.Enabled = true;
            UpdateTrigger();
        }

        private void numTimeLoop_ValueChanged(object sender, EventArgs e)
        {
            UpdateTrigger();
        }

        private void treeViewLocal_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (treeViewScript.IsCheckedByMouse)
            {
                UpdateTrigger();
            }
        }
        #endregion

        #region Private methods
        private ITriggerHandler GetCurrentTriggerHandler()
        {
            ITriggerHandler triggerHandler = null;

            if (Repository.CurrentEntite != null)
                triggerHandler = Repository.CurrentEntite;
            else if (Repository.CurrentObject != null && Repository.CurrentObject is ITriggerHandler)
                triggerHandler = (ITriggerHandler)Repository.CurrentObject;

            return triggerHandler;
        }

        private TriggerBase GetCurrentTrigger()
        {
            TriggerBase trigger = null;

            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (triggerHandler != null && listboxTrigger.SelectedIndex != -1)
                trigger = triggerHandler.ListTrigger[listboxTrigger.SelectedIndex];

            return trigger;
        }

        private void ShowPropertyDetail(PropertyInfo propInfo)
        {
            TriggerValueChanged trigger = null;

            TriggerBase triggerBase = GetCurrentTrigger();
            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (Repository.CurrentEntite.ListTrigger[listboxTrigger.SelectedIndex] is TriggerValueChanged)
                trigger = (TriggerValueChanged)Repository.CurrentEntite.ListTrigger[listboxTrigger.SelectedIndex];

            //--- Initialisation des valeurs par défaut
            txtProp1.Text = "0";
            txtProp2.Text = "0";
            txtProp3.Text = "0";

            cmbProp1.SelectedIndex = 0;
            cmbProp2.SelectedIndex = 0;
            cmbProp3.SelectedIndex = 0;
            //---

            if (propInfo.PropertyType.Name == "Vector2")
            {
                //sens = new TriggerValueChangedSens[] { (TriggerValueChangedSens)cmbProp1.SelectedIndex, (TriggerValueChangedSens)cmbProp2.SelectedIndex };
                //values = new Vector2(float.Parse(txtProp1.Text), float.Parse(txtProp2.Text));
                if (trigger != null && trigger.Sens != null)
                {
                    cmbProp1.SelectedIndex = (int)trigger.Sens[0];
                    cmbProp2.SelectedIndex = (int)trigger.Sens[1];

                    txtProp1.Text = ((Vector2)trigger.Value).X.ToString();
                    txtProp2.Text = ((Vector2)trigger.Value).Y.ToString();
                }

                lblProp1.Visible = true;
                lblProp2.Visible = true;
                lblProp3.Visible = false;

                lblProp1.Text = "X";
                lblProp2.Text = "Y";
                lblProp3.Text = "";

                cmbProp1.Visible = true;
                cmbProp2.Visible = true;
                cmbProp3.Visible = false;

                txtProp1.Visible = true;
                txtProp2.Visible = true;
                txtProp3.Visible = false;
            }
            else if (propInfo.PropertyType.Name == "Single")
            {
                //sens = new TriggerValueChangedSens[] { (TriggerValueChangedSens)cmbProp1.SelectedIndex };
                //values = float.Parse(txtProp1.Text);

                if (trigger != null && trigger.Sens != null)
                {
                    cmbProp1.SelectedIndex = (int)trigger.Sens[0];

                    txtProp1.Text = ((float)trigger.Value).ToString();
                }

                lblProp1.Visible = true;
                lblProp2.Visible = false;
                lblProp3.Visible = false;

                lblProp1.Text = propInfo.Name;
                lblProp2.Text = "";
                lblProp3.Text = "";

                cmbProp1.Visible = true;
                cmbProp2.Visible = false;
                cmbProp3.Visible = false;

                txtProp1.Visible = true;
                txtProp2.Visible = false;
                txtProp3.Visible = false;
            }
            else if (propInfo.PropertyType.Name == "Color")
            {
                //sens = new TriggerValueChangedSens[] { (TriggerValueChangedSens)cmbProp1.SelectedIndex, (TriggerValueChangedSens)cmbProp2.SelectedIndex, (TriggerValueChangedSens)cmbProp3.SelectedIndex };
                //Microsoft.Xna.Framework.Graphics.Color color = new Microsoft.Xna.Framework.Graphics.Color();

                //TODO : ajouter l'alpha
                //color = new Microsoft.Xna.Framework.Graphics.Color(byte.Parse(txtProp1.Text), byte.Parse(txtProp2.Text), byte.Parse(txtProp3.Text));
                //values = color;

                if (trigger != null && trigger.Sens != null)
                {
                    cmbProp1.SelectedIndex = (int)trigger.Sens[0];
                    cmbProp2.SelectedIndex = (int)trigger.Sens[1];
                    cmbProp3.SelectedIndex = (int)trigger.Sens[2];

                    txtProp1.Text = ((Microsoft.Xna.Framework.Graphics.Color)trigger.Value).R.ToString();
                    txtProp2.Text = ((Microsoft.Xna.Framework.Graphics.Color)trigger.Value).G.ToString();
                    txtProp3.Text = ((Microsoft.Xna.Framework.Graphics.Color)trigger.Value).B.ToString();
                }

                lblProp1.Visible = true;
                lblProp2.Visible = true;
                lblProp3.Visible = true;

                lblProp1.Text = "R";
                lblProp2.Text = "G";
                lblProp3.Text = "B";

                cmbProp1.Visible = true;
                cmbProp2.Visible = true;
                cmbProp3.Visible = true;

                txtProp1.Visible = true;
                txtProp2.Visible = true;
                txtProp3.Visible = true;
            }
        }

        private void RefreshTreeViewScript(TriggerBase trigger)
        {
            //---
            treeViewScript.RefreshView();
            //---

            //--- Liste des scripts
            if (trigger != null)
            {
                treeViewScript.CheckNodes<Script>(trigger.ListScript);
            }
            //---
        }

        private void SelectTrigger(TriggerBase trigger)
        {
            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (trigger == null)
            {
                pnlTypeTrigger.Visible = false;
                pnlEntityCollision.Visible = false;
                pnlValueOverflow.Visible = false;
                pnlMouse.Visible = false;
                pnlTime.Visible = false;
                pnlScript.Visible = false;

                return;
            }
            else
            {
                pnlTypeTrigger.Visible = true;
                pnlScript.Visible = true;

                RefreshTreeViewScript(trigger);
                CheckNodeGlobalTreeView<TriggerBase>(trigger);
            }

            if (trigger is TriggerCollision)
            {
                TriggerCollision triggerCol = (TriggerCollision)trigger;

                //--- Type de trigger
                optTypeTriggerCollision.Checked = true;
                optTypeTriggerCollision_CheckedChanged(null, null);
                //---

                //--- Trigger collision
                //if (triggerCol.TargetEntite != null && treeviewEntiteTargetCollision.Nodes.Find(triggerCol.TargetEntite.Name, true).Length > 0)
                //{
                //    TreeNode treeNode = treeviewEntiteTargetCollision.Nodes.Find(triggerCol.TargetEntite.Name, true)[0];
                //    treeNode.Checked = true;
                //}
                treeViewCollision.CheckNode<Entite>(triggerCol.TargetEntite);
                //---
            }
            else if (trigger is TriggerValueChanged)
            {
                TriggerValueChanged triggerVal = (TriggerValueChanged)trigger;

                //--- Type de trigger
                optTypeTriggerValueOverflow.Checked = true;
                //optTypeTriggerValueOverflow_CheckedChanged(null, null);
                //---

                //--- Trigger ValueChanged
                if (triggerVal.IsCustomProperty && !String.IsNullOrEmpty(triggerVal.PropertyName))
                {
                    //TreeNode treeNode = treeViewCustomProperties.Nodes.Find(triggerVal.PropertyName, true)[0];
                    //treeNode.Checked = true;
                    //treeViewValueChanged.CheckNode<PropertyInfo>(triggerVal.TriggerProperty);
                }
                else if (!triggerVal.IsCustomProperty && !String.IsNullOrEmpty(triggerVal.PropertyName))
                {
                    //TreeNode treeNode = treeViewProperties.Nodes.Find(triggerVal.PropertyName, true)[0];
                    //treeNode.Checked = true;

                    treeViewValueChanged.CheckNode<PropertyInfo>(triggerVal.TriggerProperty, triggerVal.Entite.TreeViewPath);
                    //List<PropertyInfo> l = treeViewValueChanged.GetCheckedNodes<PropertyInfo>();
                    ShowPropertyDetail(triggerVal.TriggerProperty);
                }
                //---
            }
            else if (trigger is TriggerMouse)
            {
                TriggerMouse triggerMouse = (TriggerMouse)trigger;

                //--- Type de trigger
                optTypeTriggerMouse.Checked = true;
                optTypeTriggerMouse_CheckedChanged(null, null);
                //---

                //--- Trigger Mouse
                switch (triggerMouse.TriggerMouseType)
                {
                    case TriggerMouseType.MouseRightClick:
                        optMouseRightClick.Checked = true;
                        break;
                    case TriggerMouseType.MouseLeftClick:
                        optMouseLeftClick.Checked = true;
                        break;
                    case TriggerMouseType.MouseEnter:
                        optMouseEnter.Checked = true;
                        break;
                    case TriggerMouseType.MouseLeave:
                        optMouseLeave.Checked = true;
                        break;
                    case TriggerMouseType.MouseOver:
                        optMouseStayOver.Checked = true;
                        break;
                    default:
                        break;
                }
                //---
            }
            else if (trigger is TriggerLoad)
            {
                TriggerLoad triggerLoad = (TriggerLoad)trigger;

                //--- Type de trigger
                optTypeTriggerLoading.Checked = true;
                optTypeTriggerLoading_CheckedChanged(null, null);
                //---
            }
            else if (trigger is TriggerTime)
            {
                TriggerTime triggerTime = (TriggerTime)trigger;

                //--- Type de trigger
                optTypeTriggerTime.Checked = true;
                //optTypeTriggerTime_CheckedChanged(null, null);
                //---

                //--- Trigger Time
                if (triggerTime.TimeLoop == 0)
                {
                    optTimeLoopAlways.Checked = true;
                }
                else
                {
                    optTimeLoopParam.Checked = true;
                    numTimeLoop.Value = triggerTime.TimeLoop;
                }
                //---
            }
        }

        private void UpdateTrigger()
        {
            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (triggerHandler != null && listboxTrigger.SelectedIndex != -1)
            {
                TriggerBase trigger = triggerHandler.ListTrigger[listboxTrigger.SelectedIndex];
                TriggerBase newTrigger = null;

                //--- Recherche des scripts cochés
                trigger.ListScript = treeViewScript.GetCheckedNodes<Script>();
                //---

                 if (optTypeTriggerCollision.Checked)
                {
                    //if (node == null)
                    //{
                    //    MessageBox.Show("Vous-devez cocher au moins une entité de collision!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}

                    List<Entite> listEntite = treeViewCollision.GetCheckedNodes<Entite>();

                    Entite targetEntiteCollision = null;

                    if (listEntite.Count > 0)
                        targetEntiteCollision = listEntite[0];

                    newTrigger = new TriggerCollision(trigger.TriggerName, trigger.TriggerHandler, targetEntiteCollision);
                    newTrigger.ListScript = trigger.ListScript;

                    triggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
                if (optTypeTriggerValueOverflow.Checked)
                {
                    //TreeNode nodeProp = GetCheckedNode(treeViewProperties.TopNode);
                    //TreeNode nodeCustomProp = GetCheckedNode(treeViewCustomProperties.TopNode);
                    Type propertyType = null;
                    String propertyName = String.Empty;
                    bool isCustomProperty = false;
                    TriggerValueChangedSens[] sens = null;
                    Object values = null;

                    List<PropertyInfo> listProperties = treeViewValueChanged.GetCheckedNodes<PropertyInfo>();

                    //--- Détection du type et du nom de la propriété
                    if (listProperties.Count>0)
                    {
                        PropertyInfo propInfo = listProperties[0];

                        propertyType = propInfo.PropertyType;
                        propertyName = propInfo.Name;
                    }
                    //else if (nodeCustomProp != null)
                    //{
                    //    //isCustomProperty = true;

                    //    //Object customProp = ((Entite)trigger.TriggerHandler).ListCustomProperties[nodeCustomProp.Text];

                    //    //propertyType = customProp.GetType();
                    //    //propertyName = nodeCustomProp.Text;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Vous-devez cocher une propriété!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}
                    //---

                    if (propertyType != null && propertyType.Name == "Vector2")
                    {
                        sens = new TriggerValueChangedSens[] { (TriggerValueChangedSens)cmbProp1.SelectedIndex, (TriggerValueChangedSens)cmbProp2.SelectedIndex };
                        
                        float valueX = 0f;
                        float valueY = 0f;

                        float.TryParse(txtProp1.Text, out valueX);
                        float.TryParse(txtProp2.Text, out valueY);

                        values = new Vector2(valueX, valueY);
                    }
                    else if (propertyType != null && propertyType.Name == "Single")
                    {
                        sens = new TriggerValueChangedSens[] { (TriggerValueChangedSens)cmbProp1.SelectedIndex };

                        float value = 0f;

                        float.TryParse(txtProp1.Text, out value);

                        values = value;
                    }
                    else if (propertyType != null && propertyType.Name == "Color")
                    {
                        sens = new TriggerValueChangedSens[] { (TriggerValueChangedSens)cmbProp1.SelectedIndex, (TriggerValueChangedSens)cmbProp2.SelectedIndex, (TriggerValueChangedSens)cmbProp3.SelectedIndex };
                        Microsoft.Xna.Framework.Graphics.Color color = new Microsoft.Xna.Framework.Graphics.Color();

                        byte valueR = 0;
                        byte valueG = 0;
                        byte valueB = 0;

                        byte.TryParse(txtProp1.Text, out valueR);
                        byte.TryParse(txtProp2.Text, out valueG);
                        byte.TryParse(txtProp3.Text, out valueB);

                        //TODO : ajouter l'alpha
                        color = new Microsoft.Xna.Framework.Graphics.Color(valueR, valueG, valueB);
                        values = color;
                    }

                    //--- Enregistrement du trigger
                    newTrigger = new TriggerValueChanged(trigger.TriggerName, trigger.TriggerHandler, propertyName, sens, values, isCustomProperty);
                    newTrigger.ListScript = trigger.ListScript;

                    triggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                    //---
                }
                else if (optTypeTriggerMouse.Checked)
                {
                    TriggerMouseType triggerMousetype = TriggerMouseType.MouseRightClick;

                    if (optMouseRightClick.Checked)
                        triggerMousetype = TriggerMouseType.MouseRightClick;
                    else if (optMouseLeftClick.Checked)
                        triggerMousetype = TriggerMouseType.MouseLeftClick;
                    else if (optMouseEnter.Checked)
                        triggerMousetype = TriggerMouseType.MouseEnter;
                    else if (optMouseLeave.Checked)
                        triggerMousetype = TriggerMouseType.MouseLeave;
                    else if (optMouseStayOver.Checked)
                        triggerMousetype = TriggerMouseType.MouseOver;

                    newTrigger = new TriggerMouse(trigger.TriggerName, trigger.TriggerHandler, triggerMousetype);
                    newTrigger.ListScript = trigger.ListScript;
                    triggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
                else if (optTypeTriggerLoading.Checked)
                {
                    newTrigger = new TriggerLoad(trigger.TriggerName, trigger.TriggerHandler);
                    newTrigger.ListScript = trigger.ListScript;
                    triggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
                else if (optTypeTriggerTime.Checked)
                {
                    newTrigger = new TriggerTime(trigger.TriggerName, trigger.TriggerHandler);
                    newTrigger.ListScript = trigger.ListScript;

                    if (optTimeLoopAlways.Checked)
                        ((TriggerTime)newTrigger).TimeLoop = 0;
                    else
                        ((TriggerTime)newTrigger).TimeLoop = (int)numTimeLoop.Value;

                    triggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
            }
        }
        #endregion

        #region Public methods
        public TriggerBase AddTriggerToCurrentEntity()
        {
            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (triggerHandler == null)
                return null;

            //--- Calcul du nom du déclencheur
            string triggerName = String.Empty;

            if (String.IsNullOrEmpty(txtTriggerName.Text))
            {
                triggerName = String.Format("Trigger{0}", triggerHandler.ListTrigger.Count + 1);
            }
            else
            {
                triggerName = txtTriggerName.Text;
            }

            if (triggerHandler.ListTrigger.Exists(s => s.TriggerName == triggerName))
            {
                MessageBox.Show(String.Format("Le nom du déclencheur '{0}' existe déja", triggerName), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            //---

            //--- Création du trigger modèle
            TriggerTime trigger = new TriggerTime(triggerName, triggerHandler);
            trigger.TimeLoop = 0;

            triggerHandler.ListTrigger.Add(trigger);
            //---

            //--- Rafraichissement de la liste des déclencheurs et de l'arborescence
            RefreshTriggerList();
            RefreshGlobalTreeView();
            //---

            //--- Sélectionne le nouveau déclencheur
            listboxTrigger.SelectedIndex = listboxTrigger.Items.Count - 1;
            //---
            
            optTypeTriggerTime_CheckedChanged(null, null);

            return trigger;
        }

        public void RefreshTriggerList()
        {
            listboxTrigger.Items.Clear();
            ITriggerHandler triggerHandler = null;

            if (Repository.CurrentEntite != null)
                triggerHandler = Repository.CurrentEntite;
            else if (Repository.CurrentObject != null && Repository.CurrentObject is ITriggerHandler)
                triggerHandler = (ITriggerHandler)Repository.CurrentObject;

            if (triggerHandler == null)
            {
                SelectTrigger(null);
                return;
            }

            foreach (TriggerBase trigger in triggerHandler.ListTrigger)
            {
                listboxTrigger.Items.Add(trigger.TriggerName);
            }

            //if (triggerHandler.ListTrigger.Count > 0)
            //    listboxTrigger.SelectedIndex = 0;
            //else
            {
                SelectTrigger(null);
            }
        }
        #endregion
    }
}
