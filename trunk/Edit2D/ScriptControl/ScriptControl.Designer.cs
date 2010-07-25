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
            this.pnlScript = new System.Windows.Forms.TableLayoutPanel();
            this.btnPlayScript = new System.Windows.Forms.Button();
            this.btnAddScript = new System.Windows.Forms.Button();
            this.btnDelSrcipt = new System.Windows.Forms.Button();
            this.listboxScript = new System.Windows.Forms.ListBox();
            this.txtScriptName = new System.Windows.Forms.TextBox();
            this.pnlAction = new System.Windows.Forms.TableLayoutPanel();
            this.btnDelAction = new System.Windows.Forms.Button();
            this.btnAddAction = new System.Windows.Forms.Button();
            this.treeViewAction = new System.Windows.Forms.TreeView();
            this.btnPlayScriptAction = new System.Windows.Forms.Button();
            this.btnActionDown = new System.Windows.Forms.Button();
            this.btnActionUp = new System.Windows.Forms.Button();
            this.propAction = new System.Windows.Forms.PropertyGrid();
            this.curveControl = new Xna.Tools.CurveControl();
            this.pnlActionEvent = new System.Windows.Forms.Panel();
            this.pnlActionEventLines = new System.Windows.Forms.Panel();
            this.cmbActionType = new Edit2D.UC.ComboBoxLocal();
            this.cmbActionProperties = new Edit2D.UC.ComboBoxLocal();
            this.actionSoundControl = new Edit2D.ScriptControl.ActionSoundControl();
            this.pnlMain.SuspendLayout();
            this.pnlScript.SuspendLayout();
            this.pnlAction.SuspendLayout();
            this.pnlActionEvent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 5;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlMain.Controls.Add(this.pnlScript, 0, 0);
            this.pnlMain.Controls.Add(this.pnlAction, 1, 0);
            this.pnlMain.Controls.Add(this.curveControl, 2, 0);
            this.pnlMain.Controls.Add(this.pnlActionEvent, 3, 0);
            this.pnlMain.Controls.Add(this.actionSoundControl, 4, 0);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 1;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Size = new System.Drawing.Size(940, 305);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlScript
            // 
            this.pnlScript.ColumnCount = 3;
            this.pnlScript.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlScript.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlScript.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlScript.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlScript.Controls.Add(this.btnPlayScript, 2, 1);
            this.pnlScript.Controls.Add(this.btnAddScript, 0, 1);
            this.pnlScript.Controls.Add(this.btnDelSrcipt, 1, 1);
            this.pnlScript.Controls.Add(this.listboxScript, 0, 2);
            this.pnlScript.Controls.Add(this.txtScriptName, 0, 0);
            this.pnlScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScript.Location = new System.Drawing.Point(0, 0);
            this.pnlScript.Margin = new System.Windows.Forms.Padding(0);
            this.pnlScript.Name = "pnlScript";
            this.pnlScript.RowCount = 3;
            this.pnlScript.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.pnlScript.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.pnlScript.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlScript.Size = new System.Drawing.Size(132, 305);
            this.pnlScript.TabIndex = 8;
            // 
            // btnPlayScript
            // 
            this.btnPlayScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPlayScript.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnPlayScript.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPlayScript.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayScript.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlayScript.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayScript.Image")));
            this.btnPlayScript.Location = new System.Drawing.Point(89, 47);
            this.btnPlayScript.Margin = new System.Windows.Forms.Padding(1);
            this.btnPlayScript.Name = "btnPlayScript";
            this.btnPlayScript.Size = new System.Drawing.Size(42, 35);
            this.btnPlayScript.TabIndex = 13;
            this.btnPlayScript.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPlayScript.UseVisualStyleBackColor = true;
            this.btnPlayScript.Click += new System.EventHandler(this.btnPlayScript_Click);
            // 
            // btnAddScript
            // 
            this.btnAddScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddScript.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAddScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddScript.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnAddScript.Location = new System.Drawing.Point(1, 47);
            this.btnAddScript.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddScript.Name = "btnAddScript";
            this.btnAddScript.Size = new System.Drawing.Size(42, 35);
            this.btnAddScript.TabIndex = 0;
            this.btnAddScript.Text = "+";
            this.btnAddScript.UseVisualStyleBackColor = true;
            this.btnAddScript.Click += new System.EventHandler(this.btnAddScript_Click);
            // 
            // btnDelSrcipt
            // 
            this.btnDelSrcipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelSrcipt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelSrcipt.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnDelSrcipt.Location = new System.Drawing.Point(45, 47);
            this.btnDelSrcipt.Margin = new System.Windows.Forms.Padding(1);
            this.btnDelSrcipt.Name = "btnDelSrcipt";
            this.btnDelSrcipt.Size = new System.Drawing.Size(42, 35);
            this.btnDelSrcipt.TabIndex = 1;
            this.btnDelSrcipt.Text = "-";
            this.btnDelSrcipt.UseVisualStyleBackColor = true;
            this.btnDelSrcipt.Click += new System.EventHandler(this.btnDelScript_Click);
            // 
            // listboxScript
            // 
            this.listboxScript.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlScript.SetColumnSpan(this.listboxScript, 3);
            this.listboxScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listboxScript.FormattingEnabled = true;
            this.listboxScript.Items.AddRange(new object[] {
            "test"});
            this.listboxScript.Location = new System.Drawing.Point(3, 86);
            this.listboxScript.Name = "listboxScript";
            this.listboxScript.Size = new System.Drawing.Size(126, 260);
            this.listboxScript.TabIndex = 8;
            this.listboxScript.SelectedIndexChanged += new System.EventHandler(this.listboxScript_SelectedIndexChanged);
            // 
            // txtScriptName
            // 
            this.txtScriptName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlScript.SetColumnSpan(this.txtScriptName, 3);
            this.txtScriptName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScriptName.Location = new System.Drawing.Point(1, 1);
            this.txtScriptName.Margin = new System.Windows.Forms.Padding(1);
            this.txtScriptName.MaxLength = 35;
            this.txtScriptName.MinimumSize = new System.Drawing.Size(30, 20);
            this.txtScriptName.Name = "txtScriptName";
            this.txtScriptName.Size = new System.Drawing.Size(130, 20);
            this.txtScriptName.TabIndex = 14;
            // 
            // pnlAction
            // 
            this.pnlAction.BackColor = System.Drawing.SystemColors.Control;
            this.pnlAction.ColumnCount = 5;
            this.pnlAction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlAction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlAction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlAction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlAction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlAction.Controls.Add(this.btnDelAction, 1, 2);
            this.pnlAction.Controls.Add(this.btnAddAction, 0, 2);
            this.pnlAction.Controls.Add(this.cmbActionType, 0, 0);
            this.pnlAction.Controls.Add(this.cmbActionProperties, 0, 1);
            this.pnlAction.Controls.Add(this.treeViewAction, 0, 3);
            this.pnlAction.Controls.Add(this.btnPlayScriptAction, 4, 2);
            this.pnlAction.Controls.Add(this.btnActionDown, 3, 2);
            this.pnlAction.Controls.Add(this.btnActionUp, 2, 2);
            this.pnlAction.Controls.Add(this.propAction, 2, 3);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAction.Enabled = false;
            this.pnlAction.Location = new System.Drawing.Point(132, 0);
            this.pnlAction.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.RowCount = 5;
            this.pnlAction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.pnlAction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.pnlAction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.pnlAction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlAction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlAction.Size = new System.Drawing.Size(220, 305);
            this.pnlAction.TabIndex = 0;
            // 
            // btnDelAction
            // 
            this.btnDelAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelAction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelAction.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelAction.Location = new System.Drawing.Point(47, 46);
            this.btnDelAction.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnDelAction.Name = "btnDelAction";
            this.btnDelAction.Size = new System.Drawing.Size(38, 34);
            this.btnDelAction.TabIndex = 16;
            this.btnDelAction.Text = "-";
            this.btnDelAction.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelAction.UseVisualStyleBackColor = true;
            this.btnDelAction.Click += new System.EventHandler(this.btnDelAction_Click);
            // 
            // btnAddAction
            // 
            this.btnAddAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddAction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddAction.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAction.Location = new System.Drawing.Point(3, 46);
            this.btnAddAction.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnAddAction.Name = "btnAddAction";
            this.btnAddAction.Size = new System.Drawing.Size(38, 34);
            this.btnAddAction.TabIndex = 15;
            this.btnAddAction.Text = "+";
            this.btnAddAction.UseVisualStyleBackColor = true;
            this.btnAddAction.Click += new System.EventHandler(this.btnAddAction_Click);
            // 
            // treeViewAction
            // 
            this.treeViewAction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlAction.SetColumnSpan(this.treeViewAction, 5);
            this.treeViewAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAction.HideSelection = false;
            this.treeViewAction.Location = new System.Drawing.Point(2, 86);
            this.treeViewAction.Margin = new System.Windows.Forms.Padding(2, 3, 2, 2);
            this.treeViewAction.Name = "treeViewAction";
            this.treeViewAction.Size = new System.Drawing.Size(216, 106);
            this.treeViewAction.TabIndex = 13;
            this.treeViewAction.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewAction_AfterSelect);
            // 
            // btnPlayScriptAction
            // 
            this.btnPlayScriptAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPlayScriptAction.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnPlayScriptAction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPlayScriptAction.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayScriptAction.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlayScriptAction.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayScriptAction.Image")));
            this.btnPlayScriptAction.Location = new System.Drawing.Point(179, 49);
            this.btnPlayScriptAction.Name = "btnPlayScriptAction";
            this.btnPlayScriptAction.Size = new System.Drawing.Size(38, 31);
            this.btnPlayScriptAction.TabIndex = 12;
            this.btnPlayScriptAction.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPlayScriptAction.UseVisualStyleBackColor = true;
            this.btnPlayScriptAction.Click += new System.EventHandler(this.btnPlayScriptAction_Click);
            // 
            // btnActionDown
            // 
            this.btnActionDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnActionDown.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnActionDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActionDown.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActionDown.Location = new System.Drawing.Point(135, 46);
            this.btnActionDown.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnActionDown.Name = "btnActionDown";
            this.btnActionDown.Size = new System.Drawing.Size(38, 34);
            this.btnActionDown.TabIndex = 8;
            this.btnActionDown.Text = "↓";
            this.btnActionDown.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnActionDown.UseVisualStyleBackColor = true;
            this.btnActionDown.Click += new System.EventHandler(this.btnActionDown_Click);
            // 
            // btnActionUp
            // 
            this.btnActionUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnActionUp.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnActionUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActionUp.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActionUp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnActionUp.Location = new System.Drawing.Point(91, 46);
            this.btnActionUp.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnActionUp.Name = "btnActionUp";
            this.btnActionUp.Size = new System.Drawing.Size(38, 34);
            this.btnActionUp.TabIndex = 9;
            this.btnActionUp.Text = "↑";
            this.btnActionUp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnActionUp.UseVisualStyleBackColor = true;
            this.btnActionUp.Click += new System.EventHandler(this.btnActionUp_Click);
            // 
            // propAction
            // 
            this.pnlAction.SetColumnSpan(this.propAction, 5);
            this.propAction.CommandsVisibleIfAvailable = false;
            this.propAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propAction.HelpVisible = false;
            this.propAction.LargeButtons = true;
            this.propAction.LineColor = System.Drawing.SystemColors.ControlDarkDark;
            this.propAction.Location = new System.Drawing.Point(2, 194);
            this.propAction.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.propAction.Name = "propAction";
            this.propAction.Size = new System.Drawing.Size(216, 111);
            this.propAction.TabIndex = 17;
            this.propAction.ToolbarVisible = false;
            this.propAction.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propAction_PropertyValueChanged);
            // 
            // curveControl
            // 
            this.curveControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curveControl.Location = new System.Drawing.Point(355, 3);
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
            this.pnlActionEvent.Location = new System.Drawing.Point(621, 3);
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
            // cmbActionType
            // 
            this.cmbActionType.BackColor = System.Drawing.SystemColors.Control;
            this.pnlAction.SetColumnSpan(this.cmbActionType, 5);
            this.cmbActionType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbActionType.FormattingEnabled = true;
            this.cmbActionType.Items.AddRange(new object[] {
            "Courbe",
            "Evènement",
            "Son"});
            this.cmbActionType.Location = new System.Drawing.Point(3, 0);
            this.cmbActionType.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.Size = new System.Drawing.Size(214, 24);
            this.cmbActionType.TabIndex = 11;
            this.cmbActionType.SelectedIndexChanged += new System.EventHandler(this.cmbActionType_SelectedIndexChanged);
            // 
            // cmbActionProperties
            // 
            this.cmbActionProperties.BackColor = System.Drawing.SystemColors.Control;
            this.pnlAction.SetColumnSpan(this.cmbActionProperties, 5);
            this.cmbActionProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbActionProperties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionProperties.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbActionProperties.FormattingEnabled = true;
            this.cmbActionProperties.ItemHeight = 16;
            this.cmbActionProperties.Items.AddRange(new object[] {
            "Position",
            "Angle",
            "Color"});
            this.cmbActionProperties.Location = new System.Drawing.Point(3, 23);
            this.cmbActionProperties.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cmbActionProperties.Name = "cmbActionProperties";
            this.cmbActionProperties.Size = new System.Drawing.Size(214, 24);
            this.cmbActionProperties.TabIndex = 14;
            // 
            // actionSoundControl
            // 
            this.actionSoundControl.ActionSound = null;
            this.actionSoundControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionSoundControl.Location = new System.Drawing.Point(728, 3);
            this.actionSoundControl.Name = "actionSoundControl";
            this.actionSoundControl.Size = new System.Drawing.Size(367, 299);
            this.actionSoundControl.TabIndex = 15;
            this.actionSoundControl.Visible = false;
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
            this.pnlScript.ResumeLayout(false);
            this.pnlScript.PerformLayout();
            this.pnlAction.ResumeLayout(false);
            this.pnlActionEvent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private Xna.Tools.CurveControl curveControl;
        private System.Windows.Forms.TableLayoutPanel pnlScript;
        private System.Windows.Forms.Button btnPlayScript;
        private System.Windows.Forms.Button btnAddScript;
        private System.Windows.Forms.Button btnDelSrcipt;
        private System.Windows.Forms.TableLayoutPanel pnlAction;
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
        private System.Windows.Forms.TextBox txtScriptName;
    }
}
