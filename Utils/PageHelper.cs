using HtmlAgilityPack;

namespace webcrawler.Utils;

public static class PageHelper
{
  public static List<string> GeneratePagesUrl(string baseUrl, string htmlContent)
  {
    HtmlDocument htmlDoc = new HtmlDocument();
    htmlDoc.LoadHtml(htmlContent);

    // getting the pages count
    var lastPageLink = htmlDoc.DocumentNode
      .SelectNodes("//a[contains(@class, 'page-link')]")
      ?.LastOrDefault();

    if (lastPageLink == null || !int.TryParse(lastPageLink.InnerText.Trim(), out int pagesCount))
    {
      throw new Exception("Não foi possível identificar o número de páginas");
    }

    // generating urls
    List<string> urls = [];
    for (int i = 1; i <= pagesCount; i++)
    {
      urls.Add($"{baseUrl}/page/{i}");
    }

    return urls;
  }
}
