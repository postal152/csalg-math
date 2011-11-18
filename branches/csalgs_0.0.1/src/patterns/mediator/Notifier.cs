using System;

namespace csalgs.patterns
{
	public class Notifier:INotifier
	{
		private string name;
		private Strategy strategy;

		private MediatorEvent onRegEvent;
		private MediatorEvent onUnRegEvent;

		public Notifier(string name) {
			this.name = name;
			strategy = new Strategy();
		}

		public void sendNotify(INotify notify)
		{
			Mediator.sendNotify(notify);
		}

		public void recvNotify(INotify notify)
		{
			Strategy.Execute(notify);
		}

		public Strategy Strategy
		{
			get { return strategy; }
		}

		public MediatorEvent onRegistered
		{
			get
			{
				return onRegEvent;
			}
			set
			{
				onRegEvent = value;
			}
		}

		public MediatorEvent onUnregistered
		{
			get
			{
				return onUnRegEvent;
			}
			set
			{
				onUnRegEvent = value;
			}
		}

		public string Name
		{
			get { return name; }
		}
	}
}
