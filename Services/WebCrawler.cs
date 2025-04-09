using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace webcrawler.Services;

public class WebCrawler(string url)
{
  private readonly string _url = url;

  public Task<string> ScrapeContentAsync()
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


    string title = driver.Title;
    driver.Close();
    Console.WriteLine("Fim de execucao na URL: " + _url);
    return Task.FromResult(title);
  }
}
