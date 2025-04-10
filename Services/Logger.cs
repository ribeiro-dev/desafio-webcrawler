using Dapper;
using Microsoft.Data.Sqlite;
using webcrawler.Models;

namespace webcrawler.Services;

public class Logger
{
  private readonly string _connectionString = "Data Source=Database/database.db";

  public void Create(ScrapingData scrapingData)
  {
    using var connection = new SqliteConnection(_connectionString);
    connection.Open();
    string sql = @"CREATE TABLE IF NOT EXISTS logs (
                  id INTEGER PRIMARY KEY AUTOINCREMENT,
                  start_time TEXT,
                  end_time TEXT,
                  pages_count INTEGER,
                  proxies_count INTEGER
                );";
    connection.Execute(sql);


    string sqlInsert = @"INSERT INTO logs (start_time, end_time, pages_count, proxies_count)
                       VALUES (@StartTime, @EndTime, @PagesCount, @ProxiesCount)";

    connection.Execute(sqlInsert, scrapingData);
  }
}
