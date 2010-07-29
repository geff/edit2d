namespace Edit2D.ScriptControl
{
    partial class ScriptControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptControl));
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlScriptAction = new System.Windows.Forms.Panel();
            this.pnlAction = new System.Windows.Forms.Panel();
            this.propAction = new Edit2D.UC.PropertyGridLocal();
            this.treeViewAction = new System.Windows.Forms.TreeView();
            this.btnPlayScriptAction = new System.Windows.Forms.Button();
            this.btnAddAction = new System.Windows.Forms.Button();
            this.btnActionDown = new System.Windows.Forms.Button();
            this.btnDelAction = new System.Windows.Forms.Button();
            this.btnActionUp = new System.Windows.Forms.Button();
            this.lblActionProperty = new System.Windows.Forms.Label();
            this.lblActionName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnChangeScriptName = new System.Windows.Forms.Button();
            this.listboxScript = new System.Windows.Forms.ListBox();
            this.btnAddScript = new System.Windows.Forms.Button();
            this.btnDelSrcipt = new System.Windows.Forms.Button();
            this.btnPlayScript = new System.Windows.Forms.Button();
            this.lblScriptName = new System.Windows.Forms.Label();
            this.txtScriptName = new System.Windows.Forms.RichTextBox();
            this.cmbActionProperties = new Edit2D.UC.ComboBoxLocal();
            this.cmbActionType = new Edit2D.UC.ComboBoxLocal();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAction = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblScript = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.pnlActionEvent = new System.Windows.Forms.Panel();
            this.pnlActionEventLines = new System.Windows.Forms.Panel();
            this.pnlCurve = new System.Windows.Forms.Panel();
            this.curveControl = new Xna.Tools.CurveControl();
            this.actionSoundControl = new Edit2D.ScriptControl.ActionSoundControl();
            this.pnlMain.SuspendLayout();
            this.pnlScriptAction.SuspendLayout();
            this.pnlAction.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlActionEvent.SuspendLayout();
            this.pnlCurve.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 2;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 393F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 737F));
            this.pnlMain.Controls.Add(this.pnlScriptAction, 0, 0);
            this.pnlMain.Controls.Add(this.pnlActions, 1, 0);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 1;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlMain.Size = new System.Drawing.Size(1130, 305);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlScriptAction
            // 
            this.pnlScriptAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlScriptAction.Controls.Add(this.pnlAction);
            this.pnlScriptAction.Controls.Add(this.lblActionProperty);
            this.pnlScriptAction.Controls.Add(this.lblActionName);
            this.pnlScriptAction.Controls.Add(this.panel1);
            this.pnlScriptAction.Controls.Add(this.lblScriptName);
            this.pnlScriptAction.Controls.Add(this.txtScriptName);
            this.pnlScriptAction.Controls.Add(this.cmbActionProperties);
            this.pnlScriptAction.Controls.Add(this.cmbActionType);
            this.pnlScriptAction.Controls.Add(this.panel5);
            this.pnlScriptAction.Controls.Add(this.panel4);
            this.pnlScriptAction.Controls.Add(this.panel3);
            this.pnlScriptAction.Controls.Add(this.panel2);
            this.pnlScriptAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScriptAction.Location = new System.Drawing.Point(0, 0);
            this.pnlScriptAction.Margin = new System.Windows.Forms.Padding(0);
            this.pnlScriptAction.Name = "pnlScriptAction";
            this.pnlScriptAction.Size = new System.Drawing.Size(393, 305);
            this.pnlScriptAction.TabIndex = 16;
            this.pnlScriptAction.Tag = "BG1";
            // 
            // pnlAction
            // 
            this.pnlAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlAction.BackColor = System.Drawing.Color.DarkGray;
            this.pnlAction.Controls.Add(this.propAction);
            this.pnlAction.Controls.Add(this.treeViewAction);
            this.pnlAction.Controls.Add(this.btnPlayScriptAction);
            this.pnlAction.Controls.Add(this.btnAddAction);
            this.pnlAction.Controls.Add(this.btnActionDown);
            this.pnlAction.Controls.Add(this.btnDelAction);
            this.pnlAction.Controls.Add(this.btnActionUp);
            this.pnlAction.Location = new System.Drawing.Point(180, 109);
            this.pnlAction.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(214, 196);
            this.pnlAction.TabIndex = 21;
            this.pnlAction.Tag = "B";
            // 
            // propAction
            // 
            this.propAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propAction.Location = new System.Drawing.Point(0, 132);
            this.propAction.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.propAction.Name = "propAction";
            this.propAction.Size = new System.Drawing.Size(210, 60);
            this.propAction.TabIndex = 0;
            // 
            // treeViewAction
            // 
            this.treeViewAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewAction.BackColor = System.Drawing.Color.Gray;
            this.treeViewAction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewAction.HideSelection = false;
            this.treeViewAction.Location = new System.Drawing.Point(0, 42);
            this.treeViewAction.Margin = new System.Windows.Forms.Padding(0, 2, 2, 2);
            this.treeViewAction.Name = "treeViewAction";
            this.treeViewAction.Size = new System.Drawing.Size(210, 86);
            this.treeViewAction.TabIndex = 13;
            this.treeViewAction.Tag = "BG2";
            this.treeViewAction.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewAction_AfterSelect);
            // 
            // btnPlayScriptAction
            // 
            this.btnPlayScriptAction.BackColor = System.Drawing.Color.Gray;
            this.btnPlayScriptAction.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnPlayScriptAction.FlatAppearance.BorderSize = 0;
            this.btnPlayScriptAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayScriptAction.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayScriptAction.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlayScriptAction.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayScriptAction.Image")));
            this.btnPlayScriptAction.Location = new System.Drawing.Point(168, 4);
            this.btnPlayScriptAction.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlayScriptAction.Name = "btnPlayScriptAction";
            this.btnPlayScriptAction.Size = new System.Drawing.Size(42, 35);
            this.btnPlayScriptAction.TabIndex = 12;
            this.btnPlayScriptAction.Tag = "BG2";
            this.btnPlayScriptAction.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPlayScriptAction.UseVisualStyleBackColor = false;
            this.btnPlayScriptAction.Click += new System.EventHandler(this.btnPlayScriptAction_Click);
            // 
            // btnAddAction
            // 
            this.btnAddAction.BackColor = System.Drawing.Color.Gray;
            this.btnAddAction.FlatAppearance.BorderSize = 0;
            this.btnAddAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAction.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAction.Location = new System.Drawing.Point(0, 4);
            this.btnAddAction.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddAction.Name = "btnAddAction";
            this.btnAddAction.Size = new System.Drawing.Size(42, 35);
            this.btnAddAction.TabIndex = 15;
            this.btnAddAction.Tag = "BG2";
            this.btnAddAction.Text = "+";
            this.btnAddAction.UseVisualStyleBackColor = false;
            this.btnAddAction.Click += new System.EventHandler(this.btnAddAction_Click);
            // 
            // btnActionDown
            // 
            this.btnActionDown.BackColor = System.Drawing.Color.Gray;
            this.btnActionDown.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnActionDown.FlatAppearance.BorderSize = 0;
            this.btnActionDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionDown.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActionDown.Location = new System.Drawing.Point(84, 4);
            this.btnActionDown.Margin = new System.Windows.Forms.Padding(0);
            this.btnActionDown.Name = "btnActionDown";
            this.btnActionDown.Size = new System.Drawing.Size(42, 35);
            this.btnActionDown.TabIndex = 8;
            this.btnActionDown.Tag = "BG2";
            this.btnActionDown.Text = "↓";
            this.btnActionDown.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnActionDown.UseVisualStyleBackColor = false;
            this.btnActionDown.Click += new System.EventHandler(this.btnActionDown_Click);
            // 
            // btnDelAction
            // 
            this.btnDelAction.BackColor = System.Drawing.Color.Gray;
            this.btnDelAction.FlatAppearance.BorderSize = 0;
            this.btnDelAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelAction.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelAction.Location = new System.Drawing.Point(42, 4);
            this.btnDelAction.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelAction.Name = "btnDelAction";
            this.btnDelAction.Size = new System.Drawing.Size(42, 35);
            this.btnDelAction.TabIndex = 16;
            this.btnDelAction.Tag = "BG2";
            this.btnDelAction.Text = "-";
            this.btnDelAction.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelAction.UseVisualStyleBackColor = false;
            this.btnDelAction.Click += new System.EventHandler(this.btnDelAction_Click);
            // 
            // btnActionUp
            // 
            this.btnActionUp.BackColor = System.Drawing.Color.Gray;
            this.btnActionUp.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnActionUp.FlatAppearance.BorderSize = 0;
            this.btnActionUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionUp.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActionUp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnActionUp.Location = new System.Drawing.Point(126, 4);
            this.btnActionUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnActionUp.Name = "btnActionUp";
            this.btnActionUp.Size = new System.Drawing.Size(42, 35);
            this.btnActionUp.TabIndex = 9;
            this.btnActionUp.Tag = "BG2";
            this.btnActionUp.Text = "↑";
            this.btnActionUp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnActionUp.UseVisualStyleBackColor = false;
            this.btnActionUp.Click += new System.EventHandler(this.btnActionUp_Click);
            // 
            // lblActionProperty
            // 
            this.lblActionProperty.AutoSize = true;
            this.lblActionProperty.ForeColor = System.Drawing.Color.White;
            this.lblActionProperty.Location = new System.Drawing.Point(193, 83);
            this.lblActionProperty.Name = "lblActionProperty";
            this.lblActionProperty.Size = new System.Drawing.Size(49, 13);
            this.lblActionProperty.TabIndex = 20;
            this.lblActionProperty.Text = "Propriété";
            // 
            // lblActionName
            // 
            this.lblActionName.AutoSize = true;
            this.lblActionName.ForeColor = System.Drawing.Color.White;
            this.lblActionName.Location = new System.Drawing.Point(193, 60);
            this.lblActionName.Name = "lblActionName";
            this.lblActionName.Size = new System.Drawing.Size(29, 13);
            this.lblActionName.TabIndex = 19;
            this.lblActionName.Text = "Nom";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Controls.Add(this.btnChangeScriptName);
            this.panel1.Controls.Add(this.listboxScript);
            this.panel1.Controls.Add(this.btnAddScript);
            this.panel1.Controls.Add(this.btnDelSrcipt);
            this.panel1.Controls.Add(this.btnPlayScript);
            this.panel1.Location = new System.Drawing.Point(0, 109);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 196);
            this.panel1.TabIndex = 18;
            this.panel1.Tag = "B";
            // 
            // btnChangeScriptName
            // 
            this.btnChangeScriptName.BackColor = System.Drawing.Color.Gray;
            this.btnChangeScriptName.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnChangeScriptName.FlatAppearance.BorderSize = 0;
            this.btnChangeScriptName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeScriptName.Font = new System.Drawing.Font("Wingdings", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnChangeScriptName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnChangeScriptName.Location = new System.Drawing.Point(90, 4);
            this.btnChangeScriptName.Margin = new System.Windows.Forms.Padding(0);
            this.btnChangeScriptName.Name = "btnChangeScriptName";
            this.btnChangeScriptName.Size = new System.Drawing.Size(42, 35);
            this.btnChangeScriptName.TabIndex = 14;
            this.btnChangeScriptName.Tag = "BG2";
            this.btnChangeScriptName.Text = "ü";
            this.btnChangeScriptName.UseVisualStyleBackColor = false;
            this.btnChangeScriptName.Click += new System.EventHandler(this.btnChangeScriptName_Click);
            // 
            // listboxScript
            // 
            this.listboxScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listboxScript.BackColor = System.Drawing.Color.Gray;
            this.listboxScript.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listboxScript.FormattingEnabled = true;
            this.listboxScript.Items.AddRange(new object[] {
            "test"});
            this.listboxScript.Location = new System.Drawing.Point(6, 42);
            this.listboxScript.Name = "listboxScript";
            this.listboxScript.Size = new System.Drawing.Size(168, 143);
            this.listboxScript.TabIndex = 8;
            this.listboxScript.Tag = "BG2";
            this.listboxScript.SelectedIndexChanged += new System.EventHandler(this.listboxScript_SelectedIndexChanged);
            // 
            // btnAddScript
            // 
            this.btnAddScript.BackColor = System.Drawing.Color.Gray;
            this.btnAddScript.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAddScript.FlatAppearance.BorderSize = 0;
            this.btnAddScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddScript.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnAddScript.Location = new System.Drawing.Point(6, 4);
            this.btnAddScript.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddScript.Name = "btnAddScript";
            this.btnAddScript.Size = new System.Drawing.Size(42, 35);
            this.btnAddScript.TabIndex = 0;
            this.btnAddScript.Tag = "BG2";
            this.btnAddScript.Text = "+";
            this.btnAddScript.UseVisualStyleBackColor = false;
            this.btnAddScript.Click += new System.EventHandler(this.btnAddScript_Click);
            // 
            // btnDelSrcipt
            // 
            this.btnDelSrcipt.BackColor = System.Drawing.Color.Gray;
            this.btnDelSrcipt.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnDelSrcipt.FlatAppearance.BorderSize = 0;
            this.btnDelSrcipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelSrcipt.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelSrcipt.Location = new System.Drawing.Point(48, 4);
            this.btnDelSrcipt.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelSrcipt.Name = "btnDelSrcipt";
            this.btnDelSrcipt.Size = new System.Drawing.Size(42, 35);
            this.btnDelSrcipt.TabIndex = 1;
            this.btnDelSrcipt.Tag = "BG2";
            this.btnDelSrcipt.Text = "-";
            this.btnDelSrcipt.UseVisualStyleBackColor = false;
            this.btnDelSrcipt.Click += new System.EventHandler(this.btnDelScript_Click);
            // 
            // btnPlayScript
            // 
            this.btnPlayScript.BackColor = System.Drawing.Color.Gray;
            this.btnPlayScript.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnPlayScript.FlatAppearance.BorderSize = 0;
            this.btnPlayScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayScript.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayScript.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlayScript.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayScript.Image")));
            this.btnPlayScript.Location = new System.Drawing.Point(132, 4);
            this.btnPlayScript.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlayScript.Name = "btnPlayScript";
            this.btnPlayScript.Size = new System.Drawing.Size(42, 35);
            this.btnPlayScript.TabIndex = 13;
            this.btnPlayScript.Tag = "BG2";
            this.btnPlayScript.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPlayScript.UseVisualStyleBackColor = false;
            this.btnPlayScript.Click += new System.EventHandler(this.btnPlayScript_Click);
            // 
            // lblScriptName
            // 
            this.lblScriptName.AutoSize = true;
            this.lblScriptName.ForeColor = System.Drawing.Color.White;
            this.lblScriptName.Location = new System.Drawing.Point(10, 60);
            this.lblScriptName.Name = "lblScriptName";
            this.lblScriptName.Size = new System.Drawing.Size(29, 13);
            this.lblScriptName.TabIndex = 17;
            this.lblScriptName.Text = "Nom";
            // 
            // txtScriptName
            // 
            this.txtScriptName.BackColor = System.Drawing.Color.Gray;
            this.txtScriptName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScriptName.Location = new System.Drawing.Point(45, 57);
            this.txtScriptName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtScriptName.Multiline = false;
            this.txtScriptName.Name = "txtScriptName";
            this.txtScriptName.Size = new System.Drawing.Size(126, 21);
            this.txtScriptName.TabIndex = 14;
            this.txtScriptName.Tag = "BG2";
            this.txtScriptName.Text = "";
            this.txtScriptName.ZoomFactor = 1.2F;
            this.txtScriptName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtScriptName_MouseDown);
            // 
            // cmbActionProperties
            // 
            this.cmbActionProperties.BackColor = System.Drawing.SystemColors.Control;
            this.cmbActionProperties.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbActionProperties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionProperties.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbActionProperties.FormattingEnabled = true;
            this.cmbActionProperties.ItemHeight = 16;
            this.cmbActionProperties.Items.AddRange(new object[] {
            "Position",
            "Angle",
            "Color"});
            this.cmbActionProperties.Location = new System.Drawing.Point(248, 80);
            this.cmbActionProperties.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.cmbActionProperties.Name = "cmbActionProperties";
            this.cmbActionProperties.Size = new System.Drawing.Size(142, 22);
            this.cmbActionProperties.TabIndex = 14;
            // 
            // cmbActionType
            // 
            this.cmbActionType.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmbActionType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbActionType.FormattingEnabled = true;
            this.cmbActionType.Items.AddRange(new object[] {
            "Courbe",
            "Evènement",
            "Son"});
            this.cmbActionType.Location = new System.Drawing.Point(248, 57);
            this.cmbActionType.Margin = new System.Windows.Forms.Padding(3, 6, 3, 1);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.Size = new System.Drawing.Size(142, 21);
            this.cmbActionType.TabIndex = 11;
            this.cmbActionType.SelectedIndexChanged += new System.EventHandler(this.cmbActionType_SelectedIndexChanged);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BackColor = System.Drawing.Color.DarkGray;
            this.panel5.Location = new System.Drawing.Point(174, 49);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(6, 256);
            this.panel5.TabIndex = 3;
            this.panel5.Tag = "B";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkGray;
            this.panel4.Controls.Add(this.lblAction);
            this.panel4.Location = new System.Drawing.Point(174, 10);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(232, 41);
            this.panel4.TabIndex = 2;
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
            this.lblAction.Size = new System.Drawing.Size(58, 19);
            this.lblAction.TabIndex = 1;
            this.lblAction.Tag = "F2";
            this.lblAction.Text = "Action";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Location = new System.Drawing.Point(174, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(280, 10);
            this.panel3.TabIndex = 1;
            this.panel3.Tag = "BG2";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.lblScript);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(174, 50);
            this.panel2.TabIndex = 0;
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
            this.lblScript.Size = new System.Drawing.Size(54, 19);
            this.lblScript.TabIndex = 0;
            this.lblScript.Tag = "F2";
            this.lblScript.Text = "Script";
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.pnlActionEvent);
            this.pnlActions.Controls.Add(this.pnlCurve);
            this.pnlActions.Controls.Add(this.actionSoundControl);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActions.Location = new System.Drawing.Point(393, 0);
            this.pnlActions.Margin = new System.Windows.Forms.Padding(0);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(737, 305);
            this.pnlActions.TabIndex = 11;
            // 
            // pnlActionEvent
            // 
            this.pnlActionEvent.AutoScroll = true;
            this.pnlActionEvent.Controls.Add(this.pnlActionEventLines);
            this.pnlActionEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActionEvent.Location = new System.Drawing.Point(0, 0);
            this.pnlActionEvent.Margin = new System.Windows.Forms.Padding(0);
            this.pnlActionEvent.Name = "pnlActionEvent";
            this.pnlActionEvent.Size = new System.Drawing.Size(737, 305);
            this.pnlActionEvent.TabIndex = 14;
            this.pnlActionEvent.Visible = false;
            // 
            // pnlActionEventLines
            // 
            this.pnlActionEventLines.AutoSize = true;
            this.pnlActionEventLines.Location = new System.Drawing.Point(0, 0);
            this.pnlActionEventLines.Margin = new System.Windows.Forms.Padding(0);
            this.pnlActionEventLines.Name = "pnlActionEventLines";
            this.pnlActionEventLines.Size = new System.Drawing.Size(232, 51);
            this.pnlActionEventLines.TabIndex = 0;
            // 
            // pnlCurve
            // 
            this.pnlCurve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlCurve.Controls.Add(this.curveControl);
            this.pnlCurve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCurve.Location = new System.Drawing.Point(0, 0);
            this.pnlCurve.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCurve.Name = "pnlCurve";
            this.pnlCurve.Size = new System.Drawing.Size(737, 305);
            this.pnlCurve.TabIndex = 0;
            this.pnlCurve.Visible = false;
            // 
            // curveControl
            // 
            this.curveControl.BackColor = System.Drawing.Color.Silver;
            this.curveControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curveControl.GridBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.curveControl.GridLineColor = System.Drawing.Color.Gray;
            this.curveControl.Location = new System.Drawing.Point(0, 0);
            this.curveControl.Margin = new System.Windows.Forms.Padding(0);
            this.curveControl.Name = "curveControl";
            this.curveControl.Size = new System.Drawing.Size(737, 305);
            this.curveControl.TabIndex = 10;
            this.curveControl.TimeLine = 0;
            this.curveControl.CurveChange += new Xna.Tools.CurveControl.CurveChangeEventHandler(this.curveControl_CurveChange);
            this.curveControl.TimeLineChange += new Xna.Tools.CurveControl.TimeLineChangeEventHandler(this.curveControl_TimeLineChange);
            // 
            // actionSoundControl
            // 
            this.actionSoundControl.ActionSound = null;
            this.actionSoundControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionSoundControl.Location = new System.Drawing.Point(0, 0);
            this.actionSoundControl.Name = "actionSoundControl";
            this.actionSoundControl.Size = new System.Drawing.Size(737, 305);
            this.actionSoundControl.TabIndex = 15;
            this.actionSoundControl.Visible = false;
            // 
            // ScriptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "ScriptControl";
            this.Size = new System.Drawing.Size(1130, 305);
            this.Resize += new System.EventHandler(this.ScriptControl_Resize);
            this.pnlMain.ResumeLayout(false);
            this.pnlScriptAction.ResumeLayout(false);
            this.pnlScriptAction.PerformLayout();
            this.pnlAction.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlActionEvent.ResumeLayout(false);
            this.pnlActionEvent.PerformLayout();
            this.pnlCurve.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Button btnPlayScript;
        private System.Windows.Forms.Button btnAddScript;
        private System.Windows.Forms.Button btnDelSrcipt;
        private System.Windows.Forms.TreeView treeViewAction;
        private System.Windows.Forms.Button btnPlayScriptAction;
        private System.Windows.Forms.Button btnActionDown;
        private System.Windows.Forms.Button btnActionUp;
        private Edit2D.UC.ComboBoxLocal cmbActionType;
        private Edit2D.UC.ComboBoxLocal cmbActionProperties;
        private System.Windows.Forms.Button btnDelAction;
        private System.Windows.Forms.Button btnAddAction;
        private System.Windows.Forms.ListBox listboxScript;
        private System.Windows.Forms.RichTextBox txtScriptName;
        private System.Windows.Forms.Panel pnlScriptAction;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label lblScript;
        private System.Windows.Forms.Label lblScriptName;
        private System.Windows.Forms.Label lblActionProperty;
        private System.Windows.Forms.Label lblActionName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlAction;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Panel pnlActionEvent;
        private System.Windows.Forms.Panel pnlActionEventLines;
        private ActionSoundControl actionSoundControl;
        private System.Windows.Forms.Panel pnlCurve;
        private Xna.Tools.CurveControl curveControl;
        private Edit2D.UC.PropertyGridLocal propAction;
        private System.Windows.Forms.Button btnChangeScriptName;
    }
}
