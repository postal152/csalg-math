using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mathematic.automatic_data_processing.neural_network.base_elements;

namespace Mathematic.automatic_data_processing.neural_network
{
	public class InputLayer:Layer
	{

		public InputLayer():base() {

			//Layer();
		}

		public void AddInputNeuron(InputNeuron inpNeuron) {
			if (inpNeuron != null) {
				_neurons.Add(inpNeuron);
			}
		}



	}
}
