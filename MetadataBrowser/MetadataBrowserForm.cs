using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using Npgsql;
using ScintillaNET;

namespace MetadataBrowser
{
  /// <summary>
  /// Главная форма приложения.
  /// </summary>
  public partial class MetadataBrowserForm : Form
  {
    /// <summary>
    /// Полное дерево метаданных.
    /// </summary>
    private MetadataTree metadataTree = new MetadataTree();

    /// <summary>
    /// Список всех узлов дерева.
    /// </summary>
    private List<MetadataFileInfo> allTreeNodes = new List<MetadataFileInfo>();

    /// <summary>
    /// Узел элементов метаданных, для которых не удалось найти родительский узел.
    /// </summary>
    private MetadataFileInfo unparentedMetadataFileInfo = new MetadataFileInfo();

    /// <summary>
    /// Папки разработки.
    /// </summary>
    private List<string> developmentFolders = new List<string>();

    /// <summary>
    /// Конструктор.
    /// </summary>
    public MetadataBrowserForm()
    {
      InitializeComponent();
      this.scintilla.Styles[Style.Json.Default].ForeColor = Color.Silver;
      this.scintilla.Styles[Style.Json.BlockComment].ForeColor = Color.FromArgb(0, 128, 0); 
      this.scintilla.Styles[Style.Json.LineComment].ForeColor = Color.FromArgb(0, 128, 0); 
      this.scintilla.Styles[Style.Json.Number].ForeColor = Color.Olive;
      this.scintilla.Styles[Style.Json.PropertyName].ForeColor = Color.Blue;
      this.scintilla.Styles[Style.Json.String].ForeColor = Color.FromArgb(163, 21, 21);
      this.scintilla.Styles[Style.Json.StringEol].BackColor = Color.Pink;
      this.scintilla.Styles[Style.Json.Operator].ForeColor = Color.Purple;
      #pragma warning disable CS0618
      {
        this.scintilla.Lexer = Lexer.Json;
      }
    }

    /// <summary>
    /// Перейти к узлу дерева.
    /// </summary>
    /// <param name="node">Узел дерева.</param>
    /// <returns>Истина, если переход к узлу выполнен.</returns>
    public bool GoToNode(MetadataTreeNode node)
    {
      var treeNode = FindTreeNodeByMetadataTreeNode(node, treeView.Nodes);
      if (treeNode != null)
      {
        var currentParentNode = treeNode.Parent;
        while (currentParentNode != null)
        {
          currentParentNode.Expand();
          currentParentNode = currentParentNode.Parent;
        }
        treeView.SelectedNode = treeNode;
        var metadataFileInfo = ((MetadataTreeNode)treeNode.Tag).MetadataFileInfo;
        this.UpdateTextBoxAndStatusLabel(metadataFileInfo);
        return true;
      }
      return false;
    }

    /// <summary>
    /// Выделить текст в текстовом редакторе.
    /// </summary>
    /// <param name="startIndex">Начальный индекс выделения.</param>
    /// <param name="length">Длина выделения.</param>
    public void SelectInEditor(int startIndex, int length)
    {
      this.scintilla.SelectionStart = startIndex;
      this.scintilla.SelectionEnd = startIndex + length;
      var selectionStartLine = this.scintilla.LineFromPosition(startIndex);
      var linesOnScreen = this.scintilla.LinesOnScreen - 2; 
      var start = scintilla.Lines[selectionStartLine - (linesOnScreen / 2)].Position;
      var end = scintilla.Lines[selectionStartLine + (linesOnScreen / 2)].Position;
      scintilla.ScrollRange(start, end);
    }

    /// <summary>
    /// Найти узел визуального дерева по узлу дерева метаданных.
    /// </summary>
    /// <param name="node">Узел дерева метаданных.</param>
    /// <returns>Узел визуального дерева.</returns>
    public TreeNode FindTreeNodeByMetadataTreeNode(MetadataTreeNode node)
    {
      return this.FindTreeNodeByMetadataTreeNode(node, this.treeView.Nodes);
    }

