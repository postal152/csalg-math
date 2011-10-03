using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeFromula.formula
{
	public class ReversePolishNotationParser
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		public static ReversePolishNotationParserResult Parse(InfixNotationParserResult result) {
			if (result == null) {
				return new ReversePolishNotationParserResult("InfixNotationParserResult instance is null!");
			}

			if (result.isError) {
				return new ReversePolishNotationParserResult("InfixNotationParserResult instance with error!");
			}

			Stack<Element> stack = new Stack<Element>();
			List<Element> output = new List<Element>();
			List<Element> chunks = result.Result;
			for (int i = 0; i < chunks.Count; i++)
			{

				if (chunks[i].Chunk == CHUNK.NUMBER || chunks[i].Chunk == CHUNK.VARIABLE)
				{
					output.Add(chunks[i]);
				}

				if (chunks[i].Chunk == CHUNK.FUNCTION)
				{
					stack.Push(chunks[i]);
				}

				if (chunks[i].Chunk == CHUNK.ZPTAYA) {
					bool wasLP = false;
					while (stack.Count > 0) {
						if (stack.Peek().Chunk == CHUNK.LPARENTS){
							wasLP = true;
							break;
						};
						output.Add(stack.Pop());
					}
					if (wasLP == false) {
						return new ReversePolishNotationParserResult("Mismatch , or (");
					}
				}

				if (chunks[i].Chunk == CHUNK.LPARENTS)
				{
					stack.Push(chunks[i]);
				}

				if (chunks[i].Chunk == CHUNK.RPARENTS)
				{
					while (stack.Count > 0)
					{
						if (stack.Peek().Chunk != CHUNK.LPARENTS)
						{
							output.Add(stack.Pop());
						}
						else
						{
							stack.Pop();
							break;
						}
					}


					if (stack.Peek().Chunk == CHUNK.FUNCTION)
					{
						output.Add(stack.Pop());
					}
				}

				if (chunks[i].Chunk == CHUNK.OPERATION)
				{
					Element op1 = chunks[i];
					Element op2 = null;// stack.Peek();

					while (stack.Count > 0)
					{
						op2 = stack.Peek();
						if (op2.Chunk != CHUNK.OPERATION) break;
						if ((op1.IsLeftAssociate && op1.Priority <= op2.Priority) || (!op1.IsLeftAssociate && op1.Priority < op2.Priority))
						{
							output.Add(stack.Pop());
						}
						else
						{
							break;
						}
					}
					//double f = 2 / -2;
					stack.Push(op1);
				}
			}

			while (stack.Count > 0) output.Add(stack.Pop());

			return new ReversePolishNotationParserResult(output);
		}

	}

	/// <summary>
	/// 
	/// </summary>
	public class ReversePolishNotationParserResult {
		private string message = "No message";
		private List<Element> result;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public ReversePolishNotationParserResult(string message)
		{
			this.message = message;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="res"></param>
		public ReversePolishNotationParserResult(List<Element> res)
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
		public string Message
		{
			get { return message; }
		}

		/// <summary>
		/// 
		/// </summary>
		public List<Element> Result { get { return result; } }

		
	}

}
