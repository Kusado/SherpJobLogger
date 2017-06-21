using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SherpJobLogger {

  public partial class MainForm : Form {

    public MainForm() {
      InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e) {
      Splash.Status = "GetCurentUserID";
      this.CurrentUser = GetCurentUserID();
      this.label1.Text = $@"Your ID is: {this.CurrentUser.IDUser}. Your name is: {this.CurrentUser.FullName}";
      Splash.Status = "Get Executor ID";
      if (Settings.pType == ProjectControl.RFM) this.label2.Text = $@"Your ExecutorID is: {GetExecutorID(this.CurrentUser.IDUser)}";
      if (Settings.pType == ProjectControl.LG) this.label2.Text = $@"Your ExecutorID is: {GetExecutorID()}";

      if (this.ExecutorExists) {
        Splash.Status = "Get Tasks";
        this.Work = GetProjectWorkCU();
        Splash.Status = "Populate tree with tasks";
        PopulateJobsTree();
      }
      else this.treeViewAllJobs.Nodes.Add("No jobs for you...");
      var now = DateTime.Now;
      Splash.Status = "Get last logged job time...";
      if ((this.dateTimeFromDays.Value = GetLastLoggedJobTime()).Date != now.Date) SetDateTimeNBD(this.dateTimeFromDays);
      this.dateTimeToDays.Value = now;
      Splash.Status = "Showing main form...";
      Show();
      Focus();
      BringToFront();
      Activate();
    }

    private void MainForm_Shown(object sender, EventArgs e) {
      Splash.Status = "Closing splash screen";
      this.Splash.CloseSplash();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
      SqlConnection?.Close();
      SaveSettings();
    }

    private void ButtonExit_Click(object sender, EventArgs e) {
      this.Close();
    }

    private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e) {
      if (e.Action != TreeViewAction.ByKeyboard && e.Action != TreeViewAction.ByMouse) return;
      bool state = e.Node.Checked;

      CheckNodeTree(e.Node, state);
      this.SelectedJobs = GetSelectedJobs();
      if (this.SelectedJobs.Count > 0) { ((Control)this.tabPageJobs).Hide(); }
      else { ((Control)this.tabPageJobs).Show(); }
    }

    private void ButtonJobDescriptions_Click(object sender, EventArgs e) {
      OpenJobDescriptionsDialog();
    }

    private void TabPageJobs_Enter(object sender, EventArgs e) {
      if (this.SelectedJobs == null || this.SelectedJobs.Count == 0) {
        this.tabControl1.SelectedTab = this.tabPage1;
        return;
      }

      this.dataGridViewJobs.Rows.Clear();
      FillGridWithJobs();
    }

    private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
      if (e.ColumnIndex != 2) return;
      DataGridView dgv = sender as DataGridView;
      JobDescriptionsDialog jdd = OpenJobDescriptionsDialog();
      DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
      if (jdd.DialogResult == DialogResult.OK) {
        Settings.JobDescriptions = jdd.JobDescriptions;
        List<string> jbz = Settings.JobDescriptions.Select(x => x).ToList();
        jbz.Add("random");
        cell.Value = jdd.SelectedValue;
        dgv.RefreshEdit();
      }
      dgv.ClearSelection();
    }

    private void ButtonRegisterJobs_Click(object sender, EventArgs e) {
      RegisterJobs();
    }

    private void CheckBoxWhatIf_CheckedChanged(object sender, EventArgs e) {
      if (sender is CheckBox chb) {
        Settings.WhatIfChecked = chb.Checked;
      }
    }

    private void CheckBoxDinner_CheckedChanged(object sender, EventArgs e) {
      if (sender is CheckBox chb) {
        Settings.DinnerChecked = chb.Checked;
      }
    }
  }
}