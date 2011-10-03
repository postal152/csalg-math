using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalg_math.formula
{
	/// <summary>
	/// Этот класс является скорее абстрактным, базовым для всех функций
	/// </summary>
	public class Function:Expression
	{
		private List<Expression> _params;

		/// <summary>
		/// Конструктор
		/// </summary>
		public Function() {
			//параметры являются выражениями и вычисляются перед вычислением самой функции смотреть реализации GetValue в
			// наследниках Function
			_params = new List<Expression>();
			//так же стоит просмотреть как реализованы все функции в Functions.cs
		}

		/// <summary>
		/// Доступ к параметрам по индексу
		/// </summary>
		/// <param name="i">индекс</param>
		/// <returns>Expression который по факту является параметром функции, это может быть как переменная и число так и сложное выражение</returns>
		public Expression this[int i]{
			get{
				return _params[i];
			}
			set {
				_params[i] = value;
			}
		}

		/// <summary>
		/// Добавляет параметр в функцию, насчет ее использования, тоже стоит заглянуть в Functions.cs
		/// </summary>
		/// <param name="e">Выражение выступающее в качестве параметра</param>
		public void AddParameter(Expression e) {
			_params.Add(e);
		}


		/// <summary>
		/// Возвращает количество необходимых для функции параметров (см. Functions.cs)
		/// </summary>
		public virtual int ParamsCount {
			get {
				return _params.Count;
			}
		}


		/// <summary>
		/// Возвращает значение функции
		/// </summary>
		/// <returns></returns>
		public override double GetValue()
		{
			return 0;// base.GetValue();
		}

		public override string ToString()
		{

			//Type f = typeof();
			string paramString = "";
			for (int i = 0; i < ParamsCount; i++) {
				paramString += _params[i].ToString() + (i < ParamsCount - 1 ? "," : "");
			}

			return this.GetType().Name+"("+paramString+")";//base.ToString();
		}

	}
}
