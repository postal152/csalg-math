using System;
using csalgs.math.elementary;

/*
 * Author: Shirobok Pavel (ramshteks@gmail.com)
 * Created: 17.11.2011
 * 
 * Class description:
 * (RUS)
 * Класс содержит реализацию метода наименьших квадратов.
 * TODO Нуждается в доработке!
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
	public class LeastSquareMethod
	{
		private double[] result;
		public LeastSquareMethod() {
		
		}

		public double GetY(double X) {
			return 0;
			//TODO LeastSquareMethod
		}

		public void Solve(double[] X, double[] Y, int order)
		{
			RealMatrix A = RealMatrix.GetQuadroMatrix(order);
			RealMatrix B = new RealMatrix(order, 1);

			for (int i = 0; i < order; i++) {
				for (int j = 0; j < order; j++) {
					for(int k=0; k < X.Length; k++){
						A[i,j] += Math.Pow(X[k], i) * Math.Pow(X[k], j);
					}
				}
			}

			for (int i = 0; i < order; i++) {
				for (int k = 0; k < X.Length; k++)
				{
					B[i,0] += Math.Pow(X[k], i) * Y[k];
				}
			}

			RealMatrix resM = !A * B;
			
			double[] result = resM.GetColumnArray(0);

			this.result = result;
		}
	}
}
