using Link.Checker.ClassLibrary;

Console.WriteLine("=== Hello, I am Link Checker! ===");
Console.WriteLine("Please enter your url to link check below.");

string url = Console.ReadLine();

if (!string.IsNullOrEmpty(url))
{
    HtmlFetcher htmlFetcher = new HtmlFetcher(url);

    LinkChecker linkChecker = new LinkChecker();
    linkChecker.CheckLinks(url);
    linkChecker.CheckHeadStatusCode(url);
}