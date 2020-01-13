using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace floss
{
    public class PackageJsonObject
    {
        [JsonPropertyName("dependencies")]
        public Dictionary<string, string> Dependencies { get; set; }
    }

    public static class PackageJson
    {
        public static IEnumerable<string> Parse(string fileLocation)
        {
            if (!File.Exists(fileLocation))
                throw new FileNotFoundException($"The file {fileLocation} was not found.");

            string fileContent = File.ReadAllText(fileLocation);

            PackageJsonObject packageJson = JsonSerializer.Deserialize<PackageJsonObject>(fileContent);

            return packageJson.Dependencies.Select(x => x.Key);
        }
    }
}