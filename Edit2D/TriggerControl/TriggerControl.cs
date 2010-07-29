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

namespace Edit2D.TriggerControl
{
    public partial class TriggerControl : UserControl
    {
        public Repository repository { get; set; }

        public TriggerControl()
        {
            InitializeComponent();
        }

        #region Events
        private void btnAddTrigger_Click(object sender, EventArgs e)
        {
            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (triggerHandler == null)
                return;

            TriggerCollision trigger = new TriggerCollision(String.Format("Trigger{0}", triggerHandler.ListTrigger.Count + 1), triggerHandler, null);

            triggerHandler.ListTrigger.Add(trigger);

            RefreshTriggerList();

            listboxTrigger.SelectedIndex = listboxTrigger.Items.Count - 1;
        }

        private void btnDelTrigger_Click(object sender, EventArgs e)
        {
            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (triggerHandler != null && listboxTrigger.SelectedIndex != -1)
            {
                triggerHandler.ListTrigger.RemoveAt(listboxTrigger.SelectedIndex);
                RefreshTriggerList();

                if (triggerHandler.ListTrigger.Count > 0)
                    listboxTrigger.SelectedIndex = 0;
            }
        }

        private void btnChangeTriggerName_Click(object sender, EventArgs e)
        {

        }

        private void listboxTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (triggerHandler == null || listboxTrigger.SelectedIndex == -1)
                return;

            TriggerBase trigger = triggerHandler.ListTrigger[listboxTrigger.SelectedIndex];

            SelectTrigger(trigger);
        }

