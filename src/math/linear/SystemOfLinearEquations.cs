using System;
using csalgs.math.elementary;

/*
 * Author: Shirobok Pavel (ramshteks@gmail.com)
 * Created: 17.11.2011
 * 
 * Class description:
 * (RUS)
 * Файл содержит классы для решения систем линейных уравнений.
 * На данный момент есть:
 * -Метод Гаусса
 * -Матричный метод(TODO НЕ ТЕСТИРОВАЛСЯ, НЕОБХОДИМА ДОРАБОТКА)
 * 
 * (ENG)
 * 
 * License:
 * AS-IS
 * 
 * Part of:
 * csalgs project
 */

namespace csalgs.math.linear
{
	public interface ISLEMethod {
		double[] Solve(RealMatrix A, RealMatrix B);
	}

	public class GausseMethod : ISLEMethod {

		public double[] Solve(RealMatrix _A, RealMatrix _B)
		{
			int i = 0, j, k;
			int N = (int)_A.RowCount;

			RealMatrix A = _A.Clone();
			RealMatrix B = _B.Clone();

			N--;
			for (i = 0; i < N - 1; i++)
			{
				for (j = i + 1; j < N; j++)
				{
					A[j, i] = -A[j, i] / A[i, i];
					for (k = i + 1; k < N; k++)
					{
						A[j, k] = A[j, k] + A[j, i] * A[i, k];
						B[j, 0] = B[j, 0] + A[j, i] * B[i, 0];
					}


				}
			}

			double[] X = new double[N];

			X[N] = B[N, 0] / A[N, N];
			double h;
			for (i = N - 1; i >= 0; i--)
			{
				h = B[i, 0];
				for (j = i + 1; j < N; j++)
				{
					h = h - X[j] * A[i, j];
				}

				X[i] = h / A[i, i];
			}

			return X;
		}
	}

	public class MatrixMethod : ISLEMethod {

		public double[] Solve(RealMatrix A, RealMatrix B)
		{
			//TODO выяснить со столбца брать или со строки
			return (!A * B).GetColumnArray(0);
		}
	}

}
