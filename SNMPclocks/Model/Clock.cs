using BToolbox.Model;
using SNMPclocks.Model;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Timers;
using System.Xml;

namespace SNMPclocks
{
    public class Clock : INotifyPropertyChanged
    {

        #region Initialization
        private bool _beforeInit = true;

        public void Init()
        {
            _beforeInit = false;
            lock (_clockList)
            {
                _clockList.Add(this);
            }
        }
        #endregion

        #region Property: ID
        private int _id = 0;

        [JsonPropertyName("id")]
        public int ID
        {
            get => _id;
            set
            {
                if (value == _id)
                    return;
                _id = value;
                PropertyChanged?.Invoke(nameof(ID));
            }
        }
        #endregion

        #region Property: Label
        private string _label = "(no label)";

        [JsonPropertyName("label")]
        public string Label
        {
            get => _label;
            set
            {
                if (value == _label)
                    return;
                _label = value;
                PropertyChanged?.Invoke(nameof(Label));
            }
        }
        #endregion

        #region Property: Mode
        private ClockMode _mode = ClockMode.Time;

        [JsonPropertyName("mode")]
        public ClockMode Mode
        {
            get => _mode;
            set
            {
                if (value == ClockMode.Time)
                    State = ClockState.Time;
                if (value == _mode)
                    return;
                _mode = value;
                PropertyChanged?.Invoke(nameof(Mode));
                calculateCanShowEditing();
            }
        }
        #endregion

        #region Property: State
        private ClockState _state = ClockState.Reset;

        [JsonPropertyName("state")]
        public ClockState State
        {
            get => _state;
            set
            {
                if (value == _state)
                    return;
                _state = value;
                PropertyChanged?.Invoke(nameof(State));
                calculateCanShowEditing();
            }
        }
        #endregion

        #region Property: StartValue
        private int _startValue;

        [JsonPropertyName("startValue")]
        public int StartValue
        {
            get => _startValue;
            set
            {
                if (value == _startValue)
                    return;
                _startValue = value;
                StartValueStringHhMmSs = value.SecondsToHhMmSs();
                PropertyChanged?.Invoke(nameof(StartValue));
                if (_beforeInit || ((State == ClockState.Reset) && (Mode == ClockMode.Down)))
                {
                    ValueSeconds = value;
                    _inheritedSeconds = value;
                }
            }
        }

        private string _startValueStringHhMmSs = "00:00:00";

        [JsonIgnore]
        public string StartValueStringHhMmSs
        {
            get => _startValueStringHhMmSs;
            set
            {
                if (value == _startValueStringHhMmSs)
                    return;
                _startValueStringHhMmSs = value;
                PropertyChanged?.Invoke(nameof(StartValueStringHhMmSs));
            }
        }
        #endregion

        #region Property: ValueSeconds, ValueSecondsWithEditing
        private int _valueSeconds = -1;

        public event Action<Clock> TimeChanged;

        [JsonPropertyName("value")]
        public int ValueSeconds
        {
            get => _valueSeconds;
            set
            {
                if (value == _valueSeconds)
                    return;
                _valueSeconds = value;
                PropertyChanged?.Invoke(nameof(ValueSeconds));
                PropertyChanged?.Invoke(nameof(ValueSecondsWithEditing));
                int valueAbs = (value > 0) ? value : -value;
                string stringHhMmSs = valueAbs.SecondsToHhMmSs();
                string minus = (value >= 0) ? "" : "-";
                ValueStringHhMmSs = minus + stringHhMmSs;
                TimeChanged?.Invoke(this);
            }
        }

        [JsonIgnore]
        public int ValueSecondsWithEditing
        {
            get
            {
                if (RemoteEditMode == ClockRemoteEditMode.NotEditing)
                    return _valueSeconds;
                return _remoteEditSeconds;
            }
        }
        #endregion

        #region Property: ValueStringHhMmSs, ValueStringHhMmSsWithEditing
        private string _valueStringHhMmSs = "00:00:00";

        [JsonIgnore]
        public string ValueStringHhMmSs
        {
            get => _valueStringHhMmSs;
            set
            {
                if (value == _valueStringHhMmSs)
                    return;
                _valueStringHhMmSs = value;
                PropertyChanged?.Invoke(nameof(ValueStringHhMmSs));
                PropertyChanged?.Invoke(nameof(ValueStringHhMmSsWithEditing));
            }
        }

