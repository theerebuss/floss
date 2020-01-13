using CommandLine;

public class Arguments
{
    [Option('t', "target", Required = true, HelpText = "Set target package.json file")]
    public string Target { get; set; }
}