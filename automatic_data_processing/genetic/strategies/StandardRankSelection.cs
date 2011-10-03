using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csalg_math.utils;

namespace csalg_math.automatic_data_processing.genetic.strategies
{
	public class StandardRankSelection:IDNASelectionStrategy
	{

		public List<DNA> CalcRankForAllDNA(List<DNA> _dnaList, IDNARankStrategy rankStrategy)
		{
			int dnaCount = _dnaList.Count;
			DNA currentDNA;
			double currentRank;
			for (int i = 0; i < dnaCount; i++)
			{
				currentDNA = _dnaList[i];
				currentRank = rankStrategy.GetRank(currentDNA);
				currentDNA.Rank = currentRank;
			}
			
			Comparison<DNA> rankTest = (a, b) =>
			{

				if (a.Rank > b.Rank)
				{
					return -1;
				}

				if (a.Rank < b.Rank) {
					return 1;
				}

				if (a.Rank == b.Rank) {
					return 0;
				}
				

				return 0;
			};

			_dnaList.Sort(rankTest);
			return _dnaList;
		}


		public List<DNA> SelectParentPool(List<DNA> dnaList) {
			List<DNA> parentPool = new List<DNA>();

			int dnaCount = dnaList.Count;
			double bestPartIndex = 0.3;

			int bestPartCount = (int)Math.Floor(dnaCount * bestPartIndex);

			for (int i = 0; i < bestPartCount; i++)
			{
				parentPool.Add(dnaList[i].Copy());
			}

			return parentPool;
		}

		

	}
}
