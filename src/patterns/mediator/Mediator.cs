using System;
using System.Collections;

namespace csalgs.patterns
{
	public class Mediator
	{
		private static Mediator instance;
		private Hashtable handlers;

		public Mediator() {
			if (instance != null) throw new InvalidOperationException("this is singletooooon!");
			instance = this;
			handlers = new Hashtable();
		}

		public static Mediator Self {
			get {
				if(instance==null)instance = new Mediator();
				return instance;
			}
		}

		public static void RegisterSubsystem(INotifier subsystem) {
			if (subsystem == null) throw new ArgumentNullException("subsystem is null");

			if (Self.handlers.ContainsKey(subsystem.Name)==false) {
				Self.handlers.Add(subsystem.Name, subsystem);
				if (subsystem.onRegistered != null) subsystem.onRegistered.Invoke();
			}
		}

		public static void sendNotify(INotify notify) 
		{
			if (notify.Target == "" || notify.Target == null) {
				Self.MulticastSending(notify);
			}else {
				
				INotifier notifier = Self.handlers[notify.Target] as INotifier;
				if (notifier != null) { 
					notifier.recvNotify(notify);
				}
			}
		}
		
		public void MulticastSending(INotify notify) {
			throw new NotSupportedException("no multicast yet, define Target name");
		}

	}
}
