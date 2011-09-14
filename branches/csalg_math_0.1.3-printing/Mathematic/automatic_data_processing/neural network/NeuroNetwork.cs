using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Mathematic.automatic_data_processing.neural_network
{
	public class NeuroNetwork
	{

		private InputLayer _inputLayer;
		private List<ActiveLayer> _layers;
		private const string CRLF = "\r\n";

		public NeuroNetwork() {
			_layers = new List<ActiveLayer>();
		}

		public void Compute() {
			foreach (ActiveLayer layer in _layers) {
				layer.Compute();
			}
		}

		public void GenerateMLP(int inputCount, List<int> middleLayersCounts)
		{
			_inputLayer = null;
			_layers = new List<ActiveLayer>();
			
			InputLayer inp = new InputLayer();

			for (int i = 0; i < inputCount; i++)
			{
				inp.AddInputNeuron(new InputNeuron());
			}

			AddInputLayer(inp);

			int middleLayerNeuroCount;
			uint layersCount = (uint)middleLayersCounts.Count;

			ActiveLayer act;
			ActiveNeuron neuro;
			for (int i = 0; i < layersCount; i++)
			{
				act = new ActiveLayer();
				middleLayerNeuroCount = middleLayersCounts[i];
				for (int j = 0; j < middleLayerNeuroCount; j++)
				{
					neuro = new ActiveNeuron(ActivationFunction.GetActivationFunction(ActivationFunction.SIGMOID));
					if (i == 0)
					{
						for (int k = 0; k < inputCount; k++)
						{
							neuro.AddLink(inp[k]);
						}
					}
					else
					{
						int tMLCount = middleLayersCounts[i-1];
						for (int k = 0; k < tMLCount; k++)
						{
							neuro.AddLink(this[i - 1][k]);
						}

					}
					act.AddActiveNeuron(neuro);
				}

				this.AddActiveLayer(act);
			}
		}

		public string SerializeMLP() {
			string result = "";
			///количество входов
			///количество слоев(включая выходной)
			///количество нейронов на каждом слое(включая выходной)
			///последовательно параметры

			int inputCount = _inputLayer.NeuronsCount;
			int layersCount = _layers.Count;
			List<int> layerNeuronsCountList = new List<int>();
			for (int i = 0; i < _layers.Count; i++) {
				layerNeuronsCountList.Add(_layers[i].NeuronsCount);
			}

			List<double> arguments = GetNetworkArguments();

			result += inputCount.ToString() + CRLF;
			result += layersCount.ToString() + CRLF;

			for (int i = 0; i < layerNeuronsCountList.Count; i++)
			{
				result += layerNeuronsCountList[i].ToString() + CRLF;
			}

			result += CRLF;//просто раздлим чисто визуально

			for (int i = 0; i < arguments.Count; i++)
			{
				result += arguments[i].ToString() + CRLF;
			}

			return result;
		}

		public void DeserializeMLP(string serializedData) {
			//[-0-9,]+\n*
			Regex strExp = new Regex("[-0-9,]+\n*");

			MatchCollection results = strExp.Matches(serializedData);

			///количество входов
			///количество слоев(включая выходной)
			///количество нейронов на каждом слое(включая выходной)
			///последовательно параметры

			int inputCount = 0;
			int layersCount = 0;
			List<int> layerNeuronsCountList = new List<int>();
			List<double> arguments=new List<double>();

			Match data = results[0];
			inputCount = int.Parse(data.Value);
//
			//Console.WriteLine(inputCount);

			data = results[1];
			layersCount = int.Parse(data.Value);

			//Console.WriteLine(layersCount);
			

			for (int i = 2; i < layersCount + 2; i++)
			{
				data = results[i];
				//Console.WriteLine("lC=" + data.Value);
				layerNeuronsCountList.Add(int.Parse(data.Value));
			}



			
			string separator = results[2 + layersCount].Value;
			//Console.WriteLine("SEP"+separator);
			for (int i = 2 + layersCount; i < results.Count; i++) {
				//Console.WriteLine("arg=" + data.Value);
				arguments.Add(double.Parse(results[i].Value));
			}
			//Console.ReadKey();

			GenerateMLP(inputCount, layerNeuronsCountList);
			SetNetworkArguments(arguments);


		}

		public ActiveLayer this[int i]
		{
			get {
				return _layers[i];
			}
		}

		public void AddInputLayer(InputLayer inpLayer) {
			_inputLayer = inpLayer;
		}

		public void AddActiveLayer(ActiveLayer activeLayer) {
			if (activeLayer != null) {
				_layers.Add(activeLayer);
			}
		}

		public InputLayer Input {
			get {
				return _inputLayer;
			}
		}

		public ActiveLayer Output {
			get {
				return _layers[_layers.Count - 1];
			}
		}

		public List<double> GetNetworkArguments(){
			List<double> result = new List<double>();
			foreach (ActiveLayer layer in _layers) {
				result.AddRange(layer.GetLayerArguments());
			}
			return result;
		}

		public void SetNetworkArguments(List<double> args) {
			int shift = 0;
			int count = 0;

			List<double> subList;

			foreach (ActiveLayer layer in _layers)
			{
				count = layer.ArgumentsCount;
				subList = new List<double>();

				for (int i = shift; i < shift + count; i++) {
					subList.Add(args[i]);
				}
				layer.SetLayerArguments(subList);
				//layer.SetLayerArguments(args.GetRange(shift, count));
				shift += count;
			}
		}

		public int ArgumentsCount {
			get {
				int summ = 0;
				foreach (ActiveLayer layer in _layers) {
					summ += layer.ArgumentsCount;
				}

				return summ;
			}
		}

	}
}
