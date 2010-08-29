using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Edit2DEngine;
using Edit2DEngine.Trigger;
using System.Reflection;
using Edit2DEngine.Action;
using Edit2DEngine.Entities;
using Edit2DEngine.Entities.Particles;

namespace Edit2D.UC
{
    public partial class TreeViewLocal : TreeView
    {
        public Repository Repository { get; set; }

        [Browsable(true)]
        public TreeViewLocalOrderType OrderType { get; set; }
        public TreeViewLocalItemType ItemTypeShowed { get; set; }
        public TreeViewLocalItemType ItemTypeCheckBoxed { get; set; }

        [Browsable(true), DefaultValue(true)]
        public Boolean AllowMultipleItemChecked { get; set; }

        [Browsable(true), DefaultValue(true)]
        public Boolean AllowUncheckedNode { get; set; }

        public Boolean IsCheckedByMouse { get; set; }

        private const String IMAGE_KEY_EMPTY = "icon_Empty";
        private const String IMAGE_KEY_ENTITY = "icon_Entity";
        private const String IMAGE_KEY_PARTICLE_SYSTEM = "icon_Particlesystem";
        private const String IMAGE_KEY_SCRIPT = "icon_Script";
        private const String IMAGE_KEY_TRIGGER = "icon_Trigger";
        private const String IMAGE_KEY_WORLD = "icon_World";
        private const String IMAGE_KEY_CHECKED = "icon_Checked";
        private const String IMAGE_KEY_UNCHECKED = "icon_Unchecked";
        private const String IMAGE_KEY_EXPANDED = "icon_Expanded";
        private const String IMAGE_KEY_COLLAPSED = "icon_Collapsed";
        private const String IMAGE_KEY_MOUSE = "icon_Mouse";
        private const String IMAGE_KEY_PROPERTY = "icon_Properties";

        private const String MOUSE_X = "MOUSE_X";
        private const String MOUSE_Y = "MOUSE_Y";

        private string NODE_WORLD = "Monde";
        private string NODE_MOUSE = "Souris";
        private string NODE_ENTITY = "Entités";
        private string NODE_PROPERTIES = "Propriétés";
        private string NODE_SCRIPT = "Scripts";
        private string NODE_TRIGGER = "Déclencheurs";
        private string NODE_PARTICLESYSTEM = "Systèmes de particules";

        private SolidBrush backgroundBrush;
        private Boolean IsRefreshing { get; set; }

        public TreeViewLocal()
        {
            InitializeComponent();
            this.IsCheckedByMouse = false;
        }

        public void RefreshView<T>(T nodeToCheck)
        {
            RefreshView(false);

            CheckNode<T>(nodeToCheck);
        }

        public void RefreshView<T>(List<T> nodesToCheck)
        {
            RefreshView(false);

            CheckNodes<T>(nodesToCheck);
        }

        public void RefreshView()
        {
            RefreshView(true);
        }

