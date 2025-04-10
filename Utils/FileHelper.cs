using System.Text.Json;

namespace webcrawler.Utils;

public static class FileHelper
{
  public static void CreateJson(object fileContent, string fileName) {
    string jsonString = JsonSerializer.Serialize(fileContent, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText($"{fileName}.json", jsonString);
  }

}
