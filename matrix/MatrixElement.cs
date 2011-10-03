using System;
using System.Collections.Generic;
using System.Text;

namespace csalg_math.matrix
{
	public class MatrixElement
	{
		private double _value = 0;

		public MatrixElement() {
		
		}

		public MatrixElement(double value) {
		
		}

		/*public static double operator << (double value){
			
		}*/

		public double Value{
			get{
				return _value;
			}

			set{
				_value=value;
			}
		
		}
	}
}
