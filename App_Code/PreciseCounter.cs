using System;
using System.Runtime.InteropServices;

namespace EZGoal
{
    public enum TimeUnit
    {
        Second, MilliSecond, MicroSecond, NanoSecond
    }

    public class PreciseCounter
    {
        private static long frequency;
        public static long Frequency
        {
            get { return frequency; }
        }

        public static bool IsHighResolution { get; private set; }

        private long COUNTER_0 = 0L;
        private long COUNTER_1 = 0L;
        private long initialCounter;
        private long currentCounter;
        private bool isRunning = false;

        [DllImport("Kernel32.dll", EntryPoint = "QueryPerformanceCounter")]
        private static extern bool GetCounter(out long counter);

        [DllImport("Kernel32.dll", EntryPoint = "QueryPerformanceFrequency")]
        private static extern bool GetFrequency(out long frequency);

        static PreciseCounter()
        {
            if (!GetFrequency(out frequency))
            {
                IsHighResolution = false;
                // throw new System.ComponentModel.Win32Exception();
            }
            else
            {
                IsHighResolution = true;
            }
        }

        public static long Counter
        {
            get
            {
                long counter;
                GetCounter(out counter);
                return counter;
            }
        }

        public PreciseCounter()
        {
            GetCounter(out initialCounter);
        }

        public void Reset()
        {
            GetCounter(out initialCounter);
        }

        public void Starting()
        {
            isRunning = true;
            GetCounter(out COUNTER_0);
        }

        public void Stopping()
        {
            if (isRunning)
            {
                GetCounter(out COUNTER_1);
                isRunning = false;
            }
            else
            {
                throw new Exception("Must call Starting() before calling Stopping().");
            }
        }

        public long LifeTicks
        {
            get
            {
                GetCounter(out currentCounter);
                return currentCounter - initialCounter;
            }
        }

        public long SpanTicks
        {
            get
            {
                if (!isRunning)
                {
                    return COUNTER_1 - COUNTER_0;
                }
                throw new Exception("Must call Stopping() before calling SpanTicks().");
            }
        }

        public decimal LifeTime(TimeUnit unit)
        {
            return (((decimal)LifeTicks * Multiplier(unit)) / (decimal)frequency);
        }

        public decimal SpanTime(TimeUnit unit)
        {
            return (((decimal)SpanTicks * Multiplier(unit)) / (decimal)frequency);
        }

        public static decimal TimeSpan(long counter0, long counter1, TimeUnit unit)
        {
            return (((decimal)(counter1 - counter0) * Multiplier(unit)) / (decimal)frequency);
        }

        private static decimal Multiplier(TimeUnit unit)
        {
            decimal multiplier = new decimal(1.0);
            switch (unit)
            {
                case TimeUnit.Second:
                    break;
                case TimeUnit.MilliSecond:
                    multiplier = new decimal(1.0e3);
                    break;
                case TimeUnit.MicroSecond:
                    multiplier = new decimal(1.0e6);
                    break;
                case TimeUnit.NanoSecond:
                    multiplier = new decimal(1.0e9);
                    break;
            }
            return multiplier;
        }
    }

}