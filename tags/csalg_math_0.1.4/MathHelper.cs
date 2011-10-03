using System;
using System.Collections.Generic;
using System.Text;

namespace csalg_math
{
	public class MathHelper
    {
		private static Random _random;

        public static double random()
        {
			if (_random == null) {
				_random = new Random(DateTime.Now.Millisecond);
			}
            //Random rand = new Random(DateTime.Now.Millisecond);
			return _random.NextDouble();
        }

        /*
         * Statistic methods
         * 
         */

        public static double GetMathWating(List<double> data){
            int S = data.Count;
            double summ = 0;

            for (int i = 0; i < S; i++) {
                summ += data[i];
            }
            summ /= S;
            return summ;
        }

        public static double GetDispersion(List<double> data, double mathWaiting) {
            int S = data.Count;
            double summ = 0;
            double mw = mathWaiting;

            for (int i = 0; i < S; i++)
            {
                summ += (Math.Pow(data[i] - mw, 2));
            }
            //summ /= S;
            return summ / S;////// Math.Sqrt(summ);
            //return Math.Sqrt(summ / S);
        }

        public static double GetCovariation(List<double> dataX, List<double> dataY)
        {
            double mwX = GetMathWating(dataX);
            double mwY = GetMathWating(dataY);

            List<double> tempData = new List<double>();
            for (int i = 0; i < dataX.Count; i++) {
                tempData.Add((dataX[i] - mwX) * (dataY[i] - mwY));
            }

            return GetMathWating(tempData);
        }

        public static double GetCorrelation(List<double> dataOne, List<double> dataTwo) {
            return GetCovariation(dataOne, dataTwo) / (Math.Sqrt(MathHelper.GetDispersion(dataOne, GetMathWating(dataOne))) * Math.Sqrt(MathHelper.GetDispersion(dataTwo, GetMathWating(dataTwo))));
        }

        /*
         * Work with Matrix
         * 
         */

        public static List<List<double>> initMatrix(int N, int M) {
            List<List<double>> newMatrix = new List<List<double>>();

            List<double> currentVector;
            for (int i = 0; i < N; i++)
            {
                currentVector = new List<double>();
                for (int j = 0; j < M; j++)
                {
                    currentVector.Add(0);
                }
                newMatrix.Add(currentVector);
            }
            return newMatrix;
        }

        public static List<List<double>> GetSingularMatrix(int N)
        {
            List<List<double>> newMatrix = initMatrix(N, N);
            for (int i = 0; i < N; i++)
            {
                //for (int j = 0; j < N; j++)
                //{
                    newMatrix[i][i] = 1;
                //}
            }
            return newMatrix;
        }


        public static List<List<double>> TransparateMatrix(List<List<double>> matrix){
            return RotateMatrix(matrix);//TODO check transparent matrix
        }

        public static List<List<double>> MultiplicateMatrix(List<List<double>> matrix1, List<List<double>> matrix2)
        {
            int m1 = matrix1.Count;
            int n1 = matrix1[0].Count;
            int m2 = matrix2.Count;
            int n2 = matrix2[0].Count;

            if (m1 != n2) throw new Exception("Херня с матрицами");

            List<List<double>> result = initMatrix(m1, n2);

            for (int i = 0; i < m1; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    for (int k = 0; k < n1; k++)
                    {
                        result[i][j] +=( matrix1[i][k] * matrix2[k][j]);
                    }
                }
            }

            return result;
        }

