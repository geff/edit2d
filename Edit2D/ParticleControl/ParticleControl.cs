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

namespace Edit2D.ParticleControl
{
    public partial class ParticleControl : UserControl
    {
        public Repository Repository { get; set; }

        public ParticleControl()
        {
            InitializeComponent();
        }

        #region Events
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

            propParticleSystem.PropertyGrid.SelectedObject = Repository.CurrentParticleSystem;

            RefreshParticleTemplateListBox();

            if (listBoxParticleTemplate.Items.Count > 0)
                listBoxParticleTemplate.SelectedIndex = 0;
        }

        private void btnAddParticleTemplate_Click(object sender, EventArgs e)
        {
            //ParticleSystem particleSystem = GetCurrentParticleSystem();

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

                Particle particleTemplate = new Particle(false, bmp.Tag.ToString(), particleName, Repository.CurrentParticleSystem);

                Repository.CurrentParticleSystem.ListParticleTemplate.Add(particleTemplate);

                RefreshParticleTemplateListBox();
                listBoxParticleTemplate.SelectedIndex = listBoxParticleTemplate.Items.Count - 1;
                cmbParticleTemplate.SelectedItem = bmp;
            }
        }

        private void btnDelParticleTemplate_Click(object sender, EventArgs e)
        {
            Particle particleTemplate = GetCurrentParticleTemplate();

            if (particleTemplate != null)
            {
                particleTemplate.ParticleSystem.ListParticleTemplate.Remove(particleTemplate);

                RefreshParticleTemplateListBox();

                if (listBoxParticleTemplate.Items.Count > 0)
                    listBoxParticleTemplate.SelectedIndex = 0;
                else
                {
                    cmbParticleTemplate.SelectedIndex = 0;
                    propParticleTemplate.PropertyGrid.SelectedObject = null;
                    listBoxParticleTemplate.SelectedIndex = -1;
                }
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
                    e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
                }
                else
                {
                    e.DrawBackground();
                }

                Bitmap bmp = (Bitmap)cmbParticleTemplate.Items[e.Index];
                Rectangle rec = new Rectangle(10, e.Bounds.Top, e.Bounds.Height, e.Bounds.Height);
                e.Graphics.DrawImage(bmp, rec);

                e.Graphics.DrawString(bmp.Tag.ToString(), cmbParticleTemplate.Font, Brushes.Black, new Point(e.Bounds.Location.X + 50, e.Bounds.Location.Y + 10));
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
                if (listBoxParticleSystem.Items.Count > 0)
                    listBoxParticleSystem.SelectedIndex = 0;
                else
                {
                    propParticleSystem.PropertyGrid.SelectedObject = null;
                    listBoxParticleSystem.SelectedIndex = -1;
                }
            }
        }

        private void RefreshParticleTemplateListBox()
        {
            listBoxParticleTemplate.Items.Clear();

            //ParticleSystem particleSystem = GetCurrentParticleSystem();

            if (Repository.CurrentParticleSystem != null)
            {
                foreach (Particle particleTemplate in Repository.CurrentParticleSystem.ListParticleTemplate)
                {
                    listBoxParticleTemplate.Items.Add(particleTemplate.Name);
                }
            }
        }
        #endregion

        #region Public methods
        public void InitParticleControl()
        {
            InitComboParticleTexture();
        }

        public void RefreshParticleControl()
        {
            if (Repository.CurrentEntite == null)
            {
                this.Visible = false;
                return;
            }
            else
            {
                this.Visible = true;
            }

            RefreshParticleSystemListBox(true);

            //if (listBoxParticleSystem.Items.Count > 0)
            //    listBoxParticleSystem.SelectedIndex = 0;
            //else
            //{
            //    RefreshParticleTemplateListBox();
            //    propParticleSystem.PropertyGrid.SelectedObject = null;
            //    propParticleTemplate.PropertyGrid.SelectedObject = null;
            //    cmbParticleTemplate.SelectedIndex = 0;
            //}
        }

        public void AddParticleSystem()
        {
            if (Repository.CurrentEntite != null)
            {
                ParticleSystem particleSystem = new ParticleSystem(Repository.CurrentEntite);
                particleSystem.Name = Common.CreateNewName<ParticleSystem>(Repository.CurrentEntite.ListParticleSystem, "Name", "ParticleSystem{0}");

                Repository.CurrentEntite.ListParticleSystem.Add(particleSystem);

                RefreshParticleSystemListBox(false);
                listBoxParticleSystem.SelectedIndex = listBoxParticleSystem.Items.Count - 1;
            }
        }

        public void DeleteParticleSystem()
        {
            if (Repository.CurrentParticleSystem != null)
            {
                Repository.CurrentEntite.ListParticleSystem.Remove(Repository.CurrentParticleSystem);

                RefreshParticleSystemListBox(true);
            }
        }

        //public ParticleSystem GetCurrentParticleSystem()
        //{
        //    if (Repository.CurrentEntite != null && listBoxParticleSystem.SelectedIndex != -1)
        //    {
        //        return Repository.CurrentEntite.ListParticleSystem[listBoxParticleSystem.SelectedIndex];
        //    }

        //    return null;
        //}

        public Particle GetCurrentParticleTemplate()
        {
            //ParticleSystem particleSystem = GetCurrentParticleSystem();

            if (Repository.CurrentParticleSystem != null && listBoxParticleTemplate.SelectedIndex != -1)
            {
                return Repository.CurrentParticleSystem.ListParticleTemplate[listBoxParticleTemplate.SelectedIndex];
            }

            return null;
        }
        #endregion
    }
}