        public void RefreshView(bool keepCheckedNode)
        {
            this.IsRefreshing = true;
            this.BeginUpdate();

            //--- Conserve les noeuds cochés et le positionnement vertical
            List<String> listCheckedNodesPath = GetCheckedNodesPath();
            List<String> listCollapsedNodesPath = GetCollapsedNodesPath();
            TreeNode firstDrawingNode = this.GetNodeAt(0, 0);
            string firstDrawingNodePath = String.Empty;

            if (firstDrawingNode != null)
                firstDrawingNodePath = firstDrawingNode.FullPath;
            //---

            backgroundBrush = new SolidBrush(this.BackColor);

            this.Nodes.Clear();

            //--- World
            TreeNode nodeWorld = this.Nodes.Add(NODE_WORLD, NODE_WORLD, IMAGE_KEY_WORLD);
            nodeWorld.Tag = new Object[] { TreeViewLocalItemType.World, Repository.World };
            //---

            //--- World Trigger
            if ((ItemTypeShowed & TreeViewLocalItemType.Trigger) == TreeViewLocalItemType.Trigger)
            {
                TreeNode nodeTriggersWorld = nodeWorld.Nodes.Add(NODE_TRIGGER, NODE_TRIGGER, IMAGE_KEY_TRIGGER);
                nodeTriggersWorld.Tag = new Object[] { TreeViewLocalItemType.Trigger };

                foreach (TriggerBase trigger in Repository.World.ListTrigger)
                {
                    TreeNode nodeTriggerWorld = nodeTriggersWorld.Nodes.Add(trigger.TriggerName, trigger.TriggerName, IMAGE_KEY_EMPTY);
                    nodeTriggerWorld.Tag = new Object[] { TreeViewLocalItemType.Trigger, trigger };
                }
            }
            //---

            //--- Souris
            if ((ItemTypeShowed & TreeViewLocalItemType.Mouse) == TreeViewLocalItemType.Mouse)
            {
                TreeNode nodeMouse = nodeWorld.Nodes.Add(NODE_MOUSE, NODE_MOUSE, IMAGE_KEY_MOUSE);
                nodeMouse.Tag = new Object[] { TreeViewLocalItemType.Mouse };

                TreeNode nodeMouseX = nodeMouse.Nodes.Add("X", "X", IMAGE_KEY_EMPTY);
                nodeMouseX.Tag = new Object[] { TreeViewLocalItemType.Mouse, MOUSE_X };

                TreeNode nodeMouseY = nodeMouse.Nodes.Add("Y", "Y", IMAGE_KEY_EMPTY);
                nodeMouseY.Tag = new Object[] { TreeViewLocalItemType.Mouse, MOUSE_Y };
            }
            //---

            //--- Entités
            TreeNode nodeEntities = nodeWorld.Nodes.Add(NODE_ENTITY, NODE_ENTITY, IMAGE_KEY_ENTITY);
            //---

            //--- Entité
            foreach (Entity entity in Repository.listEntity)
            {
                TreeNode nodeEntity = nodeEntities.Nodes.Add(entity.Name, entity.Name, IMAGE_KEY_EMPTY);
                nodeEntity.Tag = new Object[] { TreeViewLocalItemType.Entity, entity };

                //--- Propriétés
                if ((ItemTypeShowed & TreeViewLocalItemType.EntityProperties) == TreeViewLocalItemType.EntityProperties)
                {
                    TreeNode nodeProperties = nodeEntity.Nodes.Add(NODE_PROPERTIES, NODE_PROPERTIES, IMAGE_KEY_PROPERTY);
                    nodeProperties.Tag = new Object[] { TreeViewLocalItemType.EntityProperties };

                    PropertyInfo[] propertiesInfo = entity.GetType().GetProperties();

                    foreach (PropertyInfo propertyInfo in propertiesInfo)
                    {
                        if (propertyInfo.GetCustomAttributes(typeof(AttributeAction), true).Length > 0)
                        {
                            TreeNode nodeProperty = nodeProperties.Nodes.Add(propertyInfo.Name, propertyInfo.Name, IMAGE_KEY_EMPTY);
                            nodeProperty.Tag = new Object[] { TreeViewLocalItemType.EntityProperties, propertyInfo };
                        }
                    }
                }
                //---

                //TODO : gérer les propriétés personalisées
                //--- Propriétés personalisées
                //for (int i = 0; i < entity.ListCustomProperties.Count; i++)
                //{
                //    KeyValuePair<String, Object> customProp = entity.ListCustomProperties.ElementAt(i);

                //    TreeNode node = new TreeNode(customProp.Key);
                //    node.Tag = customProp.Key;

                //    treeViewCustomProperties.Nodes.Add(node);
                //}
                //---

                //--- Script et Trigger
                AddScriptAndTriggerNode((IActionHandler)entity, (ITriggerHandler)entity, nodeEntity);
                //---

                //--- ParticleSystem
                if ((ItemTypeShowed & TreeViewLocalItemType.ParticleSystem) == TreeViewLocalItemType.ParticleSystem ||
                    (ItemTypeShowed & TreeViewLocalItemType.Script) == TreeViewLocalItemType.Script ||
                    (ItemTypeShowed & TreeViewLocalItemType.Trigger) == TreeViewLocalItemType.Trigger)
                {
                    TreeNode nodeParticleSystems = nodeEntity.Nodes.Add(NODE_PARTICLESYSTEM, NODE_PARTICLESYSTEM, IMAGE_KEY_PARTICLE_SYSTEM);
                    nodeParticleSystems.Tag = new Object[] { TreeViewLocalItemType.ParticleSystem };

                    foreach (ParticleSystem particleSystem in entity.ListParticleSystem)
                    {
                        TreeNode nodeParticleSystem = nodeParticleSystems.Nodes.Add(particleSystem.Name, particleSystem.Name, IMAGE_KEY_EMPTY);
                        nodeParticleSystem.Tag = new Object[] { TreeViewLocalItemType.ParticleSystem, particleSystem };

                        //--- Script
                        AddScriptAndTriggerNode((IActionHandler)particleSystem, null, nodeParticleSystem);
                        //---

                        foreach (Particle particle in particleSystem.ListParticleTemplate)
                        {
                            TreeNode nodeParticle = nodeParticleSystem.Nodes.Add(particle.Name, particle.Name, IMAGE_KEY_EMPTY);
                            nodeParticle.Tag = new Object[] { TreeViewLocalItemType.ParticleSystem, particle };

                            //--- Script et Trigger
                            AddScriptAndTriggerNode((IActionHandler)particle, (ITriggerHandler)particle, nodeParticle);
                            //---
                        }
                    }
                }
                //---
            }
            //---

            this.ExpandAll();

            //--- Coche les noeuds cochés avant le rafraichissement
            if (keepCheckedNode)
            {
                foreach (String nodePath in listCheckedNodesPath)
                {
                    CheckNode(Nodes[0], nodePath);
                }
            }
            //---

            //--- Garde les noeuds pliés selon l'état précédent le rafraichissement
            foreach (String nodePath in listCollapsedNodesPath)
            {
                TreeNode nodeToCollapse = GetNodeWithPath(nodePath);

                if (nodeToCollapse != null)
                    nodeToCollapse.Collapse();
            }
            //---

            //--- Positionne verticalement l'arborescence selon son état précédent le rafraichissement
            if (!String.IsNullOrEmpty(firstDrawingNodePath))
            {
                TreeNode newFirstDrawingNode = GetNodeWithPath(firstDrawingNodePath);

                if (newFirstDrawingNode != null)
                    newFirstDrawingNode.EnsureVisible();
            }
            //---

            this.EndUpdate();
            this.IsRefreshing = false;
        }

