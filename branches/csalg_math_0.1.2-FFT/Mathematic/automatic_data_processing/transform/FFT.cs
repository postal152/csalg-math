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
		/// Экспериментально, не пользоваться лучше
		/// </summary>
		/// <param name="A"></param>
		/// <returns></returns>
		public static List<Complex> directTransform(List<Complex> A) {
			int n = A.Count;
			if (n == 1) {
				return A;
			}
			
			Complex w = new Complex(1.0, 0);
			Complex wn = Complex.FromExp((2 * Math.PI) / (double)n);
			
			List<Complex> a0 = new List<Complex>();
			List<Complex> a1 = new List<Complex>();
			
			for (int i = 0; i < n; i++) {
				if ((i%2)==0)
				{
					//Console.WriteLine("1");
					a1.Add(A[i].Copy());
				}else{
					//Console.WriteLine("0");

					a0.Add(A[i].Copy());
				}
			}

			List<Complex> y0 = directTransform(a0);
			List<Complex> y1 = directTransform(a1);

			List<Complex> y = new List<Complex>();

			/*Console.WriteLine("--------------------------");
			Console.WriteLine("n = " + n);
			Console.WriteLine("y0.Count = " + y0.Count);
			Console.WriteLine("y1.Count = " + y1.Count);*/

			//return null;
			for (int i = 0; i < n; i++) {
				y.Add(new Complex(0,1));
				//y.Add(null);
			}

			//Complex[] y = new Complex[n];

			//Console.WriteLine(y.Count);
			Complex y0k;
			Complex y1k;
			for (int k = 0; k < (n / 2); k++)
			{
				y0k = y0[k];
				y1k = y1[k];

				Complex w0k = w.Multiply(y1k);
				y[k] = (y0k.Add(w0k));
				y[k + (n / 2)] = (y0k.Subtract(w0k));
				w = w.Multiply(wn);
			}



			return y;//.ToList<Complex>(); ;
		}

		public static List<Complex> reverseTransform(List<Complex> A)
		{
			int n = A.Count;
			if (n == 1)
			{
				return A;
			}

			Complex w = new Complex(1.0, 0);
			Complex wn = Complex.FromExp(-(2 * Math.PI) / n);

			List<Complex> a0 = new List<Complex>();
			List<Complex> a1 = new List<Complex>();

			for (int i = 0; i < n; i++)
			{
				if ((i % 2) == 0)
				{
					//Console.WriteLine("1");
					a1.Add(A[i].Copy());
				}
				else
				{
					//Console.WriteLine("0");

					a0.Add(A[i].Copy());
				}
			}

			List<Complex> y0 = directTransform(a0);
			List<Complex> y1 = directTransform(a1);

			List<Complex> y = new List<Complex>();

			/*Console.WriteLine("--------------------------");
			Console.WriteLine("n = " + n);
			Console.WriteLine("y0.Count = " + y0.Count);
			Console.WriteLine("y1.Count = " + y1.Count);*/

			//return null;
			for (int i = 0; i < n; i++)
			{
				y.Add(new Complex(0, 1));
				//y.Add(null);
			}

			//Complex[] y = new Complex[n];

			//Console.WriteLine(y.Count);
			Complex y0k;
			Complex y1k;
			for (int k = 0; k < (n / 2); k++)
			{
				y0k = y0[k];
				y1k = y1[k];

				/*Console.WriteLine("-------------");
				Console.WriteLine(y0k);
				Console.WriteLine(y1k);*/
				y[k] = (y0[k].Add(w.Multiply(y1[k])));
				y[k + (n / 2)] = (y0[k].Subtract(w.Multiply(y1[k])));
				w = w.Multiply(wn);
			}



			return y;//.ToList<Complex>(); ;
		}

	}
}
