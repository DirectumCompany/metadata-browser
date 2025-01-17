using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MetadataBrowser
{
  public partial class SearchForm : Form
  {
    private MetadataBrowserForm parent;
    private List<SearchResult> searchResults;
    private int searchResultIndex;
    public SearchForm(MetadataBrowserForm parent)
    {
      this.parent = parent;
      InitializeComponent();
    }

    private void GoToSearchResultByIndex(int index)
    {
      if (index < searchResults.Count)
      {
        if (searchResults[index].Node != null)
        {
          if (this.parent.GoToNode(searchResults[index].Node))
            this.parent.SelectInEditor(searchResults[index].Index, searchTextBox.Text.Length);
        }
      }
    }

    private void searchButton_Click(object sender, EventArgs e)
    {
      if (this.currentFileOnlyCheckBox.Checked)
        this.searchResults = this.parent.FindInCurrentNodeByContent(searchTextBox.Text).ToList();
      else
        this.searchResults = this.parent.FindTreeNodeByContent(searchTextBox.Text).ToList();

      var forRemove = new List<SearchResult>();
      foreach (var currentResult in this.searchResults)
      {
        var treeNode = this.parent.FindTreeNodeByMetadataTreeNode(currentResult.Node);
        if (treeNode == null)
          forRemove.Add(currentResult);
      }
      foreach (var currentRemoved in forRemove)
        this.searchResults.Remove(currentRemoved);
      
      this.searchResultIndex = 0;
      this.GoToSearchResultByIndex(this.searchResultIndex);
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
      if (this.searchResults == null)
      {
        this.searchButton_Click(this, new EventArgs());
        return;
      }
      if (this.searchResultIndex < this.searchResults.Count - 1)
      {
        this.searchResultIndex++;
        this.GoToSearchResultByIndex(this.searchResultIndex);
      }
      else
        MessageBox.Show("Поиск завершен");      
    }

    private void prevButton_Click(object sender, EventArgs e)
    {
      if (this.searchResultIndex >= 1)
      {
        this.searchResultIndex--;
        this.GoToSearchResultByIndex(this.searchResultIndex);
      }
      else
        MessageBox.Show("Поиск завершен");      
    }

    private void SearchForm_Shown(object sender, EventArgs e)
    {
      this.searchResults = null;
      this.searchTextBox_TextChanged(this, new EventArgs());
    }

    private void searchTextBox_TextChanged(object sender, EventArgs e)
    {
      var isEmptyText = string.IsNullOrEmpty(this.searchTextBox.Text);
      this.nextButton.Enabled = !isEmptyText;
      this.prevButton.Enabled = !isEmptyText;
      this.searchButton.Enabled = !isEmptyText;
    }
  }
}
