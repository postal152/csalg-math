using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mathematic.matrix;

namespace Mathematic.automatic_data_processing.modeling
{
	public class LeastSquareMethod
	{

		/// <summary>
		/// Метод наименьших квадратов
		/// </summary>
		/// <param name="X">данные по икс</param>
		/// <param name="Y">данные по игрек</param>
		/// <param name="order">степень полинома, который надо получить, 
		/// для примера с+bx+ax^2, в таком же порядке и будет возвращен результат</param>
		/// <returns>Возвращаются показатели при полиноме</returns>
		public static List<double> Solve(List<double> X, List<double> Y, uint order)
		{
			Matrix A = new Matrix(order);
			Matrix B = new Matrix(order, 1);
			//Matrix X = new Matrix(1, order);

			for (int i = 0; i < order; i++) {
				for (int j = 0; j < order; j++) {
					for(int k=0; k < X.Count; k++){
						A[i][j].Value += Math.Pow(X[k], i) * Math.Pow(X[k], j);
					}
					
				}
			}

			for (int i = 0; i < order; i++) {
				for(int k=0; k < X.Count; k++){
					B[i][0].Value += Math.Pow(X[k], i) * Y[k];
				}
			}

			Matrix resM = A.InverseMatrix().MultiplyMatrixByMatrix(B);
			Console.WriteLine(resM.printMe());
			List<double> result = resM.Columns[0].GetRawData();
			//result.Reverse();
			return result;
		}
	}
}
