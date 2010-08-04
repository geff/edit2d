using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Edit2DEngine.Particles;
using Edit2DEngine;
using Edit2D.UC;

namespace Edit2D.ParticleControl
{
    public partial class ParticleControl : UserControlLocal
    {
        public Repository Repository { get; set; }

        public ParticleControl()
        {
            InitializeComponent();

            propParticleSystem.PropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(PropertyGrid_PropertyValueChanged);
            propParticleTemplate.PropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(PropertyGrid_PropertyValueChanged);
        }

        #region Events
        private void ParticleControl_Load(object sender, EventArgs e)
        {
            InitComboParticleTexture();
        }

        private void PropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            RefreshGlobalTreeView();
        }

        private void btnAddParticleSystem_Click(object sender, EventArgs e)
        {
            AddParticleSystem();
        }

        private void btnDelParticleSystem_Click(object sender, EventArgs e)
        {
            DeleteParticleSystem();
        }

        private void listBoxParticleSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Repository.CurrentParticleSystem = null;

            if (Repository.CurrentEntite != null && listBoxParticleSystem.SelectedIndex != -1)
            {
                Repository.CurrentParticleSystem = Repository.CurrentEntite.ListParticleSystem[listBoxParticleSystem.SelectedIndex];
            }

            CheckNodeGlobalTreeView<ParticleSystem>(Repository.CurrentParticleSystem);
            propParticleSystem.PropertyGrid.SelectedObject = Repository.CurrentParticleSystem;

