
namespace MetadataBrowser
{
  partial class MetadataBrowserForm
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetadataBrowserForm));
      this.splitContainer = new System.Windows.Forms.SplitContainer();
      this.treeView = new System.Windows.Forms.TreeView();
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.scintilla = new ScintillaNET.Scintilla();
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.toolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripFindButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripExpandButton = new System.Windows.Forms.ToolStripButton();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.toolStripMenuFileItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuFindItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripFileSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuExitItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripCollapseButton = new System.Windows.Forms.ToolStripButton();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
      this.splitContainer.Panel1.SuspendLayout();
      this.splitContainer.Panel2.SuspendLayout();
      this.splitContainer.SuspendLayout();
      this.statusStrip.SuspendLayout();
      this.toolStrip.SuspendLayout();
      this.menuStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer
      // 
      this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer.Location = new System.Drawing.Point(8, 50);
      this.splitContainer.Name = "splitContainer";
      // 
      // splitContainer.Panel1
      // 
      this.splitContainer.Panel1.Controls.Add(this.treeView);
      // 
      // splitContainer.Panel2
      // 
      this.splitContainer.Panel2.Controls.Add(this.scintilla);
      this.splitContainer.Size = new System.Drawing.Size(865, 370);
      this.splitContainer.SplitterDistance = 287;
      this.splitContainer.TabIndex = 4;
      this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
      // 
      // treeView
      // 
      this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.treeView.HideSelection = false;
      this.treeView.ImageIndex = 0;
      this.treeView.ImageList = this.imageList;
      this.treeView.Location = new System.Drawing.Point(0, 0);
      this.treeView.Name = "treeView";
      this.treeView.SelectedImageIndex = 0;
      this.treeView.Size = new System.Drawing.Size(286, 370);
      this.treeView.TabIndex = 0;
      this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
      // 
      // imageList
      // 
      this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
      this.imageList.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList.Images.SetKeyName(0, "desktop_16.png");
      // 
      // scintilla
      // 
      this.scintilla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.scintilla.AutoCMaxHeight = 9;
      this.scintilla.BiDirectionality = ScintillaNET.BiDirectionalDisplayType.Disabled;
      this.scintilla.CaretLineBackColor = System.Drawing.Color.White;
      this.scintilla.CaretLineVisible = true;
      this.scintilla.LexerName = null;
      this.scintilla.Location = new System.Drawing.Point(1, 0);
      this.scintilla.Name = "scintilla";
      this.scintilla.ScrollWidth = 49;
      this.scintilla.Size = new System.Drawing.Size(573, 370);
      this.scintilla.TabIndents = true;
      this.scintilla.TabIndex = 1;
      this.scintilla.UseRightToLeftReadingLayout = false;
      this.scintilla.WrapMode = ScintillaNET.WrapMode.None;
      // 
      // statusStrip
      // 
      this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
      this.statusStrip.Location = new System.Drawing.Point(0, 428);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Size = new System.Drawing.Size(884, 22);
      this.statusStrip.TabIndex = 5;
      this.statusStrip.Text = "statusStrip";
      // 
      // toolStripStatusLabel
      // 
      this.toolStripStatusLabel.Name = "toolStripStatusLabel";
      this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
      // 
      // toolStrip
      // 
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox,
            this.toolStripSeparator,
            this.toolStripFindButton,
            this.toolStripExpandButton,
            this.toolStripCollapseButton});
      this.toolStrip.Location = new System.Drawing.Point(0, 24);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Size = new System.Drawing.Size(884, 25);
      this.toolStrip.TabIndex = 6;
      this.toolStrip.Text = "toolStrip";
      // 
      // toolStripTextBox
      // 
      this.toolStripTextBox.AutoSize = false;
      this.toolStripTextBox.Name = "toolStripTextBox";
      this.toolStripTextBox.Size = new System.Drawing.Size(280, 25);
      this.toolStripTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox_KeyDown);
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripFindButton
      // 
      this.toolStripFindButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripFindButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripFindButton.Image")));
      this.toolStripFindButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripFindButton.Name = "toolStripFindButton";
      this.toolStripFindButton.Size = new System.Drawing.Size(23, 22);
      this.toolStripFindButton.Text = "Поиск...";
      this.toolStripFindButton.Click += new System.EventHandler(this.toolStripFindButton_Click);
      // 
      // toolStripExpandButton
      // 
      this.toolStripExpandButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripExpandButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripExpandButton.Image")));
      this.toolStripExpandButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripExpandButton.Name = "toolStripExpandButton";
      this.toolStripExpandButton.Size = new System.Drawing.Size(23, 22);
      this.toolStripExpandButton.Text = "Развернуть все";
      this.toolStripExpandButton.Click += new System.EventHandler(this.toolStripExpandButton_Click);
      // 
      // menuStrip
      // 
      this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuFileItem});
      this.menuStrip.Location = new System.Drawing.Point(0, 0);
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.Size = new System.Drawing.Size(884, 24);
      this.menuStrip.TabIndex = 7;
      this.menuStrip.Text = "menuStrip";
      // 
      // toolStripMenuFileItem
      // 
      this.toolStripMenuFileItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuFindItem,
            this.toolStripFileSeparator,
            this.toolStripMenuExitItem});
      this.toolStripMenuFileItem.Name = "toolStripMenuFileItem";
      this.toolStripMenuFileItem.Size = new System.Drawing.Size(37, 20);
      this.toolStripMenuFileItem.Text = "&File";
      // 
      // toolStripMenuFindItem
      // 
      this.toolStripMenuFindItem.Name = "toolStripMenuFindItem";
      this.toolStripMenuFindItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
      this.toolStripMenuFindItem.Size = new System.Drawing.Size(146, 22);
      this.toolStripMenuFindItem.Text = "Find...";
      this.toolStripMenuFindItem.Click += new System.EventHandler(this.toolStripMenuFindItem_Click);
      // 
      // toolStripFileSeparator
      // 
      this.toolStripFileSeparator.Name = "toolStripFileSeparator";
      this.toolStripFileSeparator.Size = new System.Drawing.Size(143, 6);
      // 
      // toolStripMenuExitItem
      // 
      this.toolStripMenuExitItem.Name = "toolStripMenuExitItem";
      this.toolStripMenuExitItem.Size = new System.Drawing.Size(146, 22);
      this.toolStripMenuExitItem.Text = "E&xit";
      this.toolStripMenuExitItem.Click += new System.EventHandler(this.toolStripMenuExitItem_Click);
      // 
      // toolStripCollapseButton
      // 
      this.toolStripCollapseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripCollapseButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripCollapseButton.Image")));
      this.toolStripCollapseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripCollapseButton.Name = "toolStripCollapseButton";
      this.toolStripCollapseButton.Size = new System.Drawing.Size(23, 22);
      this.toolStripCollapseButton.Text = "Свернуть все";
      this.toolStripCollapseButton.Click += new System.EventHandler(this.toolStripCollapseButton_Click);
      // 
      // MetadataBrowserForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(884, 450);
      this.Controls.Add(this.toolStrip);
      this.Controls.Add(this.statusStrip);
      this.Controls.Add(this.menuStrip);
      this.Controls.Add(this.splitContainer);
      this.MainMenuStrip = this.menuStrip;
      this.Name = "MetadataBrowserForm";
      this.Text = "MetadataBrowser";
      this.Load += new System.EventHandler(this.MetadataBrowserForm_Load);
      this.splitContainer.Panel1.ResumeLayout(false);
      this.splitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
      this.splitContainer.ResumeLayout(false);
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.SplitContainer splitContainer;
    private System.Windows.Forms.TreeView treeView;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripTextBox toolStripTextBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    private System.Windows.Forms.ToolStripButton toolStripFindButton;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuFileItem;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuFindItem;
    private System.Windows.Forms.ToolStripSeparator toolStripFileSeparator;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuExitItem;
    private System.Windows.Forms.ImageList imageList;
    private ScintillaNET.Scintilla scintilla;
    private System.Windows.Forms.ToolStripButton toolStripExpandButton;
    private System.Windows.Forms.ToolStripButton toolStripCollapseButton;
  }
}

