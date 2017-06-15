﻿using System;
using System.Windows.Forms;

namespace SherpJobLogger {

  internal static class Program {
    public static Random rnd;
    public static MainForm MainForm;

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args) {
      rnd = new Random();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      ProjectControl pType;
      if (args.Length > 0 && args[0] == "rfm") pType = ProjectControl.RFM;
      else pType = ProjectControl.LG;

      MainForm = new MainForm();
      MainForm.SetupVars(pType);
      if (!MainForm.SqlConnected) return;
      Application.Run(MainForm);
    }
  }
}