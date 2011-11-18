using System;
using csalgs.math.elementary;

/*
 * Author: Shirobok Pavel (ramshteks@gmail.com)
 * Created: 17.11.2011
 * 
 * Class description:
 * (RUS)
 * Класс содержит ряд методов для статистического анализа
 * 
 * (ENG)
 * 
 * License:
 * AS-IS
 * 
 * Part of:
 * csalgs project
 */

namespace csalgs.math.statistics
{
	class Statistics
	{

		/// <summary>
		/// Get mean value
		/// </summary>
		/// <param name="data">selection</param>
		/// <returns></returns>
		public static double GetMeanValue(double[] data)
		{
			int S = data.Length;
			double summ = 0;

			for (int i = 0; i < S; i++)
			{
				summ += data[i];
			}
			summ /= S;
			return summ;
		}

		/// <summary>
		/// Get dispersion value
		/// </summary>
		/// <param name="data">selection</param>
		/// <param name="meanValue">mean value</param>
		/// <returns></returns>
		public static double GetDispersionValue(double[] data, double meanValue)
		{
			int S = data.Length;
			double summ = 0;
			double mw = meanValue;

			for (int i = 0; i < S; i++)
			{
				summ += (Math.Pow(data[i] - mw, 2));
			}
			return Math.Sqrt(summ / S);
		}

		/// <summary>
		/// Get dispersion value
		/// </summary>
		/// <param name="data">selection</param>
		/// <returns></returns>
		public static double GetDispersionValue(double[] data)
		{
			int S = data.Length;
			double summ = 0;
			double mw = GetMeanValue(data);

			for (int i = 0; i < S; i++)
			{
				summ += (Math.Pow(data[i] - mw, 2));
			}
			return Math.Sqrt(summ / S);
		}

		/// <summary>
		/// Get value of covatiance
		/// </summary>
		/// <param name="dataX">first selection</param>
		/// <param name="dataY">second selection</param>
		/// <returns></returns>
		public static double GetCovarianceValue(double[] dataX, double[] dataY)
		{
			double mwX = GetMeanValue(dataX);
			double mwY = GetMeanValue(dataY);

			double[] tempData = new double[dataX.Length];
			for (int i = 0; i < dataX.Length; i++)
			{
				tempData[i] = (dataX[i] - mwX) * (dataY[i] - mwY);
			}

			return GetMeanValue(tempData);
		}

		/// <summary>
		/// Get value of correlation
		/// </summary>
		/// <param name="dataOne">first selection</param>
		/// <param name="dataTwo">second selection</param>
		/// <returns></returns>
		public static double GetCorrelationValue(double[] dataOne, double[] dataTwo)
		{
			return GetCovarianceValue(dataOne, dataTwo) / (Math.Sqrt(GetDispersionValue(dataOne, GetMeanValue(dataOne))) * Math.Sqrt(GetDispersionValue(dataTwo, GetMeanValue(dataTwo))));
		}

		/// <summary>
		/// Get matrix with pair correlation values
		/// </summary>
		/// <param name="matrix">matrix with selections</param>
		/// <returns></returns>
		public static RealMatrix GetPairCorrelationsMatrix(RealMatrix matrix)
		{
			int N = matrix.ColumnCount;
			int i, j;

			RealMatrix result = new RealMatrix(N, N);

			for (i = 0; i < N; i++)
			{
				for (j = 0; j < N; j++)
				{
					result[i,j] = GetCorrelationValue(matrix.GetColumnArray(i), matrix.GetColumnArray(j));
				}
			}
			return result;
		}

	}
}