        [JsonIgnore]
        public string ValueStringHhMmSsWithEditing
        {
            get
            {
                
                return RemoteEditMode switch
                {
                    ClockRemoteEditMode.NotEditing => _valueStringHhMmSs,
                    ClockRemoteEditMode.EditingSeconds => $"E {_remoteEditSeconds.SecondsToHhMmSs()}",
                    ClockRemoteEditMode.EditingHhMmSs => _remoteEditHhMmSsString,
                    _ => _valueStringHhMmSs
                };
            }
        }
        #endregion

        #region Property: CanBeNegative
        private bool _canBeNegative = false;

        public bool CanBeNegative
        {
            get => _canBeNegative;
            set
            {
                if (value == _canBeNegative)
                    return;
                _canBeNegative = value;
                PropertyChanged?.Invoke(nameof(CanBeNegative));
            }
        }
        #endregion

        #region Property: BelowZero
        private bool _reachedZero = false;

        public bool ReachedZero
        {
            get => _reachedZero;
            set
            {
                if (value == _reachedZero)
                    return;
                _reachedZero = value;
                PropertyChanged?.Invoke(nameof(ReachedZero));
            }
        }
        #endregion

        #region Property: CanShowEditing
        private bool _canShowEditing = false;

        private bool CanShowEditing
        {
            get => _canShowEditing;
            set
            {
                if (value == _canShowEditing)
                    return;
                _canShowEditing = value;
                PropertyChanged?.Invoke(nameof(ValueStringHhMmSs));
                PropertyChanged?.Invoke(nameof(ValueStringHhMmSsWithEditing));
            }
        }

        private void calculateCanShowEditing()
            => CanShowEditing = (Mode == ClockMode.Down) && (State == ClockState.Reset);
        #endregion

        #region Property: RemoteEditMode
        private ClockRemoteEditMode _remoteEditMode = ClockRemoteEditMode.NotEditing;

        public ClockRemoteEditMode RemoteEditMode
        {
            get => _remoteEditMode;
            set
            {
                if (value == _remoteEditMode)
                    return;
                _remoteEditMode = value;
                PropertyChanged?.Invoke(nameof(RemoteEditMode));
                if (CanShowEditing)
                {
                    PropertyChanged?.Invoke(nameof(ValueSecondsWithEditing));
                    PropertyChanged?.Invoke(nameof(ValueStringHhMmSsWithEditing));
                }
            }
        }
        #endregion

        #region Remote edit
        private int _remoteEditSeconds = 0;
        private int _remoteEditHhMmSsDigitIndex = 0;
        private int[] _remoteEditHhMmSsDigits = new int[6];
        private string _remoteEditHhMmSsString;

        public void StartRemoteEdit(ClockRemoteEditMode remoteEditMode)
        {
            if (remoteEditMode == ClockRemoteEditMode.NotEditing)
                throw new ArgumentException(nameof(remoteEditMode));
            _remoteEditSeconds = 0;
            _remoteEditHhMmSsDigitIndex = 0;
            for (int i = 0; i < 6; i++)
                _remoteEditHhMmSsDigits[i] = 0;
            _remoteEditHhMmSsString = "--:--:--";
            RemoteEditMode = remoteEditMode;
        }

        public void EndRemoteEdit()
        {
            if (RemoteEditMode == ClockRemoteEditMode.NotEditing)
                throw new InvalidOperationException();
            StartValue = _remoteEditSeconds;
            RemoteEditMode = ClockRemoteEditMode.NotEditing;
        }

        public void CancelRemoteEdit()
        {
            RemoteEditMode = ClockRemoteEditMode.NotEditing;
        }

