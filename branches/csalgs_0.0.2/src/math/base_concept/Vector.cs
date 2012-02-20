using System;

namespace csalgs.math
{
	public interface IVector {
		double this[int index]{
			get;
			set;
		}

		double[] Values{
			get;
		}

		int Size
		{
			get;
		}

		IVector Copy(int[] indexes);
		IVector Copy();
	}


	public class Vector:IVector
	{
		private double[] values;
		private int size = 0;
		public Vector(double[] values) {
			init(values);
		}

		public Vector(int size) {
			double[] vv = new double[size];
			init(vv);
		}

		private void init(double[] values) {
			this.values = values;
			this.size = values.Length;
		}

		public double this[int index]
		{
			get
			{
				return values[index];
			}
			set
			{
				values[index] = value;
			}
		}

		public int Size
		{
			get { return size; }
		}

		public IVector Copy(int[] indexes)
		{
			int i = 0;
			double[] copy = new double[indexes.Length];
			for (i = 0; i < indexes.Length; i++) {
				copy[i] = values[indexes[i]];
			}
			return new Vector(copy);
		}

		public IVector Copy()
		{
			double[] copy = new double[size];
			Array.Copy(values, copy, size);
			return new Vector(copy);
		}
	

		public double[]  Values
		{
			get {
				double[] copy = new double[size];
				Array.Copy(values, copy, size);
				return copy;
			}
		}
	}
}
