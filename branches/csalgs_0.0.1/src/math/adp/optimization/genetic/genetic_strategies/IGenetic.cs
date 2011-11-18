using System;
using csalgs.utils;

namespace csalgs.math.adp
{
	public interface IRanker {
		double GetRank(double[] genes);
	}

	public interface IMutator {
		void Mutate(Dna genes);
	}

	public interface IMerger {
		Dna Merge(Dna father, Dna mother);
	}

	public interface ISelector {
		Selection GetBest(Selection childs);
	}

	public interface ICrosser {
		Selection GetNewSelection(Selection parentPool, int maxSelectionSize, IMerger merger, IMutator mutator);
	}

	public class StandardRanker:IRanker {

		public double GetRank(double[] genes)
		{
			double rank = 0;
			for (int i = 0; i < genes.Length; i++) {
				rank += genes[i];
			}
			return rank;
		}
	}

	public class StandardMutator : IMutator {

		public void Mutate(Dna dna)
		{
			int genesCount = dna.Size;

			int randomGeneIndex = (int)Math.Floor(Rnd.GetNextDouble() * (double)genesCount);
			double shift = (0.05) * Math.Abs(Rnd.GetNextDouble());

			if (Rnd.GetNextDouble() > 0.5)
			{
				shift = -shift;
			}
			
			dna[randomGeneIndex] += shift;
		}
	}

	public class StandardMerger:IMerger {

		public Dna Merge(Dna father, Dna mother)
		{
			int geneCount = mother.Size;

			Dna child = new Dna(geneCount);

			int locus = geneCount / 2;

			int firstPart = locus;
			int secondPart = geneCount - locus;

			for (int i = 0; i < firstPart; i++)
			{
				child[i] = mother[i];
			}

			for (int i = firstPart; i < geneCount; i++)
			{
				child[i] = father[i];
			}
			
			return child;
		}
	}

	public class StandardSelector:ISelector
	{
		public Selection GetBest(Selection childs)
		{
			childs.SortSelection();
			
			Selection parentPool = new Selection();

			int dnaCount = childs.Size;
			double bestPartIndex = 0.3;

			int bestPartCount = (int)Math.Floor(dnaCount * bestPartIndex);

			for (int i = 0; i < bestPartCount; i++)
			{
				parentPool.AddDna(childs[i].Clone());
			}

			return parentPool;
		}
	}

	public class StandardCrosser:ICrosser{
		private double mergeChance = 0.3;
		private double mutationChance = 0.2;

		public StandardCrosser(){
		
		}

		public StandardCrosser(double mergeChance, double mutationChance){
			this.mergeChance = mergeChance;
			this.mutationChance = mutationChance;
		}

		public Selection  GetNewSelection(Selection parentPool, int maxSelectionSize, IMerger merger, IMutator mutator)
		{
			
			Selection newGeneration = new Selection();
			int genesCount = parentPool[0].Size;

			int parentsCount = parentPool.Size;

			int randMotherIndex;
			int randFatherIndex;
			int locus;

			Dna child;

			while (true)
			{
				randMotherIndex = (int)Math.Floor(Rnd.GetNextDouble() * parentsCount);
				randFatherIndex = (int)Math.Floor(Rnd.GetNextDouble() * parentsCount);

				child = parentPool[randMotherIndex];

				if (Rnd.GetNextDouble() < mergeChance)
				{
					locus = (int)Math.Floor(Rnd.GetNextDouble() * genesCount);
					child = (merger.Merge(parentPool[randMotherIndex], parentPool[randFatherIndex]));
				}

				if (Rnd.GetNextDouble() < mutationChance)
				{
					child.Mutate(mutator);
				}

				parentPool.AddDna(child);

				if (parentPool.Size >= maxSelectionSize) break;
			}

			return parentPool;
		}

	}
}
