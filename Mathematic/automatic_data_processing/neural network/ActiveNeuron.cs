using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mathematic.automatic_data_processing.neural_network.base_elements;

namespace Mathematic.automatic_data_processing.neural_network
{
	public class ActiveNeuron:Neuron
	{
		private ActivationFunction _activationFunction;

		private List<Neuron> _inputs;
		private List<double> _arguments;

		public ActiveNeuron(ActivationFunction actFunc) {
			_activationFunction = actFunc;

			_inputs = new List<Neuron>();
			_arguments = new List<double>();

		}

		public void Compute() {
			double summ = 0;
			int inputCount=_inputs.Count;
			for (int i = 0; i < inputCount; i++ )
			{
				summ += _inputs[i].Value * _arguments[i];
			}

			Value = _activationFunction.Calculate(summ);

		}

		public List<double> GetArguments() {
			List<double> result = new List<double>();

			for (int i = 0; i < _arguments.Count; i++) {
				result.Add(_arguments[i]);
			}

			//_arguments.CopyTo(result
			return result;
		}

		public void SetArguments(List<double> args) {
			for (int i = 0; i < args.Count; i++) {
				this[i] = args[i];
			}
		}

		public int ArgumentsCount {
			get {
				return _arguments.Count;
			}
		}

		public void AddLink(Neuron neuron)
		{
			if (neuron != null) {
				_inputs.Add(neuron);
				_arguments.Add(1.0);
			}
		}

		public void AddLink(Neuron neuron, double argument)
		{
			if (neuron != null)
			{
				_arguments.Add(argument);
				_inputs.Add(neuron);
			}
		}

		public double this[int i] {
			get {
				return _arguments[i];
			}
			set {
				_arguments[i] = value;
			}
		}

	}
}
