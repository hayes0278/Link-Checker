
using Link.Checker.ClassLibrary;

Console.WriteLine("Hello, Link Checker!");
Console.WriteLine("Please enter your url to link check below.");

string url = Console.ReadLine();

LinkChecker linkChecker = new LinkChecker();
linkChecker.CheckLinks(url);