    /// <summary>
    /// Найти узел визуального дерева по узлу дерева метаданных.
    /// </summary>
    /// <param name="node">Узел дерева метаданных.</param>
    /// <param name="nodes">Список узлов визуального дерева.</param>
    /// <returns>Узел визуального дерева.</returns>
    public TreeNode FindTreeNodeByMetadataTreeNode(MetadataTreeNode node, TreeNodeCollection nodes)
    {
      foreach (TreeNode currentNode in nodes)
      {
        if ((MetadataTreeNode)currentNode.Tag == node)
          return currentNode;
        var result = this.FindTreeNodeByMetadataTreeNode(node, currentNode.Nodes);
        if (result != null)
          return result;
      }
      return null;
    }

    /// <summary>
    /// Найти информацию о метаданных по guid.
    /// </summary>
    /// <param name="guid">Guid метаданных.</param>
    /// <returns>Элемент списка метаданных, имеющий заданный guid.</returns>
    private MetadataFileInfo FindMetadataFileInfoByGuid(string guid)
    {
      foreach (MetadataFileInfo metadataFileInfo in this.allTreeNodes)
      {
        if (metadataFileInfo.NameGuid == guid)
          return metadataFileInfo;
      }
      return null;
    }

    /// <summary>
    /// Найти узел дерева по guid.
    /// </summary>
    /// <param name="guid">Guid.</param>
    /// <returns>Узел дерева.</returns>
    private MetadataTreeNode FindTreeNodeByGuid(string guid)
    {
      return this.InternalFindTreeNodeByGuid(guid, this.metadataTree.Nodes);
    }

    /// <summary>
    /// Внутренний метод поиска узла дерева по guid.
    /// </summary>
    /// <param name="guid">Guid.</param>
    /// <param name="nodes">Список узлов дерева.</param>
    /// <returns>Узел дерева.</returns>
    private MetadataTreeNode InternalFindTreeNodeByGuid(string guid, List<MetadataTreeNode> nodes)
    {
      foreach (MetadataTreeNode node in nodes)
      {
        var metadataFileInfo = node.MetadataFileInfo;
        if (metadataFileInfo.NameGuid == guid)
          return node;

        MetadataTreeNode result = null;
        if (node.Nodes.Count > 0)
          result = this.InternalFindTreeNodeByGuid(guid, node.Nodes);
        if (result != null)
          return result;
      }
      return null;
    }

    /// <summary>
    /// Внутренний метод поиска узла дерева по имени файла.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="nodes">Список узлов дерева.</param>
    /// <returns>Узел дерева.</returns>
    private MetadataTreeNode InternalFindTreeNodeByFileName(string fileName, List<MetadataTreeNode> nodes)
    {
      foreach (MetadataTreeNode node in nodes)
      {
        var metadataFileInfo = node.MetadataFileInfo;
        if (metadataFileInfo.FileName == fileName)
          return node;

        MetadataTreeNode result = null;
        if (node.Nodes.Count > 0)
          result = this.InternalFindTreeNodeByFileName(fileName, node.Nodes);
        if (result != null)
          return result;
      }
      return null;
    }

    /// <summary>
    /// Найти узел дерева по имени файла.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Узел дерева.</returns>
    private MetadataTreeNode FindTreeNodeByFileName(string fileName)
    {
      return this.InternalFindTreeNodeByFileName(fileName, this.metadataTree.Nodes);
    }

    /// <summary>
    /// Найти узел дерева в папке по содержимому.
    /// </summary>
    /// <param name="folder">Папка.</param>
    /// <param name="searchString">Искомая строка.</param>
    /// <returns>Узел дерева.</returns>
    private IEnumerable<SearchResult> FindByContentInFolder(string folder, string searchString)
    {
      foreach (var fileName in Directory.EnumerateFiles(folder))
        if (Path.GetExtension(fileName) == ".mtd" && !fileName.Contains("VersionData"))
        {
          var fileContent = File.ReadAllText(fileName, Encoding.UTF8);
          var index = fileContent.IndexOf(searchString);
          while (index >= 0)
          {
            var treeNode = FindTreeNodeByFileName(fileName);
            if (treeNode != null)
            {
              SearchResult searchResult = new SearchResult(treeNode, index);
              yield return searchResult;
            }
            index = fileContent.IndexOf(searchString, index + 1);
          }
        }
      foreach (var directory in Directory.EnumerateDirectories(folder))
      {
        var result = FindByContentInFolder(directory, searchString);
        foreach (var searchResult in result)
          yield return searchResult;
      }
    }

