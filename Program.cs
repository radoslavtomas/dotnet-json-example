using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

await Main();

static async Task Main()
{
    Console.WriteLine("Program started");
    await FetchUserDataAsync();
}

static async Task FetchUserDataAsync()
{
    using (HttpClient client = new HttpClient())
    {
        try
        {
            // Make a GET request to the JSONPlaceholder API
            HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");

            // Check if the request was successful (status code 200)
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string jsonString = await response.Content.ReadAsStringAsync();

                // Parse the JSON string into a list of User objects
                List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString);

                // Print user data using foreach loop
                foreach (User user in users)
                {
                    Console.WriteLine($"User ID: {user.id}");
                    Console.WriteLine($"Name: {user.name}");
                    Console.WriteLine($"Username: {user.username}");
                    Console.WriteLine($"Email: {user.email}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
}


// Define a User class to match the structure of the JSON data
class User
{
    public int id { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    // Add more properties as needed
}
