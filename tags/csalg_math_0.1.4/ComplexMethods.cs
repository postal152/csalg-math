using System;
using System.Collections.Generic;
using System.Text;
using csalg_math.matrix;

namespace csalg_math
{
	public class ComplexMethods
	{
		#region Клевые сложные различные методы

		/// <summary>
		/// Стандартизирует переданную матрицу
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns>Возвращает стандартизированную матрицу</returns>
		public static Matrix StandardizeTheMatrix(Matrix matrix) {
			Matrix result = new Matrix(matrix.RowCount, matrix.ColumnCount);
			int N = (int)result.ColumnCount;
			int M = (int)result.RowCount;
			int i,j;
			double mathWaiting;
			double dispertion;
			for (i = 0; i < N; i++) {
				mathWaiting = Statistics.GetMathWating(matrix.Columns[i].GetRawData());
				dispertion = Statistics.GetDispersion(matrix.Columns[i].GetRawData(), mathWaiting);
				for (j = 0; j < M; j++) {
					result.Columns[i][j].Value = (matrix.Columns[i][j].Value - mathWaiting) / dispertion;
				}
			}
			return result;
		}

		/// <summary>
		/// Эм... чего-то про след матрицы и метод Фадеева
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns></returns>
		public static List<double> GetFadeevSolution(Matrix matrix) {
			if (matrix.RowCount != matrix.ColumnCount) throw new Exception("Матрица не квадратная!");

			int N = (int)matrix.RowCount;
			Matrix A = matrix.Copy();
			Matrix protoA = matrix.Copy();
			Matrix B = new Matrix((uint)N, (double)1);
			Matrix E = new Matrix((uint)N, (double)1);
			List<double> Pn = new List<double>();

			int i;
			double tempP;

			for (i = 0; i < N; i++) {
				A = protoA.MultiplyMatrixByMatrix(B);
				tempP = ((1 / ((double)(i + 1))) * A.GetTrOfMatrix());
				B = A.SubtractionMatrix(E.MultiplyMatrixByNumber(tempP));
				Pn.Add(tempP);
			}

			return Pn;
		}

		/// <summary>
		/// Возвращает норму вектора
		/// </summary>
		/// <param name="vector">Вектор</param>
		/// <returns>Норма</returns>
		public static double VectorNorm(List<double> vector) {
			double result = 0;

			for (int i = 0; i < vector.Count; i++) {
				result += Math.Pow(vector[i], 2);
			}
			return Math.Sqrt(result);
		}

		/// <summary>
		/// Возвращает нормализованную матрицу
		/// </summary>
		/// <param name="matrix">исходная матрица</param>
		/// <returns>нормализованная</returns>
		public static Matrix NormalizeVector(Matrix matrix) {
			Matrix V = new Matrix(matrix.RowCount, matrix.ColumnCount);

			List<double> tempVector;
			int i, j;

			for (i = 0; i < V.RowCount; i++) {
				tempVector = matrix[i].GetRawData();
				for (j = 0; j < V.ColumnCount; j++) {
					V[i][j].Value = tempVector[j] / VectorNorm(tempVector);
				}
			}
			return V;
		}

