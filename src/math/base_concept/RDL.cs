using System;
namespace csalgs.math
{
	public interface IRDL
	{
		void newSeed();
		void newSeed(int seed);
		double Get();
		double Get(double min, double max);
		double Get(Range range);
		double Get2(double meanValue, double dispersion);

		Random Source
		{
			get;
		}

	}

	abstract public class BaseRDL : IRDL {
		protected Random rand_source;
		public BaseRDL() {
			newSeed();
		}

		public void newSeed()
		{
			int seed = System.DateTime.Now.Millisecond;
			newSeed(seed);
			
		}

		public void newSeed(int seed)
		{
			rand_source = new Random(seed);
		}

		public Random Source {
			get {
				return rand_source;
			}
		}

		public double Get() {
			return Get(0, 1);
		}

		public double Get(double min, double max) {
			return Get(new Range(min, max));
		}

		abstract public double Get(Range range);

		public double Get2(double meanValue, double dispersion) {
			return Get(new Range(meanValue - dispersion / 2, meanValue + dispersion / 2));
		}
	}

	public class EvenRDL:BaseRDL {

		
		public override double Get(Range range)
		{
			return rand_source.NextDouble() * range.Length + range.Min;
		}
	}

	public class NormalRDL : BaseRDL {
		public override double Get(Range range)
		{
			double s = -1, x = 0, y = 0, z1;

			while (s <= 0 || s > 1)
			{
				x = -1 + rand_source.NextDouble() * 2;
				y = -1 + rand_source.NextDouble() * 2;

				s = x * x + y * y;
			}

			z1 = x * Math.Sqrt((-2 * Math.Log(s)) / s);

			double norm_z = 1/2 + z1 / 2;
			return range.Length * norm_z + range.Min;
		}
	}
}
