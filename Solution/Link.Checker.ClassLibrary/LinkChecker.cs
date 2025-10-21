using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Xml;

namespace Link.Checker.ClassLibrary
{
    public class LinkChecker
    {

        string _url = null;
        List<string> _links = new List<string>();

        public async Task<List<string>> ExtractLinksFromUrl(string url)

        {
            Console.WriteLine($"Main thread ID: {Thread.CurrentThread.ManagedThreadId}");

            // Define the work to be performed in the background task
            Action backgroundWork = () =>
            {
                Console.WriteLine($"Task started on thread ID: {Thread.CurrentThread.ManagedThreadId}");

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
                                _links.Add(absoluteUri.ToString());
                            }

                            else
                            {
                                _links.Add(href);
                            }
                        }
                    }
                }

                Thread.Sleep(2000);
                Console.WriteLine("Task completed.");
            };

            // Run the task immediately on a thread pool thread
            Task task = Task.Run(backgroundWork);

            Console.WriteLine("Main thread continues execution immediately after starting the task.");

            // Optionally, wait for the task to complete if necessary
            task.Wait();

            Console.WriteLine("Main thread finished.");

            return _links;
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
