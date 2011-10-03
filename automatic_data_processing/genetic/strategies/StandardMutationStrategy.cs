using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csalg_math.utils;

namespace csalg_math.automatic_data_processing.genetic.strategies
{
	public class StandardMutationStrategy:IDNAMutationStrategy
	{

		public DNA MutateDNA(DNA dna) {
			int genesCount = (int)dna.GeneCount;

			int randomGeneIndex = (int)Math.Floor(GetRandom.GetNextDouble() * (double)genesCount);
			double shift = (0.05)*Math.Abs(GetRandom.GetNextDouble());

			if (GetRandom.GetNextDouble() > 0.5) {
				shift = -shift;
			}
			//Console.WriteLine(dna.Rank);
			dna[randomGeneIndex] += shift;

			return dna;
		}

	}
}
