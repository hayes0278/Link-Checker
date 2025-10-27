using Link.Checker.ClassLibrary;

Console.WriteLine("=== Hello, I am Link Checker! ===");
Console.WriteLine("Please enter your url to link check below.");

try
{
    string url = Console.ReadLine().Trim();

    if (!string.IsNullOrEmpty(url))
    {
        HtmlFetcher htmlFetcher = new HtmlFetcher(url);

        LinkChecker linkChecker = new LinkChecker();
        linkChecker.CheckLinks(url);
        linkChecker.CheckHeadStatusCode(url);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}
finally
{
    Console.WriteLine("Thank you for using the link checker.");
}