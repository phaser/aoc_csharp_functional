// See https://aka.ms/new-console-template for more information

using System.Reflection;

var url = "https://adventofcode.com/2015/day/1";
var solutionFolder = GetSolutionFolder();
var libFolder = Path.Combine(solutionFolder, "adventofcode");
var testFolder = Path.Combine(solutionFolder, "adventofcode.tests");
var uri = new Uri(url);
var aocDir = Path.Combine(libFolder, uri.Host);
var testDir = Path.Combine(testFolder, uri.Host);
CreateMainDirIfNotExists(aocDir);
CreateMainDirIfNotExists(testDir);
var problemDirectory = CreateDirHierarchyIfNotExists(uri, aocDir);
var solutionName = uri.LocalPath
    .Split('/')
    .Where(p => !string.IsNullOrEmpty(p))
    .Aggregate((a, b) => a + b);
var solutionNamespace = "adventofcode." + uri.Host + "." 
                        + uri.LocalPath
                            .Split('/')
                            .Where(p => !string.IsNullOrEmpty(p))
                            .Select(p => char.IsDigit(p[0]) ? $"_{p}" : p)
                            .Aggregate((a, b) => a + "." + b);
File.WriteAllText(Path.Combine(problemDirectory, $"Solution{solutionName}.cs"), $@"
namespace {solutionNamespace};

public class Solution{solutionName}
{{
}}
");

problemDirectory = CreateDirHierarchyIfNotExists(uri, testDir);
solutionName = uri.LocalPath
    .Split('/')
    .Where(p => !string.IsNullOrEmpty(p))
    .Aggregate((a, b) => a + b);
solutionNamespace = "adventofcode.tests." + uri.Host + "." 
                        + uri.LocalPath
                            .Split('/')
                            .Where(p => !string.IsNullOrEmpty(p))
                            .Select(p => char.IsDigit(p[0]) ? $"_{p}" : p)
                            .Aggregate((a, b) => a + "." + b);
File.WriteAllText(Path.Combine(problemDirectory, $"Testing{solutionName}.cs"), $@"
namespace {solutionNamespace};

public class Testing{solutionName}
{{
    [TestCase]
    public void TestSolution()
    {{
    }}
}}
");

var httpClient = new HttpClient();
var response = await httpClient.GetAsync($"{url}/input");
var input = "";
if (response.IsSuccessStatusCode)
{
    input = await response.Content.ReadAsStringAsync();
}
else
{
    Console.Error.WriteLine("Error occured at input downloading");
}

File.WriteAllText(Path.Combine(problemDirectory, $"Input{solutionName}.cs"), $@"
namespace {solutionNamespace};

public class Input{solutionName}
{{
    public string MainInput = \@""{{input}}"";
}}");

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

string CreateDirHierarchyIfNotExists(Uri uri1, string aocDir1)
{
    var paths = uri1.LocalPath.Split('/');
    var currentPath = aocDir1;
    foreach (var path in paths)
    {
        if (string.IsNullOrEmpty(path))
            continue;
        currentPath = Path.Combine(currentPath, path);
        if (!Directory.Exists(currentPath))
        {
            Directory.CreateDirectory(currentPath);
        }
    }

    return currentPath;
}