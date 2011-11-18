using System;

namespace csalgs.patterns
{

	public interface INamed{
		string Name{
			get;
		}
	}

	public interface INotify:INamed {
		string Target
		{
			get;
		}
	}

	public class Notify:INotify {

		private string name;
		private string target = "";

		public Notify(string name) {
			this.name = name;
		}

		public Notify(string name, string target) {
			this.name = name;
			this.target = target;
		}

		public string Target
		{
			get { return target; }
		}

		public string Name
		{
			get { return name; }
		}
	}

	public delegate void MediatorEvent();

	public interface INotifier:INamed 
	{
		void sendNotify(INotify notify);
		void recvNotify(INotify notify);
		
		Strategy Strategy{
			get;
		}

		MediatorEvent onRegistered{
			get;
			set;
		}

		MediatorEvent onUnregistered
		{
			get;
			set;
		}

	}

	public class Named:INamed {
		private string name;
		public Named(String name){
			this.name = name;
		}

		public string Name
		{
			get { return name; }
		}
	}

	public interface IMethod {
		void Execute(INamed arg);
	}
}
