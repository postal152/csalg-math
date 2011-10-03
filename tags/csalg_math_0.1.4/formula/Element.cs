using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalg_math.formula
{
	
	/// Тут перечисления и класс необходимые для промежуточного парсинга инфиксной записи

	/// <summary>
	/// Это чанки. Вот =)
	/// 
	/// </summary>
	public enum CHUNK
	{
		NUMBER, OPERATION, FUNCTION, VARIABLE, LPARENTS, RPARENTS, UNKNOWN,
		ZPTAYA, EQUAL
	};

	/// <summary>
	/// Это сложный элемент хранящий чанк и сам буфер который был распознан как один из чанков.
	/// </summary>
	public class Element
	{

		public const char LP = '(';
		public const char RP = ')';
		public const char PLUS = '+';
		public const char MINUS = '-';
		public const char MULTI = '*';
		public const char DIVIS = '/';
		public const char POWER = '^';
		public const char FLP = '(';
		public const char FRP = ')';
		public const char ZPT = ',';
		public const char SPACE = ' ';
		public const char EQUAL = '=';

		private CHUNK whis;
		private string buff;
		private int priority;
		private bool isLeft;
		private bool minus = false;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="w">тут тип записи</param>
		/// <param name="b">сама запись</param>
		public Element(CHUNK w, string b)
		{
			
			whis = w;
			buff = b;
			priority = 1;
			if (w == CHUNK.OPERATION)
			{
				isLeft = true;
				if (b == PLUS.ToString() || b == MINUS.ToString())
				{
					priority = 2;
				}
				if (b == DIVIS.ToString() || b == MULTI.ToString())
				{
					priority = 3;
				}
			}

			if (w == CHUNK.EQUAL) {
				priority = 0;
			}

			if (w == CHUNK.FUNCTION)
			{
				isLeft = false;
				priority = 4;

				if (b[0] == '-' || b[0] == '+')
				{

					minus = (b[0] == '-');
					//Console.WriteLine(w + " " + b + " "+minus) ;
					buff = buff.Substring(1);
				}

			}

			if (w == CHUNK.VARIABLE) {
				
				if (b[0] == '-' || b[0] == '+') {
					
					minus = (b[0] == '-');
					//Console.WriteLine(w + " " + b + " "+minus) ;
					buff = buff.Substring(1);
				}
			}
		}

		/// <summary>
		/// Если это операция или функция, то содержит информацию о том лево или правоассоциирован этот эелемент
		/// </summary>
		public bool IsLeftAssociate { get { return isLeft; } }

		/// <summary>
		/// Приоритет операции
		/// </summary>
		public int Priority { get { return priority; } }

		/// <summary>
		/// Говорит о том был ли минус меняющий знак
		/// </summary>
		public bool Minus { get { return minus; } }

		/// <summary>
		/// Копирует этот элемент
		/// </summary>
		/// <returns></returns>
		public Element Copy()
		{
			return new Element(whis, buff);
		}

		/// <summary>
		/// Доступ к перечислению с типом чанка
		/// </summary>
		public CHUNK Chunk
		{
			get
			{
				return whis;
			}
		}

		/// <summary>
		/// Буффер (.)(.)
		/// </summary>
		public string Buffer
		{
			get
			{
				return buff;
			}
		}

		public override string ToString()
		{
			return (minus == true ? "-" : "") + buff;//base.ToString();
		}

	}
}
