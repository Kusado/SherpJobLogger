namespace SherpJobLogger {
  partial class Splash {
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
      this.ButtonCancel = new System.Windows.Forms.Button();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.label3 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // ButtonCancel
      // 
      this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.ButtonCancel.Location = new System.Drawing.Point(194, 138);
      this.ButtonCancel.Name = "ButtonCancel";
      this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
      this.ButtonCancel.TabIndex = 0;
      this.ButtonCancel.Text = "Cancel";
      this.ButtonCancel.UseVisualStyleBackColor = false;
      this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
      // 
      // pictureBox1
      // 
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Image = global::SherpJobLogger.Properties.Resources.giphy;
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(479, 269);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.UseWaitCursor = true;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label1.ForeColor = System.Drawing.Color.Green;
      this.label1.Location = new System.Drawing.Point(12, 2);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(455, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Загрузка...";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.BackColor = System.Drawing.Color.Transparent;
      this.label2.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label2.ForeColor = System.Drawing.Color.Green;
      this.label2.Location = new System.Drawing.Point(12, 15);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(455, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Загрузка...";
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new System.Drawing.Point(357, 0);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(122, 17);
      this.checkBox1.TabIndex = 4;
      this.checkBox1.Text = "LoadDefaultSettings";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label3.Location = new System.Drawing.Point(354, 242);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(113, 18);
      this.label3.TabIndex = 5;
      this.label3.Text = "label3";
      this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
      // 
      // Splash
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.ButtonCancel;
      this.ClientSize = new System.Drawing.Size(479, 269);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.ButtonCancel);
      this.Cursor = System.Windows.Forms.Cursors.AppStarting;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "Splash";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Form1";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Splash_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button ButtonCancel;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.Label label3;
  }
}