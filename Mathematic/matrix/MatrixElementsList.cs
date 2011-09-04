using System;
using System.Collections.Generic;
using System.Text;

namespace Mathematic.matrix
{
	public class MatrixElementsList
	{
		private List<MatrixElement> _list;
		private uint _count=0;
		
		/// <summary>
		/// Создает ряд или столбец элементов матрицы
		/// </summary>
		/// <param name="count">количество элементов в матрице</param>
		public MatrixElementsList(uint count) {
			_count = count;
			_list = new List<MatrixElement>((int)count);
			for (int i = 0; i < Count; i++) {
				_list.Add(new MatrixElement());
			}
		}

		/// <summary>
		/// Возвращает список элементов типа double
		/// </summary>
		/// <returns></returns>
		public List<double> GetRawData() {
			List<double> data = new List<double>();

			for (int i = 0; i < Count; i++) {
				data.Add(List[i].Value);
			}

			return data;
		}

		public MatrixElement this[int i] {
			get {
				return _list[i];
			}
			set {
				_list[i] = value;
			}

		}

		/// <summary>
		/// Количество элементов в списке Readonly
		/// </summary>
		public uint Count {
			get {
				return _count;
			}
		}

		/// <summary>
		/// Доступ к List<MatrixElement>
		/// </summary>
		public List<MatrixElement> List {
			get {
				return _list;
			}
		}

	}
}
