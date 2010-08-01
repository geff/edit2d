namespace Edit2D.TriggerControl
{
    partial class TriggerControl
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
            this.listboxTrigger = new System.Windows.Forms.ListBox();
            this.pnlValueProp = new System.Windows.Forms.Panel();
            this.txtProp3 = new System.Windows.Forms.RichTextBox();
            this.txtProp2 = new System.Windows.Forms.RichTextBox();
            this.txtProp1 = new System.Windows.Forms.RichTextBox();
            this.cmbProp3 = new System.Windows.Forms.ComboBox();
            this.lblProp3 = new System.Windows.Forms.Label();
            this.cmbProp2 = new System.Windows.Forms.ComboBox();
            this.lblProp2 = new System.Windows.Forms.Label();
            this.cmbProp1 = new System.Windows.Forms.ComboBox();
            this.lblProp1 = new System.Windows.Forms.Label();
            this.numTimeLoop = new System.Windows.Forms.NumericUpDown();
            this.optTimeLoopParam = new System.Windows.Forms.RadioButton();
            this.optTimeLoopAlways = new System.Windows.Forms.RadioButton();
            this.pnlTrigger = new System.Windows.Forms.Panel();
            this.lblScriptName = new System.Windows.Forms.Label();
            this.txtTriggerName = new System.Windows.Forms.RichTextBox();
            this.panel17 = new System.Windows.Forms.Panel();
            this.lblScript = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.btnChangeTriggerName = new System.Windows.Forms.Button();
            this.btnAddTrigger = new System.Windows.Forms.Button();
            this.btnDelTrigger = new System.Windows.Forms.Button();
            this.pnlTypeTrigger = new System.Windows.Forms.Panel();
            this.optTypeTriggerTime = new System.Windows.Forms.RadioButton();
            this.optTypeTriggerLoading = new System.Windows.Forms.RadioButton();
            this.optTypeTriggerMouse = new System.Windows.Forms.RadioButton();
            this.optTypeTriggerValueOverflow = new System.Windows.Forms.RadioButton();
            this.optTypeTriggerNoCollision = new System.Windows.Forms.RadioButton();
            this.optTypeTriggerCollision = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlMouse = new System.Windows.Forms.Panel();
            this.optMouseStayOver = new System.Windows.Forms.RadioButton();
            this.optMouseLeave = new System.Windows.Forms.RadioButton();
            this.optMouseEnter = new System.Windows.Forms.RadioButton();
            this.optMouseLeftClick = new System.Windows.Forms.RadioButton();
            this.optMouseRightClick = new System.Windows.Forms.RadioButton();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlEntityCollision = new System.Windows.Forms.Panel();
            this.treeViewCollision = new Edit2D.UC.TreeViewLocal();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlValueOverflow = new System.Windows.Forms.Panel();
            this.treeViewValueChanged = new Edit2D.UC.TreeViewLocal();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlTime = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel21 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlScript = new System.Windows.Forms.Panel();
            this.treeViewScript = new Edit2D.UC.TreeViewLocal();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlValueProp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeLoop)).BeginInit();
            this.pnlTrigger.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel16.SuspendLayout();
            this.pnlTypeTrigger.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlMouse.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlEntityCollision.SuspendLayout();
            this.panel9.SuspendLayout();
            this.pnlValueOverflow.SuspendLayout();
            this.panel11.SuspendLayout();
            this.pnlTime.SuspendLayout();
            this.panel13.SuspendLayout();
            this.pnlScript.SuspendLayout();
            this.panel15.SuspendLayout();
            this.SuspendLayout();
            // 
            // listboxTrigger
            // 
            this.listboxTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listboxTrigger.BackColor = System.Drawing.Color.Gray;
            this.listboxTrigger.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listboxTrigger.FormattingEnabled = true;
            this.listboxTrigger.Location = new System.Drawing.Point(6, 42);
            this.listboxTrigger.Name = "listboxTrigger";
            this.listboxTrigger.Size = new System.Drawing.Size(162, 143);
            this.listboxTrigger.TabIndex = 11;
            this.listboxTrigger.Tag = "BG2";
            this.listboxTrigger.SelectedIndexChanged += new System.EventHandler(this.listboxTrigger_SelectedIndexChanged);
            // 
            // pnlValueProp
            // 
            this.pnlValueProp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlValueProp.BackColor = System.Drawing.Color.DarkGray;
            this.pnlValueProp.Controls.Add(this.txtProp3);
            this.pnlValueProp.Controls.Add(this.txtProp2);
            this.pnlValueProp.Controls.Add(this.txtProp1);
            this.pnlValueProp.Controls.Add(this.cmbProp3);
            this.pnlValueProp.Controls.Add(this.lblProp3);
            this.pnlValueProp.Controls.Add(this.cmbProp2);
            this.pnlValueProp.Controls.Add(this.lblProp2);
            this.pnlValueProp.Controls.Add(this.cmbProp1);
            this.pnlValueProp.Controls.Add(this.lblProp1);
            this.pnlValueProp.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlValueProp.Location = new System.Drawing.Point(0, 222);
            this.pnlValueProp.Name = "pnlValueProp";
            this.pnlValueProp.Size = new System.Drawing.Size(208, 83);
            this.pnlValueProp.TabIndex = 21;
            this.pnlValueProp.Tag = "B";
            // 
            // txtProp3
            // 
            this.txtProp3.BackColor = System.Drawing.Color.Gray;
            this.txtProp3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProp3.Location = new System.Drawing.Point(112, 57);
            this.txtProp3.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtProp3.Multiline = false;
            this.txtProp3.Name = "txtProp3";
            this.txtProp3.Size = new System.Drawing.Size(90, 21);
            this.txtProp3.TabIndex = 22;
            this.txtProp3.Tag = "BG2";
            this.txtProp3.Text = "";
            this.txtProp3.ZoomFactor = 1.2F;
            this.txtProp3.TextChanged += new System.EventHandler(this.txtProp3_TextChanged);
            // 
            // txtProp2
            // 
            this.txtProp2.BackColor = System.Drawing.Color.Gray;
            this.txtProp2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProp2.Location = new System.Drawing.Point(112, 28);
            this.txtProp2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtProp2.Multiline = false;
            this.txtProp2.Name = "txtProp2";
            this.txtProp2.Size = new System.Drawing.Size(90, 21);
            this.txtProp2.TabIndex = 21;
            this.txtProp2.Tag = "BG2";
            this.txtProp2.Text = "";
            this.txtProp2.ZoomFactor = 1.2F;
            this.txtProp2.TextChanged += new System.EventHandler(this.txtProp2_TextChanged);
            // 
            // txtProp1
            // 
            this.txtProp1.BackColor = System.Drawing.Color.Gray;
            this.txtProp1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProp1.Location = new System.Drawing.Point(112, 0);
            this.txtProp1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtProp1.Multiline = false;
            this.txtProp1.Name = "txtProp1";
            this.txtProp1.Size = new System.Drawing.Size(90, 21);
            this.txtProp1.TabIndex = 20;
            this.txtProp1.Tag = "BG2";
            this.txtProp1.Text = "";
            this.txtProp1.ZoomFactor = 1.2F;
            this.txtProp1.TextChanged += new System.EventHandler(this.txtProp1_TextChanged);
            // 
            // cmbProp3
            // 
            this.cmbProp3.AccessibleDescription = "";
            this.cmbProp3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProp3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProp3.FormattingEnabled = true;
            this.cmbProp3.Items.AddRange(new object[] {
            "No",
            ">",
            "<",
            ">=",
            "<=",
            "=",
            "!="});
            this.cmbProp3.Location = new System.Drawing.Point(66, 57);
            this.cmbProp3.Name = "cmbProp3";
            this.cmbProp3.Size = new System.Drawing.Size(40, 21);
            this.cmbProp3.TabIndex = 7;
            this.cmbProp3.Tag = "BG2";
            this.cmbProp3.SelectedIndexChanged += new System.EventHandler(this.cmbProp3_SelectedIndexChanged);
            // 
            // lblProp3
            // 
            this.lblProp3.Location = new System.Drawing.Point(4, 57);
            this.lblProp3.Name = "lblProp3";
            this.lblProp3.Size = new System.Drawing.Size(57, 20);
            this.lblProp3.TabIndex = 6;
            this.lblProp3.Text = "R";
            this.lblProp3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProp2
            // 
            this.cmbProp2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProp2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProp2.FormattingEnabled = true;
            this.cmbProp2.Items.AddRange(new object[] {
            "No",
            ">",
            "<",
            ">=",
            "<=",
            "=",
            "!="});
            this.cmbProp2.Location = new System.Drawing.Point(66, 28);
            this.cmbProp2.Name = "cmbProp2";
            this.cmbProp2.Size = new System.Drawing.Size(40, 21);
            this.cmbProp2.TabIndex = 4;
            this.cmbProp2.Tag = "BG2";
            this.cmbProp2.SelectedIndexChanged += new System.EventHandler(this.cmbProp2_SelectedIndexChanged);
            // 
            // lblProp2
            // 
            this.lblProp2.Location = new System.Drawing.Point(3, 28);
            this.lblProp2.Name = "lblProp2";
            this.lblProp2.Size = new System.Drawing.Size(57, 20);
            this.lblProp2.TabIndex = 3;
            this.lblProp2.Text = "R";
            this.lblProp2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProp1
            // 
            this.cmbProp1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProp1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProp1.FormattingEnabled = true;
            this.cmbProp1.Items.AddRange(new object[] {
            "No",
            ">",
            "<",
            ">=",
            "<=",
            "=",
            "!="});
            this.cmbProp1.Location = new System.Drawing.Point(66, 0);
            this.cmbProp1.Name = "cmbProp1";
            this.cmbProp1.Size = new System.Drawing.Size(40, 21);
            this.cmbProp1.TabIndex = 1;
            this.cmbProp1.Tag = "BG2";
            this.cmbProp1.SelectedIndexChanged += new System.EventHandler(this.cmbProp1_SelectedIndexChanged);
            // 
            // lblProp1
            // 
            this.lblProp1.Location = new System.Drawing.Point(3, 0);
            this.lblProp1.Name = "lblProp1";
            this.lblProp1.Size = new System.Drawing.Size(57, 20);
            this.lblProp1.TabIndex = 0;
            this.lblProp1.Text = "R";
            this.lblProp1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numTimeLoop
            // 
            this.numTimeLoop.BackColor = System.Drawing.Color.Gray;
            this.numTimeLoop.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numTimeLoop.Enabled = false;
            this.numTimeLoop.Location = new System.Drawing.Point(70, 165);
            this.numTimeLoop.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numTimeLoop.Name = "numTimeLoop";
            this.numTimeLoop.Size = new System.Drawing.Size(67, 16);
            this.numTimeLoop.TabIndex = 2;
            this.numTimeLoop.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numTimeLoop.ValueChanged += new System.EventHandler(this.numTimeLoop_ValueChanged);
            // 
            // optTimeLoopParam
            // 
            this.optTimeLoopParam.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTimeLoopParam.FlatAppearance.BorderSize = 0;
            this.optTimeLoopParam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optTimeLoopParam.Location = new System.Drawing.Point(10, 159);
            this.optTimeLoopParam.Margin = new System.Windows.Forms.Padding(0);
            this.optTimeLoopParam.Name = "optTimeLoopParam";
            this.optTimeLoopParam.Size = new System.Drawing.Size(188, 25);
            this.optTimeLoopParam.TabIndex = 1;
            this.optTimeLoopParam.Text = "Toutes les                         ms";
            this.optTimeLoopParam.UseVisualStyleBackColor = true;
            this.optTimeLoopParam.CheckedChanged += new System.EventHandler(this.optTimeLoopParam_CheckedChanged);
            // 
            // optTimeLoopAlways
            // 
            this.optTimeLoopAlways.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTimeLoopAlways.Checked = true;
            this.optTimeLoopAlways.FlatAppearance.BorderSize = 0;
            this.optTimeLoopAlways.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optTimeLoopAlways.Location = new System.Drawing.Point(10, 132);
            this.optTimeLoopAlways.Margin = new System.Windows.Forms.Padding(0);
            this.optTimeLoopAlways.Name = "optTimeLoopAlways";
            this.optTimeLoopAlways.Size = new System.Drawing.Size(188, 25);
            this.optTimeLoopAlways.TabIndex = 0;
            this.optTimeLoopAlways.TabStop = true;
            this.optTimeLoopAlways.Text = "Toujours";
            this.optTimeLoopAlways.UseVisualStyleBackColor = true;
            this.optTimeLoopAlways.CheckedChanged += new System.EventHandler(this.optTimeLoopAlways_CheckedChanged);
            // 
            // pnlTrigger
            // 
            this.pnlTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlTrigger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlTrigger.Controls.Add(this.lblScriptName);
            this.pnlTrigger.Controls.Add(this.txtTriggerName);
            this.pnlTrigger.Controls.Add(this.panel17);
            this.pnlTrigger.Controls.Add(this.panel16);
            this.pnlTrigger.Location = new System.Drawing.Point(0, 0);
            this.pnlTrigger.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTrigger.Name = "pnlTrigger";
            this.pnlTrigger.Size = new System.Drawing.Size(174, 304);
            this.pnlTrigger.TabIndex = 2;
            this.pnlTrigger.Tag = "BG1";
            // 
            // lblScriptName
            // 
            this.lblScriptName.AutoSize = true;
            this.lblScriptName.ForeColor = System.Drawing.Color.White;
            this.lblScriptName.Location = new System.Drawing.Point(10, 60);
            this.lblScriptName.Name = "lblScriptName";
            this.lblScriptName.Size = new System.Drawing.Size(29, 13);
            this.lblScriptName.TabIndex = 20;
            this.lblScriptName.Text = "Nom";
            // 
            // txtTriggerName
            // 
            this.txtTriggerName.BackColor = System.Drawing.Color.Gray;
            this.txtTriggerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTriggerName.Location = new System.Drawing.Point(45, 57);
            this.txtTriggerName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtTriggerName.Multiline = false;
            this.txtTriggerName.Name = "txtTriggerName";
            this.txtTriggerName.Size = new System.Drawing.Size(126, 21);
            this.txtTriggerName.TabIndex = 19;
            this.txtTriggerName.Tag = "BG2";
            this.txtTriggerName.Text = "";
            this.txtTriggerName.ZoomFactor = 1.2F;
            this.txtTriggerName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtTriggerName_MouseDown);
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.Gray;
            this.panel17.Controls.Add(this.lblScript);
            this.panel17.Location = new System.Drawing.Point(0, 0);
            this.panel17.Margin = new System.Windows.Forms.Padding(0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(174, 50);
            this.panel17.TabIndex = 18;
            this.panel17.Tag = "BG2";
            // 
            // lblScript
            // 
            this.lblScript.AutoSize = true;
            this.lblScript.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScript.ForeColor = System.Drawing.Color.Black;
            this.lblScript.Location = new System.Drawing.Point(6, 16);
            this.lblScript.Margin = new System.Windows.Forms.Padding(6);
            this.lblScript.Name = "lblScript";
            this.lblScript.Size = new System.Drawing.Size(106, 19);
            this.lblScript.TabIndex = 0;
            this.lblScript.Tag = "F2";
            this.lblScript.Text = "Déclencheur";
            // 
            // panel16
            // 
            this.panel16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel16.BackColor = System.Drawing.Color.Silver;
            this.panel16.Controls.Add(this.btnChangeTriggerName);
            this.panel16.Controls.Add(this.btnAddTrigger);
            this.panel16.Controls.Add(this.btnDelTrigger);
            this.panel16.Controls.Add(this.listboxTrigger);
            this.panel16.Location = new System.Drawing.Point(0, 109);
            this.panel16.Margin = new System.Windows.Forms.Padding(0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(174, 195);
            this.panel16.TabIndex = 12;
            this.panel16.Tag = "B";
            // 
            // btnChangeTriggerName
            // 
            this.btnChangeTriggerName.BackColor = System.Drawing.Color.Gray;
            this.btnChangeTriggerName.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnChangeTriggerName.FlatAppearance.BorderSize = 0;
            this.btnChangeTriggerName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeTriggerName.Font = new System.Drawing.Font("Wingdings", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnChangeTriggerName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnChangeTriggerName.Location = new System.Drawing.Point(109, 4);
            this.btnChangeTriggerName.Margin = new System.Windows.Forms.Padding(0);
            this.btnChangeTriggerName.Name = "btnChangeTriggerName";
            this.btnChangeTriggerName.Size = new System.Drawing.Size(42, 35);
            this.btnChangeTriggerName.TabIndex = 17;
            this.btnChangeTriggerName.Tag = "BG2";
            this.btnChangeTriggerName.Text = "ü";
            this.btnChangeTriggerName.UseVisualStyleBackColor = false;
            this.btnChangeTriggerName.Click += new System.EventHandler(this.btnChangeTriggerName_Click);
            // 
            // btnAddTrigger
            // 
            this.btnAddTrigger.BackColor = System.Drawing.Color.Gray;
            this.btnAddTrigger.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAddTrigger.FlatAppearance.BorderSize = 0;
            this.btnAddTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTrigger.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnAddTrigger.Location = new System.Drawing.Point(25, 4);
            this.btnAddTrigger.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddTrigger.Name = "btnAddTrigger";
            this.btnAddTrigger.Size = new System.Drawing.Size(42, 35);
            this.btnAddTrigger.TabIndex = 15;
            this.btnAddTrigger.Tag = "BG2";
            this.btnAddTrigger.Text = "+";
            this.btnAddTrigger.UseVisualStyleBackColor = false;
            this.btnAddTrigger.Click += new System.EventHandler(this.btnAddTrigger_Click);
            this.btnAddTrigger.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddTrigger_MouseDown);
            // 
            // btnDelTrigger
            // 
            this.btnDelTrigger.BackColor = System.Drawing.Color.Gray;
            this.btnDelTrigger.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnDelTrigger.FlatAppearance.BorderSize = 0;
            this.btnDelTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelTrigger.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelTrigger.Location = new System.Drawing.Point(67, 4);
            this.btnDelTrigger.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelTrigger.Name = "btnDelTrigger";
            this.btnDelTrigger.Size = new System.Drawing.Size(42, 35);
            this.btnDelTrigger.TabIndex = 16;
            this.btnDelTrigger.Tag = "BG2";
            this.btnDelTrigger.Text = "-";
            this.btnDelTrigger.UseVisualStyleBackColor = false;
            this.btnDelTrigger.Click += new System.EventHandler(this.btnDelTrigger_Click);
            // 
            // pnlTypeTrigger
            // 
            this.pnlTypeTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlTypeTrigger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlTypeTrigger.Controls.Add(this.optTypeTriggerTime);
            this.pnlTypeTrigger.Controls.Add(this.optTypeTriggerLoading);
            this.pnlTypeTrigger.Controls.Add(this.optTypeTriggerMouse);
            this.pnlTypeTrigger.Controls.Add(this.optTypeTriggerValueOverflow);
            this.pnlTypeTrigger.Controls.Add(this.optTypeTriggerNoCollision);
            this.pnlTypeTrigger.Controls.Add(this.optTypeTriggerCollision);
            this.pnlTypeTrigger.Controls.Add(this.panel1);
            this.pnlTypeTrigger.Controls.Add(this.panel5);
            this.pnlTypeTrigger.Location = new System.Drawing.Point(174, 0);
            this.pnlTypeTrigger.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTypeTrigger.Name = "pnlTypeTrigger";
            this.pnlTypeTrigger.Size = new System.Drawing.Size(184, 305);
            this.pnlTypeTrigger.TabIndex = 3;
            this.pnlTypeTrigger.Tag = "BG1";
            this.pnlTypeTrigger.Visible = false;
            // 
            // optTypeTriggerTime
            // 
            this.optTypeTriggerTime.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTypeTriggerTime.Checked = true;
            this.optTypeTriggerTime.FlatAppearance.BorderSize = 0;
            this.optTypeTriggerTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optTypeTriggerTime.Location = new System.Drawing.Point(17, 234);
            this.optTypeTriggerTime.Margin = new System.Windows.Forms.Padding(0);
            this.optTypeTriggerTime.Name = "optTypeTriggerTime";
            this.optTypeTriggerTime.Size = new System.Drawing.Size(154, 25);
            this.optTypeTriggerTime.TabIndex = 26;
            this.optTypeTriggerTime.TabStop = true;
            this.optTypeTriggerTime.Text = "Temps";
            this.optTypeTriggerTime.UseVisualStyleBackColor = true;
            this.optTypeTriggerTime.CheckedChanged += new System.EventHandler(this.optTypeTriggerTime_CheckedChanged);
            // 
            // optTypeTriggerLoading
            // 
            this.optTypeTriggerLoading.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTypeTriggerLoading.FlatAppearance.BorderSize = 0;
            this.optTypeTriggerLoading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optTypeTriggerLoading.Location = new System.Drawing.Point(17, 209);
            this.optTypeTriggerLoading.Margin = new System.Windows.Forms.Padding(0);
            this.optTypeTriggerLoading.Name = "optTypeTriggerLoading";
            this.optTypeTriggerLoading.Size = new System.Drawing.Size(154, 25);
            this.optTypeTriggerLoading.TabIndex = 25;
            this.optTypeTriggerLoading.Text = "Chargement";
            this.optTypeTriggerLoading.UseVisualStyleBackColor = true;
            this.optTypeTriggerLoading.CheckedChanged += new System.EventHandler(this.optTypeTriggerLoading_CheckedChanged);
            // 
            // optTypeTriggerMouse
            // 
            this.optTypeTriggerMouse.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTypeTriggerMouse.FlatAppearance.BorderSize = 0;
            this.optTypeTriggerMouse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optTypeTriggerMouse.Location = new System.Drawing.Point(17, 184);
            this.optTypeTriggerMouse.Margin = new System.Windows.Forms.Padding(0);
            this.optTypeTriggerMouse.Name = "optTypeTriggerMouse";
            this.optTypeTriggerMouse.Size = new System.Drawing.Size(154, 25);
            this.optTypeTriggerMouse.TabIndex = 24;
            this.optTypeTriggerMouse.Text = "Souris";
            this.optTypeTriggerMouse.UseVisualStyleBackColor = true;
            this.optTypeTriggerMouse.CheckedChanged += new System.EventHandler(this.optTypeTriggerMouse_CheckedChanged);
            // 
            // optTypeTriggerValueOverflow
            // 
            this.optTypeTriggerValueOverflow.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTypeTriggerValueOverflow.FlatAppearance.BorderSize = 0;
            this.optTypeTriggerValueOverflow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optTypeTriggerValueOverflow.Location = new System.Drawing.Point(17, 159);
            this.optTypeTriggerValueOverflow.Margin = new System.Windows.Forms.Padding(0);
            this.optTypeTriggerValueOverflow.Name = "optTypeTriggerValueOverflow";
            this.optTypeTriggerValueOverflow.Size = new System.Drawing.Size(154, 25);
            this.optTypeTriggerValueOverflow.TabIndex = 23;
            this.optTypeTriggerValueOverflow.Text = "Changement de valeur";
            this.optTypeTriggerValueOverflow.UseVisualStyleBackColor = true;
            this.optTypeTriggerValueOverflow.CheckedChanged += new System.EventHandler(this.optTypeTriggerValueOverflow_CheckedChanged);
            // 
            // optTypeTriggerNoCollision
            // 
            this.optTypeTriggerNoCollision.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTypeTriggerNoCollision.FlatAppearance.BorderSize = 0;
            this.optTypeTriggerNoCollision.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optTypeTriggerNoCollision.Location = new System.Drawing.Point(17, 134);
            this.optTypeTriggerNoCollision.Margin = new System.Windows.Forms.Padding(0);
            this.optTypeTriggerNoCollision.Name = "optTypeTriggerNoCollision";
            this.optTypeTriggerNoCollision.Size = new System.Drawing.Size(154, 25);
            this.optTypeTriggerNoCollision.TabIndex = 22;
            this.optTypeTriggerNoCollision.Text = "Pas de collision";
            this.optTypeTriggerNoCollision.UseVisualStyleBackColor = true;
            this.optTypeTriggerNoCollision.CheckedChanged += new System.EventHandler(this.optTypeTriggerNoCollision_CheckedChanged);
            // 
            // optTypeTriggerCollision
            // 
            this.optTypeTriggerCollision.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTypeTriggerCollision.FlatAppearance.BorderSize = 0;
            this.optTypeTriggerCollision.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optTypeTriggerCollision.Location = new System.Drawing.Point(17, 109);
            this.optTypeTriggerCollision.Margin = new System.Windows.Forms.Padding(0);
            this.optTypeTriggerCollision.Name = "optTypeTriggerCollision";
            this.optTypeTriggerCollision.Size = new System.Drawing.Size(154, 25);
            this.optTypeTriggerCollision.TabIndex = 21;
            this.optTypeTriggerCollision.Text = "Collision";
            this.optTypeTriggerCollision.UseVisualStyleBackColor = true;
            this.optTypeTriggerCollision.CheckedChanged += new System.EventHandler(this.optTypeTriggerCollision_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(6, 255);
            this.panel1.TabIndex = 20;
            this.panel1.Tag = "BG2";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gray;
            this.panel5.Controls.Add(this.label7);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(184, 50);
            this.panel5.TabIndex = 19;
            this.panel5.Tag = "BG2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Margin = new System.Windows.Forms.Padding(6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(168, 19);
            this.label7.TabIndex = 0;
            this.label7.Tag = "F2";
            this.label7.Text = "Type de déclencheur";
            // 
            // pnlMouse
            // 
            this.pnlMouse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMouse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlMouse.Controls.Add(this.optMouseStayOver);
            this.pnlMouse.Controls.Add(this.optMouseLeave);
            this.pnlMouse.Controls.Add(this.optMouseEnter);
            this.pnlMouse.Controls.Add(this.optMouseLeftClick);
            this.pnlMouse.Controls.Add(this.optMouseRightClick);
            this.pnlMouse.Controls.Add(this.panel7);
            this.pnlMouse.Location = new System.Drawing.Point(358, 0);
            this.pnlMouse.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMouse.Name = "pnlMouse";
            this.pnlMouse.Size = new System.Drawing.Size(208, 305);
            this.pnlMouse.TabIndex = 4;
            this.pnlMouse.Tag = "B";
            this.pnlMouse.Visible = false;
            // 
            // optMouseStayOver
            // 
            this.optMouseStayOver.Appearance = System.Windows.Forms.Appearance.Button;
            this.optMouseStayOver.FlatAppearance.BorderSize = 0;
            this.optMouseStayOver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optMouseStayOver.Location = new System.Drawing.Point(10, 209);
            this.optMouseStayOver.Margin = new System.Windows.Forms.Padding(0);
            this.optMouseStayOver.Name = "optMouseStayOver";
            this.optMouseStayOver.Size = new System.Drawing.Size(188, 25);
            this.optMouseStayOver.TabIndex = 35;
            this.optMouseStayOver.Text = "Reste au dessus";
            this.optMouseStayOver.UseVisualStyleBackColor = true;
            this.optMouseStayOver.CheckedChanged += new System.EventHandler(this.optMouseStayOver_CheckedChanged);
            // 
            // optMouseLeave
            // 
            this.optMouseLeave.Appearance = System.Windows.Forms.Appearance.Button;
            this.optMouseLeave.FlatAppearance.BorderSize = 0;
            this.optMouseLeave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optMouseLeave.Location = new System.Drawing.Point(10, 184);
            this.optMouseLeave.Margin = new System.Windows.Forms.Padding(0);
            this.optMouseLeave.Name = "optMouseLeave";
            this.optMouseLeave.Size = new System.Drawing.Size(188, 25);
            this.optMouseLeave.TabIndex = 34;
            this.optMouseLeave.Text = "Sort";
            this.optMouseLeave.UseVisualStyleBackColor = true;
            this.optMouseLeave.CheckedChanged += new System.EventHandler(this.optMouseLeave_CheckedChanged);
            // 
            // optMouseEnter
            // 
            this.optMouseEnter.Appearance = System.Windows.Forms.Appearance.Button;
            this.optMouseEnter.FlatAppearance.BorderSize = 0;
            this.optMouseEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optMouseEnter.Location = new System.Drawing.Point(10, 159);
            this.optMouseEnter.Margin = new System.Windows.Forms.Padding(0);
            this.optMouseEnter.Name = "optMouseEnter";
            this.optMouseEnter.Size = new System.Drawing.Size(188, 25);
            this.optMouseEnter.TabIndex = 33;
            this.optMouseEnter.Text = "Entre";
            this.optMouseEnter.UseVisualStyleBackColor = true;
            this.optMouseEnter.CheckedChanged += new System.EventHandler(this.optMouseEnter_CheckedChanged);
            // 
            // optMouseLeftClick
            // 
            this.optMouseLeftClick.Appearance = System.Windows.Forms.Appearance.Button;
            this.optMouseLeftClick.FlatAppearance.BorderSize = 0;
            this.optMouseLeftClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optMouseLeftClick.Location = new System.Drawing.Point(10, 134);
            this.optMouseLeftClick.Margin = new System.Windows.Forms.Padding(0);
            this.optMouseLeftClick.Name = "optMouseLeftClick";
            this.optMouseLeftClick.Size = new System.Drawing.Size(188, 25);
            this.optMouseLeftClick.TabIndex = 32;
            this.optMouseLeftClick.Text = "Click gauche";
            this.optMouseLeftClick.UseVisualStyleBackColor = true;
            this.optMouseLeftClick.CheckedChanged += new System.EventHandler(this.optMouseLeftClick_CheckedChanged);
            // 
            // optMouseRightClick
            // 
            this.optMouseRightClick.Appearance = System.Windows.Forms.Appearance.Button;
            this.optMouseRightClick.Checked = true;
            this.optMouseRightClick.FlatAppearance.BorderSize = 0;
            this.optMouseRightClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optMouseRightClick.Location = new System.Drawing.Point(10, 109);
            this.optMouseRightClick.Margin = new System.Windows.Forms.Padding(0);
            this.optMouseRightClick.Name = "optMouseRightClick";
            this.optMouseRightClick.Size = new System.Drawing.Size(188, 25);
            this.optMouseRightClick.TabIndex = 31;
            this.optMouseRightClick.TabStop = true;
            this.optMouseRightClick.Text = "Click droit";
            this.optMouseRightClick.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.DarkGray;
            this.panel7.Controls.Add(this.panel18);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(208, 50);
            this.panel7.TabIndex = 30;
            this.panel7.Tag = "B";
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.Gray;
            this.panel18.Location = new System.Drawing.Point(0, 0);
            this.panel18.Margin = new System.Windows.Forms.Padding(0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(208, 10);
            this.panel18.TabIndex = 2;
            this.panel18.Tag = "BG2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Margin = new System.Windows.Forms.Padding(6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 19);
            this.label8.TabIndex = 0;
            this.label8.Tag = "F2";
            this.label8.Text = "Souris";
            // 
            // pnlEntityCollision
            // 
            this.pnlEntityCollision.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlEntityCollision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlEntityCollision.Controls.Add(this.treeViewCollision);
            this.pnlEntityCollision.Controls.Add(this.panel9);
            this.pnlEntityCollision.Location = new System.Drawing.Point(358, 0);
            this.pnlEntityCollision.Margin = new System.Windows.Forms.Padding(0);
            this.pnlEntityCollision.Name = "pnlEntityCollision";
            this.pnlEntityCollision.Size = new System.Drawing.Size(208, 304);
            this.pnlEntityCollision.TabIndex = 5;
            this.pnlEntityCollision.Tag = "B";
            this.pnlEntityCollision.Visible = false;
            // 
            // treeViewCollision
            // 
            this.treeViewCollision.AllowMultipleItemChecked = false;
            this.treeViewCollision.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewCollision.BackColor = System.Drawing.Color.Gray;
            this.treeViewCollision.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewCollision.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.treeViewCollision.HideSelection = false;
            this.treeViewCollision.HotTracking = true;
            this.treeViewCollision.ImageIndex = 0;
            this.treeViewCollision.Indent = 15;
            this.treeViewCollision.IsCheckedByMouse = false;
            this.treeViewCollision.ItemHeight = 25;
            this.treeViewCollision.ItemTypeCheckBoxed = Edit2D.UC.TreeViewLocalItemType.Entity;
            this.treeViewCollision.ItemTypeShowed = Edit2D.UC.TreeViewLocalItemType.Entity;
            this.treeViewCollision.Location = new System.Drawing.Point(6, 61);
            this.treeViewCollision.Name = "treeViewCollision";
            this.treeViewCollision.OrderType = Edit2D.UC.TreeViewLocalOrderType.OrderByEntity;
            this.treeViewCollision.Repository = null;
            this.treeViewCollision.SelectedImageIndex = 0;
            this.treeViewCollision.Size = new System.Drawing.Size(196, 237);
            this.treeViewCollision.TabIndex = 24;
            this.treeViewCollision.Tag = "BG2";
            this.treeViewCollision.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCollision_AfterCheck);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.DarkGray;
            this.panel9.Controls.Add(this.panel19);
            this.panel9.Controls.Add(this.label9);
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(208, 50);
            this.panel9.TabIndex = 23;
            this.panel9.Tag = "B";
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.Gray;
            this.panel19.Location = new System.Drawing.Point(0, 0);
            this.panel19.Margin = new System.Windows.Forms.Padding(0);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(208, 10);
            this.panel19.TabIndex = 2;
            this.panel19.Tag = "BG2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(6, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 19);
            this.label9.TabIndex = 0;
            this.label9.Tag = "F2";
            this.label9.Text = "Entite en collision";
            // 
            // pnlValueOverflow
            // 
            this.pnlValueOverflow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlValueOverflow.BackColor = System.Drawing.Color.DarkGray;
            this.pnlValueOverflow.Controls.Add(this.treeViewValueChanged);
            this.pnlValueOverflow.Controls.Add(this.panel11);
            this.pnlValueOverflow.Controls.Add(this.pnlValueProp);
            this.pnlValueOverflow.Location = new System.Drawing.Point(358, 0);
            this.pnlValueOverflow.Margin = new System.Windows.Forms.Padding(0);
            this.pnlValueOverflow.Name = "pnlValueOverflow";
            this.pnlValueOverflow.Size = new System.Drawing.Size(208, 305);
            this.pnlValueOverflow.TabIndex = 6;
            this.pnlValueOverflow.Tag = "B";
            this.pnlValueOverflow.Visible = false;
            // 
            // treeViewValueChanged
            // 
            this.treeViewValueChanged.AllowMultipleItemChecked = false;
            this.treeViewValueChanged.BackColor = System.Drawing.Color.Gray;
            this.treeViewValueChanged.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewValueChanged.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.treeViewValueChanged.HideSelection = false;
            this.treeViewValueChanged.HotTracking = true;
            this.treeViewValueChanged.ImageIndex = 0;
            this.treeViewValueChanged.Indent = 15;
            this.treeViewValueChanged.IsCheckedByMouse = false;
            this.treeViewValueChanged.ItemHeight = 25;
            this.treeViewValueChanged.ItemTypeCheckBoxed = Edit2D.UC.TreeViewLocalItemType.None;
            this.treeViewValueChanged.ItemTypeShowed = Edit2D.UC.TreeViewLocalItemType.None;
            this.treeViewValueChanged.Location = new System.Drawing.Point(7, 53);
            this.treeViewValueChanged.Name = "treeViewValueChanged";
            this.treeViewValueChanged.OrderType = Edit2D.UC.TreeViewLocalOrderType.OrderByEntity;
            this.treeViewValueChanged.Repository = null;
            this.treeViewValueChanged.SelectedImageIndex = 0;
            this.treeViewValueChanged.Size = new System.Drawing.Size(195, 166);
            this.treeViewValueChanged.TabIndex = 27;
            this.treeViewValueChanged.Tag = "BG2";
            this.treeViewValueChanged.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewValueChanged_AfterCheck);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.DarkGray;
            this.panel11.Controls.Add(this.panel20);
            this.panel11.Controls.Add(this.label10);
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Margin = new System.Windows.Forms.Padding(0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(208, 50);
            this.panel11.TabIndex = 26;
            this.panel11.Tag = "B";
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.Gray;
            this.panel20.Location = new System.Drawing.Point(0, 0);
            this.panel20.Margin = new System.Windows.Forms.Padding(0);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(208, 10);
            this.panel20.TabIndex = 2;
            this.panel20.Tag = "BG2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Margin = new System.Windows.Forms.Padding(6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(188, 19);
            this.label10.TabIndex = 0;
            this.label10.Tag = "F2";
            this.label10.Text = "Dépassement de valeur";
            // 
            // pnlTime
            // 
            this.pnlTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlTime.Controls.Add(this.panel13);
            this.pnlTime.Controls.Add(this.numTimeLoop);
            this.pnlTime.Controls.Add(this.optTimeLoopParam);
            this.pnlTime.Controls.Add(this.optTimeLoopAlways);
            this.pnlTime.Location = new System.Drawing.Point(358, 0);
            this.pnlTime.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTime.Name = "pnlTime";
            this.pnlTime.Size = new System.Drawing.Size(208, 305);
            this.pnlTime.TabIndex = 7;
            this.pnlTime.Tag = "B";
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.DarkGray;
            this.panel13.Controls.Add(this.panel21);
            this.panel13.Controls.Add(this.label11);
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Margin = new System.Windows.Forms.Padding(0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(208, 50);
            this.panel13.TabIndex = 19;
            this.panel13.Tag = "B";
            // 
            // panel21
            // 
            this.panel21.BackColor = System.Drawing.Color.Gray;
            this.panel21.Location = new System.Drawing.Point(0, 0);
            this.panel21.Margin = new System.Windows.Forms.Padding(0);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(208, 10);
            this.panel21.TabIndex = 2;
            this.panel21.Tag = "BG2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(6, 16);
            this.label11.Margin = new System.Windows.Forms.Padding(6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 19);
            this.label11.TabIndex = 0;
            this.label11.Tag = "F2";
            this.label11.Text = "Temps";
            // 
            // pnlScript
            // 
            this.pnlScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlScript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlScript.Controls.Add(this.treeViewScript);
            this.pnlScript.Controls.Add(this.panel2);
            this.pnlScript.Controls.Add(this.panel15);
            this.pnlScript.Location = new System.Drawing.Point(566, 0);
            this.pnlScript.Margin = new System.Windows.Forms.Padding(0);
            this.pnlScript.Name = "pnlScript";
            this.pnlScript.Size = new System.Drawing.Size(239, 305);
            this.pnlScript.TabIndex = 8;
            this.pnlScript.Tag = "BG1";
            this.pnlScript.Visible = false;
            // 
            // treeViewScript
            // 
            this.treeViewScript.AllowMultipleItemChecked = true;
            this.treeViewScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewScript.BackColor = System.Drawing.Color.Gray;
            this.treeViewScript.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewScript.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.treeViewScript.HideSelection = false;
            this.treeViewScript.HotTracking = true;
            this.treeViewScript.ImageIndex = 0;
            this.treeViewScript.Indent = 15;
            this.treeViewScript.IsCheckedByMouse = false;
            this.treeViewScript.ItemHeight = 25;
            this.treeViewScript.ItemTypeCheckBoxed = Edit2D.UC.TreeViewLocalItemType.None;
            this.treeViewScript.ItemTypeShowed = Edit2D.UC.TreeViewLocalItemType.None;
            this.treeViewScript.Location = new System.Drawing.Point(3, 53);
            this.treeViewScript.Name = "treeViewScript";
            this.treeViewScript.OrderType = Edit2D.UC.TreeViewLocalOrderType.OrderByEntity;
            this.treeViewScript.Repository = null;
            this.treeViewScript.SelectedImageIndex = 0;
            this.treeViewScript.Size = new System.Drawing.Size(227, 252);
            this.treeViewScript.TabIndex = 9;
            this.treeViewScript.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLocal_AfterCheck);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Location = new System.Drawing.Point(233, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(6, 255);
            this.panel2.TabIndex = 28;
            this.panel2.Tag = "BG2";
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.Gray;
            this.panel15.Controls.Add(this.label12);
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Margin = new System.Windows.Forms.Padding(0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(239, 50);
            this.panel15.TabIndex = 27;
            this.panel15.Tag = "BG2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(6, 16);
            this.label12.Margin = new System.Windows.Forms.Padding(6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 19);
            this.label12.TabIndex = 0;
            this.label12.Tag = "F2";
            this.label12.Text = "Script";
            // 
            // TriggerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlScript);
            this.Controls.Add(this.pnlTrigger);
            this.Controls.Add(this.pnlTypeTrigger);
            this.Controls.Add(this.pnlTime);
            this.Controls.Add(this.pnlEntityCollision);
            this.Controls.Add(this.pnlMouse);
            this.Controls.Add(this.pnlValueOverflow);
            this.Name = "TriggerControl";
            this.Size = new System.Drawing.Size(805, 304);
            this.Tag = "BG1";
            this.Load += new System.EventHandler(this.TriggerControl_Load);
            this.pnlValueProp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTimeLoop)).EndInit();
            this.pnlTrigger.ResumeLayout(false);
            this.pnlTrigger.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.pnlTypeTrigger.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlMouse.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnlEntityCollision.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.pnlValueOverflow.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.pnlTime.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.pnlScript.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listboxTrigger;
        private System.Windows.Forms.Panel pnlValueProp;
        private System.Windows.Forms.ComboBox cmbProp3;
        private System.Windows.Forms.Label lblProp3;
        private System.Windows.Forms.ComboBox cmbProp2;
        private System.Windows.Forms.Label lblProp2;
        private System.Windows.Forms.ComboBox cmbProp1;
        private System.Windows.Forms.Label lblProp1;
        private System.Windows.Forms.NumericUpDown numTimeLoop;
        private System.Windows.Forms.RadioButton optTimeLoopParam;
        private System.Windows.Forms.RadioButton optTimeLoopAlways;
        private System.Windows.Forms.Panel pnlTrigger;
        private System.Windows.Forms.Panel pnlTypeTrigger;
        private System.Windows.Forms.Panel pnlMouse;
        private System.Windows.Forms.Panel pnlEntityCollision;
        private System.Windows.Forms.Panel pnlValueOverflow;
        private System.Windows.Forms.Panel pnlTime;
        private System.Windows.Forms.Panel pnlScript;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button btnChangeTriggerName;
        private System.Windows.Forms.Button btnAddTrigger;
        private System.Windows.Forms.Button btnDelTrigger;
        private System.Windows.Forms.Label lblScriptName;
        private System.Windows.Forms.RichTextBox txtTriggerName;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label lblScript;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RichTextBox txtProp3;
        private System.Windows.Forms.RichTextBox txtProp2;
        private System.Windows.Forms.RichTextBox txtProp1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton optTypeTriggerTime;
        private System.Windows.Forms.RadioButton optTypeTriggerLoading;
        private System.Windows.Forms.RadioButton optTypeTriggerMouse;
        private System.Windows.Forms.RadioButton optTypeTriggerValueOverflow;
        private System.Windows.Forms.RadioButton optTypeTriggerNoCollision;
        private System.Windows.Forms.RadioButton optTypeTriggerCollision;
        private System.Windows.Forms.RadioButton optMouseStayOver;
        private System.Windows.Forms.RadioButton optMouseLeave;
        private System.Windows.Forms.RadioButton optMouseEnter;
        private System.Windows.Forms.RadioButton optMouseLeftClick;
        private System.Windows.Forms.RadioButton optMouseRightClick;
        private Edit2D.UC.TreeViewLocal treeViewScript;
        private Edit2D.UC.TreeViewLocal treeViewCollision;
        private Edit2D.UC.TreeViewLocal treeViewValueChanged;
    }
}
