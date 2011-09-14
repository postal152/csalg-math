using System;
using System.Collections.Generic;
using System.Text;

namespace csalg_math.matrix
{
	public class Matrix
	{
		#region Данные по матрице и ее инициализация

		private List<MatrixElementsList> _rows;
		private List<MatrixElementsList> _columns;
		private uint _rowCount=0;
		private uint _columnCount=0;

		/// <summary>
		/// Создает матрицу размером rowCount*columnCount
		/// </summary>
		/// <param name="rowCount">Количество строк</param>
		/// <param name="columnCount">Количество столбцов</param>
		public Matrix(uint rowCount, uint columnCount) {
			initMatrix(rowCount, columnCount);
		}

		/// <summary>
		/// Создает квадратную матрицу размера size
		/// </summary>
		/// <param name="size">Порядок матрицы</param>
		public Matrix(uint size) {
			initMatrix(size, size);
			
		}

		/// <summary>
		/// Создает диагональную матрицу с заданным диагональным числом
		/// </summary>
		/// <param name="size">Размер матрицы</param>
		/// <param name="number">Число на диагонали</param>
		public Matrix(uint size, double number) {
			initMatrix(size, size);
			int i;
			for (i = 0; i < size; i++) {
				this[i][i].Value = number;
			}
		}

		/// <summary>
		/// Инициализация матрицы
		/// </summary>
		/// <param name="rowCount"></param>
		/// <param name="columnCount"></param>
		private void initMatrix(uint rowCount, uint columnCount)
		{
			_rowCount = rowCount;
			_columnCount = columnCount;

			_rows = new List<MatrixElementsList>((int)RowCount);
			_columns = new List<MatrixElementsList>((int)ColumnCount);

			//инициализируем данные
			int i,j;
			for (i = 0; i < RowCount; i++) {
				_rows.Add(new MatrixElementsList(ColumnCount));
			}

			//а теперь просто записываем ссылки на MAtrixElement в columns
			// это даст доступ не только к строкам, но и к столбцам.

			for (i = 0; i < ColumnCount; i++)
			{
				_columns.Add(new MatrixElementsList(RowCount));
				for (j = 0; j < RowCount; j++)
				{
					_columns[i][j] = _rows[j][i];
				}
			}


		}

		/// <summary>
		/// Перечисление строк матрицы
		/// </summary>
		/// <param name="i">индекс строки</param>
		/// <returns>Возвращает MatrixElementsList</returns>
		public MatrixElementsList this[int i] {
			get {
				return _rows[(int)i];
			}
			set {
				_rows[(int)i] = value;
			}
		
		}

		/// <summary>
		/// Количество строк в матрице READ-ONLY
		/// </summary>
		public uint RowCount
		{
			get
			{
				return _rowCount;
			}
		}

		/// <summary>
		/// Количество столбцов в матрице READ-ONLY
		/// </summary>
		public uint ColumnCount {
			get {
				return _columnCount;
			}
		}

		/// <summary>
		/// Список со строками
		/// </summary>
		public List<MatrixElementsList> Rows {
			get {
				return _rows;
			}
		}

		/// <summary>
		/// Список со столбцами
		/// </summary>
		public List<MatrixElementsList> Columns {
			get {
				return _columns;
			}
		}

		#endregion

		public List<List<double>> GetRawData() {
			List<List<double>> result = new List<List<double>>();

			for (int i = 0; i < _rowCount; i++) {
				result.Add(_rows[i].GetRawData());
			}

			return result;
		}

		#region Элементарные методы для работы с матрицами

		/// <summary>
		/// Умножает текущую матрицу на число и возвращает новую матрицу, содержащую результат
		/// </summary>
		/// <param name="number">Множитель</param>
		/// <returns>Новая матрица=Старая*number</returns>
		public Matrix MultiplyMatrixByNumber(double number) {
			Matrix resultMatrix = new Matrix(RowCount, ColumnCount);
			int i,j;
			for (i = 0; i < RowCount; i++) {
				for (j = 0; j < ColumnCount; j++) {
					resultMatrix[i][j].Value = this[i][j].Value * number;
				}
			}
			return resultMatrix;
		}

		/// <summary>
		/// Умножение матрицы на матрицу
		/// </summary>
		/// <param name="matrix">матрица на которую нужно умножить текущую</param>
		/// <returns>новая матрица являющаяся результатом умножения текущий матрицы на переданную</returns>
		public Matrix MultiplyMatrixByMatrix(Matrix matrix) {
			if (this.ColumnCount == matrix.RowCount)
			{
				Matrix resultMatrix = new Matrix(RowCount, matrix.ColumnCount);
				int i,j,r;
				double tempResult;

				for (i = 0; i < RowCount; i++)
				{
					for (j = 0; j < matrix.ColumnCount; j++)
					{
						tempResult = 0;
						for (r = 0; r < this.ColumnCount; r++)
						{
							tempResult += this[i][r].Value * matrix[r][j].Value;
						}
						resultMatrix[i][j].Value = tempResult;
					}
				}
				return resultMatrix;
			}
			else {
				throw new Exception("Матрицы невозможно перемножить this.ColumnCount != matrix.RowCount");
			}
		}

