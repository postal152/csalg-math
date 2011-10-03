using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeFromula.formula
{
	/// <summary>
	/// Класс для парсинга математической формулы в инфиксной записи
	/// </summary>
	public class InfixNotationParser
	{
		private const string picard2 = "   .-'---`-.\n,'          `.\n|             \\\n|              \\\n\\           _  \\\n,\\  _    ,'-,/-)\\\n( * \\ \\,' ,' ,'-)\n `._,)     -',-')\n  \\/         ''/\n    )        / /\n   /       ,'-'";
		private const char VAR_START = '@';
		private const char FUN_START = '#';

		/// <summary>
		/// Возвращает лист с уже разделенными элементами строковой последовательности
		/// с определенными типами элемента Element
		/// </summary>
		/// <param name="s">Строка содержащая матеметическое выражение</param>
		/// <returns>Объект содержащий либо результат или информацию об ошибке</returns>
		public static InfixNotationParserResult Parse(string s) {
			List<Element> chunks = new List<Element>();
			s = ClearWhites(s);
			//Console.WriteLine("Source "+s);
			int len = s.Length;
			if (len == 0) {
				return new InfixNotationParserResult("Input string is ''");
			}
			char c;
			int parentsCount = 0;
			CHUNK whatIsReading = CHUNK.UNKNOWN;

			for (int i = 0; i < len; i++) {
				c = s[i];
				whatIsReading = CHUNK.UNKNOWN;

				#region Проверка операции - и + на операция это или принадлежит к операнду или функции
				if (c == Element.MINUS || c == Element.PLUS) {
					Element lastElement;
					CHUNK lastChunk;
					if (chunks.Count > 0)
					{
						//Список токенов не пуст
						lastElement = chunks[chunks.Count - 1];
						lastChunk = lastElement.Chunk;

						if (lastChunk == CHUNK.LPARENTS ||
							(lastChunk == CHUNK.OPERATION && (lastElement.Buffer == Element.MULTI.ToString() || lastElement.Buffer == Element.DIVIS.ToString())))
						{
							//Прочитаный - или + стоит либо после ( либо перед * или /
							if (i == len - 1) return new InfixNotationParserResult("Unexpected " + c + " in " + i);

							if (isNumber(s[i + 1]))
							{
								whatIsReading = CHUNK.NUMBER;
							}
							else if (VAR_START == s[i + 1])
							{
								whatIsReading = CHUNK.VARIABLE;
							}
							else if (FUN_START == s[i + 1])
							{
								whatIsReading = CHUNK.FUNCTION;
							}
							else
							{
								return new InfixNotationParserResult("Unexpected symbol after " + c + " in sign context: " + s[i + 1]);
							}
						}
						else
						{
							if (i == len - 1 || i==0) return new InfixNotationParserResult("Unexpected operation '" + c + "' in " + i);
							if (lastChunk == CHUNK.OPERATION) return new InfixNotationParserResult("Unexpected operation '" + c + "' in " + i);
							whatIsReading = CHUNK.OPERATION;
						}
					}
					else
					{
						//Список токенов пустой
						if (i == len - 1) return new InfixNotationParserResult("Unexpected " + c + " in " + i);

						if (isNumber(s[i + 1]))
						{
							whatIsReading = CHUNK.NUMBER;
						}
						else if (VAR_START == s[i + 1])
						{
							whatIsReading = CHUNK.VARIABLE;
						}
						else if (FUN_START == s[i + 1])
						{
							whatIsReading = CHUNK.FUNCTION;
						}
						else
						{
							return new InfixNotationParserResult("Unexpected symbol after " + c + " in sign context: " + s[i + 1]);
						}
					}

				}
				#endregion

				#region Проверка операции * и /
				if (c == Element.DIVIS || c == Element.MULTI) {

					if (i == len - 1 || i == 0) return new InfixNotationParserResult("Unexpected operation '" + c + "' in " + i);

					if (chunks.Count > 0 && (chunks[chunks.Count - 1].Chunk == CHUNK.OPERATION)) return new InfixNotationParserResult("Unexpected operation '" + c + "' in " + i);

					whatIsReading = CHUNK.OPERATION;
				}
				#endregion

				#region Проверка на число
				if (isNumber(c))
				{
					whatIsReading = CHUNK.NUMBER;
				} 
				#endregion

				#region Переменные, функции, разделители, ),(
				if (isVar(c))
				{
					whatIsReading = CHUNK.VARIABLE;
				}

				if (isFunc(c))
				{
					whatIsReading = CHUNK.FUNCTION;
				}

				if (c == Element.ZPT)
				{
					whatIsReading = CHUNK.ZPTAYA;
				}

				if (c == Element.LP)
				{
					whatIsReading = CHUNK.LPARENTS;
					parentsCount++;
				}
				if (c == Element.RP)
				{
					whatIsReading = CHUNK.RPARENTS;
					parentsCount--;
				}
				#endregion

				#region Somecool things
				if (parentsCount < 0)
				{
					return new InfixNotationParserResult("Unexpected ) at " + i);
				}

				if (c == '$')
				{
					Console.Beep(2000, 500);
					return new InfixNotationParserResult("It's not fucking php! IT'S NOT FUCKING PHP!!!\n" + picard2 + "\n" + "facepalm.jpg");
				} 
				#endregion

				switch (whatIsReading) {
					#region Чтение числа
					case CHUNK.NUMBER:
						string operand = c.ToString();
						i++;
						bool pointBeen = false;
						while (i <= len - 1 && (isNumber(s[i]) || s[i] == '.'))
						{
							if (s[i] == '.' && pointBeen) return new InfixNotationParserResult("Unexpected point in number at " + i);


							if (s[i] == '.') pointBeen = true;


							if (i == len - 2 && (s[i + 1] == '.'))
							{
								return new InfixNotationParserResult("Unexpected point at " + i);
							}
							else if (i == len - 3 && (s[i + 1] == '.' && !isNumber(s[i + 2])))
							{
								return new InfixNotationParserResult("Unexpected point in end of number at " + i);
							}

							operand += s[i].ToString();
							i++;
						}

						i--;
						chunks.Add(new Element(CHUNK.NUMBER, operand));
						break; 
					#endregion

					#region Variable
					case CHUNK.VARIABLE:
						string var_name = "";
						
						if ((i + 1) == len) return new InfixNotationParserResult("Unexpected end of var_name");

						if (c == '-' || c == '+') {
							var_name = c.ToString();
							i++;
						}

						i++;

						while (i <= len - 1)
						{
							char curr_vn = s[i];
							if (!isOperation(curr_vn) &&
								curr_vn != Element.ZPT &&
								curr_vn != Element.RP)
							{
								var_name += curr_vn.ToString();
								i++;
							}
							else if (curr_vn == Element.LP)
							{
								return new InfixNotationParserResult("Unexpected ( at " + i);
							}
							else
							{
								i--;
								break;
							}
						}

						if (var_name.Length>0 && (!CheckIdentifier(var_name))) return new InfixNotationParserResult("Incorrect identifier name: " + var_name + " at " + i);
						
						chunks.Add(new Element(CHUNK.VARIABLE, var_name));
						break; 
					#endregion

					#region Func
					case CHUNK.FUNCTION:
						string func_name = "";

						if ((i + 1) == len) return new InfixNotationParserResult("Unexpected end of func_name");

						if (c == '-' || c == '+')
						{
							func_name = c.ToString();
							i++;
						}

						i++;

						while (i <= len - 1)
						{
							char curr_vn = s[i];
							if (curr_vn != Element.LP)
							{
								if (!CheckIdentifier(curr_vn.ToString())) {
									return new InfixNotationParserResult("Unexpected symbol in '" + func_name + "' at " + i);
								}


								func_name += curr_vn.ToString();
								i++;
							}
							else if (curr_vn == Element.LP)
							{
								i--;
								break;
							}
							else
							{
								return new InfixNotationParserResult("Incorrect symbol after " + func_name + " at " + i);
							}
						}

						if (func_name.Length > 0 && (!CheckIdentifier(func_name))) return new InfixNotationParserResult("Incorrect identifier name: " + func_name + " at " + i);

						chunks.Add(new Element(CHUNK.FUNCTION, func_name));

						break; 
					#endregion

					#region Zpt, oper, parents
					case CHUNK.EQUAL:
					case CHUNK.LPARENTS:
					case CHUNK.RPARENTS:
					case CHUNK.ZPTAYA:
					case CHUNK.OPERATION:
						chunks.Add(new Element(whatIsReading, c.ToString()));
						break; 
					#endregion

					#region Unknow
					case CHUNK.UNKNOWN:
						return new InfixNotationParserResult("What's the fuck!? Are you, crazy? Unknown sequence'" + c + "' at " + i);
					#endregion
				}
							
			}

			if (parentsCount != 0) {
				return new InfixNotationParserResult("Parentheses are not compatible");
			}
			
			return new InfixNotationParserResult(chunks);
		}

		/// <summary>
		/// Удаляет лишние пробелы и переносы
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private static string ClearWhites(string s)
		{
			string res = s.Replace("\t", "");
			res = res.Replace("\n", "");
			res = res.Replace(" ", "");
			return res;
		}

		/// <summary>
		/// Сообщает относится ли переданный символ к оперции
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private static bool isOperation(char c)
		{
			if ("+/*-".IndexOf(c) != -1) return true;
			return false;
		}

		/// <summary>
		/// Сообщает относится ли переданный символ к числу
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private static bool isNumber(char c)
		{
			if ("01234567890".IndexOf(c) != -1) return true;
			return false;
		}

		/// <summary>
		/// проверяет прочитанный идинтификатор на корректность
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private static bool CheckIdentifier(string s) {
			if (isNumber(s[0])) return false;
			for (int i = 0; i < s.Length; i++)
			{
				//Console.WriteLine(s[i]);
				if ("%$#@!^&.?/\\}{[]()'\"|<>№:;=".IndexOf(s[i].ToString()) != -1) return false;
			}
			
			return true;
		}

		/// <summary>
		/// Является ли символ началом переменной
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private static bool isVar(char c)
		{
			if (VAR_START == c) return true;
			return false;
		}

		/// <summary>
		/// Является ли символ началом названия функции
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private static bool isFunc(char c)
		{
			if (FUN_START == c) return true;
			return false;
		}

	}

	/// <summary>
	/// Класс для хранения результата работы  InfixNotationParser
	/// </summary>
	public class InfixNotationParserResult {

		private string message="No message";
		private List<Element> result;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public InfixNotationParserResult(string message) {
			this.message = message;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="res"></param>
		public InfixNotationParserResult(List<Element> res)
		{
			result = res;
		}

		/// <summary>
		/// 
		/// </summary>
		public bool isError { get { return result == null ? true : false; } }

		/// <summary>
		/// 
		/// </summary>
		public string Message {
			get { return message; }
		}

		/// <summary>
		/// 
		/// </summary>
		public List<Element> Result { get { return result; } }

	}

}
