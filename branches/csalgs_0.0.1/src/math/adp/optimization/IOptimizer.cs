using System;
using csalgs.math.elementary;

namespace csalgs.math.adp
{
	public interface IOptimizer
	{

		bool HasMoreSteps
		{
			get;
		}

		void NextStep();

		double Score
		{
			get;
		}

		Vector LastVector
		{
			get;
		}

	}
}
