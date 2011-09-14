using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalg_math.automatic_data_processing.neural_network.base_elements
{
	public class Neuron
	{
		private double _value;

		public Neuron() {
			
		}

		public double Value {
			get {
				return _value;
			}
			set {
				_value = value;
			}
		}

	}
}
