﻿namespace Edit2D.ScriptControl
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
            this.tabScriptAction = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPlayScript = new System.Windows.Forms.Button();
            this.btnAddScript = new System.Windows.Forms.Button();
            this.btnDelSrcipt = new System.Windows.Forms.Button();
            this.listboxScript = new System.Windows.Forms.ListBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDelAction = new System.Windows.Forms.Button();
            this.btnAddAction = new System.Windows.Forms.Button();
            this.cmbActionType = new System.Windows.Forms.ComboBox();
            this.cmbActionProperties = new System.Windows.Forms.ComboBox();
            this.treeViewAction = new System.Windows.Forms.TreeView();
            this.btnPlayScriptAction = new System.Windows.Forms.Button();
            this.btnActionDown = new System.Windows.Forms.Button();
            this.btnActionUp = new System.Windows.Forms.Button();
            this.propAction = new System.Windows.Forms.PropertyGrid();
            this.curveControl = new Xna.Tools.CurveControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlActionEventLines = new System.Windows.Forms.Panel();
            this.actionSoundControl = new Edit2D.ScriptControl.ActionSoundControl();
            this.pnlMain.SuspendLayout();
            this.tabScriptAction.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 4;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.44056F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.55944F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 266F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.pnlMain.Controls.Add(this.tabScriptAction, 0, 0);
            this.pnlMain.Controls.Add(this.curveControl, 2, 0);
            this.pnlMain.Controls.Add(this.panel2, 1, 0);
            this.pnlMain.Controls.Add(this.actionSoundControl, 3, 0);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 1;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlMain.Size = new System.Drawing.Size(696, 305);
            this.pnlMain.TabIndex = 0;
            // 
            // tabScriptAction
            // 
            this.tabScriptAction.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabScriptAction.Controls.Add(this.tabPage5);
            this.tabScriptAction.Controls.Add(this.tabPage6);
            this.tabScriptAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabScriptAction.Location = new System.Drawing.Point(0, 0);
            this.tabScriptAction.Margin = new System.Windows.Forms.Padding(0);
            this.tabScriptAction.Name = "tabScriptAction";
            this.tabScriptAction.SelectedIndex = 0;
            this.tabScriptAction.Size = new System.Drawing.Size(235, 305);
            this.tabScriptAction.TabIndex = 13;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.tableLayoutPanel2);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(227, 276);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Script";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel2.Controls.Add(this.btnPlayScript, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAddScript, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDelSrcipt, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.listboxScript, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(227, 276);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // btnPlayScript
            // 
            this.btnPlayScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPlayScript.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnPlayScript.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPlayScript.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayScript.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlayScript.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayScript.Image")));
            this.btnPlayScript.Location = new System.Drawing.Point(147, 3);
            this.btnPlayScript.Name = "btnPlayScript";
            this.btnPlayScript.Size = new System.Drawing.Size(77, 26);
            this.btnPlayScript.TabIndex = 13;
            this.btnPlayScript.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPlayScript.UseVisualStyleBackColor = true;
            this.btnPlayScript.Click += new System.EventHandler(this.btnPlayScript_Click);
            // 
            // btnAddScript
            // 
            this.btnAddScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddScript.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddScript.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnAddScript.Location = new System.Drawing.Point(3, 3);
            this.btnAddScript.Name = "btnAddScript";
            this.btnAddScript.Size = new System.Drawing.Size(45, 26);
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
            this.btnDelSrcipt.Location = new System.Drawing.Point(54, 3);
            this.btnDelSrcipt.Name = "btnDelSrcipt";
            this.btnDelSrcipt.Size = new System.Drawing.Size(57, 26);
            this.btnDelSrcipt.TabIndex = 1;
            this.btnDelSrcipt.Text = "-";
            this.btnDelSrcipt.UseVisualStyleBackColor = true;
            this.btnDelSrcipt.Click += new System.EventHandler(this.btnDelScript_Click);
            // 
            // listboxScript
            // 
            this.listboxScript.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.listboxScript, 4);
            this.listboxScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listboxScript.FormattingEnabled = true;
            this.listboxScript.Location = new System.Drawing.Point(3, 35);
            this.listboxScript.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.listboxScript.Name = "listboxScript";
            this.listboxScript.Size = new System.Drawing.Size(221, 236);
            this.listboxScript.TabIndex = 8;
            this.listboxScript.SelectedIndexChanged += new System.EventHandler(this.listboxScript_SelectedIndexChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.tableLayoutPanel3);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(227, 276);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Action";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.btnDelAction, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnAddAction, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmbActionType, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.cmbActionProperties, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.treeViewAction, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.btnPlayScriptAction, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnActionDown, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnActionUp, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.propAction, 2, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(221, 270);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // btnDelAction
            // 
            this.btnDelAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelAction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelAction.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelAction.Location = new System.Drawing.Point(58, 0);
            this.btnDelAction.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnDelAction.Name = "btnDelAction";
            this.btnDelAction.Size = new System.Drawing.Size(49, 32);
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
            this.btnAddAction.Location = new System.Drawing.Point(3, 0);
            this.btnAddAction.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnAddAction.Name = "btnAddAction";
            this.btnAddAction.Size = new System.Drawing.Size(49, 32);
            this.btnAddAction.TabIndex = 15;
            this.btnAddAction.Text = "+";
            this.btnAddAction.UseVisualStyleBackColor = true;
            this.btnAddAction.Click += new System.EventHandler(this.btnAddAction_Click);
            // 
            // cmbActionType
            // 
            this.cmbActionType.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.SetColumnSpan(this.cmbActionType, 3);
            this.cmbActionType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbActionType.FormattingEnabled = true;
            this.cmbActionType.Items.AddRange(new object[] {
            "Courbe",
            "Evènement",
            "Son"});
            this.cmbActionType.Location = new System.Drawing.Point(3, 35);
            this.cmbActionType.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.Size = new System.Drawing.Size(159, 21);
            this.cmbActionType.TabIndex = 11;
            this.cmbActionType.SelectedIndexChanged += new System.EventHandler(this.cmbActionType_SelectedIndexChanged);
            // 
            // cmbActionProperties
            // 
            this.cmbActionProperties.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.SetColumnSpan(this.cmbActionProperties, 3);
            this.cmbActionProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbActionProperties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionProperties.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbActionProperties.FormattingEnabled = true;
            this.cmbActionProperties.ItemHeight = 13;
            this.cmbActionProperties.Items.AddRange(new object[] {
            "Position",
            "Angle",
            "Color"});
            this.cmbActionProperties.Location = new System.Drawing.Point(3, 62);
            this.cmbActionProperties.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cmbActionProperties.Name = "cmbActionProperties";
            this.cmbActionProperties.Size = new System.Drawing.Size(159, 21);
            this.cmbActionProperties.TabIndex = 14;
            // 
            // treeViewAction
            // 
            this.treeViewAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel3.SetColumnSpan(this.treeViewAction, 4);
            this.treeViewAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAction.HideSelection = false;
            this.treeViewAction.Location = new System.Drawing.Point(2, 87);
            this.treeViewAction.Margin = new System.Windows.Forms.Padding(2, 3, 2, 2);
            this.treeViewAction.Name = "treeViewAction";
            this.treeViewAction.Size = new System.Drawing.Size(217, 88);
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
            this.btnPlayScriptAction.Location = new System.Drawing.Point(168, 38);
            this.btnPlayScriptAction.Name = "btnPlayScriptAction";
            this.tableLayoutPanel3.SetRowSpan(this.btnPlayScriptAction, 2);
            this.btnPlayScriptAction.Size = new System.Drawing.Size(50, 43);
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
            this.btnActionDown.Location = new System.Drawing.Point(168, 0);
            this.btnActionDown.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnActionDown.Name = "btnActionDown";
            this.btnActionDown.Size = new System.Drawing.Size(50, 32);
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
            this.btnActionUp.Location = new System.Drawing.Point(113, 0);
            this.btnActionUp.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnActionUp.Name = "btnActionUp";
            this.btnActionUp.Size = new System.Drawing.Size(49, 32);
            this.btnActionUp.TabIndex = 9;
            this.btnActionUp.Text = "↑";
            this.btnActionUp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnActionUp.UseVisualStyleBackColor = true;
            this.btnActionUp.Click += new System.EventHandler(this.btnActionUp_Click);
            // 
            // propAction
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.propAction, 4);
            this.propAction.CommandsVisibleIfAvailable = false;
            this.propAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propAction.HelpVisible = false;
            this.propAction.Location = new System.Drawing.Point(2, 177);
            this.propAction.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.propAction.Name = "propAction";
            this.propAction.Size = new System.Drawing.Size(217, 93);
            this.propAction.TabIndex = 17;
            this.propAction.ToolbarVisible = false;
            this.propAction.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propAction_PropertyValueChanged);
            // 
            // curveControl
            // 
            this.curveControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curveControl.Location = new System.Drawing.Point(398, 3);
            this.curveControl.Name = "curveControl";
            this.curveControl.Size = new System.Drawing.Size(260, 299);
            this.curveControl.TabIndex = 10;
            this.curveControl.TimeLine = 0;
            this.curveControl.CurveChange += new Xna.Tools.CurveControl.CurveChangeEventHandler(this.curveControl_CurveChange);
            this.curveControl.TimeLineChange += new Xna.Tools.CurveControl.TimeLineChangeEventHandler(this.curveControl_TimeLineChange);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.pnlActionEventLines);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(238, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(154, 299);
            this.panel2.TabIndex = 14;
            // 
            // pnlActionEventLines
            // 
            this.pnlActionEventLines.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActionEventLines.Location = new System.Drawing.Point(0, 0);
            this.pnlActionEventLines.Name = "pnlActionEventLines";
            this.pnlActionEventLines.Size = new System.Drawing.Size(154, 100);
            this.pnlActionEventLines.TabIndex = 0;
            // 
            // actionSoundControl
            // 
            this.actionSoundControl.ActionSound = null;
            this.actionSoundControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionSoundControl.Location = new System.Drawing.Point(664, 3);
            this.actionSoundControl.Name = "actionSoundControl";
            this.actionSoundControl.Size = new System.Drawing.Size(29, 299);
            this.actionSoundControl.TabIndex = 15;
            // 
            // ScriptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "ScriptControl";
            this.Size = new System.Drawing.Size(696, 305);
            this.Resize += new System.EventHandler(this.ScriptControl_Resize);
            this.pnlMain.ResumeLayout(false);
            this.tabScriptAction.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private Xna.Tools.CurveControl curveControl;
        private System.Windows.Forms.TabControl tabScriptAction;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnPlayScript;
        private System.Windows.Forms.Button btnAddScript;
        private System.Windows.Forms.Button btnDelSrcipt;
        private System.Windows.Forms.ListBox listboxScript;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TreeView treeViewAction;
        private System.Windows.Forms.Button btnPlayScriptAction;
        private System.Windows.Forms.Button btnActionDown;
        private System.Windows.Forms.Button btnActionUp;
        private System.Windows.Forms.ComboBox cmbActionType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlActionEventLines;
        private System.Windows.Forms.ComboBox cmbActionProperties;
        private System.Windows.Forms.Button btnDelAction;
        private System.Windows.Forms.Button btnAddAction;
        private System.Windows.Forms.PropertyGrid propAction;
        private ActionSoundControl actionSoundControl;
    }
}