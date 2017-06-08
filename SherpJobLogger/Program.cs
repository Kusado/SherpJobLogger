using System;
using System.Windows.Forms;

namespace SherpJobLogger {

  internal static class Program {
    public static Random rnd;
    public static MainForm MainForm;

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      rnd = new Random();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      //Application.Run();

      MainForm = new MainForm();
      MainForm.Splash = Splash.ShowSplash(MainForm);
      MainForm.SetupVars();
      if (!MainForm.SqlConnected) return;
      Application.Run(MainForm);
    }
  }
}