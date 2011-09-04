using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathematic.utils
{
	
	public class GetRandom
	{

		private static Random _random;

		public static double GetNextDouble() {
			initRand();
			return _random.NextDouble();
		}

		public static double GetNextDoubleFromRange(double x, double y) {
			double result;

			double range = y - x;

			result = GetNextDouble() * range + x;
			
			return result;
		}

		public static void ReInit(){
			_random=null;
			initRand();
		}

		private static void initRand() {
			if (_random == null) {
				_random = new Random(DateTime.Now.Millisecond);
			}		
		}
	}

	

}
