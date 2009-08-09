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

namespace Edit2D.ScriptControl
{
    public partial class ActionEventLineControl : UserControl
    {
        public ActionEvent ActionEvent { get; set; }
        public int ActionEventIndex { get; set; }
        public Boolean IsInitialized { get; set; }
        public Type PropertyType { get; set; }
        public Repository repository { get; set; }

        public ActionEventLineControl(Type propertyType, Repository repository)
        {
            InitializeComponent();
            this.PropertyType = propertyType;
            this.repository = repository;

            RefreshTreeViewEntite(treeviewEntiteTargetCollision);
        }

        #region Evènements
        private void optDurationDeactivate_CheckedChanged(object sender, EventArgs e)
        {
            numDuration.Enabled = false;
            UpdateActionEvent();
        }

        private void optDurationActivate_CheckedChanged(object sender, EventArgs e)
        {
            numDuration.Enabled = true;
            UpdateActionEvent();
        }

        private void optActionEventLineDeactivated_CheckedChanged(object sender, EventArgs e)
        {
            numFixedValue.Enabled = false;
            numRndMax.Enabled = false;
            treeviewEntiteTargetCollision.Enabled = false;
            treeViewProperties.Enabled = false;
            numRndMin.Enabled = false;
            numRndMax.Enabled = false;
            optActionEventLineMouseX.Enabled = false;
            optActionEventLineMouseY.Enabled = false;

            optDurationActivate.Enabled = false;
            optDurationDeactivate.Enabled = false;

            optFixedValueTrue.Enabled = false;
            optFixedValueFalse.Enabled = false;

            UpdateActionEvent();
        }

        private void optActionEventLineMouse_CheckedChanged(object sender, EventArgs e)
        {
            numFixedValue.Enabled = false;
            numRndMax.Enabled = false;
            treeviewEntiteTargetCollision.Enabled = false;
            treeViewProperties.Enabled = false;
            numRndMin.Enabled = false;
            numRndMax.Enabled = false;
            optActionEventLineMouseX.Enabled = true;
            optActionEventLineMouseY.Enabled = true;

            optDurationActivate.Enabled = true;
            optDurationDeactivate.Enabled = true;

            optFixedValueTrue.Enabled = false;
            optFixedValueFalse.Enabled = false;

            UpdateActionEvent();
        }

        private void optActionEventLineFixedValue_CheckedChanged(object sender, EventArgs e)
        {
            numFixedValue.Enabled = true;
            numRndMax.Enabled = false;
            treeviewEntiteTargetCollision.Enabled = false;
            treeViewProperties.Enabled = false;
            numRndMin.Enabled = false;
            numRndMax.Enabled = false;
            optActionEventLineMouseX.Enabled = false;
            optActionEventLineMouseY.Enabled = false;

            optDurationActivate.Enabled = true;
            optDurationDeactivate.Enabled = true;

            optFixedValueTrue.Enabled = true;
            optFixedValueFalse.Enabled = true;

            UpdateActionEvent();
        }

        private void optActionEventLineEntity_CheckedChanged(object sender, EventArgs e)
        {
            numFixedValue.Enabled = false;
            numRndMax.Enabled = false;
            treeviewEntiteTargetCollision.Enabled = true;
            treeViewProperties.Enabled = true;
            numRndMin.Enabled = false;
            numRndMax.Enabled = false;
            optActionEventLineMouseX.Enabled = false;
            optActionEventLineMouseY.Enabled = false;

            optDurationActivate.Enabled = true;
            optDurationDeactivate.Enabled = true;

            optFixedValueTrue.Enabled = false;
            optFixedValueFalse.Enabled = false;

            UpdateActionEvent();
        }

        private void optActionEventLineRandom_CheckedChanged(object sender, EventArgs e)
        {
            numFixedValue.Enabled = false;
            numRndMax.Enabled = false;
            treeviewEntiteTargetCollision.Enabled = false;
            treeViewProperties.Enabled = false;
            numRndMin.Enabled = true;
            numRndMax.Enabled = true;
            optActionEventLineMouseX.Enabled = false;
            optActionEventLineMouseY.Enabled = false;

            optDurationActivate.Enabled = true;
            optDurationDeactivate.Enabled = true;

            optFixedValueTrue.Enabled = false;
            optFixedValueFalse.Enabled = false;

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

        private void chkRelative_CheckedChanged(object sender, EventArgs e)
        {
            UpdateActionEvent();
        }
        #endregion

        #region Private methods
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

        private void UpdateActionEvent()
        {
            if (IsInitialized && ActionEvent != null)
            {
                if (optActionEventLineDeactivated.Checked)
                {
                    ActionEvent.ActionEventTypes[ActionEventIndex] = ActionEventType.Deactivated;
                }
                else if (optActionEventLineMouse.Checked && optActionEventLineMouseX.Checked)
                {
                    ActionEvent.ActionEventTypes[ActionEventIndex] = ActionEventType.MouseX;
                }
                else if (optActionEventLineMouse.Checked && optActionEventLineMouseY.Checked)
                {
                    ActionEvent.ActionEventTypes[ActionEventIndex] = ActionEventType.MouseY;
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
                    GetCheckedNodes(treeviewEntiteTargetCollision.Nodes[0], propNodes);
                    //---

                    if(propNodes.Count >0)
                    {
                        ActionEvent.EntiteBindingProperties[ActionEventIndex] = (PropertyInfo)((Object[])propNodes[0].Tag)[0];
                        ActionEvent.EntiteBindingPropertyId[ActionEventIndex] = (int)((Object[])propNodes[0].Tag)[1];
                        ActionEvent.EntiteBindings[ActionEventIndex] = ((Entite)propNodes[0].Parent.Tag);
                    }
                }
                else if (optActionEventLineRandom.Checked)
                {
                    ActionEvent.ActionEventTypes[ActionEventIndex] = ActionEventType.Random;
                    ActionEvent.RndMinValues[ActionEventIndex] = (float)numRndMin.Value;
                    ActionEvent.RndMaxValues[ActionEventIndex] = (float)numRndMax.Value;
                }

                //--- Relative
                ActionEvent.IsRelative[ActionEventIndex] = chkRelative.Checked;
                //---

                //--- Duration
                if (optDurationActivate.Checked)
                {
                    ActionEvent.Durations[ActionEventIndex] = (int)numDuration.Value;
                }
                else
                {
                    ActionEvent.Durations[ActionEventIndex] = 0;
                }
                //---
            }
        }
        #endregion

        private void treeviewEntiteTargetCollision_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                if (e.Node.Tag is Object[])
                {
                    bool isChecked = e.Node.Checked;
                    ChangeNodeCheck(e.Node.Parent, false);

                    e.Node.Checked = isChecked;
                }
                else
                {
                    e.Node.Checked = false;
                }

                UpdateActionEvent();
            }
        }

        private void treeViewProperties_AfterCheck(object sender, TreeViewEventArgs e)
        {

        }

        private void ChangeNodeCheck(TreeNode node, bool isChecked)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].Checked = isChecked;
            }
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

                RefreshProperties(entite, nodeEntite);

                //--- ParticleSystem
                for (int j = 0; j < entite.ListParticleSystem.Count; j++)
                {
                    ParticleSystem pSystem = entite.ListParticleSystem[j];
                    TreeNode nodePSystem = nodeEntite.Nodes.Add(entite.ListParticleSystem[j].ParticleSystemName);
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
                                node.Nodes.Add(properties[i].Name + ".X").Tag = new Object[]{ properties[i],1};
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
    }
}
