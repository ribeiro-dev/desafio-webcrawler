using webcrawler.Services;
using webcrawler.Utils;

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
      webCrawler.ScrapeContent();
    }
    finally {
      semaphore.Release();
    }

  }));
  await Task.WhenAll(tasks);
}
catch (Exception ex) {

  Console.WriteLine($"Falha na execução: {ex.Message}"); ;
}
