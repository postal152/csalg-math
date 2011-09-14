using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csalg_math.utils;
using csalg_math.automatic_data_processing.genetic.strategies;

namespace csalg_math.automatic_data_processing.genetic
{
	public class GenerationController
	{

		private double _mutationChance;
		private double _mergeChance;

		private IDNAMergeStrategy _mergeStrategy;
		private IDNAMutationStrategy _mutationStrategy;
		private IDNARankStrategy _rankStrategy;
		private IDNASelectionStrategy _selectionStrategy;

		private Generation _lastGeneration;
		

		public GenerationController(double mutationChance, double mergeChance,
									IDNARankStrategy rankStrategy,
									IDNASelectionStrategy selectionStrategy,
									IDNAMergeStrategy mergeStrategy,
									IDNAMutationStrategy mutationStrategy) {
			_mutationChance = mutationChance;
			_mergeChance = mergeChance;
			_mergeStrategy = mergeStrategy;
			_mutationStrategy = mutationStrategy;
			_rankStrategy = rankStrategy;
			_selectionStrategy = selectionStrategy;

		}

		public void SetStartGeneration(Generation firstGeneration) {
			_lastGeneration = firstGeneration;
			_lastGeneration.Ranking(_rankStrategy);
		}

		public void SexThemAll() {
			/*if (_lastGeneration != null) {
				_lastGeneration.MergeChance = _mergeChance;
				_lastGeneration.MutationChance = _mutationChance;
				_lastGeneration = _lastGeneration.SelectionAndMerge(_rankStrategy, 
																	_selectionStrategy, 
																	_mergeStrategy, 
																	_mutationStrategy);
				
			}*/

			if (_lastGeneration != null)
			{
				int totalPopulationCount = _lastGeneration.DNAList.Count;
				int genesCount = (int)_lastGeneration.DNAList[0].GeneCount;

				Generation newGeneration = new Generation();

				List<DNA> parentPool = _selectionStrategy.SelectParentPool(_lastGeneration.DNAList);
				int parentsCount = parentPool.Count;

				//заношу родительский пул в новое поколение
				
				//for (int i = 0; i < parentsCount; i++) {
					//newGeneration.AddDNA(parentPool[i].Copy());
				//}

				List<DNA> newPopulation = new List<DNA>();
				for (int i = 0; i < parentsCount; i++) {
					newPopulation.Add(parentPool[i].Copy());
				}

				int randMotherIndex;
				int randFatherIndex;
				int locus;

				DNA child;

				while (true)
				{
					randMotherIndex = (int)Math.Floor(GetRandom.GetNextDouble()*parentsCount);
					randFatherIndex = (int)Math.Floor(GetRandom.GetNextDouble() * parentsCount);

					child=newPopulation[randMotherIndex];

					if (GetRandom.GetNextDouble() < _mergeChance) {
						locus = (int)Math.Floor(GetRandom.GetNextDouble() * genesCount);
						child=(_mergeStrategy.MergeDNAs(newPopulation[randMotherIndex], newPopulation[randFatherIndex], (uint)locus));
					}

					if (GetRandom.GetNextDouble() < _mutationChance) {
						child = _mutationStrategy.MutateDNA(child);
					}

					newPopulation.Add(child);

					if (newPopulation.Count == totalPopulationCount) break;
				}

				for (int i = 0; i < newPopulation.Count; i++)
				{
					newGeneration.AddDNA(newPopulation[i]);
				}

				_lastGeneration = newGeneration;
				_lastGeneration.Ranking(_rankStrategy);

			}

		}


		private DNA ComplexMerge(DNA mother, DNA father, int locus, 
								 IDNAMergeStrategy mergeStrategy,
								 IDNAMutationStrategy mutationStrategy)
		{
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

		public Generation LastGeneration {
			get {
				return _lastGeneration;
			}
		}


	}
}