            RefreshParticleTemplateListBox(true);
        }

        private void btnAddParticleTemplate_Click(object sender, EventArgs e)
        {
            AddParticleTemplate();
        }

        private void btnDelParticleTemplate_Click(object sender, EventArgs e)
        {
            DeleteParticleTemplate();
        }

        private void btnAddParticleSystem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                AddParticleSystem();
            }
        }

        private void btnDelParticleSystem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                DeleteParticleSystem();
            }
        }

        private void btnParticleTemplate_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                AddParticleTemplate();
            }
        }

        private void btnDelParticleTemplate_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                DeleteParticleTemplate();
            }
        }

        private void listBoxParticleTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Particle particleTemplate = GetCurrentParticleTemplate();

            propParticleTemplate.PropertyGrid.SelectedObject = particleTemplate;

            if (particleTemplate != null)
            {
                foreach (Bitmap bmp in cmbParticleTemplate.Items)
                {
                    if (bmp.Tag.ToString() == particleTemplate.TextureName)
                    {
                        cmbParticleTemplate.SelectedItem = bmp;
                    }
                }
            }
            else
            {
                cmbParticleTemplate.SelectedIndex = 0;
            }
        }

        private void btnModifParticleTemplateTexture_Click(object sender, EventArgs e)
        {
            Particle particleTemplate = GetCurrentParticleTemplate();

            if (particleTemplate != null && cmbParticleTemplate.SelectedIndex != -1)
            {
                Bitmap bmp = (Bitmap)cmbParticleTemplate.SelectedItem;

                //TODO : il faut peut etre supprimer les body précédents
                particleTemplate.ChangeTexture(bmp.Tag.ToString(), true, true);

                propParticleTemplate.Refresh();
            }
        }

        private void cmbParticleTemplate_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                if (e.State == DrawItemState.Focus)
                {
                    e.Graphics.FillRectangle(WinformVisualStyle.BrushMouseOverColor, e.Bounds);
                }
                else
                {
                    e.DrawBackground();
                }

                Bitmap bmp = (Bitmap)cmbParticleTemplate.Items[e.Index];
                Rectangle rec = new Rectangle(10, e.Bounds.Top, e.Bounds.Height, e.Bounds.Height);
                e.Graphics.DrawImage(bmp, rec);

                e.Graphics.DrawString(bmp.Tag.ToString(), cmbParticleTemplate.Font, WinformVisualStyle.BrushForeColor1, new Point(e.Bounds.Location.X + 50, e.Bounds.Location.Y + 10));
            }
        }
        #endregion

        #region Private methods
        private void InitComboParticleTexture()
        {
            cmbParticleTemplate.Items.Clear();

            foreach (String textureName in TextureManager.ListParticleTexture2D.Keys)
            {
                Bitmap bmp = TextureManager.GetBitmapFromTexture2D(TextureManager.ListParticleTexture2D[textureName]);
                bmp.Tag = textureName;

                cmbParticleTemplate.Items.Add(bmp);
            }
        }

        private void RefreshParticleSystemListBox(bool selectParticleSystem)
        {
            listBoxParticleSystem.Items.Clear();

            if (Repository.CurrentEntite != null)
            {
                foreach (ParticleSystem particleSystem in Repository.CurrentEntite.ListParticleSystem)
                {
                    listBoxParticleSystem.Items.Add(particleSystem.Name);
                }
            }

            if (selectParticleSystem)
            {
                if (Repository.CurrentObject is Particle)
                {
                    listBoxParticleSystem.SelectedIndex = listBoxParticleSystem.FindString(Repository.CurrentParticleSystem.Name);
                    RefreshParticleTemplateListBox(true);
                }
                else if (Repository.CurrentParticleSystem != null)
                {
                    listBoxParticleSystem.SelectedIndex = listBoxParticleSystem.FindString(Repository.CurrentParticleSystem.Name);
                }
                else if (listBoxParticleSystem.Items.Count > 0)
                {
                    RefreshGlobalTreeView();
                    listBoxParticleSystem.SelectedIndex = 0;
                }
                else
                {
                    propParticleSystem.PropertyGrid.SelectedObject = null;
                    listBoxParticleSystem.SelectedIndex = -1;
                    RefreshParticleTemplateListBox(true);

                    RefreshGlobalTreeView<Entite>(Repository.CurrentEntite);
                }
            }
        }

        private void RefreshParticleTemplateListBox(bool selectParticleTemplate)
        {
            listBoxParticleTemplate.Items.Clear();

            if (Repository.CurrentParticleSystem != null)
            {
                foreach (Particle particleTemplate in Repository.CurrentParticleSystem.ListParticleTemplate)
                {
                    listBoxParticleTemplate.Items.Add(particleTemplate.Name);
                }
            }

            if (selectParticleTemplate)
            {
                if (Repository.CurrentObject is Particle)
                {
                    listBoxParticleTemplate.SelectedIndex = listBoxParticleTemplate.FindString(((Particle)Repository.CurrentObject).Name);
                    RefreshGlobalTreeView<Particle>((Particle)Repository.CurrentObject);
                }
                else if (listBoxParticleTemplate.Items.Count > 0)
                {
                    listBoxParticleTemplate.SelectedIndex = 0;
                }
                else
                {
                    listBoxParticleTemplate.SelectedIndex = -1;
                    propParticleTemplate.PropertyGrid.SelectedObject = null;
                    listBoxParticleTemplate.SelectedIndex = -1;
                }
            }
        }
        #endregion

        #region Public methods
        public void InitParticleControl()
        {
            InitComboParticleTexture();
        }

        public void RefreshParticleControl(bool selectParticleSystem)
        {

            RefreshParticleSystemListBox(selectParticleSystem);

            this.Visible = (Repository.CurrentEntite != null);
        }

        public void AddParticleSystem()
        {
            if (Repository.CurrentEntite != null)
            {
                ParticleSystem particleSystem = new ParticleSystem(Repository.CurrentEntite);
                particleSystem.Name = Common.CreateNewName<ParticleSystem>(Repository.CurrentEntite.ListParticleSystem, "Name", "ParticleSystem{0}");

                Repository.CurrentEntite.ListParticleSystem.Add(particleSystem);

                //--- Rafraichissement de la liste des scripts et de l'arborescence
                RefreshParticleSystemListBox(false);
                RefreshGlobalTreeView();
                //---

                //--- Sélectionne le nouveau système de particules
                listBoxParticleSystem.SelectedIndex = listBoxParticleSystem.Items.Count - 1;
                //---
            }
        }

        public void DeleteParticleSystem()
        {
            if (Repository.CurrentEntite != null && Repository.CurrentParticleSystem != null)
            {
                Repository.CurrentEntite.ListParticleSystem.Remove(Repository.CurrentParticleSystem);

                Repository.CurrentParticleSystem = null;

                RefreshParticleSystemListBox(true);
            }
        }

        public Particle AddParticleTemplate()
        {
            Particle particleTemplate = null;

            if (Repository.CurrentParticleSystem != null)
            {
                Bitmap bmp = null;

                if (cmbParticleTemplate.SelectedIndex != -1)
                {
                    bmp = (Bitmap)cmbParticleTemplate.SelectedItem;
                }
                else
                {
                    bmp = (Bitmap)cmbParticleTemplate.Items[0];
                }

                string particleName = Common.CreateNewName<Particle>(Repository.CurrentParticleSystem.ListParticleTemplate, "Name", "Particle{0}");

                particleTemplate = new Particle(false, bmp.Tag.ToString(), particleName, Repository.CurrentParticleSystem);

                Repository.CurrentParticleSystem.ListParticleTemplate.Add(particleTemplate);

                RefreshParticleTemplateListBox(false);
                listBoxParticleTemplate.SelectedIndex = listBoxParticleTemplate.Items.Count - 1;
                cmbParticleTemplate.SelectedItem = bmp;

                RefreshGlobalTreeView();
            }

            return particleTemplate;
        }

        public void DeleteParticleTemplate()
        {
            Particle particleTemplate = GetCurrentParticleTemplate();

            if (particleTemplate != null)
            {
                particleTemplate.ParticleSystem.ListParticleTemplate.Remove(particleTemplate);

                if (Repository.CurrentObject is Particle)
                {
                    Repository.CurrentObject = null;
                }

                RefreshParticleTemplateListBox(true);

                RefreshGlobalTreeView<ParticleSystem>(Repository.CurrentParticleSystem);
            }
        }

        public Particle GetCurrentParticleTemplate()
        {
            if (Repository.CurrentParticleSystem != null && listBoxParticleTemplate.SelectedIndex != -1)
            {
                return Repository.CurrentParticleSystem.ListParticleTemplate[listBoxParticleTemplate.SelectedIndex];
            }

            return null;
        }
        #endregion
    }
}
