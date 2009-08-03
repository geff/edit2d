namespace Edit2D.ParticleControl
{
    partial class ParticleControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlParticleControl = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxParticleTemplate = new System.Windows.Forms.ListBox();
            this.propParticleTemplate = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDelParticleTemplate = new System.Windows.Forms.Button();
            this.btnAddParticleTemplate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDelParticleSystem = new System.Windows.Forms.Button();
            this.btnAddParticleSystem = new System.Windows.Forms.Button();
            this.listBoxParticleSystem = new System.Windows.Forms.ListBox();
            this.propParticleSystem = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnModifParticleTemplateTexture = new System.Windows.Forms.Button();
            this.cmbParticleTemplate = new System.Windows.Forms.ComboBox();
            this.pnlParticleControl.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlParticleControl
            // 
            this.pnlParticleControl.ColumnCount = 4;
            this.pnlParticleControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlParticleControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlParticleControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlParticleControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlParticleControl.Controls.Add(this.listBoxParticleTemplate, 2, 1);
            this.pnlParticleControl.Controls.Add(this.propParticleTemplate, 3, 1);
            this.pnlParticleControl.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.pnlParticleControl.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.pnlParticleControl.Controls.Add(this.listBoxParticleSystem, 0, 1);
            this.pnlParticleControl.Controls.Add(this.propParticleSystem, 1, 0);
            this.pnlParticleControl.Controls.Add(this.panel1, 3, 0);
            this.pnlParticleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParticleControl.Location = new System.Drawing.Point(0, 0);
            this.pnlParticleControl.Name = "pnlParticleControl";
            this.pnlParticleControl.RowCount = 2;
            this.pnlParticleControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.pnlParticleControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlParticleControl.Size = new System.Drawing.Size(795, 194);
            this.pnlParticleControl.TabIndex = 0;
            // 
            // listBoxParticleTemplate
            // 
            this.listBoxParticleTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxParticleTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxParticleTemplate.FormattingEnabled = true;
            this.listBoxParticleTemplate.Location = new System.Drawing.Point(399, 46);
            this.listBoxParticleTemplate.Name = "listBoxParticleTemplate";
            this.listBoxParticleTemplate.Size = new System.Drawing.Size(192, 145);
            this.listBoxParticleTemplate.TabIndex = 6;
            this.listBoxParticleTemplate.SelectedIndexChanged += new System.EventHandler(this.listBoxParticleTemplate_SelectedIndexChanged);
            // 
            // propParticleTemplate
            // 
            this.propParticleTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propParticleTemplate.HelpVisible = false;
            this.propParticleTemplate.Location = new System.Drawing.Point(597, 46);
            this.propParticleTemplate.Name = "propParticleTemplate";
            this.propParticleTemplate.Size = new System.Drawing.Size(195, 145);
            this.propParticleTemplate.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnDelParticleTemplate, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAddParticleTemplate, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(399, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(192, 37);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnDelParticleTemplate
            // 
            this.btnDelParticleTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelParticleTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelParticleTemplate.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnDelParticleTemplate.Location = new System.Drawing.Point(99, 3);
            this.btnDelParticleTemplate.Name = "btnDelParticleTemplate";
            this.btnDelParticleTemplate.Size = new System.Drawing.Size(90, 31);
            this.btnDelParticleTemplate.TabIndex = 1;
            this.btnDelParticleTemplate.Text = "-";
            this.btnDelParticleTemplate.UseVisualStyleBackColor = true;
            this.btnDelParticleTemplate.Click += new System.EventHandler(this.btnDelParticleTemplate_Click);
            // 
            // btnAddParticleTemplate
            // 
            this.btnAddParticleTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddParticleTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddParticleTemplate.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnAddParticleTemplate.Location = new System.Drawing.Point(3, 3);
            this.btnAddParticleTemplate.Name = "btnAddParticleTemplate";
            this.btnAddParticleTemplate.Size = new System.Drawing.Size(90, 31);
            this.btnAddParticleTemplate.TabIndex = 0;
            this.btnAddParticleTemplate.Text = "+";
            this.btnAddParticleTemplate.UseVisualStyleBackColor = true;
            this.btnAddParticleTemplate.Click += new System.EventHandler(this.btnAddParticleTemplate_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnDelParticleSystem, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddParticleSystem, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(192, 37);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnDelParticleSystem
            // 
            this.btnDelParticleSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelParticleSystem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelParticleSystem.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnDelParticleSystem.Location = new System.Drawing.Point(99, 3);
            this.btnDelParticleSystem.Name = "btnDelParticleSystem";
            this.btnDelParticleSystem.Size = new System.Drawing.Size(90, 31);
            this.btnDelParticleSystem.TabIndex = 1;
            this.btnDelParticleSystem.Text = "-";
            this.btnDelParticleSystem.UseVisualStyleBackColor = true;
            this.btnDelParticleSystem.Click += new System.EventHandler(this.btnDelParticleSystem_Click);
            // 
            // btnAddParticleSystem
            // 
            this.btnAddParticleSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddParticleSystem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddParticleSystem.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnAddParticleSystem.Location = new System.Drawing.Point(3, 3);
            this.btnAddParticleSystem.Name = "btnAddParticleSystem";
            this.btnAddParticleSystem.Size = new System.Drawing.Size(90, 31);
            this.btnAddParticleSystem.TabIndex = 0;
            this.btnAddParticleSystem.Text = "+";
            this.btnAddParticleSystem.UseVisualStyleBackColor = true;
            this.btnAddParticleSystem.Click += new System.EventHandler(this.btnAddParticleSystem_Click);
            // 
            // listBoxParticleSystem
            // 
            this.listBoxParticleSystem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxParticleSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxParticleSystem.FormattingEnabled = true;
            this.listBoxParticleSystem.Location = new System.Drawing.Point(3, 46);
            this.listBoxParticleSystem.Name = "listBoxParticleSystem";
            this.listBoxParticleSystem.Size = new System.Drawing.Size(192, 145);
            this.listBoxParticleSystem.TabIndex = 2;
            this.listBoxParticleSystem.SelectedIndexChanged += new System.EventHandler(this.listBoxParticleSystem_SelectedIndexChanged);
            // 
            // propParticleSystem
            // 
            this.propParticleSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propParticleSystem.HelpVisible = false;
            this.propParticleSystem.Location = new System.Drawing.Point(201, 3);
            this.propParticleSystem.Name = "propParticleSystem";
            this.pnlParticleControl.SetRowSpan(this.propParticleSystem, 2);
            this.propParticleSystem.Size = new System.Drawing.Size(192, 188);
            this.propParticleSystem.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnModifParticleTemplateTexture);
            this.panel1.Controls.Add(this.cmbParticleTemplate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(597, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 37);
            this.panel1.TabIndex = 7;
            // 
            // btnModifParticleTemplateTexture
            // 
            this.btnModifParticleTemplateTexture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnModifParticleTemplateTexture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifParticleTemplateTexture.Font = new System.Drawing.Font("Wingdings", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnModifParticleTemplateTexture.Location = new System.Drawing.Point(155, 0);
            this.btnModifParticleTemplateTexture.Name = "btnModifParticleTemplateTexture";
            this.btnModifParticleTemplateTexture.Size = new System.Drawing.Size(40, 37);
            this.btnModifParticleTemplateTexture.TabIndex = 18;
            this.btnModifParticleTemplateTexture.Text = "ü";
            this.btnModifParticleTemplateTexture.UseVisualStyleBackColor = true;
            this.btnModifParticleTemplateTexture.Click += new System.EventHandler(this.btnModifParticleTemplateTexture_Click);
            // 
            // cmbParticleTemplate
            // 
            this.cmbParticleTemplate.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbParticleTemplate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbParticleTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParticleTemplate.FormattingEnabled = true;
            this.cmbParticleTemplate.ItemHeight = 30;
            this.cmbParticleTemplate.Location = new System.Drawing.Point(0, 0);
            this.cmbParticleTemplate.Name = "cmbParticleTemplate";
            this.cmbParticleTemplate.Size = new System.Drawing.Size(155, 36);
            this.cmbParticleTemplate.TabIndex = 6;
            this.cmbParticleTemplate.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbParticleTemplate_DrawItem);
            // 
            // ParticleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlParticleControl);
            this.Name = "ParticleControl";
            this.Size = new System.Drawing.Size(795, 194);
            this.pnlParticleControl.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlParticleControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAddParticleSystem;
        private System.Windows.Forms.PropertyGrid propParticleTemplate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnDelParticleTemplate;
        private System.Windows.Forms.Button btnAddParticleTemplate;
        private System.Windows.Forms.Button btnDelParticleSystem;
        private System.Windows.Forms.ListBox listBoxParticleSystem;
        private System.Windows.Forms.PropertyGrid propParticleSystem;
        private System.Windows.Forms.ListBox listBoxParticleTemplate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbParticleTemplate;
        private System.Windows.Forms.Button btnModifParticleTemplateTexture;
    }
}
