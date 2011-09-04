using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using Mathematic.automatic_data_processing.genetic.strategies;

namespace Mathematic.automatic_data_processing.genetic.learner
{
	public class LearnerParameters
	{
		public LearnerParameters() {}
		~LearnerParameters() { }

		private IDNAMergeStrategy _mergeStrategy;
		private IDNAMutationStrategy _mutationStrategy;
		private IDNARankStrategy _rankStrategy;
		private IDNASelectionStrategy _selectionStrategy;

		private int _genesCount;
		private int _individualsCount;
		private int _generationsCount;
		private double _stopOptimizationPoint;

		private double _maxRandomValue;
		private double _minRandomValue;

		private double _mutationRatio;
		private double _mergeRatio;

		private ThreadPriority _threadPriority;

		public double MutationChance {
			get {
				return _mutationRatio;
			}

			set {
				_mutationRatio = value;
			}
		}

		public double MergeChance
		{
			get
			{
				return _mergeRatio;
			}

			set
			{
				_mergeRatio = value;
			}
		}


		

		public double MaxRandomValue {
			get {
				return _maxRandomValue;
			}
			set {
				_maxRandomValue = value;
			}
		}

		public double MinRandomValue {
			get {
				return _minRandomValue;
			}

			set {
				_minRandomValue = value;
			}
		}

		public ThreadPriority LearnerThreadPriority {
			set {
				_threadPriority = value;
			}
			get {
				return _threadPriority;
			}
		}

		public IDNAMergeStrategy MergeStrategy {
			set {
				_mergeStrategy = value;
			}
			get {
				return _mergeStrategy;
			}
		}

		public IDNAMutationStrategy MutationStrategy
		{
			set {
				_mutationStrategy=value;
			}
			get {
				return _mutationStrategy;
			}
		}
		
		public IDNARankStrategy RankStrategy
		{
			set {
				_rankStrategy=value;
			}
			get {
				return _rankStrategy;
			}
		}
		
		public IDNASelectionStrategy SelectionStrategy
		{
			set {
				_selectionStrategy=value;
			}
			get {
				return _selectionStrategy;
			}
		}

		public int GenesCount {
			get {
				return _genesCount;
			}
			set {
				_genesCount = value;
			}
		}
		
		public int IndividualCount
		{
			set {
				_individualsCount=value;
			}
			get {
				return _individualsCount;
			}
		}
		
		public int GenerationCount
		{
			set {
				_generationsCount = value;
			}
			get {
				return _generationsCount;
			}
		}

		public double OptimizationStopPoint
		{
			set {
				_stopOptimizationPoint = value;
			}
			get {
				return _stopOptimizationPoint;
			}
		}		
	}
}
