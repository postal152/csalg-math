using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalgs.math.elementary
{
	public class Vector
	{
		private double[] values;
		public Vector(double[] values) {
			this.values = values;
		}

		public double[] Values {
			get {
				return values;
			}
		}
	}
}
