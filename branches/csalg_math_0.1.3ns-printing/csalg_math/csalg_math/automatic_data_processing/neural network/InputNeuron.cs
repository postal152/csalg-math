using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using csalg_math.automatic_data_processing.neural_network.base_elements;

namespace csalg_math.automatic_data_processing.neural_network
{
	public class InputNeuron:Neuron
	{
		public InputNeuron() {
			//base.Neuron();
		}

		public void SetValue(double value) {
			Value = value;
		}

	}
}
