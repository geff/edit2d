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
            this.btnAddScript = new System.Windows.Forms.Button();
            this.btnDelSrcipt = new System.Windows.Forms.Button();
            this.listboxScript = new System.Windows.Forms.ListBox();
            this.btnDelAction = new System.Windows.Forms.Button();
            this.btnAddAction = new System.Windows.Forms.Button();
            this.treeViewAction = new System.Windows.Forms.TreeView();
            this.btnActionDown = new System.Windows.Forms.Button();
            this.btnActionUp = new System.Windows.Forms.Button();
            this.propAction = new System.Windows.Forms.PropertyGrid();
            this.curveControl = new Xna.Tools.CurveControl();
            this.pnlActionEvent = new System.Windows.Forms.Panel();
            this.pnlActionEventLines = new System.Windows.Forms.Panel();
            this.pnlPropAction = new System.Windows.Forms.Panel();
            this.txtScriptName = new System.Windows.Forms.RichTextBox();
            this.pnlScriptAction = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnPlayScriptAction = new System.Windows.Forms.Button();
            this.btnPlayScript = new System.Windows.Forms.Button();
            this.actionSoundControl = new Edit2D.ScriptControl.ActionSoundControl();
            this.cmbActionProperties = new Edit2D.UC.ComboBoxLocal();
            this.cmbActionType = new Edit2D.UC.ComboBoxLocal();
            this.pnlMain.SuspendLayout();
            this.pnlActionEvent.SuspendLayout();
            this.pnlPropAction.SuspendLayout();
            this.pnlScriptAction.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 4;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 393F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlMain.Controls.Add(this.curveControl, 1, 0);
            this.pnlMain.Controls.Add(this.pnlActionEvent, 2, 0);
            this.pnlMain.Controls.Add(this.actionSoundControl, 3, 0);
            this.pnlMain.Controls.Add(this.pnlScriptAction, 0, 0);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 1;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Size = new System.Drawing.Size(940, 305);
            this.pnlMain.TabIndex = 0;
            // 
            // btnAddScript
            // 
            this.btnAddScript.BackColor = System.Drawing.Color.Gray;
            this.btnAddScript.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAddScript.FlatAppearance.BorderSize = 0;
            this.btnAddScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddScript.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnAddScript.Location = new System.Drawing.Point(24, 4);
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
            this.btnDelSrcipt.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnDelSrcipt.Location = new System.Drawing.Point(66, 4);
            this.btnDelSrcipt.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelSrcipt.Name = "btnDelSrcipt";
            this.btnDelSrcipt.Size = new System.Drawing.Size(42, 35);
            this.btnDelSrcipt.TabIndex = 1;
            this.btnDelSrcipt.Tag = "BG2";
            this.btnDelSrcipt.Text = "-";
            this.btnDelSrcipt.UseVisualStyleBackColor = false;
            this.btnDelSrcipt.Click += new System.EventHandler(this.btnDelScript_Click);
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
            this.listboxScript.Location = new System.Drawing.Point(3, 42);
            this.listboxScript.Name = "listboxScript";
            this.listboxScript.Size = new System.Drawing.Size(168, 156);
            this.listboxScript.TabIndex = 8;
            this.listboxScript.Tag = "BG2";
            this.listboxScript.SelectedIndexChanged += new System.EventHandler(this.listboxScript_SelectedIndexChanged);
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
            this.treeViewAction.Size = new System.Drawing.Size(210, 70);
            this.treeViewAction.TabIndex = 13;
            this.treeViewAction.Tag = "BG2";
            this.treeViewAction.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewAction_AfterSelect);
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
            // propAction
            // 
            this.propAction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propAction.BackColor = System.Drawing.Color.DimGray;
            this.propAction.CommandsVisibleIfAvailable = false;
            this.propAction.HelpVisible = false;
            this.propAction.LargeButtons = true;
            this.propAction.LineColor = System.Drawing.SystemColors.ControlDarkDark;
            this.propAction.Location = new System.Drawing.Point(-1, -1);
            this.propAction.Name = "propAction";
            this.propAction.Size = new System.Drawing.Size(212, 88);
            this.propAction.TabIndex = 17;
            this.propAction.Tag = "BG2";
            this.propAction.ToolbarVisible = false;
            this.propAction.ViewBackColor = System.Drawing.Color.DimGray;
            this.propAction.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propAction_PropertyValueChanged);
            // 
            // curveControl
            // 
            this.curveControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curveControl.Location = new System.Drawing.Point(396, 3);
            this.curveControl.Name = "curveControl";
            this.curveControl.Size = new System.Drawing.Size(260, 299);
            this.curveControl.TabIndex = 10;
            this.curveControl.TimeLine = 0;
            this.curveControl.Visible = false;
            this.curveControl.CurveChange += new Xna.Tools.CurveControl.CurveChangeEventHandler(this.curveControl_CurveChange);
            this.curveControl.TimeLineChange += new Xna.Tools.CurveControl.TimeLineChangeEventHandler(this.curveControl_TimeLineChange);
            // 
            // pnlActionEvent
            // 
            this.pnlActionEvent.AutoScroll = true;
            this.pnlActionEvent.Controls.Add(this.pnlActionEventLines);
            this.pnlActionEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActionEvent.Location = new System.Drawing.Point(662, 3);
            this.pnlActionEvent.Name = "pnlActionEvent";
            this.pnlActionEvent.Size = new System.Drawing.Size(101, 299);
            this.pnlActionEvent.TabIndex = 14;
            this.pnlActionEvent.Visible = false;
            // 
            // pnlActionEventLines
            // 
            this.pnlActionEventLines.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActionEventLines.Location = new System.Drawing.Point(0, 0);
            this.pnlActionEventLines.Name = "pnlActionEventLines";
            this.pnlActionEventLines.Size = new System.Drawing.Size(101, 100);
            this.pnlActionEventLines.TabIndex = 0;
            // 
            // pnlPropAction
            // 
            this.pnlPropAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlPropAction.Controls.Add(this.propAction);
            this.pnlPropAction.Location = new System.Drawing.Point(0, 115);
            this.pnlPropAction.Name = "pnlPropAction";
            this.pnlPropAction.Size = new System.Drawing.Size(210, 86);
            this.pnlPropAction.TabIndex = 1;
            // 
            // txtScriptName
            // 
            this.txtScriptName.BackColor = System.Drawing.Color.Gray;
            this.txtScriptName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScriptName.Location = new System.Drawing.Point(45, 48);
            this.txtScriptName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.txtScriptName.Name = "txtScriptName";
            this.txtScriptName.Size = new System.Drawing.Size(126, 21);
            this.txtScriptName.TabIndex = 14;
            this.txtScriptName.Tag = "BG2";
            this.txtScriptName.Text = "";
            this.txtScriptName.ZoomFactor = 1.2F;
            // 
            // pnlScriptAction
            // 
            this.pnlScriptAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlScriptAction.Controls.Add(this.panel6);
            this.pnlScriptAction.Controls.Add(this.label5);
            this.pnlScriptAction.Controls.Add(this.label4);
            this.pnlScriptAction.Controls.Add(this.panel1);
            this.pnlScriptAction.Controls.Add(this.label3);
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(174, 41);
            this.panel2.TabIndex = 0;
            this.panel2.Tag = "BG2";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Location = new System.Drawing.Point(174, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(220, 10);
            this.panel3.TabIndex = 1;
            this.panel3.Tag = "BG2";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkGray;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(174, 10);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(220, 31);
            this.panel4.TabIndex = 2;
            this.panel4.Tag = "B";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BackColor = System.Drawing.Color.DarkGray;
            this.panel5.Location = new System.Drawing.Point(174, 41);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(6, 264);
            this.panel5.TabIndex = 3;
            this.panel5.Tag = "B";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 0;
            this.label1.Tag = "F2";
            this.label1.Text = "Script";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 19);
            this.label2.TabIndex = 1;
            this.label2.Tag = "F2";
            this.label2.Text = "Action";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Nom";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Controls.Add(this.listboxScript);
            this.panel1.Controls.Add(this.btnAddScript);
            this.panel1.Controls.Add(this.btnDelSrcipt);
            this.panel1.Controls.Add(this.btnPlayScript);
            this.panel1.Location = new System.Drawing.Point(0, 101);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 204);
            this.panel1.TabIndex = 18;
            this.panel1.Tag = "B";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(193, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Nom";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(193, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Propriété";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel6.BackColor = System.Drawing.Color.DarkGray;
            this.panel6.Controls.Add(this.pnlPropAction);
            this.panel6.Controls.Add(this.treeViewAction);
            this.panel6.Controls.Add(this.btnPlayScriptAction);
            this.panel6.Controls.Add(this.btnAddAction);
            this.panel6.Controls.Add(this.btnActionDown);
            this.panel6.Controls.Add(this.btnDelAction);
            this.panel6.Controls.Add(this.btnActionUp);
            this.panel6.Location = new System.Drawing.Point(180, 101);
            this.panel6.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(214, 204);
            this.panel6.TabIndex = 21;
            this.panel6.Tag = "B";
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
            // btnPlayScript
            // 
            this.btnPlayScript.BackColor = System.Drawing.Color.Gray;
            this.btnPlayScript.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnPlayScript.FlatAppearance.BorderSize = 0;
            this.btnPlayScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayScript.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayScript.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlayScript.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayScript.Image")));
            this.btnPlayScript.Location = new System.Drawing.Point(108, 4);
            this.btnPlayScript.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlayScript.Name = "btnPlayScript";
            this.btnPlayScript.Size = new System.Drawing.Size(42, 35);
            this.btnPlayScript.TabIndex = 13;
            this.btnPlayScript.Tag = "BG2";
            this.btnPlayScript.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPlayScript.UseVisualStyleBackColor = false;
            this.btnPlayScript.Click += new System.EventHandler(this.btnPlayScript_Click);
            // 
            // actionSoundControl
            // 
            this.actionSoundControl.ActionSound = null;
            this.actionSoundControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionSoundControl.Location = new System.Drawing.Point(769, 3);
            this.actionSoundControl.Name = "actionSoundControl";
            this.actionSoundControl.Size = new System.Drawing.Size(367, 299);
            this.actionSoundControl.TabIndex = 15;
            this.actionSoundControl.Visible = false;
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
            this.cmbActionProperties.Location = new System.Drawing.Point(271, 71);
            this.cmbActionProperties.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.cmbActionProperties.Name = "cmbActionProperties";
            this.cmbActionProperties.Size = new System.Drawing.Size(119, 22);
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
            this.cmbActionType.Location = new System.Drawing.Point(271, 48);
            this.cmbActionType.Margin = new System.Windows.Forms.Padding(3, 6, 3, 1);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.Size = new System.Drawing.Size(119, 21);
            this.cmbActionType.TabIndex = 11;
            this.cmbActionType.SelectedIndexChanged += new System.EventHandler(this.cmbActionType_SelectedIndexChanged);
            // 
            // ScriptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "ScriptControl";
            this.Size = new System.Drawing.Size(940, 305);
            this.Resize += new System.EventHandler(this.ScriptControl_Resize);
            this.pnlMain.ResumeLayout(false);
            this.pnlActionEvent.ResumeLayout(false);
            this.pnlPropAction.ResumeLayout(false);
            this.pnlScriptAction.ResumeLayout(false);
            this.pnlScriptAction.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private Xna.Tools.CurveControl curveControl;
        private System.Windows.Forms.Button btnPlayScript;
        private System.Windows.Forms.Button btnAddScript;
        private System.Windows.Forms.Button btnDelSrcipt;
        private System.Windows.Forms.TreeView treeViewAction;
        private System.Windows.Forms.Button btnPlayScriptAction;
        private System.Windows.Forms.Button btnActionDown;
        private System.Windows.Forms.Button btnActionUp;
        private Edit2D.UC.ComboBoxLocal cmbActionType;
        private System.Windows.Forms.Panel pnlActionEvent;
        private System.Windows.Forms.Panel pnlActionEventLines;
        private Edit2D.UC.ComboBoxLocal cmbActionProperties;
        private System.Windows.Forms.Button btnDelAction;
        private System.Windows.Forms.Button btnAddAction;
        private System.Windows.Forms.PropertyGrid propAction;
        private ActionSoundControl actionSoundControl;
        private System.Windows.Forms.ListBox listboxScript;
        private System.Windows.Forms.RichTextBox txtScriptName;
        private System.Windows.Forms.Panel pnlPropAction;
        private System.Windows.Forms.Panel pnlScriptAction;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
    }
}
