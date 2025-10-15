using HtmlAgilityPack;
using System.Xml;

namespace Link.Checker.ClassLibrary
{
    public class LinkChecker
    {

        string _url = null;
        List<string> _links = new List<string>();

        public List<string> ExtractLinksFromUrl(string url)

        {
            var links = new List<string>();
            using (var httpClient = new HttpClient())
            {
                var html = httpClient.GetStringAsync(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(html.ToString());

                foreach (var link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    string href = link.GetAttributeValue("href", string.Empty);

                    if (!string.IsNullOrEmpty(href))
                    {
                        // Convert relative URLs to absolute URLs
                        if (Uri.TryCreate(new Uri(url), href, out Uri absoluteUri))
                        {
                            links.Add(absoluteUri.ToString());
                        }

                        else
                        {
                            links.Add(href);
                        }
                    }
                }
            }

            _links = links;

            return links;

        }

        public static async Task GetHeadStatusCode(string url)
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

        public LinkChecker()
        {

        }

        public void CheckLinks(string url)
        {
            Console.WriteLine($"Checking links for {url}");

            ExtractLinksFromUrl(url);

            foreach (var link in _links) 
            {
                GetHeadStatusCode(link);
            }

        }
    }
}
