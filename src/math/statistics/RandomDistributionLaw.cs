using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csalgs.utils;
namespace csalgs.math
{
	public interface IRandom
	{
		double Get();
	}

	public class EvenLaw : IRandom {
		private double a = 0;
		private double b = 1;
		public EvenLaw() { }

		public EvenLaw(double a, double b) {
			if (a >= b) throw new ArgumentOutOfRangeException("a must be < b");
			this.a = a;
			this.b = b;
		}

		public double Get()
		{
			return Rnd.GetNextDouble() * (b - a) + a;
		}
	}

	public class NormalLaw : IRandom
	{
		/*Преобразование Бокса — Мюллера второй вариант
		 */
		private double m = 0;
		private double d = 1;
		public NormalLaw() { }

		public NormalLaw(double m, double d)
		{
			this.m = m;
			this.d = d;
		}

		public double Get()
		{
			double s = -1, x = 0, y = 0, z1, z2; ;

			while (s <= 0 || s > 1) {
				x = Rnd.GetNextDoubleFromRange(-1, 1);
				y = Rnd.GetNextDoubleFromRange(-1, 1);

				s = x * x + y * y;
			}

			z1 = x * Math.Sqrt((-2 * Math.Log(s)) / s);
			z2 = y * Math.Sqrt((-2 * Math.Log(s)) / s);

			return m + d * z1;
		}
	}


}