		public static Matrix NewtonMethod(List<double> param, double precision)
		{
			//копируем начальный набор параметров
			int N = param.Count;
			int i, j;
			List<double> W = new List<double>();
			for (i = 0; i < N; i++) W.Add(0);

			List<double> P = param;

			//решаем характеристическое уравнение


			List<List<double>> L1 = new List<List<double>>();
			//L.Add(new List<double>());
			//N = N + 1;

			for (i = 0; i < N + 1; i++)
			{
				L1.Add(new List<double>());
				for (j = 0; j < N + 1; j++)
				{
					L1[i].Add(0);
				}
			}

			//задаем начальные приближения
			for (i = 1; i < N; i++)
			{
				L1[i][i] = (-P[i] / (P[i - 1]));
			}

			List<double> L = new List<double>();

			N--;
			for (i = 0; i < N; i++)
			{
				L.Add(L1[i + 1][i + 1]);
			}

			//выполняем итерационный процесс по методу Ньютона
			double b, _b;
			double e = 0;
			Boolean flag = true;
			do
			{
				for (i = 0; i < N; i++) W[i] = 0;

				for (i = 0; i < N; i++)
				{
					b = 0;
					_b = 0;
					b = SolutePolynom(P, L[i]);
					W[i] = b;
					_b = SoluteProizvPolynom(P, L[i]);
					L[i] = L[i] - (b / _b);
					e = b;
				}

				flag = false;
				for (i = 0; i < N; i++)
				{
					if (Math.Abs(W[i]) > precision)
					{
						flag = true;
					}
				}

			} while (flag);

			Matrix result = new Matrix((uint)L.Count);

			for (i = 0; i < L.Count; i++)
			{
				result[i][i].Value = L[i];
			}

			return result;
		}

		/// <summary>
		/// Метод Ньютона для нахождения корня полинома
		/// </summary>
		/// <param name="param">параметры полинома</param>
		/// <param name="startCore">начальное приближение корня</param>
		/// <param name="precision">точность</param>
		/// <returns>найденный корень</returns>
		public static double SimpleNewtonMethod(List<double> param, double startCore, double precision) {
			double W=0;
			double b, _b;
			double e = 0;
			double L = startCore;
			List<double> P=param;
			bool flag=true;

			do
			{
				b = 0;
				_b = 0;
				b = ComplexMethods.SolutePolynom(P, L);
				W = b;
				_b = ComplexMethods.SoluteProizvPolynom(P, L);
				L = L - (b / _b);
				e = b;
				if (Math.Abs(W) < precision)
				{
					flag = false;
				}
			} while (flag);

			return L;
		}

		/// <summary>
		/// Метод Ньютона типа сложный чтобы найти все корни
		/// </summary>
		/// <param name="coresNeeded">количество необходимых корней</param>
		/// <param name="param">параметры для полинома</param>
		/// <param name="startCore">начальное значения диапозона</param>
		/// <param name="finishCore">конечное значение диапозона</param>
		/// <param name="coreStep">шаг для приближенных корней</param>
		/// <param name="corePrecision">точность с которой корни должны отличаться</param>
		/// <param name="simplePrecision">точность для нахождения одного приближенного корня</param>
		/// <returns></returns>
		public static Matrix ComplexNewtonMethod(ComplexNewtonMethodData data)
		{
			uint coresNeeded = data.coresNeeded;
			List<double> param = data.param;
			double startCore = data.startCore;
			double finishCore = data.finishCore;
			double coreStep = data.coreStep;
			double corePrecision = data.corePrecision;
			double simplePrecision = data.simplePrecision;


			Matrix result = new Matrix(coresNeeded);

			List<double> res = new List<double>();

			double currentCore = startCore;
			//double oldCore = 0;
			//bool flag = true;

			List<double> allPrecisions=new List<double>();
			
			do{
				allPrecisions.Add(currentCore);	
				currentCore+=coreStep;
			}while(currentCore<finishCore);
			
			int i=0;
			for (i = 0; i < allPrecisions.Count; i++) {
				res.Add(SimpleNewtonMethod(param, allPrecisions[i], simplePrecision));	
			}


			List<double> res2 = new List<double>();
			for (i = 0; i < res.Count; i++) {
				if (!NewtonCheckForCores(coresNeeded, res2, res[i], corePrecision) && res[i]>0) {
					res2.Add(res[i]);
				}
			}

			for (i = 0; i < result.RowCount; i++)
			{
				result[i][i].Value = res2[i];
			}
			return result;
		}

