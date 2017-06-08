using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SherpJobLogger {

  public partial class MainForm : Form {
    public string ConnectionString { get; set; }
    public static SqlConnection SqlConnection;
    public bool SqlConnected;
    private int UserId;
    private static Guid ExecutorGuid;
    private bool ExecutorExists;
    private List<LGTask> Work;
    private List<LGTask> SelectedJobs;
    public static List<string> JobDescriptions;
    public Splash Splash;

    public MainForm() {
      InitializeComponent();
    }

    public void SetupVars() {
      this.ConnectionString = @"Server=uran\lg;Database=KB;Trusted_Connection=True;Connection Timeout=3;";
      this.SqlConnected = GetSqlConnection(this.ConnectionString, out SqlConnection);
    }

    private void Form1_Load(object sender, EventArgs e) {
      this.label1.Text = $@"Your ID is: {GetCurentUserID()}";
      this.label2.Text = $@"Your ExecutorID is: {GetExecutorID()}";
      if (this.ExecutorExists) {
        this.Work = GetProjectWorkCU();
        PopulateJobsTree();
      }
      else this.treeViewAllJobs.Nodes.Add("No jobs for you...");
      var now = DateTime.Now;
      if ((this.dateTimeFromDays.Value = GetLastLoggedJobTime()) != now) SetDateTimeNBD(this.dateTimeFromDays);
      this.dateTimeToDays.Value = now;
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

      Show();
      Focus();
      BringToFront();
      Activate();
    }

    private void SetDateTimeNBD(DateTimePicker p) {
      p.Value = p.Value.AddDays(1);
      while (isHoliday(p.Value)) {
        p.Value = p.Value.AddDays(1);
      }
    }

    private void ButtonExit_Click(object sender, EventArgs e) {
      Application.Exit();
    }

    private int GetCurentUserID() {
      string q = @"DECLARE @Uid int SET @Uid = dbo.GetCurrentUser() select @Uid";
      DataTable dt = GetSqlDataTable(q);
      int result = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
      this.UserId = result;
      return this.UserId;
    }

    private Guid GetExecutorID(int id = -1) {
      string q = string.Empty;
      if (id == -1) q = $"SELECT TOP (1000) [IdExecutor] FROM [ProjectControl].[dbo].[vExecutorCurrent]";
      else q = $"SELECT TOP(1000) *FROM[ProjectControl].[dbo].[vExecutor] where IdUser = {id}";

      DataTable dt = GetSqlDataTable(q);
      this.ExecutorExists = Guid.TryParse(dt.Rows[0].ItemArray[0].ToString(), out Guid result);
      ExecutorGuid = this.ExecutorExists ? result : Guid.Empty;
      return ExecutorGuid;
    }

    private DateTime GetLastLoggedJobTime() {
      DateTime result;
      const string query = @"SELECT TOP (1) [OperationDate]
                             FROM [ProjectControl].[dbo].[vOperationShedule_CU]
                             order by OperationDate desc";
      DataTable dt = GetSqlDataTable(query);
      try {
        result = DateTime.Parse(dt.Rows[0].ItemArray[0].ToString());
      }
      catch (Exception) {
        result = DateTime.Now;
      }
      return result;
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
      string q = @"USE ProjectControl SELECT * FROM dbo.vWork_CU_All";
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
      this.treeViewAllJobs.Nodes.Clear();
      foreach (var ContName in lgw.OrderBy(x => x.ContractorName).GroupBy(Cont => Cont.ContractorName)) {
        var rootNode = this.treeViewAllJobs.Nodes.Add(ContName.Select(x => x.IdContractorExt).FirstOrDefault().ToString(), ContName.Key);
        if (string.IsNullOrEmpty(rootNode.Text)) rootNode.Text = rootNode.Name;
        foreach (var proj in ContName.OrderBy(x => x.ProjectName).GroupBy(y => y.ProjectName)) {
          var nodec1 = rootNode.Nodes.Add(proj.Key);
          foreach (var work in proj.OrderBy(x => x.WorkName).Select(x => x.IDWork).Distinct()) {
            nodec1.Nodes.Add(work.ToString(), proj.Where(x => x.IDWork.ToString() == work.ToString()).Select(x => x.WorkName).FirstOrDefault());
          }
        }
        rootNode.Expand();
      }
    }

    private static void CheckNodeTree(TreeNode node, bool state) {
      node.Checked = state;
      foreach (TreeNode childNode in node.Nodes) {
        CheckNodeTree(childNode, state);
      }
    }

    private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e) {
      if (e.Action != TreeViewAction.ByKeyboard && e.Action != TreeViewAction.ByMouse) return;
      bool state = e.Node.Checked;

      CheckNodeTree(e.Node, state);
      this.SelectedJobs = GetSelectedJobs();
      if (this.SelectedJobs.Count > 0) { ((Control)this.tabPageJobs).Hide(); }
      else { ((Control)this.tabPageJobs).Show(); }
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
      return JobDescriptions[Program.rnd.Next(JobDescriptions.Count)];
    }

    private void buttonJobDescriptions_Click(object sender, EventArgs e) {
      OpenJobDescriptionsDialog();
    }

    private JobDescriptionsDialog OpenJobDescriptionsDialog() {
      var jdd = new JobDescriptionsDialog(JobDescriptions);
      jdd.ShowInTaskbar = false;
      jdd.Activate();
      jdd.ShowDialog();
      if (jdd.DialogResult == DialogResult.OK) {
        JobDescriptions = jdd.JobDescriptions;
      }
      return jdd;
    }

    private void tabPageJobs_Enter(object sender, EventArgs e) {
      if (this.SelectedJobs == null || this.SelectedJobs.Count == 0) {
        this.tabControl1.SelectedTab = this.tabPage1;
        return;
      }

      this.dataGridView1.Rows.Clear();
      FillGridWithJobs();
    }

    private void FillGridWithJobs() {
      List<string> jbz = JobDescriptions.Select(x => x).ToList();
      jbz.Add("random");
      DataGridView dgv = this.dataGridView1;
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

    private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
      if (e.ColumnIndex != 2) return;
      DataGridView dgv = sender as DataGridView;
      JobDescriptionsDialog jdd = OpenJobDescriptionsDialog();
      DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
      if (jdd.DialogResult == DialogResult.OK) {
        JobDescriptions = jdd.JobDescriptions;
        List<string> jbz = JobDescriptions.Select(x => x).ToList();
        jbz.Add("random");
        cell.Value = jdd.SelectedValue;
        dgv.RefreshEdit();
      }
      dgv.ClearSelection();
    }

    private int GetJobRatesSum() {
      var dgv = this.dataGridView1;
      int JobRateSum = 0;
      foreach (DataGridViewRow r in dgv.Rows) {
        DataGridViewCell cell = r.Cells[3];
        if (cell.FormattedValue != null) {
          JobRateSum += int.Parse(cell.FormattedValue.ToString());
        }
      }
      return JobRateSum;
    }

    private void button1_Click(object sender, EventArgs e) {
      RegisterJobs();
    }

    private void RegisterJobs() {
      List<JobLog> jobs = GetJobRates();
      var workSpans = GetWorkSpans().OrderByDescending(x => x.Length);
      foreach (WorkSpan span in workSpans) {
        span.registerJob(jobs, this.checkBoxWhatIf.Checked);
      }

      //var jobLogs = new List<JobLog>();
      //var filteredJobs = jobs.Where(x => x.Hours > 3).ToList();
      //int i = 0;
      //while (filteredJobs.Count > 0) {
      //  WorkSpan WorkSpan = workSpans[i];
      //  JobLog jobLog = GetRandomJobLog(filteredJobs);
      //  jobLog.Hours -= WorkSpan.Length;
      //  if (jobLog.Description == "random") jobLog.Description = this.JobDescriptions[Program.rnd.Next(0, this.JobDescriptions.Count)];
      //  jobLog.BeginDateTime = WorkSpan.from;
      //  jobLog.EndDateTime = WorkSpan.to;
      //
      //  jobLogs.Add(jobLog);

      //  filteredJobs = jobs.Where(x => x.Hours > 3).ToList();
      //  i++;
      //}

      //foreach (JobLog jobLog in jobLogs) {
      //  if (!RegisterJob(jobLog)) this.Close(); return;
      //}

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
    public static JobLog GetRandomJobLog(List<JobLog> JobLogs) { return GetRandomJobLog(JobLogs, Program.rnd); }
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
      foreach (DataGridViewRow row in this.dataGridView1.Rows) {
        int jobRate = int.Parse(row.Cells[3].FormattedValue.ToString());
        string descr = row.Cells[2].FormattedValue.ToString();
        Guid IDwork = Guid.Parse(row.Cells[1].Value.ToString());
        LGTask w = this.SelectedJobs.FirstOrDefault(x => x.IDWork == IDwork);
        JobLog jobLog = new JobLog(w, Helpers.RoundToFraction(jobRate / JobRateTotal * totalHours, 1), descr);

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
      return GetWorkDates(from, to);
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
      if (isHoliday(d)) return false;
      return true;
    }

    /// <summary>
    /// Проверяем, не является ли день праздничным
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    private static bool isHoliday(DateTime d) {
      //ToDo: Реализовать посик праздничных дней.
      return false;
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
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
      SqlConnection?.Close();
    }
    private void MainForm_Shown(object sender, EventArgs e) {
      this.Splash.CloseSplash();
    }
  }
  /// <summary>
  /// Описание схемы БД с работами
  /// </summary>
  public class LGTask {
    public Guid IDWork { get; set; }
    public string WorkNumber { get; set; }
    public Guid IDProject { get; set; }
    public string ProjectName { get; set; }
    public string ProjectShortName { get; set; }
    public string ProjectNumber { get; set; }
    public string WorkText { get; set; }
    public string WorkName { get; set; }
    public Guid IDExecutor { get; set; }
    public string ContractorName { get; set; }
    public Guid IdContractorExt { get; set; }

    public static LGTask FromDataTable(DataRow dr) {
      LGTask result = new LGTask {
        IDWork = Guid.Parse(dr["IDWork"].ToString()),
        WorkNumber = dr["WorkNumber"].ToString(),
        IDProject = Guid.Parse(dr["IDProject"].ToString()),
        ProjectName = dr["ProjectName"].ToString(),
        ProjectShortName = dr["ProjectShortName"].ToString(),
        ProjectNumber = dr["ProjectNumber"].ToString(),
        WorkText = dr["WorkText"].ToString(),
        WorkName = dr["WorkName"].ToString(),
        IDExecutor = Guid.Parse(dr["IDExecutor"].ToString()),
        ContractorName = dr["ContractorName"].ToString()
      };
      if (!String.IsNullOrEmpty(dr["IdContractorExt"].ToString())) { result.IdContractorExt = Guid.Parse(dr["IdContractorExt"].ToString()); }
      return result;
    }
  }
  /// <summary>
  /// Выполненная работа с описанием проделанного, временными отметками
  /// </summary>
  public class JobLog {
    public LGTask Task { get; }
    /// <summary>
    /// Общее число часов, затраченное на конкретную задачу
    /// </summary>
    public double Hours { get; set; }
    /// <summary>
    /// Проделанная исполнителем работа
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    /// Дата и время начала конкретного отрезка работы
    /// </summary>
    public DateTime BeginDateTime { get; set; }
    /// <summary>
    ///Дата и время окончания конкретного отрезка работы
    /// </summary>
    public DateTime EndDateTime { get; set; }

    public JobLog(LGTask task, double hours, string JobDescription) {
      this.Task = task;
      this.Hours = hours;
      this.Description = JobDescription;
    }

    public JobLog(JobLog j) {
      this.Task = j.Task;
      this.Hours = j.Hours;
      this.Description = j.Description;
      this.BeginDateTime = j.BeginDateTime;
      this.EndDateTime = j.EndDateTime;
    }
    public JobLog(JobLog j, string description) {
      this.Task = j.Task;
      this.Hours = j.Hours;
      this.Description = description;
      this.BeginDateTime = j.BeginDateTime;
      this.EndDateTime = j.EndDateTime;
    }
    public double LengthCurrentJob() {
      return (this.EndDateTime - this.BeginDateTime).TotalHours;
    }
  }

  /// <summary>
  /// Временные метки выполненной работы
  /// </summary>
  public class WorkSpan {
    public WorkSpan(DateTime f, DateTime t) {
      this.from = f;
      this.to = t;
    }
    /// <summary>
    /// Начало рабочего отрезка
    /// </summary>
    public DateTime from { get; }
    /// <summary>
    /// Окончание рабочего отрезка
    /// </summary>
    public DateTime to { get; }
    /// <summary>
    /// Продолжительность рабочего отрезка
    /// </summary>
    public double Length {
      get {
        return this.to.Hour - this.from.Hour;
      }
    }
    /// <summary>
    /// Зарегистрировать случайную работу из списка
    /// в рамках рабочего отрезка
    /// </summary>
    /// <param name="Jobs"></param>
    /// <returns></returns>
    public bool registerJob(List<JobLog> Jobs, bool Debug) {

      var filteredJobs = Jobs.Where(x => x.Hours > this.Length).ToList();
      if (filteredJobs.Count <= 0) return false;

      JobLog jobLog = MainForm.GetRandomJobLog(filteredJobs);
      jobLog.Hours -= this.Length;

      string Description = (jobLog.Description == "random") ? MainForm.GetRandomJobDescription() : jobLog.Description;
      JobLog loggedJob = new JobLog(jobLog, Description) {
        BeginDateTime = this.@from,
        EndDateTime = this.to
      };
      if (Debug) {
        List<string> DebMessage = new List<string>();
        DebMessage.Add($"Дата:{loggedJob.BeginDateTime.ToShortDateString()}:");
        DebMessage.Add($"С {loggedJob.BeginDateTime.ToShortTimeString()}, по {loggedJob.EndDateTime.ToShortTimeString()} - {loggedJob.LengthCurrentJob()}  часов");
        DebMessage.Add($"Наименование контрагента: {loggedJob.Task.ContractorName}");
        DebMessage.Add($"WorkName: {loggedJob.Task.WorkName}");
        DebMessage.Add($"Что сделано: {loggedJob.Description}");
        RichMessageBox.ShowNew(DebMessage);
        //return false;
      }
      return MainForm.RegisterJob(loggedJob, Debug);
    }
    public static List<WorkSpan> GetWorkSpanList(DateTime from, DateTime to, Random rnd) {
      int minimumWorkSpanHours = 3;
      var result = new List<WorkSpan>();
      TimeSpan span = to - from;
      double hours = span.TotalHours;
      if (span.TotalHours > minimumWorkSpanHours) {
        int mult;
        double tempDouble = Helpers.GetRandomDouble(hours, rnd, out mult, minimumWorkSpanHours, 0.25);
        int i = rnd.Next(0, 2);
        WorkSpan res;
        if (i == 1) {
          DateTime newFrom = from.AddHours(tempDouble);
          res = new WorkSpan(from, newFrom);
          from = newFrom;
        }
        else {
          DateTime newTo = to.AddHours(-tempDouble);
          res = new WorkSpan(newTo, to);
          to = newTo;
        }
        result.Add(res);
        if (from != to) result.AddRange(GetWorkSpanList(from, to, rnd));

      }
      else {
        result.Add(new WorkSpan(from, to));
      }
      return result;
    }
  }
}