using System;
using System.Collections.Generic;
using System.Text;
using Mathematic.matrix;

namespace Mathematic
{
	public class Statistics
	{
		/// <summary>
		/// Нахождение оценки математического ожидания
		/// </summary>
		/// <param name="data">массив с данными</param>
		/// <returns></returns>
		public static double GetMathWating(List<double> data)
		{
			int S = data.Count;
			double summ = 0;

			for (int i = 0; i < S; i++)
			{
				summ += data[i];
			}
			summ /= S;
			return summ;
		}

		/// <summary>
		/// Возвращает оценку дисперсии
		/// </summary>
		/// <param name="data">массив с данными</param>
		/// <param name="mathWaiting"></param>
		/// <returns></returns>
		public static double GetDispersion(List<double> data, double mathWaiting)
		{
			int S = data.Count;
			double summ = 0;
			double mw = mathWaiting;

			for (int i = 0; i < S; i++)
			{
				summ += (Math.Pow(data[i] - mw, 2));
			}
			return Math.Sqrt(summ / S);
		}

		/// <summary>
		/// Расчет ковариации между двумя выборками
		/// </summary>
		/// <param name="dataX">выборка 1</param>
		/// <param name="dataY">выборка 2</param>
		/// <returns></returns>
		public static double GetCovariation(List<double> dataX, List<double> dataY)
		{
			double mwX = GetMathWating(dataX);
			double mwY = GetMathWating(dataY);

			List<double> tempData = new List<double>();
			for (int i = 0; i < dataX.Count; i++)
			{
				tempData.Add((dataX[i] - mwX) * (dataY[i] - mwY));
			}

			return GetMathWating(tempData);
		}

		/// <summary>
		/// Расчет корреляции между двумя выборками
		/// </summary>
		/// <param name="dataOne">выборка 1</param>
		/// <param name="dataTwo">выборка 2</param>
		/// <returns></returns>
		public static double GetCorrelation(List<double> dataOne, List<double> dataTwo)
		{
			return GetCovariation(dataOne, dataTwo) / (Math.Sqrt(MathHelper.GetDispersion(dataOne, GetMathWating(dataOne))) * Math.Sqrt(MathHelper.GetDispersion(dataTwo, GetMathWating(dataTwo))));
		}

		/// <summary>
		/// Вычисляет матрицу парных корреляций
		/// </summary>
		/// <returns>МАтрицу парных корреляция</returns>
		public static Matrix GetPairCorrelationsMatrix(Matrix matrix) {
			int N = (int)matrix.ColumnCount;
			int i, j;

			Matrix result = new Matrix((uint)N);

			for (i = 0; i < N; i++) {
				for (j = 0; j < N; j++) {
					result[i][j].Value = GetCorrelation(matrix.Columns[i].GetRawData(), matrix.Columns[j].GetRawData());
				}
			}
			return result;
		}

	}
}
