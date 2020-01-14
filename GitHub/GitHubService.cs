using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace floss.GitHub
{
    public static class GitHubService
    {
        private static string ParseUri(string uri)
        {
            return string.Join('/', uri.Split("/").TakeLast(2));
        }

        public static async Task<string> GetRepositoryInfo(HttpClient client, string repositoryUri)
        {
            string repoPath = ParseUri(repositoryUri);
            Console.WriteLine($"https://api.github.com/repos/{repoPath}");
            var response = await client.GetAsync($"https://api.github.com/repos/{repoPath}");
            // response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public static async Task<IEnumerable<string>> GetRepositoryInfo(HttpClient client, IEnumerable<string> repositoryNames)
        {
            IEnumerable<Task<string>> requests = repositoryNames.Select(repositoryName => GetRepositoryInfo(client, repositoryName));

            return await Task.WhenAll(requests);
        }
    }
}