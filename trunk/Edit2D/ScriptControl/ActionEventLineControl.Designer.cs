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
            this.optActionEventLineEntity = new System.Windows.Forms.RadioButton();
            this.optActionEventLineRandom = new System.Windows.Forms.RadioButton();
            this.optSpeedActivate = new System.Windows.Forms.RadioButton();
            this.treeViewBoundEntity = new System.Windows.Forms.TreeView();
            this.pnlRandomValue = new System.Windows.Forms.Panel();
            this.numRndMax = new System.Windows.Forms.NumericUpDown();
            this.lblRandomValueMax = new System.Windows.Forms.Label();
            this.numRndMin = new System.Windows.Forms.NumericUpDown();
            this.lblRandomValueMin = new System.Windows.Forms.Label();
            this.optDurationActivate = new System.Windows.Forms.RadioButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblActionEventPropertyName = new System.Windows.Forms.Label();
            this.optDurationDeactivate = new System.Windows.Forms.RadioButton();
            this.optActionEventLineDeactivated = new System.Windows.Forms.RadioButton();
            this.pnlFixedValue = new System.Windows.Forms.Panel();
            this.optFixedValueFalse = new System.Windows.Forms.RadioButton();
            this.optFixedValueTrue = new System.Windows.Forms.RadioButton();
            this.numFixedValue = new System.Windows.Forms.NumericUpDown();
            this.pnlDuration = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTransitionUnit = new System.Windows.Forms.Label();
            this.numSpeed = new System.Windows.Forms.NumericUpDown();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.pnlBoundValue = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlRelative = new System.Windows.Forms.Panel();
            this.chkRelative = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.pnlTransition = new System.Windows.Forms.Panel();
            this.pnlRandomValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRndMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRndMin)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlFixedValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFixedValue)).BeginInit();
            this.pnlDuration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            this.pnlBoundValue.SuspendLayout();
            this.pnlRelative.SuspendLayout();
            this.pnlTransition.SuspendLayout();
            this.SuspendLayout();
            // 
            // optActionEventLineFixedValue
            // 
            this.optActionEventLineFixedValue.Appearance = System.Windows.Forms.Appearance.Button;
            this.optActionEventLineFixedValue.FlatAppearance.BorderSize = 0;
            this.optActionEventLineFixedValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optActionEventLineFixedValue.Location = new System.Drawing.Point(88, 50);
            this.optActionEventLineFixedValue.Margin = new System.Windows.Forms.Padding(0);
            this.optActionEventLineFixedValue.Name = "optActionEventLineFixedValue";
            this.optActionEventLineFixedValue.Size = new System.Drawing.Size(119, 24);
            this.optActionEventLineFixedValue.TabIndex = 4;
            this.optActionEventLineFixedValue.Tag = "BG2";
            this.optActionEventLineFixedValue.Text = "Valeur fixe";
            this.optActionEventLineFixedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optActionEventLineFixedValue.UseVisualStyleBackColor = true;
            this.optActionEventLineFixedValue.CheckedChanged += new System.EventHandler(this.optActionEventLineFixedValue_CheckedChanged);
            // 
            // optActionEventLineEntity
            // 
            this.optActionEventLineEntity.Appearance = System.Windows.Forms.Appearance.Button;
            this.optActionEventLineEntity.FlatAppearance.BorderSize = 0;
            this.optActionEventLineEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optActionEventLineEntity.Location = new System.Drawing.Point(207, 50);
            this.optActionEventLineEntity.Margin = new System.Windows.Forms.Padding(0);
            this.optActionEventLineEntity.Name = "optActionEventLineEntity";
            this.optActionEventLineEntity.Size = new System.Drawing.Size(240, 24);
            this.optActionEventLineEntity.TabIndex = 6;
            this.optActionEventLineEntity.Tag = "BG2";
            this.optActionEventLineEntity.Text = "Valeur liée";
            this.optActionEventLineEntity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optActionEventLineEntity.UseVisualStyleBackColor = true;
            this.optActionEventLineEntity.CheckedChanged += new System.EventHandler(this.optActionEventLineEntity_CheckedChanged);
            // 
            // optActionEventLineRandom
            // 
            this.optActionEventLineRandom.Appearance = System.Windows.Forms.Appearance.Button;
            this.optActionEventLineRandom.FlatAppearance.BorderSize = 0;
            this.optActionEventLineRandom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optActionEventLineRandom.Location = new System.Drawing.Point(447, 50);
            this.optActionEventLineRandom.Margin = new System.Windows.Forms.Padding(0);
            this.optActionEventLineRandom.Name = "optActionEventLineRandom";
            this.optActionEventLineRandom.Size = new System.Drawing.Size(119, 24);
            this.optActionEventLineRandom.TabIndex = 7;
            this.optActionEventLineRandom.Tag = "BG2";
            this.optActionEventLineRandom.Text = "Valeur aléatoire";
            this.optActionEventLineRandom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optActionEventLineRandom.UseVisualStyleBackColor = true;
            this.optActionEventLineRandom.CheckedChanged += new System.EventHandler(this.optActionEventLineRandom_CheckedChanged);
            // 
            // optSpeedActivate
            // 
            this.optSpeedActivate.Appearance = System.Windows.Forms.Appearance.Button;
            this.optSpeedActivate.FlatAppearance.BorderSize = 0;
            this.optSpeedActivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optSpeedActivate.Location = new System.Drawing.Point(85, 24);
            this.optSpeedActivate.Margin = new System.Windows.Forms.Padding(0);
            this.optSpeedActivate.Name = "optSpeedActivate";
            this.optSpeedActivate.Size = new System.Drawing.Size(84, 24);
            this.optSpeedActivate.TabIndex = 9;
            this.optSpeedActivate.Tag = "B";
            this.optSpeedActivate.Text = "Vitesse";
            this.optSpeedActivate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optSpeedActivate.UseVisualStyleBackColor = true;
            this.optSpeedActivate.CheckedChanged += new System.EventHandler(this.optSpeedActivate_CheckedChanged);
            // 
            // treeViewBoundEntity
            // 
            this.treeViewBoundEntity.BackColor = System.Drawing.Color.Silver;
            this.treeViewBoundEntity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewBoundEntity.CheckBoxes = true;
            this.treeViewBoundEntity.Enabled = false;
            this.treeViewBoundEntity.Location = new System.Drawing.Point(3, 24);
            this.treeViewBoundEntity.Name = "treeViewBoundEntity";
            this.treeViewBoundEntity.Size = new System.Drawing.Size(233, 111);
            this.treeViewBoundEntity.TabIndex = 23;
            this.treeViewBoundEntity.Tag = "BG2";
            this.treeViewBoundEntity.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewBoundEntity_AfterCheck);
            // 
            // pnlRandomValue
            // 
            this.pnlRandomValue.BackColor = System.Drawing.Color.Silver;
            this.pnlRandomValue.Controls.Add(this.numRndMax);
            this.pnlRandomValue.Controls.Add(this.lblRandomValueMax);
            this.pnlRandomValue.Controls.Add(this.numRndMin);
            this.pnlRandomValue.Controls.Add(this.lblRandomValueMin);
            this.pnlRandomValue.Location = new System.Drawing.Point(447, 74);
            this.pnlRandomValue.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRandomValue.Name = "pnlRandomValue";
            this.pnlRandomValue.Size = new System.Drawing.Size(119, 138);
            this.pnlRandomValue.TabIndex = 12;
            this.pnlRandomValue.Tag = "BG1";
            // 
            // numRndMax
            // 
            this.numRndMax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numRndMax.Enabled = false;
            this.numRndMax.Location = new System.Drawing.Point(6, 60);
            this.numRndMax.Name = "numRndMax";
            this.numRndMax.Size = new System.Drawing.Size(105, 16);
            this.numRndMax.TabIndex = 9;
            this.numRndMax.Tag = "BG2";
            this.numRndMax.ValueChanged += new System.EventHandler(this.numRndMax_ValueChanged);
            // 
            // lblRandomValueMax
            // 
            this.lblRandomValueMax.AutoSize = true;
            this.lblRandomValueMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRandomValueMax.Location = new System.Drawing.Point(3, 44);
            this.lblRandomValueMax.Name = "lblRandomValueMax";
            this.lblRandomValueMax.Size = new System.Drawing.Size(51, 13);
            this.lblRandomValueMax.TabIndex = 8;
            this.lblRandomValueMax.Text = "Maximum";
            // 
            // numRndMin
            // 
            this.numRndMin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numRndMin.Enabled = false;
            this.numRndMin.Location = new System.Drawing.Point(6, 19);
            this.numRndMin.Name = "numRndMin";
            this.numRndMin.Size = new System.Drawing.Size(105, 16);
            this.numRndMin.TabIndex = 7;
            this.numRndMin.Tag = "BG2";
            this.numRndMin.ValueChanged += new System.EventHandler(this.numRndMin_ValueChanged);
            // 
            // lblRandomValueMin
            // 
            this.lblRandomValueMin.AutoSize = true;
            this.lblRandomValueMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRandomValueMin.Location = new System.Drawing.Point(3, 3);
            this.lblRandomValueMin.Name = "lblRandomValueMin";
            this.lblRandomValueMin.Size = new System.Drawing.Size(48, 13);
            this.lblRandomValueMin.TabIndex = 6;
            this.lblRandomValueMin.Text = "Minimum";
            // 
            // optDurationActivate
            // 
            this.optDurationActivate.Appearance = System.Windows.Forms.Appearance.Button;
            this.optDurationActivate.FlatAppearance.BorderSize = 0;
            this.optDurationActivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optDurationActivate.Location = new System.Drawing.Point(0, 24);
            this.optDurationActivate.Margin = new System.Windows.Forms.Padding(0);
            this.optDurationActivate.Name = "optDurationActivate";
            this.optDurationActivate.Size = new System.Drawing.Size(84, 24);
            this.optDurationActivate.TabIndex = 6;
            this.optDurationActivate.Tag = "B";
            this.optDurationActivate.Text = "Durée";
            this.optDurationActivate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optDurationActivate.UseVisualStyleBackColor = true;
            this.optDurationActivate.CheckedChanged += new System.EventHandler(this.optDurationActivate_CheckedChanged);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblActionEventPropertyName);
            this.pnlHeader.Location = new System.Drawing.Point(0, 10);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(736, 40);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Tag = "BG2";
            // 
            // lblActionEventPropertyName
            // 
            this.lblActionEventPropertyName.AutoSize = true;
            this.lblActionEventPropertyName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblActionEventPropertyName.Location = new System.Drawing.Point(6, 6);
            this.lblActionEventPropertyName.Name = "lblActionEventPropertyName";
            this.lblActionEventPropertyName.Size = new System.Drawing.Size(53, 19);
            this.lblActionEventPropertyName.TabIndex = 5;
            this.lblActionEventPropertyName.Tag = "F2";
            this.lblActionEventPropertyName.Text = "NULL";
            // 
            // optDurationDeactivate
            // 
            this.optDurationDeactivate.Appearance = System.Windows.Forms.Appearance.Button;
            this.optDurationDeactivate.Checked = true;
            this.optDurationDeactivate.FlatAppearance.BorderSize = 0;
            this.optDurationDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optDurationDeactivate.Location = new System.Drawing.Point(0, 0);
            this.optDurationDeactivate.Margin = new System.Windows.Forms.Padding(0);
            this.optDurationDeactivate.Name = "optDurationDeactivate";
            this.optDurationDeactivate.Size = new System.Drawing.Size(168, 24);
            this.optDurationDeactivate.TabIndex = 5;
            this.optDurationDeactivate.TabStop = true;
            this.optDurationDeactivate.Tag = "B";
            this.optDurationDeactivate.Text = "Désactiver transition";
            this.optDurationDeactivate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optDurationDeactivate.UseVisualStyleBackColor = true;
            this.optDurationDeactivate.CheckedChanged += new System.EventHandler(this.optDurationDeactivate_CheckedChanged);
            // 
            // optActionEventLineDeactivated
            // 
            this.optActionEventLineDeactivated.Appearance = System.Windows.Forms.Appearance.Button;
            this.optActionEventLineDeactivated.Checked = true;
            this.optActionEventLineDeactivated.FlatAppearance.BorderSize = 0;
            this.optActionEventLineDeactivated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optActionEventLineDeactivated.Location = new System.Drawing.Point(6, 50);
            this.optActionEventLineDeactivated.Margin = new System.Windows.Forms.Padding(0);
            this.optActionEventLineDeactivated.Name = "optActionEventLineDeactivated";
            this.optActionEventLineDeactivated.Size = new System.Drawing.Size(82, 24);
            this.optActionEventLineDeactivated.TabIndex = 9;
            this.optActionEventLineDeactivated.TabStop = true;
            this.optActionEventLineDeactivated.Tag = "BG2";
            this.optActionEventLineDeactivated.Text = "Désactiver";
            this.optActionEventLineDeactivated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optActionEventLineDeactivated.UseVisualStyleBackColor = true;
            this.optActionEventLineDeactivated.CheckedChanged += new System.EventHandler(this.optActionEventLineDeactivated_CheckedChanged);
            // 
            // pnlFixedValue
            // 
            this.pnlFixedValue.BackColor = System.Drawing.Color.Silver;
            this.pnlFixedValue.Controls.Add(this.optFixedValueFalse);
            this.pnlFixedValue.Controls.Add(this.optFixedValueTrue);
            this.pnlFixedValue.Controls.Add(this.numFixedValue);
            this.pnlFixedValue.Location = new System.Drawing.Point(88, 74);
            this.pnlFixedValue.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFixedValue.Name = "pnlFixedValue";
            this.pnlFixedValue.Size = new System.Drawing.Size(119, 138);
            this.pnlFixedValue.TabIndex = 10;
            this.pnlFixedValue.Tag = "BG1";
            // 
            // optFixedValueFalse
            // 
            this.optFixedValueFalse.Appearance = System.Windows.Forms.Appearance.Button;
            this.optFixedValueFalse.Enabled = false;
            this.optFixedValueFalse.FlatAppearance.BorderSize = 0;
            this.optFixedValueFalse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optFixedValueFalse.Location = new System.Drawing.Point(33, 57);
            this.optFixedValueFalse.Margin = new System.Windows.Forms.Padding(0);
            this.optFixedValueFalse.Name = "optFixedValueFalse";
            this.optFixedValueFalse.Size = new System.Drawing.Size(52, 38);
            this.optFixedValueFalse.TabIndex = 2;
            this.optFixedValueFalse.Tag = "BG2";
            this.optFixedValueFalse.Text = "False";
            this.optFixedValueFalse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optFixedValueFalse.UseVisualStyleBackColor = true;
            this.optFixedValueFalse.CheckedChanged += new System.EventHandler(this.optFixedValueFalse_CheckedChanged);
            // 
            // optFixedValueTrue
            // 
            this.optFixedValueTrue.Appearance = System.Windows.Forms.Appearance.Button;
            this.optFixedValueTrue.Checked = true;
            this.optFixedValueTrue.Enabled = false;
            this.optFixedValueTrue.FlatAppearance.BorderSize = 0;
            this.optFixedValueTrue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optFixedValueTrue.Location = new System.Drawing.Point(33, 19);
            this.optFixedValueTrue.Margin = new System.Windows.Forms.Padding(0);
            this.optFixedValueTrue.Name = "optFixedValueTrue";
            this.optFixedValueTrue.Size = new System.Drawing.Size(52, 38);
            this.optFixedValueTrue.TabIndex = 1;
            this.optFixedValueTrue.TabStop = true;
            this.optFixedValueTrue.Tag = "BG2";
            this.optFixedValueTrue.Text = "True";
            this.optFixedValueTrue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optFixedValueTrue.UseVisualStyleBackColor = true;
            this.optFixedValueTrue.CheckedChanged += new System.EventHandler(this.optFixedValueTrue_CheckedChanged);
            // 
            // numFixedValue
            // 
            this.numFixedValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numFixedValue.Enabled = false;
            this.numFixedValue.Location = new System.Drawing.Point(21, 19);
            this.numFixedValue.Name = "numFixedValue";
            this.numFixedValue.Size = new System.Drawing.Size(80, 16);
            this.numFixedValue.TabIndex = 0;
            this.numFixedValue.Tag = "BG2";
            this.numFixedValue.ValueChanged += new System.EventHandler(this.numFixedValue_ValueChanged);
            // 
            // pnlDuration
            // 
            this.pnlDuration.Controls.Add(this.panel4);
            this.pnlDuration.Controls.Add(this.panel6);
            this.pnlDuration.Controls.Add(this.panel3);
            this.pnlDuration.Controls.Add(this.lblTransitionUnit);
            this.pnlDuration.Controls.Add(this.numSpeed);
            this.pnlDuration.Controls.Add(this.numDuration);
            this.pnlDuration.Enabled = false;
            this.pnlDuration.Location = new System.Drawing.Point(568, 98);
            this.pnlDuration.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDuration.Name = "pnlDuration";
            this.pnlDuration.Size = new System.Drawing.Size(168, 56);
            this.pnlDuration.TabIndex = 27;
            this.pnlDuration.Tag = "BG1";
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(6, 50);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(156, 6);
            this.panel4.TabIndex = 32;
            this.panel4.Tag = "B";
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(162, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(6, 56);
            this.panel6.TabIndex = 31;
            this.panel6.Tag = "B";
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(6, 56);
            this.panel3.TabIndex = 30;
            this.panel3.Tag = "B";
            // 
            // lblTransitionUnit
            // 
            this.lblTransitionUnit.AutoSize = true;
            this.lblTransitionUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransitionUnit.Location = new System.Drawing.Point(97, 20);
            this.lblTransitionUnit.Name = "lblTransitionUnit";
            this.lblTransitionUnit.Size = new System.Drawing.Size(63, 13);
            this.lblTransitionUnit.TabIndex = 11;
            this.lblTransitionUnit.Text = "unités / sec";
            // 
            // numSpeed
            // 
            this.numSpeed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numSpeed.Location = new System.Drawing.Point(24, 18);
            this.numSpeed.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numSpeed.Name = "numSpeed";
            this.numSpeed.Size = new System.Drawing.Size(67, 16);
            this.numSpeed.TabIndex = 10;
            this.numSpeed.Tag = "BG2";
            this.numSpeed.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // numDuration
            // 
            this.numDuration.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numDuration.Location = new System.Drawing.Point(24, 18);
            this.numDuration.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(67, 16);
            this.numDuration.TabIndex = 8;
            this.numDuration.Tag = "BG2";
            this.numDuration.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numDuration.ValueChanged += new System.EventHandler(this.numDuration_ValueChanged);
            // 
            // pnlBoundValue
            // 
            this.pnlBoundValue.Controls.Add(this.label1);
            this.pnlBoundValue.Controls.Add(this.treeViewBoundEntity);
            this.pnlBoundValue.Location = new System.Drawing.Point(207, 74);
            this.pnlBoundValue.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBoundValue.Name = "pnlBoundValue";
            this.pnlBoundValue.Size = new System.Drawing.Size(240, 138);
            this.pnlBoundValue.TabIndex = 28;
            this.pnlBoundValue.Tag = "BG1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Entités";
            // 
            // pnlRelative
            // 
            this.pnlRelative.Controls.Add(this.chkRelative);
            this.pnlRelative.Enabled = false;
            this.pnlRelative.Location = new System.Drawing.Point(568, 154);
            this.pnlRelative.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRelative.Name = "pnlRelative";
            this.pnlRelative.Size = new System.Drawing.Size(168, 58);
            this.pnlRelative.TabIndex = 29;
            this.pnlRelative.Tag = "BG1";
            // 
            // chkRelative
            // 
            this.chkRelative.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRelative.FlatAppearance.BorderSize = 0;
            this.chkRelative.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRelative.Location = new System.Drawing.Point(6, 18);
            this.chkRelative.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.chkRelative.Name = "chkRelative";
            this.chkRelative.Size = new System.Drawing.Size(156, 24);
            this.chkRelative.TabIndex = 6;
            this.chkRelative.Tag = "BG2";
            this.chkRelative.Text = "Valeur relative";
            this.chkRelative.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRelative.UseVisualStyleBackColor = true;
            this.chkRelative.CheckedChanged += new System.EventHandler(this.chkRelative_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(736, 10);
            this.panel2.TabIndex = 6;
            this.panel2.Tag = "BG2";
            // 
            // panel7
            // 
            this.panel7.Location = new System.Drawing.Point(0, 50);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(6, 163);
            this.panel7.TabIndex = 31;
            this.panel7.Tag = "BG2";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Gray;
            this.panel8.Location = new System.Drawing.Point(0, 212);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(736, 2);
            this.panel8.TabIndex = 32;
            this.panel8.Tag = "BG2";
            // 
            // pnlTransition
            // 
            this.pnlTransition.BackColor = System.Drawing.Color.LightGray;
            this.pnlTransition.Controls.Add(this.optDurationDeactivate);
            this.pnlTransition.Controls.Add(this.optDurationActivate);
            this.pnlTransition.Controls.Add(this.optSpeedActivate);
            this.pnlTransition.Enabled = false;
            this.pnlTransition.Location = new System.Drawing.Point(568, 50);
            this.pnlTransition.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTransition.Name = "pnlTransition";
            this.pnlTransition.Size = new System.Drawing.Size(168, 48);
            this.pnlTransition.TabIndex = 10;
            // 
            // ActionEventLineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTransition);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlRelative);
            this.Controls.Add(this.pnlRandomValue);
            this.Controls.Add(this.pnlBoundValue);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFixedValue);
            this.Controls.Add(this.pnlDuration);
            this.Controls.Add(this.optActionEventLineDeactivated);
            this.Controls.Add(this.optActionEventLineFixedValue);
            this.Controls.Add(this.optActionEventLineEntity);
            this.Controls.Add(this.optActionEventLineRandom);
            this.Name = "ActionEventLineControl";
            this.Size = new System.Drawing.Size(737, 214);
            this.Tag = "BG1";
            this.pnlRandomValue.ResumeLayout(false);
            this.pnlRandomValue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRndMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRndMin)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFixedValue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numFixedValue)).EndInit();
            this.pnlDuration.ResumeLayout(false);
            this.pnlDuration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            this.pnlBoundValue.ResumeLayout(false);
            this.pnlBoundValue.PerformLayout();
            this.pnlRelative.ResumeLayout(false);
            this.pnlTransition.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFixedValue;
        private System.Windows.Forms.Label lblRandomValueMin;
        private System.Windows.Forms.Label lblRandomValueMax;
        public System.Windows.Forms.RadioButton optActionEventLineFixedValue;
        public System.Windows.Forms.RadioButton optActionEventLineEntity;
        public System.Windows.Forms.RadioButton optActionEventLineRandom;
        public System.Windows.Forms.Label lblActionEventPropertyName;
        public System.Windows.Forms.RadioButton optActionEventLineDeactivated;
        public System.Windows.Forms.NumericUpDown numFixedValue;
        public System.Windows.Forms.NumericUpDown numRndMax;
        public System.Windows.Forms.NumericUpDown numRndMin;
        public System.Windows.Forms.TreeView treeViewBoundEntity;
        public System.Windows.Forms.NumericUpDown numDuration;
        public System.Windows.Forms.RadioButton optDurationActivate;
        public System.Windows.Forms.RadioButton optDurationDeactivate;
        public System.Windows.Forms.Panel pnlRandomValue;
        public System.Windows.Forms.RadioButton optFixedValueFalse;
        public System.Windows.Forms.RadioButton optFixedValueTrue;
        public System.Windows.Forms.Panel pnlDuration;
        public System.Windows.Forms.Label lblTransitionUnit;
        public System.Windows.Forms.NumericUpDown numSpeed;
        public System.Windows.Forms.RadioButton optSpeedActivate;
        private System.Windows.Forms.Panel pnlBoundValue;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlRelative;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkRelative;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlTransition;

    }
}
