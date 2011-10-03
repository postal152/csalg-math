using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mathematic.matrix;

namespace Mathematic.factor_analisys
{
	public class MainFactorsMethod
	{

		public static MainFactorsMethodsResult Solve(Matrix rawData, ComplexNewtonMethodData newtonData) {
			MainFactorsMethodsResult resultData = null;

			int N, M, i, j, k;

			//находим нормированную матрицу
			Matrix Z = ComplexMethods.StandardizeTheMatrix(rawData);
			N = (int)Z.ColumnCount;
			M = (int)Z.RowCount;

			//находим матрицу парных корреляций
			Matrix R = Statistics.GetPairCorrelationsMatrix(Z);

			//находим след матрицы
			List<double> Pn = ComplexMethods.GetFadeevSolution(R);

			//определяем входные параметры для полинома
			List<double> P = new List<double>();
			N = Pn.Count;
			for (i = 0; i < N + 1; i++) P.Add(0);
			P[0] = 1;
			for (i = 1; i < P.Count; i++) P[i] = -Pn[i - 1];

			//находим корни полинома

			List<double> L = new List<double>();

			for (i = 0; i < R.RowCount; i++) {
				L.Add(-(P[i] / P[i + 1]));
			}

			newtonData.param = P;
			newtonData.coresNeeded = R.RowCount;

			Matrix lambda = ComplexMethods.ComplexNewtonMethod(newtonData);
			double temp = 0;
			for (i = 0; i < R.RowCount; i++) {
				
				//lambda[i][i].Value = ComplexMethods.SimpleNewtonMethod(P, L[i], newtonData.simplePrecision);
				temp += lambda[i][i].Value;
			}

			//Console.WriteLine("TEST " + temp);
			//6, P, 0, 10, 0.05, 0.01, 0.0001 - дают шесть корней, сумма лямбд 5,99529677515991

			//Console.WriteLine(lambda.printMe());


			List<Matrix> listOfUSystem = new List<Matrix>();
			List<Matrix> listOfBSystem = new List<Matrix>();

			#region инициализация данных для нахождения систем U
			N = (int)lambda.RowCount;

			for (i = 0; i < N; i++)
			{
				listOfBSystem.Add(new Matrix((uint)N, (uint)1));
			}

			for (i = 0; i < N; i++)
			{
				listOfUSystem.Add(new Matrix((uint)N, (uint)N));
			}

			for (i = 0; i < N; i++)
			{
				for (j = 0; j < listOfUSystem[i].RowCount; j++)
				{
					for (k = 0; k < listOfUSystem[i].ColumnCount; k++)
					{
						listOfUSystem[i][j][k].Value = 1;
					}
				}
				for (j = 0; j < listOfBSystem[i].RowCount; j++)
				{
					for (k = 0; k < listOfBSystem[i].ColumnCount; k++)
					{
						listOfBSystem[i][j][k].Value = 1;
					}
				}
			}


			Matrix tempMatrix;

			for (i = 0; i < N; i++)
			{
				tempMatrix = listOfUSystem[i];

				//приравниваем единице последний элемент
				for (j = 0; j < N; j++)
				{
					tempMatrix[j][N - 1].Value = 1;
				}


				for (j = 0; j < N - 1; j++)
				{
					for (k = 0; k < N - 1; k++)
					{
						if (j == k)
						{
							tempMatrix[j][k].Value = 1 - lambda[i][i].Value;
						}
						else
						{
							tempMatrix[j][k].Value = R[j][k].Value;
						}
					}
				}

				for (j = 0; j < N - 1; j++)
				{
					listOfBSystem[i][j][0].Value = -R[N - 1][j].Value * tempMatrix[j][N - 1].Value;//-r[i,m]*u[m,i]
				}

			}
			#endregion

			List<Matrix> tempU = new List<Matrix>();
			Matrix U = new Matrix((uint)N, (uint)N);
			#region Нахождение U
			for (i = 0; i < N; i++)
			{
				tempU.Add(ComplexMethods.GauseMethod((listOfUSystem[i]), listOfBSystem[i]));
			}

			for (i = 0; i < N; i++)
			{
				for (j = 0; j < N; j++)
				{
					U[i][j].Value = tempU[i][j][0].Value;
				}
			}
			#endregion

			///.Console.WriteLine..Console.Console.WriteLine(U.printMe());

			Matrix V = ComplexMethods.NormalizeVector(U);

			//Console.WriteLine(V.printMe());

			//Находим А
			//для начала квадратные корни извлекаем из всех корней лямбды
			Matrix quadroLambda = lambda.Copy();
			for (i = 0; i < lambda.RowCount; i++)
			{
				quadroLambda[i][i].Value = Math.Sqrt(Math.Abs(lambda[i][i].Value));
			}

			//Console.WriteLine(quadroLambda.printMe());

			Matrix A = V.MultiplyMatrixByMatrix(quadroLambda).InverseMatrix();
			//Console.WriteLine(A.printMe());
			// находим результирующую матрицу F и транспонируем, чтобы было красивее =) 
			Matrix F = A.MultiplyMatrixByMatrix(Z.TransposeMatrix());

			//Console.WriteLine((A.MultiplyMatrixByMatrix(A.InverseMatrix())).printMe());
			//Console.WriteLine((A.InverseMatrix().MultiplyMatrixByMatrix(A)).printMe());
			F = F.TransposeMatrix();

			resultData = new MainFactorsMethodsResult();
			resultData.F = F;
			resultData.A = A;

			return resultData;
		}
	}
}
