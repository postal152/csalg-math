using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

using csalg_math.utils;
using csalg_math.automatic_data_processing.genetic;
using csalg_math.automatic_data_processing.genetic.strategies;
using csalg_math.automatic_data_processing.genetic.learner;


namespace csalg_math.automatic_data_processing.genetic.learner
{
	public class Learner
	{
		#region События
		public delegate void LearnerEvent(Learner obj, LearnerArgs args);
		public event LearnerEvent OptimizeStart;
		public event LearnerEvent OptimizeStop;
		public event LearnerEvent OptimizeTick;
		public event LearnerEvent OptimizeComplete;
		#endregion

		

		private Generation _firstGeneration;
		private GenerationController _generationController;

		private Thread _thread;

		private LearnerParameters _parameters;

		public Learner(LearnerParameters parameters)
		{
			_parameters = parameters;
			InitLearner(_parameters);
		}

		protected virtual void InitLearner(LearnerParameters parameters) {

			IDNAMergeStrategy merge = parameters.MergeStrategy;
			IDNAMutationStrategy mutation = parameters.MutationStrategy;
			IDNARankStrategy rank = parameters.RankStrategy;
			IDNASelectionStrategy select = parameters.SelectionStrategy;

			int genesCount = parameters.GenesCount;
			double maxRand = parameters.MaxRandomValue;
			double minRand = parameters.MinRandomValue;

			int generationCount = parameters.GenerationCount;
			int individualCount = parameters.IndividualCount;

			DNA dna;

			_firstGeneration = new Generation();

			for (int i = 0; i < individualCount; i++) {
				dna = new DNA((uint)genesCount);
				for (int j = 0; j < genesCount; j++) {
					dna[j] = GetRandom.GetNextDoubleFromRange(minRand, maxRand);
				}
				_firstGeneration.AddDNA(dna);
			}

			_generationController = new GenerationController(parameters.MutationChance,
															 parameters.MergeChance,
															 rank, select, merge, mutation);

			_generationController.SetStartGeneration(_firstGeneration);

			_thread = new Thread(DoOptimization);
			_thread.Priority = parameters.LearnerThreadPriority;

		}

		public virtual void StartOptimization()
		{
			if (_thread != null) {

				/*object p = new object();
				p.param = _parameters;*/

				ThreadObjectParams param = new ThreadObjectParams();
				param.parameters = _parameters;
				param.generationController = _generationController;

				_thread.Start(param);

				if (OptimizeStart != null) {
					OptimizeStart(this, null);
				}

			}
		}

		protected virtual void DoOptimization(object p) {
			ThreadObjectParams param = p as ThreadObjectParams;
			if (param != null) {
				
				int count = param.parameters.GenerationCount;
				double abortValue = param.parameters.OptimizationStopPoint;
				GenerationController gController = param.generationController;

				LearnerArgs args;

				Stopwatch watch = new Stopwatch();

				int timeOfTick = 0;
				int timeLeft = 0;

				for (int i = 0; i < count; i++) {
					watch.Start();
					gController.SexThemAll();
					watch.Stop();

					timeOfTick = (int)((double)(watch.ElapsedMilliseconds) / 1000.0);
					timeLeft = (int)((double)((count - i) * timeOfTick));
					watch.Reset();
					
					if (OptimizeTick != null)
					{
						args = new LearnerArgs((int)timeOfTick, (int)timeLeft, gController.LastGeneration, 0, i + 1); 
						OptimizeTick(null, args);
					}

					if (abortValue <= gController.LastGeneration.BestDNA.Rank) {
						break;
					}

					

				}

				args = new LearnerArgs((int)timeOfTick, (int)timeLeft, gController.LastGeneration, 0, count); 

				if (OptimizeComplete != null) {
					OptimizeComplete(null, args);
				}

			}
			

		}

		public virtual void StopOptimization()
		{
			if (_thread != null)
			{
				_thread.Suspend();
				if (OptimizeStart != null)
				{
					OptimizeStop(this, null);
				}
			}
		}

		public virtual void AbortOptimization() {
			if (_thread != null)
			{
				_thread.Abort();
			}
		}

	}

	internal class ThreadObjectParams{
		public LearnerParameters parameters;
		public GenerationController generationController;
	}

}