    /// <summary>
    /// Найти узел дерева по содержимому.
    /// </summary>
    /// <param name="searchString">Искомая строка.</param>
    /// <returns>Узел дерева.</returns>
    internal IEnumerable<SearchResult> FindTreeNodeByContent(string searchString)
    {
      foreach (var developmentFolder in this.developmentFolders)
      {
        foreach (var searchResult in FindByContentInFolder(developmentFolder, searchString))
          yield return searchResult;
      }
    }

    /// <summary>
    /// Выполнить поиск в текущем узле дерева по строке.
    /// </summary>
    /// <param name="searchString">Искомая строка.</param>
    /// <returns>Список результатов поиска.</returns>
    internal IEnumerable<SearchResult> FindInCurrentNodeByContent(string searchString)
    {
      var node = this.treeView.SelectedNode;
      var metadataTreeNode = (MetadataTreeNode)node.Tag;
      var fileContent = File.ReadAllText(metadataTreeNode.MetadataFileInfo.FileName, Encoding.UTF8);
      var index = fileContent.IndexOf(searchString);
      while (index >= 0)
      {
        var treeNode = FindTreeNodeByFileName(metadataTreeNode.MetadataFileInfo.FileName);
        if (treeNode != null)
        {
          SearchResult searchResult = new SearchResult(treeNode, index);
          yield return searchResult;
        }
        index = fileContent.IndexOf(searchString, index + 1);
      }
    }

    /// <summary>
    /// Добавить файл метаданных.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    private void AddMetadataFile(string fileName, string fileText)
    {      
      var metadataFileInfo = new MetadataFileInfo();
      metadataFileInfo.FileName = fileName;

      var document = JsonDocument.Parse(fileText);
      var nameGuid = document.RootElement.GetProperty("NameGuid");
      metadataFileInfo.NameGuid = nameGuid.ToString();

      if (document.RootElement.TryGetProperty("BaseGuid", out JsonElement baseGuidProperty))
        metadataFileInfo.BaseGuid = baseGuidProperty.ToString();
      else
        metadataFileInfo.BaseGuid = Guid.Empty.ToString();

      if (Path.GetFileName(fileName).EndsWith("Module.mtd"))
        metadataFileInfo.NodeName = metadataFileInfo.FileName + " (" + document.RootElement.GetProperty("Name") + ")";

      this.allTreeNodes.Add(metadataFileInfo);
    }

    /// <summary>
    /// Добавить информацию о файле метаданных.
    /// </summary>
    /// <param name="metadataFileInfo">Информация о файле метаданных.</param>
    /// <param name="isRootLevelNode">Признак узла верхнего уровня.</param>
    private void AddMetadataFileInfo(MetadataFileInfo metadataFileInfo, bool isRootLevelNode = false)
    {
      var currentTreeNode = FindTreeNodeByGuid(metadataFileInfo.NameGuid);
      if (currentTreeNode != null)
        return;

      var treeNode = new MetadataTreeNode(Path.GetFileName(metadataFileInfo.NodeName ?? metadataFileInfo.FileName), null);
      treeNode.MetadataFileInfo = metadataFileInfo;
      var parentMetadataFileInfo = FindMetadataFileInfoByGuid(metadataFileInfo.BaseGuid);
      var parentTreeNode = FindTreeNodeByGuid(metadataFileInfo.BaseGuid);
      if (parentMetadataFileInfo != null && parentTreeNode == null)
      {
        AddMetadataFileInfo(parentMetadataFileInfo);
        parentTreeNode = FindTreeNodeByGuid(metadataFileInfo.BaseGuid);
        parentTreeNode.Nodes.Add(treeNode);
        treeNode.Parent = parentTreeNode;
      }
      else if (parentTreeNode != null)
      {
        parentTreeNode.Nodes.Add(treeNode);
        treeNode.Parent = parentTreeNode;
      }
      else if (metadataFileInfo != this.unparentedMetadataFileInfo && !isRootLevelNode)
      {
        parentTreeNode = FindTreeNodeByGuid(this.unparentedMetadataFileInfo.NameGuid);
        parentTreeNode.Nodes.Add(treeNode);
        treeNode.Parent = parentTreeNode;
      }
      else
        metadataTree.Nodes.Add(treeNode);
    }

