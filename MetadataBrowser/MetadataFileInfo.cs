namespace MetadataBrowser
{
  /// <summary>
  /// Информация о файле метаданных.
  /// </summary>
  public class MetadataFileInfo
  {
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string FileName { get; set; }
    /// <summary>
    /// Имя узла.
    /// </summary>
    public string NodeName { get; set; }
    /// <summary>
    /// Guid самих метаданных.
    /// </summary>
    public string NameGuid { get; set; }
    /// <summary>
    /// Базовый Guid.
    /// </summary>
    public string BaseGuid { get; set; }

    /// <summary>
    /// Содержимое файла.
    /// </summary>
    public string FileContent { get; set; }
  }
}
