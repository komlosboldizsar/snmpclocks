namespace SNMPclocks.Model
{
    internal static class ClockHelpers
    {

        public static string SecondsToHhMmSs(this int seconds)
        {
            int hours = seconds / 3600;
            seconds -= hours * 3600;
            int minutes = seconds / 60;
            seconds -= minutes * 60;
            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public static int ToSeconds(this DateTime dt)
            => dt.Hour * 3600 + dt.Minute * 60 + dt.Second;

    }
}
