namespace Edit2D.ScriptControl
{
    partial class ActionEventLineControl
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
            this.optActionEventLineFixedValue = new System.Windows.Forms.RadioButton();
            this.optActionEventLineMouse = new System.Windows.Forms.RadioButton();
            this.optActionEventLineEntity = new System.Windows.Forms.RadioButton();
            this.optActionEventLineRandom = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDuration = new System.Windows.Forms.Panel();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.optDurationActivate = new System.Windows.Forms.RadioButton();
            this.optDurationDeactivate = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.treeViewProperties = new System.Windows.Forms.TreeView();
            this.treeviewEntiteTargetCollision = new System.Windows.Forms.TreeView();
            this.pnlRandom = new System.Windows.Forms.Panel();
            this.numRndMax = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numRndMin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblActionEventPropertyName = new System.Windows.Forms.Label();
            this.optActionEventLineDeactivated = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.optFixedValueFalse = new System.Windows.Forms.RadioButton();
            this.optFixedValueTrue = new System.Windows.Forms.RadioButton();
            this.numFixedValue = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chkRelative = new System.Windows.Forms.CheckBox();
            this.optActionEventLineMouseY = new System.Windows.Forms.RadioButton();
            this.optActionEventLineMouseX = new System.Windows.Forms.RadioButton();
            this.numSpeed = new System.Windows.Forms.NumericUpDown();
            this.optSpeedActivate = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlDuration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            this.pnlRandom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRndMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRndMin)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFixedValue)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // optActionEventLineFixedValue
            // 
            this.optActionEventLineFixedValue.AutoSize = true;
            this.optActionEventLineFixedValue.Location = new System.Drawing.Point(100, 33);
            this.optActionEventLineFixedValue.Name = "optActionEventLineFixedValue";
            this.optActionEventLineFixedValue.Size = new System.Drawing.Size(74, 15);
            this.optActionEventLineFixedValue.TabIndex = 4;
            this.optActionEventLineFixedValue.Text = "Valeur fixe";
            this.optActionEventLineFixedValue.UseVisualStyleBackColor = true;
            this.optActionEventLineFixedValue.CheckedChanged += new System.EventHandler(this.optActionEventLineFixedValue_CheckedChanged);
            // 
            // optActionEventLineMouse
            // 
            this.optActionEventLineMouse.AutoSize = true;
            this.optActionEventLineMouse.Location = new System.Drawing.Point(3, 54);
            this.optActionEventLineMouse.Name = "optActionEventLineMouse";
            this.optActionEventLineMouse.Size = new System.Drawing.Size(54, 16);
            this.optActionEventLineMouse.TabIndex = 5;
            this.optActionEventLineMouse.Text = "Souris";
            this.optActionEventLineMouse.UseVisualStyleBackColor = true;
            this.optActionEventLineMouse.CheckedChanged += new System.EventHandler(this.optActionEventLineMouse_CheckedChanged);
            // 
            // optActionEventLineEntity
            // 
            this.optActionEventLineEntity.AutoSize = true;
            this.optActionEventLineEntity.Location = new System.Drawing.Point(197, 33);
            this.optActionEventLineEntity.Name = "optActionEventLineEntity";
            this.optActionEventLineEntity.Size = new System.Drawing.Size(52, 15);
            this.optActionEventLineEntity.TabIndex = 6;
            this.optActionEventLineEntity.Text = "Entite";
            this.optActionEventLineEntity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.optActionEventLineEntity.UseVisualStyleBackColor = true;
            this.optActionEventLineEntity.CheckedChanged += new System.EventHandler(this.optActionEventLineEntity_CheckedChanged);
            // 
            // optActionEventLineRandom
            // 
            this.optActionEventLineRandom.AutoSize = true;
            this.optActionEventLineRandom.Location = new System.Drawing.Point(520, 33);
            this.optActionEventLineRandom.Name = "optActionEventLineRandom";
            this.optActionEventLineRandom.Size = new System.Drawing.Size(66, 15);
            this.optActionEventLineRandom.TabIndex = 7;
            this.optActionEventLineRandom.Text = "Aléatoire";
            this.optActionEventLineRandom.UseVisualStyleBackColor = true;
            this.optActionEventLineRandom.CheckedChanged += new System.EventHandler(this.optActionEventLineRandom_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pnlDuration, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeViewProperties, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.treeviewEntiteTargetCollision, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnlRandom, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.optActionEventLineDeactivated, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.optActionEventLineFixedValue, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.optActionEventLineMouse, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.optActionEventLineEntity, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.optActionEventLineRandom, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(647, 200);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // pnlDuration
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pnlDuration, 3);
            this.pnlDuration.Controls.Add(this.label5);
            this.pnlDuration.Controls.Add(this.numSpeed);
            this.pnlDuration.Controls.Add(this.optSpeedActivate);
            this.pnlDuration.Controls.Add(this.numDuration);
            this.pnlDuration.Controls.Add(this.optDurationActivate);
            this.pnlDuration.Controls.Add(this.optDurationDeactivate);
            this.pnlDuration.Controls.Add(this.label4);
            this.pnlDuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDuration.Location = new System.Drawing.Point(194, 0);
            this.pnlDuration.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDuration.Name = "pnlDuration";
            this.pnlDuration.Size = new System.Drawing.Size(453, 30);
            this.pnlDuration.TabIndex = 27;
            // 
            // numDuration
            // 
            this.numDuration.Enabled = false;
            this.numDuration.Location = new System.Drawing.Point(197, 3);
            this.numDuration.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(55, 20);
            this.numDuration.TabIndex = 8;
            this.numDuration.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numDuration.ValueChanged += new System.EventHandler(this.numDuration_ValueChanged);
            // 
            // optDurationActivate
            // 
            this.optDurationActivate.AutoSize = true;
            this.optDurationActivate.Location = new System.Drawing.Point(143, 5);
            this.optDurationActivate.Name = "optDurationActivate";
            this.optDurationActivate.Size = new System.Drawing.Size(54, 17);
            this.optDurationActivate.TabIndex = 6;
            this.optDurationActivate.Text = "Durée";
            this.optDurationActivate.UseVisualStyleBackColor = true;
            this.optDurationActivate.CheckedChanged += new System.EventHandler(this.optDurationActivate_CheckedChanged);
            // 
            // optDurationDeactivate
            // 
            this.optDurationDeactivate.AutoSize = true;
            this.optDurationDeactivate.Checked = true;
            this.optDurationDeactivate.Location = new System.Drawing.Point(61, 4);
            this.optDurationDeactivate.Name = "optDurationDeactivate";
            this.optDurationDeactivate.Size = new System.Drawing.Size(76, 17);
            this.optDurationDeactivate.TabIndex = 5;
            this.optDurationDeactivate.TabStop = true;
            this.optDurationDeactivate.Text = "Désactiver";
            this.optDurationDeactivate.UseVisualStyleBackColor = true;
            this.optDurationDeactivate.CheckedChanged += new System.EventHandler(this.optDurationDeactivate_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 17);
            this.label4.TabIndex = 4;
            // 
            // treeViewProperties
            // 
            this.treeViewProperties.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewProperties.CheckBoxes = true;
            this.treeViewProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewProperties.Enabled = false;
            this.treeViewProperties.Location = new System.Drawing.Point(391, 54);
            this.treeViewProperties.Name = "treeViewProperties";
            this.tableLayoutPanel1.SetRowSpan(this.treeViewProperties, 2);
            this.treeViewProperties.Size = new System.Drawing.Size(123, 135);
            this.treeViewProperties.TabIndex = 24;
            this.treeViewProperties.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewProperties_AfterCheck);
            // 
            // treeviewEntiteTargetCollision
            // 
            this.treeviewEntiteTargetCollision.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeviewEntiteTargetCollision.CheckBoxes = true;
            this.treeviewEntiteTargetCollision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeviewEntiteTargetCollision.Enabled = false;
            this.treeviewEntiteTargetCollision.Location = new System.Drawing.Point(197, 54);
            this.treeviewEntiteTargetCollision.Name = "treeviewEntiteTargetCollision";
            this.tableLayoutPanel1.SetRowSpan(this.treeviewEntiteTargetCollision, 2);
            this.treeviewEntiteTargetCollision.Size = new System.Drawing.Size(188, 135);
            this.treeviewEntiteTargetCollision.TabIndex = 23;
            this.treeviewEntiteTargetCollision.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeviewEntiteTargetCollision_AfterCheck);
            // 
            // pnlRandom
            // 
            this.pnlRandom.Controls.Add(this.numRndMax);
            this.pnlRandom.Controls.Add(this.label3);
            this.pnlRandom.Controls.Add(this.numRndMin);
            this.pnlRandom.Controls.Add(this.label2);
            this.pnlRandom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRandom.Location = new System.Drawing.Point(520, 54);
            this.pnlRandom.Name = "pnlRandom";
            this.tableLayoutPanel1.SetRowSpan(this.pnlRandom, 2);
            this.pnlRandom.Size = new System.Drawing.Size(124, 135);
            this.pnlRandom.TabIndex = 12;
            // 
            // numRndMax
            // 
            this.numRndMax.Enabled = false;
            this.numRndMax.Location = new System.Drawing.Point(3, 60);
            this.numRndMax.Name = "numRndMax";
            this.numRndMax.Size = new System.Drawing.Size(120, 20);
            this.numRndMax.TabIndex = 9;
            this.numRndMax.ValueChanged += new System.EventHandler(this.numRndMax_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Maximum";
            // 
            // numRndMin
            // 
            this.numRndMin.Enabled = false;
            this.numRndMin.Location = new System.Drawing.Point(3, 19);
            this.numRndMin.Name = "numRndMin";
            this.numRndMin.Size = new System.Drawing.Size(120, 20);
            this.numRndMin.TabIndex = 7;
            this.numRndMin.ValueChanged += new System.EventHandler(this.numRndMin_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Minimum";
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblActionEventPropertyName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 30);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Propriété :";
            // 
            // lblActionEventPropertyName
            // 
            this.lblActionEventPropertyName.AutoSize = true;
            this.lblActionEventPropertyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionEventPropertyName.Location = new System.Drawing.Point(72, 8);
            this.lblActionEventPropertyName.Name = "lblActionEventPropertyName";
            this.lblActionEventPropertyName.Size = new System.Drawing.Size(35, 13);
            this.lblActionEventPropertyName.TabIndex = 5;
            this.lblActionEventPropertyName.Text = "NULL";
            // 
            // optActionEventLineDeactivated
            // 
            this.optActionEventLineDeactivated.AutoSize = true;
            this.optActionEventLineDeactivated.Checked = true;
            this.optActionEventLineDeactivated.Location = new System.Drawing.Point(3, 33);
            this.optActionEventLineDeactivated.Name = "optActionEventLineDeactivated";
            this.optActionEventLineDeactivated.Size = new System.Drawing.Size(76, 15);
            this.optActionEventLineDeactivated.TabIndex = 9;
            this.optActionEventLineDeactivated.TabStop = true;
            this.optActionEventLineDeactivated.Text = "Désactiver";
            this.optActionEventLineDeactivated.UseVisualStyleBackColor = true;
            this.optActionEventLineDeactivated.CheckedChanged += new System.EventHandler(this.optActionEventLineDeactivated_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.optFixedValueFalse);
            this.panel2.Controls.Add(this.optFixedValueTrue);
            this.panel2.Controls.Add(this.numFixedValue);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(100, 54);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 2);
            this.panel2.Size = new System.Drawing.Size(91, 135);
            this.panel2.TabIndex = 10;
            // 
            // optFixedValueFalse
            // 
            this.optFixedValueFalse.AutoSize = true;
            this.optFixedValueFalse.Enabled = false;
            this.optFixedValueFalse.Location = new System.Drawing.Point(3, 52);
            this.optFixedValueFalse.Name = "optFixedValueFalse";
            this.optFixedValueFalse.Size = new System.Drawing.Size(50, 17);
            this.optFixedValueFalse.TabIndex = 2;
            this.optFixedValueFalse.Text = "False";
            this.optFixedValueFalse.UseVisualStyleBackColor = true;
            this.optFixedValueFalse.CheckedChanged += new System.EventHandler(this.optFixedValueFalse_CheckedChanged);
            // 
            // optFixedValueTrue
            // 
            this.optFixedValueTrue.AutoSize = true;
            this.optFixedValueTrue.Checked = true;
            this.optFixedValueTrue.Enabled = false;
            this.optFixedValueTrue.Location = new System.Drawing.Point(3, 29);
            this.optFixedValueTrue.Name = "optFixedValueTrue";
            this.optFixedValueTrue.Size = new System.Drawing.Size(47, 17);
            this.optFixedValueTrue.TabIndex = 1;
            this.optFixedValueTrue.TabStop = true;
            this.optFixedValueTrue.Text = "True";
            this.optFixedValueTrue.UseVisualStyleBackColor = true;
            this.optFixedValueTrue.CheckedChanged += new System.EventHandler(this.optFixedValueTrue_CheckedChanged);
            // 
            // numFixedValue
            // 
            this.numFixedValue.Enabled = false;
            this.numFixedValue.Location = new System.Drawing.Point(3, 3);
            this.numFixedValue.Name = "numFixedValue";
            this.numFixedValue.Size = new System.Drawing.Size(52, 20);
            this.numFixedValue.TabIndex = 0;
            this.numFixedValue.ValueChanged += new System.EventHandler(this.numFixedValue_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 195);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(647, 5);
            this.panel3.TabIndex = 25;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.chkRelative);
            this.panel5.Controls.Add(this.optActionEventLineMouseY);
            this.panel5.Controls.Add(this.optActionEventLineMouseX);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 76);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(91, 113);
            this.panel5.TabIndex = 26;
            // 
            // chkRelative
            // 
            this.chkRelative.AutoSize = true;
            this.chkRelative.Location = new System.Drawing.Point(22, 76);
            this.chkRelative.Name = "chkRelative";
            this.chkRelative.Size = new System.Drawing.Size(56, 17);
            this.chkRelative.TabIndex = 8;
            this.chkRelative.Text = "Relatif";
            this.chkRelative.UseVisualStyleBackColor = true;
            this.chkRelative.CheckedChanged += new System.EventHandler(this.chkRelative_CheckedChanged);
            // 
            // optActionEventLineMouseY
            // 
            this.optActionEventLineMouseY.AutoSize = true;
            this.optActionEventLineMouseY.Enabled = false;
            this.optActionEventLineMouseY.Location = new System.Drawing.Point(22, 30);
            this.optActionEventLineMouseY.Name = "optActionEventLineMouseY";
            this.optActionEventLineMouseY.Size = new System.Drawing.Size(64, 17);
            this.optActionEventLineMouseY.TabIndex = 7;
            this.optActionEventLineMouseY.Text = "Souris.Y";
            this.optActionEventLineMouseY.UseVisualStyleBackColor = true;
            this.optActionEventLineMouseY.CheckedChanged += new System.EventHandler(this.optActionEventLineMouseY_CheckedChanged);
            // 
            // optActionEventLineMouseX
            // 
            this.optActionEventLineMouseX.AutoSize = true;
            this.optActionEventLineMouseX.Enabled = false;
            this.optActionEventLineMouseX.Location = new System.Drawing.Point(22, 10);
            this.optActionEventLineMouseX.Name = "optActionEventLineMouseX";
            this.optActionEventLineMouseX.Size = new System.Drawing.Size(64, 17);
            this.optActionEventLineMouseX.TabIndex = 6;
            this.optActionEventLineMouseX.Text = "Souris.X";
            this.optActionEventLineMouseX.UseVisualStyleBackColor = true;
            this.optActionEventLineMouseX.CheckedChanged += new System.EventHandler(this.optActionEventLineMouseX_CheckedChanged);
            // 
            // numSpeed
            // 
            this.numSpeed.Enabled = false;
            this.numSpeed.Location = new System.Drawing.Point(326, 3);
            this.numSpeed.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numSpeed.Name = "numSpeed";
            this.numSpeed.Size = new System.Drawing.Size(51, 20);
            this.numSpeed.TabIndex = 10;
            this.numSpeed.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // optSpeedActivate
            // 
            this.optSpeedActivate.AutoSize = true;
            this.optSpeedActivate.Location = new System.Drawing.Point(261, 3);
            this.optSpeedActivate.Name = "optSpeedActivate";
            this.optSpeedActivate.Size = new System.Drawing.Size(59, 17);
            this.optSpeedActivate.TabIndex = 9;
            this.optSpeedActivate.Text = "Vitesse";
            this.optSpeedActivate.UseVisualStyleBackColor = true;
            this.optSpeedActivate.CheckedChanged += new System.EventHandler(this.optSpeedActivate_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(383, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "unités / sec";
            // 
            // ActionEventLineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ActionEventLineControl";
            this.Size = new System.Drawing.Size(647, 200);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlDuration.ResumeLayout(false);
            this.pnlDuration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            this.pnlRandom.ResumeLayout(false);
            this.pnlRandom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRndMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRndMin)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFixedValue)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.RadioButton optActionEventLineFixedValue;
        public System.Windows.Forms.RadioButton optActionEventLineMouse;
        public System.Windows.Forms.RadioButton optActionEventLineEntity;
        public System.Windows.Forms.RadioButton optActionEventLineRandom;
        public System.Windows.Forms.Label lblActionEventPropertyName;
        public System.Windows.Forms.RadioButton optActionEventLineDeactivated;
        public System.Windows.Forms.NumericUpDown numFixedValue;
        public System.Windows.Forms.NumericUpDown numRndMax;
        public System.Windows.Forms.NumericUpDown numRndMin;
        public System.Windows.Forms.TreeView treeViewProperties;
        public System.Windows.Forms.TreeView treeviewEntiteTargetCollision;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.RadioButton optActionEventLineMouseY;
        public System.Windows.Forms.RadioButton optActionEventLineMouseX;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numDuration;
        public System.Windows.Forms.RadioButton optDurationActivate;
        public System.Windows.Forms.RadioButton optDurationDeactivate;
        public System.Windows.Forms.Panel pnlRandom;
        public System.Windows.Forms.RadioButton optFixedValueFalse;
        public System.Windows.Forms.RadioButton optFixedValueTrue;
        public System.Windows.Forms.Panel pnlDuration;
        private System.Windows.Forms.CheckBox chkRelative;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown numSpeed;
        public System.Windows.Forms.RadioButton optSpeedActivate;

    }
}
