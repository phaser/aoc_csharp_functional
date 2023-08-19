// See https://aka.ms/new-console-template for more information

using System.Reflection;

var url = "https://adventofcode.com/2015/day/5";
var year = "2015";
var solutionFolder = GetSolutionFolder();
var libFolder = Path.Combine(solutionFolder, "adventofcode");
var testFolder = Path.Combine(solutionFolder, "adventofcode.tests");
var uri = new Uri(url);
var aocDir = Path.Combine(libFolder, uri.Host);
var testDir = Path.Combine(testFolder, uri.Host);
CreateMainDirIfNotExists(aocDir);
var problemDirectory = CreateProblemsDirectory(aocDir, year);
var solutionName = uri.LocalPath
    .Split('/')
    .Where(p => !string.IsNullOrEmpty(p))
    .Select(p => char.IsDigit(p[0]) ? $"{int.Parse(p):0000}" : p)
    .Aggregate((a, b) => a + b);
var solutionNamespace = "adventofcode." + uri.Host + "." 
                        + uri.LocalPath
                            .Split('/')
                            .Where(p => !string.IsNullOrEmpty(p))
                            .Select(p => char.IsDigit(p[0]) ? $"_{int.Parse(p):0000}" : p)
                            .Aggregate((a, b) => a + "." + b);
File.WriteAllText(Path.Combine(problemDirectory, $"Solution{solutionName}.cs"), $@"
namespace adventofcode.{uri.Host}._{year};

public class Solution{solutionName}
{{
}}
");

CreateMainDirIfNotExists(testDir);
problemDirectory = CreateProblemsDirectory(testDir, year);
solutionName = uri.LocalPath
    .Split('/')
    .Where(p => !string.IsNullOrEmpty(p))
    .Select(p => char.IsDigit(p[0]) ? $"{int.Parse(p):0000}" : p)
    .Aggregate((a, b) => a + b);
solutionNamespace = "adventofcode.tests." + uri.Host + "." 
                        + uri.LocalPath
                            .Split('/')
                            .Where(p => !string.IsNullOrEmpty(p))
                            .Select(p => char.IsDigit(p[0]) ? $"_{int.Parse(p):0000}" : p)
                            .Aggregate((a, b) => a + "." + b);
File.WriteAllText(Path.Combine(problemDirectory, $"Testing{solutionName}.cs"), $@"
namespace adventofcode.tests.{uri.Host}._{year};

public class Testing{solutionName}
{{
    [TestCase]
    public void TestSolution()
    {{
    }}

    private const string MainInput = @""{{input}}"";
}}
");

return;

string GetSolutionFolder()
{
    var assemblyLocation = Assembly.GetExecutingAssembly().Location;
    var currentPath = assemblyLocation;
    while (!File.Exists(Path.Combine(currentPath, "adventofcode.sln")))
    {
        currentPath = Path.GetFullPath(Path.Combine(currentPath, ".."));
    }

    return currentPath;
}

void CreateMainDirIfNotExists(string s)
{
    if (!Directory.Exists(s))
    {
        Directory.CreateDirectory(s);
    }
}

string CreateProblemsDirectory(string s1, string year1)
{
    var problemDirectory1 = Path.Combine(s1, year1);
    if (!Directory.Exists(problemDirectory1))
    {
        Directory.CreateDirectory(problemDirectory1);
    }

    return problemDirectory1;
}