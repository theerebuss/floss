using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CommandLine;

namespace floss
{
    class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Arguments>(args)
                  .WithParsed<Arguments>(async opts =>
                  {
                      HttpClient client = new HttpClient();
                      IEnumerable<string> dependencyList = PackageJson.Parse(opts.Target);
                      IEnumerable<string> packages = await NpmService.GetPackageInfo(client, dependencyList);

                      foreach (string package in packages)
                      {
                          Console.WriteLine(package);
                      }
                  });
        }
    }
}
