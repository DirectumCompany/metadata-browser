using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataBrowser
{
  public class SearchResult
  {
    /// <summary>
    /// Узел дерева.
    /// </summary>
    public MetadataTreeNode Node { get; set; }

    /// <summary>
    /// Индекс искомой строки в файле.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="node">Узел дерева.</param>
    /// <param name="index">Индекс искомой подстроки.</param>
    public SearchResult(MetadataTreeNode node, int index)
    {
      this.Node = node;
      this.Index = index;
    }
  }
}
