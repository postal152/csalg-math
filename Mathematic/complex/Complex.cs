using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathematic.complex
{
	public class Complex
	{

		private double _R;
		private double _Im;

		public double R{
			get {
				return _R;
			}
			set {
				_R = value;
			}
		}

		public double Im {
			get {
				return _Im;
			}
			set {
				_Im = value;
			}

		}

		public Complex(double r) {
			R = r;
			Im = 1;
		}

		public Complex(double r, double im) {
			R = r;
			Im = im;
		}

		public Complex() {
			R = 0;
			Im = 1;
		}

		public Complex Copy() {
			return new Complex(R, Im);
		}

		public static Complex FromExp(double pe) {

			return new Complex(Math.Cos(pe), Math.Sin(pe));

		}

		public override bool Equals(object obj)
		{
			Complex c = (Complex)obj;
			if (c == null) return false;

			if (c.R == R && c.Im == Im) {
				return true;
			}
			return false;
		}

		public Complex Add(Complex c) {
			return new Complex(R + c.R, Im + c.Im);
		}

		public Complex Subtract(Complex c) {
			return new Complex(R - c.R, Im - c.Im);
		}

		public Complex Multiply(Complex c) {
			return new Complex(R * c.R - Im * c.Im, Im * c.R + R * c.Im);
		}

		public Complex Multiply(double d)
		{
			return new Complex(R * d, d * Im);
		}

		public Complex Division(double d) {
			if (d == 0) throw new DivideByZeroException();
			double z = Math.Pow(d, 2);
			return new Complex((R * d) / z, (d * Im) / z);
		}

		public Complex Division(Complex c)
		{
			if (c.R == 0 && c.Im == 0) throw new DivideByZeroException();

			double z = Math.Pow(c.R, 2) + Math.Pow(c.Im, 2);
			return new Complex((R * c.R + Im * c.Im) / z, (c.R * Im - R * c.Im) / z);
		}

		public override string ToString()
		{
			return "("+R.ToString()+", "+Im.ToString() +")";
		}

	}
}
