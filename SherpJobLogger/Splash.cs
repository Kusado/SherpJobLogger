using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SherpJobLogger {

  public partial class Splash : Form {
    public Thread MyThread;

    public Splash() {
      InitializeComponent();
      this.label1.Parent = this.pictureBox1;
      this.label1.BackColor = Color.Transparent;
    }

    private MainForm main;

    public delegate void CloseDel();

    public static Splash ShowSplash(MainForm main) {
      Splash s = new Splash();
      s.main = main;
      s.MyThread = new Thread(s._showSplash);
      s.MyThread.Start();

      return s;
    }

    private void _showSplash() {
      this.BringToFront();
      this.ShowDialog();
    }

    public void CloseSplash() {
      this.Invoke((MethodInvoker)delegate { this.Close(); });
    }

    private void ButtonCancel_Click(object sender, EventArgs e) {
      this.Close();
    }

    private void Splash_FormClosing(object sender, FormClosingEventArgs e) {
    }
  }
}