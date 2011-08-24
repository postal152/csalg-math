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

	//	private double _mutationChance = 0.1;
		//private double _mergeChance = 0.5;
		
		public Generation() {
			_dnaList = new List<DNA>();
		}
		
		~Generation(){}

		public List<DNA> DNAList {
			get {
				return _dnaList;
			}
		}


		public void AddDNA(DNA dna){
			if (dna != null) {
				_dnaList.Add(dna);
			}
		}

		public void Ranking(IDNARankStrategy rankStrategy) {
			int count=_dnaList.Count;
			DNA dna;
			for (int i = 0; i < count; i++) {
				dna=_dnaList[i];
				dna.Rank = rankStrategy.GetRank(dna);
			}

			Comparison<DNA> rankTest = (a, b) =>
			{

				if (a.Rank > b.Rank)
				{
					return -1;
				}

				if (a.Rank < b.Rank)
				{
					return 1;
				}

				if (a.Rank == b.Rank)
				{
					return 0;
				}


				return 0;
			};

			_dnaList.Sort(rankTest);
		}


		/*public Generation SelectionAndMerge(IDNARankStrategy rankStrategy, 
											IDNASelectionStrategy selectionStrategy, 
											IDNAMergeStrategy mergeStrategy,
											IDNAMutationStrategy mutationStrategy)
		{
			_dnaList = selectionStrategy.CalcRankForAllDNA(_dnaList, rankStrategy);

			List<DNA> parentPool = selectionStrategy.SelectParentPool(_dnaList);

			parentPool.GroupBy(x => GetRandom.GetNextDouble());
			parentPool.GroupBy(x => GetRandom.GetNextDouble());

			Generation nextGeneration = new Generation();

			int parentCount = parentPool.Count;

			if (parentCount % 2 != 0) {
				parentCount -= 1;
			}
			
			DNA father;
			DNA mother;

			DNA child;

			/*for (int i = 0; i < parentCount; i += 2)
			{
			int i = 0;
			while (nextGeneration.DNAList.Count > DNAList.Count-1)
			{
				try
				{
				mother = parentPool[i];
				
					father = parentPool[i + 1];
				}
				catch (Exception e) {
					break;
				}
				int locus = (int)Math.Floor(GetRandom.GetNextDouble() * (double)(mother.GeneCount));
				child = ComplexMerge(mother, father, locus, mergeStrategy, mutationStrategy);
				nextGeneration.AddDNA(child);

				child = ComplexMerge(father, mother, locus, mergeStrategy, mutationStrategy);
				nextGeneration.AddDNA(child);
				i++;
			}
			
			//}

			for (i = 0; i < parentCount; i++) {
				nextGeneration.AddDNA(parentPool[i].Copy());
			}

			
			
			return nextGeneration;
		}
*/
		/*private DNA ComplexMerge(DNA mother, DNA father, int locus,
								 IDNAMergeStrategy mergeStrategy, 
								 IDNAMutationStrategy mutationStrategy) {
			DNA child;
			if (GetRandom.GetNextDouble() < _mergeChance)
			{
				child = mergeStrategy.MergeDNAs(mother, father, (uint)locus);
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
		}*/

		public DNA BestDNA
		{
			get {
				if (_bestDNA==null)
				{
					double max = double.MinValue;
					int dnaCount=_dnaList.Count;
					DNA currentDna;
					for (int i = 0; i < dnaCount; i++)
					{
						currentDna = _dnaList[i];
						//Console.WriteLine(currentDna.Rank);
						if (max < currentDna.Rank)
						{
							max = currentDna.Rank;
							_bestDNA = currentDna;
						}
					}
					if (_bestDNA == null) {
						//_bestDNA = _dnaList[0];
					}

				}
				//Trace.Fail("");
				return _bestDNA;
			
			}
		}

	}
}
