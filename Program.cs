using webcrawler.Services;
using webcrawler.Utils;

// Getting html

HttpClient client = new HttpClient();
string baseUrl = "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc";
var response = await client.GetStringAsync(baseUrl);


// Generating pages url
List<string> pagesURL = [];
try {
  pagesURL = PageHelper.GeneratePagesUrl(baseUrl, response);
  WebCrawler webCrawler = new WebCrawler(pagesURL[0]);
  string title = await webCrawler.ScrapeContentAsync();
  Console.WriteLine(title);
}
catch (Exception ex) {

  Console.WriteLine($"Falha na execução: {ex.Message}"); ;
}
