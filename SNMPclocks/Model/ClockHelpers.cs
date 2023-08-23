namespace SNMPclocks.Model
{
    internal static class ClockHelpers
    {

        public static string SecondsToHhMmSs(this int seconds)
        {
            int secondsAbs = Math.Abs(seconds);
            int hours = secondsAbs / 3600;
            secondsAbs -= hours * 3600;
            int minutes = secondsAbs / 60;
            secondsAbs -= minutes * 60;
            string minus = (seconds < 0) ? "-" : string.Empty;
            return $"{minus}{hours:D2}:{minutes:D2}:{secondsAbs:D2}";
        }

        public static int ToSeconds(this DateTime dt)
            => dt.Hour * 3600 + dt.Minute * 60 + dt.Second;

    }
}
