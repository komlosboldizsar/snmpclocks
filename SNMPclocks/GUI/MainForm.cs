using BToolbox.GUI.Helpers.Converters;
using BToolbox.GUI.Tables;
using BToolbox.Model;
using SNMPclocks.Model;
using SNMPclocks.SNMP;
using System.Data;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SNMPclocks.GUI
{
    public partial class MainForm : Form
    {

        public MainForm() : this(new(), null) { }

        public MainForm(ObservableList<Clock> clockList, SnmpAgent snmpAgent)
        {
            _clockList = clockList;
            _snmpAgent = snmpAgent;
            InitializeComponent();
            initDataGridView();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (_snmpAgent != null)
                _snmpAgent.StatusChanged += snmpAgentStatusChangedHandler;
            displaySnmpAgentStatus();
            endEditingClock();
        }

        #region SNMP agent status
        private SnmpAgent _snmpAgent;

        private void displaySnmpAgentStatus()
        {
            if (_snmpAgent == null)
                return;
            if (_snmpAgent.Started)
            {
                int receiverCount = _snmpAgent.TrapSendingConfig.ReceiverCount;
                string receiverPlural = (receiverCount == 1) ? "s" : string.Empty;
                snmpStatusToolStripStatusLabel.Text = $"SNMP agent started at port {_snmpAgent.Port}, sending traps for {receiverCount} receiver{receiverPlural}.";
                snmpStatusToolStripStatusLabel.ForeColor = SystemColors.WindowText;
                tryRestartSnmpAgentToolStripStatusLabel.Visible = false;
            }
            else
            {
                snmpStatusToolStripStatusLabel.Text = "SNMP agent not started.";
                snmpStatusToolStripStatusLabel.ForeColor = Color.Red;
                tryRestartSnmpAgentToolStripStatusLabel.Visible = true;
            }
        }

        private void snmpAgentStatusChangedHandler(bool started, Exception startException)
            => displaySnmpAgentStatus();

        private void tryRestartSnmpAgentToolStripStatusLabel_Click(object sender, EventArgs e)
            => _snmpAgent.Start();
        #endregion

        #region About window
        private void aboutToolStripStatusLabel_Click(object sender, EventArgs e)
            => (new AboutForm()).ShowDialog();
        #endregion

        #region Table
        private ObservableList<Clock> _clockList;
        private CustomDataGridView<Clock> _table;

        private void initDataGridView()
        {

            _table = new CustomDataGridView<Clock>();
            clockTablePanel.Controls.Clear();
            clockTablePanel.Controls.Add(_table);
            _table.Dock = DockStyle.Fill;

            CustomDataGridViewColumnDescriptorBuilder<Clock> builder;
            Func<CustomDataGridViewColumnDescriptorBuilder<Clock>> getBuilder = () => new(_table);

            // Column: ID
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("#");
            builder.Width(40);
            builder.UpdaterMethod((clock, cell) => { cell.Value = clock.ID; });
            builder.AddChangeEvent(nameof(Clock.Mode));
            builder.BuildAndAdd();

            // Column: label
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Label");
            builder.Width(150);
            builder.UpdaterMethod((clock, cell) => { cell.Value = clock.Label; });
            builder.AddChangeEvent(nameof(Clock.Label));
            builder.BuildAndAdd();

            // Column: mode
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("Mode");
            builder.Width(60);
            builder.UpdaterMethod((clock, cell) => { cell.Value = modeImageConverter.Convert(clock.Mode); });
            builder.CellStyle(FOUR_PIXELS_PADDING_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.Mode));
            builder.BuildAndAdd();

            // Column: state
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("State");
            builder.Width(60);
            builder.UpdaterMethod((clock, cell) => { cell.Value = stateImageConverter.Convert(clock.State); });
            builder.CellStyle(FOUR_PIXELS_PADDING_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.State));
            builder.BuildAndAdd();

            // Column: value
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Value");
            builder.Width(110);
            builder.UpdaterMethod((clock, cell) =>
            {
                cell.Value = clock.Enabled ? clock.ValueStringHhMmSs.PadLeft(9, ' ') : string.Empty;
                Color foreColor = _table.DefaultCellStyle.ForeColor;
                if (!clock.Enabled)
                    foreColor = Color.LightGray;
                else if (clock.ReachedZero)
                    foreColor = Color.Red;
                cell.Style.ForeColor = foreColor;
            });
            builder.CellStyle(TIME_VALUE_CELL_STYLE);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.AddChangeEvent(nameof(Clock.ValueStringHhMmSs));
            builder.AddChangeEvent(nameof(Clock.ReachedZero));
            builder.AddChangeEvent(nameof(Clock.Enabled));
            builder.BuildAndAdd();

            // Column: offset
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Offset");
            builder.Width(110);
            builder.UpdaterMethod((clock, cell) =>
            {
                cell.Value = (clock.Mode == ClockMode.Time) ? clock.OffsetStringHhMmSs.PadLeft(9, ' ') : string.Empty;
            });
            builder.CellStyle(TIME_VALUE_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.OffsetStringHhMmSs));
            builder.AddChangeEvent(nameof(Clock.Mode));
            builder.BuildAndAdd();

            // Column: start value
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Start value");
            builder.Width(110);
            builder.UpdaterMethod((clock, cell) =>
            {
                cell.Value = (clock.Mode == ClockMode.Down) ? clock.StartValueStringHhMmSs : string.Empty;
            });
            builder.CellStyle(TIME_VALUE_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.StartValueSeconds));
            builder.AddChangeEvent(nameof(Clock.Mode));
            builder.BuildAndAdd();

            // Column: until
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Until");
            builder.Width(110);
            builder.UpdaterMethod((clock, cell) =>
            {
                cell.Value = (clock.Mode == ClockMode.Until) ? clock.UntilStringHhMmSs : string.Empty;
            });
            builder.CellStyle(TIME_VALUE_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.UntilStringHhMmSs));
            builder.AddChangeEvent(nameof(Clock.Mode));
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.BuildAndAdd();

            // Column: start
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("Start");
            builder.Width(60);
            builder.ButtonImage(ImageResources.do_start);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((clock, cell) =>
            {
                ((DataGridViewDisableButtonCell)cell).Enabled = (((clock.State == ClockState.Stopped) || (clock.State == ClockState.Reset)) && (clock.Mode != ClockMode.Until));
            });
            builder.CellContentClickHandlerMethod((clock, cell, e) => clock.Start());
            builder.CellStyle(TIME_VALUE_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.State));
            builder.BuildAndAdd();

            // Column: stop
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("Stop");
            builder.Width(60);
            builder.ButtonImage(ImageResources.do_stop);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((clock, cell) =>
            {
                ((DataGridViewDisableButtonCell)cell).Enabled = ((clock.State == ClockState.Running) && (clock.Mode != ClockMode.Until));
            });
            builder.CellContentClickHandlerMethod((clock, cell, e) => clock.Stop());
            builder.CellStyle(TIME_VALUE_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.State));
            builder.BuildAndAdd();

            // Column: reset
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("Reset");
            builder.Width(60);
            builder.ButtonImage(ImageResources.state_reset);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((clock, cell) =>
            {
                ((DataGridViewDisableButtonCell)cell).Enabled = (clock.Mode != ClockMode.Time);
            });
            builder.CellContentClickHandlerMethod((clock, cell, e) => clock.Reset());
            builder.CellStyle(TIME_VALUE_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.Mode));
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.BuildAndAdd();

            // Column: edit
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("Edit");
            builder.Width(60);
            builder.ButtonImage(ImageResources.do_edit);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.CellContentClickHandlerMethod((clock, cell, e) => selectForEdit(clock));
            builder.CellStyle(TIME_VALUE_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.Mode));
            builder.BuildAndAdd();

            // Column: delete
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("Delete");
            builder.Width(60);
            builder.ButtonImage(ImageResources.do_delete);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.CellContentClickHandlerMethod((clock, cell, e) =>
            {
                DialogResult promptResult = MessageBox.Show($"Are you sure you want to delete this clock?\n({clock.ID}) {clock.Label}", "Confirm delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (promptResult == DialogResult.OK)
                {
                    _clockList.Remove(clock);
                    clock.Removed();
                }
            });
            builder.CellStyle(TIME_VALUE_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.Mode));
            builder.BuildAndAdd();

            _table.BoundCollection = _clockList;

        }

        protected const int DEFAULT_DIVIDER_WIDTH = 3;

        protected readonly Padding DEFAULT_IMAGE_BUTTON_PADDING = new(4);

        protected DataGridViewCellStyle BOLD_TEXT_CELL_STYLE
        {
            get
            {
                DataGridViewCellStyle boldTextCellStyle = _table.DefaultCellStyle.Clone();
                boldTextCellStyle.Font = new(_table.DefaultCellStyle.Font, FontStyle.Bold);
                return boldTextCellStyle;
            }
        }

        protected DataGridViewCellStyle FOUR_PIXELS_PADDING_CELL_STYLE
        {
            get
            {
                DataGridViewCellStyle twoPixelsPaddingCellStyle = _table.DefaultCellStyle.Clone();
                twoPixelsPaddingCellStyle.Padding = new(4);
                return twoPixelsPaddingCellStyle;
            }
        }

        protected DataGridViewCellStyle TIME_VALUE_CELL_STYLE
        {
            get
            {
                DataGridViewCellStyle timeValueCellStyle = _table.DefaultCellStyle.Clone();
                timeValueCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                timeValueCellStyle.Font = new(FontFamily.GenericMonospace, _table.DefaultCellStyle.Font.Size);
                return timeValueCellStyle;
            }
        }

        private static readonly EnumToBitmapConverter<ClockMode> modeImageConverter = new() {
            { ClockMode.Time, ImageResources.mode_time },
            { ClockMode.Up, ImageResources.mode_up },
            { ClockMode.Down, ImageResources.mode_down },
            { ClockMode.Until, ImageResources.mode_until }
        };

        private static readonly EnumToBitmapConverter<ClockState> stateImageConverter = new() {
            { ClockState.Disabled, ImageResources.state_disabled },
            { ClockState.Time, ImageResources.state_time },
            { ClockState.Reset, ImageResources.state_reset },
            { ClockState.Stopped, ImageResources.state_stopped },
            { ClockState.Running, ImageResources.state_running },
            { ClockState.Expired, ImageResources.state_expired }
        };
        #endregion

        #region Edit form
        Clock _editedClock = null;
        ClockMode _selectedClockMode = ClockMode.Time;

        private void selectForEdit(Clock clock)
        {
            _editedClock = clock;
            startEditingClock("Edit clock");
            clockIdNumericUpDown.Enabled = false;
            clockIdNumericUpDown.Value = clock.ID;
            clockLabelTextBox.Text = clock.Label;
            clockModeTimeRadioButton.Checked = (clock.Mode == ClockMode.Time);
            clockModeUpRadioButton.Checked = (clock.Mode == ClockMode.Up);
            clockModeDownRadioButton.Checked = (clock.Mode == ClockMode.Down);
            clockModeUntilRadioButton.Checked = (clock.Mode == ClockMode.Until);
            clockEnabledCheckBox.Checked = clock.Enabled;
            clockOffsetDateTimePicker.Value = DATETIME_ZERO + new TimeSpan(clock.OffsetSeconds * TimeSpan.TicksPerSecond);
            clockStartValueDateTimePicker.Value = DATETIME_ZERO + new TimeSpan(clock.StartValueSeconds * TimeSpan.TicksPerSecond); ;
            clockUntilDateTimePicker.Value = DATETIME_ZERO + new TimeSpan(clock.UntilSeconds * TimeSpan.TicksPerSecond);
            clockEnableNegativeCheckBox.Checked = clock.CanBeNegative;
        }

        private void clearEditForm()
        {
            _editedClock = null;
            clockIdNumericUpDown.Enabled = true;
            clockIdNumericUpDown.Value = 999;
            clockLabelTextBox.Text = string.Empty;
            clockModeTimeRadioButton.Checked = true;
            clockModeUpRadioButton.Checked = false;
            clockModeDownRadioButton.Checked = false;
            clockModeUntilRadioButton.Checked = false;
            clockEnabledCheckBox.Checked = true;
            clockOffsetDateTimePicker.Value = DATETIME_ZERO;
            clockStartValueDateTimePicker.Value = DATETIME_ZERO;
            clockUntilDateTimePicker.Value = DATETIME_ZERO;
            clockEnableNegativeCheckBox.Checked = false;
        }

        private readonly DateTime DATETIME_ZERO = new(2000, 1, 1, 0, 0, 0);

        private void addNewClockButton_Click(object sender, EventArgs e)
        {
            clearEditForm();
            clockIdNumericUpDown.Value = _clockList.Max(c => c.ID) + 1;
            _editedClock = null;
            startEditingClock("Add new clock");
        }

        private void editClockOkButton_Click(object sender, EventArgs e)
        {
            int newId = (int)clockIdNumericUpDown.Value;
            Clock clockWithSameId = _clockList.FirstOrDefault(c => c.ID == newId);
            if ((clockWithSameId != null) && (clockWithSameId != _editedClock))
            {
                MessageBox.Show($"A clock with the same ID exists:\n(#{clockWithSameId.ID}) {clockWithSameId.Label}", "Error adding new clock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool addingNew = (_editedClock == null);
            Clock clock = _editedClock ?? new Clock();
            if (addingNew)
                clock.ID = newId;
            clock.Label = clockLabelTextBox.Text;
            clock.Mode = _selectedClockMode;
            clock.Enabled = clockEnabledCheckBox.Checked;
            int offsetSeconds = (int)clockOffsetDateTimePicker.Value.TimeOfDay.TotalSeconds;
            if (clockOffsetNegativeButton.Text == BUTTONT_TEXT_NEGATIVE)
                offsetSeconds *= -1;
            clock.OffsetSeconds = offsetSeconds;
            clock.StartValueSeconds = (int)clockStartValueDateTimePicker.Value.TimeOfDay.TotalSeconds;
            clock.UntilSeconds = (int)clockUntilDateTimePicker.Value.TimeOfDay.TotalSeconds;
            clock.CanBeNegative = clockEnableNegativeCheckBox.Checked;
            if (addingNew)
                _clockList.Add(clock);
            endEditingClock();
            if (!addingNew)
                ClockEdited?.Invoke(clock);
        }

        private void editClockCancelButton_Click(object sender, EventArgs e)
        {
            endEditingClock();
            clearEditForm();
        }

        private void startEditingClock(string title)
        {
            editClockTitleLabel.Text = title;
            addNewClockPanel.Visible = false;
            editClockPanel.Visible = true;
        }

        private void endEditingClock()
        {
            addNewClockPanel.Visible = true;
            editClockPanel.Visible = false;
        }

        private void clockIdNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int newId = (int)clockIdNumericUpDown.Value;
            Clock clockWithSameId = _clockList.FirstOrDefault(c => c.ID == newId);
            bool error = ((clockWithSameId != null) && (clockWithSameId != _editedClock));
            clockIdNumericUpDown.ForeColor = error ? Color.Red : SystemColors.Window;
        }

        private void clockModeTimeRadioButton_CheckedChanged(object sender, EventArgs e)
            => clockModeSelected(ClockMode.Time);

        private void clockModeUpRadioButton_CheckedChanged(object sender, EventArgs e)
            => clockModeSelected(ClockMode.Up);

        private void clockModeDownRadioButton_CheckedChanged(object sender, EventArgs e)
            => clockModeSelected(ClockMode.Down);

        private void clockModeUntilRadioButton_CheckedChanged(object sender, EventArgs e)
            => clockModeSelected(ClockMode.Until);

        private void clockModeSelected(ClockMode mode)
        {
            _selectedClockMode = mode;
            clockEnabledCheckBox.Enabled = ((mode == ClockMode.Time) || (mode == ClockMode.Until));
            clockOffsetNegativeButton.Enabled = (mode == ClockMode.Time);
            clockOffsetDateTimePicker.Enabled = (mode == ClockMode.Time);
            clockStartValueDateTimePicker.Enabled = (mode == ClockMode.Down);
            clockUntilDateTimePicker.Enabled = (mode == ClockMode.Until);
            clockEnableNegativeCheckBox.Enabled = ((mode == ClockMode.Down) || (mode == ClockMode.Until));
        }

        private const string BUTTONT_TEXT_POSITIVE = "+";
        private const string BUTTONT_TEXT_NEGATIVE = "-";

        private void clockOffsetDateTimePicker_ValueChanged(object sender, EventArgs e)
            => updateOffsetSecondsLabel();

        private void clockOffsetNegativeButton_Click(object sender, EventArgs e)
        {
            bool isNegative = (clockOffsetNegativeButton.Text == BUTTONT_TEXT_NEGATIVE);
            clockOffsetNegativeButton.Text = isNegative ? BUTTONT_TEXT_POSITIVE : BUTTONT_TEXT_NEGATIVE;
            updateOffsetSecondsLabel();
        }

        private void updateOffsetSecondsLabel()
            => updateSecondsLabel(clockOffsetDateTimePicker, clockOffsetSecondsLabel, clockOffsetNegativeButton.Text == BUTTONT_TEXT_NEGATIVE);

        private void clockStartValueDateTimePicker_ValueChanged(object sender, EventArgs e)
            => updateSecondsLabel(clockStartValueDateTimePicker, clockStartValueSecondsLabel);

        private void updateSecondsLabel(DateTimePicker dateTimePicker, Label label, bool negative = false)
        {
            int seconds = (int)dateTimePicker.Value.TimeOfDay.TotalSeconds;
            string negativeSign = (negative && (seconds > 0)) ? "-" : string.Empty;
            label.Text = $"= {negativeSign}{seconds} seconds";
        }

        public delegate void ClockEditedDelegate(Clock clock);
        public event ClockEditedDelegate ClockEdited;
        #endregion


    }
}