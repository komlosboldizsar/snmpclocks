namespace SNMPclocks.GUI
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            logoPictureBox = new PictureBox();
            titleLabel = new Label();
            topTableLayoutPanel = new TableLayoutPanel();
            descriptionLabel = new Label();
            authorLabel = new Label();
            githubLinkLabel = new Label();
            githubLabel = new Label();
            licenseLabel = new Label();
            mainPanel = new Panel();
            closeButton = new Button();
            thirdpartyTableLayoutPanel = new TableLayoutPanel();
            thirdpartyTitleLabel = new Label();
            thirdPartyIcons8Label = new Label();
            thirdpartyIcons8LinkLabel = new Label();
            thirdpartySharpsnmpLabel = new Label();
            thirdpartySharpsnmpLinkLabel = new Label();
            separatorContainerPanel1 = new Panel();
            separatorPanel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            topTableLayoutPanel.SuspendLayout();
            mainPanel.SuspendLayout();
            thirdpartyTableLayoutPanel.SuspendLayout();
            separatorContainerPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // logoPictureBox
            // 
            logoPictureBox.Dock = DockStyle.Fill;
            logoPictureBox.Image = (Image)resources.GetObject("logoPictureBox.Image");
            logoPictureBox.Location = new Point(3, 3);
            logoPictureBox.Name = "logoPictureBox";
            topTableLayoutPanel.SetRowSpan(logoPictureBox, 2);
            logoPictureBox.Size = new Size(96, 123);
            logoPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            logoPictureBox.TabIndex = 0;
            logoPictureBox.TabStop = false;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            topTableLayoutPanel.SetColumnSpan(titleLabel, 2);
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            titleLabel.Location = new Point(105, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Padding = new Padding(0, 0, 0, 5);
            titleLabel.Size = new Size(409, 59);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "SNMPclocks";
            // 
            // topTableLayoutPanel
            // 
            topTableLayoutPanel.AutoSize = true;
            topTableLayoutPanel.ColumnCount = 3;
            topTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            topTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            topTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            topTableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
            topTableLayoutPanel.Controls.Add(titleLabel, 1, 0);
            topTableLayoutPanel.Controls.Add(descriptionLabel, 1, 1);
            topTableLayoutPanel.Controls.Add(authorLabel, 0, 2);
            topTableLayoutPanel.Controls.Add(githubLinkLabel, 1, 4);
            topTableLayoutPanel.Controls.Add(githubLabel, 0, 4);
            topTableLayoutPanel.Controls.Add(licenseLabel, 0, 3);
            topTableLayoutPanel.Dock = DockStyle.Top;
            topTableLayoutPanel.Location = new Point(8, 8);
            topTableLayoutPanel.Name = "topTableLayoutPanel";
            topTableLayoutPanel.RowCount = 5;
            topTableLayoutPanel.RowStyles.Add(new RowStyle());
            topTableLayoutPanel.RowStyles.Add(new RowStyle());
            topTableLayoutPanel.RowStyles.Add(new RowStyle());
            topTableLayoutPanel.RowStyles.Add(new RowStyle());
            topTableLayoutPanel.RowStyles.Add(new RowStyle());
            topTableLayoutPanel.Size = new Size(517, 204);
            topTableLayoutPanel.TabIndex = 2;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            topTableLayoutPanel.SetColumnSpan(descriptionLabel, 2);
            descriptionLabel.Dock = DockStyle.Top;
            descriptionLabel.Location = new Point(105, 59);
            descriptionLabel.Margin = new Padding(3, 0, 3, 10);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(409, 60);
            descriptionLabel.TabIndex = 2;
            descriptionLabel.Text = "A simple application to create multiple clocks/timers with various operation modes and settings and manage them trough SNMP.";
            // 
            // authorLabel
            // 
            authorLabel.AutoSize = true;
            topTableLayoutPanel.SetColumnSpan(authorLabel, 2);
            authorLabel.Location = new Point(3, 129);
            authorLabel.Margin = new Padding(3, 0, 3, 5);
            authorLabel.Name = "authorLabel";
            authorLabel.Size = new Size(233, 20);
            authorLabel.TabIndex = 3;
            authorLabel.Text = "Made by KOMLÓS Boldizsár, 2023";
            // 
            // githubLinkLabel
            // 
            githubLinkLabel.AutoSize = true;
            githubLinkLabel.Cursor = Cursors.Hand;
            githubLinkLabel.Dock = DockStyle.Left;
            githubLinkLabel.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            githubLinkLabel.ForeColor = Color.Blue;
            githubLinkLabel.Location = new Point(105, 179);
            githubLinkLabel.Name = "githubLinkLabel";
            githubLinkLabel.Size = new Size(323, 25);
            githubLinkLabel.TabIndex = 5;
            githubLinkLabel.Text = "http://github.com/komlosboldizsar/snmpclocks";
            githubLinkLabel.Click += githubLinkLabel_Click;
            // 
            // githubLabel
            // 
            githubLabel.AutoSize = true;
            githubLabel.Location = new Point(3, 179);
            githubLabel.Margin = new Padding(3, 0, 3, 5);
            githubLabel.Name = "githubLabel";
            githubLabel.Size = new Size(59, 20);
            githubLabel.TabIndex = 6;
            githubLabel.Text = "GitHub:";
            // 
            // licenseLabel
            // 
            licenseLabel.AutoSize = true;
            topTableLayoutPanel.SetColumnSpan(licenseLabel, 3);
            licenseLabel.Location = new Point(3, 154);
            licenseLabel.Margin = new Padding(3, 0, 3, 5);
            licenseLabel.Name = "licenseLabel";
            licenseLabel.Size = new Size(154, 20);
            licenseLabel.TabIndex = 7;
            licenseLabel.Text = "Licensed under GPLv3.";
            // 
            // mainPanel
            // 
            mainPanel.AutoSize = true;
            mainPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            mainPanel.Controls.Add(closeButton);
            mainPanel.Controls.Add(thirdpartyTableLayoutPanel);
            mainPanel.Controls.Add(separatorContainerPanel1);
            mainPanel.Controls.Add(topTableLayoutPanel);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(8);
            mainPanel.Size = new Size(533, 394);
            mainPanel.TabIndex = 3;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.Location = new Point(427, 354);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(94, 29);
            closeButton.TabIndex = 4;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += closeButton_Click;
            // 
            // thirdpartyTableLayoutPanel
            // 
            thirdpartyTableLayoutPanel.AutoSize = true;
            thirdpartyTableLayoutPanel.ColumnCount = 2;
            thirdpartyTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            thirdpartyTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            thirdpartyTableLayoutPanel.Controls.Add(thirdpartyTitleLabel, 0, 0);
            thirdpartyTableLayoutPanel.Controls.Add(thirdPartyIcons8Label, 1, 1);
            thirdpartyTableLayoutPanel.Controls.Add(thirdpartyIcons8LinkLabel, 1, 2);
            thirdpartyTableLayoutPanel.Controls.Add(thirdpartySharpsnmpLabel, 1, 3);
            thirdpartyTableLayoutPanel.Controls.Add(thirdpartySharpsnmpLinkLabel, 1, 4);
            thirdpartyTableLayoutPanel.Dock = DockStyle.Top;
            thirdpartyTableLayoutPanel.Location = new Point(8, 227);
            thirdpartyTableLayoutPanel.Name = "thirdpartyTableLayoutPanel";
            thirdpartyTableLayoutPanel.RowCount = 5;
            thirdpartyTableLayoutPanel.RowStyles.Add(new RowStyle());
            thirdpartyTableLayoutPanel.RowStyles.Add(new RowStyle());
            thirdpartyTableLayoutPanel.RowStyles.Add(new RowStyle());
            thirdpartyTableLayoutPanel.RowStyles.Add(new RowStyle());
            thirdpartyTableLayoutPanel.RowStyles.Add(new RowStyle());
            thirdpartyTableLayoutPanel.Size = new Size(517, 109);
            thirdpartyTableLayoutPanel.TabIndex = 3;
            // 
            // thirdpartyTitleLabel
            // 
            thirdpartyTitleLabel.AutoSize = true;
            thirdpartyTableLayoutPanel.SetColumnSpan(thirdpartyTitleLabel, 2);
            thirdpartyTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            thirdpartyTitleLabel.Location = new Point(3, 0);
            thirdpartyTitleLabel.Margin = new Padding(3, 0, 3, 3);
            thirdpartyTitleLabel.Name = "thirdpartyTitleLabel";
            thirdpartyTitleLabel.Size = new Size(207, 20);
            thirdpartyTitleLabel.TabIndex = 0;
            thirdpartyTitleLabel.Text = "Third party libraries and assets:";
            // 
            // thirdPartyIcons8Label
            // 
            thirdPartyIcons8Label.AutoSize = true;
            thirdPartyIcons8Label.Location = new Point(23, 23);
            thirdPartyIcons8Label.Name = "thirdPartyIcons8Label";
            thirdPartyIcons8Label.Size = new Size(141, 20);
            thirdPartyIcons8Label.TabIndex = 1;
            thirdPartyIcons8Label.Text = "Icons by icons8.com";
            // 
            // thirdpartyIcons8LinkLabel
            // 
            thirdpartyIcons8LinkLabel.AutoSize = true;
            thirdpartyIcons8LinkLabel.Cursor = Cursors.Hand;
            thirdpartyIcons8LinkLabel.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            thirdpartyIcons8LinkLabel.ForeColor = Color.Blue;
            thirdpartyIcons8LinkLabel.Location = new Point(23, 43);
            thirdpartyIcons8LinkLabel.Margin = new Padding(3, 0, 3, 3);
            thirdpartyIcons8LinkLabel.Name = "thirdpartyIcons8LinkLabel";
            thirdpartyIcons8LinkLabel.Size = new Size(131, 20);
            thirdpartyIcons8LinkLabel.TabIndex = 3;
            thirdpartyIcons8LinkLabel.Text = "http://icons8.com/";
            thirdpartyIcons8LinkLabel.Click += thirdpartyIcons8LinkLabel_Click;
            // 
            // thirdpartySharpsnmpLabel
            // 
            thirdpartySharpsnmpLabel.AutoSize = true;
            thirdpartySharpsnmpLabel.Location = new Point(23, 66);
            thirdpartySharpsnmpLabel.Name = "thirdpartySharpsnmpLabel";
            thirdpartySharpsnmpLabel.Size = new Size(215, 20);
            thirdpartySharpsnmpLabel.TabIndex = 2;
            thirdpartySharpsnmpLabel.Text = "Sharp SNMP library by lextudio";
            // 
            // thirdpartySharpsnmpLinkLabel
            // 
            thirdpartySharpsnmpLinkLabel.AutoSize = true;
            thirdpartySharpsnmpLinkLabel.Cursor = Cursors.Hand;
            thirdpartySharpsnmpLinkLabel.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            thirdpartySharpsnmpLinkLabel.ForeColor = Color.Blue;
            thirdpartySharpsnmpLinkLabel.Location = new Point(23, 86);
            thirdpartySharpsnmpLinkLabel.Margin = new Padding(3, 0, 3, 3);
            thirdpartySharpsnmpLinkLabel.Name = "thirdpartySharpsnmpLinkLabel";
            thirdpartySharpsnmpLinkLabel.Size = new Size(281, 20);
            thirdpartySharpsnmpLinkLabel.TabIndex = 4;
            thirdpartySharpsnmpLinkLabel.Text = "http://github.com/lextudio/sharpsnmplib";
            thirdpartySharpsnmpLinkLabel.Click += thirdpartySharpsnmpLinkLabel_Click;
            // 
            // separatorContainerPanel1
            // 
            separatorContainerPanel1.AutoSize = true;
            separatorContainerPanel1.Controls.Add(separatorPanel1);
            separatorContainerPanel1.Dock = DockStyle.Top;
            separatorContainerPanel1.Location = new Point(8, 212);
            separatorContainerPanel1.Name = "separatorContainerPanel1";
            separatorContainerPanel1.Padding = new Padding(0, 5, 0, 8);
            separatorContainerPanel1.Size = new Size(517, 15);
            separatorContainerPanel1.TabIndex = 0;
            // 
            // separatorPanel1
            // 
            separatorPanel1.BackColor = SystemColors.ControlDark;
            separatorPanel1.Dock = DockStyle.Top;
            separatorPanel1.Location = new Point(0, 5);
            separatorPanel1.Margin = new Padding(3, 8, 3, 8);
            separatorPanel1.Name = "separatorPanel1";
            separatorPanel1.Size = new Size(517, 2);
            separatorPanel1.TabIndex = 0;
            // 
            // AboutForm
            // 
            AcceptButton = closeButton;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = closeButton;
            ClientSize = new Size(533, 394);
            Controls.Add(mainPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "SNMPclocks :: about";
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            topTableLayoutPanel.ResumeLayout(false);
            topTableLayoutPanel.PerformLayout();
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            thirdpartyTableLayoutPanel.ResumeLayout(false);
            thirdpartyTableLayoutPanel.PerformLayout();
            separatorContainerPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox logoPictureBox;
        private TableLayoutPanel topTableLayoutPanel;
        private Label titleLabel;
        private Label descriptionLabel;
        private Panel mainPanel;
        private Label authorLabel;
        private Label githubLinkLabel;
        private Label githubLabel;
        private Label licenseLabel;
        private TableLayoutPanel thirdpartyTableLayoutPanel;
        private Panel separatorContainerPanel1;
        private Panel separatorPanel1;
        private Label thirdpartyTitleLabel;
        private Label thirdPartyIcons8Label;
        private Label thirdpartyIcons8LinkLabel;
        private Label thirdpartySharpsnmpLabel;
        private Label thirdpartySharpsnmpLinkLabel;
        private Button closeButton;
    }
}