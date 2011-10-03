using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csalg_math.utils;
namespace csalg_math.automatic_data_processing.genetic.strategies
{
	public class StandardMergeStrategy:IDNAMergeStrategy
	{
		public DNA MergeDNAs(DNA motherDna, DNA father, uint locus) {
			uint geneCount = motherDna.GeneCount;

			DNA child = new DNA(geneCount);

			//int locus = locus;//(int)Math.Ceiling(GetRandom.GetNextDouble() * (double)(geneCount));

			int firstPart = (int)locus;
			int secondPart = (int)geneCount - (int)locus;

			for (int i = 0; i < firstPart; i++) {
				child[i] = motherDna[i];
			}

			for (int i = firstPart; i < geneCount; i++) {
				child[i] = father[i];
			}
			//child[0] = motherDna[0];
			//child[1] = motherDna[1];

			return child;
		}
	}
}
