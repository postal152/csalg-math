using System;
using System.Collections.Generic;
using System.Text;

namespace csalgs.math
{
	public interface ISelection {
		IVector this[int index]
		{
			get;
			set;
		}

		IVector Add(IVector vector);
		IVector Remove(int index);

		void Swap(int index_first, int index_second);

		ISelection Copy();
		ISelection Copy(int[] indexes);

		void SortBy(int index);

		int Size
		{
			get;
		}
	}

	public class Selection:ISelection
	{
		private SelectionComparator comparator;

		private List<IVector> selection;
		public Selection() {
			selection = new List<IVector>();
			comparator = new SelectionComparator();
		}

		public Selection(IVector[] vectors) {
			selection = new List<IVector>();
			for (int i = 0, len = vectors.Length; i < len; i++) {
				Add(vectors[i]);
			}
		}


		public IVector this[int index]
		{
			get { return selection[index]; }
			set {
				selection[index] = value;
			}
		}

		public IVector Add(IVector vector)
		{
			selection.Add(vector);
			return vector;
		}

		public IVector Remove(int index)
		{
			IVector vector = selection[index];
			selection.RemoveAt(index);
			return vector;
		}

		public void Swap(int index_first, int index_second)
		{
			IVector first = selection[index_first];
			IVector second = selection[index_second];

			selection[index_first] = second;
			selection[index_second] = first;
		}

		public ISelection Copy()
		{
			IVector[] result = new Vector[selection.Count];

			for (int i = 0, len = selection.Count; i < len; i++) {
				result[i] = selection[i].Copy();
			}

			return new Selection(result);
		}

		public ISelection Copy(int[] indexes)
		{
			Selection result = new Selection();
			for (int i = 0, len = Size; i < len; i++) {
				result.Add(this[i].Copy(indexes));
			}
			return result;
		}

		public void SortBy(int index)
		{
			comparator.index = index;
			selection.Sort(comparator);
		}

		public int Size
		{
			get { return selection.Count; }
		}
	}

	class SelectionComparator : IComparer<IVector> {
		public int index = 0;
		public int Compare(IVector x, IVector y)
		{
			if (x[index] > y[index]) return -1;
			if (x[index] < y[index]) return 1;
			return 0;
		}
	}
}
