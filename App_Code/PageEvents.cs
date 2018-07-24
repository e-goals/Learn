
public class PageEvents
{
    private static string chain = "Page Events:<br />";

    private PageEvents() { }

    public static void Append(long counter, string className, string eventName)
    {
        chain += string.Format("{0} - {1}.{2}<br />", counter, className, eventName);
    }

    public static void Reset()
    {
        chain = "Page Events:<br />";
    }

    public static string GetChain()
    {
        return chain;
    }
}