using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalg_math.formula.base_elements
{
	/// <summary>
	/// Базовый класс операнда, используется для переменных и чисел
	/// </summary>
	public class Operand:Expression
	{
		/// <summary>
		/// Зачем-то давно сделано, хрен с ним пусть валяется.
		/// Меня мало интересует ваше мнение насчет наличия матов в коде ;)
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static Operand GetNumber(string p)
		{
			return new Number(p);
		}
	}

	/// <summary>
	/// Класс переменной
	/// </summary>
	public class Variable : Operand{
		private string _name;
		private bool _minus;
		private VariableDataSource _source;

		/// <summary>
		/// Конструктор, инициализирующий переменную
		/// </summary>
		/// <param name="name">Название</param>
		/// <param name="minus">Означает, что перед переменной стоял минус не как операция, а как изменение знака</param>
		/// <param name="s">Источник значений переменных</param>
		public Variable(string name, bool minus, VariableDataSource s)
		{
			_name = name;
			_minus = minus;
			_source = s;
		}

		/// <summary>
		/// Возвращает название переменной, ее имя
		/// </summary>
		public string Name { get { return _name; } }

		/// <summary>
		/// Предоставляет достут до источника данных использующегося в переменной
		/// </summary>
		public VariableDataSource Source { get { return _source; } }

		/// <summary>
		/// Используется ли изменение знака переменной
		/// </summary>
		public bool Minus { get { return _minus; } }

		/// <summary>
		/// Возвращает значение, в данном случае это значение из источника данных
		/// </summary>
		/// <returns></returns>
		public override double GetValue()
		{
			double res = _source.GetByName(_name) * (_minus == true ? -1 : 1);
			return res;
		}

		/// <summary>
		/// Возвращает string представление
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return (_minus == true ? "-" : "") + _name;
		}

	}

	/// <summary>
	/// Класс представляющий собой число. Да кэп.
	/// </summary>
	public class Number : Operand
	{
		private string strValue = "0";
		private double doubleValue = 0;
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="value">строковое представление числа</param>
		public Number(string value)
		{
			doubleValue = Double.Parse((strValue = value.Replace('.', ',')));
		}

		/// <summary>
		/// Возвращает значение числа
		/// </summary>
		/// <returns></returns>
		public override double GetValue()
		{
			return doubleValue;
		}

		public override string ToString()
		{
			return doubleValue.ToString();
			//return base.ToString();
		}

	}

}
