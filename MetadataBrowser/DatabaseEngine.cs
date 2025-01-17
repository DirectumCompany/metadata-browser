

using System.Data.Common;
using System.Data.SqlClient;
using Npgsql;

namespace MetadataBrowser
{
  /// <summary>
  /// Класс слоя изоляции над СУБД.
  /// </summary>
  public class DatabaseEngine
  {
    private static DatabaseEngine instance;

    /// <summary>
    /// Ссылка на экземпляр одиночки.
    /// </summary>
    public static DatabaseEngine Instance
    {
      get
      {
        if (instance == null)
          instance = new DatabaseEngine();
        return instance;
      }
    }

    /// <summary>
    /// Создать подключение к БД.
    /// </summary>
    /// <param name="connectionString">Строка подключения.</param>
    /// <returns>Подключение к БД.</returns>
    public DbConnection CreateConnection(string connectionString)
    {
      if (AppSettings.Instance.DatabaseEngine == "postgres")
        return new NpgsqlConnection(connectionString);
      else if (AppSettings.Instance.DatabaseEngine == "mssql")
        return new SqlConnection(connectionString);
      else
        throw new System.Exception("Unsupported databse engine " + AppSettings.Instance.DatabaseEngine);
    }

    /// <summary>
    /// Создать запрос к БД.
    /// </summary>
    /// <param name="query">Текст запроса.</param>
    /// <param name="connection">Соединение.</param>
    /// <returns>SQL-команда.</returns>
    public DbCommand CreateCommand(string query, DbConnection connection)
    {
      if (AppSettings.Instance.DatabaseEngine == "postgres")
        return new NpgsqlCommand(query, (NpgsqlConnection)connection);
      else if (AppSettings.Instance.DatabaseEngine == "mssql")
        return new SqlCommand(query, (SqlConnection)connection);
      else
        throw new System.Exception("Unsupported databse engine " + AppSettings.Instance.DatabaseEngine);
    }
  }
}
