using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Edit2DEngine.Action;
using Edit2DEngine.Particles;
using System.Reflection;
using Edit2DEngine;
using Edit2D.UC;

namespace Edit2D.ScriptControl
{
    public partial class ActionEventLineControl : UserControlLocal
    {
        public ActionEvent ActionEvent { get; set; }
        public int ActionEventIndex { get; set; }
        public Boolean IsInitialized { get; set; }
        public Type PropertyType { get; set; }
        public Repository repository { get; set; }

        private const string MOUSE_X_TAG = "MOUSEX";
        private const string MOUSE_Y_TAG = "MOUSEY";

        public ActionEventLineControl(Type propertyType, Repository repository, string nameProperty, ActionEvent actionEvent, int index,
            float fixedValue, float fixedMinValue, float fixedMaxValue, float rndMinValue, float rndMinMinValue, float rndMinMaxValue, float rndMaxValue, float rndMaxMinValue, float rndMaxMaxValue)
        {
            InitializeComponent();

            this.PropertyType = propertyType;
            this.repository = repository;
            this.ActionEvent = actionEvent;
            this.ActionEventIndex = index;

            this.lblActionEventPropertyName.Text = nameProperty;

            Init(fixedValue, fixedMinValue, fixedMaxValue, rndMinValue, rndMinMinValue, rndMinMaxValue, rndMaxValue, rndMaxMinValue, rndMaxMaxValue);

            RefreshTreeViewEntite(treeViewBoundEntity);
        }

        #region Evènements
        private void optDurationDeactivate_CheckedChanged(object sender, EventArgs e)
        {
            pnlDuration.Enabled = false;
            UpdateActionEvent();
        }

        private void optDurationActivate_CheckedChanged(object sender, EventArgs e)
        {
            pnlDuration.Enabled = true;
            numDuration.Visible = true;
            numSpeed.Visible = false;
            lblTransitionUnit.Text = "ms";

            UpdateActionEvent();
        }

        private void optSpeedActivate_CheckedChanged(object sender, EventArgs e)
        {
            pnlDuration.Enabled = true;
            numDuration.Visible = false;
            numSpeed.Visible = true;
            lblTransitionUnit.Text = "unités / sec";

            UpdateActionEvent();
        }

        private void optActionEventLineDeactivated_CheckedChanged(object sender, EventArgs e)
        {
            numFixedValue.Enabled = false;
            numRndMax.Enabled = false;
            treeViewBoundEntity.Enabled = false;
            numRndMin.Enabled = false;
            numRndMax.Enabled = false;

            pnlTransition.Enabled = false;
            pnlDuration.Enabled = false;
            pnlRelative.Enabled = false;

            optFixedValueTrue.Enabled = false;
            optFixedValueFalse.Enabled = false;

            pnlFixedValue.Tag = "BG1";
            pnlBoundValue.Tag = "BG1";
            pnlRandomValue.Tag = "BG1";

            WinformVisualStyle.ApplyStyle(pnlFixedValue);
            WinformVisualStyle.ApplyStyle(pnlBoundValue);
            WinformVisualStyle.ApplyStyle(pnlRandomValue);

            UpdateActionEvent();
        }

        private void optActionEventLineFixedValue_CheckedChanged(object sender, EventArgs e)
        {
            numFixedValue.Enabled = true;
            numRndMax.Enabled = false;
            treeViewBoundEntity.Enabled = false;
            numRndMin.Enabled = false;
            numRndMax.Enabled = false;

            pnlTransition.Enabled = true;
            pnlDuration.Enabled = true;
            pnlRelative.Enabled = true;

            optFixedValueTrue.Enabled = true;
            optFixedValueFalse.Enabled = true;

            pnlFixedValue.Tag = "B";
            pnlBoundValue.Tag = "BG1";
            pnlRandomValue.Tag = "BG1";

            WinformVisualStyle.ApplyStyle(pnlFixedValue);
            WinformVisualStyle.ApplyStyle(pnlBoundValue);
            WinformVisualStyle.ApplyStyle(pnlRandomValue);

            UpdateActionEvent();
        }

