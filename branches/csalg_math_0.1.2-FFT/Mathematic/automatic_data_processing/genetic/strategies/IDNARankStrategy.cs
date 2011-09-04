using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathematic.automatic_data_processing.genetic.strategies
{
	public interface IDNARankStrategy
	{
		double GetRank(DNA dna);
	}
}
