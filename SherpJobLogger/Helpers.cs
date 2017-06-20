using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace SherpJobLogger {

  public static class Helpers {

    /// <summary>
    /// Случайный double с заданной точностью
    /// </summary>
    /// <param name="max">Включаемая верхняя граница возвращаемого случайного числа</param>
    /// <param name="rnd">Инициализированный генератор случайных чисел</param>
    /// <param name="multiplier">Множитель, использованный при вычислении</param>
    /// <param name="min">Включаемая нижняя граница возвращаемого случайного числа</param>
    /// <param name="accurancy">Требуемая точность - Double выше нуля и до единицы</param>
    /// <returns></returns>
    public static double GetRandomDouble(double max, Random rnd, out int multiplier, int min = 0, double accurancy = 0.1) {
      int tempInt = GetNearestIntMulti(max, out multiplier, accurancy);
      int tempMin = (int)(min / accurancy);
      int tempMax = (int)(tempInt / accurancy);
      tempInt = rnd.Next(tempMin, tempMax + 1);
      double tempDouble = tempInt * accurancy / multiplier;
      return tempDouble;
    }

    /// <summary>
    /// Вычисляем ближайшее целое число, умножая double
    /// </summary>
    /// <param name="doubleD"></param>
    /// <param name="multiplier">Множитель, использовавшийся для нахождения целого числа</param>
    /// <param name="accurancy">Требуемая точность - Double выше нуля и до единицы</param>
    /// <returns></returns>
    public static int GetNearestIntMulti(double doubleD, out int multiplier, double accurancy = 0.1) {
      if (accurancy <= 0 || accurancy > 1) throw new ArgumentOutOfRangeException(nameof(accurancy));
      double temp = 1 / accurancy;
      int mult = (int)temp;
      double fraction = RoundToFraction(doubleD % 1, accurancy);
      for (int i = 1; i <= mult; i++) {
        if (fraction * (double)i % 1 != (double)0) continue;
        mult = i;
        break;
      }
      multiplier = mult;
      return Convert.ToInt32(doubleD * mult);
    }

    /// <summary>
    /// Округление числа с double точностью
    /// </summary>
    /// <param name="value">Значение, подвергаемое округлению</param>
    /// <param name="fraction">Точность с которой округляем</param>
    /// <returns>double</returns>
    public static double RoundToFraction(double value, double fraction) {
      return Math.Round(value / fraction) * fraction;
    }

    public static Stream ToStream(this string str) {
      MemoryStream stream = new MemoryStream();
      StreamWriter writer = new StreamWriter(stream);
      writer.Write(str);
      writer.Flush();
      stream.Position = 0;
      return stream;
    }

    public static string ToNiceString(int num, NiceStringType type) {

      int digit = num % 10;

      string result = String.Empty;
      switch (type) {
        case NiceStringType.Hours:
          switch (digit) {
            case 1:
              result = "час";
              break;
            case 2:
            case 3:
            case 4:
              result = "часа";
              break;
            default:
              result = "часов";
              break;
          }
          break;
        case NiceStringType.Days:
          switch (digit) {
            case 1:
              result = "день";
              break;
            case 2:
            case 3:
            case 4:
              result = "дня";
              break;
            default:
              result = "дней";
              break;
          }
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }

      return result;
    }
    public static string ToNiceString(double num, NiceStringType type) {
      return ToNiceString((int)Math.Floor(num), type);
    }

    /// <summary>
    /// Биндилка таблицы в класс
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static T BindData<T>(DataTable dt) {
      DataRow dr = dt.Rows[0];

      // Get all columns' name
      List<string> columns = new List<string>();
      foreach (DataColumn dc in dt.Columns) {
        columns.Add(dc.ColumnName);
      }

      // Create object
      var ob = Activator.CreateInstance<T>();

      // Get all fields
      var fields = typeof(T).GetFields();
      foreach (var fieldInfo in fields) {
        if (columns.Contains(fieldInfo.Name)) {
          // Fill the data into the field
          fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
        }
      }

      // Get all properties
      var properties = typeof(T).GetProperties();
      foreach (var propertyInfo in properties) {
        if (columns.Contains(propertyInfo.Name)) {
          // Fill the data into the property
          try {
            propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
          }
          catch (ArgumentException) {
            if (propertyInfo.PropertyType == typeof(Guid) && dr[propertyInfo.Name] == null) {
              propertyInfo.SetValue(ob, Guid.Empty);
            }
          }
        }
      }

      return ob;
    }

    public enum NiceStringType {
      Hours,
      Days
    }
    public static string GetFQDN() {
      string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
      string hostName = Dns.GetHostName();

      domainName = "." + domainName;
      if (!hostName.EndsWith(domainName))  // if hostname does not already include domain name
      {
        hostName += domainName;   // add the domain name part
      }

      return hostName;                    // return the fully qualified name
    }
  }
}