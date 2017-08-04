using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SherpJobLogger {
  public partial class JobDescriptionsDialog : Form {
    public List<string> JobDescriptions;
    public string SelectedValue;

    public JobDescriptionsDialog(List<string> jobs) {
      InitializeComponent();
      this.JobDescriptions = jobs;
    }

    private void buttonCancel_Click(object sender, EventArgs e) {
      this.DialogResult = DialogResult.Cancel;
      Close();
    }

    private void buttonOk_Click(object sender, EventArgs e) {
      SaveAndExit();
    }

    private void JobDescriptionsDialog_Load(object sender, EventArgs e) {
      ListToRows();
    }

    private void ListToRows() {
      this.dataGridViewJobsDescriptions.Rows.Clear();

      foreach (string jd in this.JobDescriptions) this.dataGridViewJobsDescriptions.Rows.Add(jd);
    }

    private List<string> RowsToList(DataGridView dg) {
      var result = new List<string>();
      foreach (DataGridViewRow row in dg.Rows) if (!row.IsNewRow) result.Add(Convert.ToString(row.Cells[0].Value));
      return result;
    }

    private void button1_Click(object sender, EventArgs e) {
      Save();
    }

    private void Save() {
      DataGridView dg = this.dataGridViewJobsDescriptions;
      dg.EndEdit();
      foreach (DataGridViewRow row in dg.Rows)
        if (row.Cells[0] is DataGridViewTextBoxCell cell)
          if (string.IsNullOrEmpty(cell.Value?.ToString())) if (!row.IsNewRow) dg.Rows.Remove(row);
      string tempString = dg.SelectedCells[0].FormattedValue.ToString();
      this.SelectedValue = string.IsNullOrEmpty(tempString) ? "random" : tempString;
      this.JobDescriptions = RowsToList(dg);
    }

    private void SaveAndExit() {
      Save();
      this.DialogResult = DialogResult.OK;
      Close();
    }

    private void buttonRevert_Click(object sender, EventArgs e) {
      this.dataGridViewJobsDescriptions.CancelEdit();
      ListToRows();
    }

    private void dataGridViewJobsDescriptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
      SaveAndExit();
    }

    private void dataGridViewJobsDescriptions_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
      if (sender is DataGridView dg) {
        DataGridViewTextBoxCell cell = dg[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
        if (cell.KeyEntersEditMode(new KeyEventArgs(Keys.Delete)))
          foreach (DataGridViewRow row in dg.SelectedRows) dg.Rows.Remove(row);
      }
    }
  }
}