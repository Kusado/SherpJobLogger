using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using OpenDataParser;

namespace SherpJobLogger {

  public partial class MainForm : Form {



    private int GetCurentUserID() {
      string q = @"DECLARE @Uid int SET @Uid = dbo.GetCurrentUser() select @Uid";
      DataTable dt = GetSqlDataTable(q);
      int result = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
      this.UserId = result;
      return this.UserId;
    }

    private Guid GetExecutorID(int id = -1) {
      string q = string.Empty;
      if (id == -1) q = $"SELECT [IdExecutor],[Login] FROM [ProjectControl].[dbo].[vExecutorCurrent]";
      else q = $"SELECT TOP(1000) *FROM[ProjectControl].[dbo].[vExecutor] where IdUser = {id}";

      DataTable dt = GetSqlDataTable(q);
      this.ExecutorExists = Guid.TryParse(dt.Rows[0].ItemArray[0].ToString(), out Guid result);
      try {
        this.Text = dt.Rows[0].ItemArray[1].ToString();
      }
      catch {
        // ignored
      }

      ExecutorGuid = this.ExecutorExists ? result : Guid.Empty;
      return ExecutorGuid;
    }

    private DateTime GetLastLoggedJobTime() {
      DateTime result;
      string query = String.Empty;
      switch (Settings.pType) {
        case ProjectControl.RFM:
          query = $@"SELECT TOP (1)[OperationDate]
        FROM [ProjectControl].[dbo].[vOperation]
        Where IdExecutor = '{ExecutorGuid}'
        Order by OperationEnd desc";
          break;

        case ProjectControl.LG:
          query = @"SELECT TOP (1) [OperationDate]
                             FROM [ProjectControl].[dbo].[vOperationShedule_CU]
                             order by OperationDate desc";

          //вьюха пиздец тормозная, выполняется секунд 5.

          //query = @"SELECT 1";
          break;
      }

      try {
        DataTable dt = GetSqlDataTable(query);
        result = DateTime.Parse(dt.Rows[0].ItemArray[0].ToString());
      }
      catch (Exception) {
        result = DateTime.Now;
      }
      return result;
    }

    private bool IsRFM() {
      if (Settings.pType == ProjectControl.RFM) return true; else return false;
    }

    private DataTable GetSqlDataTable(string q) {
      DataTable dt = new DataTable();

      SqlCommand command = new SqlCommand(q, SqlConnection);

      SqlDataReader r = command.ExecuteReader();
      if (dt != null) dt.Load(r);

      return dt;
    }

    private bool GetSqlConnection(string s, out SqlConnection con) {
      bool result = false;
      con = new SqlConnection(s);
      try {
        con.Open();
        result = true;
      }
      catch (SqlException) {
        var f = new GetSqlConnectionForm(s);
        f.ShowDialog();
        f.TopMost = true;
        if (f.DialogResult == DialogResult.OK) { return GetSqlConnection(f.ConnectionString, out con); }
      }
      return result;
    }

    private List<LGTask> GetProjectWorkCU() {
      List<LGTask> result = new List<LGTask>();
      string q = string.Empty;
      switch (Settings.pType) {
        case ProjectControl.LG:
          q = @"USE ProjectControl SELECT * FROM dbo.vWork_CU_All";
          break;

        case ProjectControl.RFM:
          q = $@"SELECT * FROM [ProjectControl].[dbo].[vWork]
              WHERE IdExecutor = '{ExecutorGuid}'";
          break;

        default:
          throw new ArgumentOutOfRangeException();
      }

      DataTable dt = GetSqlDataTable(q);
      foreach (DataRow r in dt.Rows) {
        result.Add(LGTask.FromDataTable(r));
      }
      return result;
    }

    private void PopulateJobsTree() {
      PopulateJobsTree(this.Work);
    }

