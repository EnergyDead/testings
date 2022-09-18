using System.Diagnostics;

string _triangleAppName = "C:\\Users\\user\\ruslan\\learning\\testings\\lab1\\Triangle\\Triangle\\bin\\Debug\\net6.0\\Triangle.exe";
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
        inputArgs = inputArgs.Replace('\t', ' ');
        var testArgs = inputArgs.Split(' ');
        if (testArgs.Length != 4) throw new ArgumentException();

        TestCase testCase = new()
        {
            a = testArgs[0],
            b = testArgs[1],
            c = testArgs[2],
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
    public string a;
    public string b;
    public string c;
    public string result;
}