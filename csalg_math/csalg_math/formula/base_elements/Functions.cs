using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using csalg_math.formula.base_elements;


namespace csalg_math.formula
{
	/// <summary>
	/// Класс источник реализаций функций, которые будет понимать FormulaParser
	/// </summary>
	public class Functions {

		private Dictionary<String, Type> _list;

		/// <summary>
		/// Конструктор
		/// </summary>
		public Functions() {
			_list = new Dictionary<String, Type>();
		}

		/// <summary>
		/// Предоставляет доступ к функциям добавленным в этот источник
		/// </summary>
		/// <param name="funcName">по текстовому имени</param>
		/// <returns>Возвращает экземпляр Function</returns>
		public Function this[string funcName] {
			get {

				Function f = null;
				Type t = null;
				if (_list.ContainsKey(funcName)) {
					t = _list[funcName];
				}
				if (t == null) return null;

				f = (Function)Assembly.GetExecutingAssembly().CreateInstance(t.FullName);
				if (f == null) return null;


				return f;
			}
		}
		
		/// <summary>
		/// Добавляет в источник новую функцию
		/// </summary>
		/// <param name="name">название функции</param>
		/// <param name="func">Ссылка на тип</param>
		public void AddFunction(string name, Type func) {
			if (_list.ContainsKey(name))
			{
				throw new InvalidOperationException("Func registered!");
			}
			else {
				_list.Add(name, func);
			}
		}
		
		/// <summary>
		/// Эта функция просто добавляет в источник стандартные математические функции
		/// </summary>
		public void CreateStandardFunctionsList() {
			_list = new Dictionary<String, Type>();
			_list.Add("pow", typeof(PowFunc));
			_list.Add("answer", typeof(AnswerFunc));
			_list.Add("sin", typeof(SinFunc));
			_list.Add("cos", typeof(CosFunc));
			_list.Add("sqrt", typeof(SqrtFunc));
			_list.Add("exp", typeof(ExpFunc));
		}
	}
}

namespace csalg_math.formula.base_elements {

	/// <summary>
	/// Реализует функцию экспоненты, назначение остальных методов смотреть в Function
	/// </summary>
	public class ExpFunc : Function
	{
		public ExpFunc()
			: base()
		{
			AddParameter(null);
		}

		public override int ParamsCount
		{
			get
			{
				return 1;// base.ParamsCount;
			}
		}

		public override double GetValue()
		{
			return Math.Exp(this[0].GetValue());// base.GetValue();
		}

	}

	/// <summary>
	/// Возведение в степень
	/// </summary>
	public class PowFunc : Function
	{
		public PowFunc()
			: base()
		{
			AddParameter(null);
			AddParameter(null);
		}

		public override int ParamsCount
		{
			get
			{
				return 2;
			}
		}

		public override double GetValue()
		{
			return Math.Pow(this[0].GetValue(), this[1].GetValue());//base.GetValue();
		}
	}

	/// <summary>
	/// Квадратный корень
	/// </summary>
	public class SqrtFunc : Function
	{
		public SqrtFunc()
			: base()
		{
			AddParameter(null);
		}

		public override int ParamsCount
		{
			get
			{
				return 1;
			}
		}

		public override double GetValue()
		{
			return Math.Sqrt(this[0].GetValue());//base.GetValue();
		}
	}

	/// <summary>
	/// Синус
	/// </summary>
	public class SinFunc : Function
	{
		public SinFunc()
			: base()
		{
			AddParameter(null);
		}

		public override int ParamsCount
		{
			get
			{
				return 1;
			}
		}

		public override double GetValue()
		{
			return Math.Sin(this[0].GetValue());//base.GetValue();
		}
	}

	/// <summary>
	/// Косинус
	/// </summary>
	public class CosFunc : Function
	{
		public CosFunc()
			: base()
		{
			AddParameter(null);
		}

		public override int ParamsCount
		{
			get
			{
				return 1;
			}
		}

		public override double GetValue()
		{
			return Math.Cos(this[0].GetValue());//base.GetValue();
		}
	}

	/// <summary>
	/// Ответ на главный вопрос вселенной, жизни и всего такого
	/// </summary>
	public class AnswerFunc : Function
	{
		public AnswerFunc()
			: base()
		{
			AddParameter(null);
		}

		public override int ParamsCount
		{
			get
			{
				return 1;
			}
		}

		public override double GetValue()
		{
			return 42;//base.GetValue();
		}
	}



}
