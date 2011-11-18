using System;
using System.Text;

namespace csalgs.math.adp
{
	public class Dna:IComparable<Dna>
	{
		private double[] genes;
		private double rank;
		private int size;

		public Dna(int size) {
			if (size <= 0) throw new ArgumentOutOfRangeException("size must be > 0");
			
			genes = new double[size];
			this.size = size;
		}

		public void UpdateRank(IRanker ranker){
			rank = ranker.GetRank(genes);
		}

		public void Mutate(IMutator mutator) {
			mutator.Mutate(this);
		}

		public Dna Merge(IMerger merger) {
			return null;
		}

		public double[] RawGenes {
			get {
				return genes;
			}
		}

		public double Rank {
			get {
				return rank;
			}
		}

		public int Size {
			get {
				return size;
			}
		}

		public double this[int geneIndex] {
			get {
				return genes[geneIndex];
			}
			set {
				genes[geneIndex] = value;
			}
		}

		public Dna Clone() {
			Dna dna = new Dna(Size);

			for (int i = 0; i < Size; i++) {
				dna[i] = this[i];
			}

			return dna;
		}

		public int CompareTo(Dna dna)
		{
			return (dna.Rank == Rank ? 0 : (dna.Rank < Rank ? 1 : -1));
		}
	}
}
