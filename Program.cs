using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using floss.GitHub;
using floss.Npms;

namespace floss
{
    class Program
    {
        /// <param name="target" >Set the target package.json location</param>
        public static async Task Main(string target)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "floss");

            IEnumerable<string> dependencyList = PackageJson.Parser.Parse(target);
            IEnumerable<string> packages = await NpmsService.GetPackageInfo(client, dependencyList);
            IEnumerable<string> repositories = await GitHubService.GetRepositoryInfo(client, packages);

            foreach (string beb in repositories)
            {
                Console.WriteLine(beb);
            }
        }
    }
}
