using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalg_math.automatic_data_processing.genetic.strategies
{
	public interface IDNAMergeStrategy
	{
		DNA MergeDNAs(DNA motherDna, DNA father, uint locus);
	}
}
