using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace restapiwithhttpclient
{
    class Program
    {
        // Create one HttpClient object for the entire application
        static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

            Console.WriteLine("** Testing REST API Calls with HttpClient **\n");

            GetAllPostsAsync().Wait();
            CreatePostAsync().Wait();
            UpdatePostAsync().Wait();
            DeletePostAsync().Wait();

            Console.WriteLine("\n** All operations completed successfully! **");
        }

        // GET: Fetch all posts
        static async Task GetAllPostsAsync()
        {
            Console.WriteLine("GET: Fetching posts...");

            HttpResponseMessage response = await client.GetAsync("posts");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();

                Console.WriteLine("\nGET Response:\n" +
                    data.Substring(0, 200) + "...\n");
            }
            else
            {
                Console.WriteLine("GET failed: " + response.StatusCode);
            }
        }

        // POST: Create a new post
        static async Task CreatePostAsync()
        {
            Console.WriteLine("POST: Creating a new post...");

            var newPost = new Post
            {
                UserId = 1,
                Title = "Testing HttpClient POST",
                Body = "This is a dummy post created using HttpClient."
            };

            var json = JsonConvert.SerializeObject(newPost);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response =
                await client.PostAsync("posts", content);

            if (response.IsSuccessStatusCode)
            {
                string data =
                    await response.Content.ReadAsStringAsync();

                Console.WriteLine("\nPOST Response:\n" +
                    data + "\n");
            }
            else
            {
                Console.WriteLine("POST failed: " +
                    response.StatusCode);
            }
        }

        // PUT: Update a post
        static async Task UpdatePostAsync()
        {
            Console.WriteLine("PUT: Updating an existing post...");

            var updatedPost = new Post
            {
                Id = 1,
                UserId = 1,
                Title = "Updated Title via PUT",
                Body = "This post has been updated using PUT request."
            };

            var json =
                JsonConvert.SerializeObject(updatedPost);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response =
                await client.PutAsync("posts/1", content);

            if (response.IsSuccessStatusCode)
            {
                string data =
                    await response.Content.ReadAsStringAsync();

                Console.WriteLine("\nPUT Response:\n" +
                    data + "\n");
            }
            else
            {
                Console.WriteLine("PUT failed: " +
                    response.StatusCode);
            }
        }

        // DELETE: Remove a post
        static async Task DeletePostAsync()
        {
            Console.WriteLine("DELETE: Deleting a post...");

            HttpResponseMessage response =
                await client.DeleteAsync("posts/1");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nDELETE successful.\n");
            }
            else
            {
                Console.WriteLine("DELETE failed: " +
                    response.StatusCode);
            }
        }
    }

    // Post class
    class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}