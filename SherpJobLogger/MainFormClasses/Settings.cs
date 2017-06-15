using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using OpenDataParser;

namespace SherpJobLogger {

  public partial class MainForm {

    #region Properties

    public static SqlConnection SqlConnection;
    public bool SqlConnected { get; set; }
    private int UserId { get; set; }
    private static Guid ExecutorGuid { get; set; }
    private bool ExecutorExists { get; set; }
    private List<LGTask> Work { get; set; }
    private List<LGTask> SelectedJobs { get; set; }
    public Splash Splash { get; set; }
    public static Settings Settings;
    private OPD Opd;
    public static ProductionCalendar calendar;

    #endregion Properties

    public void SetupVars(ProjectControl pType) {
      Splash = Splash.ShowSplash(this);
      Splash.Status = "Loading settings...";
      Settings = new Settings();
      LoadSettings();
      if (!ApplySettings(out Exception e)) { RichMessageBox.ShowNew(e.Message); return; }
      Settings.pType = pType;
      Splash.Status = "Connecting to SQL...";
      this.SqlConnected = GetSqlConnection(Settings.ConnectionString, out SqlConnection);
      if (!SqlConnected) return;
      Splash.Status = "Get production calendar...";
      this.Opd = new OPD {
        Type = DataType.ProductionCalendar,
        ApiKey = "b4cf9c9bcf820263c676baedee59acd2"
      };
      calendar = this.Opd.GetProductionCalendar();
    }

    public bool ApplySettings(out Exception exception) {
      try {
        this.checkBoxDinner.Checked = Settings.DinnerChecked;
        this.checkBoxWhatIf.Checked = Settings.WhatIfChecked;
        this.dateTimeFromHours.Value = Settings.DateTimeHoursFrom;
        this.dateTimeToHours.Value = Settings.DateTimeHoursTo;
        this.dateTimeDinnerFrom.Value = Settings.DateTimeDinnerFrom;
        this.dateTimeDinnerTo.Value = Settings.DateTimeDinnerTo;
      }
      catch (Exception e) {
        exception = e;
        return false;
      }
      exception = null;
      return true;
    }

    private void LoadSettings() {
      Splash.Status = "Loading settings from registry...";
      Settings = Settings.LoadSettings(out bool loaded);
      if (!loaded) {
        LoadDefaultSettings();
      }
    }

    private void LoadDefaultSettings() {
      Splash.Status = "Loading default settings...";
      if (Settings.pType == ProjectControl.LG) Settings.ConnectionString = @"Server=uran\lg;Database=KB;Trusted_Connection=True;Connection Timeout=3;";
      if (Settings.pType == ProjectControl.RFM) Settings.ConnectionString = @"Server=MARS\RFM;Database=KB;Trusted_Connection=True;Connection Timeout=3;";
      Settings.JobDescriptions = new List<string>() {
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
      Settings.JobLogAccurancy = 0.25;
      Settings.minimumWorkSpanHours = 3;
      Settings.DateTimeDinnerFrom = new DateTime(1983, 8, 19, 13, 00, 00);
      Settings.DateTimeDinnerTo = Settings.DateTimeDinnerFrom.AddHours(1);
      Settings.DateTimeHoursFrom = new DateTime(1983, 8, 19, 09, 00, 00);
      Settings.DateTimeHoursTo = new DateTime(1983, 8, 19, 18, 00, 00);
      Settings.WhatIfChecked = true;
      Settings.DinnerChecked = true;
    }

    private void SaveSettings() {
      Settings.SaveSettings(Settings);
    }
  }

  [Serializable]
  public class Settings {
    private string regKeyPath;
    private string regValueName;
    [NonSerialized] private RegistryKey regKey;
    public ProjectControl pType;
    public string ConnectionString;
    public List<string> JobDescriptions;
    public double JobLogAccurancy;
    public int minimumWorkSpanHours;
    public DateTime DateTimeDinnerFrom;
    public DateTime DateTimeDinnerTo;
    public DateTime DateTimeHoursFrom;
    public DateTime DateTimeHoursTo;
    public bool WhatIfChecked;
    public bool DinnerChecked;

    public Settings() {
      this.regKeyPath = @"SOFTWARE\KusaderSoft\";
      this.regValueName = "string";
      this.regKey = Registry.CurrentUser.OpenSubKey(this.regKeyPath, true);
    }

    public Settings LoadSettings(out bool b) {
      Settings result = new Settings();
      XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(typeof(Settings));
      try {
        string xml = string.Join(
        "",
        (string[])this.regKey.GetValue(this.regValueName)
        );
        XmlTextReader reader = new XmlTextReader(xml.ToStream());
        result = (Settings)formatter.Deserialize(reader);
        b = true;
      }
      catch (Exception) { b = false; }
      return result;
    }

    public bool SaveSettings(Settings s) {
      var stream = new MemoryStream();
      var formatter = new System.Xml.Serialization.XmlSerializer(s.GetType());
      try {
        formatter.Serialize(stream, s);
      }
      catch (Exception e) {
        Console.WriteLine(e);
        throw;
      }
      try {
        string value = Encoding.UTF8.GetString(stream.ToArray());
        var sa = value.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        this.regKey.SetValue(this.regValueName, sa, RegistryValueKind.MultiString);
      }
      catch (Exception e) {
        Console.WriteLine(e);
        throw;
      }
      return true;
    }
  }
}