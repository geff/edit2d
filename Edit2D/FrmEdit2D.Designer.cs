using WinFormsContentLoading;
namespace Edit2D
{
    partial class FrmEdit2D
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEdit2D));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusMouse = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlMain = new System.Windows.Forms.SplitContainer();
            this.pnlViewerModes = new System.Windows.Forms.SplitContainer();
            this.pnlModes = new System.Windows.Forms.TableLayoutPanel();
            this.pnlButtonsModes = new System.Windows.Forms.FlowLayoutPanel();
            this.btnScriptModeBar = new System.Windows.Forms.RadioButton();
            this.btnTriggerModeBar = new System.Windows.Forms.RadioButton();
            this.btnParticleSystemModeBar = new System.Windows.Forms.RadioButton();
            this.pnlRight = new System.Windows.Forms.SplitContainer();
            this.pnlRightBarMode = new System.Windows.Forms.FlowLayoutPanel();
            this.optRightBarEntities = new System.Windows.Forms.RadioButton();
            this.optRightBarSpring = new System.Windows.Forms.RadioButton();
            this.optRightBarJoint = new System.Windows.Forms.RadioButton();
            this.optRightBarProperties = new System.Windows.Forms.RadioButton();
            this.pnlEntitys = new System.Windows.Forms.Panel();
            this.listView = new System.Windows.Forms.ListView();
            this.btnDelEntity = new System.Windows.Forms.Button();
            this.btnAddEntity = new System.Windows.Forms.Button();
            this.pnlSpring = new System.Windows.Forms.Panel();
            this.btnFixedAngleSpring = new System.Windows.Forms.Button();
            this.btnLinearSpring = new System.Windows.Forms.Button();
            this.btnAngleSpring = new System.Windows.Forms.Button();
            this.btnFixedLinearSpring = new System.Windows.Forms.Button();
            this.pnlJoint = new System.Windows.Forms.Panel();
            this.btnFixedRevoluteJoint = new System.Windows.Forms.Button();
            this.btnRevoluteJoint = new System.Windows.Forms.Button();
            this.btnPinJoint = new System.Windows.Forms.Button();
            this.pnlRightEntities = new System.Windows.Forms.TableLayoutPanel();
            this.btnDownEntity = new System.Windows.Forms.Button();
            this.btnUpEntity = new System.Windows.Forms.Button();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPlay = new System.Windows.Forms.ToolStripButton();
            this.btnPause = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRecObjectStatus = new System.Windows.Forms.ToolStripButton();
            this.btnRecObjectStatusLoop = new System.Windows.Forms.ToolStripButton();
            this.txtRecTime = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowDebugMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMove = new System.Windows.Forms.ToolStripButton();
            this.btnResize = new System.Windows.Forms.ToolStripButton();
            this.btnRotate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPinStatic = new System.Windows.Forms.ToolStripButton();
            this.btnColisionable = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnResetEntityPhysic = new System.Windows.Forms.ToolStripButton();
            this.btnCursorToEntityCenter = new System.Windows.Forms.ToolStripButton();
            this.btnSetCenterEntity = new System.Windows.Forms.ToolStripButton();
            this.btnGameClickableOnPlay = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOrderUp = new System.Windows.Forms.ToolStripButton();
            this.btnOrderDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxEntityName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPanelBottom = new System.Windows.Forms.ToolStripButton();
            this.btnPanelRight = new System.Windows.Forms.ToolStripButton();
            this.modelViewerControl = new WinFormsContentLoading.ModelViewerControl();
            this.triggerControl = new Edit2D.TriggerControl.TriggerControl();
            this.particleControl = new Edit2D.ParticleControl.ParticleControl();
            this.scriptControl = new Edit2D.ScriptControl.ScriptControl();
            this.propertyGrid = new Edit2D.UC.PropertyGridLocal();
            this.treeView = new Edit2D.UC.TreeViewLocal();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.pnlMain.Panel1.SuspendLayout();
            this.pnlMain.Panel2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlViewerModes.Panel1.SuspendLayout();
            this.pnlViewerModes.Panel2.SuspendLayout();
            this.pnlViewerModes.SuspendLayout();
            this.pnlModes.SuspendLayout();
            this.pnlButtonsModes.SuspendLayout();
            this.pnlRight.Panel1.SuspendLayout();
            this.pnlRight.Panel2.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlRightBarMode.SuspendLayout();
            this.pnlEntitys.SuspendLayout();
            this.pnlSpring.SuspendLayout();
            this.pnlJoint.SuspendLayout();
            this.pnlRightEntities.SuspendLayout();
            this.toolStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.AutoScroll = true;
            this.toolStripContainer.ContentPanel.Controls.Add(this.pnlMain);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(913, 671);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(913, 725);
            this.toolStripContainer.TabIndex = 5;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.BackColor = System.Drawing.Color.Black;
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStripMenu);
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusMouse});
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(913, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusMouse
            // 
            this.toolStripStatusMouse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusMouse.Margin = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.toolStripStatusMouse.Name = "toolStripStatusMouse";
            this.toolStripStatusMouse.Size = new System.Drawing.Size(131, 19);
            this.toolStripStatusMouse.Text = "Mouse.X : 0 ; Mouse.Y : 0";
            this.toolStripStatusMouse.ToolTipText = "Position de la souris";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            // 
            // pnlMain.Panel1
            // 
            this.pnlMain.Panel1.Controls.Add(this.pnlViewerModes);
            // 
            // pnlMain.Panel2
            // 
            this.pnlMain.Panel2.Controls.Add(this.pnlRight);
            this.pnlMain.Size = new System.Drawing.Size(913, 671);
            this.pnlMain.SplitterDistance = 591;
            this.pnlMain.SplitterWidth = 6;
            this.pnlMain.TabIndex = 5;
            this.pnlMain.Tag = "BG1";
            // 
            // pnlViewerModes
            // 
            this.pnlViewerModes.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlViewerModes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewerModes.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.pnlViewerModes.Location = new System.Drawing.Point(0, 0);
            this.pnlViewerModes.Name = "pnlViewerModes";
            this.pnlViewerModes.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // pnlViewerModes.Panel1
            // 
            this.pnlViewerModes.Panel1.Controls.Add(this.modelViewerControl);
            // 
            // pnlViewerModes.Panel2
            // 
            this.pnlViewerModes.Panel2.Controls.Add(this.pnlModes);
            this.pnlViewerModes.Size = new System.Drawing.Size(591, 671);
            this.pnlViewerModes.SplitterDistance = 315;
            this.pnlViewerModes.SplitterWidth = 6;
            this.pnlViewerModes.TabIndex = 0;
            // 
            // pnlModes
            // 
            this.pnlModes.ColumnCount = 1;
            this.pnlModes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlModes.Controls.Add(this.pnlButtonsModes, 0, 0);
            this.pnlModes.Controls.Add(this.triggerControl, 0, 3);
            this.pnlModes.Controls.Add(this.particleControl, 0, 2);
            this.pnlModes.Controls.Add(this.scriptControl, 0, 1);
            this.pnlModes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlModes.Location = new System.Drawing.Point(0, 0);
            this.pnlModes.Name = "pnlModes";
            this.pnlModes.RowCount = 2;
            this.pnlModes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.pnlModes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlModes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.pnlModes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.pnlModes.Size = new System.Drawing.Size(591, 350);
            this.pnlModes.TabIndex = 0;
            // 
            // pnlButtonsModes
            // 
            this.pnlButtonsModes.Controls.Add(this.btnScriptModeBar);
            this.pnlButtonsModes.Controls.Add(this.btnTriggerModeBar);
            this.pnlButtonsModes.Controls.Add(this.btnParticleSystemModeBar);
            this.pnlButtonsModes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtonsModes.Location = new System.Drawing.Point(0, 0);
            this.pnlButtonsModes.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButtonsModes.Name = "pnlButtonsModes";
            this.pnlButtonsModes.Size = new System.Drawing.Size(871, 33);
            this.pnlButtonsModes.TabIndex = 15;
            // 
            // btnScriptModeBar
            // 
            this.btnScriptModeBar.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnScriptModeBar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnScriptModeBar.FlatAppearance.BorderSize = 2;
            this.btnScriptModeBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScriptModeBar.Image = global::Edit2D.Properties.Resources.icon_Script;
            this.btnScriptModeBar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScriptModeBar.Location = new System.Drawing.Point(10, 2);
            this.btnScriptModeBar.Margin = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.btnScriptModeBar.Name = "btnScriptModeBar";
            this.btnScriptModeBar.Size = new System.Drawing.Size(140, 29);
            this.btnScriptModeBar.TabIndex = 3;
            this.btnScriptModeBar.Text = "Script && Action";
            this.btnScriptModeBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnScriptModeBar.UseCompatibleTextRendering = true;
            this.btnScriptModeBar.UseVisualStyleBackColor = true;
            this.btnScriptModeBar.CheckedChanged += new System.EventHandler(this.btnScriptModeBar_CheckedChanged);
            // 
            // btnTriggerModeBar
            // 
            this.btnTriggerModeBar.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnTriggerModeBar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnTriggerModeBar.FlatAppearance.BorderSize = 2;
            this.btnTriggerModeBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTriggerModeBar.Image = global::Edit2D.Properties.Resources.icon_Trigger;
            this.btnTriggerModeBar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTriggerModeBar.Location = new System.Drawing.Point(151, 2);
            this.btnTriggerModeBar.Margin = new System.Windows.Forms.Padding(1, 2, 0, 0);
            this.btnTriggerModeBar.Name = "btnTriggerModeBar";
            this.btnTriggerModeBar.Size = new System.Drawing.Size(140, 29);
            this.btnTriggerModeBar.TabIndex = 4;
            this.btnTriggerModeBar.Text = "Déclencheur";
            this.btnTriggerModeBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnTriggerModeBar.UseVisualStyleBackColor = true;
            this.btnTriggerModeBar.CheckedChanged += new System.EventHandler(this.btnTriggerModeBar_CheckedChanged);
            // 
            // btnParticleSystemModeBar
            // 
            this.btnParticleSystemModeBar.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnParticleSystemModeBar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnParticleSystemModeBar.FlatAppearance.BorderSize = 2;
            this.btnParticleSystemModeBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnParticleSystemModeBar.Image = global::Edit2D.Properties.Resources.icon_ParticleSystem;
            this.btnParticleSystemModeBar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnParticleSystemModeBar.Location = new System.Drawing.Point(292, 2);
            this.btnParticleSystemModeBar.Margin = new System.Windows.Forms.Padding(1, 2, 0, 0);
            this.btnParticleSystemModeBar.Name = "btnParticleSystemModeBar";
            this.btnParticleSystemModeBar.Size = new System.Drawing.Size(140, 29);
            this.btnParticleSystemModeBar.TabIndex = 5;
            this.btnParticleSystemModeBar.Text = "Système de particules";
            this.btnParticleSystemModeBar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnParticleSystemModeBar.UseVisualStyleBackColor = true;
            this.btnParticleSystemModeBar.CheckedChanged += new System.EventHandler(this.btnParticleSystemModeBar_CheckedChanged);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.SystemColors.Control;
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // pnlRight.Panel1
            // 
            this.pnlRight.Panel1.Controls.Add(this.propertyGrid);
            this.pnlRight.Panel1.Controls.Add(this.pnlRightBarMode);
            this.pnlRight.Panel1.Controls.Add(this.pnlEntitys);
            this.pnlRight.Panel1.Controls.Add(this.pnlSpring);
            this.pnlRight.Panel1.Controls.Add(this.pnlJoint);
            // 
            // pnlRight.Panel2
            // 
            this.pnlRight.Panel2.Controls.Add(this.pnlRightEntities);
            this.pnlRight.Size = new System.Drawing.Size(316, 671);
            this.pnlRight.SplitterDistance = 416;
            this.pnlRight.SplitterWidth = 6;
            this.pnlRight.TabIndex = 0;
            this.pnlRight.Tag = "B";
            // 
            // pnlRightBarMode
            // 
            this.pnlRightBarMode.Controls.Add(this.optRightBarEntities);
            this.pnlRightBarMode.Controls.Add(this.optRightBarSpring);
            this.pnlRightBarMode.Controls.Add(this.optRightBarJoint);
            this.pnlRightBarMode.Controls.Add(this.optRightBarProperties);
            this.pnlRightBarMode.Location = new System.Drawing.Point(3, 3);
            this.pnlRightBarMode.Name = "pnlRightBarMode";
            this.pnlRightBarMode.Size = new System.Drawing.Size(291, 34);
            this.pnlRightBarMode.TabIndex = 9;
            // 
            // optRightBarEntities
            // 
            this.optRightBarEntities.Appearance = System.Windows.Forms.Appearance.Button;
            this.optRightBarEntities.Checked = true;
            this.optRightBarEntities.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.optRightBarEntities.FlatAppearance.BorderSize = 2;
            this.optRightBarEntities.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optRightBarEntities.Location = new System.Drawing.Point(0, 2);
            this.optRightBarEntities.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.optRightBarEntities.Name = "optRightBarEntities";
            this.optRightBarEntities.Size = new System.Drawing.Size(70, 29);
            this.optRightBarEntities.TabIndex = 6;
            this.optRightBarEntities.TabStop = true;
            this.optRightBarEntities.Text = "Entitys";
            this.optRightBarEntities.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optRightBarEntities.UseCompatibleTextRendering = true;
            this.optRightBarEntities.UseVisualStyleBackColor = true;
            this.optRightBarEntities.CheckedChanged += new System.EventHandler(this.optRightBarEntities_CheckedChanged);
            // 
            // optRightBarSpring
            // 
            this.optRightBarSpring.Appearance = System.Windows.Forms.Appearance.Button;
            this.optRightBarSpring.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.optRightBarSpring.FlatAppearance.BorderSize = 2;
            this.optRightBarSpring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optRightBarSpring.Location = new System.Drawing.Point(71, 2);
            this.optRightBarSpring.Margin = new System.Windows.Forms.Padding(1, 2, 0, 0);
            this.optRightBarSpring.Name = "optRightBarSpring";
            this.optRightBarSpring.Size = new System.Drawing.Size(70, 29);
            this.optRightBarSpring.TabIndex = 7;
            this.optRightBarSpring.Text = "Spring";
            this.optRightBarSpring.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optRightBarSpring.UseVisualStyleBackColor = true;
            this.optRightBarSpring.CheckedChanged += new System.EventHandler(this.optRightBarSpring_CheckedChanged);
            // 
            // optRightBarJoint
            // 
            this.optRightBarJoint.Appearance = System.Windows.Forms.Appearance.Button;
            this.optRightBarJoint.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.optRightBarJoint.FlatAppearance.BorderSize = 2;
            this.optRightBarJoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optRightBarJoint.Location = new System.Drawing.Point(142, 2);
            this.optRightBarJoint.Margin = new System.Windows.Forms.Padding(1, 2, 0, 0);
            this.optRightBarJoint.Name = "optRightBarJoint";
            this.optRightBarJoint.Size = new System.Drawing.Size(70, 29);
            this.optRightBarJoint.TabIndex = 8;
            this.optRightBarJoint.Text = "Joint";
            this.optRightBarJoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optRightBarJoint.UseVisualStyleBackColor = true;
            this.optRightBarJoint.CheckedChanged += new System.EventHandler(this.optRightBarJoint_CheckedChanged);
            // 
            // optRightBarProperties
            // 
            this.optRightBarProperties.Appearance = System.Windows.Forms.Appearance.Button;
            this.optRightBarProperties.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.optRightBarProperties.FlatAppearance.BorderSize = 2;
            this.optRightBarProperties.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optRightBarProperties.Location = new System.Drawing.Point(214, 2);
            this.optRightBarProperties.Margin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.optRightBarProperties.Name = "optRightBarProperties";
            this.optRightBarProperties.Size = new System.Drawing.Size(70, 29);
            this.optRightBarProperties.TabIndex = 9;
            this.optRightBarProperties.Text = "Propriétés";
            this.optRightBarProperties.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optRightBarProperties.UseCompatibleTextRendering = true;
            this.optRightBarProperties.UseVisualStyleBackColor = true;
            this.optRightBarProperties.CheckedChanged += new System.EventHandler(this.optRightBarProperties_CheckedChanged);
            // 
            // pnlEntitys
            // 
            this.pnlEntitys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlEntitys.Controls.Add(this.listView);
            this.pnlEntitys.Controls.Add(this.btnDelEntity);
            this.pnlEntitys.Controls.Add(this.btnAddEntity);
            this.pnlEntitys.Location = new System.Drawing.Point(0, 43);
            this.pnlEntitys.Margin = new System.Windows.Forms.Padding(0);
            this.pnlEntitys.Name = "pnlEntitys";
            this.pnlEntitys.Size = new System.Drawing.Size(314, 373);
            this.pnlEntitys.TabIndex = 6;
            // 
            // listView
            // 
            this.listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.BackColor = System.Drawing.Color.Gray;
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(0, 32);
            this.listView.Margin = new System.Windows.Forms.Padding(0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.OwnerDraw = true;
            this.listView.ShowGroups = false;
            this.listView.Size = new System.Drawing.Size(314, 341);
            this.listView.TabIndex = 7;
            this.listView.TileSize = new System.Drawing.Size(177, 80);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Tile;
            this.listView.Resize += new System.EventHandler(this.listView_Resize);
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // btnDelEntity
            // 
            this.btnDelEntity.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelEntity.Location = new System.Drawing.Point(78, 0);
            this.btnDelEntity.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelEntity.Name = "btnDelEntity";
            this.btnDelEntity.Size = new System.Drawing.Size(78, 29);
            this.btnDelEntity.TabIndex = 2;
            this.btnDelEntity.Text = "Supprimer";
            this.btnDelEntity.UseVisualStyleBackColor = true;
            this.btnDelEntity.Click += new System.EventHandler(this.btnDelEntity_Click);
            // 
            // btnAddEntity
            // 
            this.btnAddEntity.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddEntity.Location = new System.Drawing.Point(0, 0);
            this.btnAddEntity.Name = "btnAddEntity";
            this.btnAddEntity.Size = new System.Drawing.Size(78, 29);
            this.btnAddEntity.TabIndex = 1;
            this.btnAddEntity.Text = "Ajouter";
            this.btnAddEntity.UseVisualStyleBackColor = true;
            this.btnAddEntity.Click += new System.EventHandler(this.btnAddEntity_Click);
            // 
            // pnlSpring
            // 
            this.pnlSpring.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSpring.Controls.Add(this.btnFixedAngleSpring);
            this.pnlSpring.Controls.Add(this.btnLinearSpring);
            this.pnlSpring.Controls.Add(this.btnAngleSpring);
            this.pnlSpring.Controls.Add(this.btnFixedLinearSpring);
            this.pnlSpring.Location = new System.Drawing.Point(0, 43);
            this.pnlSpring.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSpring.Name = "pnlSpring";
            this.pnlSpring.Size = new System.Drawing.Size(316, 373);
            this.pnlSpring.TabIndex = 6;
            this.pnlSpring.Visible = false;
            // 
            // btnFixedAngleSpring
            // 
            this.btnFixedAngleSpring.Location = new System.Drawing.Point(3, 90);
            this.btnFixedAngleSpring.Name = "btnFixedAngleSpring";
            this.btnFixedAngleSpring.Size = new System.Drawing.Size(113, 23);
            this.btnFixedAngleSpring.TabIndex = 5;
            this.btnFixedAngleSpring.Text = "FixedAngleSpring";
            this.btnFixedAngleSpring.UseVisualStyleBackColor = true;
            this.btnFixedAngleSpring.Click += new System.EventHandler(this.btnFixedAngleSpring_Click);
            // 
            // btnLinearSpring
            // 
            this.btnLinearSpring.Location = new System.Drawing.Point(3, 32);
            this.btnLinearSpring.Name = "btnLinearSpring";
            this.btnLinearSpring.Size = new System.Drawing.Size(113, 23);
            this.btnLinearSpring.TabIndex = 3;
            this.btnLinearSpring.Text = "LinearSpring";
            this.btnLinearSpring.UseVisualStyleBackColor = true;
            this.btnLinearSpring.Click += new System.EventHandler(this.btnLinearSpring_Click);
            // 
            // btnAngleSpring
            // 
            this.btnAngleSpring.Location = new System.Drawing.Point(3, 61);
            this.btnAngleSpring.Name = "btnAngleSpring";
            this.btnAngleSpring.Size = new System.Drawing.Size(113, 23);
            this.btnAngleSpring.TabIndex = 4;
            this.btnAngleSpring.Text = "AngleSpring";
            this.btnAngleSpring.UseVisualStyleBackColor = true;
            this.btnAngleSpring.Click += new System.EventHandler(this.btnAngleSpring_Click);
            // 
            // btnFixedLinearSpring
            // 
            this.btnFixedLinearSpring.Location = new System.Drawing.Point(3, 3);
            this.btnFixedLinearSpring.Name = "btnFixedLinearSpring";
            this.btnFixedLinearSpring.Size = new System.Drawing.Size(113, 23);
            this.btnFixedLinearSpring.TabIndex = 2;
            this.btnFixedLinearSpring.Text = "FixedLinearSpring";
            this.btnFixedLinearSpring.UseVisualStyleBackColor = true;
            this.btnFixedLinearSpring.Click += new System.EventHandler(this.btnFixedLinearSpring_Click);
            // 
            // pnlJoint
            // 
            this.pnlJoint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlJoint.Controls.Add(this.btnFixedRevoluteJoint);
            this.pnlJoint.Controls.Add(this.btnRevoluteJoint);
            this.pnlJoint.Controls.Add(this.btnPinJoint);
            this.pnlJoint.Location = new System.Drawing.Point(0, 43);
            this.pnlJoint.Margin = new System.Windows.Forms.Padding(0);
            this.pnlJoint.Name = "pnlJoint";
            this.pnlJoint.Size = new System.Drawing.Size(316, 373);
            this.pnlJoint.TabIndex = 7;
            this.pnlJoint.Visible = false;
            // 
            // btnFixedRevoluteJoint
            // 
            this.btnFixedRevoluteJoint.Location = new System.Drawing.Point(3, 32);
            this.btnFixedRevoluteJoint.Name = "btnFixedRevoluteJoint";
            this.btnFixedRevoluteJoint.Size = new System.Drawing.Size(113, 23);
            this.btnFixedRevoluteJoint.TabIndex = 5;
            this.btnFixedRevoluteJoint.Text = "FixedRevoluteJoint";
            this.btnFixedRevoluteJoint.UseVisualStyleBackColor = true;
            this.btnFixedRevoluteJoint.Click += new System.EventHandler(this.btnFixedRevoluteJoint_Click);
            // 
            // btnRevoluteJoint
            // 
            this.btnRevoluteJoint.Location = new System.Drawing.Point(3, 61);
            this.btnRevoluteJoint.Name = "btnRevoluteJoint";
            this.btnRevoluteJoint.Size = new System.Drawing.Size(113, 23);
            this.btnRevoluteJoint.TabIndex = 4;
            this.btnRevoluteJoint.Text = "RevoluteJoint";
            this.btnRevoluteJoint.UseVisualStyleBackColor = true;
            this.btnRevoluteJoint.Click += new System.EventHandler(this.btnRevoluteJoint_Click);
            // 
            // btnPinJoint
            // 
            this.btnPinJoint.Location = new System.Drawing.Point(3, 3);
            this.btnPinJoint.Name = "btnPinJoint";
            this.btnPinJoint.Size = new System.Drawing.Size(113, 23);
            this.btnPinJoint.TabIndex = 3;
            this.btnPinJoint.Text = "PinJoint";
            this.btnPinJoint.UseVisualStyleBackColor = true;
            this.btnPinJoint.Click += new System.EventHandler(this.btnPinJoint_Click);
            // 
            // pnlRightEntities
            // 
            this.pnlRightEntities.ColumnCount = 2;
            this.pnlRightEntities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.67532F));
            this.pnlRightEntities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.32468F));
            this.pnlRightEntities.Controls.Add(this.btnDownEntity, 1, 0);
            this.pnlRightEntities.Controls.Add(this.btnUpEntity, 0, 0);
            this.pnlRightEntities.Controls.Add(this.treeView, 0, 1);
            this.pnlRightEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightEntities.Location = new System.Drawing.Point(0, 0);
            this.pnlRightEntities.Name = "pnlRightEntities";
            this.pnlRightEntities.RowCount = 2;
            this.pnlRightEntities.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.pnlRightEntities.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlRightEntities.Size = new System.Drawing.Size(316, 249);
            this.pnlRightEntities.TabIndex = 0;
            // 
            // btnDownEntity
            // 
            this.btnDownEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownEntity.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDownEntity.FlatAppearance.BorderSize = 0;
            this.btnDownEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownEntity.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownEntity.Location = new System.Drawing.Point(156, 0);
            this.btnDownEntity.Margin = new System.Windows.Forms.Padding(0);
            this.btnDownEntity.Name = "btnDownEntity";
            this.btnDownEntity.Size = new System.Drawing.Size(160, 30);
            this.btnDownEntity.TabIndex = 12;
            this.btnDownEntity.Text = "↓";
            this.btnDownEntity.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDownEntity.UseVisualStyleBackColor = true;
            this.btnDownEntity.Click += new System.EventHandler(this.btnDownEntity_Click);
            // 
            // btnUpEntity
            // 
            this.btnUpEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpEntity.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnUpEntity.FlatAppearance.BorderSize = 0;
            this.btnUpEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpEntity.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpEntity.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUpEntity.Location = new System.Drawing.Point(0, 0);
            this.btnUpEntity.Margin = new System.Windows.Forms.Padding(0);
            this.btnUpEntity.Name = "btnUpEntity";
            this.btnUpEntity.Size = new System.Drawing.Size(156, 30);
            this.btnUpEntity.TabIndex = 11;
            this.btnUpEntity.Text = "↑";
            this.btnUpEntity.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUpEntity.UseVisualStyleBackColor = true;
            this.btnUpEntity.Click += new System.EventHandler(this.btnUpEntity_Click);
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(20, 25);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.toolStripSeparator5,
            this.btnPlay,
            this.btnPause,
            this.btnStop,
            this.toolStripSeparator1,
            this.btnRecObjectStatus,
            this.btnRecObjectStatusLoop,
            this.txtRecTime,
            this.toolStripSeparator7,
            this.btnShowDebugMode,
            this.toolStripSeparator2,
            this.btnMove,
            this.btnResize,
            this.btnRotate,
            this.toolStripSeparator3,
            this.btnPinStatic,
            this.btnColisionable,
            this.toolStripSeparator4,
            this.btnResetEntityPhysic,
            this.btnCursorToEntityCenter,
            this.btnSetCenterEntity,
            this.btnGameClickableOnPlay,
            this.toolStripSeparator6,
            this.btnOrderUp,
            this.btnOrderDown,
            this.toolStripSeparator9,
            this.toolStripLabel1,
            this.toolStripTextBoxEntityName,
            this.toolStripSeparator8,
            this.btnPanelBottom,
            this.btnPanelRight});
            this.toolStripMenu.Location = new System.Drawing.Point(3, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripMenu.Size = new System.Drawing.Size(910, 32);
            this.toolStripMenu.TabIndex = 3;
            this.toolStripMenu.Tag = "B";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 29);
            this.btnNew.Text = "Nouveau";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 29);
            this.btnOpen.Text = "Ouvrir";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 29);
            this.btnSave.Text = "Enregister";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 32);
            // 
            // btnPlay
            // 
            this.btnPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlay.Image = global::Edit2D.Properties.Resources.icon_Play;
            this.btnPlay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(29, 29);
            this.btnPlay.Text = "Jouer";
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPause.Enabled = false;
            this.btnPause.Image = global::Edit2D.Properties.Resources.icon_Pause;
            this.btnPause.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(29, 29);
            this.btnPause.Text = "Pause";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Image = global::Edit2D.Properties.Resources.icon_Stop;
            this.btnStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(29, 29);
            this.btnStop.Text = "toolStripButton1";
            this.btnStop.ToolTipText = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // btnRecObjectStatus
            // 
            this.btnRecObjectStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRecObjectStatus.Image = global::Edit2D.Properties.Resources.icon_Rec;
            this.btnRecObjectStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRecObjectStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecObjectStatus.Name = "btnRecObjectStatus";
            this.btnRecObjectStatus.Size = new System.Drawing.Size(29, 29);
            this.btnRecObjectStatus.Text = "toolStripButton1";
            this.btnRecObjectStatus.ToolTipText = "Enregistre la position, la taille et la rotation de l\'bjet";
            this.btnRecObjectStatus.Click += new System.EventHandler(this.btnRecObjectStatus_Click);
            // 
            // btnRecObjectStatusLoop
            // 
            this.btnRecObjectStatusLoop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRecObjectStatusLoop.Image = global::Edit2D.Properties.Resources.icon_RecLoop;
            this.btnRecObjectStatusLoop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRecObjectStatusLoop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecObjectStatusLoop.Name = "btnRecObjectStatusLoop";
            this.btnRecObjectStatusLoop.Size = new System.Drawing.Size(29, 29);
            this.btnRecObjectStatusLoop.Text = "toolStripButton1";
            this.btnRecObjectStatusLoop.ToolTipText = "Boucler l\'enregistrement";
            this.btnRecObjectStatusLoop.Click += new System.EventHandler(this.btnRecObjectStatusLoop_Click);
            // 
            // txtRecTime
            // 
            this.txtRecTime.Name = "txtRecTime";
            this.txtRecTime.Size = new System.Drawing.Size(40, 32);
            this.txtRecTime.Text = "1000";
            this.txtRecTime.ToolTipText = "Temps entre chaque enregistrement (ms)";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 32);
            // 
            // btnShowDebugMode
            // 
            this.btnShowDebugMode.Checked = true;
            this.btnShowDebugMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowDebugMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowDebugMode.Image = global::Edit2D.Properties.Resources.icon_EditMode;
            this.btnShowDebugMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnShowDebugMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowDebugMode.Name = "btnShowDebugMode";
            this.btnShowDebugMode.Size = new System.Drawing.Size(29, 29);
            this.btnShowDebugMode.Text = "Afficher le mode debug";
            this.btnShowDebugMode.Click += new System.EventHandler(this.btnShowPhysic_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // btnMove
            // 
            this.btnMove.Checked = true;
            this.btnMove.CheckOnClick = true;
            this.btnMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMove.Image = ((System.Drawing.Image)(resources.GetObject("btnMove.Image")));
            this.btnMove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(23, 29);
            this.btnMove.Text = "Déplacement de l\'objet";
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnResize
            // 
            this.btnResize.CheckOnClick = true;
            this.btnResize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnResize.Image = ((System.Drawing.Image)(resources.GetObject("btnResize.Image")));
            this.btnResize.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnResize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(23, 29);
            this.btnResize.Text = "Redimensionnement de l\'objet";
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.CheckOnClick = true;
            this.btnRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRotate.Image = ((System.Drawing.Image)(resources.GetObject("btnRotate.Image")));
            this.btnRotate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(23, 29);
            this.btnRotate.Text = "Rotation de l\'objet";
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // btnPinStatic
            // 
            this.btnPinStatic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPinStatic.Image = ((System.Drawing.Image)(resources.GetObject("btnPinStatic.Image")));
            this.btnPinStatic.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPinStatic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPinStatic.Name = "btnPinStatic";
            this.btnPinStatic.Size = new System.Drawing.Size(23, 29);
            this.btnPinStatic.Text = "Fixe l\'objet";
            this.btnPinStatic.Click += new System.EventHandler(this.btnPinStatic_Click);
            // 
            // btnColisionable
            // 
            this.btnColisionable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColisionable.Image = ((System.Drawing.Image)(resources.GetObject("btnColisionable.Image")));
            this.btnColisionable.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnColisionable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColisionable.Name = "btnColisionable";
            this.btnColisionable.Size = new System.Drawing.Size(23, 29);
            this.btnColisionable.Text = "Activer les collisions";
            this.btnColisionable.Click += new System.EventHandler(this.btnColisionable_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // btnResetEntityPhysic
            // 
            this.btnResetEntityPhysic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnResetEntityPhysic.Image = global::Edit2D.Properties.Resources.icon_ResetPhysic;
            this.btnResetEntityPhysic.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnResetEntityPhysic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResetEntityPhysic.Name = "btnResetEntityPhysic";
            this.btnResetEntityPhysic.Size = new System.Drawing.Size(29, 29);
            this.btnResetEntityPhysic.Text = "Mise à zéro des propriétés physiques de l\'entité";
            this.btnResetEntityPhysic.Click += new System.EventHandler(this.btnResetEntityPhysic_Click);
            // 
            // btnCursorToEntityCenter
            // 
            this.btnCursorToEntityCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCursorToEntityCenter.Image = global::Edit2D.Properties.Resources.icon_CursorToCenter;
            this.btnCursorToEntityCenter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCursorToEntityCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCursorToEntityCenter.Name = "btnCursorToEntityCenter";
            this.btnCursorToEntityCenter.Size = new System.Drawing.Size(29, 29);
            this.btnCursorToEntityCenter.Text = "Cursor to entity center";
            this.btnCursorToEntityCenter.Click += new System.EventHandler(this.btnCursorToEntityCenter_Click);
            // 
            // btnSetCenterEntity
            // 
            this.btnSetCenterEntity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSetCenterEntity.Image = global::Edit2D.Properties.Resources.icon_CenterToCursor;
            this.btnSetCenterEntity.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSetCenterEntity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetCenterEntity.Name = "btnSetCenterEntity";
            this.btnSetCenterEntity.Size = new System.Drawing.Size(29, 29);
            this.btnSetCenterEntity.Text = "Déterminer le centrer de l\'entité";
            this.btnSetCenterEntity.ToolTipText = "Déterminer le centre de l\'entité";
            this.btnSetCenterEntity.Click += new System.EventHandler(this.btnSetCenterEntity_Click);
            // 
            // btnGameClickableOnPlay
            // 
            this.btnGameClickableOnPlay.CheckOnClick = true;
            this.btnGameClickableOnPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGameClickableOnPlay.Image = global::Edit2D.Properties.Resources.icon_EditWhenPlaying;
            this.btnGameClickableOnPlay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGameClickableOnPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGameClickableOnPlay.Name = "btnGameClickableOnPlay";
            this.btnGameClickableOnPlay.Size = new System.Drawing.Size(29, 29);
            this.btnGameClickableOnPlay.Text = "Modifier les éléments lorsque l\'environnement est en cours d\'exécution";
            this.btnGameClickableOnPlay.Click += new System.EventHandler(this.btnGameClickableOnPlay_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 32);
            // 
            // btnOrderUp
            // 
            this.btnOrderUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOrderUp.Image = global::Edit2D.Properties.Resources.icon_OrderUp;
            this.btnOrderUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnOrderUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOrderUp.Name = "btnOrderUp";
            this.btnOrderUp.Size = new System.Drawing.Size(29, 29);
            this.btnOrderUp.Text = "toolStripButton1";
            this.btnOrderUp.ToolTipText = "Entité devant la sélection";
            this.btnOrderUp.Click += new System.EventHandler(this.btnOrderUp_Click);
            // 
            // btnOrderDown
            // 
            this.btnOrderDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOrderDown.Image = global::Edit2D.Properties.Resources.icon_OrderDown;
            this.btnOrderDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnOrderDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOrderDown.Name = "btnOrderDown";
            this.btnOrderDown.Size = new System.Drawing.Size(29, 29);
            this.btnOrderDown.Text = "toolStripButton1";
            this.btnOrderDown.ToolTipText = "Entité derrière la sélection";
            this.btnOrderDown.Click += new System.EventHandler(this.btnOrderDown_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(85, 29);
            this.toolStripLabel1.Text = "Nom de l\'entité :";
            // 
            // toolStripTextBoxEntityName
            // 
            this.toolStripTextBoxEntityName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toolStripTextBoxEntityName.Name = "toolStripTextBoxEntityName";
            this.toolStripTextBoxEntityName.Size = new System.Drawing.Size(150, 32);
            this.toolStripTextBoxEntityName.Validated += new System.EventHandler(this.toolStripTextBoxEntityName_Validated);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 32);
            // 
            // btnPanelBottom
            // 
            this.btnPanelBottom.Checked = true;
            this.btnPanelBottom.CheckOnClick = true;
            this.btnPanelBottom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnPanelBottom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPanelBottom.Image = global::Edit2D.Properties.Resources.icon_PanelBottom;
            this.btnPanelBottom.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPanelBottom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPanelBottom.Name = "btnPanelBottom";
            this.btnPanelBottom.Size = new System.Drawing.Size(29, 29);
            this.btnPanelBottom.Text = "Afficher le visuel physique";
            this.btnPanelBottom.Click += new System.EventHandler(this.btnPanelBottom_Click);
            // 
            // btnPanelRight
            // 
            this.btnPanelRight.Checked = true;
            this.btnPanelRight.CheckOnClick = true;
            this.btnPanelRight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnPanelRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPanelRight.Image = global::Edit2D.Properties.Resources.icon_PanelRight;
            this.btnPanelRight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPanelRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPanelRight.Name = "btnPanelRight";
            this.btnPanelRight.Size = new System.Drawing.Size(29, 29);
            this.btnPanelRight.Text = "Afficher le visuel physique";
            this.btnPanelRight.Click += new System.EventHandler(this.btnPanelRight_Click);
            // 
            // modelViewerControl
            // 
            this.modelViewerControl.BackColor = System.Drawing.Color.Black;
            this.modelViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelViewerControl.Location = new System.Drawing.Point(0, 0);
            this.modelViewerControl.Name = "modelViewerControl";
            this.modelViewerControl.Size = new System.Drawing.Size(591, 315);
            this.modelViewerControl.TabIndex = 2;
            this.modelViewerControl.Text = "modelViewerControl";
            this.modelViewerControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.modelViewerControl_MouseMove);
            this.modelViewerControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.modelViewerControl_MouseDown);
            this.modelViewerControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.modelViewerControl_MouseUp);
            // 
            // triggerControl
            // 
            this.triggerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggerControl.Location = new System.Drawing.Point(3, 353);
            this.triggerControl.Name = "triggerControl";
            this.triggerControl.Repository = null;
            this.triggerControl.Size = new System.Drawing.Size(865, 1);
            this.triggerControl.TabIndex = 6;
            this.triggerControl.Tag = "BG1";
            // 
            // particleControl
            // 
            this.particleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.particleControl.Location = new System.Drawing.Point(3, 353);
            this.particleControl.Name = "particleControl";
            this.particleControl.Repository = null;
            this.particleControl.Size = new System.Drawing.Size(865, 1);
            this.particleControl.TabIndex = 17;
            this.particleControl.Tag = "BG1";
            // 
            // scriptControl
            // 
            this.scriptControl.BackColor = System.Drawing.SystemColors.Control;
            this.scriptControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptControl.Location = new System.Drawing.Point(3, 36);
            this.scriptControl.Name = "scriptControl";
            this.scriptControl.Repository = null;
            this.scriptControl.Size = new System.Drawing.Size(865, 311);
            this.scriptControl.TabIndex = 16;
            this.scriptControl.TimeLineValue = 0;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.BackColor = System.Drawing.Color.Gray;
            this.propertyGrid.Location = new System.Drawing.Point(0, 43);
            this.propertyGrid.Margin = new System.Windows.Forms.Padding(0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(316, 373);
            this.propertyGrid.TabIndex = 8;
            this.propertyGrid.Tag = "B;F1";
            this.propertyGrid.TagLineColor = "BG2";
            this.propertyGrid.Visible = false;
            // 
            // treeView
            // 
            this.treeView.AllowMultipleItemChecked = false;
            this.treeView.AllowUncheckedNode = false;
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlRightEntities.SetColumnSpan(this.treeView, 2);
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.treeView.HideSelection = false;
            this.treeView.HotTracking = true;
            this.treeView.ImageIndex = 0;
            this.treeView.Indent = 19;
            this.treeView.IsCheckedByMouse = false;
            this.treeView.ItemHeight = 25;
            this.treeView.ItemTypeCheckBoxed = Edit2D.UC.TreeViewLocalItemType.None;
            this.treeView.ItemTypeShowed = Edit2D.UC.TreeViewLocalItemType.None;
            this.treeView.Location = new System.Drawing.Point(3, 33);
            this.treeView.Name = "treeView";
            this.treeView.OrderType = Edit2D.UC.TreeViewLocalOrderType.OrderByEntity;
            this.treeView.Repository = null;
            this.treeView.SelectedImageIndex = 0;
            this.treeView.ShowPlusMinus = false;
            this.treeView.ShowRootLines = false;
            this.treeView.Size = new System.Drawing.Size(310, 213);
            this.treeView.TabIndex = 13;
            this.treeView.Tag = "BG2";
            this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
            // 
            // FrmEdit2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 725);
            this.Controls.Add(this.toolStripContainer);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 600);
            this.Name = "FrmEdit2D";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Studio d\'animation 2D - Geff";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnlMain.Panel1.ResumeLayout(false);
            this.pnlMain.Panel2.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlViewerModes.Panel1.ResumeLayout(false);
            this.pnlViewerModes.Panel2.ResumeLayout(false);
            this.pnlViewerModes.ResumeLayout(false);
            this.pnlModes.ResumeLayout(false);
            this.pnlButtonsModes.ResumeLayout(false);
            this.pnlRight.Panel1.ResumeLayout(false);
            this.pnlRight.Panel2.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRightBarMode.ResumeLayout(false);
            this.pnlEntitys.ResumeLayout(false);
            this.pnlSpring.ResumeLayout(false);
            this.pnlJoint.ResumeLayout(false);
            this.pnlRightEntities.ResumeLayout(false);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.modelViewerControltureBox modelViewerControlImage;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnPlay;
        private System.Windows.Forms.ToolStripButton btnPause;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnShowDebugMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnMove;
        private System.Windows.Forms.ToolStripButton btnResize;
        private System.Windows.Forms.ToolStripButton btnRotate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnPinStatic;
        private System.Windows.Forms.ToolStripButton btnColisionable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnResetEntityPhysic;
        private System.Windows.Forms.ToolStripButton btnCursorToEntityCenter;
        private System.Windows.Forms.ToolStripButton btnSetCenterEntity;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.SplitContainer pnlMain;
        private System.Windows.Forms.SplitContainer pnlViewerModes;
        public ModelViewerControl modelViewerControl;
        private System.Windows.Forms.SplitContainer pnlRight;
        private System.Windows.Forms.TableLayoutPanel pnlRightEntities;
        private System.Windows.Forms.TableLayoutPanel pnlModes;
        private System.Windows.Forms.FlowLayoutPanel pnlButtonsModes;
        private System.Windows.Forms.RadioButton btnScriptModeBar;
        private System.Windows.Forms.RadioButton btnTriggerModeBar;
        private System.Windows.Forms.RadioButton btnParticleSystemModeBar;
        private System.Windows.Forms.Button btnDownEntity;
        private System.Windows.Forms.Button btnUpEntity;
        private Edit2D.ParticleControl.ParticleControl particleControl;
        private Edit2D.ScriptControl.ScriptControl scriptControl;
        private Edit2D.TriggerControl.TriggerControl triggerControl;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxEntityName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusMouse;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnGameClickableOnPlay;
        private System.Windows.Forms.ToolStripButton btnRecObjectStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button btnDelEntity;
        private System.Windows.Forms.Button btnAddEntity;
        private System.Windows.Forms.Button btnLinearSpring;
        private System.Windows.Forms.Button btnFixedLinearSpring;
        private System.Windows.Forms.Button btnFixedRevoluteJoint;
        private System.Windows.Forms.Button btnRevoluteJoint;
        private System.Windows.Forms.Button btnPinJoint;
        private System.Windows.Forms.ToolStripButton btnPanelBottom;
        private System.Windows.Forms.ToolStripButton btnPanelRight;
        private System.Windows.Forms.ToolStripTextBox txtRecTime;
        private System.Windows.Forms.ToolStripButton btnRecObjectStatusLoop;
        private System.Windows.Forms.ToolStripButton btnOrderUp;
        private System.Windows.Forms.ToolStripButton btnOrderDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.Button btnAngleSpring;
        private System.Windows.Forms.Button btnFixedAngleSpring;
        private System.Windows.Forms.FlowLayoutPanel pnlRightBarMode;
        private System.Windows.Forms.Panel pnlEntitys;
        private System.Windows.Forms.Panel pnlSpring;
        private System.Windows.Forms.Panel pnlJoint;
        private System.Windows.Forms.RadioButton optRightBarEntities;
        private System.Windows.Forms.RadioButton optRightBarSpring;
        private System.Windows.Forms.RadioButton optRightBarJoint;
        private System.Windows.Forms.RadioButton optRightBarProperties;
        private Edit2D.UC.PropertyGridLocal propertyGrid;
        public Edit2D.UC.TreeViewLocal treeView;
    }
}

