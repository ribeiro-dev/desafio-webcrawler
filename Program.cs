using HtmlAgilityPack;

// Getting html

HttpClient client = new HttpClient();
string baseUrl = "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc";
var response = await client.GetStringAsync(baseUrl);


// Generating pages url

HtmlDocument htmlDoc = new HtmlDocument();
htmlDoc.LoadHtml(response);

var lastPageLink = htmlDoc.DocumentNode.SelectNodes("//a[contains(@class, 'page-link')]").Last();
int pagesCount = int.Parse(lastPageLink.InnerText);

List<string> pagesURL = [];
for (int i = 1; i <= pagesCount; i++)
{
  string fullURL = $"{baseUrl}/page/{i}";
  pagesURL.Add(fullURL);
  Console.WriteLine(fullURL);
}