		/// <summary>
		/// Возвращает матрицу равную текущей, но транспонированной 
		/// </summary>
		/// <returns>Транспонированная матрица</returns>
		public Matrix TransposeMatrix() {
			Matrix resultMatrix = new Matrix(ColumnCount, RowCount);

			int i,j;
			for (i = 0; i < ColumnCount; i++) {
				for (j = 0; j < RowCount; j++) {
					resultMatrix[i][j].Value = this[j][i].Value;
				}
			}
			return resultMatrix;
		}

		/// <summary>
		/// Вычитает из текущей матрицу другую матрицу
		/// </summary>
		/// <param name="matrix">Матрица</param>
		/// <returns>Новая матрица содержащая результат</returns>
		public Matrix SubtractionMatrix(Matrix matrix) {
			if(this.ColumnCount==matrix.ColumnCount && this.RowCount==matrix.RowCount){
				Matrix resultMatrix = new Matrix(matrix.RowCount, matrix.ColumnCount);
				int i, j;
				for (i = 0; i < RowCount; i++) {
					for (j = 0; j < ColumnCount; j++) {
						resultMatrix[i][j].Value = this[i][j].Value - matrix[i][j].Value;
					}
				}
				return resultMatrix;

			}else{
				throw new Exception("Матрицы не совпадают по размеру");
			}
		}

		/// <summary>
		/// Складывает две матрицы
		/// </summary>
		/// <param name="matrix">Вторая слагаемая матрица</param>
		/// <returns>Новая матрица содержащая результат</returns>
		public Matrix AdditionMatrix(Matrix matrix) {
			if (this.ColumnCount == matrix.ColumnCount && this.RowCount == matrix.RowCount)
			{
				Matrix resultMatrix = new Matrix(matrix.RowCount, matrix.ColumnCount);
				int i, j;
				for (i = 0; i < RowCount; i++)
				{
					for (j = 0; j < ColumnCount; j++)
					{
						resultMatrix[i][j].Value = this[i][j].Value + matrix[i][j].Value;
					}
				}
				return resultMatrix;
			}
			else
			{
				throw new Exception("Матрицы не совпадают по размеру");
			}
		}

		/// <summary>
		/// Считает определитель матрицы
		/// </summary>
		/// <returns></returns>
		public double ComputeDeterminant() {
			double result = 0;
			if (ColumnCount == RowCount)
			{
				if (ColumnCount == 2 && RowCount == 2)
				{
					return (this[0][0].Value * this[1][1].Value - this[0][1].Value * this[1][0].Value);//this[0][0].Value;
				}
				else {
					for(int i=0; i<ColumnCount; i++){

						result+=this[0][i].Value*(Math.Pow(-1,i))*CutMinorMatrix(0,(uint)i).ComputeDeterminant();
					}
				}
			}
			else {
				throw new Exception("Матрица то не квадратная");
			}

			return result;
		}

		/// <summary>
		/// Рекурсивное нахождение дополнительного минора
		/// </summary>
		/// <param name="rowIndex">iый индекс</param>
		/// <param name="columnIndex">jый индекс</param>
		/// <returns>минор</returns>
		public Matrix CutMinorMatrix(uint rowIndex, uint columnIndex) {
			Matrix resultMatrix = new Matrix(RowCount - 1, ColumnCount - 1);
			int i=0, j, newi, newj;
			for (i = 0; i < RowCount; i++) {

				for (j = 0; j < ColumnCount; j++) {
					if (i != rowIndex && j != columnIndex) {
						newi=i;
						newj=j;
						if(i>rowIndex){
							newi--;
						}
						if(j>columnIndex){
							newj--;
						}
						resultMatrix[newi][newj].Value = this[i][j].Value;
					}
				}
			}
			return resultMatrix;
		}

		/// <summary>
		/// Возвращает некое Tr от матрицы
		/// </summary>
		/// <returns></returns>
		public double GetTrOfMatrix() {
			double tr = 0;
			if (ColumnCount == RowCount)
			{
				for (int i = 0; i < ColumnCount; i++) {
					tr += this[i][i].Value;
				}
				return tr;
			}
			else
			{
				throw new Exception("Матрица то не квадратная");
			}
		}

		/// <summary>
		/// Находит союзную матрицу
		/// </summary>
		/// <returns>Возвращает новую союзную матрицу</returns>
		public Matrix UnionMatrix() {
			if (ColumnCount == RowCount)
			{
				Matrix transposeM = TransposeMatrix();
				Matrix result=new Matrix(ColumnCount);

				for (int i = 0; i < ColumnCount; i++) {
					for (int j = 0; j < ColumnCount; j++) {
						result[i][j].Value = Math.Pow(-1, i + j) * transposeM.CutMinorMatrix((uint)i, (uint)j).ComputeDeterminant();
					}

				}


				return result;
			}
			else
			{
				throw new Exception("Матрица не квадратная");
			
			}
		}

