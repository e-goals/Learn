using System.Runtime.InteropServices;

namespace EZGoal
{
    public static class Datetime
    {
        public static bool IsHighResolution { get; private set; }

        [DllImport("Kernel32.dll", EntryPoint = "GetSystemTimePreciseAsFileTime")]
        private static extern void GetTime(out long filetime);

        static Datetime()
        {
            try
            {
                long filetime;
                GetTime(out filetime);
                IsHighResolution = true;
            }
            catch (System.EntryPointNotFoundException)
            {
                IsHighResolution = false;
            }
        }

        public static System.DateTime Now
        {
            get
            {
                if (!IsHighResolution)
                {
                    // throw new System.InvalidOperationException("High resolution clock isn't available.");
                    return System.DateTime.Now;
                }
                long filetime;
                GetTime(out filetime);
                return System.DateTime.FromFileTime(filetime);
            }
        }

        public static System.DateTime UtcNow
        {
            get
            {
                if (!IsHighResolution)
                {
                    // throw new System.InvalidOperationException("High resolution clock isn't available.");
                    return System.DateTime.UtcNow;
                }
                long filetime;
                GetTime(out filetime);
                return System.DateTime.FromFileTimeUtc(filetime);
            }
        }

    }

}