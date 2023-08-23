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

        #region Property: StartValueSeconds, StartValueStringHhMmSs
        private int _startValueSeconds;

        [JsonPropertyName("startValue")]
        public int StartValueSeconds
        {
            get => _startValueSeconds;
            set
            {
                if (value == _startValueSeconds)
                    return;
                _startValueSeconds = value;
                PropertyChanged?.Invoke(nameof(StartValueSeconds));
                StartValueStringHhMmSs = value.SecondsToHhMmSs();
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

        #region Property: OffsetSeconds, OffsetStringHhMmSs
        private int _offsetSeconds;

        [JsonPropertyName("offset")]
        public int OffsetSeconds
        {
            get => _offsetSeconds;
            set
            {
                if (value == _offsetSeconds)
                    return;
                _offsetSeconds = value;
                PropertyChanged?.Invoke(nameof(OffsetSeconds));
                OffsetStringHhMmSs = value.SecondsToHhMmSs();
            }
        }

        private string _offsetStringHhMmSs = "00:00:00";

        [JsonIgnore]
        public string OffsetStringHhMmSs
        {
            get => _offsetStringHhMmSs;
            set
            {
                if (value == _offsetStringHhMmSs)
                    return;
                _offsetStringHhMmSs = value;
                PropertyChanged?.Invoke(nameof(OffsetStringHhMmSs));
            }
        }
        #endregion

        #region Property: UntilSeconds, UntilStringHhMmSs, UntilTimestamp; method: updateUntilTimestamp
        private int _untilSeconds;

        [JsonPropertyName("until")]
        public int UntilSeconds
        {
            get => _untilSeconds;
            set
            {
                if (value == _untilSeconds)
                    return;
                _untilSeconds = value;
                PropertyChanged?.Invoke(nameof(UntilSeconds));
                UntilStringHhMmSs = value.SecondsToHhMmSs();
                if (!_beforeInit)
                    updateUntilTimestamp();
            }
        }

        private string _untilStringHhMmSs = "00:00:00";

        [JsonIgnore]
        public string UntilStringHhMmSs
        {
            get => _untilStringHhMmSs;
            set
            {
                if (value == _untilStringHhMmSs)
                    return;
                _untilStringHhMmSs = value;
                PropertyChanged?.Invoke(nameof(UntilStringHhMmSs));
            }
        }

        private DateTime _untilTimestamp;

        [JsonPropertyName("untilTimestamp")]
        public long UntilTimestamp // for persistence
        {
            get => _untilTimestamp.ToUniversalTime().ToBinary();
            set
            {
                _untilTimestamp = DateTime.FromBinary(value).ToLocalTime();
                if ((Mode == ClockMode.Until) && (_untilTimestamp <= DateTime.Now))
                {
                    ReachedZero = true;
                    if (!CanBeNegative)
                        State = ClockState.Expired;
                }
            }
        }

        private void updateUntilTimestamp()
        {
            DateTime now = DateTime.Now;
            DateTime until = now - now.TimeOfDay + new TimeSpan(UntilSeconds * TimeSpan.TicksPerSecond);
            if (until < now)
                until += TimeSpan.FromDays(1);
            _untilTimestamp = until;
        }

        #endregion

        #region Property: ValueSeconds, ValueStringHhMmSs; event: TimeChanged
        private int _valueSeconds = -1;

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
                ValueStringHhMmSs = value.SecondsToHhMmSs();
                TimeChanged?.Invoke(this);
                if (_beforeInit && (Mode == ClockMode.Down))
                    _inheritedSeconds = value;
            }
        }

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
            }
        }

        public event Action<Clock> TimeChanged;
        #endregion

        #region Property: Enabled
        private bool _enabled = true;

        [JsonPropertyName("enabled")]
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (value == _enabled)
                    return;
                _enabled = value;
                PropertyChanged?.Invoke(nameof(Enabled));
                if (((Mode == ClockMode.Time) || (Mode == ClockMode.Until)) && (value == false))
                {
                    ValueSeconds = -1;
                    ValueStringHhMmSs = "XX:XX:XX";
                    State = ClockState.Disabled;
                }
            }
        }
        #endregion

        #region Property: CanBeNegative
        private bool _canBeNegative = false;

        [JsonPropertyName("canBeNegative")]
        public bool CanBeNegative
        {
            get => _canBeNegative;
            set
            {
                if (value == _canBeNegative)
                    return;
                _canBeNegative = value;
                PropertyChanged?.Invoke(nameof(CanBeNegative));
                if (value && (State == ClockState.Expired))
                    State = ClockState.Running;
            }
        }
        #endregion

        #region Property: BelowZero
        private bool _reachedZero = false;

        [JsonIgnore]
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
            }
        }

        private void calculateCanShowEditing()
            => CanShowEditing = (Mode == ClockMode.Down) && (State == ClockState.Reset);
        #endregion

        #region Property: RemoteEditMode
        private ClockRemoteEditMode _remoteEditMode = ClockRemoteEditMode.NotEditing;

        [JsonIgnore]
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
                    //PropertyChanged?.Invoke(nameof(ValueSecondsWithEditing));
                    //PropertyChanged?.Invoke(nameof(ValueStringHhMmSsWithEditing));
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
            StartValueSeconds = _remoteEditSeconds;
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
                    // TODO
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
                    //PropertyChanged?.Invoke(nameof(ValueSecondsWithEditing));
                    //ropertyChanged?.Invoke(nameof(ValueStringHhMmSsWithEditing));
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
            if (Mode == ClockMode.Down)
            {
                _inheritedSeconds = StartValueSeconds;
                ValueSeconds = StartValueSeconds;
            }
            else if (Mode == ClockMode.Up)
            {
                _inheritedSeconds = 0;
                ValueSeconds = 0;
            }
            if (!_beforeInit)
                updateUntilTimestamp();
            ReachedZero = false;
            if (((State == ClockState.Stopped) || (State == ClockState.Expired)) && (Mode != ClockMode.Until))
            {
                State = ClockState.Reset;
            }
            else if (Mode == ClockMode.Until)
            {
                State = ClockState.Running;
                calcValueUntil();
            }
        }
        #endregion
        
        #region Timing
        private void tick()
        {
            DateTime now = DateTime.Now;
            if (Mode == ClockMode.Time)
            {
                if (Enabled)
                {
                    ValueSeconds = now.ToSeconds() + OffsetSeconds;
                    State = ClockState.Time;
                }
            }
            else if (Mode == ClockMode.Until)
            {
                calcValueUntil();
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

        private void calcValueUntil()
        {
            if (Enabled)
            {
                DateTime now = DateTime.Now;
                TimeSpan diff = _untilTimestamp - now;
                int seconds = (int)diff.TotalSeconds;
                if (!CanBeNegative && (seconds < 0))
                    seconds = 0;
                ValueSeconds = seconds;
                if (seconds <= 0)
                {
                    ReachedZero = true;
                    State = CanBeNegative ? ClockState.Running : ClockState.Expired;
                }
                else
                {
                    State = ClockState.Running;
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

        internal void Removed()
        {
            // TODO
        }

    }
}
