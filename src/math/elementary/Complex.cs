using System;

/*
 * Author: Shirobok Pavel (ramshteks@gmail.com)
 * Created: 17.11.2011
 * 
 * Class description:
 * (RUS)
 * Класс для работы с комплексными числами.
 * Переопределены операторы для основных действий
 * 
 * (ENG)
 * 
 * License:
 * AS-IS
 * 
 * Part of:
 * csalgs project
 */

namespace csalgs.math.elementary
{
	public class Complex
	{
		private double _R;
		private double _Im;

		/// <summary>
		/// Real part of number
		/// </summary>
		public double R{
			get {
				return _R;
			}
			set {
				_R = value;
			}
		}

		/// <summary>
		/// Imaginary part of part
		/// </summary>
		public double Im {
			get {
				return _Im;
			}
			set {
				_Im = value;
			}

		}

		/// <summary>
		/// Constructor Real = <paramref name="r"/> Imaginary = 1
		/// </summary>
		/// <param name="r"></param>
		public Complex(double r) {
			R = r;
			Im = 1;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="r">real part</param>
		/// <param name="im">imaginary part</param>
		public Complex(double r, double im) {
			R = r;
			Im = im;
		}

		/// <summary>
		/// Constructor Real = 0 Imaginary = 1
		/// </summary>
		public Complex() {
			R = 0;
			Im = 1;
		}

		/// <summary>
		/// Clone
		/// </summary>
		/// <returns>clone o current number</returns>
		public Complex Clone() {
			return new Complex(R, Im);
		}

		/// <summary>
		/// Generate complex number from exponent power
		/// </summary>
		/// <param name="pe">power of exponent</param>
		/// <returns></returns>
		public static Complex FromExp(double pe) {

			return new Complex(Math.Cos(pe), Math.Sin(pe));

		}

		public static Complex operator +(Complex c1, Complex c2) {
			return c1.Clone().Append(c2);
		}

		public static Complex operator +(Complex c1, double c2)
		{
			return c1.Clone().Append(c2);
		}

		public static Complex operator -(Complex c1, Complex c2)
		{
			return c1.Clone().Subtract(c2);
		}

		public static Complex operator -(Complex c1, double c2)
		{
			return c1.Clone().Subtract(c2);
		}

		public static Complex operator *(Complex c1, Complex c2)
		{
			return c1.Clone().Multiply(c2);
		}

		public static Complex operator *(Complex c1, double c2)
		{
			return c1.Clone().Multiply(c2);
		}

		public static Complex operator /(Complex c1, Complex c2)
		{
			return c1.Clone().Divide(c2);
		}

		public static Complex operator /(Complex c1, double c2)
		{
			return c1.Clone().Divide(c2);
		}

		/// <summary>
		/// Append complex number
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public Complex Append(Complex c) {
			double newR = R + c.R;
			double newIm = Im + c.Im;

			R = newR;
			Im = newIm;

			return this;
		}

		/// <summary>
		/// Append
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public Complex Append(double c)
		{
			double newR = R + c;
			R = newR;
			return this;
		}

		/// <summary>
		/// Subtract complex number
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public Complex Subtract(Complex c) {
			double newR = R - c.R;
			double newIm = Im - c.Im;

			R = newR;
			Im = newIm;

			return this;
		}

		/// <summary>
		/// Subtact
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public Complex Subtract(double c)
		{
			double newR = R - c;
			R = newR;

			return this;
		}

		/// <summary>
		/// Multiply
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public Complex Multiply(Complex c) {
			double newR = R * c.R - Im * c.Im;
			double newIm = Im * c.R + R * c.Im;

			R = newR;
			Im = newIm;

			return this;
		}

		/// <summary>
		/// Multiply
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public Complex Multiply(double d)
		{
			double newR = R * d;
			double newIm = d * Im;

			R = newR;
			Im = newIm;

			return this;
		}

		/// <summary>
		/// Divide
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public Complex Divide(double d) {
			if (d == 0) throw new DivideByZeroException();
			double z = Math.Pow(d, 2);

			double newR = (R * d) / z;
			double newIm = (d * Im) / z;

			R = newR;
			Im = newIm;

			return this;
		}

		/// <summary>
		/// Divide
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public Complex Divide(Complex c)
		{
			if (c.R == 0 && c.Im == 0) throw new DivideByZeroException();

			double z = Math.Pow(c.R, 2) + Math.Pow(c.Im, 2);

			double newR = (R * c.R + Im * c.Im) / z;
			double newIm = (c.R * Im - R * c.Im) / z;

			R = newR;
			Im = newIm;

			return this;
		}

		public override string ToString()
		{
			return "("+R.ToString()+", "+Im.ToString() +")";
		}

	}
}
