using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using webcrawler.Enums;

namespace webcrawler.Services;

public class WebCrawler(string url)
{
  private readonly string _url = url;

  public void ScrapeContent()
  {
    var options = new ChromeOptions();
    options.AddArgument("--headless");
    using IWebDriver driver = new ChromeDriver(options);
    driver.Navigate().GoToUrl(_url);

    // waits page full load
    TimeSpan timeout =  TimeSpan.FromSeconds(30);
    var wait = new WebDriverWait(driver, timeout);
    wait.Until(d =>
      {
        var state = ((IJavaScriptExecutor)d)
        .ExecuteScript("return document.readyState")?.ToString() ?? "";

        return state == "complete";
      });

    // getting all the elemets
    IWebElement tableNode = driver.FindElement(By.XPath("//table"));
    IReadOnlyCollection<IWebElement> tableRows = tableNode.FindElements(By.XPath("./tbody/tr"));

    // extracting the data
    List<Dictionary<string, string>> pageContent = [];
    foreach (IWebElement row in tableRows)
    {
      IReadOnlyCollection<IWebElement> rowData = row.FindElements(By.XPath(("./td")));

      string ip       = rowData.ElementAt((int)ColumnIndex.Ip).Text.Trim();
      string port     = rowData.ElementAt((int)ColumnIndex.Port).Text.Trim();
      string country  = rowData.ElementAt((int)ColumnIndex.Country).Text.Trim();
      string protocol = rowData.ElementAt((int)ColumnIndex.Protocol).Text.Trim();

      Dictionary<string, string> data = new()
      {
        { "ip", ip },
        { "port", port },
        { "country", country },
        { "protocol", protocol }
      };

      pageContent.Add(data);
    }

    string pageSource = driver.PageSource;
    char pageNumber = _url[^1];
    driver.Close();

    SavePageHtml($"page_{pageNumber}", pageSource);
    Console.WriteLine("Fim de execucao na URL: " + _url);
  }

  private void SavePageHtml(string fileName, string fileContent) {
    File.WriteAllText($"files/{fileName}.html", fileContent);
  }
}
