using System;
using System.Collections.Generic;
using System.Text;

namespace csalgs.math
{
	public class Range
	{
		private double min;
		private double max;
		public Range(double v1, double v2) {
			init(v1, v2);
		}

		private void init(double v1, double v2) {
			min = Math.Min(v1, v2);
			max = Math.Max(v1, v2);
		}

		public double Min {
			set {
				init(value, Max);
			}

			get {
				return min;
			}
		}

		public double Max
		{
			set
			{
				init(Min, value);
			}

			get
			{
				return max;
			}
		}

		public double Length {
			get {
				return max - min;
			}
		}

		public bool check(double value) {
			return value > min && value < max;
		}

		public bool checkLeft(double value) {
			return value >= min && value < max;
		}

		public bool checkRight(double value) {
			return value > min && value <= max;
		}

		public bool checkFull(double value) {
			return value >= min && value <= max;
		}
	}
}
