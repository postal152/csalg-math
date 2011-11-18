using System;
using System.Collections;

namespace csalgs.patterns
{
	public delegate void Method(INamed obj);

	public class Strategy
	{
		private Hashtable handlers;
		

		public Strategy() {
			handlers = new Hashtable();
		}

		public void Execute(INamed arg) {
			
			if (handlers.ContainsKey(arg.Name)) {
				Method m = (Method)handlers[arg.Name];
				m(arg);
			}
		}

		public void RegisterMethod(String name, Method method) {
			if (method == null) throw new ArgumentNullException("method is null");

			if (!handlers.ContainsKey(name)) {
				handlers.Add(name, method);
			}

		}

		public void UnregisterMethod(String name) {
			if (handlers.ContainsKey(name)) {
				handlers.Remove(name);
			}
		}
	}
}
