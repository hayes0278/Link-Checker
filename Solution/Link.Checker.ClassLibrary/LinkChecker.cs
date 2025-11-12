using HtmlAgilityPack;

namespace Link.Checker.ClassLibrary
{
    public class LinkCheckerApp
    {

        string _defaultProtocol = "https://";
        List<string> _links = new List<string>();
        HtmlDocument _document = new HtmlDocument();

        public async Task<List<string>> ExtractLinksFromUrl(string url)

        {
            Action backgroundWork = () =>
            {
                Console.WriteLine($"Get HTML Task started on thread ID: {Thread.CurrentThread.ManagedThreadId}");

                if (!url.Contains("http") && !url.Contains("https"))
                {
                    url = _defaultProtocol + url;
                }

                HtmlFetcher fetch = new HtmlFetcher(url);
                var html = fetch.GetHtmlAsStringAsync(url);
                _document.LoadHtml(html.ToString());
            };

            Task task = Task.Run(backgroundWork);

            task.Wait();

            Console.WriteLine("Get HTML Task completed.");

            if (_document != null)
            {
                var nodes = _document.DocumentNode.SelectNodes("//a[@href]");

                if (nodes != null)
                {
                    foreach (var link in nodes)
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
            }

            Console.WriteLine("Main thread finished.");

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

        public bool CheckLinks(string url)
        {
            Console.WriteLine($"Checking links for {url}");

            ExtractLinksFromUrl(url);

            if (_links != null)
            {
                foreach (string linkUrl in _links)
                {
                    Console.WriteLine($"Checking link {linkUrl}");
                    GetHeadStatusCode(linkUrl);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckHeadStatusCode(string url)
        {
            Console.WriteLine($"Checking status code for {url}");

            GetHeadStatusCode(url);

            return true;

        }
    }
}