    /// <summary>
    /// Заполнить дерево метаданных.
    /// </summary>
    /// <param name="currentPath">Путь к папке, содержащей файлы метаданных.</param>
    private void FillMetadataTreeView(string currentPath)
    {
      foreach (var directory in Directory.EnumerateDirectories(currentPath))
        FillMetadataTreeView(directory);
      foreach (var fileName in Directory.EnumerateFiles(currentPath))
        if (Path.GetExtension(fileName) == ".mtd" && !fileName.Contains("VersionData"))
          this.AddMetadataFile(fileName, File.ReadAllText(fileName));
    }

    /// <summary>
    /// Внутренний метод заполнения дерева метаданных.
    /// </summary>
    /// <param name="searchText">Искомый текст.</param>
    /// <param name="nodes">Список узлов дерева метаданных.</param>
    private List<TreeNode> InternalFilterTreeView(string searchText, List<MetadataTreeNode> nodes)
    {
      var result = new List<TreeNode>();
      foreach (MetadataTreeNode metadataTreeNode in nodes)
      {
        if (metadataTreeNode.Caption.StartsWith(searchText, StringComparison.OrdinalIgnoreCase) || searchText == string.Empty)
        {
          var newNode = new TreeNode(metadataTreeNode.Caption);
          newNode.ImageIndex = 0;
          newNode.Tag = metadataTreeNode;
          result.Add(newNode);                    
        }
        result.AddRange(InternalFilterTreeView(searchText, metadataTreeNode.Nodes));
      }
      return result;
    }

    /// <summary>
    /// Добавить в дерево отфильтрованные узлы со всеми их предками.
    /// </summary>
    /// <param name="filteredNodes">Отфильтрованные узлы.</param>
    private void AddFilteredNodes(List<TreeNode> filteredNodes)
    {
      foreach (var filteredNode in filteredNodes)
      {
        var metadataTreeNode = (MetadataTreeNode)filteredNode.Tag;
        var nodesPath = new List<MetadataTreeNode>();
        var currentNode = metadataTreeNode;
        while (currentNode != null)
        {
          nodesPath.Add(currentNode);
          currentNode = currentNode.Parent;
        }
        nodesPath.Reverse();
        var treeNodes = this.treeView.Nodes;
        foreach (var node in nodesPath)
        {
          var currentTreeNode = this.FindTreeNodeByMetadataTreeNode(node);
          if (currentTreeNode == null)
          {
            currentTreeNode = new TreeNode(node.Caption);
            currentTreeNode.ImageIndex = 0;
            currentTreeNode.Tag = node;
            treeNodes.Add(currentTreeNode);
          }
          treeNodes = currentTreeNode.Nodes;
        }
      }
    }

    /// <summary>
    /// Заполнить дерево метаданных.
    /// </summary>
    /// <param name="searchText">Искомый текст.</param>
    private void DisplayTreeView(string searchText)
    {
      treeView.Nodes.Clear();      
      var filteredNodes = InternalFilterTreeView(searchText, this.metadataTree.Nodes);
      AddFilteredNodes(filteredNodes);

      if (treeView.Nodes.Count > 0 && treeView.Focused)
      {
        var treeNode = treeView.Nodes[0];
        UpdateTextBoxAndStatusLabel(((MetadataTreeNode)treeNode.Tag).MetadataFileInfo);
      }
    }