		private static bool NewtonCheckForCores(uint coresNeeded, List<double> cores, double core, double corePrecision) {
			for (int i = 0; i < cores.Count; i++) {
				if (NewtonCoreRule(cores[i], core, corePrecision) && cores.Count==coresNeeded) {
					return true;
				}
			}

			return false;
		}

		private static bool NewtonCoreRule(double core1, double core2, double corePrecision)
		{
			if (Math.Abs(Math.Abs(core1) - Math.Abs(core2)) < Math.Abs(corePrecision)) {
				return true;
			}
			return false;
		}


		public static Matrix SoluteSystemOfLinearEquations(Matrix A, Matrix B) {
			return A.InverseMatrix().MultiplyMatrixByMatrix(B);
		}

		public static Matrix GauseMethod(Matrix _A, Matrix _B) {
			int i = 0, j, k;
			int N = (int)_A.RowCount;

			Matrix A = _A.Copy();
			Matrix B = _B.Copy();
			Matrix X = new Matrix((uint)N, (uint)1);
			

			N--;

			for (i = 0; i < N - 1; i++)
			{
				for (j = i + 1; j < N; j++)
				{
					A[j][i].Value = -A[j][i].Value / A[i][i].Value;
					for (k = i + 1; k < N; k++)
					{
						A[j][k].Value = A[j][k].Value + A[j][i].Value * A[i][k].Value;
						B[j][0].Value = B[j][0].Value + A[j][i].Value * B[i][0].Value;
					}


				}
			}

			X[N][0].Value = B[N][0].Value / A[N][N].Value;
			double h;
			for (i = N - 1; i >= 0; i--)
			{
				h = B[i][0].Value;
				for (j = i + 1; j < N; j++)
				{
					h = h - X[j][0].Value * A[i][j].Value;
				}
				//for j:=i+1 to n do h:=h-Result[j]*A[i,j];  
				X[i][0].Value = h / A[i][i].Value;
			}
			return X;
		}


		/// <summary>
		/// Решает полином с заданными параметрами
		/// </summary>
		/// <param name="parames">Параметры при переменных</param>
		/// <param name="core">Значение переменной</param>
		/// <returns>значение</returns>
		public static double SolutePolynom(List<double> parames, double core)
		{
			double result = 0;
			int pN = parames.Count;
			for (int i = 0; i < pN; i++)
			{
				result = result + parames[i] * Math.Pow(core, pN - i - 1);
			}
			return result;
		}

		/// <summary>
		/// Решает производную полинома
		/// </summary>
		/// <param name="parames">Параметры при переменных</param>
		/// <param name="core">Значение переменной</param>
		/// <returns>значение</returns>
		public static double SoluteProizvPolynom(List<double> parames, double core)
		{
			double result = 0;
			int pN = parames.Count;
			int cN = pN - 1;
			for (int i = 0; i < cN; i++)
			{
				result = result + (cN - i) * parames[i] * Math.Pow(core, cN - i - 1);
			}
			return result;
		}

		/// <summary>
		/// Оценка Розенблата Парзена
		/// </summary>
		/// <param name="x">Текущее значение для оценки</param>
		/// <param name="data">Данные где столбцы это признаки</param>
		/// <param name="h">массив коэффицентов размытости</param>
		/// <param name="kernel">Функция ядра</param>
		/// <returns>Оценку... Внезапно</returns>
		public static double RosenblattParzenAssessment(List<double> x,int indexOfX, Matrix data, List<double> h, KernelType kernel) {

			int i, j;
			double resultSumm = 0;
			double resultMult = 0;
			for (i = 0; i < data.RowCount; i++) {
				resultMult = 1;
				for (j = 0; j < data.ColumnCount; j++) {
					//if (indexOfX != i)
					//{
						resultMult *= (1.0 / h[j]) * kernel.Calculate((x[j] - data[i][j].Value) / h[j]);
					//}
				}
				resultSumm += resultMult;
			}

			return resultSumm/data.RowCount;
		}

		#endregion


	}
}
