using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;

namespace floss.Npms
{
    public static class NpmsService
    {
        public static async Task<string> GetPackageInfo(HttpClient client, string packageName)
        {
            try
            {
                var response = await client.GetAsync($"https://api.npms.io/v2/package/{packageName}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<NpmsObject>(content)
                    .Collected
                    .Metadata
                    .Links
                    .Repository;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public static async Task<IEnumerable<string>> GetPackageInfo(HttpClient client, IEnumerable<string> packageNames)
        {
            IEnumerable<Task<string>> requests = packageNames.Select(packageName => GetPackageInfo(client, packageName));

            return await Task.WhenAll(requests);
        }
    }
}