using webcrawler.Services;
using webcrawler.Utils;

// Getting html

HttpClient client = new HttpClient();
string baseUrl = "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc";
var response = await client.GetStringAsync(baseUrl);


try {
  List<string> pagesURL = PageHelper.GeneratePagesUrl(baseUrl, response);
  WebCrawler webCrawler = new WebCrawler(pagesURL[0]);
  List<Dictionary<string, string>> extractedContent = await webCrawler.ScrapeContentAsync();

  foreach (var item in extractedContent)
  {
    Console.WriteLine(item["ip"]);
    Console.WriteLine(item["port"]);
    Console.WriteLine();
  }
}
catch (Exception ex) {

  Console.WriteLine($"Falha na execução: {ex.Message}"); ;
}
