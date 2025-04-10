using webcrawler.Models;
using webcrawler.Services;
using webcrawler.Utils;

DateTime startTime = DateTime.Now;

// Getting html
HttpClient client = new HttpClient();
string baseUrl = "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc";
var response = await client.GetStringAsync(baseUrl);


try {
  int maxThreads = 3;
  var semaphore = new SemaphoreSlim(maxThreads);
  var random = new Random();

  List<string> pagesURLs = PageHelper.GeneratePagesUrl(baseUrl, response);
  var tasks = pagesURLs.Select(url => Task.Run(async () =>
  {
    try {
      await Task.Delay(random.Next(500, 1500)); // prevents being blocked
      await semaphore.WaitAsync();

      WebCrawler webCrawler = new WebCrawler(url);
      return webCrawler.ScrapeContent();
    }
    finally {
      semaphore.Release();
    }

  }));

  // on tasks complete
  List<Proxy>[] results = await Task.WhenAll(tasks);
  List<Proxy> pagesContent = results.SelectMany(x => x).ToList();
  FileHelper.CreateJson(pagesContent, "files/proxies_data");


  // log creation
  ScrapingData scrapingData = new()
  {
    StartTime     = startTime,
    EndTime       = DateTime.Now,
    PagesCount    = pagesURLs.Count,
    ProxiesCount  = pagesContent.Count
  };

  Logger logger = new();
  logger.Create(scrapingData);
}
catch (Exception ex) {

  Console.WriteLine($"Falha na execução: {ex.Message}"); ;
}
