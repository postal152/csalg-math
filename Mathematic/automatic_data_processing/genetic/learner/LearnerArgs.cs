using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mathematic.automatic_data_processing.genetic;

namespace Mathematic.automatic_data_processing.genetic.learner
{
	public class LearnerArgs:EventArgs
	{

		private int _lastOptimizeStopTime;
		private int _timeLeft;
		private Generation _lastGeneration;
		private double _lastError;
		private int _counter;

		public LearnerArgs(int lastOptTime, int timeLeft, Generation lastGen, double lastError, int count):base() {
			_lastError = lastError;
			_lastOptimizeStopTime = lastOptTime;
			_lastGeneration = lastGen;
			_timeLeft = timeLeft;
			_counter = count;
		}

		public int Counter {
			get {
				return _counter;
			}
		}

		public Generation Generation {
			get {
				return _lastGeneration;
			}
		}

		public int LastOptimizeStopTime {
			get {

				return _lastOptimizeStopTime;
			}
		}

		public int TimeLeft {
			get {
				return _timeLeft;
			}
		}

		public double LastError {
			get {
				return _lastError;
			}
		}


	}
}
