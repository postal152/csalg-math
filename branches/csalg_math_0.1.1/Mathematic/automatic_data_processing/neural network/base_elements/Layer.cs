using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Mathematic.automatic_data_processing.neural_network.base_elements{
	public class Layer
	{
		protected List<Neuron> _neurons;

		public Layer() {
			_neurons = new List<Neuron>();
		}

		public Neuron this[int i] {
			get {
				return _neurons[i];
			}
		}

		public List<Neuron> Neurons {
			get {
				return _neurons;
			}
		}

		public int NeuronsCount {
			get {
				return _neurons.Count;
			}
		}



		public List<double> Output {
			get {
				List<double> outp = new List<double>();

				foreach (Neuron n in _neurons) {
					outp.Add(n.Value);
				}
				return outp;
			}
		}

		public override string ToString()
		{
			string outp = "Layer {";

			foreach (Neuron n in _neurons) {
				outp += "[" + n.Value + "]";
			}
			outp += "}";

			return outp;
		}

		/*public double output[int i]{
			get{
				return 0;
			}
		}*/

	}
}
