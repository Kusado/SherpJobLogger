using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SherpJobLogger {

  public partial class MainForm : Form {
    public MainForm() {
      InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e) {
      this.label1.Text = $@"Your ID is: {GetCurentUserID()}";
      this.label2.Text = $@"Your ExecutorID is: {GetExecutorID()}";
      if (this.ExecutorExists) {
        this.Work = GetProjectWorkCU();
        PopulateJobsTree();
      }
      else this.treeViewAllJobs.Nodes.Add("No jobs for you...");
      var now = DateTime.Now;
      if ((this.dateTimeFromDays.Value = GetLastLoggedJobTime()) != now) SetDateTimeNBD(this.dateTimeFromDays);
      this.dateTimeToDays.Value = now;

      Show();
      Focus();
      BringToFront();
      Activate();
    }

    private void MainForm_Shown(object sender, EventArgs e) {
      this.Splash.CloseSplash();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
      SqlConnection?.Close();
      SaveSettings();
    }

    private void ButtonExit_Click(object sender, EventArgs e) {
      Application.Exit();
    }


    private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e) {
      if (e.Action != TreeViewAction.ByKeyboard && e.Action != TreeViewAction.ByMouse) return;
      bool state = e.Node.Checked;

      CheckNodeTree(e.Node, state);
      this.SelectedJobs = GetSelectedJobs();
      if (this.SelectedJobs.Count > 0) { ((Control)this.tabPageJobs).Hide(); }
      else { ((Control)this.tabPageJobs).Show(); }
    }

    private void buttonJobDescriptions_Click(object sender, EventArgs e) {
      OpenJobDescriptionsDialog();
    }

    private void tabPageJobs_Enter(object sender, EventArgs e) {
      if (this.SelectedJobs == null || this.SelectedJobs.Count == 0) {
        this.tabControl1.SelectedTab = this.tabPage1;
        return;
      }

      this.dataGridViewJobs.Rows.Clear();
      FillGridWithJobs();
    }

    private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
      if (e.ColumnIndex != 2) return;
      DataGridView dgv = sender as DataGridView;
      JobDescriptionsDialog jdd = OpenJobDescriptionsDialog();
      DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
      if (jdd.DialogResult == DialogResult.OK) {
        JobDescriptions = jdd.JobDescriptions;
        List<string> jbz = JobDescriptions.Select(x => x).ToList();
        jbz.Add("random");
        cell.Value = jdd.SelectedValue;
        dgv.RefreshEdit();
      }
      dgv.ClearSelection();
    }

    private void buttonRegisterJobs_Click(object sender, EventArgs e) {
      RegisterJobs();
    }
  }

}