using System.Collections.Generic;

namespace MetadataBrowser
{
  public class MetadataTree
  {
    public List<MetadataTreeNode> Nodes { get; }

    public MetadataTree()
    {
      this.Nodes = new List<MetadataTreeNode>();
    }
  }
}