        private void optActionEventLineEntity_CheckedChanged(object sender, EventArgs e)
        {
            numFixedValue.Enabled = false;
            numRndMax.Enabled = false;
            treeViewBoundEntity.Enabled = true;
            numRndMin.Enabled = false;
            numRndMax.Enabled = false;

            pnlTransition.Enabled = true;
            pnlDuration.Enabled = true;
            pnlRelative.Enabled = true;

            optFixedValueTrue.Enabled = false;
            optFixedValueFalse.Enabled = false;

            pnlFixedValue.Tag = "BG1";
            pnlBoundValue.Tag = "B";
            pnlRandomValue.Tag = "BG1";

            WinformVisualStyle.ApplyStyle(pnlFixedValue);
            WinformVisualStyle.ApplyStyle(pnlBoundValue);
            WinformVisualStyle.ApplyStyle(pnlRandomValue);

            UpdateActionEvent();
        }

        private void optActionEventLineRandom_CheckedChanged(object sender, EventArgs e)
        {
            numFixedValue.Enabled = false;
            numRndMax.Enabled = false;
            treeViewBoundEntity.Enabled = false;
            numRndMin.Enabled = true;
            numRndMax.Enabled = true;

            pnlTransition.Enabled = true;
            pnlDuration.Enabled = true;
            pnlRelative.Enabled = true;

            optFixedValueTrue.Enabled = false;
            optFixedValueFalse.Enabled = false;

            pnlFixedValue.Tag = "BG1";
            pnlBoundValue.Tag = "BG1";
            pnlRandomValue.Tag = "B";

            WinformVisualStyle.ApplyStyle(pnlFixedValue);
            WinformVisualStyle.ApplyStyle(pnlBoundValue);
            WinformVisualStyle.ApplyStyle(pnlRandomValue);

            UpdateActionEvent();
        }

        private void optActionEventLineMouseX_CheckedChanged(object sender, EventArgs e)
        {
            UpdateActionEvent();
        }

        private void optActionEventLineMouseY_CheckedChanged(object sender, EventArgs e)
        {
            UpdateActionEvent();
        }

        private void numFixedValue_ValueChanged(object sender, EventArgs e)
        {
            UpdateActionEvent();
        }

        private void optFixedValueTrue_CheckedChanged(object sender, EventArgs e)
        {
            UpdateActionEvent();
        }

        private void optFixedValueFalse_CheckedChanged(object sender, EventArgs e)
        {
            UpdateActionEvent();
        }

        private void numRndMin_ValueChanged(object sender, EventArgs e)
        {
            UpdateActionEvent();
        }

        private void numRndMax_ValueChanged(object sender, EventArgs e)
        {
            UpdateActionEvent();
        }

        private void numDuration_ValueChanged(object sender, EventArgs e)
        {
            UpdateActionEvent();
        }

        private void numSpeed_ValueChanged(object sender, EventArgs e)
        {
            UpdateActionEvent();
        }

        private void chkRelative_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRelative.Checked)
                pnlRelative.Tag = "B";
            else
                pnlRelative.Tag = "BG1";

            WinformVisualStyle.ApplyStyle(pnlRelative);

