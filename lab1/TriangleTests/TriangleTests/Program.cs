using System.Diagnostics;

string _triangleAppName = "Triangle.exe";
string _outputFileName = "result.txt";

Console.WriteLine("Type path to the file:");
string? pathToInpFile = Console.ReadLine();
if(!File.Exists(pathToInpFile))
{
    Console.WriteLine("Error open file.");
    return;
}
using StreamReader sr = new(pathToInpFile);
using StreamWriter sw = new(_outputFileName);

string? inputArgs;
while ((inputArgs = sr.ReadLine()) != null)
{
    try
    {
        var testArgs = inputArgs.Split(' ');
        if (testArgs.Length != 4) throw new ArgumentException();

        TestCase testCase = new()
        {
            a = double.Parse(testArgs[0]),
            b = double.Parse(testArgs[1]),
            c = double.Parse(testArgs[2]),
            result = testArgs[3]
        };

        var startInfo = new ProcessStartInfo
        {
            FileName = _triangleAppName,
            Arguments = $"{testCase.a} {testCase.b} {testCase.c}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using Process? proc = Process.Start(startInfo);
        string? result = proc.StandardOutput.ReadLine();
        proc.WaitForExit();

        string outputRes = (testCase.result == result) ? "success" : "error";
        sw.WriteLine(outputRes);
    }
    catch (Exception)
    {
        sw.WriteLine("error");
    }
}

struct TestCase
{
    public double a;
    public double b;
    public double c;
    public string result;
}