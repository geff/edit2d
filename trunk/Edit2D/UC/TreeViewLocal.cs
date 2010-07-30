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
using Edit2DEngine.Particles;

namespace Edit2D.UC
{
    public partial class TreeViewLocal : TreeView
    {
        public Repository Repository { get; set; }

        [Browsable(true)]
        public TreeViewLocalOrderType OrderType { get; set; }
        public TreeViewLocalItemType ItemTypeShowed { get; set; }
        public TreeViewLocalItemType ItemTypeCheckBoxed { get; set; }

        private const int IMAGE_INDEX_EMPTY = 0;
        private const int IMAGE_INDEX_ENTITY = 1;
        private const int IMAGE_INDEX_PARTICLE_SYSTEM = 2;
        private const int IMAGE_INDEX_SCRIPT = 3;
        private const int IMAGE_INDEX_TRIGGER = 4;
        private const int IMAGE_INDEX_WORLD = 5;
        private const int IMAGE_INDEX_CHECKED = 6;
        private const int IMAGE_INDEX_UNCHECKED = 7;
        //private const int IMAGE_INDEX_SUBENTITY = 6;

        private SolidBrush backgroundBrush;

        public TreeViewLocal()
        {
            InitializeComponent();
        }

        public void RefreshView()
        {
            backgroundBrush = new SolidBrush(this.BackColor);

            this.Nodes.Clear();

            //--- World
            TreeNode nodeWorld = this.Nodes.Add("World", "World", IMAGE_INDEX_WORLD, IMAGE_INDEX_WORLD);
            //---

            //--- Entités
            TreeNode nodeEntities = nodeWorld.Nodes.Add("Entity", "Entités", IMAGE_INDEX_ENTITY, IMAGE_INDEX_ENTITY);
            //---

            //--- Entité
            foreach (Entite entite in Repository.listEntite)
            {
                TreeNode nodeEntity = nodeEntities.Nodes.Add(entite.Name, entite.Name, IMAGE_INDEX_EMPTY, IMAGE_INDEX_EMPTY);

                //--- Script
                if ((ItemTypeShowed & TreeViewLocalItemType.Script) == TreeViewLocalItemType.Script)
                {
                    TreeNode nodeScripts = nodeEntity.Nodes.Add("Script", "Scripts", IMAGE_INDEX_SCRIPT, IMAGE_INDEX_SCRIPT);
                    nodeScripts.Tag = new Object[] { TreeViewLocalItemType.Script };

                    foreach (Script script in entite.ListScript)
                    {
                        TreeNode nodeScript = nodeScripts.Nodes.Add(script.ScriptName, script.ScriptName, IMAGE_INDEX_EMPTY, IMAGE_INDEX_EMPTY);
                        nodeScript.Tag = new Object[] { TreeViewLocalItemType.Script, script };
                    }
                }
                //---

                //--- Trigger
                if ((ItemTypeShowed & TreeViewLocalItemType.Trigger) == TreeViewLocalItemType.Trigger)
                {
                    TreeNode nodeTriggers = nodeEntity.Nodes.Add("Trigger", "Déclencheurs", IMAGE_INDEX_TRIGGER, IMAGE_INDEX_TRIGGER);
                    nodeTriggers.Tag = new Object[] { TreeViewLocalItemType.Trigger };

                    foreach (TriggerBase trigger in entite.ListTrigger)
                    {
                        TreeNode nodeTrigger = nodeTriggers.Nodes.Add(trigger.TriggerName, trigger.TriggerName, IMAGE_INDEX_EMPTY, IMAGE_INDEX_EMPTY);
                        nodeTrigger.Tag = new Object[] { TreeViewLocalItemType.Trigger, trigger };
                    }
                }
                //---


                //--- ParticleSystem
                if ((ItemTypeShowed & TreeViewLocalItemType.ParticleSystem) == TreeViewLocalItemType.ParticleSystem)
                {
                    TreeNode nodeParticleSystems = nodeEntity.Nodes.Add("ParticleSystem", "Systèmes de particules", IMAGE_INDEX_PARTICLE_SYSTEM, IMAGE_INDEX_PARTICLE_SYSTEM);
                    nodeParticleSystems.Tag = new Object[] { TreeViewLocalItemType.ParticleSystem };

                    foreach (ParticleSystem particleSystem in entite.ListParticleSystem)
                    {
                        TreeNode nodeParticleSystem = nodeParticleSystems.Nodes.Add(particleSystem.ParticleSystemName, particleSystem.ParticleSystemName, IMAGE_INDEX_EMPTY, IMAGE_INDEX_EMPTY);
                        nodeParticleSystem.Tag = new Object[] { TreeViewLocalItemType.ParticleSystem, particleSystem };

                        foreach (Particle particle in particleSystem.ListParticleTemplate)
                        {
                            TreeNode nodeParticle = nodeParticleSystem.Nodes.Add(particle.Name, particle.Name, IMAGE_INDEX_EMPTY, IMAGE_INDEX_EMPTY);
                            nodeParticle.Tag = new Object[] { TreeViewLocalItemType.ParticleSystem, particle };
                        }
                    }
                }
                //---


            }
            //---

            this.ExpandAll();
        }

