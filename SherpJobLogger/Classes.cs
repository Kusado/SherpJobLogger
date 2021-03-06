﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SherpJobLogger {
  /// <summary>
  ///   Описание схемы БД с работами
  /// </summary>
  public class LGTask {
    public static LGTask FromDataTable(DataRow dr) {
      LGTask result = new LGTask {
        IDWork = Guid.Parse(dr["IDWork"].ToString()),
        WorkNumber = dr["WorkNumber"].ToString(),
        IDProject = Guid.Parse(dr["IDProject"].ToString()),
        ProjectName = dr["ProjectName"].ToString(),
        ProjectNumber = dr["ProjectNumber"].ToString(),
        WorkText = dr["WorkText"].ToString(),
        WorkName = dr["WorkName"].ToString(),
        IDExecutor = Guid.Parse(dr["IDExecutor"].ToString())
      };
      if (MainForm.Settings.pType == ProjectControl.RFM) {
        result.ProjectShortName = result.ProjectNumber;
        result.ContractorName = dr["WorkProducerFullName"].ToString();
        if (int.TryParse(dr["CompletePercentDeveloper"].ToString(), out int tepmInt))
          result.CompletePercentDeveloper = tepmInt;
      }
      else if (MainForm.Settings.pType == ProjectControl.LG) {
        result.ContractorName = dr["ContractorName"].ToString();
        result.ProjectShortName = dr["ProjectShortName"].ToString();
      }
      if (MainForm.Settings.pType == ProjectControl.LG && !string.IsNullOrEmpty(dr["IdContractorExt"].ToString()))
        result.IdContractorExt = Guid.Parse(dr["IdContractorExt"].ToString());
      return result;
    }

    #region Properties

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
    public int CompletePercentDeveloper { get; set; }

    #endregion Properties
  }

  /// <summary>
  ///   Выполненная работа с описанием проделанного, временными отметками
  /// </summary>
  public class JobLog {
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

    public JobLog(LGTask task, double hours, string JobDescription) {
      this.Task = task;
      this.Hours = hours;
      this.Description = JobDescription;
    }

    public double LengthCurrentJob() {
      return (this.EndDateTime - this.BeginDateTime).TotalHours;
    }

    #region Properties

    public LGTask Task { get; }

    /// <summary>
    ///   Общее число часов, затраченное на конкретную задачу
    /// </summary>
    public double Hours { get; set; }

    /// <summary>
    ///   Проделанная исполнителем работа
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///   Дата и время начала конкретного отрезка работы
    /// </summary>
    public DateTime BeginDateTime { get; set; }

    /// <summary>
    ///   Дата и время окончания конкретного отрезка работы
    /// </summary>
    public DateTime EndDateTime { get; set; }

    #endregion Properties
  }

  /// <summary>
  ///   Временные метки выполненной работы
  /// </summary>
  public class WorkSpan {
    public WorkSpan(DateTime f, DateTime t) {
      this.From = f;
      this.To = t;
    }

    /// <summary>
    ///   Зарегистрировать случайную работу из списка
    ///   в рамках рабочего отрезка
    /// </summary>
    /// <param name="Jobs"></param>
    /// <param name="Debug"></param>
    /// <returns></returns>
    public bool RegisterJob(List<JobLog> Jobs, bool Debug, Splash splash = null) {
      var filteredJobs = Jobs.Where(x => x.Hours >= this.Length).ToList();
      if (filteredJobs.Count <= 0) return false;

      JobLog jobLog = MainForm.GetRandomJobLog(filteredJobs);
      jobLog.Hours -= this.Length;
      if (splash != null) splash.Status = jobLog.Task.WorkText;

      string Description = jobLog.Description == "random" ? MainForm.GetRandomJobDescription() : jobLog.Description;
      JobLog loggedJob = new JobLog(jobLog, Description) {
        BeginDateTime = this.From,
        EndDateTime = this.To
      };
      if (Debug) {
        var DebMessage = new List<string> {
          $"Дата:{loggedJob.BeginDateTime.ToShortDateString()}:",
          $"С {loggedJob.BeginDateTime.ToShortTimeString()}, по {loggedJob.EndDateTime.ToShortTimeString()} - {loggedJob.LengthCurrentJob()}  часов",
          $"Наименование контрагента: {loggedJob.Task.ContractorName}",
          $"WorkName: {loggedJob.Task.WorkName}",
          $"Что сделано: {loggedJob.Description}"
        };
        RichMessageBox.ShowNew(DebMessage);
        //return false;
      }
      return MainForm.RegisterJob(loggedJob, Debug);
    }

    public static List<WorkSpan> GetWorkSpanList(DateTime from, DateTime to, Random rnd) {
      //int minimumWorkSpanHours = 3;
      var result = new List<WorkSpan>();
      TimeSpan span = to - from;
      double hours = span.TotalHours;
      if (span.TotalHours > MainForm.Settings.minimumWorkSpanHours) {
        double tempDouble = Helpers.GetRandomDouble(hours, rnd, out int mult, MainForm.Settings.minimumWorkSpanHours,
          MainForm.Settings.JobLogAccurancy);
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

    #region Properties

    /// <summary>
    ///   Начало рабочего отрезка
    /// </summary>
    public DateTime From { get; }

    /// <summary>
    ///   Окончание рабочего отрезка
    /// </summary>
    public DateTime To { get; }

    /// <summary>
    ///   Продолжительность рабочего отрезка
    /// </summary>
    public double Length {
      get { return (this.To - this.From).TotalMinutes / 60.0; }
    }

    #endregion Properties
  }

  public class Users {
    #region Instance Properties

    public int IDUser { get; set; }

    public Guid IDFavoriteMenu { get; set; }

    public string EMail { get; set; }

    public int? IDAuthor { get; set; }

    public DateTime CreateDate { get; set; }

    public int? IDModifier { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string ModifyLogin { get; set; }

    public Guid IDOrg { get; set; }

    public Guid IDUserGuid { get; set; }

    public Guid IDDesktop { get; set; }

    public Guid IDShortcut { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string FullName { get; set; }

    public string ShortName { get; set; }

    public object Settings { get; set; }

    #endregion Instance Properties
  }

  public enum ProjectControl {
    LG,
    RFM
  }
}