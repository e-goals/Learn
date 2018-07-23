using System;

public partial class TimePrecision : System.Web.UI.Page
{
    protected string output = "";

    private void WriteLine(string text)
    {
        output += text + "<br />";
    }

    private void WriteLine(string format, params object[] args)
    {
        output += string.Format(format + "<br />", args);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            double precision0 = 0, precision1 = 0, precision2 = 0;
            int times = 100;

            for (int i = 0; i < times; i++)
            {
                precision0 += Test0();
            }

            for (int i = 0; i < times; i++)
            {
                precision1 += Test1();
            }

            for (int i = 0; i < times; i++)
            {
                precision2 += Test2();
            }

            WriteLine("System.Environment.TickCount: {0:0.000} ms", precision0 / times);
            WriteLine("System.DateTime.Now.Ticks: {0:0.0000} ms", precision1 / times);
            WriteLine("EzGoal.DateTime.Now.Ticks: {0:0.0} μs", precision2 / times);
        }
    }

    private void Test()
    {
        var beginTime = EasyGoal.Datetime.Now;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        DateTime dt0, dt1;
        TimeSpan ts;

        while (stopwatch.Elapsed.TotalSeconds < 5)
        {
            dt0 = EasyGoal.Datetime.Now;
            ts = stopwatch.Elapsed;

            dt1 = beginTime + ts;
            TimeSpan diff = dt0 - dt1;

            output += string.Format("Difference: {0:0.0000} ms<br />", diff.TotalMilliseconds);

            System.Threading.Thread.Sleep(1000);
        }
    }

    private int Test0()
    {
        int t0 = Environment.TickCount;
        int t1 = Environment.TickCount;
        while (t0 == t1)
        {
            t1 = Environment.TickCount;
        }
        return (t1 - t0);
    }

    private double Test1()
    {
        long t0 = DateTime.Now.Ticks;
        long t1 = DateTime.Now.Ticks;
        while (t0 == t1)
        {
            t1 = DateTime.Now.Ticks;
        }
        return (t1 - t0) / 10000.0;
    }

    private double Test2()
    {
        long t0 = EasyGoal.Datetime.Now.Ticks;
        long t1 = EasyGoal.Datetime.Now.Ticks;
        while (t0 == t1)
        {
            t1 = EasyGoal.Datetime.Now.Ticks;
        }
        return (t1 - t0) / 10.0;
    }

}