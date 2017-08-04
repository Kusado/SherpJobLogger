using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace SherpJobLogger {
  public partial class Splash : Form {
    //private MainForm mainForm{ get; set; }

    public delegate void CloseDel();

    private string _status;
    public bool LoadDefaults;
    public Thread MyThread;

    public Splash() {
      InitializeComponent();
      this.label1.Parent = this.pictureBox1;
      this.label1.BackColor = Color.Transparent;
      this.label1.ForeColor = Color.Green;

      this.label2.Parent = this.pictureBox1;
      this.label2.BackColor = Color.Transparent;
      this.label2.ForeColor = Color.Green;

      Version version = Assembly.GetExecutingAssembly().GetName().Version;
      this.label3.Text = version.ToString();
      this.label3.Parent = this.pictureBox1;
      this.label3.BackColor = Color.Transparent;
      this.label3.ForeColor = Color.Green;


      this.Status = string.Empty;
    }

    public string Status {
      get { return this._status; }
      set {
        this._status = value;
        if (this.Created) Invoke((MethodInvoker) delegate { this.label2.Text = this._status; });
      }
    }

    public static Splash ShowSplash(string Status1 = "Загрузка...") {
      Splash s = new Splash();
      s.label1.Text = Status1;
      //s.mainForm = mainForm;
      s.MyThread = new Thread(s._showSplash);
      s.MyThread.Start();

      return s;
    }

    private void _showSplash() {
      BringToFront();
      ShowDialog();
    }

    public void CloseSplash() {
      Invoke((MethodInvoker) delegate { Close(); });
    }

    private void ButtonCancel_Click(object sender, EventArgs e) {
      Close();
    }

    private void Splash_FormClosing(object sender, FormClosingEventArgs e) {
    }
  }
}