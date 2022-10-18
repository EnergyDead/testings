using LinkTests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;

string mainUrl = "http://links.qatl.ru/";
string bonusUrl = "https://montresor.ru/";

IWebDriver webDriver = new ChromeDriver();
List<string> urls = new();
Dictionary<string, HttpStatusCode> history = new();

urls.AddUrls(webDriver, bonusUrl);

for (int i = 0; i < urls.Count; i++)
{
    Console.WriteLine($"{i} - " + urls[i]);
    string url = urls[i];
    if (url is null || history.ContainsKey(url))
    {
        continue;
    }
    try
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
            || uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
        {
            throw new Exception();
        }
        var req = (HttpWebRequest)WebRequest.Create(url);
        var response = (HttpWebResponse)req.GetResponse();
        history.Add(url, response.StatusCode);
        if (url.Contains("montresor.ru"))
        {
            urls.AddUrls(webDriver, url);
        }
    }
    catch (Exception error)
    {
        var e = error as WebException;
        if (e != null)
        {
            var errorResponse = e.Response as HttpWebResponse;
            history.Add(url, errorResponse?.StatusCode ?? HttpStatusCode.NotFound);
        }
        else
        {
            history.Add(url, HttpStatusCode.NotFound);
        }
    }
}
webDriver.Dispose();

List<string> valid = new();
List<string> invalid = new();

foreach (var item in history)
{
    if((int)item.Value < 300)
    {
        valid.Add($"url: {item.Key} code: {(int)item.Value}({item.Value})");
    }
    else
    {
        invalid.Add($"url: {item.Key} code: {(int)item.Value}({item.Value})");
    }
}
valid.Add($"Count: {valid.Count}. Date: {DateTime.UtcNow:mm:hh dd.MM.yy}");
invalid.Add($"Count: {invalid.Count}. Date: {DateTime.UtcNow:mm:hh dd.MM.yy}");
File.WriteAllLines("valid.txt", valid);
File.WriteAllLines("invalid.txt", invalid);