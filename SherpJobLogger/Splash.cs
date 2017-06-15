﻿using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SherpJobLogger {

  public partial class Splash : Form {
    public Thread MyThread;
    private string _status;

    public string Status {
      get { return this._status; }
      set {
        this._status = value;
        if (this.Created) this.Invoke((MethodInvoker)delegate { this.label2.Text = this._status; });
      }
    }

    public Splash() {
      InitializeComponent();
      this.label1.Parent = this.pictureBox1;
      this.label1.BackColor = Color.Transparent;
      this.label2.Parent = this.pictureBox1;
      this.label2.BackColor = Color.Transparent;
      Status = String.Empty;
    }

    private MainForm mainForm;

    public delegate void CloseDel();

    public static Splash ShowSplash(MainForm mainForm) {
      Splash s = new Splash();
      s.mainForm = mainForm;
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