    private void MetadataBrowserForm_Load(object sender, EventArgs e)
    {
      this.unparentedMetadataFileInfo = new MetadataFileInfo();
      this.unparentedMetadataFileInfo.FileName = "(unparented)";
      this.unparentedMetadataFileInfo.NameGuid = "3a5d05a5-e5c4-4851-b958-e30f53bfe8fc";
      this.unparentedMetadataFileInfo.FileContent = "=== Метаданные узла без родителя ===";
      this.AddMetadataFileInfo(this.unparentedMetadataFileInfo, true);

      var taskMetadataFileInfo = new MetadataFileInfo();
      taskMetadataFileInfo.FileName = "Задача.mtd";
      taskMetadataFileInfo.NameGuid = "d795d1f6-45c1-4e5e-9677-b53fb7280c7e";
      taskMetadataFileInfo.FileContent = "=== Метаданные базовой задачи ===";
      this.AddMetadataFileInfo(taskMetadataFileInfo, true);

      var databookMetadataFileInfo = new MetadataFileInfo();
      databookMetadataFileInfo.FileName = "Справочник.mtd";
      databookMetadataFileInfo.NameGuid = "04581d26-0780-4cfd-b3cd-c2cafc5798b0";
      databookMetadataFileInfo.FileContent = "=== Метаданные справочника ===";
      this.AddMetadataFileInfo(databookMetadataFileInfo, true);

      var docMetadataFileInfo = new MetadataFileInfo();
      docMetadataFileInfo.FileName = "Документ.mtd";
      docMetadataFileInfo.NameGuid = "030d8d67-9b94-4f0d-bcc6-691016eb70f3";
      docMetadataFileInfo.FileContent = "=== Метаданные документа ===";
      this.AddMetadataFileInfo(docMetadataFileInfo, true);

      var childMetadataFileInfo = new MetadataFileInfo();
      childMetadataFileInfo.FileName = "Дочерняя сущность.mtd";
      childMetadataFileInfo.NameGuid = "a3d38bf5-0414-41f6-bb33-a4621d2e5a60";
      childMetadataFileInfo.FileContent = "=== Метаданные дочерней сущности ===";
      this.AddMetadataFileInfo(childMetadataFileInfo, true);

      var userMetadataFileInfo = new MetadataFileInfo();
      userMetadataFileInfo.FileName = "Пользователь.mtd";
      userMetadataFileInfo.NameGuid = "243c2d26-f5f7-495f-9faf-951d91215c77";
      userMetadataFileInfo.FileContent = "=== Метаданные пользователя ===";
      this.AddMetadataFileInfo(userMetadataFileInfo, true);

      var groupMetadataFileInfo = new MetadataFileInfo();
      groupMetadataFileInfo.FileName = "Группа.mtd";
      groupMetadataFileInfo.NameGuid = "31b5643f-ddd7-4021-8f3c-29b43f4df51f";
      groupMetadataFileInfo.FileContent = "=== Метаданные группы ===";
      this.AddMetadataFileInfo(groupMetadataFileInfo, true);

      var moduleMetadataFileInfo = new MetadataFileInfo();
      moduleMetadataFileInfo.FileName = "Модуль.mtd";
      moduleMetadataFileInfo.NameGuid = "00000000-0000-0000-0000-000000000000";
      moduleMetadataFileInfo.FileContent = "=== Метаданные модуля ===";
      this.AddMetadataFileInfo(moduleMetadataFileInfo, true);

      var reportMetadataFileInfo = new MetadataFileInfo();
      reportMetadataFileInfo.FileName = "Отчет.mtd";
      reportMetadataFileInfo.NameGuid = "cef9a810-3f30-4eca-9fe3-30992af0b818";
      reportMetadataFileInfo.FileContent = "=== Метаданные отчета ===";
      this.AddMetadataFileInfo(reportMetadataFileInfo, true);

      var assignmentMetadataFileInfo = new MetadataFileInfo();
      assignmentMetadataFileInfo.FileName = "Задание.mtd";
      assignmentMetadataFileInfo.NameGuid = "91cbfdc8-5d5d-465e-95a4-3235b8c01d5b";
      assignmentMetadataFileInfo.FileContent = "=== Метаданные задания ===";
      this.AddMetadataFileInfo(assignmentMetadataFileInfo, true);

      var versionMetadataFileInfo = new MetadataFileInfo();
      versionMetadataFileInfo.FileName = "Версия.mtd";
      versionMetadataFileInfo.NameGuid = "ca20a436-3798-4efc-9997-a3f31394c334";
      versionMetadataFileInfo.FileContent = "=== Метаданные версии ===";
      this.AddMetadataFileInfo(versionMetadataFileInfo, true);

      var groupRecipientMetadataFileInfo = new MetadataFileInfo();
      groupRecipientMetadataFileInfo.FileName = "Член группы.mtd";
      groupRecipientMetadataFileInfo.NameGuid = "20784da1-10a0-4ce1-97de-8a075142c47a";
      groupRecipientMetadataFileInfo.FileContent = "=== Метаданные члена группы ===";
      this.AddMetadataFileInfo(groupRecipientMetadataFileInfo, true);

      var noticeMetadataFileInfo = new MetadataFileInfo();
      noticeMetadataFileInfo.FileName = "Уведомление.mtd";
      noticeMetadataFileInfo.NameGuid = "ef79164b-2ce7-451b-9ba6-eb59dd9a4a74";
      noticeMetadataFileInfo.FileContent = "=== Метаданные уведомления ===";
      this.AddMetadataFileInfo(noticeMetadataFileInfo, true);

      var taskObserversMetadataFileInfo = new MetadataFileInfo();
      taskObserversMetadataFileInfo.FileName = "Наблюдатели.mtd";
      taskObserversMetadataFileInfo.NameGuid = "ac08b548-e666-4d9b-816f-a6c5e08e360f";
      taskObserversMetadataFileInfo.FileContent = "=== Метаданные наблюдателей ===";
      this.AddMetadataFileInfo(taskObserversMetadataFileInfo, true);

      var documentTemplateMetadataFileInfo = new MetadataFileInfo();
      documentTemplateMetadataFileInfo.FileName = "Шаблон документа.mtd";
      documentTemplateMetadataFileInfo.NameGuid = "9abcf1b7-f630-4a82-9912-7f79378ab199";
      documentTemplateMetadataFileInfo.FileContent = "=== Метаданные шаблона документа ===";
      this.AddMetadataFileInfo(documentTemplateMetadataFileInfo, true);

      var documentTemplateVersionsMetadataFileInfo = new MetadataFileInfo();
      documentTemplateVersionsMetadataFileInfo.FileName = "Версия шаблона документа.mtd";
      documentTemplateVersionsMetadataFileInfo.NameGuid = "4a8c9edc-8b7c-450e-8222-dc12b025e449";
      documentTemplateVersionsMetadataFileInfo.FileContent = "=== Метаданные версии шаблона документа ===";
      this.AddMetadataFileInfo(documentTemplateVersionsMetadataFileInfo, true);

      if (!string.IsNullOrEmpty(AppSettings.Instance.ConnectionString))
        this.developmentFolders.Add(ExportDevelopmentToLocalFolder());
      else
        this.developmentFolders = AppSettings.Instance.DevelopmentFolders;

      foreach (var developmentFolder in this.developmentFolders)
        FillMetadataTreeView(developmentFolder);

      foreach (var metadataFileInfo in this.allTreeNodes)
        AddMetadataFileInfo(metadataFileInfo);

      DisplayTreeView(toolStripTextBox.Text);
    }

