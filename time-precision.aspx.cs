using System;

public partial class TimePrecision : System.Web.UI.Page
{
    protected System.Text.StringBuilder outputBuilder = new System.Text.StringBuilder("", 5120);

    private void WriteLine(string text)
    {
        outputBuilder.Append(text);
        outputBuilder.Append("<br />");
    }

    private void WriteLine(string format, params object[] args)
    {
        outputBuilder.AppendFormat(format, args);
        outputBuilder.Append("<br />");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //TestDifference();
            TestResolution();
            TestPrecisions();
        }
    }

    private void TestResolution()
    {
        DateTime t1, t2, t3, t4;
        t1 = System.DateTime.Now;
        t2 = System.DateTime.Now;
        t3 = EZGoal.Datetime.Now;
        t4 = EZGoal.Datetime.Now;
        WriteLine(t1.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
        WriteLine(t2.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
        WriteLine(t3.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
        WriteLine(t4.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
    }

    private void TestDifference()
    {
        var beginTime = EZGoal.Datetime.Now;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        DateTime dt0, dt1;
        TimeSpan ts;

        while (stopwatch.Elapsed.TotalSeconds < 20)
        {
            dt0 = EZGoal.Datetime.Now;
            ts = stopwatch.Elapsed;

            dt1 = beginTime + ts;
            TimeSpan d = dt0 - dt1;

            WriteLine("Difference: {0:0.0000} ms", d.TotalMilliseconds);

            System.Threading.Thread.Sleep(1000);
        }
    }


    private void TestPrecisions()
    {
        double precision0 = 0, precision1 = 0, precision2 = 0;
        int times = 100;

        for (int i = 0; i < times; i++)
        {
            precision0 += TestPrecision0();
        }

        for (int i = 0; i < times; i++)
        {
            precision1 += TestPrecision1();
        }

        for (int i = 0; i < times; i++)
        {
            precision2 += TestPrecision2();
        }

        WriteLine("System.Environment.TickCount: {0:0.000} ms", precision0 / times);
        WriteLine("System.DateTime.Now.Ticks: {0:0.0000} ms", precision1 / times);
        WriteLine("EzGoal.Datetime.Now.Ticks: {0:0.0} μs", precision2 / times);
    }

    private int TestPrecision0()
    {
        int t0 = Environment.TickCount;
        int t1 = Environment.TickCount;
        while (t0 == t1)
        {
            t1 = Environment.TickCount;
        }
        return (t1 - t0);
    }

    private double TestPrecision1()
    {
        long t0 = DateTime.Now.Ticks;
        long t1 = DateTime.Now.Ticks;
        while (t0 == t1)
        {
            t1 = DateTime.Now.Ticks;
        }
        return (t1 - t0) / 10000.0;
    }

    private double TestPrecision2()
    {
        long t0 = EZGoal.Datetime.Now.Ticks;
        long t1 = EZGoal.Datetime.Now.Ticks;
        while (t0 == t1)
        {
            t1 = EZGoal.Datetime.Now.Ticks;
        }
        return (t1 - t0) / 10.0;
    }

}