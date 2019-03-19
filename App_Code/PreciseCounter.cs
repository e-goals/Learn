using System;

namespace EZGoal
{
    public enum TimeUnit
    {
        Second, MilliSecond, MicroSecond, NanoSecond
    }

    public class PreciseCounter
    {
        public static long Frequency;

        private long COUNTER_0 = 0L;
        private long COUNTER_1 = 0L;
        private long initialCounter;
        private long currentCounter;
        private bool isRunning = false;

        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long counter);

        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long frequency);

        static PreciseCounter()
        {
            if (!QueryPerformanceFrequency(out Frequency))
            {
                throw new System.ComponentModel.Win32Exception();
            }
        }

        public static long Counter
        {
            get
            {
                long counter;
                QueryPerformanceCounter(out counter);
                return counter;
            }
        }

        public PreciseCounter()
        {
            QueryPerformanceCounter(out initialCounter);
        }

        public void Reset()
        {
            QueryPerformanceCounter(out initialCounter);
        }

        public void Starting()
        {
            isRunning = true;
            QueryPerformanceCounter(out COUNTER_0);
        }

        public void Stopping()
        {
            if (isRunning)
            {
                QueryPerformanceCounter(out COUNTER_1);
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
                QueryPerformanceCounter(out currentCounter);
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
                throw new Exception("Must call Stopping() before calling TimeInSpan().");
            }
        }

        public decimal LifeTime(TimeUnit unit)
        {
            return (((decimal)LifeTicks * Multiplier(unit)) / (decimal)Frequency);
        }

        public decimal SpanTime(TimeUnit unit)
        {
            return (((decimal)SpanTicks * Multiplier(unit)) / (decimal)Frequency);
        }

        public static decimal TimeSpan(long counter0, long counter1, TimeUnit unit)
        {
            return (((decimal)(counter1 - counter0) * Multiplier(unit)) / (decimal)Frequency);
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