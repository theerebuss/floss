using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;

namespace floss
{
    public static class NpmService
    {
        public static async Task<string> GetPackageInfo(HttpClient client, string packageName)
        {
            Console.WriteLine(packageName);
            try
            {
                var stream = await client.GetStreamAsync($"https://api.npms.io/v2/package/{packageName}");
                var content = await JsonSerializer.DeserializeAsync<string>(stream);
                return content;
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