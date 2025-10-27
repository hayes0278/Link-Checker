using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Xml;

namespace Link.Checker.ClassLibrary
{
    public class LinkChecker
    {

        string _url = null;
        List<string> _links = new List<string>();
        HtmlDocument _doc = new HtmlDocument();

        public async Task<List<string>> ExtractLinksFromUrl(string url)

        {
            Action backgroundWork = () =>
            {
                Console.WriteLine($"Get HTML Task started on thread ID: {Thread.CurrentThread.ManagedThreadId}");

                HtmlFetcher fetch = new HtmlFetcher(url);
                var html = fetch.GetHtmlAsStringAsync(url);
                _doc.LoadHtml(html.ToString());

                Console.WriteLine("Get HTML Task completed.");
            };

            Task task = Task.Run(backgroundWork);

            task.Wait();

            if (_doc != null)
            {
                foreach (var link in _doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    string href = link.GetAttributeValue("href", string.Empty);

                    if (!string.IsNullOrEmpty(href))
                    {
                        // Convert relative URLs to absolute URLs
                        if (Uri.TryCreate(new Uri(url), href, out Uri absoluteUri))
                        {
                            _links.Add(absoluteUri.ToString());
                        }

                        else
                        {
                            _links.Add(href);
                        }
                    }
                }
            }

            Console.WriteLine("Main thread finished.");

            Console.WriteLine(_doc.BackwardCompatibility);

            return _links;
        }

        public async Task GetHeadStatusCode(string url)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Head, url);
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"OK - {request.RequestUri}");
                }
                else
                {
                    Console.WriteLine($"BAD - {request.RequestUri} - ({response.StatusCode})");
                }
            }
        }

        public void CheckLinks(string url)
        {
            Console.WriteLine($"Checking links for {url}");

            ExtractLinksFromUrl(url);

            foreach (string linkUrl in _links)
            {
                Console.WriteLine($"Checking link {linkUrl}");
                GetHeadStatusCode(linkUrl);
            }

        }

        public void CheckHeadStatusCode(string url)
        {
            Console.WriteLine($"Checking status code for {url}");

            GetHeadStatusCode(url);

        }
    }
}
