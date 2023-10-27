using System.Text.RegularExpressions;

namespace WebLogParser
{
    public class WebLogParserClass
    {
        static async Task Main()
        {
            try
            {
                string logUrl = "https://coderbyte.com/api/challenges/logs/web-logs-raw";
                string? logContent = await FetchWebLogs(logUrl);
                if (!string.IsNullOrWhiteSpace(logContent))
                {
                    List<string> uniqueIds = ExtractUniqueIds(logContent);
                    PrintUniqueIds(uniqueIds);
                }
                else
                {
                    Console.WriteLine("Failed to retrieve log data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static async Task<string?> FetchWebLogs(string url)
        {
            using HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        public static List<string> ExtractUniqueIds(string logContent)
        {
            List<string> uniqueIds = new();
            string pattern = @"\?sharelinkId-(\d+)";

            MatchCollection matches = Regex.Matches(logContent, pattern);
            foreach (Match match in matches.Cast<Match>())
            {
                string id = match.Groups[1].Value;
                uniqueIds.Add(id);
            }

            return uniqueIds;
        }

        public static void PrintUniqueIds(List<string> uniqueIds)
        {
            var idCount = new Dictionary<string, int>();

            foreach (string id in uniqueIds)
            {
                if (idCount.ContainsKey(id))
                {
                    idCount[id]++;
                }
                else
                {
                    idCount[id] = 1;
                }
            }

            foreach (var pair in idCount)
            {
                if (pair.Value > 1)
                {
                    Console.WriteLine($"{pair.Key}:{pair.Value}");
                }
                else
                {
                    Console.WriteLine(pair.Key);
                }
            }
        }
    }
}