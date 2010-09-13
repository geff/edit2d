using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;
using Microsoft.Xna.Framework;
using Edit2DEngine.Entities.Particles;
using Edit2DEngine;
using Edit2D.UC;
using Edit2DEngine.Entities;
using Edit2DEngine.Triggers;

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
            //--- Initialise les arborescences
            treeViewValueChanged.ItemTypeShowed = TreeViewLocalItemType.EntityProperties | TreeViewLocalItemType.CustomProperties;
            treeViewValueChanged.ItemTypeCheckBoxed = TreeViewLocalItemType.EntityProperties | TreeViewLocalItemType.CustomProperties;
            treeViewValueChanged.AllowMultipleItemChecked = false;
            treeViewValueChanged.AllowUncheckedNode = true;

            treeViewCollision.ItemTypeShowed = TreeViewLocalItemType.Entity;
            treeViewCollision.ItemTypeCheckBoxed = TreeViewLocalItemType.Entity;
            treeViewCollision.AllowMultipleItemChecked = false;
            treeViewCollision.AllowUncheckedNode = true;

            treeViewScript.ItemTypeShowed = TreeViewLocalItemType.Script;
            treeViewScript.ItemTypeCheckBoxed = TreeViewLocalItemType.Script;
            treeViewScript.AllowMultipleItemChecked = true;
            treeViewScript.AllowUncheckedNode = true;
            //---
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
            DeleteCurrentTrigger();
        }

        private void btnDelTrigger_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                DeleteCurrentTrigger();
            }
        }

        private void btnChangeTriggerName_Click(object sender, EventArgs e)
        {
            ChangeCurrentTriggerName();
        }

        private void btnChangeTriggerName_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                ChangeCurrentTriggerName();
            }
        }

        private void listboxTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Repository.CurrentTriggerHandler == null || listboxTrigger.SelectedIndex == -1)
                return;

            TriggerBase trigger = Repository.CurrentTriggerHandler.ListTrigger[listboxTrigger.SelectedIndex];
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

                if (Repository.CurrentTrigger != null && Repository.CurrentTrigger is TriggerCollision)
                    treeViewCollision.RefreshView<ITriggerCollisionHandler>(((TriggerCollision)Repository.CurrentTrigger).TargetEntity);
                else
                {
                    treeViewCollision.RefreshView(false);
                    UpdateTrigger();
                }
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

                if (Repository.CurrentTrigger != null && Repository.CurrentTrigger is TriggerCollision)
                    treeViewCollision.RefreshView<ITriggerCollisionHandler>(((TriggerCollision)Repository.CurrentTrigger).TargetEntity);
                else
                {
                    treeViewCollision.RefreshView(false);
                    UpdateTrigger();
                }
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

                if (Repository.CurrentTrigger != null && Repository.CurrentTrigger is TriggerValueChanged)
                    treeViewValueChanged.RefreshView<PropertyInfo>(((TriggerValueChanged)Repository.CurrentTrigger).TriggerProperty);
                else
                {
                    treeViewValueChanged.RefreshView(false);
                    ShowPropertyDetail(null);

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

                if (!(Repository.CurrentTrigger != null && Repository.CurrentTrigger is TriggerMouse))
                {
                    optMouseLeftClick.Checked = true;

                    UpdateTrigger();
                }
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

                if (!(Repository.CurrentTrigger != null && Repository.CurrentTrigger is TriggerLoad))
                {
                    UpdateTrigger();
                }
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

                if (!(Repository.CurrentTrigger != null && Repository.CurrentTrigger is TriggerTime))
                {
                    optTimeLoopAlways.Checked = true;
                    numTimeLoop.Value = 1000;

                    UpdateTrigger();
                }
            }
        }

        private void treeViewCollision_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (treeViewCollision.IsCheckedByMouse)
            {
                UpdateTrigger();
            }
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

        private void ShowPropertyDetail(PropertyInfo propInfo)
        {
            TriggerValueChanged trigger = null;

            if (Repository.CurrentTriggerHandler.ListTrigger[listboxTrigger.SelectedIndex] is TriggerValueChanged)
                trigger = (TriggerValueChanged)Repository.CurrentTriggerHandler.ListTrigger[listboxTrigger.SelectedIndex];

            //--- Initialisation des valeurs par défaut
            txtProp1.Text = "0";
            txtProp2.Text = "0";
            txtProp3.Text = "0";

            cmbProp1.SelectedIndex = 0;
            cmbProp2.SelectedIndex = 0;
            cmbProp3.SelectedIndex = 0;

            lblProp1.Visible = false;
            lblProp2.Visible = false;
            lblProp3.Visible = false;

            txtProp1.Visible = false;
            txtProp2.Visible = false;
            txtProp3.Visible = false;

            cmbProp1.Visible = false;
            cmbProp2.Visible = false;
            cmbProp3.Visible = false;
            //---

            //--- Met à jour le trigger dans le triggerHandler (l'initialisation des valeurs par défaut
            //    a entrainé la création d'une nouvelle référence via UpdateTrigger
            if (trigger != null)
                Repository.CurrentTriggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = trigger;
            //---

            if (propInfo == null)
                return;

            if (propInfo.PropertyType.Name == "Vector2")
            {
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
            treeViewScript.RefreshView(false);
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
            //Repository.CurrentTrigger = trigger;

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
                treeViewCollision.CheckNode<ITriggerCollisionHandler>(triggerCol.TargetEntity);
                //---
            }
            else if (trigger is TriggerValueChanged)
            {
                TriggerValueChanged triggerVal = (TriggerValueChanged)trigger;

                //--- Type de trigger
                optTypeTriggerValueOverflow.Checked = true;
                //---

                //--- Trigger ValueChanged
                if (triggerVal.IsCustomProperty && !String.IsNullOrEmpty(triggerVal.PropertyName))
                {
                }
                else if (!triggerVal.IsCustomProperty)
                {
                    if (String.IsNullOrEmpty(triggerVal.PropertyName))
                    {
                        treeViewValueChanged.RefreshView(false);
                        ShowPropertyDetail(null);
                    }
                    else
                    {
                        treeViewValueChanged.CheckNode<PropertyInfo>(triggerVal.TriggerProperty, triggerVal.Entity.TreeViewPath);
                        ShowPropertyDetail(triggerVal.TriggerProperty);
                    }
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
                optTypeTriggerTime_CheckedChanged(null, null);
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
            if (Repository.CurrentTriggerHandler != null && Repository.CurrentTrigger != null)
            {
                TriggerBase newTrigger = null;

                //--- Recherche des scripts cochés
                Repository.CurrentTrigger.ListScript = treeViewScript.GetCheckedNodes<Script>();
                //---

                if (optTypeTriggerCollision.Checked)
                {
                    List<ITriggerCollisionHandler> listEntity = treeViewCollision.GetCheckedNodes<ITriggerCollisionHandler>();

                    ITriggerCollisionHandler targetEntityCollision = null;

                    if (listEntity.Count > 0)
                        targetEntityCollision = listEntity[0];

                    newTrigger = new TriggerCollision(Repository.CurrentTrigger.TriggerName, (ITriggerCollisionHandler)Repository.CurrentTrigger.TriggerHandler, targetEntityCollision);
                    newTrigger.ListScript = Repository.CurrentTrigger.ListScript;

                    Repository.CurrentTriggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
                if (optTypeTriggerValueOverflow.Checked)
                {
                    Type propertyType = null;
                    String propertyName = String.Empty;
                    bool isCustomProperty = false;
                    TriggerValueChangedSens[] sens = null;
                    Object values = null;

                    List<PropertyInfo> listProperties = treeViewValueChanged.GetCheckedNodes<PropertyInfo>();

                    //--- Détection du type et du nom de la propriété
                    if (listProperties.Count > 0)
                    {
                        PropertyInfo propInfo = listProperties[0];

                        propertyType = propInfo.PropertyType;
                        propertyName = propInfo.Name;
                    }
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
                    newTrigger = new TriggerValueChanged(Repository.CurrentTrigger.TriggerName, Repository.CurrentTrigger.TriggerHandler, propertyName, sens, values, isCustomProperty);
                    newTrigger.ListScript = Repository.CurrentTrigger.ListScript;

                    Repository.CurrentTriggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
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

                    newTrigger = new TriggerMouse(Repository.CurrentTrigger.TriggerName, (ITriggerMouseHandler)Repository.CurrentTrigger.TriggerHandler, triggerMousetype);
                    newTrigger.ListScript = Repository.CurrentTrigger.ListScript;
                    Repository.CurrentTriggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
                else if (optTypeTriggerLoading.Checked)
                {
                    newTrigger = new TriggerLoad(Repository.CurrentTrigger.TriggerName, Repository.CurrentTrigger.TriggerHandler);
                    newTrigger.ListScript = Repository.CurrentTrigger.ListScript;
                    Repository.CurrentTriggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
                else if (optTypeTriggerTime.Checked)
                {
                    newTrigger = new TriggerTime(Repository.CurrentTrigger.TriggerName, Repository.CurrentTrigger.TriggerHandler);
                    newTrigger.ListScript = Repository.CurrentTrigger.ListScript;

                    if (optTimeLoopAlways.Checked)
                        ((TriggerTime)newTrigger).TimeLoop = 0;
                    else
                        ((TriggerTime)newTrigger).TimeLoop = (int)numTimeLoop.Value;

                    Repository.CurrentTriggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
            }
        }
        #endregion

        #region Public methods
        public TriggerBase AddTriggerToCurrentEntity()
        {
            if (Repository.CurrentTriggerHandler == null)
                return null;

            //--- Calcul du nom du déclencheur
            string triggerName = String.Empty;

            if (String.IsNullOrEmpty(txtTriggerName.Text))
            {
                triggerName = Common.CreateNewName<TriggerBase>(Repository.CurrentTriggerHandler.ListTrigger, "TriggerName", "Trigger{0}");
            }
            else
            {
                triggerName = txtTriggerName.Text;
            }

            if (Repository.CurrentTriggerHandler.ListTrigger.Exists(s => s.TriggerName == triggerName))
            {
                MessageBox.Show(String.Format("Le nom du déclencheur '{0}' existe déja", triggerName), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            //---

            //--- Création du trigger modèle
            TriggerTime trigger = new TriggerTime(triggerName, Repository.CurrentTriggerHandler);
            trigger.TimeLoop = 0;

            Repository.CurrentTriggerHandler.ListTrigger.Add(trigger);
            //---

            //--- Rafraichissement de la liste des déclencheurs et de l'arborescence
            RefreshTriggerControl(false);
            RefreshGlobalTreeView();
            //---

            //--- Sélectionne le nouveau déclencheur
            listboxTrigger.SelectedIndex = listboxTrigger.Items.Count - 1;
            //---

            return trigger;
        }

        public void DeleteCurrentTrigger()
        {
            if (Repository.CurrentTriggerHandler != null && Repository.CurrentTrigger != null)
            {
                //---> Supprime le trigger de son propriétaire
                Repository.CurrentTriggerHandler.ListTrigger.Remove(Repository.CurrentTrigger);

                //---> Supprime le trigger de la sélection
                Repository.ListSelection.RemoveAll(s => s.Trigger == Repository.CurrentTrigger); ;

                //---> Rafraichi le contrôle en sélectionnant le premier trigger
                RefreshTriggerControl(true);
            }
        }

        public void ChangeCurrentTriggerName()
        {
            if (Repository.CurrentTrigger != null &&
                !String.IsNullOrEmpty(txtTriggerName.Text) &&
                txtTriggerName.Text != Repository.CurrentTrigger.TriggerName
                )
            {
                if (Repository.CurrentTriggerHandler.ListTrigger.Exists(t => t.TriggerName == txtTriggerName.Text))
                {
                    MessageBox.Show(String.Format("Le nom du déclencheur '{0}' existe déja", txtTriggerName.Text), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int previousIndexSelected = listboxTrigger.SelectedIndex;
                    Repository.CurrentTrigger.TriggerName = txtTriggerName.Text;

                    RefreshTriggerControl(false);
                    RefreshGlobalTreeView();

                    listboxTrigger.SelectedIndex = previousIndexSelected;
                }
            }
        }

        public void RefreshTriggerControl(bool selectTrigger)
        {
            listboxTrigger.Items.Clear();
            txtTriggerName.Clear();

            if (Repository.CurrentTriggerHandler == null)
                return;

            for (int i = 0; i < Repository.CurrentTriggerHandler.ListTrigger.Count; i++)
            {
                listboxTrigger.Items.Add(Repository.CurrentTriggerHandler.ListTrigger[i].TriggerName);
            }

            //--- Afffiche les types de trigger selon les spécificités du triggerHandler
            optTypeTriggerValueOverflow.Visible = (Repository.CurrentTriggerHandler is ITriggerValueChangedHandler);
            optTypeTriggerCollision.Visible = (Repository.CurrentTriggerHandler is ITriggerCollisionHandler);
            optTypeTriggerNoCollision.Visible = (Repository.CurrentTriggerHandler is ITriggerCollisionHandler);
            //---

            if (selectTrigger)
            {
                if (Repository.CurrentTrigger != null)
                {
                    RefreshGlobalTreeView<TriggerBase>(Repository.CurrentTrigger);
                    int index = listboxTrigger.FindString(Repository.CurrentTrigger.TriggerName);
                    listboxTrigger.SelectedIndex = index;
                }
                //---> Si le triggerHandler contient des déclencheurs, alors
                //      L'arborescence est rafraichie
                //      Le premier déclencheur est sélectionné
                else if (Repository.CurrentTriggerHandler != null &&
                         Repository.CurrentTriggerHandler.ListTrigger.Count > 0)
                {
                    RefreshGlobalTreeView(false);
                    listboxTrigger.SelectedIndex = 0;
                }
                //---> Si le triggerHandler ne contient pas de déclencheurs, alors
                //      L'arborescence est rafraichie en sélectionnant le triggerHandler
                //      L'interface est vidée
                else
                {
                    RefreshGlobalTreeView<ITriggerHandler>(Repository.CurrentTriggerHandler);
                    SelectTrigger(null);
                }
            }
        }
        #endregion
    }
}