		/// <summary>
		/// Находит обратную матрицу
		/// </summary>
		/// <returns>Возвращает новую обратную матрицу</returns>
		public Matrix InverseMatrix() {
			if (ColumnCount == RowCount) {
				Matrix result=UnionMatrix();
				double det = ComputeDeterminant();
				return result.MultiplyMatrixByNumber(1 / det);
			}
			else
			{
				throw new Exception("Матрица не квадратная");
			}
		}

		/// <summary>
		/// Копирует текущую матрицу матрицу
		/// </summary>
		/// <returns></returns>
		public Matrix Copy() {
			Matrix result = new Matrix(RowCount, ColumnCount);

			int N = (int)RowCount;
			int M = (int)ColumnCount;
			int i, j;

			for (i = 0; i < N; i++) {
				for (j = 0; j < M; j++) {
					result[i][j].Value = this[i][j].Value;
				}
			}

			return result;
		}

		public string printMe() {
			string result = "";
			int i, j;
			for (i = 0; i < RowCount; i++) {
				for (j = 0; j < ColumnCount; j++) {
					result += this[i][j].Value + " | ";
				}
				result += "\n";
			}
			return result;
		}

		#endregion

		#region Перегрузки операторов

		

		#endregion

		#region Старый не работаюсщий код

		/*public double ComputeDeterminant() {
			double result = 0;
			if (ColumnCount == RowCount)
			{
				if (ColumnCount > 2)
				{
					for (int i = 0; i < RowCount; i++)
					{
						result += (Math.Pow(-1, i + 1)) * this[0][i].Value * CutMinorMatrix((uint)0, (uint)i);
						// 
					}
				}
				else
				{
					result = CutMinorMatrix(0, 0);
					
				}
			}
			else {
				throw new Exception("Матрица то не квадратная");
			}

			return result;
		}

		/// <summary>
		/// Рекурсивное нахождение дополнительного минора
		/// </summary>
		/// <param name="rowIndex">iый индекс</param>
		/// <param name="columnIndex">jый индекс</param>
		/// <returns>минор</returns>
		public double CutMinorMatrix(uint rowIndex, uint columnIndex) {
			if (ColumnCount == 2 && RowCount == 2)
			{
				return (this[0][0].Value * this[1][1].Value - this[0][1].Value * this[1][0].Value);//this[0][0].Value;
			}
			else
			{
				Matrix resultMatrix = new Matrix(RowCount - 1, ColumnCount - 1);
				int i=0, j, newi, newj;
				for (i = 0; i < RowCount; i++) {

					for (j = 0; j < ColumnCount; j++) {
						if (i != rowIndex && j != columnIndex) {
							newi=i;
							newj=j;
							if(i>rowIndex){
								newi--;
							}
							if(j>columnIndex){
								newj--;
							}
							resultMatrix[newi][newj].Value = this[i][j].Value;
						}
					}
				}
		 * public static Matrix operator *(Matrix m1, double m2){
			/*if ((m1 is Matrix) && (m2 is Matrix)) {
				Matrix matrix1=m1 as Matrix;
				Matrix matrix2=m2 as Matrix;
				return matrix1.MultiplyMatrixByMatrix(matrix2);
			}

			if ((m1 is Matrix) && ((m2 is double) || (m2 is int) || (m2 is float))) {
				Matrix matrix1 = m1 as Matrix;
				double number = (double)m2;
				return matrix1.MultiplyMatrixByNumber(number);
			}

			if ((m2 is Matrix) && ((m1 is double) || (m1 is int) || (m1 is float)))
			{
				Matrix matrix1 = m2 as Matrix;
				double number = (double)m1;
				return matrix1.MultiplyMatrixByNumber(number);
			}

			//throw new Exception("Ты че мне даешь на умножение блеать???!!!");

			return m1.MultiplyMatrixByNumber(m2);
		}*/

		/*public static Matrix operator *(Object m1, Object m2){
			if ((m1 is Matrix) && (m2 is Matrix)) {
				Matrix matrix1=m1 as Matrix;
				Matrix matrix2=m2 as Matrix;
				return matrix1.MultiplyMatrixByMatrix(matrix2);
			}

			if ((m1 is Matrix) && ((m2 is double) || (m2 is int) || (m2 is float))) {
				Matrix matrix1 = m1 as Matrix;
				double number = (double)m2;
				return matrix1.MultiplyMatrixByNumber(number);
			}

			if ((m2 is Matrix) && ((m1 is double) || (m1 is int) || (m1 is float)))
			{
				Matrix matrix1 = m2 as Matrix;
				double number = (double)m1;
				return matrix1.MultiplyMatrixByNumber(number);
			}

			throw new Exception("Ты че мне даешь на умножение блеать???!!!");

			return null;
		}//*/

		#endregion
	}
}
