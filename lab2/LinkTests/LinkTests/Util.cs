using OpenQA.Selenium;

namespace LinkTests;

internal static class Util
{
    public static List<string> AddUrls(this List<string> list, IWebDriver webDriver, string url)
    {
        webDriver.Navigate().GoToUrl(url);
        var elements = webDriver.FindElements(By.TagName("a"));

        for (int i = 0; i < elements.Count; i++)
        {
            string newUrl = elements[i].GetAttribute("href");
            if (!list.Contains(newUrl))
                list.Add(newUrl);
        }

        return list;
    }
}
