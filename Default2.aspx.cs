using System;

public partial class Default2 : System.Web.UI.Page
{
    public DateTime dt01, dt02, dt11, dt12;
    public int temp = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //dt01 = PreciseCounter.DateTimeUTC();
            //dt02 = PreciseCounter.DateTimeUTC();
            //dt11 = DateTime.UtcNow;
            //for (int i = 0; i < 10000; i++)
            //    temp = 100 * 1000;
            //dt12 = DateTime.UtcNow;

            var start = PreciseCounter.DateTimeUTC();
            var sw = System.Diagnostics.Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < 10)
            {
                DateTime nowBasedOnStopwatch = start + sw.Elapsed;
                TimeSpan diff = PreciseCounter.DateTimeUTC() - nowBasedOnStopwatch;
                Response.Write(string.Format("Diff: {0:0.000} ms<br />", diff.TotalMilliseconds));
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}