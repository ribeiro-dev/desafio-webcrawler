using webcrawler.Services;
using webcrawler.Utils;

// Getting html

HttpClient client = new HttpClient();
string baseUrl = "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc";
var response = await client.GetStringAsync(baseUrl);


List<string> pagesURLs = [
  "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc/page/1",
  "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc/page/2",
  "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc/page/3"
];

try {
  // List<string> pagesURL = PageHelper.GeneratePagesUrl(baseUrl, response);
  var tasks = pagesURLs.Select(url => Task.Run(() =>
  {
    WebCrawler webCrawler = new WebCrawler(url);
    webCrawler.ScrapeContent();
  }));
  await Task.WhenAll(tasks);
}
catch (Exception ex) {

  Console.WriteLine($"Falha na execução: {ex.Message}"); ;
}
