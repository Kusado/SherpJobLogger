using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace SherpJobLogger {

  internal static class Program {
    public static Random rnd;
    public static MainForm MainForm;
    private static SqlConnection SqlFenix;
    private static readonly Guid TelemetryGuid = Guid.NewGuid();


    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args) {
      rnd = new Random();
      BeginTelemetry();
      if (!checkLogins()) {
        EndTelemetry();
        throw new AmbiguousMatchException(RandomString());
      }
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      ProjectControl pType;
      if (args.Length > 0 && args[0] == "rfm") pType = ProjectControl.RFM;
      else pType = ProjectControl.LG;

      MainForm = new MainForm();
      MainForm.ShowSplash();
      //Thread.Sleep(1347);
      MainForm.SetupVars(pType, MainForm.Splash.LoadDefaults);
      if (!MainForm.SqlConnected) return;
      Application.Run(MainForm);
      EndTelemetry();
    }

    private static void EndTelemetry() {
      string query = "[dbo].[UpdateTelemetry]";
      SqlCommand cmd = new SqlCommand(query, SqlFenix) { CommandType = CommandType.StoredProcedure };
      SqlParameter EndTime, id;
      (EndTime = new SqlParameter("@EndTime", SqlDbType.DateTime)).Value = DateTime.Now;
      (id = new SqlParameter("@ID", SqlDbType.UniqueIdentifier)).Value = TelemetryGuid;
      cmd.Parameters.AddRange(new SqlParameter[] { EndTime, id });
      cmd.ExecuteNonQuery();
    }



    private static void BeginTelemetry() {
      SqlFenix = new SqlConnection(@"Server=saturn.formulabi.local\it;Database=SJL_telemetry;User Id=SherpLogger;Password = Zasqw12;");
      SqlFenix.Open();

      string query = "[dbo].[AddTelemetry]";
      using (SqlCommand cmd = new SqlCommand(query, SqlFenix) { CommandType = CommandType.StoredProcedure }) {
        SqlParameter appStartDate, userName, compName, id, appVersion;
        (appStartDate = new SqlParameter("@AppStartDate", SqlDbType.DateTime)).Value = DateTime.Now;
        (appVersion = new SqlParameter("@appVersion", SqlDbType.NVarChar)).Value = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        (userName = new SqlParameter("@UserName", SqlDbType.NVarChar)).Value = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        (compName = new SqlParameter("@ComputerName", SqlDbType.NVarChar)).Value = Helpers.GetFQDN();
        (id = new SqlParameter("@ID", SqlDbType.UniqueIdentifier)).Value = TelemetryGuid;

        cmd.Parameters.AddRange(new SqlParameter[] { appStartDate, userName, compName, id, appVersion });
        cmd.ExecuteNonQuery();
      }
    }

    private static bool checkLogins() {
      string userName;
      try {
        userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
      }
      catch (Exception e) {
        throw e;
      }
      SqlCommand command = new SqlCommand(@"SELECT * FROM [bannedLogins]", SqlFenix);
      using (DataTable dt = new DataTable()) {
        dt.Load(command.ExecuteReader());
        if (dt.Rows.Cast<DataRow>().Any(row => userName.Contains(row[0].ToString()))) {
          return false;
        }
      }
      return true;
    }
    private static string RandomString(int length = 64) {
      string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      chars += chars.ToLower();
      return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[rnd.Next(s.Length)]).ToArray());
    }
  }
}