using System;
/*
 * Author: Shirobok Pavel (ramshteks@gmail.com)
 * Created: 17.11.2011
 * 
 * Class description:
 * (RUS)
 * Класс содержит ряд ядер применяющихся в статистическом анализе.
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
	/// <summary>
	/// Main kernel's interface
	/// </summary>
	public interface IKernel {
		double Calculate(double value);
	}

	public class GaussKernel : IKernel {
		public double Calculate(double value) {
			return Math.Pow(2 * Math.PI, -0.5) * Math.Exp((-0.5) * Math.Pow(value, 2));
		}
	}

	public class EpanechnikovsKernel : IKernel
	{
		public double Calculate(double value)
		{
			if (Math.Abs(value) <= 1)
			{
				return (0.75) * (1 - Math.Pow(value, 2));
			}
			else {
				return 0;
			}
		}
	}

	public class QuarticalKernel : IKernel
	{
		public double Calculate(double r)
		{
			if (Math.Abs(r) <= 1)
			{
				return (15/16) * Math.Pow(1 - Math.Pow(r, 2),2);
			}
			else
			{
				return 0;
			}
		}
	}

	public class TreangleKernel : IKernel
	{
		public double Calculate(double r)
		{
			if (Math.Abs(r) <= 1)
			{
				return (1 - Math.Abs(r));
			}
			else
			{
				return 0;
			}
		}
	}

	public class RectangleKernel : IKernel
	{
		public double Calculate(double r)
		{
			if (Math.Abs(r) <= 1)
			{
				return 0.5;
			}
			else
			{
				return 0;
			}
		}
	}

}
