using System;
using csalgs.math.elementary;

namespace csalgs.math.adp
{
	public class EvolutionController:IOptimizer
	{
		private IMerger merger;
		private ISelector selector;
		private IRanker ranker;
		private IMutator mutator;
		private ICrosser crosser;

		private int currentEvoStep;
		private int evoStepCount;

		private Selection currentSelection;

		private int maxSelectionSize;

		public EvolutionController(int evoStepCount, int maxSelectionSize) {
			this.evoStepCount = evoStepCount;
			this.maxSelectionSize = maxSelectionSize;
			currentEvoStep = 0;
		}

		public void InitEvolution(Selection startSelection) {
			if (startSelection == null) throw new ArgumentNullException("start selection is null");
			if (startSelection.Size == 0) throw new ArgumentOutOfRangeException("size of start selection must be > 0");

			if (merger == null) {
				merger = new StandardMerger();
			}

			if (selector == null)
			{
				selector = new StandardSelector();
			}

			if (ranker == null) {
				ranker = new StandardRanker();
			}

			if (mutator == null) {
				mutator = new StandardMutator();
			}

			if (crosser == null) {
				crosser = new StandardCrosser();
			}

			currentSelection = startSelection;
			currentSelection.RankSelection(ranker);
		}

		public bool HasMoreSteps {
			get
			{
				return (evoStepCount <= 0 ? true : ((evoStepCount - 1 >= currentEvoStep) ? true : false));
			}
		}

		public void NextStep() {
			currentEvoStep++;

			Selection parentPool = selector.GetBest(currentSelection);
			currentSelection = crosser.GetNewSelection(parentPool, maxSelectionSize, merger, mutator);
			currentSelection.RankSelection(ranker);
		}

		public Selection CurrentSelection {
			get {
				if (currentSelection == null) throw new InvalidOperationException("Evolution not inited");
				return currentSelection;
			}
		}

		public IMerger Merger
		{
			get
			{
				return merger;
			}
			set
			{
				if (currentEvoStep != 0) throw new InvalidOperationException("Evolution is already started");
				merger = value;
			}
		}

		public ISelector Selector
		{
			get
			{
				return selector;
			}
			set
			{
				if (currentEvoStep != 0) throw new InvalidOperationException("Evolution is already started");
				selector = value;
			}
		}

		public IMutator Mutator
		{
			get
			{
				return mutator;
			}
			set
			{
				if (currentEvoStep != 0) throw new InvalidOperationException("Evolution is already started");
				mutator = value;
			}
		}

		public IRanker Ranker
		{
			get
			{
				return ranker;
			}
			set
			{
				if (currentEvoStep != 0) throw new InvalidOperationException("Evolution is already started");
				ranker = value;
			}
		}

		public ICrosser Crosser
		{
			get
			{
				return crosser;
			}
			set
			{
				if (currentEvoStep != 0) throw new InvalidOperationException("Evolution is already started");
				crosser = value;
			}
		}

		public double Score
		{
			get {
				if (currentSelection == null) throw new InvalidOperationException("Evolution not inited");
				return currentSelection[0].Rank;
			}
		}

		public elementary.Vector LastVector
		{
			get {
				if (currentSelection == null) throw new InvalidOperationException("Evolution not inited");
				return new Vector(currentSelection[0].RawGenes);
			}
		}
	}
}
