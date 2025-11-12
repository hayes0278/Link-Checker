using Link.Checker.ClassLibrary;

string appName = "Link Checker";

Console.WriteLine($"====== Hello, I am {appName}! ======");
Console.WriteLine("Please enter your url to link check below.");
Console.WriteLine("");

try
{
    string url = Console.ReadLine().Trim();

    if (!string.IsNullOrEmpty(url))
    {
        HtmlFetcher htmlFetcher = new HtmlFetcher(url);

        LinkCheckerApp linkChecker = new LinkCheckerApp();
        linkChecker.CheckLinks(url);
        linkChecker.CheckHeadStatusCode(url);
    }
}
catch (Exception e)
{
    Console.WriteLine("");
    Console.WriteLine($"Error in app: {e.ToString()}.");
}
finally
{
    Console.WriteLine("");
    Console.WriteLine($"Thank you for using the {appName}.");
}