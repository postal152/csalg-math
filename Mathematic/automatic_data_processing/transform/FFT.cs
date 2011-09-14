using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mathematic.complex;

namespace Mathematic.automatic_data_processing.transform
{
	public class FFT
	{

		/// <summary>
		/// Быстрое преобразование Фурье
		/// </summary>
		/// <param name="A">Вектор значений сигнала, длинна должна быть равна одной из степени двойки</param>
		/// <returns>Список гармоник в комплексном представлении</returns>
		public static List<Complex> directTransform(List<Complex> A) {
			int n = A.Count;
			if (n == 1) {
				return A;
			}
			
			Complex w = new Complex(1, 0);
			Complex wn = Complex.FromExp((2 * Math.PI) / (double)n);
			
			List<Complex> a0 = new List<Complex>();
			List<Complex> a1 = new List<Complex>();
			
			for (int i = 0; i < n; i++) {
				if ((i&1)==0)
				{
					a1.Add(A[i].Copy());
				}else{
					a0.Add(A[i].Copy());
				}
			}

			List<Complex> y0 = directTransform(a0);
			List<Complex> y1 = directTransform(a1);

			List<Complex> y = new List<Complex>();

			for (int i = 0; i < n; i++) {
				y.Add(new Complex(1,0));
			}

			Complex y0k;
			Complex y1k;
			for (int k = 0; k < (n / 2); k++)
			{
				y0k = y0[k];
				y1k = y1[k];

				Complex w0k = w.Multiply(y1k);
				y[k] = (y0k.Add(w0k));
				y[k + n/2] = (y0k.Subtract(w0k));
				w = w.Multiply(wn);
			}

			return y;
		}

		public static List<Complex> reverseTransform(List<Complex> A)
		{
			return null;//.ToList<Complex>(); ;
		}

	}
}
