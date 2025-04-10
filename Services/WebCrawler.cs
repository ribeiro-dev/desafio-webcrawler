using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using webcrawler.Enums;
using Proxy = webcrawler.Models.Proxy;

namespace webcrawler.Services;

public class WebCrawler(string url)
{
  private readonly string _url = url;

  public List<Proxy> ScrapeContent()
  {
    // starts and configure the driver
    var options = new ChromeOptions();
    options.AddArgument("--headless");
    var service = ChromeDriverService.CreateDefaultService();
    service.SuppressInitialDiagnosticInformation = true; // hides "Only local connections" message
    using IWebDriver driver = new ChromeDriver(service, options);
    driver.Navigate().GoToUrl(_url);

    // waits page full load
    TimeSpan timeout =  TimeSpan.FromSeconds(120);
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
    List<Proxy> pageContent = [];
    foreach (IWebElement row in tableRows)
    {
      IReadOnlyCollection<IWebElement> rowData = row.FindElements(By.XPath(("./td")));

      Proxy data = new()
      {
        Ip        = rowData.ElementAt((int)ColumnIndex.Ip).Text.Trim(),
        Port      = rowData.ElementAt((int)ColumnIndex.Port).Text.Trim(),
        Country   = rowData.ElementAt((int)ColumnIndex.Country).Text.Trim(),
        Protocol  = rowData.ElementAt((int)ColumnIndex.Protocol).Text.Trim()
      };

      pageContent.Add(data);
    }

    string pageSource = driver.PageSource;
    string pageNumber = _url.Split('/')[^1];
    driver.Close();

    SavePageHtml($"page_{pageNumber}", pageSource);
    Console.WriteLine("Fim de execucao na URL: " + _url);
    return pageContent;
  }

  private void SavePageHtml(string fileName, string fileContent) {
    File.WriteAllText($"files/{fileName}.html", fileContent);
  }
}
