﻿namespace MapEditor {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.programMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.smallMapMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumMapMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.grassButton = new System.Windows.Forms.ToolStripButton();
            this.sandButton = new System.Windows.Forms.ToolStripButton();
            this.waterButton = new System.Windows.Forms.ToolStripButton();
            this.treeButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.renderControl = new MapEditor.RenderControl();
            this.toolStripStatusLabel = new System.Windows.Forms.StatusStrip();
            this.cellPositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.toolStripStatusLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(844, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // programMenu
            // 
            this.programMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenu,
            this.loadMenu,
            this.saveMenu,
            this.toolStripMenuItem1,
            this.closeMenu});
            this.programMenu.Name = "programMenu";
            this.programMenu.Size = new System.Drawing.Size(65, 20);
            this.programMenu.Text = "Program";
            // 
            // newMenu
            // 
            this.newMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallMapMenu,
            this.mediumMapMenu});
            this.newMenu.Name = "newMenu";
            this.newMenu.Size = new System.Drawing.Size(109, 22);
            this.newMenu.Text = "New";
            // 
            // smallMapMenu
            // 
            this.smallMapMenu.Name = "smallMapMenu";
            this.smallMapMenu.Size = new System.Drawing.Size(109, 22);
            this.smallMapMenu.Text = "20 x 20";
            this.smallMapMenu.Click += new System.EventHandler(this.smallMapMenu_Click);
            // 
            // mediumMapMenu
            // 
            this.mediumMapMenu.Name = "mediumMapMenu";
            this.mediumMapMenu.Size = new System.Drawing.Size(109, 22);
            this.mediumMapMenu.Text = "40 x 40";
            this.mediumMapMenu.Click += new System.EventHandler(this.mediumMapMenu_Click);
            // 
            // loadMenu
            // 
            this.loadMenu.Name = "loadMenu";
            this.loadMenu.Size = new System.Drawing.Size(109, 22);
            this.loadMenu.Text = "Load...";
            this.loadMenu.Click += new System.EventHandler(this.loadMenu_Click);
            // 
            // saveMenu
            // 
            this.saveMenu.Enabled = false;
            this.saveMenu.Name = "saveMenu";
            this.saveMenu.Size = new System.Drawing.Size(109, 22);
            this.saveMenu.Text = "Save...";
            this.saveMenu.Click += new System.EventHandler(this.saveMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(106, 6);
            // 
            // closeMenu
            // 
            this.closeMenu.Name = "closeMenu";
            this.closeMenu.Size = new System.Drawing.Size(109, 22);
            this.closeMenu.Text = "Close";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grassButton,
            this.sandButton,
            this.waterButton,
            this.treeButton,
            this.deleteButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(844, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // grassButton
            // 
            this.grassButton.Checked = true;
            this.grassButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.grassButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.grassButton.Image = ((System.Drawing.Image)(resources.GetObject("grassButton.Image")));
            this.grassButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.grassButton.Name = "grassButton";
            this.grassButton.Size = new System.Drawing.Size(39, 22);
            this.grassButton.Text = "Grass";
            this.grassButton.Click += new System.EventHandler(this.grassButton_Click);
            // 
            // sandButton
            // 
            this.sandButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sandButton.Image = ((System.Drawing.Image)(resources.GetObject("sandButton.Image")));
            this.sandButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sandButton.Name = "sandButton";
            this.sandButton.Size = new System.Drawing.Size(37, 22);
            this.sandButton.Text = "Sand";
            this.sandButton.Click += new System.EventHandler(this.sandButton_Click);
            // 
            // waterButton
            // 
            this.waterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.waterButton.Image = ((System.Drawing.Image)(resources.GetObject("waterButton.Image")));
            this.waterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.waterButton.Name = "waterButton";
            this.waterButton.Size = new System.Drawing.Size(42, 22);
            this.waterButton.Text = "Water";
            this.waterButton.Click += new System.EventHandler(this.waterButton_Click);
            // 
            // treeButton
            // 
            this.treeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.treeButton.Image = ((System.Drawing.Image)(resources.GetObject("treeButton.Image")));
            this.treeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.treeButton.Name = "treeButton";
            this.treeButton.Size = new System.Drawing.Size(33, 22);
            this.treeButton.Text = "Tree";
            this.treeButton.Click += new System.EventHandler(this.treeButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(44, 22);
            this.deleteButton.Text = "Delete";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // timer
            // 
            this.timer.Interval = 40;
            this.timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // renderControl
            // 
            this.renderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderControl.Location = new System.Drawing.Point(0, 49);
            this.renderControl.Name = "renderControl";
            this.renderControl.Size = new System.Drawing.Size(844, 430);
            this.renderControl.TabIndex = 2;
            this.renderControl.Paint += new System.Windows.Forms.PaintEventHandler(this.renderControl_Paint);
            this.renderControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.renderControl_MouseDown);
            this.renderControl.MouseEnter += new System.EventHandler(this.renderControl_MouseEnter);
            this.renderControl.MouseLeave += new System.EventHandler(this.renderControl_MouseLeave);
            this.renderControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.renderControl_MouseMove);
            this.renderControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.renderControl_MouseUp);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cellPositionLabel});
            this.toolStripStatusLabel.Location = new System.Drawing.Point(0, 479);
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(844, 22);
            this.toolStripStatusLabel.TabIndex = 3;
            this.toolStripStatusLabel.Text = "statusStrip1";
            // 
            // cellPositionLabel
            // 
            this.cellPositionLabel.Name = "cellPositionLabel";
            this.cellPositionLabel.Size = new System.Drawing.Size(33, 17);
            this.cellPositionLabel.Text = "[cell]";
            this.cellPositionLabel.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "OctoMap|*.map";
            this.openFileDialog.Title = "Load OctoMap";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "OctoMap|*.map";
            this.saveFileDialog.Title = "Save OctoMap";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 501);
            this.Controls.Add(this.renderControl);
            this.Controls.Add(this.toolStripStatusLabel);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "OctoAwesome Map Editor";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.toolStripStatusLabel.ResumeLayout(false);
            this.toolStripStatusLabel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripMenuItem programMenu;
        private System.Windows.Forms.ToolStripMenuItem newMenu;
        private System.Windows.Forms.ToolStripMenuItem smallMapMenu;
        private System.Windows.Forms.ToolStripMenuItem mediumMapMenu;
        private System.Windows.Forms.ToolStripMenuItem closeMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Timer timer;
        private RenderControl renderControl;
        private System.Windows.Forms.StatusStrip toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel cellPositionLabel;
        private System.Windows.Forms.ToolStripButton grassButton;
        private System.Windows.Forms.ToolStripButton sandButton;
        private System.Windows.Forms.ToolStripButton waterButton;
        private System.Windows.Forms.ToolStripMenuItem loadMenu;
        private System.Windows.Forms.ToolStripMenuItem saveMenu;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripButton treeButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
    }
}

