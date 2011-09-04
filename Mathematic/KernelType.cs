using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathematic
{
	public class KernelType
	{
		private int _type;

		private const int EPANICHNIKOVA = 0;
		private const int QUARTICAL = 1;
		private const int TREANGLE = 2; 
		private const int GAUSSE = 3;
		private const int RECTANGLE = 4;

		private KernelType(int type){
			_type = type;
		}

		public static KernelType GetEpanichnikovaKernel() {
			return new KernelType(EPANICHNIKOVA);
		}

		public static KernelType GetQuarticalKernel()
		{
			return new KernelType(QUARTICAL);
		}

		public static KernelType GetTreangleKernel()
		{
			return new KernelType(TREANGLE);
		}

		public static KernelType GetGausseKernel()
		{
			return new KernelType(GAUSSE);
		}

		public static KernelType GetRectangeKernel()
		{
			return new KernelType(RECTANGLE);
		}

		public double Calculate(double r) {

			switch (_type) {
				case EPANICHNIKOVA:
					return CalcEpanechnikova(r);
				//break;
				case QUARTICAL:
					return CalcQuartical(r);
					//break;
				case TREANGLE:
					return CalcTreangle(r);
					//break;
				case GAUSSE:
					return CalcGausse(r);
					//break;
				case RECTANGLE:
					return CalcRectangle(r);
					//break;
				default:
					return CalcEpanechnikova(r);
					//break;
			}
		}

		private double CalcEpanechnikova(double r) {
			if (Math.Abs(r) <= 1)
			{
				return (0.75) * (1 - Math.Pow(r, 2));
			}
			else {
				return 0;
			}
		}

		private double CalcQuartical(double r) {
			if (Math.Abs(r) <= 1)
			{
				return (15/16) * Math.Pow(1 - Math.Pow(r, 2),2);
			}
			else
			{
				return 0;
			}
		}

		private double CalcTreangle(double r) {
			if (Math.Abs(r) <= 1)
			{
				return (1 - Math.Abs(r));
			}
			else
			{
				return 0;
			}
		}

		private double CalcRectangle(double r) {
			if (Math.Abs(r) <= 1)
			{
				return 0.5;
			}
			else
			{
				return 0;
			}
		}

		private double CalcGausse(double r) {
			return Math.Pow(2 * Math.PI, -0.5) * Math.Exp((-0.5) * Math.Pow(r, 2));
		}

	}
}
