namespace SherpJobLogger {
  partial class MainForm {
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.buttonExit = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.checkBoxJobName = new System.Windows.Forms.CheckBox();
      this.treeViewAllJobs = new System.Windows.Forms.TreeView();
      this.tabPageJobs = new System.Windows.Forms.TabPage();
      this.label7 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.dateTimeDinnerTo = new System.Windows.Forms.DateTimePicker();
      this.dateTimeDinnerFrom = new System.Windows.Forms.DateTimePicker();
      this.checkBoxDinner = new System.Windows.Forms.CheckBox();
      this.buttonRegisterJobs = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.dateTimeToHours = new System.Windows.Forms.DateTimePicker();
      this.dateTimeToDays = new System.Windows.Forms.DateTimePicker();
      this.dateTimeFromHours = new System.Windows.Forms.DateTimePicker();
      this.dateTimeFromDays = new System.Windows.Forms.DateTimePicker();
      this.dataGridViewJobs = new System.Windows.Forms.DataGridView();
      this.JobName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.IDWork = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.JobDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.JobRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.buttonJobDescriptions = new System.Windows.Forms.Button();
      this.checkBoxWhatIf = new System.Windows.Forms.CheckBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPageJobs.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJobs)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonExit
      // 
      this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonExit.Location = new System.Drawing.Point(607, 378);
      this.buttonExit.Name = "buttonExit";
      this.buttonExit.Size = new System.Drawing.Size(75, 23);
      this.buttonExit.TabIndex = 0;
      this.buttonExit.Text = "E&xit";
      this.buttonExit.UseVisualStyleBackColor = true;
      this.buttonExit.Click += new System.EventHandler(this.ButtonExit_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(40, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "UserID";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 30);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(60, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "ExecutorID";
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPageJobs);
      this.tabControl1.Location = new System.Drawing.Point(12, 46);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(670, 326);
      this.tabControl1.TabIndex = 4;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.treeViewAllJobs);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(662, 300);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Выбор проектных работ.";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // checkBoxJobName
      // 
      this.checkBoxJobName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.checkBoxJobName.AutoSize = true;
      this.checkBoxJobName.Checked = true;
      this.checkBoxJobName.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxJobName.Location = new System.Drawing.Point(12, 384);
      this.checkBoxJobName.Name = "checkBoxJobName";
      this.checkBoxJobName.Size = new System.Drawing.Size(207, 17);
      this.checkBoxJobName.TabIndex = 8;
      this.checkBoxJobName.Text = "Включать имена проектов в задачи";
      this.checkBoxJobName.UseVisualStyleBackColor = true;
      // 
      // treeViewAllJobs
      // 
      this.treeViewAllJobs.CheckBoxes = true;
      this.treeViewAllJobs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeViewAllJobs.FullRowSelect = true;
      this.treeViewAllJobs.Location = new System.Drawing.Point(3, 3);
      this.treeViewAllJobs.Name = "treeViewAllJobs";
      this.treeViewAllJobs.ShowNodeToolTips = true;
      this.treeViewAllJobs.Size = new System.Drawing.Size(656, 294);
      this.treeViewAllJobs.TabIndex = 7;
      this.treeViewAllJobs.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterCheck);
      // 
      // tabPageJobs
      // 
      this.tabPageJobs.Controls.Add(this.label7);
      this.tabPageJobs.Controls.Add(this.label3);
      this.tabPageJobs.Controls.Add(this.dateTimeDinnerTo);
      this.tabPageJobs.Controls.Add(this.dateTimeDinnerFrom);
      this.tabPageJobs.Controls.Add(this.checkBoxDinner);
      this.tabPageJobs.Controls.Add(this.buttonRegisterJobs);
      this.tabPageJobs.Controls.Add(this.label6);
      this.tabPageJobs.Controls.Add(this.label5);
      this.tabPageJobs.Controls.Add(this.label4);
      this.tabPageJobs.Controls.Add(this.dateTimeToHours);
      this.tabPageJobs.Controls.Add(this.dateTimeToDays);
      this.tabPageJobs.Controls.Add(this.dateTimeFromHours);
      this.tabPageJobs.Controls.Add(this.dateTimeFromDays);
      this.tabPageJobs.Controls.Add(this.dataGridViewJobs);
      this.tabPageJobs.Location = new System.Drawing.Point(4, 22);
      this.tabPageJobs.Name = "tabPageJobs";
      this.tabPageJobs.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageJobs.Size = new System.Drawing.Size(662, 300);
      this.tabPageJobs.TabIndex = 3;
      this.tabPageJobs.Text = "Заполнение работ";
      this.tabPageJobs.UseVisualStyleBackColor = true;
      this.tabPageJobs.Enter += new System.EventHandler(this.TabPageJobs_Enter);
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(480, 266);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(22, 13);
      this.label7.TabIndex = 20;
      this.label7.Text = "по:";
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(386, 266);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(16, 13);
      this.label3.TabIndex = 19;
      this.label3.Text = "с:";
      // 
      // dateTimeDinnerTo
      // 
      this.dateTimeDinnerTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.dateTimeDinnerTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
      this.dateTimeDinnerTo.Location = new System.Drawing.Point(503, 269);
      this.dateTimeDinnerTo.Name = "dateTimeDinnerTo";
      this.dateTimeDinnerTo.ShowUpDown = true;
      this.dateTimeDinnerTo.Size = new System.Drawing.Size(68, 20);
      this.dateTimeDinnerTo.TabIndex = 18;
      this.dateTimeDinnerTo.Value = new System.DateTime(2001, 1, 1, 14, 0, 0, 0);
      // 
      // dateTimeDinnerFrom
      // 
      this.dateTimeDinnerFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.dateTimeDinnerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
      this.dateTimeDinnerFrom.Location = new System.Drawing.Point(408, 269);
      this.dateTimeDinnerFrom.Name = "dateTimeDinnerFrom";
      this.dateTimeDinnerFrom.ShowUpDown = true;
      this.dateTimeDinnerFrom.Size = new System.Drawing.Size(68, 20);
      this.dateTimeDinnerFrom.TabIndex = 17;
      this.dateTimeDinnerFrom.Value = new System.DateTime(2001, 1, 1, 13, 0, 0, 0);
      // 
      // checkBoxDinner
      // 
      this.checkBoxDinner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.checkBoxDinner.AutoSize = true;
      this.checkBoxDinner.Checked = true;
      this.checkBoxDinner.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxDinner.Location = new System.Drawing.Point(389, 252);
      this.checkBoxDinner.Name = "checkBoxDinner";
      this.checkBoxDinner.Size = new System.Drawing.Size(108, 17);
      this.checkBoxDinner.TabIndex = 16;
      this.checkBoxDinner.Text = "Учитывать обед";
      this.checkBoxDinner.UseVisualStyleBackColor = true;
      this.checkBoxDinner.CheckedChanged += new System.EventHandler(this.CheckBoxDinner_CheckedChanged);
      // 
      // buttonRegisterJobs
      // 
      this.buttonRegisterJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonRegisterJobs.Location = new System.Drawing.Point(581, 266);
      this.buttonRegisterJobs.Name = "buttonRegisterJobs";
      this.buttonRegisterJobs.Size = new System.Drawing.Size(75, 23);
      this.buttonRegisterJobs.TabIndex = 15;
      this.buttonRegisterJobs.Text = "Заполнить";
      this.buttonRegisterJobs.UseVisualStyleBackColor = true;
      this.buttonRegisterJobs.Click += new System.EventHandler(this.ButtonRegisterJobs_Click);
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 253);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(101, 13);
      this.label6.TabIndex = 13;
      this.label6.Text = "Заполнить работы";
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(187, 266);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(22, 13);
      this.label5.TabIndex = 12;
      this.label5.Text = "по:";
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 266);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(16, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "с:";
      // 
      // dateTimeToHours
      // 
      this.dateTimeToHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.dateTimeToHours.Format = System.Windows.Forms.DateTimePickerFormat.Time;
      this.dateTimeToHours.Location = new System.Drawing.Point(299, 270);
      this.dateTimeToHours.Name = "dateTimeToHours";
      this.dateTimeToHours.ShowUpDown = true;
      this.dateTimeToHours.Size = new System.Drawing.Size(68, 20);
      this.dateTimeToHours.TabIndex = 11;
      this.dateTimeToHours.Value = new System.DateTime(2001, 1, 1, 18, 0, 0, 0);
      // 
      // dateTimeToDays
      // 
      this.dateTimeToDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.dateTimeToDays.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dateTimeToDays.Location = new System.Drawing.Point(215, 270);
      this.dateTimeToDays.Name = "dateTimeToDays";
      this.dateTimeToDays.Size = new System.Drawing.Size(78, 20);
      this.dateTimeToDays.TabIndex = 10;
      // 
      // dateTimeFromHours
      // 
      this.dateTimeFromHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.dateTimeFromHours.Format = System.Windows.Forms.DateTimePickerFormat.Time;
      this.dateTimeFromHours.Location = new System.Drawing.Point(112, 270);
      this.dateTimeFromHours.Name = "dateTimeFromHours";
      this.dateTimeFromHours.ShowUpDown = true;
      this.dateTimeFromHours.Size = new System.Drawing.Size(68, 20);
      this.dateTimeFromHours.TabIndex = 9;
      this.dateTimeFromHours.Value = new System.DateTime(2001, 1, 1, 9, 0, 0, 0);
      // 
      // dateTimeFromDays
      // 
      this.dateTimeFromDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.dateTimeFromDays.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dateTimeFromDays.Location = new System.Drawing.Point(28, 270);
      this.dateTimeFromDays.Name = "dateTimeFromDays";
      this.dateTimeFromDays.Size = new System.Drawing.Size(78, 20);
      this.dateTimeFromDays.TabIndex = 8;
      // 
      // dataGridViewJobs
      // 
      this.dataGridViewJobs.AllowUserToAddRows = false;
      this.dataGridViewJobs.AllowUserToDeleteRows = false;
      this.dataGridViewJobs.AllowUserToResizeRows = false;
      this.dataGridViewJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridViewJobs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
      this.dataGridViewJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewJobs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JobName,
            this.IDWork,
            this.JobDescription,
            this.JobRate});
      this.dataGridViewJobs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.dataGridViewJobs.Location = new System.Drawing.Point(3, 3);
      this.dataGridViewJobs.Name = "dataGridViewJobs";
      this.dataGridViewJobs.RowHeadersVisible = false;
      this.dataGridViewJobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
      this.dataGridViewJobs.Size = new System.Drawing.Size(656, 247);
      this.dataGridViewJobs.TabIndex = 2;
      this.dataGridViewJobs.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseDoubleClick);
      // 
      // JobName
      // 
      this.JobName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.JobName.DefaultCellStyle = dataGridViewCellStyle6;
      this.JobName.HeaderText = "Проектная работа";
      this.JobName.Name = "JobName";
      this.JobName.ReadOnly = true;
      // 
      // IDWork
      // 
      this.IDWork.HeaderText = "IDWork";
      this.IDWork.Name = "IDWork";
      this.IDWork.ReadOnly = true;
      this.IDWork.Visible = false;
      // 
      // JobDescription
      // 
      this.JobDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      dataGridViewCellStyle7.NullValue = "random";
      dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.JobDescription.DefaultCellStyle = dataGridViewCellStyle7;
      this.JobDescription.HeaderText = "Что сделано";
      this.JobDescription.Name = "JobDescription";
      this.JobDescription.ReadOnly = true;
      this.JobDescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      // 
      // JobRate
      // 
      this.JobRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      dataGridViewCellStyle8.Format = "N2";
      dataGridViewCellStyle8.NullValue = "1";
      this.JobRate.DefaultCellStyle = dataGridViewCellStyle8;
      this.JobRate.HeaderText = "доля участия";
      this.JobRate.Name = "JobRate";
      this.JobRate.Width = 98;
      // 
      // buttonJobDescriptions
      // 
      this.buttonJobDescriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonJobDescriptions.Location = new System.Drawing.Point(499, 378);
      this.buttonJobDescriptions.Name = "buttonJobDescriptions";
      this.buttonJobDescriptions.Size = new System.Drawing.Size(102, 23);
      this.buttonJobDescriptions.TabIndex = 5;
      this.buttonJobDescriptions.Text = "Описания работ";
      this.buttonJobDescriptions.UseVisualStyleBackColor = true;
      this.buttonJobDescriptions.Click += new System.EventHandler(this.ButtonJobDescriptions_Click);
      // 
      // checkBoxWhatIf
      // 
      this.checkBoxWhatIf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.checkBoxWhatIf.AutoSize = true;
      this.checkBoxWhatIf.Checked = true;
      this.checkBoxWhatIf.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxWhatIf.Location = new System.Drawing.Point(601, 8);
      this.checkBoxWhatIf.Name = "checkBoxWhatIf";
      this.checkBoxWhatIf.Size = new System.Drawing.Size(58, 17);
      this.checkBoxWhatIf.TabIndex = 6;
      this.checkBoxWhatIf.Text = "WhatIf";
      this.checkBoxWhatIf.UseVisualStyleBackColor = true;
      this.checkBoxWhatIf.CheckedChanged += new System.EventHandler(this.CheckBoxWhatIf_CheckedChanged);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonExit;
      this.ClientSize = new System.Drawing.Size(694, 413);
      this.Controls.Add(this.checkBoxJobName);
      this.Controls.Add(this.checkBoxWhatIf);
      this.Controls.Add(this.buttonJobDescriptions);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonExit);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(705, 227);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Form1";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.Shown += new System.EventHandler(this.MainForm_Shown);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPageJobs.ResumeLayout(false);
      this.tabPageJobs.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJobs)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonExit;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TreeView treeViewAllJobs;
    private System.Windows.Forms.Button buttonJobDescriptions;
    private System.Windows.Forms.TabPage tabPageJobs;
    private System.Windows.Forms.DataGridView dataGridViewJobs;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.DateTimePicker dateTimeToHours;
    private System.Windows.Forms.DateTimePicker dateTimeToDays;
    private System.Windows.Forms.DateTimePicker dateTimeFromHours;
    private System.Windows.Forms.DateTimePicker dateTimeFromDays;
    private System.Windows.Forms.CheckBox checkBoxJobName;
    private System.Windows.Forms.Button buttonRegisterJobs;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.DateTimePicker dateTimeDinnerTo;
    private System.Windows.Forms.DateTimePicker dateTimeDinnerFrom;
    private System.Windows.Forms.CheckBox checkBoxDinner;
    private System.Windows.Forms.DataGridViewTextBoxColumn JobName;
    private System.Windows.Forms.DataGridViewTextBoxColumn IDWork;
    private System.Windows.Forms.DataGridViewTextBoxColumn JobDescription;
    private System.Windows.Forms.DataGridViewTextBoxColumn JobRate;
    private System.Windows.Forms.CheckBox checkBoxWhatIf;
  }
}

