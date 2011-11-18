using System;
using csalgs.math.elementary;

/*
 * Author: Shirobok Pavel (ramshteks@gmail.com)
 * Created: 17.11.2011
 * 
 * Class description:
 * (RUS)
 * Файл содержит общий интерфейс для алгоритмов оценки плотности вероятности.
 * Реализована оценка плотности вероятности Розенблатта - Парзена
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
	public interface IProbabilityDensityValue {
		double Calculate(double[] vector);
	}

	public class RosenblattParzenAssessment : IProbabilityDensityValue
	{
		private RealMatrix data;
		private IKernel kernel;
		private double[] blurs;

		public RosenblattParzenAssessment(RealMatrix data, IKernel kernel, double[] h) {
			//TODO проверки на совместимость данных!
			this.data = data;
			this.kernel = kernel;
			blurs = h;
		}

		public double Calculate(double[] vector) {
			int i, j;
			double resultSumm = 0;
			double resultMult = 0;
			double temp;
			for (i = 0; i < data.RowCount; i++)
			{
				resultMult = 1;
				for (j = 0; j < data.ColumnCount; j++)
				{
					temp = (1.0 / blurs[j]) * kernel.Calculate((vector[j] - data[i,j]) / blurs[j]);
					resultMult *= (temp == 0 ? 1 : temp);
				}
				resultSumm += resultMult;
			}

			return resultSumm / data.RowCount;
		}
	}


}
