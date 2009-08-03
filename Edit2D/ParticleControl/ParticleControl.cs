using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Edit2D.Particles;

namespace Edit2D.ParticleControl
{
    public partial class ParticleControl : UserControl
    {
        public Repository repository { get; set; }

        public ParticleControl()
        {
            InitializeComponent();
        }

        #region Events
        private void btnAddParticleSystem_Click(object sender, EventArgs e)
        {
            if (repository.CurrentEntite != null)
            {
                ParticleSystem particleSystem = new ParticleSystem(repository.CurrentEntite);
                particleSystem.ParticleSystemName = String.Format("ParticleSystem{0}", repository.CurrentEntite.ListParticleSystem.Count + 1);
                repository.CurrentEntite.ListParticleSystem.Add(particleSystem);

                RefreshParticleSystemListBox();
                listBoxParticleSystem.SelectedIndex = listBoxParticleSystem.Items.Count - 1;
            }
        }

        private void btnDelParticleSystem_Click(object sender, EventArgs e)
        {
            ParticleSystem particleSystem = GetCurrentParticleSystem();

            if (particleSystem != null)
            {
                repository.CurrentEntite.ListParticleSystem.Remove(particleSystem);


                RefreshParticleSystemListBox();

                if (listBoxParticleSystem.Items.Count > 0)
                    listBoxParticleSystem.SelectedIndex = 0;
                else
                {
                    propParticleSystem.SelectedObject = null;
                    listBoxParticleSystem.SelectedIndex = -1;
                }
            }
        }

        private void listBoxParticleSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            propParticleSystem.SelectedObject = GetCurrentParticleSystem();

            RefreshParticleTemplateListBox();

            if (listBoxParticleTemplate.Items.Count > 0)
                listBoxParticleTemplate.SelectedIndex = 0;
        }

        private void btnAddParticleTemplate_Click(object sender, EventArgs e)
        {
            ParticleSystem particleSystem = GetCurrentParticleSystem();

            if (particleSystem != null)
            {
                Bitmap bmp = null;

                if (cmbParticleTemplate.SelectedIndex != -1)
                {
                    bmp = (Bitmap)cmbParticleTemplate.SelectedItem;
                }
                else
                {
                   bmp =  (Bitmap)cmbParticleTemplate.Items[0];
                }

                string particleName = String.Format("ParticleTemplate{0}", particleSystem.ListParticleTemplate.Count + 1);
                Particle particleTemplate = new Particle(false, bmp.Tag.ToString(), particleName, particleSystem);

                particleSystem.ListParticleTemplate.Add(particleTemplate);

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
                    propParticleTemplate.SelectedObject = null;
                    listBoxParticleTemplate.SelectedIndex = -1;
                }
            }
        }

        private void listBoxParticleTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Particle particleTemplate = GetCurrentParticleTemplate();

            propParticleTemplate.SelectedObject = particleTemplate;

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
                particleTemplate.ChangeTexture(bmp.Tag.ToString(), true);

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

        private void RefreshParticleSystemListBox()
        {
            listBoxParticleSystem.Items.Clear();

            if (repository.CurrentEntite != null)
            {
                foreach (ParticleSystem particleSystem in repository.CurrentEntite.ListParticleSystem)
                {
                    listBoxParticleSystem.Items.Add(particleSystem.ParticleSystemName);
                }
            }
        }

        private void RefreshParticleTemplateListBox()
        {
            listBoxParticleTemplate.Items.Clear();

            ParticleSystem particleSystem = GetCurrentParticleSystem();

            if (particleSystem != null)
            {
                foreach (Particle particleTemplate in particleSystem.ListParticleTemplate)
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
            if (repository.CurrentEntite == null)
            {
                this.Visible = false;
                return;
            }
            else
            {
                this.Visible = true;
            }

            RefreshParticleSystemListBox();

            if (listBoxParticleSystem.Items.Count > 0)
                listBoxParticleSystem.SelectedIndex = 0;
            else
            {
                RefreshParticleTemplateListBox();
                propParticleSystem.SelectedObject = null;
                propParticleTemplate.SelectedObject = null;
                cmbParticleTemplate.SelectedIndex = 0;
            }
        }

        public ParticleSystem GetCurrentParticleSystem()
        {
            if (repository.CurrentEntite != null && listBoxParticleSystem.SelectedIndex != -1)
            {
                return repository.CurrentEntite.ListParticleSystem[listBoxParticleSystem.SelectedIndex];
            }

            return null;
        }

        public Particle GetCurrentParticleTemplate()
        {
            ParticleSystem particleSystem = GetCurrentParticleSystem();

            if (particleSystem != null && listBoxParticleTemplate.SelectedIndex != -1)
            {
                return particleSystem.ListParticleTemplate[listBoxParticleTemplate.SelectedIndex];
            }

            return null;
        }
        #endregion
    }
}
