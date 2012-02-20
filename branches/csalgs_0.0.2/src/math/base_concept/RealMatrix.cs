using System;

namespace csalgs.math
{
	public class RealMatrix
	{
		#region static creators

		/// <summary>
		/// Create matrix rowCount*columnCount
		/// </summary>
		/// <param name="rowCount">count of rows</param>
		/// <param name="columnCount">count of columns</param>
		/// <returns>instance of new matrix</returns>
		public static RealMatrix GetMatrix(int rowCount, int columnCount) {
			return new RealMatrix(rowCount, columnCount);
		}

		/// <summary>
		/// Create new square matrix
		/// </summary>
		/// <param name="size">size of new square matrix</param>
		/// <returns></returns>
		public static RealMatrix GetQuadroMatrix(int size) {
			return new RealMatrix(size, size);
		}

		#endregion

		#region operators
		/// <summary>
		/// Appends two matrix
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>new matrix equals summ of m1 and m2</returns>
		public static RealMatrix operator +(RealMatrix m1, RealMatrix m2)
		{
			if (!m1.IsSizeEqual(m2)) throw new ArgumentException("Matrix's size not equal!");
			return m1.Clone().Append(m2);
		}

		/// <summary>
		/// Subtract two matrix
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>new matrix equals subtract between m1 and m2</returns>
		public static RealMatrix operator -(RealMatrix m1, RealMatrix m2)
		{
			if (!m1.IsSizeEqual(m2)) throw new ArgumentException("Matrix's size not equal!");
			return m1.Clone().Subtract(m2);
		}

		/// <summary>
		/// Multiply two matrix
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>new matrix equals multiply m1 and m2</returns>
		public static RealMatrix operator *(RealMatrix m1, RealMatrix m2)
		{
			return m1.Multiply(m2);
		}

		/// <summary>
		/// Multiply matrix and number
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>new matrix equals myltiply m1 and m2</returns>
		public static RealMatrix operator *(RealMatrix m1, Double m2)
		{
			return m1.Clone().Multiply(m2);
		}

		/// <summary>
		/// Divide matrix and number
		/// </summary>
		/// <param name="m1"></param>
		/// <param name="m2"></param>
		/// <returns>new matrix equals divide m1 and m2</returns>
		public static RealMatrix operator /(RealMatrix m1, Double m2)
		{
			return m1.Divide(m2);
		}

		/// <summary>
		/// Get inverse matrix
		/// </summary>
		/// <param name="m1"></param>
		/// <returns>new inverse matrix</returns>
		public static RealMatrix operator !(RealMatrix m1)
		{
			return m1.GetInverse();
		}

		/// <summary>
		/// Get transpose matrix
		/// </summary>
		/// <param name="m1"></param>
		/// <returns></returns>
		public static RealMatrix operator ~(RealMatrix m1)
		{
			return m1.Transpose();
		}
		#endregion

		#region private vars
		private double[,] rawData;
		private int rowCount;
		private int columnCount;
		
		private IMatrixElementIterator xIterator;
		#endregion
		
		/// <summary>
		/// Constructor for matrix rows*columns
		/// </summary>
		/// <param name="rows"></param>
		/// <param name="columns"></param>
		public RealMatrix(int rows, int columns){
			if (Math.Min(rows, columns) <= 0) throw new ArgumentOutOfRangeException("Size rows or/and columns <= 0");

			rowCount = rows;
			columnCount = columns;

			rawData = new double[rowCount, columns];
			for (int i = 0; i < rowCount; i++) {
				for (int j = 0; j < columnCount; j++) {
					rawData[i, j] = 0;
				}
			}

			xIterator = new HorizontalIterator(rawData, rowCount, columnCount);
		}

		#region elementary actions

		/// <summary>
		/// Check equal of current matrix and <paramref name="m"/>
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		public bool isEqual(RealMatrix m)
		{
			if (m == null) return false;
			if (!IsSizeEqual(m)) return false;

			for (int i = 0; i < m.rowCount; i++)
			{
				for (int j = 0; j < m.columnCount; j++)
				{
					if (this[i, j] != m[i, j]) return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Append matrix
		/// </summary>
		/// <param name="mtx"></param>
		/// <returns></returns>
		public RealMatrix Append(RealMatrix mtx) {
			if (mtx == null) throw new ArgumentNullException("mtx is null");
			if (!IsSizeEqual(mtx)) throw new ArgumentException("Invalid matrix size to append");

			for (int i = 0; i < RowCount; i++) {
				for (int j = 0; j < ColumnCount; j++) {
					this[i, j] += mtx[i, j];
				}
			}

			return this;
		}

		/// <summary>
		/// Subtract matrix
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		public RealMatrix Subtract(RealMatrix m)
		{
			if (IsSizeEqual(m) == false) throw new ArgumentException("Illegal matrix size");

			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColumnCount; j++)
				{
					this[i, j] -= m[i, j];
				}
			}

			return this;
		}

		/// <summary>
		/// Multiply by number
		/// </summary>
		/// <param name="number"></param>
		/// <returns>current matrix multiplied by number</returns>
		public RealMatrix Multiply(double number) {
			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColumnCount; j++)
				{
					this[i, j] *= number;
				}
			}

