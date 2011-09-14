using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalg_math.automatic_data_processing.neural_network
{
	public class ActivationFunction
	{
		public const int SIGMOID = 0;

		private int _type;

		private ActivationFunction(int type){
			_type = type;
		}

		public static ActivationFunction GetActivationFunction(int type) {
			return new ActivationFunction(type);
		}

		public double Calculate(double input) {
			switch (_type) {
				case SIGMOID:
					return GetSigmoid(input);
				default:
					return GetSigmoid(input);
			}
		}
		///http://www.rae.ru/monographs/65-2465

		private double GetSigmoid(double input) {

			return 1 / (1 + Math.Pow(Math.E, -input));

		}

	}
}
