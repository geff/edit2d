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
            this.cmbParticleTemplate = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAction = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblScript = new System.Windows.Forms.Label();
            this.listBoxParticleSystem = new System.Windows.Forms.ListBox();
            this.btnAddParticleSystem = new System.Windows.Forms.Button();
            this.btnDelParticleSystem = new System.Windows.Forms.Button();
            this.listBoxParticleTemplate = new System.Windows.Forms.ListBox();
            this.btnParticleTemplate = new System.Windows.Forms.Button();
            this.btnDelParticleTemplate = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.propParticleTemplate = new Edit2D.UC.PropertyGridLocal();
            this.propParticleSystem = new Edit2D.UC.PropertyGridLocal();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbParticleTemplate
            // 
            this.cmbParticleTemplate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbParticleTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParticleTemplate.FormattingEnabled = true;
            this.cmbParticleTemplate.ItemHeight = 30;
            this.cmbParticleTemplate.Location = new System.Drawing.Point(424, 56);
            this.cmbParticleTemplate.Name = "cmbParticleTemplate";
            this.cmbParticleTemplate.Size = new System.Drawing.Size(162, 36);
            this.cmbParticleTemplate.TabIndex = 6;
            this.cmbParticleTemplate.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbParticleTemplate_DrawItem);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BackColor = System.Drawing.Color.DarkGray;
            this.panel5.Location = new System.Drawing.Point(180, 50);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(6, 256);
            this.panel5.TabIndex = 7;
            this.panel5.Tag = "BG1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkGray;
            this.panel4.Controls.Add(this.lblAction);
            this.panel4.Location = new System.Drawing.Point(186, 10);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(232, 40);
            this.panel4.TabIndex = 6;
            this.panel4.Tag = "B";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.ForeColor = System.Drawing.Color.Black;
            this.lblAction.Location = new System.Drawing.Point(6, 6);
            this.lblAction.Margin = new System.Windows.Forms.Padding(6);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(88, 19);
            this.lblAction.TabIndex = 1;
            this.lblAction.Tag = "F2";
            this.lblAction.Text = "Propriétés";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Location = new System.Drawing.Point(186, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(232, 10);
            this.panel3.TabIndex = 5;
            this.panel3.Tag = "BG2";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.lblScript);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(186, 50);
            this.panel2.TabIndex = 4;
            this.panel2.Tag = "BG2";
            // 
            // lblScript
            // 
            this.lblScript.AutoSize = true;
            this.lblScript.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScript.ForeColor = System.Drawing.Color.Black;
            this.lblScript.Location = new System.Drawing.Point(6, 16);
            this.lblScript.Margin = new System.Windows.Forms.Padding(6);
            this.lblScript.Name = "lblScript";
            this.lblScript.Size = new System.Drawing.Size(177, 19);
            this.lblScript.TabIndex = 0;
            this.lblScript.Tag = "F2";
            this.lblScript.Text = "Système de particules";
            // 
            // listBoxParticleSystem
            // 
            this.listBoxParticleSystem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxParticleSystem.BackColor = System.Drawing.Color.Gray;
            this.listBoxParticleSystem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxParticleSystem.FormattingEnabled = true;
            this.listBoxParticleSystem.Location = new System.Drawing.Point(0, 41);
            this.listBoxParticleSystem.Name = "listBoxParticleSystem";
            this.listBoxParticleSystem.Size = new System.Drawing.Size(183, 208);
            this.listBoxParticleSystem.TabIndex = 11;
            this.listBoxParticleSystem.Tag = "BG2";
            this.listBoxParticleSystem.SelectedIndexChanged += new System.EventHandler(this.listBoxParticleSystem_SelectedIndexChanged);
            // 
            // btnAddParticleSystem
            // 
            this.btnAddParticleSystem.BackColor = System.Drawing.Color.Gray;
            this.btnAddParticleSystem.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAddParticleSystem.FlatAppearance.BorderSize = 0;
            this.btnAddParticleSystem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddParticleSystem.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnAddParticleSystem.Location = new System.Drawing.Point(47, 3);
            this.btnAddParticleSystem.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnAddParticleSystem.Name = "btnAddParticleSystem";
            this.btnAddParticleSystem.Size = new System.Drawing.Size(42, 35);
            this.btnAddParticleSystem.TabIndex = 9;
            this.btnAddParticleSystem.Tag = "BG2";
            this.btnAddParticleSystem.Text = "+";
            this.btnAddParticleSystem.UseVisualStyleBackColor = false;
            this.btnAddParticleSystem.Click += new System.EventHandler(this.btnAddParticleSystem_Click);
            this.btnAddParticleSystem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddParticleSystem_MouseDown);
            // 
            // btnDelParticleSystem
            // 
            this.btnDelParticleSystem.BackColor = System.Drawing.Color.Gray;
            this.btnDelParticleSystem.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnDelParticleSystem.FlatAppearance.BorderSize = 0;
            this.btnDelParticleSystem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelParticleSystem.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelParticleSystem.Location = new System.Drawing.Point(89, 3);
            this.btnDelParticleSystem.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnDelParticleSystem.Name = "btnDelParticleSystem";
            this.btnDelParticleSystem.Size = new System.Drawing.Size(42, 35);
            this.btnDelParticleSystem.TabIndex = 10;
            this.btnDelParticleSystem.Tag = "BG2";
            this.btnDelParticleSystem.Text = "-";
            this.btnDelParticleSystem.UseVisualStyleBackColor = false;
            this.btnDelParticleSystem.Click += new System.EventHandler(this.btnDelParticleSystem_Click);
            this.btnDelParticleSystem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDelParticleSystem_MouseDown);
            // 
            // listBoxParticleTemplate
            // 
            this.listBoxParticleTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxParticleTemplate.BackColor = System.Drawing.Color.Gray;
            this.listBoxParticleTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxParticleTemplate.FormattingEnabled = true;
            this.listBoxParticleTemplate.Location = new System.Drawing.Point(6, 41);
            this.listBoxParticleTemplate.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.listBoxParticleTemplate.Name = "listBoxParticleTemplate";
            this.listBoxParticleTemplate.Size = new System.Drawing.Size(162, 169);
            this.listBoxParticleTemplate.TabIndex = 18;
            this.listBoxParticleTemplate.Tag = "BG2";
            this.listBoxParticleTemplate.SelectedIndexChanged += new System.EventHandler(this.listBoxParticleTemplate_SelectedIndexChanged);
            // 
            // btnParticleTemplate
            // 
            this.btnParticleTemplate.BackColor = System.Drawing.Color.Gray;
            this.btnParticleTemplate.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnParticleTemplate.FlatAppearance.BorderSize = 0;
            this.btnParticleTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnParticleTemplate.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnParticleTemplate.Location = new System.Drawing.Point(42, 3);
            this.btnParticleTemplate.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnParticleTemplate.Name = "btnParticleTemplate";
            this.btnParticleTemplate.Size = new System.Drawing.Size(42, 35);
            this.btnParticleTemplate.TabIndex = 16;
            this.btnParticleTemplate.Tag = "BG2";
            this.btnParticleTemplate.Text = "+";
            this.btnParticleTemplate.UseVisualStyleBackColor = false;
            this.btnParticleTemplate.Click += new System.EventHandler(this.btnAddParticleTemplate_Click);
            this.btnParticleTemplate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnParticleTemplate_MouseDown);
            // 
            // btnDelParticleTemplate
            // 
            this.btnDelParticleTemplate.BackColor = System.Drawing.Color.Gray;
            this.btnDelParticleTemplate.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnDelParticleTemplate.FlatAppearance.BorderSize = 0;
            this.btnDelParticleTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelParticleTemplate.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelParticleTemplate.Location = new System.Drawing.Point(84, 3);
            this.btnDelParticleTemplate.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnDelParticleTemplate.Name = "btnDelParticleTemplate";
            this.btnDelParticleTemplate.Size = new System.Drawing.Size(42, 35);
            this.btnDelParticleTemplate.TabIndex = 17;
            this.btnDelParticleTemplate.Tag = "BG2";
            this.btnDelParticleTemplate.Text = "-";
            this.btnDelParticleTemplate.UseVisualStyleBackColor = false;
            this.btnDelParticleTemplate.Click += new System.EventHandler(this.btnDelParticleTemplate_Click);
            this.btnDelParticleTemplate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDelParticleTemplate_MouseDown);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel6.BackColor = System.Drawing.Color.DarkGray;
            this.panel6.Location = new System.Drawing.Point(168, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(6, 208);
            this.panel6.TabIndex = 15;
            this.panel6.Tag = "BG1";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.DarkGray;
            this.panel7.Controls.Add(this.label1);
            this.panel7.Location = new System.Drawing.Point(592, 10);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(232, 40);
            this.panel7.TabIndex = 14;
            this.panel7.Tag = "B";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 19);
            this.label1.TabIndex = 1;
            this.label1.Tag = "F2";
            this.label1.Text = "Propriétés";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Gray;
            this.panel8.Location = new System.Drawing.Point(592, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(232, 10);
            this.panel8.TabIndex = 13;
            this.panel8.Tag = "BG2";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Gray;
            this.panel9.Controls.Add(this.label2);
            this.panel9.Location = new System.Drawing.Point(418, 0);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(174, 50);
            this.panel9.TabIndex = 12;
            this.panel9.Tag = "BG2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 19);
            this.label2.TabIndex = 0;
            this.label2.Tag = "F2";
            this.label2.Text = "Particule";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.btnAddParticleSystem);
            this.panel1.Controls.Add(this.btnDelParticleSystem);
            this.panel1.Controls.Add(this.listBoxParticleSystem);
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 249);
            this.panel1.TabIndex = 19;
            this.panel1.Tag = "B";
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel10.Controls.Add(this.panel6);
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Controls.Add(this.btnParticleTemplate);
            this.panel10.Controls.Add(this.btnDelParticleTemplate);
            this.panel10.Controls.Add(this.listBoxParticleTemplate);
            this.panel10.Location = new System.Drawing.Point(418, 98);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(174, 207);
            this.panel10.TabIndex = 20;
            this.panel10.Tag = "B";
            // 
            // panel11
            // 
            this.panel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel11.BackColor = System.Drawing.Color.DarkGray;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Margin = new System.Windows.Forms.Padding(0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(6, 208);
            this.panel11.TabIndex = 19;
            this.panel11.Tag = "BG1";
            // 
            // propParticleTemplate
            // 
            this.propParticleTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.propParticleTemplate.Location = new System.Drawing.Point(592, 50);
            this.propParticleTemplate.Margin = new System.Windows.Forms.Padding(0);
            this.propParticleTemplate.Name = "propParticleTemplate";
            this.propParticleTemplate.Size = new System.Drawing.Size(232, 255);
            this.propParticleTemplate.TabIndex = 2;
            this.propParticleTemplate.Tag = "B;F1";
            this.propParticleTemplate.TagLineColor = "BG2";
            // 
            // propParticleSystem
            // 
            this.propParticleSystem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.propParticleSystem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.propParticleSystem.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.propParticleSystem.Location = new System.Drawing.Point(186, 50);
            this.propParticleSystem.Margin = new System.Windows.Forms.Padding(0);
            this.propParticleSystem.Name = "propParticleSystem";
            this.propParticleSystem.Size = new System.Drawing.Size(232, 255);
            this.propParticleSystem.TabIndex = 1;
            this.propParticleSystem.Tag = "B;F1";
            this.propParticleSystem.TagLineColor = "BG2";
            // 
            // ParticleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbParticleTemplate);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.propParticleTemplate);
            this.Controls.Add(this.propParticleSystem);
            this.Name = "ParticleControl";
            this.Size = new System.Drawing.Size(891, 305);
            this.Tag = "BG1";
            this.Load += new System.EventHandler(this.ParticleControl_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbParticleTemplate;
        private Edit2D.UC.PropertyGridLocal propParticleSystem;
        private Edit2D.UC.PropertyGridLocal propParticleTemplate;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblScript;
        private System.Windows.Forms.ListBox listBoxParticleSystem;
        private System.Windows.Forms.Button btnAddParticleSystem;
        private System.Windows.Forms.Button btnDelParticleSystem;
        private System.Windows.Forms.ListBox listBoxParticleTemplate;
        private System.Windows.Forms.Button btnParticleTemplate;
        private System.Windows.Forms.Button btnDelParticleTemplate;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
    }
}
