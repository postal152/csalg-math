using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mathematic.automatic_data_processing.neural_network.base_elements;

namespace Mathematic.automatic_data_processing.neural_network
{
	public class ActiveLayer:Layer
	{
		public ActiveLayer() : base() {
			
		}

		public void AddActiveNeuron(ActiveNeuron actNeuron) {
			if (actNeuron != null) {
				_neurons.Add(actNeuron);
			}
		}

		public void Compute() {
			int neuronsCount = _neurons.Count;
			ActiveNeuron neuron;
			for (int i = 0; i < neuronsCount; i++) {
				neuron=_neurons[i] as ActiveNeuron;
				neuron.Compute();
			}
		}

		public List<double> GetLayerArguments() {
			List<double> args = new List<double>();

			foreach (ActiveNeuron neuro in _neurons) {
				args.AddRange(neuro.GetArguments());
			}

			return args;
		}

		public void SetLayerArguments(List<double> args) {
			//List<double> list = new List<double>();

			int shift = 0;
			int count = 0;
			List<double> subList;

			foreach (ActiveNeuron neuro in _neurons) {
				count = neuro.ArgumentsCount;

				subList = new List<double>();
				for (int i = shift; i < shift + count; i++) {
					subList.Add(args[i]);
				}
				neuro.SetArguments(subList);
				//neuro.SetArguments(args.GetRange(shift, shift));
				shift += count;
			}
		}

		public int ArgumentsCount {
			get {
				int summ = 0;

				foreach (ActiveNeuron n in _neurons) {
					summ += n.ArgumentsCount;
				}
				return summ;
			}
		}

	}
}
