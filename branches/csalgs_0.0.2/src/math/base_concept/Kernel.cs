using System;

namespace csalgs.math
{
	public delegate double KernelCore(double value);
	public interface IKernel {
		double Calculate(double value);
	}

	public class Kernel : IKernel {
		public static IKernel GAUSSE = new Kernel((KernelCore)((double value) => {
			return Math.Pow(2 * Math.PI, -0.5) * Math.Exp((-0.5) * Math.Pow(value, 2)); 
		}));

		public static IKernel EPANICHNIKOV = new Kernel((KernelCore)((double value) => {
			if (Math.Abs(value) <= 1) 
			{
				return (0.75) * (1 - Math.Pow(value, 2));
			}
			else 
			{ 
				return 0; 
			} 
		}));

		public static IKernel QUARTICAL = new Kernel((KernelCore)((double value) =>
		{
			if (Math.Abs(value) <= 1)
			{
				return (15 / 16) * Math.Pow(1 - Math.Pow(value, 2), 2);
			}
			else
			{
				return 0;
			}
		}));

		public static IKernel TREANGLE = new Kernel((KernelCore)((double value) =>
		{
			if (Math.Abs(value) <= 1)
			{
				return (1 - Math.Abs(value));
			}
			else
			{
				return 0;
			}
		}));

		public static IKernel RECTANGLE = new Kernel((KernelCore)((double r)=>{
			if (Math.Abs(r) <= 1)
			{
				return 0.5;
			}
			else
			{
				return 0;
			}
		}));


		private KernelCore core;
		public Kernel(KernelCore core)
		{
			this.core = core;
			
		}

		public double Calculate(double value)
		{
			return core(value);
		}
	}
}