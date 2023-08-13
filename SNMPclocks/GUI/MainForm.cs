using BToolbox.GUI.Helpers.Converters;
using BToolbox.GUI.Tables;
using BToolbox.Model;
using System.Data;
using System.Windows.Forms;

namespace SNMPclocks.GUI
{
    public partial class MainForm : Form
    {

        public MainForm() : this(new()) { }

        public MainForm(ObservableList<Clock> clockList)
        {
            _clockList = clockList;
            InitializeComponent();
            initDataGridView();
        }

        private ObservableList<Clock> _clockList;
        private CustomDataGridView<Clock> _table;

        private void initDataGridView()
        {

            _table = new CustomDataGridView<Clock>();
            panel2.Controls.Clear();
            panel2.Controls.Add(_table);
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
            builder.UpdaterMethod((clock, cell) => {
                cell.Value = clock.ValueStringHhMmSs.PadLeft(9, ' ');
                cell.Style.ForeColor = clock.ReachedZero ? Color.Red : _table.DefaultCellStyle.ForeColor;
            });
            builder.CellStyle(TIME_VALUE_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.ValueStringHhMmSs));
            builder.AddChangeEvent(nameof(Clock.ReachedZero));
            builder.BuildAndAdd();

            // Column: start value
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Start value");
            builder.Width(110);
            builder.UpdaterMethod((clock, cell) => {
                cell.Value = (clock.Mode == ClockMode.Down) ? clock.StartValueStringHhMmSs : string.Empty;
            });
            builder.CellStyle(TIME_VALUE_CELL_STYLE);
            builder.AddChangeEvent(nameof(Clock.StartValue));
            builder.AddChangeEvent(nameof(Clock.Mode));
            builder.BuildAndAdd();

            // Column: start
            builder = getBuilder();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("Start");
            builder.Width(60);
            builder.ButtonImage(ImageResources.state_running);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((clock, cell) => {
                ((DataGridViewDisableButtonCell)cell).Enabled = ((clock.State == ClockState.Stopped) || (clock.State == ClockState.Reset));
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
            builder.ButtonImage(ImageResources.state_stopped);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((clock, cell) => {
                ((DataGridViewDisableButtonCell)cell).Enabled = (clock.State == ClockState.Running);
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
            builder.UpdaterMethod((clock, cell) => {
                ((DataGridViewDisableButtonCell)cell).Enabled = (clock.Mode != ClockMode.Time);
            });
            builder.CellContentClickHandlerMethod((clock, cell, e) => clock.Reset());
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
                timeValueCellStyle.Font = new(System.Drawing.FontFamily.GenericMonospace, _table.DefaultCellStyle.Font.Size);
                return timeValueCellStyle;
            }
        }

        private static readonly EnumToBitmapConverter<ClockMode> modeImageConverter = new EnumToBitmapConverter<ClockMode>() {
            { ClockMode.Time, ImageResources.mode_time },
            { ClockMode.Up, ImageResources.mode_up },
            { ClockMode.Down, ImageResources.mode_down },
        };

        private static readonly EnumToBitmapConverter<ClockState> stateImageConverter = new EnumToBitmapConverter<ClockState>() {
            { ClockState.Time, ImageResources.state_time },
            { ClockState.Reset, ImageResources.state_reset },
            { ClockState.Stopped, ImageResources.state_stopped },
            { ClockState.Running, ImageResources.state_running },
            { ClockState.Expired, ImageResources.state_expired }
        };

    }
}