    private void PopulateJobsTree(List<LGTask> lgw) {
#if DEBUG
      var time = DateTime.Now;
#endif
      this.treeViewAllJobs.Nodes.Clear();
      //foreach (var ContName in lgw.GroupBy(Cont => Cont.ContractorName).OrderBy(x => x)) {
      foreach (var ContName in lgw.OrderBy(x => x.ContractorName).GroupBy(Cont => Cont.ContractorName)) {
        TreeNode rootNode = this.treeViewAllJobs.Nodes.Add(ContName.First().IdContractorExt.ToString(), ContName.Key);
        //var rootNode = this.treeViewAllJobs.Nodes.Add(ContName.Select(x => x.IdContractorExt).FirstOrDefault().ToString(), ContName.Key);
        if (string.IsNullOrEmpty(rootNode.Text)) rootNode.Text = rootNode.Name;
        foreach (var proj in ContName.GroupBy(y => y.ProjectName)) {
          //foreach (var proj in ContName.OrderBy(x => x.ProjectName).GroupBy(y => y.ProjectName)) {
          TreeNode nodec1 = rootNode.Nodes.Add(proj.Key);
          foreach (var work in proj.Select(x => x.IDWork).Distinct()) {
            //foreach (var work in proj.OrderBy(x => x.WorkName).Select(x => x.IDWork).Distinct()) {
            nodec1.Nodes.Add(work.ToString(), proj.Where(x => x.IDWork.ToString() == work.ToString()).Select(x => x.WorkName).FirstOrDefault());
          }
        }
        rootNode.Expand();
      }
#if DEBUG
      Debug.WriteLine($"End adding tree nodes {(DateTime.Now - time).TotalSeconds}");
#endif
    }

    private static void CheckNodeTree(TreeNode node, bool state) {
      node.Checked = state;
      foreach (TreeNode childNode in node.Nodes) {
        CheckNodeTree(childNode, state);
      }
    }

    private List<Guid> GetSelectedNodes() {
      return GetSelectedNodes(this.treeViewAllJobs);
    }

    private List<Guid> GetSelectedNodes(TreeView tv) {
      List<Guid> result = new List<Guid>();
      foreach (TreeNode tvNode in tv.Nodes) {
        if (tv.Nodes.Count == 0) break;
        result.AddRange(GetSelectedNodes(tvNode));
      }
      return result;
    }

    private List<Guid> GetSelectedNodes(TreeNode tn) {
      List<Guid> result = new List<Guid>();
      if (tn.Nodes.Count == 0 && tn.Checked) {
        result.Add(Guid.Parse(tn.Name));
      }
      foreach (TreeNode node in tn.Nodes) {
        result.AddRange(GetSelectedNodes(node));
      }
      return result;
    }

    private List<LGTask> GetSelectedJobs() {
      var nodes = GetSelectedNodes();
      var result = new List<LGTask>();
      foreach (Guid node in nodes) {
        result.AddRange(this.Work.Where(x => x.IDWork == node));
      }
      return result;
    }

    public static string GetRandomJobDescription() {
      return Settings.JobDescriptions[Program.rnd.Next(Settings.JobDescriptions.Count)];
    }

    private JobDescriptionsDialog OpenJobDescriptionsDialog() {
      var jdd = new JobDescriptionsDialog(Settings.JobDescriptions);
      jdd.ShowInTaskbar = false;
      jdd.Activate();
      jdd.ShowDialog();
      if (jdd.DialogResult == DialogResult.OK) {
        Settings.JobDescriptions = jdd.JobDescriptions;
      }
      return jdd;
    }

    private void FillGridWithJobs() {
      List<string> jbz = Settings.JobDescriptions.Select(x => x).ToList();
      jbz.Add("random");
      DataGridView dgv = this.dataGridViewJobs;
      DataGridViewCellStyle temp = dgv.Columns[2].DefaultCellStyle;
      temp.NullValue = "random";
      dgv.DefaultCellStyle = temp;
      foreach (LGTask job in this.SelectedJobs) {
        string jobName = this.checkBoxJobName.Checked ? $"{job.ProjectShortName}: {job.WorkName}" : $"{job.WorkName}";
        int r = dgv.Rows.Add(jobName, job.IDWork);
        //DataGridViewCell cell = dgv.Rows[r].Cells[2];
        //var c = (DataGridViewTextBoxCell)cell;
        //c.DataSource = jbz;
      }
    }

    private int GetJobRatesSum() {
      var dgv = this.dataGridViewJobs;
      int JobRateSum = 0;
      foreach (DataGridViewRow r in dgv.Rows) {
        DataGridViewCell cell = r.Cells[3];
        if (cell.FormattedValue != null) {
          JobRateSum += int.Parse(cell.FormattedValue.ToString());
        }
      }
      return JobRateSum;
    }

