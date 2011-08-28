using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Mathematic.utils;
using Mathematic.automatic_data_processing.genetic.strategies;

namespace Mathematic.automatic_data_processing.genetic
{
	public class Generation
	{
		private List<DNA> _dnaList;

		private DNA _bestDNA;

		private double _mutationChance = 0.1;
		private double _mergeChance = 0.5;
		
		public Generation() {
			_dnaList = new List<DNA>();
		}
		
		~Generation(){}

		public void AddDNA(DNA dna){
			if (dna != null) {
				_dnaList.Add(dna);
			}
		}

		private void CalcRankForAllDNA(IDNARankStrategy rankStrategy)
		{
			int dnaCount = _dnaList.Count;
			double summOfRank = 0;
			DNA currentDNA;
			double currentRank;
			for (int i = 0; i < dnaCount; i++)
			{
				currentDNA = _dnaList[i];
				currentRank = rankStrategy.GetRank(currentDNA);
				currentDNA.Rank = rankStrategy.GetRank(currentDNA);
				//Console.WriteLine("::1::"+currentDNA.Rank);
				summOfRank += currentRank;
			}
			//Console.WriteLine(summOfRank);
			//Console.ReadKey();
			for (int i = 0; i < dnaCount; i++) {
				currentDNA = _dnaList[i];
				currentRank = currentDNA.Rank;
				currentDNA.PercentRank = 1 - currentRank / summOfRank;
				//Console.WriteLine("::2::"+currentDNA.Rank);
			}

		}

		public Generation SelectionAndMerge(IDNARankStrategy rankStrategy, 
											IDNASelectionStrategy selectionStrategy, 
											IDNAMergeStrategy mergeStrategy,
											IDNAMutationStrategy mutationStrategy)
		{
			CalcRankForAllDNA(rankStrategy);

			List<DNA> parentPool = selectionStrategy.SelectParentPool(_dnaList);

			Generation nextGeneration = new Generation();

			int parentCount=parentPool.Count;
			DNA father;
			DNA mother;

			DNA child;

			for (int i = 0; i < parentCount; i+=2) {
				mother = parentPool[i];
				father = parentPool[i + 1];

				child = ComplexMerge(mother, father, mergeStrategy, mutationStrategy);
				nextGeneration.AddDNA(child);

				child = ComplexMerge(father, mother, mergeStrategy, mutationStrategy);
				nextGeneration.AddDNA(child);

			}
			
			return nextGeneration;
		}

		private DNA ComplexMerge(DNA mother, DNA father, 
								 IDNAMergeStrategy mergeStrategy, 
								 IDNAMutationStrategy mutationStrategy) {
			DNA child;
			if (GetRandom.GetNextDouble() < _mergeChance)
			{
				child = mergeStrategy.MergeDNAs(mother, father);
			}
			else
			{
				child = mother;
			}

			if (GetRandom.GetNextDouble() < _mutationChance)
			{
				child = mutationStrategy.MutateDNA(child);
			}

			return child;

		}


		public double MergeChance {
			get {
				return _mergeChance;
			}

			set {
				_mergeChance = value;
			}
		}

		public double MutationChance {
			get {
				return _mutationChance;
			}
			set {
				_mutationChance = value;
			}
		}

		public DNA BestDNA {
			get {
				if (_bestDNA == null)
				{
					double min = double.MaxValue;
					int dnaCount=_dnaList.Count;
					DNA currentDna;
					for (int i = 0; i < dnaCount; i++)
					{
						currentDna = _dnaList[i];
						//Console.WriteLine(currentDna.Rank);
						if (min > currentDna.Rank)
						{
							min = currentDna.Rank;
							_bestDNA = currentDna;
						}
					}
					if (_bestDNA == null) {
						_bestDNA = _dnaList[0];
					}

				}
				//Trace.Fail("");
				return _bestDNA;
			
			}
		}

	}
}
