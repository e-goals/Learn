using System.Collections.Generic;
using System.Web.UI;

public class PageEvents
{
    private static Dictionary<long, string> dictionary = new Dictionary<long, string>();

    public static string Output
    {
        get
        {
            var sb = new System.Text.StringBuilder("<span>Page Events:</span><br />");
            foreach (KeyValuePair<long, string> pair in dictionary)
            {
                sb.Append("<span>");
                sb.Append(pair.Key);
                sb.Append(" - ");
                sb.Append(pair.Value);
                sb.Append("</span><br />");
            }
            return sb.ToString();
        }
    }

    private PageEvents() { }

    public static void Reset()
    {
        dictionary.Clear();
    }

    public static void Trace(Page page)
    {
        long counter = EZGoal.PreciseCounter.Counter;
        var st = new System.Diagnostics.StackTrace(true);
        var mb = st.GetFrame(1).GetMethod();
        dictionary.Add(counter, string.Format("{0}.{1}", mb.DeclaringType.FullName, mb.Name));
        page.Trace.Warn(string.Format("{0} - {1}.{2}", counter, mb.DeclaringType.FullName, mb.Name));
    }

    public static void Trace(UserControl control)
    {
        long counter = EZGoal.PreciseCounter.Counter;
        var st = new System.Diagnostics.StackTrace(true);
        var mb = st.GetFrame(1).GetMethod();
        dictionary.Add(counter, string.Format("{0}.{1}", mb.DeclaringType.FullName, mb.Name));
        control.Trace.Warn(string.Format("{0} - {1}.{2}", counter, mb.DeclaringType.FullName, mb.Name));
    }

}