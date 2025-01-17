
namespace MetadataBrowser
{
  partial class SearchForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.searchTextBox = new System.Windows.Forms.TextBox();
      this.searchLabel = new System.Windows.Forms.Label();
      this.searchButton = new System.Windows.Forms.Button();
      this.nextButton = new System.Windows.Forms.Button();
      this.prevButton = new System.Windows.Forms.Button();
      this.currentFileOnlyCheckBox = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // searchTextBox
      // 
      this.searchTextBox.Location = new System.Drawing.Point(66, 23);
      this.searchTextBox.Name = "searchTextBox";
      this.searchTextBox.Size = new System.Drawing.Size(378, 23);
      this.searchTextBox.TabIndex = 0;
      this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
      // 
      // searchLabel
      // 
      this.searchLabel.AutoSize = true;
      this.searchLabel.Location = new System.Drawing.Point(12, 26);
      this.searchLabel.Name = "searchLabel";
      this.searchLabel.Size = new System.Drawing.Size(48, 15);
      this.searchLabel.TabIndex = 1;
      this.searchLabel.Text = "Искать:";
      // 
      // searchButton
      // 
      this.searchButton.Location = new System.Drawing.Point(369, 77);
      this.searchButton.Name = "searchButton";
      this.searchButton.Size = new System.Drawing.Size(75, 23);
      this.searchButton.TabIndex = 2;
      this.searchButton.Text = "Искать";
      this.searchButton.UseVisualStyleBackColor = true;
      this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
      // 
      // nextButton
      // 
      this.nextButton.Location = new System.Drawing.Point(287, 77);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(75, 23);
      this.nextButton.TabIndex = 3;
      this.nextButton.Text = "Далее";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // prevButton
      // 
      this.prevButton.Location = new System.Drawing.Point(206, 77);
      this.prevButton.Name = "prevButton";
      this.prevButton.Size = new System.Drawing.Size(75, 23);
      this.prevButton.TabIndex = 4;
      this.prevButton.Text = "Назад";
      this.prevButton.UseVisualStyleBackColor = true;
      this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
      // 
      // currentFileOnlyCheckBox
      // 
      this.currentFileOnlyCheckBox.AutoSize = true;
      this.currentFileOnlyCheckBox.Location = new System.Drawing.Point(66, 52);
      this.currentFileOnlyCheckBox.Name = "currentFileOnlyCheckBox";
      this.currentFileOnlyCheckBox.Size = new System.Drawing.Size(163, 19);
      this.currentFileOnlyCheckBox.TabIndex = 5;
      this.currentFileOnlyCheckBox.Text = "только в текущем файле";
      this.currentFileOnlyCheckBox.UseVisualStyleBackColor = true;
      // 
      // SearchForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(460, 112);
      this.Controls.Add(this.currentFileOnlyCheckBox);
      this.Controls.Add(this.prevButton);
      this.Controls.Add(this.nextButton);
      this.Controls.Add(this.searchButton);
      this.Controls.Add(this.searchLabel);
      this.Controls.Add(this.searchTextBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SearchForm";
      this.ShowInTaskbar = false;
      this.Text = "Поиск";
      this.Shown += new System.EventHandler(this.SearchForm_Shown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox searchTextBox;
    private System.Windows.Forms.Label searchLabel;
    private System.Windows.Forms.Button searchButton;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.Button prevButton;
    private System.Windows.Forms.CheckBox currentFileOnlyCheckBox;
  }
}