    private void RegisterJobs() {
      List<JobLog> jobs = GetJobRates();
      List<SherpJobLogger.WorkSpan> AllSpans = GetWorkSpans();
      List<WorkSpan> workSpans = AllSpans.OrderByDescending(x => x.Length).ToList();
      foreach (WorkSpan span in workSpans) {
        span.RegisterJob(jobs, this.checkBoxWhatIf.Checked);
        AllSpans.Remove(span);
      }

      /*Возможна ситуация, когда не удастся все работы кратно распихать по рабочим отрезкам.
      В таком случае покажем пользователю окошко со списком работ, которые не влезли*/
      var remainingJobs = jobs.Where(x => x.Hours > 0);
      if (remainingJobs.Any()) {
        List<string> remainingJobsReport = new List<string>();
        remainingJobsReport.Add($"Не удалось автоматически распихать: ");
        foreach (JobLog job in remainingJobs) {
          remainingJobsReport.Add($"{Environment.NewLine}{job.Task.WorkName}, число часов:{job.Hours}");
        }
        RichMessageBox.ShowNew(remainingJobsReport);
      }
    }

    public static JobLog GetRandomJobLog(List<JobLog> JobLogs) {
      return GetRandomJobLog(JobLogs, Program.rnd);
    }

    public static JobLog GetRandomJobLog(List<JobLog> JobLogs, Random rnd) {
      return JobLogs[rnd.Next(JobLogs.Count)];
    }