        private void cmbTypeTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTypeTrigger.SelectedIndex == 0)
            {
                RefreshTreeViewEntite(treeviewEntiteTargetCollision);
            }
            else if (cmbTypeTrigger.SelectedIndex == 2)
            {
                RefreshTreeViewProperties();
                RefreshTreeViewCustomProperties(repository.CurrentEntite);

                pnlValueProp.Visible = false;
            }
            else if (cmbTypeTrigger.SelectedIndex == 3)
            {
                RefreshComboBoxMouseTrigger();
            }
            else if (cmbTypeTrigger.SelectedIndex == 4)
            {
            }
            else if (cmbTypeTrigger.SelectedIndex == 5)
            {
            }
        }

        private void optTypeTriggerCollision_CheckedChanged(object sender, EventArgs e)
        {
            pnlEntityCollision.Visible = true;
            pnlValueOverflow.Visible = false;
            pnlMouse.Visible = false;
            pnlTime.Visible = false;

            pnlScript.Left = pnlEntityCollision.Right;

            RefreshTreeViewEntite(treeviewEntiteTargetCollision);
        }

        private void optTypeTriggerNoCollision_CheckedChanged(object sender, EventArgs e)
        {
            pnlEntityCollision.Visible = true;
            pnlValueOverflow.Visible = false;
            pnlMouse.Visible = false;
            pnlTime.Visible = false;

            pnlScript.Left = pnlEntityCollision.Right;

            RefreshTreeViewEntite(treeviewEntiteTargetCollision);
        }

        private void optTypeTriggerValueOverflow_CheckedChanged(object sender, EventArgs e)
        {
            pnlEntityCollision.Visible = false;
            pnlValueOverflow.Visible = true;
            pnlMouse.Visible = false;
            pnlTime.Visible = false;

            pnlScript.Left = pnlValueOverflow.Right;

            RefreshTreeViewProperties();
            RefreshTreeViewCustomProperties(repository.CurrentEntite);
        }

        private void optTypeTriggerMouse_CheckedChanged(object sender, EventArgs e)
        {
            pnlEntityCollision.Visible = false;
            pnlValueOverflow.Visible = false;
            pnlMouse.Visible = true;
            pnlTime.Visible = false;

            pnlScript.Left = pnlMouse.Right;
        }

        private void optTypeTriggerLoading_CheckedChanged(object sender, EventArgs e)
        {
            pnlEntityCollision.Visible = false;
            pnlValueOverflow.Visible = false;
            pnlMouse.Visible = false;
            pnlTime.Visible = false;

            pnlScript.Left = pnlTypeTrigger.Right;
        }

        private void optTypeTriggerTime_CheckedChanged(object sender, EventArgs e)
        {
            pnlEntityCollision.Visible = false;
            pnlValueOverflow.Visible = false;
            pnlMouse.Visible = false;
            pnlTime.Visible = true;

            pnlScript.Left = pnlTime.Right;
        }

        private void treeviewEntiteTargetCollision_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                if (e.Node.Tag is Entite)
                {
                    bool isChecked = e.Node.Checked;
                    ChangeNodeCheck(treeviewEntiteTargetCollision.Nodes[0], false);
                    e.Node.Checked = isChecked;
                }
                else
                {
                    e.Node.Checked = false;
                }
            }
        }

        private void treeViewProperties_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                if (e.Node.Tag is PropertyInfo)
                {
                    //--- Décoche les custom properties
                    if (treeViewCustomProperties.Nodes != null & treeViewCustomProperties.Nodes.Count > 0)
                        ChangeNodeCheck(treeViewCustomProperties.Nodes[0], false);
                    //---

                    bool isChecked = e.Node.Checked;
                    for (int i = 0; i < treeViewProperties.Nodes.Count; i++)
                    {
                        ChangeNodeCheck(treeViewProperties, false);
                    }

                    ShowPropertyDetail((PropertyInfo)e.Node.Tag);
                    e.Node.Checked = isChecked;
                }
                else
                {
                    e.Node.Checked = false;
                }

                //--- Visibilité du panneau de saisie des valeurs
                pnlValueProp.Visible = GetCheckedNodesCount(treeViewProperties) + GetCheckedNodesCount(treeViewCustomProperties) > 0;
                //---
            }
        }

        private void treeViewCustomProperties_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                if (e.Node.Tag is String)
                {
                    //--- Décoche les properties
                    if (treeViewProperties.Nodes != null & treeViewProperties.Nodes.Count > 0)
                        ChangeNodeCheck(treeViewProperties.Nodes[0], false);
                    //---

                    bool isChecked = e.Node.Checked;
                    for (int i = 0; i < treeViewCustomProperties.Nodes.Count; i++)
                    {
                        ChangeNodeCheck(treeViewCustomProperties.Nodes[i], false);
                    }

                    //TODO
                    //ShowPropertyDetail((PropertyInfo)e.Node.Tag);
                    e.Node.Checked = isChecked;
                }
                else
                {
                    e.Node.Checked = false;
                }
            }

            //--- Visibilité du panneau de saisie des valeurs
            pnlValueProp.Visible = GetCheckedNodesCount(treeViewProperties) + GetCheckedNodesCount(treeViewCustomProperties) > 0;
            //---
        }

        private void optMouseRightClick_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optMouseLeftClick_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optMouseEnter_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optMouseLeave_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optMouseStayOver_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optTimeLoopAlways_CheckedChanged(object sender, EventArgs e)
        {
            numTimeLoop.Enabled = false;
        }

        private void optTimeLoopParam_CheckedChanged(object sender, EventArgs e)
        {
            numTimeLoop.Enabled = true;
        }

        private void treeviewEntiteScript_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                if (e.Node.Tag is Script)
                {
                    bool isChecked = e.Node.Checked;
                    //ChangeNodeCheck(treeviewEntiteScript.TopNode, false);
                    e.Node.Checked = isChecked;
                }
                else
                {
                    e.Node.Checked = false;
                }
            }
        }

        private void btnScriptToExecute_Click(object sender, EventArgs e)
        {
            ITriggerHandler triggerHandler = GetCurrentTriggerHandler();

            if (triggerHandler != null && listboxTrigger.SelectedIndex != -1)
            {
                TriggerBase trigger = triggerHandler.ListTrigger[listboxTrigger.SelectedIndex];
                TriggerBase newTrigger = null;

                //--- Recherche des scripts cochés
                List<TreeNode> scriptNodes = new List<TreeNode>();
                GetCheckedNodes(treeviewEntiteScript.Nodes[0], scriptNodes);

                if (scriptNodes.Count == 0)
                {
                    MessageBox.Show("Vous-devez cocher au moins un script!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                List<Script> listScript = new List<Script>();
                foreach (TreeNode scriptNode in scriptNodes)
                {
                    if (scriptNode.Tag is Script)
                        listScript.Add((Script)scriptNode.Tag);
                }
                //---

                if (cmbTypeTrigger.SelectedIndex == 0)
                {
                    TreeNode node = GetCheckedNode(treeviewEntiteTargetCollision.Nodes[0]);

                    if (node == null)
                    {
                        MessageBox.Show("Vous-devez cocher au moins une entité de collision!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    Entite targetEntiteCollision = (Entite)node.Tag;

                    newTrigger = new TriggerCollision(trigger.TriggerName, trigger.TriggerHandler, targetEntiteCollision);
                    newTrigger.ListScript = listScript;

                    triggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
                else if (cmbTypeTrigger.SelectedIndex == 2)
                {
                    TreeNode nodeProp = GetCheckedNode(treeViewProperties.TopNode);
                    TreeNode nodeCustomProp = GetCheckedNode(treeViewCustomProperties.TopNode);

                    Type propertyType = null;
                    String propertyName = String.Empty;
                    bool isCustomProperty = false;
                    TriggerValueChangedSens[] sens = null;
                    Object values = null;

                    //--- Détection du type et du nom de la propriété
                    if (nodeProp != null)
                    {
                        PropertyInfo propInfo = (PropertyInfo)nodeProp.Tag;

                        propertyType = propInfo.PropertyType;
                        propertyName = propInfo.Name;
                    }
                    else if (nodeCustomProp != null)
                    {
                        isCustomProperty = true;

                        Object customProp = ((Entite)trigger.TriggerHandler).ListCustomProperties[nodeCustomProp.Text];

                        propertyType = customProp.GetType();
                        propertyName = nodeCustomProp.Text;
                    }
                    else
                    {
                        MessageBox.Show("Vous-devez cocher une propriété!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //---

                    if (propertyType.Name == "Vector2")
                    {
                        sens = new TriggerValueChangedSens[] { (TriggerValueChangedSens)cmbProp1.SelectedIndex, (TriggerValueChangedSens)cmbProp2.SelectedIndex };
                        values = new Vector2(float.Parse(txtProp1.Text), float.Parse(txtProp2.Text));
                    }
                    else if (propertyType.Name == "Single")
                    {
                        sens = new TriggerValueChangedSens[] { (TriggerValueChangedSens)cmbProp1.SelectedIndex };
                        values = float.Parse(txtProp1.Text);
                    }
                    else if (propertyType.Name == "Color")
                    {
                        sens = new TriggerValueChangedSens[] { (TriggerValueChangedSens)cmbProp1.SelectedIndex, (TriggerValueChangedSens)cmbProp2.SelectedIndex, (TriggerValueChangedSens)cmbProp3.SelectedIndex };
                        Microsoft.Xna.Framework.Graphics.Color color = new Microsoft.Xna.Framework.Graphics.Color();

                        //TODO : ajouter l'alpha
                        color = new Microsoft.Xna.Framework.Graphics.Color(byte.Parse(txtProp1.Text), byte.Parse(txtProp2.Text), byte.Parse(txtProp3.Text));
                        values = color;
                    }

                    //--- Enregistrement du trigger
                    newTrigger = new TriggerValueChanged(trigger.TriggerName, trigger.TriggerHandler, propertyName, sens, values, isCustomProperty);
                    newTrigger.ListScript = listScript;

                    triggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                    //---
                }
                else if (cmbTypeTrigger.SelectedIndex == 3)
                {
                    newTrigger = new TriggerMouse(trigger.TriggerName, trigger.TriggerHandler, (TriggerMouseType)cmbMouseTriggerType.SelectedIndex);
                    newTrigger.ListScript = listScript;
                    triggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
                else if (cmbTypeTrigger.SelectedIndex == 4)
                {
                    newTrigger = new TriggerLoad(trigger.TriggerName, trigger.TriggerHandler);
                    newTrigger.ListScript = listScript;
                    triggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
                else if (cmbTypeTrigger.SelectedIndex == 5)
                {
                    newTrigger = new TriggerTime(trigger.TriggerName, trigger.TriggerHandler);
                    newTrigger.ListScript = listScript;

                    if (optTimeLoopAlways.Checked)
                        ((TriggerTime)newTrigger).TimeLoop = 0;
                    else
                        ((TriggerTime)newTrigger).TimeLoop = (int)numTimeLoop.Value;

                    triggerHandler.ListTrigger[listboxTrigger.SelectedIndex] = newTrigger;
                }
            }
        }

        private void treeviewEntiteTargetCollision_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Tag is Entite)
            {
                e.DrawDefault = true;
            }
            else
            {
                System.Drawing.Rectangle rec = new System.Drawing.Rectangle((e.Node.Level + 1) * treeviewEntiteTargetCollision.Indent, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(e.Node.Text, treeviewEntiteTargetCollision.Font, Brushes.Black, rec);
            }
        }

        private void treeviewEntiteScript_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Tag is Script)
            {
                e.DrawDefault = true;
            }
            else
            {
                System.Drawing.Rectangle rec = new System.Drawing.Rectangle((e.Node.Level + 1) * treeviewEntiteScript.Indent, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(e.Node.Text, treeviewEntiteScript.Font, Brushes.Black, rec);
            }
        }
        #endregion

        #region Private methods
        private ITriggerHandler GetCurrentTriggerHandler()
        {
            ITriggerHandler triggerHandler = null;

            if (repository.CurrentEntite != null)
                triggerHandler = repository.CurrentEntite;
            else if (repository.CurrentObject != null && repository.CurrentObject is ITriggerHandler)
                triggerHandler = (ITriggerHandler)repository.CurrentObject;

            return triggerHandler;
        }

        private void ShowPropertyDetail(PropertyInfo propInfo)
        {
            TriggerValueChanged trigger = null;

            if (repository.CurrentEntite.ListTrigger[listboxTrigger.SelectedIndex] is TriggerValueChanged)
                trigger = (TriggerValueChanged)repository.CurrentEntite.ListTrigger[listboxTrigger.SelectedIndex];

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
                if (trigger != null)
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

                if (trigger != null)
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

                if (trigger != null)
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

        //private void UncheckAllListViewItem(ListView listView)
        //{
        //    for (int i = 0; i < listView.Items.Count; i++)
        //    {
        //        listView.Items[i].Checked = false;
        //    }
        //}

        private void ChangeNodeCheck(TreeNode node, bool isChecked)
        {
            node.Checked = isChecked;

            if (node.Nodes.Count > 0)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    ChangeNodeCheck(node.Nodes[i], isChecked);
                }
            }
        }

        private void ChangeNodeCheck(TreeView treeView, bool isChecked)
        {
            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                treeView.Nodes[i].Checked = isChecked;
            }
        }

        private void RefreshTreeViewScript(TreeView treeView, TriggerBase trigger)
        {
            treeView.Nodes.Clear();
            TreeNode nodeRoot = treeView.Nodes.Add("World");

            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];

                TreeNode nodeEntite = nodeRoot.Nodes.Add(entite.Name, entite.Name);
                nodeEntite.Tag = entite;

                //--- Script
                foreach (Script script in entite.ListScript)
                {
                    TreeNode nodeScript = nodeEntite.Nodes.Add(script.ScriptName, script.ScriptName);
                    nodeScript.Tag = script;
                }
                //---

                //--- ParticleSystem
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    ParticleSystem pSystem = entite.ListParticleSystem[j];
                    TreeNode nodePSystem = null;

                    foreach (Script script in pSystem.ListScript)
                    {
                        if (nodePSystem == null)
                        {
                            nodePSystem = nodeEntite.Nodes.Add(entite.ListParticleSystem[j].ParticleSystemName);
                        }

                        TreeNode nodeScript = nodePSystem.Nodes.Add(script.ScriptName, script.ScriptName);
                        nodeScript.Tag = script;
                    }

                    //---> ParticleTemplate
                    for (int k = 0; k < pSystem.ListParticleTemplate.Count; k++)
                    {
                        Particle particle = pSystem.ListParticleTemplate[k];
                        TreeNode nodePTemplate = null;

                        //--- Script
                        foreach (Script script in particle.ListScript)
                        {
                            if (nodePSystem == null)
                            {
                                nodePSystem = nodeEntite.Nodes.Add(entite.ListParticleSystem[j].ParticleSystemName);
                            }
                            if (nodePTemplate == null)
                            {
                                nodePTemplate = nodePSystem.Nodes.Add(particle.Name);
                            }

                            TreeNode nodeScript = nodePTemplate.Nodes.Add(script.ScriptName, script.ScriptName);
                            nodeScript.Tag = script;
                        }
                        //---   
                    }
                }
                //---
            }

            nodeRoot.ExpandAll();

            //--- Liste des scripts
            if (trigger != null)
            {
                foreach (Script script in trigger.ListScript)
                {
                    List<TreeNode> nodesScript = treeviewEntiteScript.Nodes.Find(script.ScriptName, true).ToList();

                    for (int i = 0; i < nodesScript.Count; i++)
                    {
                        if (script.ActionHandler is Entite && nodesScript[i].Parent.Text == ((Entite)script.ActionHandler).Name)
                        {
                            nodesScript[i].Checked = true;
                        }
                    }
                }
            }
            //---
        }

        private void RefreshTreeViewEntite(TreeView treeView)
        {
            treeView.Nodes.Clear();
            TreeNode nodeRoot = treeView.Nodes.Add("World");

            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];

                TreeNode nodeEntite = nodeRoot.Nodes.Add(entite.Name, entite.Name);
                nodeEntite.Tag = entite;

                //--- ParticleSystem
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    ParticleSystem pSystem = entite.ListParticleSystem[j];
                    TreeNode nodePSystem = null;

                    //---> ParticleTemplate
                    for (int k = 0; k < pSystem.ListParticleTemplate.Count; k++)
                    {
                        Particle particle = pSystem.ListParticleTemplate[k];

                        if (nodePSystem == null)
                        {
                            nodePSystem = nodeEntite.Nodes.Add(entite.ListParticleSystem[j].ParticleSystemName);
                        }
                        TreeNode nodePTemplate = nodePSystem.Nodes.Add(particle.Name);
                        nodePTemplate.Tag = particle;
                    }
                }
                //---
            }

            nodeRoot.ExpandAll();
        }

        private TreeNode GetCheckedNode(TreeNode node)
        {
            if (node == null)
                return null;

            if (node.Checked)
                return node;

            if (node.Nodes.Count > 0)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    TreeNode nd = GetCheckedNode(node.Nodes[i]);

                    if (nd != null)
                        return nd;
                }

                return null;
            }
            else return null;
        }

        private void GetCheckedNodes(TreeNode node, List<TreeNode> checkedNode)
        {
            if (node == null)
                return;

            if (node.Checked)
                checkedNode.Add(node);

            if (node.Nodes.Count > 0)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    GetCheckedNodes(node.Nodes[i], checkedNode);
                }
            }
        }

        private int GetCheckedNodesCount(TreeView treeView)
        {
            int count = 0;

            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                if (treeView.Nodes[i].Checked)
                    count++;
            }

            return count;
        }

        private void RefreshTreeViewProperties()
        {
            treeViewProperties.Nodes.Clear();

            treeViewProperties.Nodes.Add(CreateTreeNodeForProperty("Position"));
            treeViewProperties.Nodes.Add(CreateTreeNodeForProperty("Rotation"));
            treeViewProperties.Nodes.Add(CreateTreeNodeForProperty("BlurFactor"));
            treeViewProperties.Nodes.Add(CreateTreeNodeForProperty("IsStatic"));
            treeViewProperties.Nodes.Add(CreateTreeNodeForProperty("IsColisionable"));
            treeViewProperties.Nodes.Add(CreateTreeNodeForProperty("Size"));
        }

        private void RefreshTreeViewCustomProperties(Entite entite)
        {
            treeViewCustomProperties.Nodes.Clear();

            if (entite == null)
                return;

            for (int i = 0; i < entite.ListCustomProperties.Count; i++)
            {
                KeyValuePair<String, Object> customProp = entite.ListCustomProperties.ElementAt(i);

                TreeNode node = new TreeNode(customProp.Key);
                node.Tag = customProp.Key;

                treeViewCustomProperties.Nodes.Add(node);
            }
        }

        private TreeNode CreateTreeNodeForProperty(string propertyName)
        {
            TreeNode node = new TreeNode(propertyName);

            PropertyInfo propInfo = typeof(Entite).GetProperty(propertyName);
            node.Tag = propInfo;

            return node;
        }

        private void RefreshComboBoxMouseTrigger()
        {
            cmbMouseTriggerType.SelectedIndex = 0;
        }

        private void SelectTrigger(TriggerBase trigger)
        {
            if (trigger == null)
            {
                //TODO rendre invisible les panneaux
                return;
            }
            else
            {
                
            }

            cmbTypeTrigger.SelectedIndex = -1;

            if (trigger is TriggerCollision)
            {
                TriggerCollision triggerCol = (TriggerCollision)trigger;

                //--- Type de trigger
                cmbTypeTrigger.SelectedIndex = 0;
                //---

                //--- Trigger collision
                if (triggerCol.TargetEntite != null && treeviewEntiteTargetCollision.Nodes.Find(triggerCol.TargetEntite.Name, true).Length > 0)
                {
                    TreeNode treeNode = treeviewEntiteTargetCollision.Nodes.Find(triggerCol.TargetEntite.Name, true)[0];
                    treeNode.Checked = true;
                }

                RefreshTreeViewScript(treeviewEntiteScript, trigger);
                //---
            }
            else if (trigger is TriggerValueChanged)
            {
                TriggerValueChanged triggerVal = (TriggerValueChanged)trigger;

                //--- Type de trigger
                cmbTypeTrigger.SelectedIndex = 2;
                //---

                //--- Trigger ValueChanged
                if (triggerVal.IsCustomProperty && !String.IsNullOrEmpty(triggerVal.PropertyName))
                {
                    TreeNode treeNode = treeViewCustomProperties.Nodes.Find(triggerVal.PropertyName, true)[0];
                    treeNode.Checked = true;
                }
                else if (!triggerVal.IsCustomProperty && !String.IsNullOrEmpty(triggerVal.PropertyName))
                {
                    TreeNode treeNode = treeViewProperties.Nodes.Find(triggerVal.PropertyName, true)[0];
                    treeNode.Checked = true;
                }

                RefreshTreeViewScript(treeviewEntiteScript, trigger);
                //---
            }
            else if (trigger is TriggerMouse)
            {
                TriggerMouse triggerMouse = (TriggerMouse)trigger;

                //--- Type de trigger
                cmbTypeTrigger.SelectedIndex = 3;
                //---

                //--- Trigger Mouse
                cmbMouseTriggerType.SelectedIndex = (int)triggerMouse.TriggerMouseType;

                RefreshTreeViewScript(treeviewEntiteScript, trigger);
                //---
            }
            else if (trigger is TriggerLoad)
            {
                TriggerLoad triggerLoad = (TriggerLoad)trigger;

                //--- Type de trigger
                cmbTypeTrigger.SelectedIndex = 4;
                //---

                RefreshTreeViewScript(treeviewEntiteScript, trigger);
            }
            else if (trigger is TriggerTime)
            {
                TriggerTime triggerTime = (TriggerTime)trigger;

                //--- Type de trigger
                cmbTypeTrigger.SelectedIndex = 5;
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

                RefreshTreeViewScript(treeviewEntiteScript, trigger);
                //---
            }
        }
        #endregion

        #region Public methods
        public void RefreshTriggerList()
        {
            listboxTrigger.Items.Clear();
            ITriggerHandler triggerHandler = null;

            if (repository.CurrentEntite != null)
                triggerHandler = repository.CurrentEntite;
            else if (repository.CurrentObject != null && repository.CurrentObject is ITriggerHandler)
                triggerHandler = (ITriggerHandler)repository.CurrentObject;

            if (triggerHandler == null)
            {
                SelectTrigger(null);
                return;
            }

            foreach (TriggerBase trigger in triggerHandler.ListTrigger)
            {
                listboxTrigger.Items.Add(trigger.TriggerName);
            }

            if (triggerHandler.ListTrigger.Count > 0)
                listboxTrigger.SelectedIndex = 0;
            else
            {
                SelectTrigger(null);

                //--- Affichage des colonnes pour une entité ne disposant pas de triggers
                //pnlTrigger.ColumnStyles[0].Width = 12;
                //pnlTrigger.ColumnStyles[1].Width = 12;
                //pnlTrigger.ColumnStyles[7].Width = 76;
                //---
            }
        }
        #endregion




    }
}
