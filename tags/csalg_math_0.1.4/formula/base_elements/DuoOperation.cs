using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csalg_math.formula;
using csalg_math.formula.base_elements;

namespace csalg_math.formula.base_elements{

	/// <summary>
	/// Представляет собой бинарную операцию, для которой требуется два оператора
	/// </summary>
	public abstract class DuoOperation:Expression
	{
		protected Expression left;
		protected Expression right;
		protected char type;

		/// <summary>
		/// Конструктор
		/// </summary>
		public DuoOperation():base() {}

		/// <summary>
		/// Тип, вроде как не использует но пусть будет, че =)
		/// </summary>
		public char Type {
			get { return type; }
			set { type = value; }
		}

		/// <summary>
		/// Это левое выражение которое использует для вычисления значения
		/// </summary>
		public Expression LeftExpression{
			get { return left; }
			set { left = value; }
		}

		/// <summary>
		/// Это правое выражение которое использует для вычисления значения
		/// </summary>
		public Expression RightExpression
		{
			get { return right; }
			set { right = value; }
		}

		/// <summary>
		/// Возвращает объект операции в зависимости от строкового представления этой операции
		/// </summary>
		/// <param name="p">операция</param>
		/// <returns>Объект реализующий эту операцию</returns>
		public static DuoOperation GetOperation(string p)
		{
			DuoOperation result = null;
			switch (p) {
				case "*": result = new MultipyOperation();break;
				case "/": result = new DivisionOperation() ;break;
				case "+": result = new PlusOperation() ;break;
				case "-": result = new MinusOperation() ;break;
			}
			return result;
		}

		public override string ToString()
		{
			return Type.ToString();
			//return base.ToString();
		}
	}

	///Описание всех методов смотреть в базовом классе DuoOperation


	/// <summary>
	/// Деление
	/// </summary>
	public class DivisionOperation : DuoOperation
	{

		public DivisionOperation()
			: base()
		{
			Type = '/';
		}

		public override double GetValue()
		{

			return left.GetValue() / right.GetValue();
		}
	}

	/// <summary>
	/// Вычитание
	/// </summary>
	public class MinusOperation : DuoOperation
	{
		public MinusOperation()
			: base()
		{
			Type = '-';
		}

		public override double GetValue()
		{

			return left.GetValue() - right.GetValue();

			//return base.GetValue();
		}

	}

	/// <summary>
	/// Умножение
	/// </summary>
	public class MultipyOperation : DuoOperation
	{
		public MultipyOperation()
			: base()
		{
			Type = '*';
		}

		public override double GetValue()
		{

			return left.GetValue() * right.GetValue();

			//return base.GetValue();
		}

	}

	/// <summary>
	/// Сложение
	/// </summary>
	public class PlusOperation : DuoOperation
	{

		public PlusOperation()
			: base()
		{
		}


		public override double GetValue()
		{
			return left.GetValue() + right.GetValue();
		}

	}

	/* не стоит учитывать был добавлен экспериментально и так же экспериментально исключен, так как он не нужен
	public class EqualOperation : DuoOperation {
		public EqualOperation()
			: base()
		{
			Type = '+';
		}


		public override double GetValue()
		{
			Variable vars = (Variable)left;

			vars.Source.SetByName(vars.Name, right.GetValue());

			return vars.GetValue();//left.GetValue() + right.GetValue();

			//return base.GetValue();
		}
	}
	*/
}