        private void AddScriptAndTriggerNode(IActionHandler actionHandler, ITriggerHandler triggerHandler, TreeNode nodeEntity)
        {
            //--- Script
            if (actionHandler != null && (ItemTypeShowed & TreeViewLocalItemType.Script) == TreeViewLocalItemType.Script)
            {
                TreeNode nodeScripts = nodeEntity.Nodes.Add(NODE_SCRIPT, NODE_SCRIPT, IMAGE_KEY_SCRIPT);
                nodeScripts.Tag = new Object[] { TreeViewLocalItemType.Script };

                foreach (Script script in actionHandler.ListScript)
                {
                    TreeNode nodeScript = nodeScripts.Nodes.Add(script.ScriptName, script.ScriptName, IMAGE_KEY_EMPTY);
                    nodeScript.Tag = new Object[] { TreeViewLocalItemType.Script, script };
                }
            }
            //---

            //--- Trigger
            if (triggerHandler != null && (ItemTypeShowed & TreeViewLocalItemType.Trigger) == TreeViewLocalItemType.Trigger)
            {
                TreeNode nodeTriggers = nodeEntity.Nodes.Add(NODE_TRIGGER, NODE_TRIGGER, IMAGE_KEY_TRIGGER);
                nodeTriggers.Tag = new Object[] { TreeViewLocalItemType.Trigger };

                foreach (TriggerBase trigger in triggerHandler.ListTrigger)
                {
                    TreeNode nodeTrigger = nodeTriggers.Nodes.Add(trigger.TriggerName, trigger.TriggerName, IMAGE_KEY_EMPTY);
                    nodeTrigger.Tag = new Object[] { TreeViewLocalItemType.Trigger, trigger };
                }
            }
            //---
        }

        private void ChangeNodeCheck(TreeNode node, bool isChecked)
        {
            Boolean prevIsCheckedByMouse = this.IsCheckedByMouse;
            this.IsCheckedByMouse = false;

            node.Checked = isChecked;

            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].Checked = isChecked;

                ChangeNodeCheck(node.Nodes[i], isChecked);
            }