            UpdateActionEvent();
        }

        private void treeViewBoundEntity_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                if (e.Node.Tag is Object[] ||
                    (e.Node.Tag is String &&
                        (e.Node.Tag.ToString() == MOUSE_X_TAG || e.Node.Tag.ToString() == MOUSE_Y_TAG)))
                {
                    bool isChecked = e.Node.Checked;

                    ChangeNodeCheck(e.Node.TreeView.Nodes[0], false);

                    e.Node.Checked = isChecked;
                }
                else
                {
                    e.Node.Checked = false;
                }

                UpdateActionEvent();
            }
        }

        #endregion

        #region Private methods
        private void Init(float fixedValue, float fixedMinValue, float fixedMaxValue, float rndMinValue, float rndMinMinValue, float rndMinMaxValue, float rndMaxValue, float rndMaxMinValue, float rndMaxMaxValue)
        {
            if (ActionEvent.PropertyType.Name == "Boolean")
            {
                optFixedValueTrue.Visible = true;
                optFixedValueFalse.Visible = true;
                numFixedValue.Visible = false;

                numRndMax.Visible = false;
                numRndMin.Visible = false;
                lblRandomValueMin.Visible = false;
                lblRandomValueMax.Visible = false;

                pnlDuration.Visible = false;
                pnlTransition.Visible = false;
                pnlRelative.Visible = false;
            }
            else
            {
                optFixedValueTrue.Visible = false;
                optFixedValueFalse.Visible = false;
                numFixedValue.Visible = true;

                numFixedValue.Minimum = (decimal)fixedMinValue;
                numFixedValue.Maximum = (decimal)fixedMaxValue;

                numRndMin.Minimum = (decimal)rndMinMinValue;
                numRndMin.Maximum = (decimal)rndMinMaxValue;

                numRndMax.Minimum = (decimal)rndMaxMinValue;
                numRndMax.Maximum = (decimal)rndMaxMaxValue;
            }

            if (ActionEvent.PropertyType.Name == "Vector2" ||
                ActionEvent.PropertyType.Name == "Size" ||
                ActionEvent.PropertyType.Name == "Single")
            {
                numFixedValue.DecimalPlaces = 2;
                numRndMin.DecimalPlaces = 2;
                numRndMax.DecimalPlaces = 2;
            }
        }

        private void RefreshTreeViewEntite(TreeView treeView)
        {
            treeView.Nodes.Clear();
            TreeNode nodeRoot = treeView.Nodes.Add("World");

            //--- Noeud Souris
            TreeNode nodeMouse = nodeRoot.Nodes.Add("Souris");
            TreeNode nodeMouseX = nodeMouse.Nodes.Add("X");
            TreeNode nodeMouseY = nodeMouse.Nodes.Add("Y");
            nodeMouseX.Tag = MOUSE_X_TAG;
            nodeMouseY.Tag = MOUSE_Y_TAG;
            //---

            for (int i = 0; i < repository.listEntite.Count; i++)
            {
                Entite entite = repository.listEntite[i];

                TreeNode nodeEntite = nodeRoot.Nodes.Add(entite.Name, entite.Name);
                nodeEntite.Tag = entite;

                RefreshProperties(entite, nodeEntite);

                //--- ParticleSystem
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    ParticleSystem pSystem = entite.ListParticleSystem[j];
                    TreeNode nodePSystem = nodeEntite.Nodes.Add(entite.ListParticleSystem[j].Name);
                    nodePSystem.Tag = pSystem;

                    RefreshProperties(pSystem, nodeEntite);

                    //---> ParticleTemplate
                    //for (int k = 0; k < pSystem.ListParticleTemplate.Count; k++)
                    //{
                    //    Particle particle = pSystem.ListParticleTemplate[k];

                    //    if (nodePSystem == null)
                    //    {
                    //        nodePSystem = nodeEntite.Nodes.Add(entite.ListParticleSystem[j].ParticleSystemName);
                    //    }
                    //    TreeNode nodePTemplate = nodePSystem.Nodes.Add(particle.Name);
                    //    nodePTemplate.Tag = particle;
                    //}
                }
                //---
            }

            nodeRoot.ExpandAll();
        }

        private void RefreshProperties(Object obj, TreeNode node)
        {
            PropertyInfo[] properties = null;

            properties = obj.GetType().GetProperties();

            if (properties != null)
            {
                List<String> propertiesName = new List<String>();
                for (int i = 0; i < properties.Length; i++)
                {
                    if (properties[i].GetCustomAttributes(typeof(AttributeAction), true).Length > 0)
                    {
                        if (PropertyType.Name == "Vector2")
                        {
                            if (properties[i].PropertyType.Name == "Vector2")
                            {
                                node.Nodes.Add(properties[i].Name + ".X").Tag = new Object[] { properties[i], 1 };
                                node.Nodes.Add(properties[i].Name + ".Y").Tag = new Object[] { properties[i], 2 };
                            }
                            else if (properties[i].PropertyType.Name == "Single" ||
                                properties[i].PropertyType.Name == "Int32")
                            {
                                node.Nodes.Add(properties[i].Name).Tag = new Object[] { properties[i], 1 };
                            }
                            else if (properties[i].PropertyType.Name == "Size")
                            {
                                node.Nodes.Add(properties[i].Name + ".Width").Tag = new Object[] { properties[i], 1 };
                                node.Nodes.Add(properties[i].Name + ".Height").Tag = new Object[] { properties[i], 2 };
                            }
                        }
                    }
                }
            }
        }

        private void ChangeNodeCheck(TreeNode node, bool isChecked)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].Checked = isChecked;

                ChangeNodeCheck(node.Nodes[i], isChecked);
            }
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

        private TreeNode GetNodeWithTag(TreeNode nodeParent, Entite entite, PropertyInfo propertyInfo, int index)
        {
            TreeNode node = null;

            foreach (TreeNode nodeChild in nodeParent.Nodes)
            {
                if (node == null &&
                    (nodeChild.Tag is Object[]) &&
                    nodeParent.Tag == entite &&
                    ((PropertyInfo)((Object[])nodeChild.Tag)[0]).Name == propertyInfo.Name &&
                     ((int)((Object[])nodeChild.Tag)[1]) == index)
                {
                    node = nodeChild;
                }

                if (node == null)
                {
                    node = GetNodeWithTag(nodeChild, entite, propertyInfo, index);
                }
            }

            return node;
        }

        /// <summary>
        /// Met à jour l'objet ActionEvent selon l'état du composant
        /// </summary>
        private void UpdateActionEvent()
        {
            if (IsInitialized && ActionEvent != null)
            {
                if (optActionEventLineDeactivated.Checked)
                {
                    ActionEvent.ActionEventTypes[ActionEventIndex] = ActionEventType.Deactivated;
                }
                else if (optActionEventLineFixedValue.Checked)
                {
                    ActionEvent.ActionEventTypes[ActionEventIndex] = ActionEventType.FixedValue;

                    if (this.PropertyType.Name == "Boolean")
                        ActionEvent.BoolValue = optFixedValueTrue.Checked;
                    else
                        ActionEvent.FloatValues[ActionEventIndex] = (float)numFixedValue.Value;
                }
                else if (optActionEventLineEntity.Checked)
                {
                    ActionEvent.ActionEventTypes[ActionEventIndex] = ActionEventType.EntityBinding;

                    //--- Recherche des propriétés cochées
                    List<TreeNode> propNodes = new List<TreeNode>();
                    GetCheckedNodes(treeViewBoundEntity.Nodes[0], propNodes);
                    //---

                    if (propNodes.Count > 0)
                    {
                        //---> Propriété liée à la position de la souris
                        if (propNodes[0].Tag is String)
                        {
                            if (propNodes[0].Tag.ToString().StartsWith(MOUSE_X_TAG))
                                ActionEvent.ActionEventTypes[ActionEventIndex] = ActionEventType.MouseX;
                            else if (propNodes[0].Tag.ToString().StartsWith(MOUSE_Y_TAG))
                                ActionEvent.ActionEventTypes[ActionEventIndex] = ActionEventType.MouseY;
                        }
                        //---> Propriété liée à une entité
                        else
                        {
                            ActionEvent.EntiteBindingProperties[ActionEventIndex] = (PropertyInfo)((Object[])propNodes[0].Tag)[0];
                            ActionEvent.EntiteBindingPropertyId[ActionEventIndex] = (int)((Object[])propNodes[0].Tag)[1];
                            ActionEvent.EntiteBindings[ActionEventIndex] = ((Entite)propNodes[0].Parent.Tag);
                        }
                    }
                }
                else if (optActionEventLineRandom.Checked)
                {
                    ActionEvent.ActionEventTypes[ActionEventIndex] = ActionEventType.Random;

                    //---> Le choix des valeurs minimum et maximum pour "Valeur aléatoire"
                    //     n'est pas possible pour les booléens
                    if (this.PropertyType.Name != "Boolean")
                    {
                        ActionEvent.RndMinValues[ActionEventIndex] = (float)numRndMin.Value;
                        ActionEvent.RndMaxValues[ActionEventIndex] = (float)numRndMax.Value;
                    }
                }

                //--- Relative
                ActionEvent.IsRelative[ActionEventIndex] = chkRelative.Checked;
                //---

                //--- Duration
                if (optDurationActivate.Checked)
                {
                    ActionEvent.Durations[ActionEventIndex] = (int)numDuration.Value;
                    ActionEvent.Speeds[ActionEventIndex] = 0;
                }
                else if (optSpeedActivate.Checked)
                {
                    ActionEvent.Durations[ActionEventIndex] = 0;
                    ActionEvent.Speeds[ActionEventIndex] = (int)numSpeed.Value;
                }
                else
                {
                    ActionEvent.Durations[ActionEventIndex] = 0;
                    ActionEvent.Speeds[ActionEventIndex] = 0;
                }
                //---
            }
        }
        #endregion

        #region Public methods
        public void RefreshActionEvent()
        {
            switch (ActionEvent.ActionEventTypes[ActionEventIndex])
            {
                case ActionEventType.Deactivated:
                    optActionEventLineDeactivated.Checked = true;
                    break;
                case ActionEventType.FixedValue:
                    optActionEventLineFixedValue.Checked = true;
                    if (ActionEvent.PropertyType.Name == "Boolean")
                    {
                        if (ActionEvent.BoolValue)
                        {
                            optFixedValueTrue.Checked = true;
                            optFixedValueFalse.Checked = false;
                        }
                        else
                        {
                            optFixedValueTrue.Checked = false;
                            optFixedValueFalse.Checked = true;
                        }
                    }
                    else
                        numFixedValue.Value = (decimal)ActionEvent.FloatValues[ActionEventIndex];
                    break;
                case ActionEventType.MouseX:
                    optActionEventLineEntity.Checked = true;
                    treeViewBoundEntity.Nodes[0].Nodes[0].Nodes[0].Checked = true;
                    break;
                case ActionEventType.MouseY:
                    optActionEventLineEntity.Checked = true;
                    treeViewBoundEntity.Nodes[0].Nodes[0].Nodes[1].Checked = true;
                    break;
                case ActionEventType.EntityBinding:
                    optActionEventLineEntity.Checked = true;

                    TreeNode node = GetNodeWithTag( treeViewBoundEntity.Nodes[0], 
                                                    ActionEvent.EntiteBindings[ActionEventIndex],
                                                    ActionEvent.EntiteBindingProperties[ActionEventIndex], 
                                                    ActionEvent.EntiteBindingPropertyId[ActionEventIndex]);
                    node.Checked = true;

                    break;
                case ActionEventType.Random:
                    optActionEventLineRandom.Checked = true;
                    numRndMin.Value = (decimal)ActionEvent.RndMinValues[ActionEventIndex];
                    numRndMax.Value = (decimal)ActionEvent.RndMaxValues[ActionEventIndex];
                    break;
                default:
                    break;
            }

            if (ActionEvent.Durations[ActionEventIndex] != 0)
            {
                optDurationActivate.Checked = true;
                numDuration.Value = ActionEvent.Durations[ActionEventIndex];
            }
            else if (ActionEvent.Speeds[ActionEventIndex] != 0)
            {
                optSpeedActivate.Checked = true;
                numSpeed.Value = ActionEvent.Speeds[ActionEventIndex];
            }
            else
            {
                optDurationDeactivate.Checked = true;
                numDuration.Value = 0;
            }

            chkRelative.Checked = ActionEvent.IsRelative[ActionEventIndex];

            IsInitialized = true;
        }
        #endregion
    }
}
