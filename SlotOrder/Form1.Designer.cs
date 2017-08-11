namespace SlotOrder
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label3 = new System.Windows.Forms.Label();
            this.ChoosePath = new System.Windows.Forms.Button();
            this.PathBox = new System.Windows.Forms.TextBox();
            this.groupsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.unitsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.reloadButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Path:";
            // 
            // ChoosePath
            // 
            this.ChoosePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChoosePath.Location = new System.Drawing.Point(617, 25);
            this.ChoosePath.Name = "ChoosePath";
            this.ChoosePath.Size = new System.Drawing.Size(31, 23);
            this.ChoosePath.TabIndex = 8;
            this.ChoosePath.Text = "...";
            this.ChoosePath.UseVisualStyleBackColor = true;
            this.ChoosePath.Click += new System.EventHandler(this.ChoosePath_Click);
            // 
            // PathBox
            // 
            this.PathBox.Location = new System.Drawing.Point(45, 27);
            this.PathBox.Name = "PathBox";
            this.PathBox.Size = new System.Drawing.Size(566, 20);
            this.PathBox.TabIndex = 7;
            this.PathBox.TextChanged += new System.EventHandler(this.Path_TextChanged);
            // 
            // groupsPanel
            // 
            this.groupsPanel.AutoScroll = true;
            this.groupsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.groupsPanel.Location = new System.Drawing.Point(10, 71);
            this.groupsPanel.Name = "groupsPanel";
            this.groupsPanel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.groupsPanel.Size = new System.Drawing.Size(317, 362);
            this.groupsPanel.TabIndex = 10;
            // 
            // unitsPanel
            // 
            this.unitsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.unitsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.unitsPanel.Location = new System.Drawing.Point(333, 71);
            this.unitsPanel.Name = "unitsPanel";
            this.unitsPanel.Size = new System.Drawing.Size(315, 363);
            this.unitsPanel.TabIndex = 11;
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(333, 439);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(315, 23);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // reloadButton
            // 
            this.reloadButton.Enabled = false;
            this.reloadButton.Location = new System.Drawing.Point(10, 439);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(317, 23);
            this.reloadButton.TabIndex = 13;
            this.reloadButton.Text = "Reload";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 53);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(636, 12);
            this.progressBar1.TabIndex = 14;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem,
            this.versionToolStripMenuItem,
            this.versionToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(660, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.versionToolStripMenuItem.Text = "IssueTracker";
            this.versionToolStripMenuItem.Click += new System.EventHandler(this.versionToolStripMenuItem_Click);
            // 
            // versionToolStripMenuItem1
            // 
            this.versionToolStripMenuItem1.Name = "versionToolStripMenuItem1";
            this.versionToolStripMenuItem1.Size = new System.Drawing.Size(57, 20);
            this.versionToolStripMenuItem1.Text = "Version";
            this.versionToolStripMenuItem1.Click += new System.EventHandler(this.versionToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 473);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.reloadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.unitsPanel);
            this.Controls.Add(this.groupsPanel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChoosePath);
            this.Controls.Add(this.PathBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Slotting Screen Rearranger";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ChoosePath;
        private System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.FlowLayoutPanel groupsPanel;
        private System.Windows.Forms.FlowLayoutPanel unitsPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button reloadButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem1;
    }
}

