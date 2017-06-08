using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SherpJobLogger {
  public partial class MainForm : Form {
    #region Fields
    public static SqlConnection SqlConnection;
    public bool SqlConnected;
    private int UserId;
    private static Guid ExecutorGuid;
    private bool ExecutorExists;
    private List<LGTask> Work;
    private List<LGTask> SelectedJobs;
    public static List<string> JobDescriptions;
    public Splash Splash;
    public static double JobLogAccurancy;
    public static int minimumWorkSpanHours;
    public DateTime DinnerStart;
    public DateTime DinnerEnd;
    #endregion Fields

    public string ConnectionString { get; set; }

    public void SetupVars() {
      LoadSettings();
      this.SqlConnected = GetSqlConnection(this.ConnectionString, out SqlConnection);
    }

    private void LoadSettings() {
      if (true) {
        LoadDefaultSettings();
      }
    }
    private void LoadDefaultSettings() {
      this.ConnectionString = @"Server=uran\lg;Database=KB;Trusted_Connection=True;Connection Timeout=3;";
      JobDescriptions = new List<string>() {
          "Настройка серверов",
          "Проверка резервного копирования",
          "Настройка ПО на рабочем месте пользователя",
          "Консультация пользователей",
          "Настройка общесистемного ПО",
          "Настройка рабочих мест",
          "Обслуживание сервера",
          "Профилактические работы",
          "Диагностика сети",
          "Участие в совещании",
          "Лабораторные работы"
      };
      JobLogAccurancy = 0.25;
      minimumWorkSpanHours = 3;
      this.DinnerStart = new DateTime(1983,8,19,13,00,00);
      this.DinnerEnd = this.DinnerStart.AddHours(1);
    }
    private void SaveSettings() {

    }
  }
}