    /// <summary>
    /// Регистрируем выполненную работу на серевере
    /// </summary>
    /// <param name="jobLog">JobLog работы для регистрации на сервере</param>
    /// <returns>Если не было исключений, вернём true</returns>
    public static bool RegisterJob(JobLog jobLog, bool Debug) {
      string query = $@"DECLARE @IdOperation uniqueidentifier
             DECLARE @return_value int
             EXEC   @return_value = [dbo].[Operation_u]
             @Action = 1,
             @IdOperation = @IdOperation OUTPUT,
             @IdWork = '{jobLog.Task.IDWork}',
             @IdExecutor = '{ExecutorGuid}',
             @OperationText = '{jobLog.Description}',
             @RequestID = NULL,
             @OperationDate = '{jobLog.BeginDateTime.Month}-{jobLog.BeginDateTime.Day}-{jobLog.BeginDateTime.Year}',
             @OperationBeginTime ='{jobLog.BeginDateTime.ToShortTimeString()}',
             @OperationEndTime = '{jobLog.EndDateTime.ToShortTimeString()}',
             @OperationBegin = NULL,
             @OperationEnd = NULL";
      SqlCommand cmd = new SqlCommand();
      try {
        cmd = new SqlCommand(query, SqlConnection);
      }
      catch (Exception exception) {
        DialogResult answer = MessageBox.Show(exception.Message, exception.Source, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        switch (answer) {
          case DialogResult.Ignore:
            break;

          case DialogResult.Abort:
            throw;
          case DialogResult.Retry:
            RegisterJob(jobLog, Debug);
            break;
        }
      }
      try {
        if (Debug) {
          RichMessageBox.ShowNew(cmd.CommandText);
        }
        else { cmd.ExecuteNonQuery(); }
      }
      catch (SqlException exception) {
        DialogResult answer = MessageBox.Show(exception.Message, exception.Source, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        switch (answer) {
          case DialogResult.Ignore:
            return false;

          case DialogResult.Abort:
            Program.MainForm.Close();
            return false;

          case DialogResult.Retry:
            RegisterJob(jobLog, Debug);
            break;
        }
      }
      return true;
    }

    /// <summary>
    /// Вычисление количественного коэффициента для каждой работы в таблице
    /// </summary>
    /// <returns></returns>
    private List<JobLog> GetJobRates() {
      var result = new List<JobLog>();
      int totalHours = GetDailyWorkHours() * GetWorkDates().Count;
      double JobRateTotal = (double)GetJobRatesSum();
      foreach (DataGridViewRow row in this.dataGridViewJobs.Rows) {
        int jobRate = int.Parse(row.Cells[3].FormattedValue.ToString());
        string descr = row.Cells[2].FormattedValue.ToString();
        Guid IDwork = Guid.Parse(row.Cells[1].Value.ToString());
        LGTask w = this.SelectedJobs.FirstOrDefault(x => x.IDWork == IDwork);
        JobLog jobLog = new JobLog(w, Helpers.RoundToFraction(jobRate / JobRateTotal * totalHours, Settings.JobLogAccurancy), descr);

        result.Add(jobLog);
      }
      return result;
    }

    /// <summary>
    /// Возвращаем из диапозона дат те, среди которых нет выходных или праздничных дней.
    /// </summary>
    /// <returns></returns>
    private List<DateTime> GetWorkDates() {
      var from = this.dateTimeFromDays.Value;
      var to = this.dateTimeToDays.Value;
      return GetWorkDates(@from, to);
    }

    /// <summary>
    /// Возвращаем из диапозона дат те, среди которых нет выходных или праздничных дней.
    /// </summary>
    /// <param name="dateFrom">Нижний край диапозона дат</param>
    /// <param name="dateTo">Верхний край диапозона дат</param>
    /// <returns></returns>
    private List<DateTime> GetWorkDates(DateTime dateFrom, DateTime dateTo) {
      List<DateTime> workDates = new List<DateTime>();
      int span;

      if (dateFrom.Year < dateTo.Year) {
        int daysInYear = new DateTime(dateFrom.Year, 12, 31).DayOfYear;
        span = daysInYear - dateFrom.DayOfYear + dateTo.DayOfYear;
      }
      else span = dateTo.DayOfYear - dateFrom.DayOfYear;

      for (int j = 0; j <= span; j++) {
        var testDate = dateFrom.AddDays(j);
        if (IsWorkDay(testDate)) workDates.Add(testDate);
      }
      return workDates;
    }

    /// <summary>
    /// Кол-во рабочих часов в день
    /// </summary>
    /// <returns></returns>
    private int GetDailyWorkHours() {
      return GetDailyWorkHours(this.dateTimeFromHours.Value, this.dateTimeToHours.Value);
    }

    /// <summary>
    /// Кол-во рабочих часов в день
    /// </summary>
    /// <returns></returns>
    private int GetDailyWorkHours(DateTime hoursFrom, DateTime hoursTo) {
      return GetDailyWorkHours(hoursFrom, hoursTo, this.dateTimeDinnerFrom.Value, this.dateTimeDinnerTo.Value);
    }

    /// <summary>
    /// Кол-во рабочих часов в день
    /// </summary>
    /// <returns></returns>
    private int GetDailyWorkHours(DateTime hoursFrom, DateTime hoursTo, DateTime dinnerFrom, DateTime dinnerTo) {
      int workMinutes = (hoursTo.Hour - hoursFrom.Hour);
      int dinerMinutes = (dinnerTo.Hour - dinnerFrom.Hour);
      return this.checkBoxDinner.Checked ? (workMinutes - dinerMinutes) : workMinutes;
    }

    /// <summary>
    /// Проверяем, является ли день рабочим
    /// </summary>
    /// <param name="d"></param>
    /// <returns>true - рабочий день, false - не рабочий день</returns>
    private static bool IsWorkDay(DateTime d) {
      if (d.DayOfWeek == DayOfWeek.Saturday) return false;
      if (d.DayOfWeek == DayOfWeek.Sunday) return false;
      if (IsHoliday(d)) return false;
      return true;
    }

    /// <summary>
    /// Проверяем, не является ли день праздничным
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    private static bool IsHoliday(DateTime d) {
      //ToDo: Реализовать посик праздничных дней.
      return calendar.IsHoliday(d);
    }

    /// <summary>
    /// Дробим дни на рабочие отрезки и возвращаем их листом
    /// </summary>
    /// <returns></returns>
    private List<WorkSpan> GetWorkSpans() {
      var result = new List<WorkSpan>();

      foreach (DateTime Day in GetWorkDates()) {
        DateTime Morningfrom = new DateTime(Day.Year, Day.Month, Day.Day, this.dateTimeFromHours.Value.Hour, 0, 0);
        DateTime Morningto = new DateTime(Day.Year, Day.Month, Day.Day, this.dateTimeDinnerFrom.Value.Hour, 0, 0);
        DateTime Dayfrom = new DateTime(Day.Year, Day.Month, Day.Day, this.dateTimeDinnerTo.Value.Hour, 0, 0);
        DateTime Dayto = new DateTime(Day.Year, Day.Month, Day.Day, this.dateTimeToHours.Value.Hour, 0, 0);

        if (this.checkBoxDinner.Checked) {
          result.AddRange(WorkSpan.GetWorkSpanList(Morningfrom, Morningto, Program.rnd));
          result.AddRange(WorkSpan.GetWorkSpanList(Dayfrom, Dayto, Program.rnd));
        }
        else {
          result.AddRange(WorkSpan.GetWorkSpanList(Morningfrom, Dayto, Program.rnd));
        }
      }
      return result;
    }

    private void SetDateTimeNBD(DateTimePicker p) {
      p.Value = p.Value.AddDays(1);
      while (!IsWorkDay(p.Value)) {
        p.Value = p.Value.AddDays(1);
      }
    }
  }
}