        public static List<List<double>> MultiplicateMatrixByNumber(List<List<double>> matrix, double number)
        {
            int N = matrix.Count;
            int M = matrix[0].Count;

            List<List<double>> newMatrix = initMatrix(N, M);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    newMatrix[i][j] = matrix[i][j] * number;
                }
            }
            return newMatrix;
        }

        public static List<List<double>> SubtractionMatrix(List<List<double>> matrix1, List<List<double>> matrix2) {
            int N = matrix1.Count;
            int M = matrix1[0].Count;

            List<List<double>> result = initMatrix(N, M);

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    result[i][j] = matrix1[i][j] - matrix2[i][j];
                }
            }
            return result;
        }

        public static List<List<double>> RotateMatrix(List<List<double>> matrix)
        {
            // matrix N*M
            int N = matrix.Count;
            int M = matrix[0].Count;

            // newMatrix M*N
            List<List<double>> newMatrix = new List<List<double>>();

            List<double> currentVector;
            for (uint i = 0; i < M; i++)
            {
                currentVector = new List<double>();
                for (int j = 0; j < N; j++)
                {
                    currentVector.Add(0);
                }
                newMatrix.Add(currentVector);
            }

            for (int i = 0; i < M; i++)
            {
                //currentVector = newMatrix[i];
                for (int j = 0; j < N; j++)
                {
                    newMatrix[M-i-1][j]=(matrix[j][i]);
                }
                //newMatrix.Add(currentVector);
            }

            return newMatrix;
        }

        public static double GetTrFromMatrix(List<List<double>> matrix) {
            double summ = 0;
            for (int i = 0; i < matrix.Count; i++)
            {
                summ += matrix[i][i];
            }
            
            return summ;
        }

        /*
         * Complex Methods 
         * 
         */

        public static List<List<double>> StandardizeMatrix(List<List<double>> matrix)
        {
            int N=matrix.Count;
            int M=matrix[0].Count;
            double mw;
            double dis;

            List<List<double>> standMatrix = initMatrix(N, M);
            for (int i = 0; i < N; i++) {
                mw = GetMathWating(matrix[i]);
                dis = GetDispersion(matrix[i], mw);
                for (int j = 0; j < M; j++) {
                    standMatrix[i][j] = (matrix[i][j] - mw) / (Math.Sqrt(dis));
                }
            }
            return standMatrix;
        }

        public static List<List<double>> GetPairCorrelationMatrix(List<List<double>> matrix)
        {
            int N = matrix.Count;

            List<List<double>> result = initMatrix(N, N);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    result[i][j] = GetCorrelation(matrix[i], matrix[j]);
                }
            }

            return result;

            //int N = matrix.Count;

            //return MultiplicateMatrixByNumber(MultiplicateMatrix(TransparateMatrix(matrix), matrix) ,1 / N);

        }

        public static List<double> GetFadeevSolution(List<List<double>> matrix) {
            int N=matrix.Count;
            Console.WriteLine("Count " + N.ToString());
            List<List<double>> A = CopyMatrix(matrix);
            List<List<double>> protoA = CopyMatrix(matrix);
            List<List<double>> B = GetSingularMatrix(N);
            List<List<double>> E = GetSingularMatrix(N);
            List<double> Pn = new List<double>();
           
            double tempP;
            for (int i = 0; i < N; i++) {
                A = MultiplicateMatrix(protoA, B);
                
                tempP = (1 / ((double)(i + 1))) * GetTrFromMatrix(A);
                //Console.WriteLine("Test " + tempP.ToString());
                B = SubtractionMatrix(A, MultiplicateMatrixByNumber(E, tempP));

                //break;
                Pn.Add(tempP);
            }



            for (int i = 0; i < 6; i++) {
                for (int j = 0; j < 6; j++) {
                   // Console.Write(B[i][j]+" ");
                }

                //Console.WriteLine();
            }
            
            //Console.WriteLine(temp);
            
            return Pn;
        }


		public static double VectorNorma(List<double> vector) {
			double result = 0;

			for (int i = 0; i < vector.Count; i++) {
				result += Math.Pow(vector[i], 2);
			}
			
			
			return Math.Sqrt(result);
		}

		public static List<List<double>> NormalizeVector(List<List<double>> vectorMatrix) {
			List<List<double>> V = new List<List<double>>();

			List<double> tempVector;
			for (int i = 0; i < vectorMatrix.Count; i++) {
				tempVector = vectorMatrix[i];
				V.Add(new List<double>());
				for (int j = 0; j < vectorMatrix.Count; j++) {
					V[i].Add(tempVector[j] / VectorNorma(tempVector));
				}
			}
			
			
			return V;
		}

		/*
		 * Gause Method
		 */
		

		public static List<double> GauseMethod(List<List<double>> A1, List<double> B2)
		{
			int i = 0, j, k;
			int N = A1.Count;

			List<List<double>> A = new List<List<double>>();
			List<double> B = new List<double>();

			for (i = 0; i < N; i++)
			{
				A.Add(new List<double>());
				for (j = 0; j < N; j++)
				{
					A[i].Add(A1[i][j]);
				}
			}

			for (i = 0; i < N; i++)
			{
				B.Add(B2[i]);
			}

			List<double> X = new List<double>();
			for (i = 0; i < N; i++)
			{
				X.Add(1);
			}

			N--;

			for (i = 0; i < N - 1; i++) {
				for (j = i + 1; j < N; j++) {
					A[j][i] = -A[j][i] / A[i][i];
					for (k = i + 1; k < N; k++) {
						A[j][k]=A[j][k]+A[j][i]*A[i][k];
						B[j] = B[j] + A[j][i] * B[i]; 
					}
					

				}
			}

			X[N] = B[N] / A[N][N];
			double h;
			for (i = N - 1; i >= 0; i--) {
				h=B[i]; 
 				for(j=i+1; j<N; j++){
					h=h-X[j]*A[i][j];
				}
				//for j:=i+1 to n do h:=h-Result[j]*A[i,j];  
				X[i]=h/A[i][i];  
			}
			return X;
		}



		public static List<double> GauseMethod2(List<List<double>> A1, List<double> B2) {
			int i = 0, j,k;
			int N = A1.Count;

			List<List<double>> A = new List<List<double>>();
			List<double> B = new List<double>();

			for (i = 0; i < N; i++) {
				A.Add(new List<double>());
				for (j = 0; j < N; j++) {
					A[i].Add(A1[i][j]);
				}
			}

			for (i = 0; i < N; i++)
			{
				B.Add(B2[i]);
			}

			i = 0;
			do
			{
				B[i] = B[i] / A[i][i];
				for (j = N - 1; j >= i; j--) {
					A[i][j] = A[i][j] / A[i][i];
				}

				for (k = i + 1; k < N - 1; k++) {
					B[k] = B[k] - B[i] * A[k][i];
					for (j = N - 1; j >= i; j--) {
						//c[k,j]:=c[k,j]-c[i,j]*c[k,i]
						A[k][j] = A[k][j] - A[i][j] * A[k][i];
					}
				}
				
				Console.WriteLine("ProtoMatrix i=" + i);
				for (j = 0; j < N; j++)
				{
					for (k = 0; k < N; k++)
					{
						Console.Write(A[j][k] + " ");
					}
					Console.WriteLine();
				}

				i++;
			} while (i != N);

			List<double> X = new List<double>();
			for (i = 0; i < N; i++) {
				X.Add(0);
			}

			X[N - 1] = 1;//B[N - 1];
			for (i = N - 2; i >= 0; i--) {
				X[i] = B[i];
				for (j = i + 1; j < N-1; j++) {
					X[i] = X[i] - X[j] * A[i][j];

				}
			}

			
			return X;
		}


        /*
         * Newton method 
         */

        
        public static List<List<double>> NewtonMethod(List<double> param, double precision)
        {
            //копируем начальный набор параметров
            int N = param.Count;
            int i, j;
            List<double> W = new List<double>();// 0;

            for (i = 0; i < N; i++) W.Add(0);

            List<double> P = new List<double>();


            for (i = 0; i < N + 1; i++)
            {
                P.Add(0);
            }

            P[0] = 1;
            for (i = 1; i < P.Count; i++)
            {
                //P[N - i - 1] = param[i - 1];
                P[i] = -param[i - 1];
            }

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
            for (i = 1; i < N + 1; i++)
            {
                L1[i][i] = (-P[i] / (P[i - 1]));
            }

            List<double> L = new List<double>();

            for (i = 0; i < N; i++) {
                L.Add(L1[i + 1][i + 1]);
            }

            //test

            /*L[0] = 0.1;
            L[1] = 0.25;
            L[2] = 0.45;
            L[3] = 0.55;
            L[4] = 2;
            L[5] = 3;*/

           /* double t = 0;
            double min = 0;
            double max = 10;// Math.Exp(1 / 6);
            t = max;
            for (i = 0; i < L.Count; i++) {
                Console.WriteLine(t);
                L[i] = t;
                t /= 2.5;
                
            }*/
            //L[L.Count-1] = min;


            //выполняем итерационный процесс по методу Ньютона
            double b, _b;
            double e=0;
            Boolean flag = true;
            do
            {
                for (i = 0; i < N; i++) W[i]=0;
                for (i = 0; i < N; i++) ///???;
                {
                    b = 0;
                    _b = 0;
                    //вычисляем значение полинома в i-й точке
                    b = SolutePolynom(P, L[i]);
                    //Console.Write(b);

                    //double b1=

                    //находим максимальную невязку
                    /*if (b < W)
                    {
                        W = b;
                    }*/

                    W[i] = b;

                    //вычисляем значение производной в i-й точке
                    _b = SoluteProizvPolynom(P, L[i]);
                    ///вносим поправку для i-й точки
                    L[i] = L[i] - (b / _b);

                    //W = Math.Abs(b - e);

                    e = b;
                }

                flag = false;
                for (i = 0; i < N; i++) {
                    if (Math.Abs(W[i]) > precision) {
                        flag = true;
                    }
                }

            } while (flag);
            //Math.Abs(W) > precision
            //копируем L в одномерный массив
            List<List<double>> result = new List<List<double>>();
			for (i = 0; i < L.Count; i++) {
				result.Add(new List<double>());
				for (j = 0; j < L.Count; j++) {
					result[i].Add(0);
				}
			}

            for (i = 0; i < L.Count; i++)
            {
				result[i][i] = L[i];
            }
            
            return result;
        }

        public static List<double> NewtonMethod2(List<double> param, List<double> cores, double precision)
        {
            
            int N = param.Count;
            int i;
            List<double> W = new List<double>();

            for (i = 0; i < N; i++) W.Add(0);


            List<double> P = new List<double>();
            List<double> L = new List<double>();

            for (i = 0; i < param.Count; i++) {
                P.Add(param[i]);
            }

            for (i = 0; i < cores.Count; i++)
            {
                L.Add(cores[i]);
            }

            //решаем характеристическое уравнение

            //выполняем итерационный процесс по методу Ньютона
            double b, _b;
            Boolean flag = true;
            do
            {
                for (i = 0; i < N; i++) W[i] = 0;

                for (i = 0; i < N-1; i++)
                {
                    b = 0;
                    _b = 0;
                    //вычисляем значение полинома в i-й точке
                    b = SolutePolynom(P, L[i]);
                    
                    W[i] = b;

                    //вычисляем значение производной в i-й точке
                    _b = SoluteProizvPolynom(P, L[i]);
                    ///вносим поправку для i-й точки
                    L[i] = L[i] - (b / _b);
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

            //копируем L в одномерный массив
            List<double> result = new List<double>();
            for (i = 0; i < L.Count; i++)
            {
                result.Add(L[i]);
            }

            return result;
        }

		public static List<double> OptimizedNewtonMethod(List<double> param, double precision, double range)
		{
			int N = param.Count;
			int i, j;
			int rN = N - 1;

			List<double> P = new List<double>();
			for (i = 0; i < param.Count; i++) P.Add(param[i]);

			//cоздаем начальный набор диапозона 
			List<double> L = new List<double>();

			double shift;// = (range * 2) / (N - 2);


			for (i = 0; i < N - 1; i++)
			{
				L.Add(0);
			}




			

			List<double> trueResult=new List<double>();

			List<double> result = new List<double>(); ;

			double range2 = 0.01;
			double rangeStep = 0.5;
			while(trueResult.Count!=rN){
				shift = (range2 * 2) / (rN - 1);
				for (i = 0; i < rN; i++) {
					L[i] = range2 - shift*i;
				}

				for (i = 0; i < L.Count; i++)
				{
					//Console.Write(L[i] + " ");
				}
				//Console.WriteLine();

				result = NewtonMethod2(P, L, precision);

				for (i = 0; i < result.Count; i++) {
					double temp = result[i];
					bool addFlag = true;

					if (trueResult.Count == 0) addFlag = true;

					for (j = 0; j < trueResult.Count; j++) {
						if (!NewtonRule(trueResult[j], temp,100000000)) {
							addFlag = false;
						}
					}

					if (addFlag) {
						trueResult.Add(temp);
					}

				}
				range2 = range2 +rangeStep;

				for (i = 0; i < trueResult.Count; i++) {
					Console.Write(trueResult[i] + " ");

				}
				Console.WriteLine();

				if (range2 > range) break;
			
			}
			if (trueResult.Count == rN)
			{
				result = NewtonMethod2(P, trueResult, precision);
			}

			return result;
		}

        public static List<double> OptimizedNewtonMethod_old2(List<double> param, double precision, double range) {
            int N = param.Count;
            int i;

            List<double> P = new List<double>();
            for (i = 0; i < param.Count; i++)P.Add(param[i]);

            //cоздаем начальный набор диапозона 
            List<double> L = new List<double>();

			double shift = (range * 2) / (N-2);
			
			
			for (i = 0; i < N - 1; i++)
			{
				L.Add(range - shift * i);
			}

			//for (i = 1; i < N; i++)
		//	{
			//	L[i - 1] = -P[i] / P[i - 1];
			//}



			//L[0] = range;
			//L[L.Count-1] = 0;//*/


			for (i = 0; i < L.Count; i++) {
				Console.Write(L[i] + " ");
			}
			Console.WriteLine();



			List<double> result;

			//Находим корни 
			result = NewtonMethod2(P, L, precision);
			//и первый корень записываем как первое приближение
			L[0] = result[0];
			L[L.Count - 1] = result[result.Count - 1];

			double min = L[L.Count - 1];
			double max = L[0];

			bool flag = true ;

			double opt_shift = 0.01;

			for (i = 1; i < L.Count; i++)
			{
				
				Console.WriteLine("i=" + i + "--- shift = " + opt_shift);
				for (int j = 0; j < L.Count; j++)
				{
					Console.WriteLine(L[j] + " - " + result[j]);
				}
				flag = true;

				

				L[i] = L[i - 1];// +opt_shift;

				do{

					L[i] -= opt_shift;
					//Console.WriteLine(L[i]+" min "+min);
					result = NewtonMethod2(P, L, precision);
					
					//

					if (L[i] < 0)
					{
						flag = false;
					}

					if (NewtonRule(result[i], result[i - 1], 100))
					{
						
						L[i] = result[i];
						flag=false;
					}

					
				
				}while(flag);

				//opt_shift /= 10;

			}

			result = NewtonMethod2(P, L, precision);


            return result;
        }

		public static List<double> OptimizedNewtonMethod_old(List<double> param, double precision, double range)
		{
			int N = param.Count;
			int i;

			List<double> P = new List<double>();
			for (i = 0; i < param.Count; i++) P.Add(param[i]);

			//cоздаем начальный набор диапозона 
			List<double> L = new List<double>();

			double shift = (range * 2) / (N - 2);


			for (i = 0; i < N - 1; i++)
			{
				L.Add(0);
			}

			L[0] = range;
			L[L.Count - 1] = -range;


			for (i = 0; i < L.Count; i++)
			{
				Console.Write(L[i] + " ");
			}
			Console.WriteLine();



			List<double> result;

			//Находим корни 
			result = NewtonMethod2(P, L, precision);
			//и первый корень записываем как первое приближение
			L[0] = result[0];
			L[L.Count - 1] = result[result.Count - 1];

			double min = L[L.Count - 1];
			double max = L[0];

			bool flag = true;

			double opt_shift = 0.001;

			for (i = 1; i < L.Count - 1; i++)
			{

				Console.WriteLine("i=" + i + "--- shift = " + opt_shift);
				for (int j = 0; j < L.Count; j++)
				{
					Console.WriteLine(L[j] + " - " + result[j]);
				}
				flag = true;

				//opt_shift /= 2;

				L[i] = L[i - 1] - opt_shift;

				do
				{


					//Console.WriteLine(L[i]+" min "+min);
					result = NewtonMethod2(P, L, precision);
					L[i] -= opt_shift;

					if (NewtonRule(result[i], result[i - 1], 10000))
					{
						L[i] = result[i];
						flag = false;
					}

					if (L[i] > min)
					{
						flag = false;
					}

				} while (flag);



			}

			result = NewtonMethod2(P, L, precision);


			return result;
		}



		private static bool NewtonRule(double r1, double r2, double precision) {
			r1 = Math.Floor(r1 * precision) / precision;
			r2 = Math.Floor(r2 * precision) / precision;

			if (r1 == r2)
			{
				return false;
			}

			if (r1 >= r2) {
				return false;
			}

			if (r1 < r2)
			{
				return true;
			}

			return false;

		}

        public static double SolutePolynom(List<double> parames, double core)
        {
            double result = 0;
            int pN = parames.Count;
            //Console.WriteLine("Param.Count=" + pN);
            for (int i = 0; i < pN; i++)
            {
                result = result + parames[i] * Math.Pow(core, pN - i - 1);
            }
            //result += parames[parames.Count - 1];

            return result;
        }

        public static double SoluteProizvPolynom(List<double> parames, double core)
        {
            double result = 0;
            int pN = parames.Count;
            int cN = pN - 1;
            for (int i = 0; i < cN; i++)
            {
                result = result + (cN - i) * parames[i] * Math.Pow(core, cN - i - 1);
            }
            //result += parames[parames.Count - 1];

            return result;
        }


        

        /*
         * Utils
         * 
         */

        public static List<List<double>> CopyMatrix(List<List<double>> matrix)
        {
            int N = matrix.Count;
            int M = matrix[0].Count;
            List<List<double>> result = initMatrix(N,M);

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    result[i][j] = matrix[i][j];
                }
            }

            return result;

        }

    }

    

}
