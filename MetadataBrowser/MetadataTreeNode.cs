using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataBrowser
{
  public class MetadataTreeNode
  {
    public MetadataFileInfo MetadataFileInfo { get; set; }

    public List<MetadataTreeNode> Nodes { get; }

    public string Caption { get; set; }

    public MetadataTreeNode Parent { get; set; }

    public MetadataTreeNode(string caption, MetadataTreeNode parent)
    {
      this.Nodes = new List<MetadataTreeNode>();
      this.Caption = caption;
      this.Parent = parent;
    }
  }
}
