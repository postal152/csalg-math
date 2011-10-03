using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csalg_math.formula.base_elements;

namespace csalg_math.formula
{
	/// <summary>
	/// 
	/// </summary>
	public class FormulaParser
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <param name="vds"></param>
		/// <param name="funcs"></param>
		/// <returns></returns>
		public static FormulaParserResult Parse(string s, VariableDataSource vds, Functions funcs) {

			InfixNotationParserResult infNPR = InfixNotationParser.Parse(s);
			if (infNPR.isError)
			{
				return new FormulaParserResult("Infix incorrected : "+infNPR.Message);
			}

			ReversePolishNotationParserResult rpnPR = ReversePolishNotationParser.Parse(infNPR);
			if (rpnPR.isError) {
				return new FormulaParserResult("Reverse polish notation is incorrected : " + rpnPR.Message);
			}
			
			List<Element> reverse = rpnPR.Result;
			List<Expression> expressions = new List<Expression>();

			for (int i = 0; i < reverse.Count; i++)
			{
				Element re = reverse[i];
				Expression temp = null;
				switch (re.Chunk) {
					case CHUNK.VARIABLE:
						temp = new Variable(re.Buffer, re.Minus, vds);
						break;
					case CHUNK.NUMBER:
						temp = Operand.GetNumber(re.Buffer);
						break;
					case CHUNK.OPERATION:
						DuoOperation opr = DuoOperation.GetOperation(re.Buffer);
						opr.Type = re.Buffer.ToCharArray()[0];
						temp = opr;
						break;
					case CHUNK.FUNCTION:

						Function f = funcs[re.Buffer];
						if (f == null) {
							return new FormulaParserResult("Unknow function " + re.Buffer);
						}

						temp = (Expression)f;

						break;
				}

				expressions.Add(temp);
			}

			DuoOperation op;
			Expression current = null;
			int iofOp = 0;

			while (expressions.Count!=1)
			{
				int iofOpT = findOperationIndex(expressions);
				int iofFuncT = findFunctionIndex(expressions);

				if (iofFuncT != -1 && iofOpT != -1) {
					iofOp = Math.Min(iofFuncT, iofOpT);
				}
				else if (iofFuncT == -1 || iofOpT == -1)
				{
					iofOp = Math.Max(iofFuncT, iofOpT);
				}

				if (iofOp == -1) {
					break;
				}

				Expression temp = expressions[iofOp];
				if (temp is DuoOperation)
				{
					op = (DuoOperation)expressions[iofOp];
					op.LeftExpression = expressions[iofOp - 2];
					op.RightExpression = expressions[iofOp - 1];
					expressions.RemoveAt(iofOp - 1);
					expressions.RemoveAt(iofOp - 2);
				}
				else if(temp is Function){
					Function func = (Function)temp;
					for (int p = 0; p < func.ParamsCount; p++) {
						func[p] = expressions[iofOp - (func.ParamsCount - p)];
					}
					
					for (int p = 0; p < func.ParamsCount; p++)
					{
						expressions.RemoveAt(iofOp - p - 1);
					}
				}
			}

			current = expressions[0];
			return new FormulaParserResult(current);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="l"></param>
		/// <returns></returns>
		private static int findFunctionIndex(List<Expression> l) {
			int result = -1;

			for (int i = 0; i < l.Count; i++)
			{
				if (l[i] is Function)
				{
					Function func = (Function)l[i];
					bool paramsClean = true;
					for (int j = 0; j < func.ParamsCount; j++)
					{

						if (func[j] != null)
						{
							paramsClean = false;
						}
					}
					
					if (paramsClean) result = i;
					break;
				}
			}

			return result;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="l"></param>
		/// <returns></returns>
		private static int findOperationIndex(List<Expression> l)
		{
			int result = -1;

			for (int i = 0; i < l.Count; i++)
			{
				if (l[i] is DuoOperation)
				{
					//Console.WriteLine("Test: DuoOp");
					DuoOperation op = (DuoOperation)l[i];
					if (op.LeftExpression == null && op.RightExpression == null)
					{
						result = i;
						break;
					}

				}
			}

			return result;
		}

	}
	
	/// <summary>
	/// 
	/// </summary>
	public class FormulaParserResult {
		private Expression expr;
		private string message = "No message";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public FormulaParserResult(Expression e) {
			expr = e;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public FormulaParserResult(string e)
		{
			message = e;
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsError { get {return (expr==null); } }

		/// <summary>
		/// 
		/// </summary>
		public string Message { get { return message; } }

		/// <summary>
		/// 
		/// </summary>
		public Expression Expression { get { return expr; } }
	}
}
