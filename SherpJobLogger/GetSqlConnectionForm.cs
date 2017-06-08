using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SherpJobLogger {

  public partial class GetSqlConnectionForm : Form {
    public string ConnectionString;

    public GetSqlConnectionForm(string s) {
      InitializeComponent();
      this.ConnectionString = s;
    }

    private void GetSqlConnectionForm_Load(object sender, EventArgs e) {
      this.textBox1.Text = this.ConnectionString;
      this.Focus();
      this.BringToFront();
      this.Activate();
    }

    private void buttonOk_Click(object sender, EventArgs e) {
      this.ConnectionString = this.textBox1.Text;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e) {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void buttonTest_Click(object sender, EventArgs e) {
      var btn = (Button)sender;
      SqlConnection test = new SqlConnection(this.ConnectionString);
      try {
        test.Open();
      }
      catch (SqlException ex) {
        ToolTip tip = new ToolTip();
        tip.SetToolTip(btn, ex.Message);
        btn.ForeColor = Color.DarkRed;
        return;
      }
      test.Close();
      btn.ForeColor = Color.DarkGreen;
    }

    private void textBox1_TextChanged(object sender, EventArgs e) {
      this.ConnectionString = this.textBox1.Text;
    }
  }
}