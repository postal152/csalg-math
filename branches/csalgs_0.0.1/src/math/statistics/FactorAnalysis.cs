using System;
using csalgs.math.elementary;

/*
 * Author: Shirobok Pavel (ramshteks@gmail.com)
 * Created: 17.11.2011
 * 
 * Class description:
 * (RUS)
 * Класс содержащий методы факторного анализа.
 * TODO
 * НЕ ПОДДЕРЖИВАЕТСЯ! так как еще не определен общий интерфей результатов работы
 * и не внедрено ряд методов необходимых для реализации методы главных компонент
 * 
 * (ENG)
 * 
 * License:
 * AS-IS
 * 
 * Part of:
 * csalgs project
 */

namespace csalgs.math.statistics
{
	public class FactorAnalysisResult {
		
	}

	public interface IFactorAnalysis {
		FactorAnalysisResult Calculate(RealMatrix data);
	}

	public class MainFactors : IFactorAnalysis {

		public FactorAnalysisResult Calculate(RealMatrix data)
		{
			//TODO FactorAnalysisResult
			return null;
		}
	}
}
