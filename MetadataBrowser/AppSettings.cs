using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MetadataBrowser
{
  /// <summary>
  /// Класс настроек приложения.
  /// </summary>
  public class AppSettings
  {
    private static AppSettings instance = null;

    /// <summary>
    /// Ссылка на экземпляр одиночки.
    /// </summary>
    public static AppSettings Instance 
    { 
      get
      {
        if (instance == null)
          instance = new AppSettings();
        return instance;
      }
    }

    private List<string> developmentFolders;
    /// <summary>
    /// Список папок разработки.
    /// </summary>
    public List<string> DevelopmentFolders
    {
      get
      {
        return this.developmentFolders;
      }
    }

    private string connectionString;
    /// <summary>
    /// Строка подключения к БД.
    /// </summary>
    public string ConnectionString
    {
      get
      {
        return this.connectionString;
      }
    }

    private string databaseEngine;
    /// <summary>
    /// Движок БД.
    /// </summary>
    public string DatabaseEngine
    {
      get
      {
        return this.databaseEngine;
      }
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public AppSettings()
    {
      string directoryName = Path.GetDirectoryName(
        Process.GetCurrentProcess().MainModule.FileName);
      var builder = new ConfigurationBuilder()
        .SetBasePath(directoryName)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      var configuration = builder.Build();
      this.developmentFolders = configuration.GetSection("developmentFolders").GetChildren().ToList().Select(x => x.Value).ToList();
      this.connectionString = configuration.GetSection("connectionString").Value;
      this.databaseEngine = configuration.GetSection("databaseEngine").Value;
    }
  }
}
