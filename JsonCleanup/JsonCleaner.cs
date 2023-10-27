using Newtonsoft.Json.Linq;

namespace JsonCleanup
{
    public class JsonCleaner
    {
        static async Task Main()
        {
            // Create an instance of HttpClient
            using (var client = new HttpClient())
            {
                try
                {
                    // Send a GET request to the specified URL
                    var response = await client.GetAsync("https://coderbyte.com/api/challenges/json/json-cleaning");

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        var jsonContent = await response.Content.ReadAsStringAsync();

                        // Parse the JSON string into a JObject
                        JObject jsonObject = JObject.Parse(jsonContent);

                        // Clean the JSON object
                        CleanJsonObject(jsonObject);

                        // Convert the modified object back to a string
                        string cleanedJson = jsonObject.ToString();

                        // Print the cleaned JSON
                        Console.WriteLine(cleanedJson);
                    }
                    else
                    {
                        Console.WriteLine("Failed to retrieve data from the API.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        public static void CleanJsonObject(JObject obj)
        {
            foreach (var property in obj.Properties().ToList())
            {
                if (property.Value.Type == JTokenType.Object)
                {
                    // Recursively clean nested objects
                    CleanJsonObject((JObject)property.Value);
                }
                else if (property.Value.Type == JTokenType.Array)
                {
                    // Clean elements within arrays
                    JArray array = (JArray)property.Value;
                    for (int i = array.Count - 1; i >= 0; i--)
                    {
                        if (array[i].Type == JTokenType.String)
                        {
                            string? value = array[i].Value<string>();
                            if (string.IsNullOrWhiteSpace(value) || value == "N/A" || value == "-")
                            {
                                array.RemoveAt(i);
                            }
                        }
                    }
                }
                else if (property.Value.Type == JTokenType.String)
                {
                    string? value = property.Value.Value<string>();
                    if (string.IsNullOrWhiteSpace(value) || value == "N/A" || value == "-")
                    {
                        property.Remove();
                    }
                }
            }
        }
    }
}