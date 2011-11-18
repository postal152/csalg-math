using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalgs.math.adp
{
	public class Selection
	{
		private List<Dna> selection;
		
		public Selection() {
			selection = new List<Dna>();
		}

		public Dna this[int dnaIndex] {
			get {
				return selection[dnaIndex];
			}
		}

		public int Size {
			get {
				return selection.Count;
			}
		}

		public void AddDna(Dna dna) {
			if (dna == null) throw new ArgumentNullException("dna == null");
			selection.Add(dna);
		}
		
		public void MutateSelection(IMutator mutate) {
			if (mutate == null) throw new ArgumentNullException("mutate == null");
			selection.ForEach((Dna dna) => { dna.Mutate(mutate); });
		}
		
		public void RankSelection(IRanker rankStr) {
			if (rankStr == null) throw new ArgumentNullException("rankStr == null");
			selection.ForEach((Dna dna) => { dna.UpdateRank(rankStr); });
		}

		public void SortSelection() {
			selection.Sort();
		}

	}
}
