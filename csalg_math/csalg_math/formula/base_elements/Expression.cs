using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalg_math.formula
{
	/// <summary>
	/// Представляет собой такую сущность как выражение
	/// Этот класс нуждается в переопределении
	/// </summary>
	public abstract class Expression
	{

		/// <summary>
		/// Возвращает значение этого выражения
		/// </summary>
		/// <returns></returns>
		public virtual double GetValue() {
			return 0;
		}
	}
}
