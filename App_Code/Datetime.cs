using System.Runtime.InteropServices;

namespace EasyGoal
{
    public static class Datetime
    {
        public static bool IsAvailable { get; private set; }

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.Winapi)]
        private static extern void GetSystemTimePreciseAsFileTime(out long filetime);

        static Datetime()
        {
            try
            {
                long filetime;
                GetSystemTimePreciseAsFileTime(out filetime);
                IsAvailable = true;
            }
            catch (System.EntryPointNotFoundException)
            {
                IsAvailable = false;
            }
        }

        public static System.DateTime Now
        {
            get
            {
                if (!IsAvailable)
                {
                    throw new System.InvalidOperationException("High resolution clock isn't available.");
                }
                long filetime;
                GetSystemTimePreciseAsFileTime(out filetime);
                return System.DateTime.FromFileTime(filetime);
            }
        }

        public static System.DateTime UtcNow
        {
            get
            {
                if (!IsAvailable)
                {
                    throw new System.InvalidOperationException("High resolution clock isn't available.");
                }
                long filetime;
                GetSystemTimePreciseAsFileTime(out filetime);
                return System.DateTime.FromFileTimeUtc(filetime);
            }
        }

    }
}