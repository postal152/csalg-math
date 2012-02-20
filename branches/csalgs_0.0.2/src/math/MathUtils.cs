using System;

namespace csalgs.math
{
	
	class MathUtils
	{
		public static int Sign(double value)
		{
			return value == 0 ? 0 : (value < 0 ? -1 : 1);
		}

		public static int Sign(int value)
		{
			return Sign((double)value);
		}

		public static int Sign(float value)
		{
			return Sign((double)value);
		}

		public static double Distance(double x1, double y1, double x2, double y2) {
			return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
		}
	}
}
