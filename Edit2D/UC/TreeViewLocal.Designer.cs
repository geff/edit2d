namespace Edit2D.UC
{
    partial class TreeViewLocal
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeViewLocal));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "icon_Empty.png");
            this.imageList.Images.SetKeyName(1, "icon_Entity.png");
            this.imageList.Images.SetKeyName(2, "icon_ParticleSystem.png");
            this.imageList.Images.SetKeyName(3, "icon_Script.gif");
            this.imageList.Images.SetKeyName(4, "icon_Trigger.png");
            this.imageList.Images.SetKeyName(5, "icon_World.png");
            this.imageList.Images.SetKeyName(6, "icon_Checked.png");
            this.imageList.Images.SetKeyName(7, "icon_Unchecked.png");
            // 
            // TreeViewLocal
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.HideSelection = false;
            this.HotTracking = true;
            this.ImageIndex = 0;
            this.ImageList = this.imageList;
            this.Indent = 19;
            this.ItemHeight = 25;
            this.LineColor = System.Drawing.Color.Black;
            this.SelectedImageIndex = 0;
            this.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewLocal_AfterSelect);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
    }
}