        private void TreeViewLocal_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse)
            {
                //---
                bool isHeadNode = false;
                TreeViewLocalItemType itemType = TreeViewLocalItemType.None;

                if (e.Node.Tag is Object[])
                {
                    itemType = (TreeViewLocalItemType)(((Object[])e.Node.Tag)[0]);
                    isHeadNode = ((Object[])e.Node.Tag).Length == 1;
                }
                //---

                if (itemType != TreeViewLocalItemType.None &&
                    (itemType & ItemTypeCheckBoxed) == itemType &&
                    !isHeadNode)
                {
                    e.Node.Checked = !e.Node.Checked;
                }
            }
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            int deltaY = 0;
            bool isHeadNode = false;
            TreeViewLocalItemType itemType = TreeViewLocalItemType.None;

            if (e.Node.Tag is Object[])
            {
                itemType = (TreeViewLocalItemType)(((Object[])e.Node.Tag)[0]);
                isHeadNode = ((Object[])e.Node.Tag).Length == 1;
            }

            Point textPosition = e.Bounds.Location;
            textPosition.X += e.Node.Level * this.Indent;

            if (e.Node.Checked)
            {
                e.Graphics.FillRectangle(WinformVisualStyle.BrushSelectedColor, e.Bounds);
            }
            else if (e.State == TreeNodeStates.Hot || e.State == TreeNodeStates.Focused || e.State == TreeNodeStates.Selected ||e.State == TreeNodeStates.Marked)
            {
                e.Graphics.FillRectangle(WinformVisualStyle.BrushMouseOverColor, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            //--- Noeuds d'entête sans case à cocher mais avec icône
            if (e.Node.ImageIndex >= 0)
            {
                deltaY = this.ItemHeight / 2 - ImageList.Images[e.Node.ImageIndex].Height / 2;

                e.Graphics.DrawImage(ImageList.Images[e.Node.ImageIndex], textPosition.X, textPosition.Y + deltaY);
                textPosition.X += ImageList.ImageSize.Width;
            }
            //---

            //--- Détection de la case à cocher
            if (itemType != TreeViewLocalItemType.None &&
                (itemType & ItemTypeCheckBoxed) == itemType &&
                !isHeadNode)
            {
                deltaY = this.ItemHeight / 2 - ImageList.Images[IMAGE_INDEX_CHECKED].Height / 2;

                if (e.Node.Checked)
                    e.Graphics.DrawImage(ImageList.Images[IMAGE_INDEX_CHECKED], textPosition.X, textPosition.Y + deltaY);
                else
                    e.Graphics.DrawImage(ImageList.Images[IMAGE_INDEX_UNCHECKED], textPosition.X, textPosition.Y + deltaY);

                textPosition.X += 10;
            }
            //---

            //--- Affichage du texte
            deltaY = this.ItemHeight / 2 - (int)e.Graphics.MeasureString(e.Node.Text, this.Font).Height / 2;
            e.Graphics.DrawString(e.Node.Text, this.Font, WinformVisualStyle.BrushForeColor1, textPosition.X, textPosition.Y + deltaY);
            //---
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
        Script = 1,
        Trigger = 2,
        ParticleSystem = 4,
        SubEntity = 8,
        EntityProperties = 16,
        CustomProperties = 32
    }
}