        public void AcceptRemoteEditDigit(int digit)
        {

            if (RemoteEditMode == ClockRemoteEditMode.NotEditing)
                throw new InvalidOperationException();
            if ((digit < 0) || (digit > 9))
                throw new ArgumentOutOfRangeException(nameof(digit));

            if (RemoteEditMode == ClockRemoteEditMode.EditingSeconds)
            {
                _remoteEditSeconds *= 10;
                _remoteEditSeconds += digit;
                if (CanShowEditing)
                {
                    PropertyChanged?.Invoke(nameof(ValueSecondsWithEditing));
                    PropertyChanged?.Invoke(nameof(ValueStringHhMmSsWithEditing));
                }
            }
            else if (RemoteEditMode == ClockRemoteEditMode.EditingHhMmSs)
            {

                _remoteEditHhMmSsDigits[_remoteEditHhMmSsDigitIndex] = digit;

                int newRemoteEditSeconds = 0;
                for (int i = 0, d = 0; i < 3; i++, d++)
                {
                    newRemoteEditSeconds *= 60;
                    newRemoteEditSeconds += _remoteEditHhMmSsDigits[d++] * 10 + _remoteEditHhMmSsDigits[d++];
                }
                _remoteEditSeconds = newRemoteEditSeconds;

                string newRemoteEditHhMmSsString = "";
                for (int i = 0; i < 6; i++)
                {
                    newRemoteEditHhMmSsString += (i > _remoteEditHhMmSsDigitIndex) ? "-" : _remoteEditHhMmSsDigits[i];
                    if ((i == 1) || (i == 3))
                        newRemoteEditHhMmSsString += ":";
                }

                _remoteEditHhMmSsDigitIndex++;

                if (_remoteEditHhMmSsDigitIndex == 6)
                {
                    EndRemoteEdit();
                }
                else if (CanShowEditing)
                {
                    PropertyChanged?.Invoke(nameof(ValueSecondsWithEditing));
                    PropertyChanged?.Invoke(nameof(ValueStringHhMmSsWithEditing));
                }
            }

        }
        #endregion

        #region Basic operations
        public void Start()
        {
            if (Mode == ClockMode.Time)
                throw new InvalidOperationException("'Start' operation cannot be executed on a clock with 'time' mode.");
            if (State == ClockState.Running)
                return;
            setStartedAt();
            State = ClockState.Running;
        }

        public void Stop()
        {
            if (Mode == ClockMode.Time)
                throw new InvalidOperationException("'Stop' operation cannot be executed on a clock with 'time' mode.");
            if (State == ClockState.Stopped)
                return;
            _inheritedSeconds = ValueSeconds;
            State = ClockState.Stopped;
        }

        public void Reset()
        {
            if (Mode == ClockMode.Time)
                throw new InvalidOperationException("'Reset' operation cannot be executed on a clock with 'time' mode.");
            setStartedAt();
            int newValue = (Mode == ClockMode.Down) ? StartValue : 0;
            _inheritedSeconds = newValue;
            ValueSeconds = newValue;
            ReachedZero = false;
            if ((State == ClockState.Stopped) || (State == ClockState.Expired))
                State = ClockState.Reset;
        }
        #endregion

        #region Timing
        private void tick()
        {

            DateTime now = DateTime.Now;
            if (Mode == ClockMode.Time)
            {
                ValueSeconds = now.ToSeconds();
            }
            else if (State == ClockState.Running)
            {
                long ticksDiff = now.Ticks - _startedAtTicks;
                int seconds = (int)(ticksDiff / TimeSpan.TicksPerSecond);
                if (Mode == ClockMode.Up)
                {
                    ValueSeconds = _inheritedSeconds + seconds;
                }
                else if (Mode == ClockMode.Down)
                {
                    int newValue = _inheritedSeconds - seconds;
                    if (!CanBeNegative && (newValue < 0))
                        newValue = 0;
                    ValueSeconds = newValue;
                    if (newValue <= 0)
                    {
                        ReachedZero = true;
                        if (!CanBeNegative)
                            State = ClockState.Expired;
                    }
                }
            }
        }

        private void setStartedAt()
            => _startedAtTicks = DateTime.Now.Ticks - TimeSpan.TicksPerMillisecond * 500;

        private long _startedAtTicks = DateTime.Now.Ticks;
        private int _inheritedSeconds = 0;

        static Clock()
        {
            _tickThread = new Thread(tickThreadMethod)
            {
                IsBackground = true
            };
            _tickThread.Start();
        }

        private static Thread _tickThread;

        private static void tickThreadMethod()
        {
            int secsPrev = DateTime.Now.Second - 1;
            Thread.Sleep(1000 - DateTime.Now.Millisecond);
            while (true)
            {
                int secsNow = DateTime.Now.Second;
                if ((secsNow > secsPrev) || (secsNow < secsPrev - 30))
                {
                    lock (_clockList)
                    {
                        _clockList.Foreach(c => c.tick());
                    }
                }
                secsPrev = secsNow;
                Thread.Sleep(Math.Min(1000 - DateTime.Now.Millisecond, 250));
            }
        }

        private static List<Clock> _clockList = new();
        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedDelegate PropertyChanged;

        public void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(propertyName);
        #endregion


    }
}
