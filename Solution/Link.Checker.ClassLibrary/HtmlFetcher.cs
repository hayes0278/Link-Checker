using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;

namespace Link.Checker.ClassLibrary
{
    public class HtmlFetcher
    {
        private readonly HttpClient _httpClient;

        public HtmlFetcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HtmlFetcher(string url)
        {
            _httpClient = new HttpClient();

            Console.WriteLine($"Main thread ID: {Thread.CurrentThread.ManagedThreadId}");

            // Define the work to be performed in the background task
            Action backgroundWork = () =>
            {
                Console.WriteLine($"Task started on thread ID: {Thread.CurrentThread.ManagedThreadId}");
                




                Thread.Sleep(2000);
                Console.WriteLine("Task completed.");
            };

            // Run the task immediately on a thread pool thread
            Task task = Task.Run(backgroundWork);

            Console.WriteLine("Main thread continues execution immediately after starting the task.");

            // Optionally, wait for the task to complete if necessary
            task.Wait();

            Console.WriteLine("Main thread finished.");
        }

        public async Task<string> GetHtmlAsStringAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status is not a success code.
                string htmlContent = await response.Content.ReadAsStringAsync();
                return htmlContent;
            }
            catch (HttpRequestException e)
            {
                // Handle exceptions related to HTTP requests (e.g., network issues, invalid URL)
                Console.WriteLine($"URL input error: {e.Message}");
                return null;
            }
        }

        public static void Main()
        {
            Console.WriteLine($"Main thread ID: {Thread.CurrentThread.ManagedThreadId}");

            // Define the work to be performed in the background task
            Action backgroundWork = () =>
            {
                Console.WriteLine($"Task started on thread ID: {Thread.CurrentThread.ManagedThreadId}");
                // Simulate a long-running operation
                Thread.Sleep(2000);
                Console.WriteLine("Task completed.");
            };

            // Run the task immediately on a thread pool thread
            Task task = Task.Run(backgroundWork);

            Console.WriteLine("Main thread continues execution immediately after starting the task.");

            // Optionally, wait for the task to complete if necessary
            task.Wait();

            Console.WriteLine("Main thread finished.");
        }
    }
}