    /// <summary>
    /// Извлечь имя файа метаданных из имени ресурса сборки.
    /// </summary>
    /// <param name="originalMetadataResourceName">Имя ресурса сборки</param>
    /// <returns>Имя файла метаданных.</returns>
    private string ExtractMetadataResourceName(string originalMetadataResourceName)
    {
      var matches = Regex.Matches(originalMetadataResourceName, "[^\\.]+\\.([^\\.]+\\.mtd)", RegexOptions.IgnoreCase);
      if (matches.Count > 0)
        return matches[0].Groups[1].Value;
      else
        return originalMetadataResourceName;
    }

    /// <summary>
    /// Получить уникальное имя файла.
    /// </summary>
    /// <param name="originalFileName">Оригинальное имя файла.</param>
    /// <returns>Уникальное имя файла.</returns>
    private string GetUniqueFileName(string originalFileName)
    {
      var currentFileName = originalFileName;
      int index = 0;
      while (File.Exists(currentFileName))
      {
        currentFileName = Path.Combine(Path.GetDirectoryName(currentFileName), Path.GetFileNameWithoutExtension(currentFileName) + index.ToString() + ".mtd");
        index++;
      }
      return currentFileName;
    }

    /// <summary>
    /// Получить запрос данных сборок.
    /// </summary>
    /// <param name="connection">Соединение.</param>
    /// <returns>Запрос данных сборок.</returns>
    private string GetAssemblyDataCommandText(DbConnection connection)
    {
      // Начиная с какой-то версии, данные сборок перенесли в другую таблицу, поддерживаенм оба варианта.
      using (var command = DatabaseEngine.Instance.CreateCommand("select 1 from information_schema.tables where table_name = 'sungero_ide_fulldeploydata'", connection))
      {
        using (var reader = command.ExecuteReader())
        {
          if (reader.HasRows)
          {
            return "select sif.name, sifdd.data " +
              "from sungero_ide_fulldeploy sif " +
              "  inner join sungero_ide_fulldeploydata sifdd " +
              "    on sif.assemblydatahash = sifdd.hash";
          }
          else
          {
            return "select name, assemblydata FROM sungero_ide_fulldeploy";
          }
        }
      }
    }

