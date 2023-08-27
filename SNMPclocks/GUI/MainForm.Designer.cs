namespace SNMPclocks.GUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            statusStrip = new StatusStrip();
            snmpStatusToolStripStatusLabel = new ToolStripStatusLabel();
            tryRestartSnmpAgentToolStripStatusLabel = new ToolStripStatusLabel();
            trayIcon = new NotifyIcon(components);
            clockTable = new DataGridView();
            editClockPanel = new Panel();
            editClockTable = new TableLayoutPanel();
            editClockTitleLabel = new Label();
            clockIdLabel = new Label();
            clockLabelLabel = new Label();
            clockModeLabel = new Label();
            clockIdNumericUpDown = new NumericUpDown();
            clockEnabledLabel = new Label();
            clockEnabledCheckBox = new CheckBox();
            clockEnableNegativeLabel = new Label();
            clockEnableNegativeCheckBox = new CheckBox();
            clockStartValueLabel = new Label();
            clockOffsetLabel = new Label();
            clockLabelTextBox = new TextBox();
            panel3 = new Panel();
            clockModeUntilRadioButton = new RadioButton();
            clockModeDownRadioButton = new RadioButton();
            clockModeUpRadioButton = new RadioButton();
            clockModeTimeRadioButton = new RadioButton();
            clockOffsetSecondsLabel = new Label();
            clockStartValueSecondsLabel = new Label();
            editClockOkButton = new Button();
            editClockCancelButton = new Button();
            clockUntilLabel = new Label();
            clockOffsetDateTimePicker = new DateTimePicker();
            clockStartValueDateTimePicker = new DateTimePicker();
            clockUntilDateTimePicker = new DateTimePicker();
            clockOffsetNegativeButton = new Button();
            addNewClockButton = new Button();
            clockTablePanel = new Panel();
            addNewClockPanel = new Panel();
            aboutToolStripStatusLabel = new ToolStripStatusLabel();
            spacerSpringtoolStripStatusLabel = new ToolStripStatusLabel();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)clockTable).BeginInit();
            editClockPanel.SuspendLayout();
            editClockTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)clockIdNumericUpDown).BeginInit();
            panel3.SuspendLayout();
            clockTablePanel.SuspendLayout();
            addNewClockPanel.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { snmpStatusToolStripStatusLabel, tryRestartSnmpAgentToolStripStatusLabel, spacerSpringtoolStripStatusLabel, aboutToolStripStatusLabel });
            statusStrip.Location = new Point(0, 527);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1182, 26);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // snmpStatusToolStripStatusLabel
            // 
            snmpStatusToolStripStatusLabel.Name = "snmpStatusToolStripStatusLabel";
            snmpStatusToolStripStatusLabel.Size = new Size(414, 20);
            snmpStatusToolStripStatusLabel.Text = "SNMP agent started at port 161, sending traps for 3 receivers.";
            // 
            // tryRestartSnmpAgentToolStripStatusLabel
            // 
            tryRestartSnmpAgentToolStripStatusLabel.IsLink = true;
            tryRestartSnmpAgentToolStripStatusLabel.Name = "tryRestartSnmpAgentToolStripStatusLabel";
            tryRestartSnmpAgentToolStripStatusLabel.Size = new Size(72, 20);
            tryRestartSnmpAgentToolStripStatusLabel.Text = "try restart";
            tryRestartSnmpAgentToolStripStatusLabel.Click += tryRestartSnmpAgentToolStripStatusLabel_Click;
            // 
            // trayIcon
            // 
            trayIcon.Icon = (Icon)resources.GetObject("trayIcon.Icon");
            trayIcon.Text = "SNMPclocks";
            trayIcon.Visible = true;
            // 
            // clockTable
            // 
            clockTable.AllowUserToAddRows = false;
            clockTable.AllowUserToDeleteRows = false;
            clockTable.AllowUserToResizeRows = false;
            clockTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            clockTable.Dock = DockStyle.Fill;
            clockTable.Location = new Point(0, 0);
            clockTable.Name = "clockTable";
            clockTable.ReadOnly = true;
            clockTable.RowHeadersWidth = 51;
            clockTable.RowTemplate.Height = 29;
            clockTable.Size = new Size(1182, 256);
            clockTable.TabIndex = 1;
            // 
            // editClockPanel
            // 
            editClockPanel.AutoSize = true;
            editClockPanel.Controls.Add(editClockTable);
            editClockPanel.Dock = DockStyle.Bottom;
            editClockPanel.Location = new Point(0, 301);
            editClockPanel.Name = "editClockPanel";
            editClockPanel.Padding = new Padding(5);
            editClockPanel.Size = new Size(1182, 226);
            editClockPanel.TabIndex = 2;
            // 
            // editClockTable
            // 
            editClockTable.AutoSize = true;
            editClockTable.ColumnCount = 9;
            editClockTable.ColumnStyles.Add(new ColumnStyle());
            editClockTable.ColumnStyles.Add(new ColumnStyle());
            editClockTable.ColumnStyles.Add(new ColumnStyle());
            editClockTable.ColumnStyles.Add(new ColumnStyle());
            editClockTable.ColumnStyles.Add(new ColumnStyle());
            editClockTable.ColumnStyles.Add(new ColumnStyle());
            editClockTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            editClockTable.ColumnStyles.Add(new ColumnStyle());
            editClockTable.ColumnStyles.Add(new ColumnStyle());
            editClockTable.Controls.Add(editClockTitleLabel, 0, 0);
            editClockTable.Controls.Add(clockIdLabel, 0, 1);
            editClockTable.Controls.Add(clockLabelLabel, 0, 2);
            editClockTable.Controls.Add(clockModeLabel, 0, 3);
            editClockTable.Controls.Add(clockIdNumericUpDown, 1, 1);
            editClockTable.Controls.Add(clockEnabledLabel, 2, 1);
            editClockTable.Controls.Add(clockEnabledCheckBox, 3, 1);
            editClockTable.Controls.Add(clockEnableNegativeLabel, 2, 5);
            editClockTable.Controls.Add(clockEnableNegativeCheckBox, 3, 5);
            editClockTable.Controls.Add(clockStartValueLabel, 2, 3);
            editClockTable.Controls.Add(clockOffsetLabel, 2, 2);
            editClockTable.Controls.Add(clockLabelTextBox, 1, 2);
            editClockTable.Controls.Add(panel3, 1, 3);
            editClockTable.Controls.Add(clockOffsetSecondsLabel, 5, 2);
            editClockTable.Controls.Add(clockStartValueSecondsLabel, 5, 3);
            editClockTable.Controls.Add(editClockOkButton, 8, 5);
            editClockTable.Controls.Add(editClockCancelButton, 7, 5);
            editClockTable.Controls.Add(clockUntilLabel, 2, 4);
            editClockTable.Controls.Add(clockOffsetDateTimePicker, 4, 2);
            editClockTable.Controls.Add(clockStartValueDateTimePicker, 3, 3);
            editClockTable.Controls.Add(clockUntilDateTimePicker, 3, 4);
            editClockTable.Controls.Add(clockOffsetNegativeButton, 3, 2);
            editClockTable.Dock = DockStyle.Fill;
            editClockTable.Location = new Point(5, 5);
            editClockTable.Name = "editClockTable";
            editClockTable.RowCount = 7;
            editClockTable.RowStyles.Add(new RowStyle());
            editClockTable.RowStyles.Add(new RowStyle());
            editClockTable.RowStyles.Add(new RowStyle());
            editClockTable.RowStyles.Add(new RowStyle());
            editClockTable.RowStyles.Add(new RowStyle());
            editClockTable.RowStyles.Add(new RowStyle());
            editClockTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            editClockTable.Size = new Size(1172, 216);
            editClockTable.TabIndex = 0;
            // 
            // editClockTitleLabel
            // 
            editClockTitleLabel.AutoSize = true;
            editClockTable.SetColumnSpan(editClockTitleLabel, 2);
            editClockTitleLabel.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            editClockTitleLabel.Location = new Point(3, 0);
            editClockTitleLabel.Margin = new Padding(3, 0, 3, 10);
            editClockTitleLabel.Name = "editClockTitleLabel";
            editClockTitleLabel.Size = new Size(143, 38);
            editClockTitleLabel.TabIndex = 1;
            editClockTitleLabel.Text = "Edit clock";
            // 
            // clockIdLabel
            // 
            clockIdLabel.AutoSize = true;
            clockIdLabel.Dock = DockStyle.Left;
            clockIdLabel.Location = new Point(3, 48);
            clockIdLabel.Margin = new Padding(3, 0, 15, 0);
            clockIdLabel.Name = "clockIdLabel";
            clockIdLabel.Size = new Size(27, 33);
            clockIdLabel.TabIndex = 2;
            clockIdLabel.Text = "ID:";
            clockIdLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // clockLabelLabel
            // 
            clockLabelLabel.AutoSize = true;
            clockLabelLabel.Dock = DockStyle.Left;
            clockLabelLabel.Location = new Point(3, 81);
            clockLabelLabel.Margin = new Padding(3, 0, 15, 0);
            clockLabelLabel.Name = "clockLabelLabel";
            clockLabelLabel.Size = new Size(48, 33);
            clockLabelLabel.TabIndex = 3;
            clockLabelLabel.Text = "Label:";
            clockLabelLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // clockModeLabel
            // 
            clockModeLabel.AutoSize = true;
            clockModeLabel.Dock = DockStyle.Left;
            clockModeLabel.Location = new Point(3, 114);
            clockModeLabel.Margin = new Padding(3, 0, 15, 0);
            clockModeLabel.Name = "clockModeLabel";
            editClockTable.SetRowSpan(clockModeLabel, 3);
            clockModeLabel.Size = new Size(51, 102);
            clockModeLabel.TabIndex = 4;
            clockModeLabel.Text = "Mode:";
            clockModeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // clockIdNumericUpDown
            // 
            clockIdNumericUpDown.Dock = DockStyle.Left;
            clockIdNumericUpDown.Location = new Point(72, 51);
            clockIdNumericUpDown.Margin = new Padding(3, 3, 33, 3);
            clockIdNumericUpDown.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            clockIdNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            clockIdNumericUpDown.Name = "clockIdNumericUpDown";
            clockIdNumericUpDown.Size = new Size(120, 27);
            clockIdNumericUpDown.TabIndex = 7;
            clockIdNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            clockIdNumericUpDown.ValueChanged += clockIdNumericUpDown_ValueChanged;
            // 
            // clockEnabledLabel
            // 
            clockEnabledLabel.AutoSize = true;
            clockEnabledLabel.Dock = DockStyle.Left;
            clockEnabledLabel.Location = new Point(328, 48);
            clockEnabledLabel.Margin = new Padding(3, 0, 15, 0);
            clockEnabledLabel.Name = "clockEnabledLabel";
            clockEnabledLabel.Size = new Size(66, 33);
            clockEnabledLabel.TabIndex = 5;
            clockEnabledLabel.Text = "Enabled:";
            clockEnabledLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // clockEnabledCheckBox
            // 
            clockEnabledCheckBox.AutoSize = true;
            clockEnabledCheckBox.Dock = DockStyle.Left;
            clockEnabledCheckBox.Location = new Point(464, 51);
            clockEnabledCheckBox.Name = "clockEnabledCheckBox";
            clockEnabledCheckBox.Size = new Size(18, 27);
            clockEnabledCheckBox.TabIndex = 10;
            clockEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // clockEnableNegativeLabel
            // 
            clockEnableNegativeLabel.AutoSize = true;
            clockEnableNegativeLabel.Dock = DockStyle.Left;
            clockEnableNegativeLabel.Location = new Point(328, 180);
            clockEnableNegativeLabel.Margin = new Padding(3, 0, 15, 0);
            clockEnableNegativeLabel.Name = "clockEnableNegativeLabel";
            clockEnableNegativeLabel.Size = new Size(118, 36);
            clockEnableNegativeLabel.TabIndex = 9;
            clockEnableNegativeLabel.Text = "Enable negative:";
            clockEnableNegativeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // clockEnableNegativeCheckBox
            // 
            clockEnableNegativeCheckBox.AutoSize = true;
            clockEnableNegativeCheckBox.Dock = DockStyle.Left;
            clockEnableNegativeCheckBox.Location = new Point(464, 183);
            clockEnableNegativeCheckBox.Name = "clockEnableNegativeCheckBox";
            clockEnableNegativeCheckBox.Size = new Size(18, 30);
            clockEnableNegativeCheckBox.TabIndex = 11;
            clockEnableNegativeCheckBox.UseVisualStyleBackColor = true;
            // 
            // clockStartValueLabel
            // 
            clockStartValueLabel.AutoSize = true;
            clockStartValueLabel.Dock = DockStyle.Left;
            clockStartValueLabel.Location = new Point(328, 114);
            clockStartValueLabel.Margin = new Padding(3, 0, 15, 0);
            clockStartValueLabel.Name = "clockStartValueLabel";
            clockStartValueLabel.Size = new Size(82, 33);
            clockStartValueLabel.TabIndex = 6;
            clockStartValueLabel.Text = "Start value:";
            clockStartValueLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // clockOffsetLabel
            // 
            clockOffsetLabel.AutoSize = true;
            clockOffsetLabel.Dock = DockStyle.Left;
            clockOffsetLabel.Location = new Point(328, 81);
            clockOffsetLabel.Margin = new Padding(3, 0, 15, 0);
            clockOffsetLabel.Name = "clockOffsetLabel";
            clockOffsetLabel.Size = new Size(52, 33);
            clockOffsetLabel.TabIndex = 12;
            clockOffsetLabel.Text = "Offset:";
            clockOffsetLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // clockLabelTextBox
            // 
            clockLabelTextBox.Dock = DockStyle.Left;
            clockLabelTextBox.Location = new Point(72, 84);
            clockLabelTextBox.Margin = new Padding(3, 3, 33, 3);
            clockLabelTextBox.Name = "clockLabelTextBox";
            clockLabelTextBox.Size = new Size(220, 27);
            clockLabelTextBox.TabIndex = 14;
            // 
            // panel3
            // 
            panel3.AutoSize = true;
            panel3.Controls.Add(clockModeUntilRadioButton);
            panel3.Controls.Add(clockModeDownRadioButton);
            panel3.Controls.Add(clockModeUpRadioButton);
            panel3.Controls.Add(clockModeTimeRadioButton);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(72, 117);
            panel3.Name = "panel3";
            editClockTable.SetRowSpan(panel3, 3);
            panel3.Size = new Size(250, 96);
            panel3.TabIndex = 15;
            // 
            // clockModeUntilRadioButton
            // 
            clockModeUntilRadioButton.AutoSize = true;
            clockModeUntilRadioButton.Dock = DockStyle.Top;
            clockModeUntilRadioButton.Location = new Point(0, 72);
            clockModeUntilRadioButton.Name = "clockModeUntilRadioButton";
            clockModeUntilRadioButton.Size = new Size(250, 24);
            clockModeUntilRadioButton.TabIndex = 3;
            clockModeUntilRadioButton.TabStop = true;
            clockModeUntilRadioButton.Text = "until";
            clockModeUntilRadioButton.UseVisualStyleBackColor = true;
            clockModeUntilRadioButton.CheckedChanged += clockModeUntilRadioButton_CheckedChanged;
            // 
            // clockModeDownRadioButton
            // 
            clockModeDownRadioButton.AutoSize = true;
            clockModeDownRadioButton.Dock = DockStyle.Top;
            clockModeDownRadioButton.Location = new Point(0, 48);
            clockModeDownRadioButton.Name = "clockModeDownRadioButton";
            clockModeDownRadioButton.Size = new Size(250, 24);
            clockModeDownRadioButton.TabIndex = 2;
            clockModeDownRadioButton.TabStop = true;
            clockModeDownRadioButton.Text = "down";
            clockModeDownRadioButton.UseVisualStyleBackColor = true;
            clockModeDownRadioButton.CheckedChanged += clockModeDownRadioButton_CheckedChanged;
            // 
            // clockModeUpRadioButton
            // 
            clockModeUpRadioButton.AutoSize = true;
            clockModeUpRadioButton.Dock = DockStyle.Top;
            clockModeUpRadioButton.Location = new Point(0, 24);
            clockModeUpRadioButton.Name = "clockModeUpRadioButton";
            clockModeUpRadioButton.Size = new Size(250, 24);
            clockModeUpRadioButton.TabIndex = 1;
            clockModeUpRadioButton.TabStop = true;
            clockModeUpRadioButton.Text = "up";
            clockModeUpRadioButton.UseVisualStyleBackColor = true;
            clockModeUpRadioButton.CheckedChanged += clockModeUpRadioButton_CheckedChanged;
            // 
            // clockModeTimeRadioButton
            // 
            clockModeTimeRadioButton.AutoSize = true;
            clockModeTimeRadioButton.Dock = DockStyle.Top;
            clockModeTimeRadioButton.Location = new Point(0, 0);
            clockModeTimeRadioButton.Name = "clockModeTimeRadioButton";
            clockModeTimeRadioButton.Size = new Size(250, 24);
            clockModeTimeRadioButton.TabIndex = 0;
            clockModeTimeRadioButton.TabStop = true;
            clockModeTimeRadioButton.Text = "time";
            clockModeTimeRadioButton.UseVisualStyleBackColor = true;
            clockModeTimeRadioButton.CheckedChanged += clockModeTimeRadioButton_CheckedChanged;
            // 
            // clockOffsetSecondsLabel
            // 
            clockOffsetSecondsLabel.AutoSize = true;
            clockOffsetSecondsLabel.Dock = DockStyle.Left;
            clockOffsetSecondsLabel.Location = new Point(621, 81);
            clockOffsetSecondsLabel.Name = "clockOffsetSecondsLabel";
            clockOffsetSecondsLabel.Size = new Size(88, 33);
            clockOffsetSecondsLabel.TabIndex = 18;
            clockOffsetSecondsLabel.Text = "= 0 seconds";
            clockOffsetSecondsLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // clockStartValueSecondsLabel
            // 
            clockStartValueSecondsLabel.AutoSize = true;
            clockStartValueSecondsLabel.Dock = DockStyle.Left;
            clockStartValueSecondsLabel.Location = new Point(621, 114);
            clockStartValueSecondsLabel.Name = "clockStartValueSecondsLabel";
            clockStartValueSecondsLabel.Size = new Size(88, 33);
            clockStartValueSecondsLabel.TabIndex = 19;
            clockStartValueSecondsLabel.Text = "= 0 seconds";
            clockStartValueSecondsLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // editClockOkButton
            // 
            editClockOkButton.Dock = DockStyle.Bottom;
            editClockOkButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            editClockOkButton.Location = new Point(1075, 184);
            editClockOkButton.Name = "editClockOkButton";
            editClockOkButton.Size = new Size(94, 29);
            editClockOkButton.TabIndex = 21;
            editClockOkButton.Text = "OK";
            editClockOkButton.UseVisualStyleBackColor = true;
            editClockOkButton.Click += editClockOkButton_Click;
            // 
            // editClockCancelButton
            // 
            editClockCancelButton.Dock = DockStyle.Bottom;
            editClockCancelButton.Location = new Point(975, 184);
            editClockCancelButton.Name = "editClockCancelButton";
            editClockCancelButton.Size = new Size(94, 29);
            editClockCancelButton.TabIndex = 20;
            editClockCancelButton.Text = "Cancel";
            editClockCancelButton.UseVisualStyleBackColor = true;
            editClockCancelButton.Click += editClockCancelButton_Click;
            // 
            // clockUntilLabel
            // 
            clockUntilLabel.AutoSize = true;
            clockUntilLabel.Dock = DockStyle.Left;
            clockUntilLabel.Location = new Point(328, 147);
            clockUntilLabel.Margin = new Padding(3, 0, 15, 0);
            clockUntilLabel.Name = "clockUntilLabel";
            clockUntilLabel.Size = new Size(43, 33);
            clockUntilLabel.TabIndex = 22;
            clockUntilLabel.Text = "Until:";
            clockUntilLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // clockOffsetDateTimePicker
            // 
            clockOffsetDateTimePicker.CustomFormat = "";
            clockOffsetDateTimePicker.Dock = DockStyle.Fill;
            clockOffsetDateTimePicker.Format = DateTimePickerFormat.Time;
            clockOffsetDateTimePicker.Location = new Point(495, 84);
            clockOffsetDateTimePicker.Name = "clockOffsetDateTimePicker";
            clockOffsetDateTimePicker.ShowUpDown = true;
            clockOffsetDateTimePicker.Size = new Size(120, 27);
            clockOffsetDateTimePicker.TabIndex = 24;
            clockOffsetDateTimePicker.Value = new DateTime(2023, 8, 23, 0, 0, 0, 0);
            clockOffsetDateTimePicker.ValueChanged += clockOffsetDateTimePicker_ValueChanged;
            // 
            // clockStartValueDateTimePicker
            // 
            editClockTable.SetColumnSpan(clockStartValueDateTimePicker, 2);
            clockStartValueDateTimePicker.Dock = DockStyle.Fill;
            clockStartValueDateTimePicker.Format = DateTimePickerFormat.Time;
            clockStartValueDateTimePicker.Location = new Point(464, 117);
            clockStartValueDateTimePicker.Name = "clockStartValueDateTimePicker";
            clockStartValueDateTimePicker.ShowUpDown = true;
            clockStartValueDateTimePicker.Size = new Size(151, 27);
            clockStartValueDateTimePicker.TabIndex = 25;
            clockStartValueDateTimePicker.Value = new DateTime(2023, 8, 23, 0, 0, 0, 0);
            clockStartValueDateTimePicker.ValueChanged += clockStartValueDateTimePicker_ValueChanged;
            // 
            // clockUntilDateTimePicker
            // 
            editClockTable.SetColumnSpan(clockUntilDateTimePicker, 2);
            clockUntilDateTimePicker.Dock = DockStyle.Fill;
            clockUntilDateTimePicker.Format = DateTimePickerFormat.Time;
            clockUntilDateTimePicker.Location = new Point(464, 150);
            clockUntilDateTimePicker.Name = "clockUntilDateTimePicker";
            clockUntilDateTimePicker.ShowUpDown = true;
            clockUntilDateTimePicker.Size = new Size(151, 27);
            clockUntilDateTimePicker.TabIndex = 26;
            clockUntilDateTimePicker.Value = new DateTime(2023, 8, 23, 0, 0, 0, 0);
            // 
            // clockOffsetNegativeButton
            // 
            clockOffsetNegativeButton.Location = new Point(464, 84);
            clockOffsetNegativeButton.Name = "clockOffsetNegativeButton";
            clockOffsetNegativeButton.Size = new Size(25, 27);
            clockOffsetNegativeButton.TabIndex = 27;
            clockOffsetNegativeButton.Text = "+";
            clockOffsetNegativeButton.UseVisualStyleBackColor = true;
            clockOffsetNegativeButton.Click += clockOffsetNegativeButton_Click;
            // 
            // addNewClockButton
            // 
            addNewClockButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addNewClockButton.Location = new Point(1016, 8);
            addNewClockButton.Name = "addNewClockButton";
            addNewClockButton.Size = new Size(158, 29);
            addNewClockButton.TabIndex = 0;
            addNewClockButton.Text = "Add new clock";
            addNewClockButton.UseVisualStyleBackColor = true;
            addNewClockButton.Click += addNewClockButton_Click;
            // 
            // clockTablePanel
            // 
            clockTablePanel.Controls.Add(clockTable);
            clockTablePanel.Dock = DockStyle.Fill;
            clockTablePanel.Location = new Point(0, 0);
            clockTablePanel.Name = "clockTablePanel";
            clockTablePanel.Size = new Size(1182, 256);
            clockTablePanel.TabIndex = 0;
            // 
            // addNewClockPanel
            // 
            addNewClockPanel.AutoSize = true;
            addNewClockPanel.Controls.Add(addNewClockButton);
            addNewClockPanel.Dock = DockStyle.Bottom;
            addNewClockPanel.Location = new Point(0, 256);
            addNewClockPanel.Name = "addNewClockPanel";
            addNewClockPanel.Padding = new Padding(0, 5, 0, 5);
            addNewClockPanel.Size = new Size(1182, 45);
            addNewClockPanel.TabIndex = 20;
            // 
            // aboutToolStripStatusLabel
            // 
            aboutToolStripStatusLabel.IsLink = true;
            aboutToolStripStatusLabel.Name = "aboutToolStripStatusLabel";
            aboutToolStripStatusLabel.Size = new Size(48, 20);
            aboutToolStripStatusLabel.Text = "about";
            aboutToolStripStatusLabel.Click += aboutToolStripStatusLabel_Click;
            // 
            // spacerSpringtoolStripStatusLabel
            // 
            spacerSpringtoolStripStatusLabel.Name = "spacerSpringtoolStripStatusLabel";
            spacerSpringtoolStripStatusLabel.Size = new Size(594, 20);
            spacerSpringtoolStripStatusLabel.Spring = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1182, 553);
            Controls.Add(clockTablePanel);
            Controls.Add(addNewClockPanel);
            Controls.Add(editClockPanel);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1200, 600);
            Name = "MainForm";
            Text = "SNMPclocks";
            Load += MainForm_Load;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)clockTable).EndInit();
            editClockPanel.ResumeLayout(false);
            editClockPanel.PerformLayout();
            editClockTable.ResumeLayout(false);
            editClockTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)clockIdNumericUpDown).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            clockTablePanel.ResumeLayout(false);
            addNewClockPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private NotifyIcon trayIcon;
        private DataGridView clockTable;
        private Panel editClockPanel;
        private Panel clockTablePanel;
        private TableLayoutPanel editClockTable;
        private Button addNewClockButton;
        private Label editClockTitleLabel;
        private Label clockIdLabel;
        private Label clockLabelLabel;
        private Label clockModeLabel;
        private Label clockEnabledLabel;
        private Label clockStartValueLabel;
        private NumericUpDown clockIdNumericUpDown;
        private Label clockEnableNegativeLabel;
        private CheckBox clockEnabledCheckBox;
        private CheckBox clockEnableNegativeCheckBox;
        private Label clockOffsetLabel;
        private TextBox clockLabelTextBox;
        private Panel panel3;
        private RadioButton clockModeDownRadioButton;
        private RadioButton clockModeUpRadioButton;
        private RadioButton clockModeTimeRadioButton;
        private NumericUpDown clockOffsetNumericUpDown;
        private NumericUpDown clockStartValueNumericUpDown;
        private Label clockOffsetSecondsLabel;
        private Label clockStartValueSecondsLabel;
        private Button editClockOkButton;
        private Button editClockCancelButton;
        private Panel addNewClockPanel;
        private RadioButton clockModeUntilRadioButton;
        private Label clockUntilLabel;
        private NumericUpDown clockUntilNumericUpDown;
        private DateTimePicker clockOffsetDateTimePicker;
        private DateTimePicker clockStartValueDateTimePicker;
        private DateTimePicker clockUntilDateTimePicker;
        private Button clockOffsetNegativeButton;
        private ToolStripStatusLabel snmpStatusToolStripStatusLabel;
        private ToolStripStatusLabel tryRestartSnmpAgentToolStripStatusLabel;
        private ToolStripStatusLabel spacerSpringtoolStripStatusLabel;
        private ToolStripStatusLabel aboutToolStripStatusLabel;
    }
}