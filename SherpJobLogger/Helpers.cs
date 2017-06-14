using System;
using System.IO;

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
  }
}