    /// <summary>
    /// Экспортировать разработку в локальную папку.
    /// </summary>
    private string ExportDevelopmentToLocalFolder()
    {
      var developmentFolder = Path.GetTempPath();
      developmentFolder = Path.Combine(developmentFolder, Guid.NewGuid().ToString());
      Directory.CreateDirectory(developmentFolder);

      using var connection = DatabaseEngine.Instance.CreateConnection(AppSettings.Instance.ConnectionString);
      connection.Open();

      using (var command = DatabaseEngine.Instance.CreateCommand(this.GetAssemblyDataCommandText(connection), connection))
      {
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var assemblyName = reader["name"].ToString();
            // if (assemblyName.StartsWith("shared"))
            {
              var assemblyFileName = Path.Combine(developmentFolder, assemblyName);
              if (assemblyFileName.EndsWith(".dll"))
              {
                var assemblyFolder = Path.GetDirectoryName(assemblyFileName);
                Directory.CreateDirectory(assemblyFolder);

                byte[] assemblyData = null;
                if (reader.GetName(1) == "assemblydata")
                  assemblyData = (byte[])reader["assemblydata"];
                else
                  assemblyData = (byte[])reader["data"];

                File.WriteAllBytes(assemblyFileName, assemblyData);
                if (assemblyName.StartsWith("shared"))
                {
                  var assembly = Assembly.LoadFrom(assemblyFileName);
                  var metadataResourceNames = assembly.GetManifestResourceNames().Where(resourceName => resourceName.EndsWith(".mtd"));
                  foreach (var metadataResourceName in metadataResourceNames)
                  {
                    var metadataFileName = Path.Combine(Path.GetDirectoryName(assemblyFileName), metadataResourceName);
                    metadataFileName = GetUniqueFileName(metadataFileName);
                    using (var stream = assembly.GetManifestResourceStream(metadataResourceName))
                    {
                      using (var memory = new MemoryStream())
                      {
                        stream.CopyTo(memory);
                        File.WriteAllBytes(metadataFileName, memory.ToArray());
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      return developmentFolder;
    }

    /// <summary>
    /// Обновить TextBox и строку состояния.
    /// </summary>
    /// <param name="metadataFileInfo">Информация о файле метаданных.</param>
    private void UpdateTextBoxAndStatusLabel(MetadataFileInfo metadataFileInfo)
    {
      string fileText;

      if (!string.IsNullOrEmpty(metadataFileInfo.FileContent) && metadataFileInfo.FileContent.StartsWith("==="))
        fileText = string.Empty;
      else
        fileText = File.ReadAllText(metadataFileInfo.FileName);

      this.scintilla.Text = fileText;
      toolStripStatusLabel.Text = metadataFileInfo.FileName;
    }

    private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      var metadataFileInfo = ((MetadataTreeNode)e.Node.Tag).MetadataFileInfo;
      this.UpdateTextBoxAndStatusLabel(metadataFileInfo);
    }

    private void toolStripTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Return)
        DisplayTreeView(toolStripTextBox.Text);
    }

    private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
    {
      toolStripTextBox.Width = splitContainer.Panel2.Left - 5;
    }

    private void ShowSearchForm()
    {
      var searchForm = new SearchForm(this);
      searchForm.ShowDialog();
    }

    private void toolStripFindButton_Click(object sender, EventArgs e)
    {
      this.ShowSearchForm();
    }

    private void toolStripMenuFindItem_Click(object sender, EventArgs e)
    {
      this.ShowSearchForm();
    }

    private void toolStripMenuExitItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void toolStripExpandButton_Click(object sender, EventArgs e)
    {
      this.treeView.ExpandAll();
    }

    private void toolStripCollapseButton_Click(object sender, EventArgs e)
    {
      this.treeView.CollapseAll();
    }
  }
}
