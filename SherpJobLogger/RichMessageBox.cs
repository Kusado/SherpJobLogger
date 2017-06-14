using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SherpJobLogger {

  public partial class RichMessageBox : Form {
    public List<string> lines;
    public string caption;

    public RichMessageBox() {
      InitializeComponent();
    }

    public static void ShowNew(List<string> sList, string caption = "Message", string description = "") {
      RichMessageBox form = new RichMessageBox {
        richTextBox1 = { Lines = sList.ToArray() },
        label1 = { Text = description },
        Text = caption,
        StartPosition = FormStartPosition.CenterParent
      };
      form.ShowDialog();
      form.Activate();
    }

    public static void ShowNew(string s, string caption = "Message", string description = "") {
      RichMessageBox form = new RichMessageBox {
        label1 = { Text = description },
        Text = caption,
        StartPosition = FormStartPosition.CenterParent
      };
      form.richTextBox1.AppendText(s);
      form.ShowDialog();
      form.Activate();
    }

    private void button1_Click(object sender, EventArgs e) {
      this.Close();
    }
  }
}