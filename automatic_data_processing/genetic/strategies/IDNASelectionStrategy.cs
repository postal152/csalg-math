using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalg_math.automatic_data_processing.genetic.strategies
{
	public interface IDNASelectionStrategy
	{
		List<DNA> SelectParentPool(List<DNA> dnaList);
		List<DNA> CalcRankForAllDNA(List<DNA> _dnaList, IDNARankStrategy rankStrategy);
	}
}
