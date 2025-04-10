namespace webcrawler.Models;

public class Proxy
{
  public required string Ip { get; set; }
  public required string Port { get; set; }
  public required string Country { get; set; }
  public required string Protocol { get; set; }
}
