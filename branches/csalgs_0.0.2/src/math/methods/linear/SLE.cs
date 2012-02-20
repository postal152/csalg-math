using System;
namespace csalgs.math.linear
{
	public interface ISLE {
		IVector Solve(RealMatrix A, RealMatrix B);
	}

	public class SLEGausse : ISLE {

		public IVector Solve(RealMatrix _A, RealMatrix _B)
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

			return new Vector(X);
		}
	}

	public class SLEMatrix : ISLE {

		public IVector Solve(RealMatrix A, RealMatrix B)
		{
			return new Vector((!A * B).GetColumnArray(0));
		}
	}

}
