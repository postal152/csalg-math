using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalg_math.automatic_data_processing.genetic
{
	public class DNA
	{
		private string _generationName = "DefaultGenaration";

		private List<double> _gene;
		private double _testResult; 
		private uint _geneCount;

		private double _percentRank;

		public DNA(uint geneCount)
		{
			_geneCount = geneCount;
			_gene = initGene(_geneCount);
			_testResult = 0;// double.MaxValue;
		}

		~DNA() {
			/*_generationName = null;
			_testResult = 0;
			_geneCount = 0;
			_gene.Clear();
			_gene = null;*/
		}

		public List<double> GetCopyOfGenes() {
			return _gene.GetRange(0, _gene.Count);
		}

		private List<double> initGene(uint geneCount)
		{
			List<double> newGene = new List<double>();

			for (int i = 0; i < geneCount; i++)
			{
				newGene.Add(0);
			}
			return newGene;
		}

		public double this[int i]
		{
			get
			{
				return _gene[(int)i];
			}
			set
			{
				_gene[(int)i] = value;
			}

		}

		public uint GeneCount
		{
			get
			{
				return _geneCount;
			}
		}

		public double Rank
		{
			get
			{
			//	if (value > 1000)
				//{
					//
					//_testResult = _testResult;
				//}
				return _testResult;
			}
			set
			{
				if (value > 1000) {
					//
					//_testResult = _testResult;
				}
				//Console.WriteLine(value);
				_testResult = value;
			}
		}

		public double PercentRank
		{
			get
			{
				return _percentRank;
			}
			set
			{
				_percentRank = value;
			}
		}



		public string GenerationName
		{
			get
			{
				return _generationName;
			}
			set
			{
				_generationName = value;
			}
		}

		public DNA Copy()
		{
			DNA copy = new DNA(_geneCount);
			for (int i = 0; i < this.GeneCount; i++)
			{
				copy[i] = this[i];
				
			}

			copy.GenerationName = "Copy of " + this.GenerationName;
			copy.Rank = this.Rank;
			copy.PercentRank = this.PercentRank;

			return copy;
		}

		public override string ToString()
		{
			string geneOut = "";

			for (int i = 0; i < _geneCount; i++)
			{
				geneOut += this[i].ToString() + (i < _geneCount - 1 ? ", " : "");
			}

			return "[" + GenerationName + " {" + geneOut + "} Rank = {" + Rank + "}]";

			//return base.ToString();
		}

	}
}
