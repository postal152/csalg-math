using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalg_math.formula
{
	/// <summary>
	/// 
	/// </summary>
	public class VariableDataSource
	{
		private Dictionary<String, Double> _vars;
		private Dictionary<String, Double> _constants;

		/// <summary>
		/// 
		/// </summary>
		public VariableDataSource() {
			_vars = new Dictionary<string, double>();
			_constants = new Dictionary<string, double>();
			_constants.Add("PI", Math.PI);
			_constants.Add("E", Math.E);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void SetByName(string name, double value) {
			if (_constants.ContainsKey(name)) {
				return;
			}

			if (_vars.ContainsKey(name) == false)
			{
				_vars.Add(name, value);
			}
			else {
				_vars[name] = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public double GetByName(string name) {
			if (_constants.ContainsKey(name)) {
				return _constants[name];
			}

			if (_vars.ContainsKey(name) == false) {
				return 0;
			}
			return _vars[name];
		}

	}
}