            this.IsCheckedByMouse = prevIsCheckedByMouse;
        }

        public void CheckNode<T>(T nodeToCheck, string pathParent)
        {
            Boolean prevIsCheckedByMouse = this.IsCheckedByMouse;
            this.IsCheckedByMouse = false;

            if (nodeToCheck == null)
                return;

            string typeNode = String.Empty;

            if (nodeToCheck is PropertyInfo)
                typeNode = "\\" + NODE_PROPERTIES + "\\" + (nodeToCheck as PropertyInfo).Name;
            else if (nodeToCheck is Script)
                typeNode = "\\" + NODE_SCRIPT + "\\" + (nodeToCheck as Script).ScriptName;
            else if (nodeToCheck is TriggerBase)
                typeNode = "\\" + NODE_TRIGGER + "\\" + (nodeToCheck as TriggerBase).TriggerName;
            else if (nodeToCheck is ParticleSystem)
                typeNode = "\\" + NODE_PARTICLESYSTEM + "\\" + (nodeToCheck as ParticleSystem).Name;
            else if (nodeToCheck is Particle)
                typeNode = "\\" + NODE_PARTICLESYSTEM + "\\" + (nodeToCheck as Particle).ParticleSystem.Name + "\\" + (nodeToCheck as Particle).Name;

            string fullPath = pathParent + typeNode;

            CheckNode(Nodes[0], fullPath);

            this.IsCheckedByMouse = prevIsCheckedByMouse;
        }

        public void CheckNode(TreeNode node, string fullPath)
        {
            Boolean prevIsCheckedByMouse = this.IsCheckedByMouse;
            this.IsCheckedByMouse = false;

            if (node == null)
                return;

            if (
                node.FullPath == fullPath
                )
            {
                node.Checked = true;
            }
            else
            {
                node.Checked = false;
            }

            if (node.Nodes.Count > 0)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    CheckNode(node.Nodes[i], fullPath);
                }
            }

            this.IsCheckedByMouse = prevIsCheckedByMouse;
        }

        public void CheckNode<T>(T nodeToCheck)
        {
            Boolean prevIsCheckedByMouse = this.IsCheckedByMouse;
            this.IsCheckedByMouse = false;

            if (nodeToCheck == null)
                return;

            CheckNode<T>(Nodes[0], nodeToCheck);

            this.IsCheckedByMouse = prevIsCheckedByMouse;
        }

        public void CheckNode<T>(TreeNode node, T nodeToCheck)
        {
            Boolean prevIsCheckedByMouse = this.IsCheckedByMouse;
            this.IsCheckedByMouse = false;

            if (node == null)
                return;

            if (nodeToCheck == null)
                return;

            if (
                node.Tag is Object[] &&
                ((Object[])node.Tag).Length > 1 &&
                (((Object[])node.Tag)[1]) is T &&
                nodeToCheck.Equals(((T)(((Object[])node.Tag)[1])))
                )
            {
                node.Checked = true;
            }
            else
            {
                node.Checked = false;
            }

            if (node.Nodes.Count > 0)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    CheckNode(node.Nodes[i], nodeToCheck);
                }
            }

            this.IsCheckedByMouse = prevIsCheckedByMouse;
        }

        public void CheckNodes<T>(List<T> listNodesToCheck)
        {
            Boolean prevIsCheckedByMouse = this.IsCheckedByMouse;
            this.IsCheckedByMouse = false;

            if (listNodesToCheck == null)
                return;

            CheckNodes<T>(Nodes[0], listNodesToCheck);

            this.IsCheckedByMouse = prevIsCheckedByMouse;
        }

        public void CheckNodes<T>(TreeNode node, List<T> listNodesToCheck)
        {
            Boolean prevIsCheckedByMouse = this.IsCheckedByMouse;
            this.IsCheckedByMouse = false;

            if (node == null)
                return;

            if (listNodesToCheck == null)
                return;

            if (
                node.Tag is Object[] &&
                ((Object[])node.Tag).Length > 1 &&
                (((Object[])node.Tag)[1]) is T &&
                listNodesToCheck.Contains(((T)(((Object[])node.Tag)[1]))))
            {
                node.Checked = true;
            }
            else
            {
                node.Checked = false;
            }

            if (node.Nodes.Count > 0)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    CheckNodes(node.Nodes[i], listNodesToCheck);
                }
            }

            this.IsCheckedByMouse = prevIsCheckedByMouse;
        }

        public TreeNode GetNodeWithPath(string fullPath)
        {
            return GetNodeWithPath(Nodes[0], fullPath);
        }

        public TreeNode GetNodeWithPath(TreeNode node, string fullPath)
        {
            TreeNode nodeFound = null;

            if (node == null)
                return null;

            if (node.FullPath == fullPath)
            {
                return node;
            }

            if (node.Nodes.Count > 0)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    nodeFound = GetNodeWithPath(node.Nodes[i], fullPath);

                    if (nodeFound != null)
                        return nodeFound;
                }
            }

            return null;
        }

        public List<T> GetCheckedNodes<T>()
        {
            List<T> listCheckedNodes = new List<T>();

            GetCheckedNodes<T>(Nodes[0], listCheckedNodes);

            return listCheckedNodes;
        }

        public void GetCheckedNodes<T>(TreeNode node, List<T> listCheckedNodes)
        {
            if (node == null)
                return;

            if (node.Checked &&
                node.Tag is Object[] &&
                (((Object[])node.Tag)[1]) is T)
            {
                listCheckedNodes.Add((T)(((Object[])node.Tag)[1]));
            }

            if (node.Nodes.Count > 0)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    GetCheckedNodes(node.Nodes[i], listCheckedNodes);
                }
            }
        }


        public List<String> GetCollapsedNodesPath()
        {
            List<String> listCollapsedNodesPath = new List<String>();
            List<TreeNode> listCollapsedNodes = new List<TreeNode>();

            if (Nodes.Count > 0)
                GetCollapsedNodes(Nodes[0], listCollapsedNodes);

            foreach (TreeNode node in listCollapsedNodes)
            {
                listCollapsedNodesPath.Add(node.FullPath);
            }

            return listCollapsedNodesPath;
        }

        public void GetCollapsedNodes(TreeNode node, List<TreeNode> listCollapsedNodes)
        {
            if (node == null)
                return;

            if (!node.IsExpanded && node.Nodes.Count > 0)
            {
                listCollapsedNodes.Add(node);
            }

            if (node.Nodes.Count > 0)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    GetCollapsedNodes(node.Nodes[i], listCollapsedNodes);
                }
            }
        }

        public List<String> GetCheckedNodesPath()
        {
            List<String> listCheckedNodesPath = new List<String>();
            List<TreeNode> listCheckedNodes = GetCheckedNodes();

            foreach (TreeNode node in listCheckedNodes)
            {
                listCheckedNodesPath.Add(node.FullPath);
            }

            return listCheckedNodesPath;
        }

        public List<TreeNode> GetCheckedNodes()
        {
            List<TreeNode> listCheckedNodes = new List<TreeNode>();

            if (Nodes.Count > 0)
                GetCheckedNodes(Nodes[0], listCheckedNodes);

            return listCheckedNodes;
        }

        public void GetCheckedNodes(TreeNode node, List<TreeNode> listCheckedNodes)
        {
            if (node == null)
                return;

            if (node.Checked)
            {
                listCheckedNodes.Add(node);
            }

            if (node.Nodes.Count > 0)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    GetCheckedNodes(node.Nodes[i], listCheckedNodes);
                }
            }
        }

        public TreeNode GetNodeWithTag(TreeNode nodeParent, Entity entity, PropertyInfo propertyInfo, int index)
        {
            TreeNode node = null;

            foreach (TreeNode nodeChild in nodeParent.Nodes)
            {
                if (node == null &&
                    (nodeChild.Tag is Object[]) &&
                    nodeParent.Tag == entity &&
                    ((PropertyInfo)((Object[])nodeChild.Tag)[0]).Name == propertyInfo.Name &&
                     ((int)((Object[])nodeChild.Tag)[1]) == index)
                {
                    node = nodeChild;
                }

                if (node == null)
                {
                    node = GetNodeWithTag(nodeChild, entity, propertyInfo, index);
                }
            }

            return node;
        }

        public Object GetItemFromNode(TreeNode node)
        {
            Object item = null;

            if (node != null &&
                node.Tag != null &&
                node.Tag is Object[] &&
                ((Object[])node.Tag).Length > 1)
            {
                item = ((Object[])node.Tag)[1];
            }

            return item;
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (this.IsRefreshing || !e.Node.IsVisible)
                return;

            int deltaY = 0;
            bool isHeadNode = true;
            TreeViewLocalItemType itemType = TreeViewLocalItemType.None;

            if (e.Node.Tag is Object[])
            {
                itemType = (TreeViewLocalItemType)(((Object[])e.Node.Tag)[0]);
                isHeadNode = ((Object[])e.Node.Tag).Length == 1;
            }

            Point textPosition = new Point(e.Node.Level * this.Indent, 0);

            //--- Tampon graphique
            Bitmap bmp = new Bitmap(e.Bounds.Width, e.Bounds.Height);
            Graphics g = Graphics.FromImage(bmp);
            //---

            if (e.Node.Checked)
            {
                g.FillRectangle(WinformVisualStyle.BrushSelectedColor, 0, 0, e.Bounds.Width, e.Bounds.Height);
            }
            else if ((e.State & TreeNodeStates.Hot) == TreeNodeStates.Hot)
            {
                g.FillRectangle(WinformVisualStyle.BrushMouseOverColor, 0, 0, e.Bounds.Width, e.Bounds.Height);
            }
            else
            {
                g.FillRectangle(backgroundBrush, 0, 0, e.Bounds.Width, e.Bounds.Height);
            }

            //--- Flèche Collapsed / Expanded
            if (e.Node.Nodes.Count > 0)
            {
                deltaY = this.ItemHeight / 2 - ImageList.Images[IMAGE_KEY_COLLAPSED].Height / 2;

                if (e.Node.IsExpanded)
                    g.DrawImage(ImageList.Images[IMAGE_KEY_EXPANDED], textPosition.X, textPosition.Y + deltaY);
                else
                    g.DrawImage(ImageList.Images[IMAGE_KEY_COLLAPSED], textPosition.X, textPosition.Y + deltaY);

                textPosition.X += 8;
            }
            //---

            //--- Noeuds d'entête sans case à cocher mais avec icône
            if (e.Node.ImageKey != IMAGE_KEY_EMPTY)
            {
                deltaY = this.ItemHeight / 2 - ImageList.Images[e.Node.ImageKey].Height / 2;

                g.DrawImage(ImageList.Images[e.Node.ImageKey], textPosition.X, textPosition.Y + deltaY);
                textPosition.X += ImageList.ImageSize.Width;
            }
            //---

            //--- Détection de la case à cocher
            if (itemType != TreeViewLocalItemType.None &&
                (itemType & ItemTypeCheckBoxed) == itemType &&
                !isHeadNode)
            {
                deltaY = this.ItemHeight / 2 - ImageList.Images[IMAGE_KEY_CHECKED].Height / 2;

                if (e.Node.Checked)
                    g.DrawImage(ImageList.Images[IMAGE_KEY_CHECKED], textPosition.X, textPosition.Y + deltaY);
                else
                    g.DrawImage(ImageList.Images[IMAGE_KEY_UNCHECKED], textPosition.X, textPosition.Y + deltaY);

                textPosition.X += 10;
            }
            //---

            //--- Affichage du texte
            Font font = new Font(this.Font, isHeadNode ? FontStyle.Bold : FontStyle.Regular);

            deltaY = this.ItemHeight / 2 - (int)g.MeasureString(e.Node.Text, font).Height / 2;
            g.DrawString(e.Node.Text, font, WinformVisualStyle.BrushForeColor1, textPosition.X, textPosition.Y + deltaY);
            //---

            //--- Affiche le tampon graphique
            e.Graphics.DrawImage(bmp, e.Bounds.Left, e.Bounds.Top);
            //---
        }

        private void TreeViewLocal_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsCheckedByMouse = true;

                //---
                bool isHeadNode = false;
                TreeViewLocalItemType itemType = TreeViewLocalItemType.None;

                if (e.Node.Tag is Object[])
                {
                    itemType = (TreeViewLocalItemType)(((Object[])e.Node.Tag)[0]);
                    isHeadNode = ((Object[])e.Node.Tag).Length == 1;
                }
                //---

                //--- Détection de la position de la flèche
                Rectangle recArrow = new Rectangle(e.Node.Level * this.Indent - 8, e.Node.Bounds.Y, 16, this.ItemHeight);
                //---

                if (recArrow.Contains(e.Location))
                {
                    e.Node.Toggle();
                }
                else if (itemType != TreeViewLocalItemType.None &&
                        (itemType & ItemTypeCheckBoxed) == itemType &&
                        !isHeadNode)
                {
                    Boolean prevCheckState = e.Node.Checked;

                    //--- Déselectionne tous les noeuds
                    if (!AllowMultipleItemChecked)
                    {
                        ChangeNodeCheck(Nodes[0], false);
                    }
                    //---

                    if (!AllowUncheckedNode & prevCheckState)
                    {
                        e.Node.Checked = prevCheckState;
                    }
                    else
                        e.Node.Checked = !prevCheckState;
                }

                IsCheckedByMouse = false;
            }
        }

        private void TreeViewLocal_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                e.Node.Expand();

                foreach (TreeNode nodeChild in e.Node.Nodes)
                {
                    nodeChild.Collapse();
                }
            }
        }
    }

    public enum TreeViewLocalOrderType
    {
        OrderByEntity,
        OrderByLayer
    }

    [FlagsAttribute]
    public enum TreeViewLocalItemType : int
    {
        None = 0,
        World = 1,
        Entity = 2,
        Script = 4,
        Trigger = 8,
        ParticleSystem = 16,
        SubEntity = 32,
        EntityProperties = 64,
        CustomProperties = 128,
        Mouse = 256
    }
}
