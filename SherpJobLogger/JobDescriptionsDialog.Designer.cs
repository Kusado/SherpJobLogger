namespace SherpJobLogger {
  partial class JobDescriptionsDialog {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.dataGridViewJobsDescriptions = new System.Windows.Forms.DataGridView();
      this.JobDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.buttonOk = new System.Windows.Forms.Button();
      this.buttonApply = new System.Windows.Forms.Button();
      this.buttonRevert = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJobsDescriptions)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridViewJobsDescriptions
      // 
      this.dataGridViewJobsDescriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dataGridViewJobsDescriptions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      this.dataGridViewJobsDescriptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewJobsDescriptions.ColumnHeadersVisible = false;
      this.dataGridViewJobsDescriptions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JobDescription});
      this.dataGridViewJobsDescriptions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.dataGridViewJobsDescriptions.Location = new System.Drawing.Point(5, 0);
      this.dataGridViewJobsDescriptions.MultiSelect = false;
      this.dataGridViewJobsDescriptions.Name = "dataGridViewJobsDescriptions";
      this.dataGridViewJobsDescriptions.RowHeadersVisible = false;
      this.dataGridViewJobsDescriptions.RowHeadersWidth = 30;
      this.dataGridViewJobsDescriptions.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
      this.dataGridViewJobsDescriptions.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.dataGridViewJobsDescriptions.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewJobsDescriptions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewJobsDescriptions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridViewJobsDescriptions.Size = new System.Drawing.Size(494, 263);
      this.dataGridViewJobsDescriptions.TabIndex = 0;
      this.dataGridViewJobsDescriptions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewJobsDescriptions_CellDoubleClick);
      this.dataGridViewJobsDescriptions.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewJobsDescriptions_CellEndEdit);
      // 
      // JobDescription
      // 
      this.JobDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.JobDescription.FillWeight = 200F;
      this.JobDescription.HeaderText = "JobDescription";
      this.JobDescription.Name = "JobDescription";
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(424, 268);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 23);
      this.buttonCancel.TabIndex = 1;
      this.buttonCancel.Text = "&Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // buttonOk
      // 
      this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOk.Location = new System.Drawing.Point(343, 268);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.Size = new System.Drawing.Size(75, 23);
      this.buttonOk.TabIndex = 2;
      this.buttonOk.Text = "&Ok";
      this.buttonOk.UseVisualStyleBackColor = true;
      this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
      // 
      // buttonApply
      // 
      this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonApply.Location = new System.Drawing.Point(262, 268);
      this.buttonApply.Name = "buttonApply";
      this.buttonApply.Size = new System.Drawing.Size(75, 23);
      this.buttonApply.TabIndex = 3;
      this.buttonApply.Text = "&Apply";
      this.buttonApply.UseVisualStyleBackColor = true;
      this.buttonApply.Click += new System.EventHandler(this.button1_Click);
      // 
      // buttonRevert
      // 
      this.buttonRevert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.buttonRevert.Location = new System.Drawing.Point(4, 268);
      this.buttonRevert.Name = "buttonRevert";
      this.buttonRevert.Size = new System.Drawing.Size(75, 23);
      this.buttonRevert.TabIndex = 4;
      this.buttonRevert.Text = "&Revert";
      this.buttonRevert.UseVisualStyleBackColor = true;
      this.buttonRevert.Click += new System.EventHandler(this.buttonRevert_Click);
      // 
      // JobDescriptionsDialog
      // 
      this.AcceptButton = this.buttonOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonCancel;
      this.ClientSize = new System.Drawing.Size(505, 296);
      this.Controls.Add(this.buttonRevert);
      this.Controls.Add(this.buttonApply);
      this.Controls.Add(this.buttonOk);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.dataGridViewJobsDescriptions);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.MinimumSize = new System.Drawing.Size(343, 168);
      this.Name = "JobDescriptionsDialog";
      this.Text = "JobDescriptionsDialog";
      this.Load += new System.EventHandler(this.JobDescriptionsDialog_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJobsDescriptions)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridViewJobsDescriptions;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOk;
    private System.Windows.Forms.Button buttonApply;
    private System.Windows.Forms.Button buttonRevert;
    private System.Windows.Forms.DataGridViewTextBoxColumn JobDescription;
  }
}