			return this;
		}

		/// <summary>
		/// Multiply by matrix
		/// </summary>
		/// <param name="m2"></param>
		/// <returns>new matrix equals this*m2</returns>
		public RealMatrix Multiply(RealMatrix m2)
		{
			if (this.ColumnCount != m2.RowCount) throw new ArgumentException("Invalid matrix size for multiply");

			RealMatrix result = RealMatrix.GetMatrix(RowCount, m2.ColumnCount);

			double temp;
			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < m2.ColumnCount; j++)
				{
					temp = 0;

					for (int k = 0; k < this.ColumnCount; k++) {
						temp += this[i, k] * m2[k, j];
					}

					result[i, j] = temp;
				}
			}

			return result;
		}

		/// <summary>
		/// Divide by number
		/// </summary>
		/// <param name="number"></param>
		/// <returns>current matrix divided by number</returns>
		public RealMatrix Divide(double number) {
			this.Multiply(1 / number);
			return this;
		}

		/// <summary>
		/// Change row's position
		/// </summary>
		/// <param name="index">row's index </param>
		/// <param name="target">index of target position</param>
		public void ChangeRowsPosition(int index, int target) {
			if (index < 0 || index > RowCount - 1 || target < 0 || target > RowCount-1) throw new ArgumentOutOfRangeException("indexes in out of range");
			if (index == target) return;

			double temp = 0;

			for (int j = 0, len = ColumnCount; j < len; j++)
			{
				temp = this[index, j];
				this[index, j] = this[index, target];
				this[index, target] = temp;
			}
		}

		/// <summary>
		/// Change column's position
		/// </summary>
		/// <param name="index">column's index </param>
		/// <param name="target">index of target position</param>
		public void ChangeColumnsPosition(int index, int target)
		{
			if (index < 0 || index > ColumnCount - 1 || target < 0 || target > ColumnCount-1) throw new ArgumentOutOfRangeException("indexes in out of range");
			if (index == target) return;
			double temp = 0;

			for (int j = 0, len = RowCount; j < len; j++)
			{
				temp = this[j, index];
				this[j, index] = this[target, index];
				this[target, index] = temp;
			}
		}

		#endregion

		#region non-simple action

		/// <summary>
		/// Transose matrix
		/// </summary>
		/// <returns>new matrix result of transosing current matrix</returns>
		public RealMatrix Transpose() {
			RealMatrix result = RealMatrix.GetMatrix(RowCount, ColumnCount);

			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColumnCount; j++)
				{
					result[i, j] = this[j, i];
				}
			}

			return result;
		}

		/// <summary>
		/// Get minor matrix
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <param name="columnIndex"></param>
		/// <returns>minor matrix</returns>
		public RealMatrix GetMinor(int rowIndex, int columnIndex)
		{
			if (rowIndex < 0 || rowIndex > RowCount - 1 || columnIndex < 0 || columnIndex > ColumnCount-1) throw new ArgumentOutOfRangeException("indexes in out of range");
			

			RealMatrix resultMatrix = RealMatrix.GetMatrix(RowCount - 1, ColumnCount - 1);
			int i = 0, j, newi, newj;
			for (i = 0; i < RowCount; i++)
			{

				for (j = 0; j < ColumnCount; j++)
				{
					if (i != rowIndex && j != columnIndex)
					{
						newi = i;
						newj = j;
						if (i > rowIndex)
						{
							newi--;
						}
						if (j > columnIndex)
						{
							newj--;
						}
						resultMatrix[newi,newj] = this[i,j];
					}
				}
			}
			return resultMatrix;
		}

		/// <summary>
		/// Get union matrix
		/// </summary>
		/// <returns>union matrix</returns>
		public RealMatrix GetUnion() {
			if (RowCount != ColumnCount) throw new ArgumentException("current matrix not square");

			RealMatrix result = RealMatrix.GetQuadroMatrix(RowCount);
			RealMatrix transposeM = Clone().Transpose();

			for (int i = 0; i < ColumnCount; i++)
			{
				for (int j = 0; j < ColumnCount; j++)
				{
					result[i,j] = Math.Pow(-1, i + j) * transposeM.GetMinor(i, j).RecursiveDetirminant();
				}

			}


			return result;
		}

		/// <summary>
		/// Get inverse matrix
		/// </summary>
		/// <returns>inverse matrix</returns>
		public RealMatrix GetInverse() {
			if (RowCount != ColumnCount) throw new InvalidOperationException("not square");

			RealMatrix result = GetUnion();
			double det = RecursiveDetirminant();
			return result/det;
		}

		/// <summary>
		/// Detirminant. Recursive method(too slow for size>10)
		/// </summary>
		/// <returns></returns>
		public double RecursiveDetirminant()
		{
			if (ColumnCount != RowCount) throw new InvalidOperationException("Matrix is not squared");

			double result = 0;

			if (ColumnCount == 2 && RowCount == 2)
			{
				return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
			}
			else
			{
				for (int i = 0; i < ColumnCount; i++)
				{

					result += this[0, i] * (Math.Pow(-1, i)) * GetMinor(0, i).RecursiveDetirminant();
				}
			}

			return result;
		}

		/// <summary>
		/// Check for size equals between this and <paramref name="matrix"/>
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns></returns>
		public bool IsSizeEqual(RealMatrix matrix) {
			return matrix.RowCount == RowCount && matrix.ColumnCount == ColumnCount;
		}

		/// <summary>
		/// Count of rows
		/// </summary>
		public int RowCount {
			get {
				return rowCount;
			}
		}

		/// <summary>
		/// Count of columns
		/// </summary>
		public int ColumnCount {
			get {
				return columnCount;
			}
		}

		/// <summary>
		/// Indexator for matrix elements
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns>matrix element</returns>
		public double this[int row, int column]
		{

			get
			{
				xIterator.setCurrentIndex(row);
				return xIterator[column];
			}

			set
			{
				xIterator.setCurrentIndex(row);
				xIterator[column] = value;
			}

		}

		/// <summary>
		/// Clone current matrix
		/// </summary>
		/// <returns>clone</returns>
		public RealMatrix Clone()
		{
			RealMatrix clone = RealMatrix.GetMatrix(rowCount, columnCount);

			for (int i = 0; i < rowCount; i++)
			{
				for (int j = 0; j < columnCount; j++)
				{
					clone[i, j] = this[i, j];
				}
			}

			return clone;
		}

		/// <summary>
		/// Return <typeparamref name="double[]"/> array of column elements
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public double[] GetColumnArray(int index) {
			double[] result = new double[rowCount];
			for (int i = 0; i < rowCount; i++) {
				xIterator.setCurrentIndex(i);
				result[i] = xIterator[index];
			}

			return result;
		}

		/// <summary>
		/// Return <typeparamref name="double[]"/> array of row elements
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public double[] GetRowArray(int index)
		{
			double[] result = new double[columnCount];

			for (int i = 0; i < columnCount; i++)
			{
				xIterator.setCurrentIndex(index);
				result[i] = xIterator[i];
			}

			return result;
		}
		#endregion

		#region private classes
		
		/// <summary>
		/// Iterator class for row elements
		/// </summary>
		private class HorizontalIterator : IMatrixElementIterator
		{
			private double[,] rawData;
			private int rows;
			private int columns;
			private int index;

			public HorizontalIterator(double[,] rawData, int rws, int clms)
			{
				this.rawData = rawData;
				this.rows = rws;
				this.columns = clms;
				index = 0;
			}

			/// <summary>
			/// Setting current index of rows
			/// </summary>
			/// <param name="index"></param>
			public void setCurrentIndex(int index)
			{
				if (index < 0 || index > this.rows - 1) 
				{
					throw new ArgumentOutOfRangeException("index out of range");
				}

				this.index = index;
			}
			
			/// <summary>
			/// Indexator for row elements by column's index
			/// </summary>
			/// <param name="column_index"></param>
			/// <returns></returns>
			public double this[int column_index]
			{
				get
				{
					return rawData[index, column_index];
				}
				set
				{
					rawData[index, column_index] = value;
				}
			}

		}

		/// <summary>
		/// Interface for matrix elements iterator
		/// </summary>
		private interface IMatrixElementIterator
		{
			void setCurrentIndex(int index);

			double this[int index]
			{
				get;
				set;
			}
		}
		#endregion
	}

}