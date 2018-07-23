using System;

public partial class TimePrecision : System.Web.UI.Page
{
    protected string output = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        double precision1 = 0, precision2 = 0;
        int times = 100;
        for (int i = 0; i < times; i++)
        {
            precision1 += Test1();
        }

        for (int i = 0; i < times; i++)
        {
            precision2 += Test2();
        }

        output = string.Format("System.DateTime.Now.Ticks: \t{0:0.0000} \tms<br />", precision1 / times);
        output += string.Format("EzGoal.DateTime.Now.Ticks: \t{0:0.0} \tμs<br />", precision2 / times);
    }

    private void Test0()
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