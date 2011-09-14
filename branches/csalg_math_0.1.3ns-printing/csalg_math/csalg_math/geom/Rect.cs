using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalg_math.geom
{
	public class Rect
	{
		private double _x;
		private double _y;
		private double _width;
		private double _height;

		//todo сделать x,y, width, height для int и float

		public double X {
			get {
				return _x;
			}
			set {
				_x = value;
			}
		}

		public double Y
		{
			get
			{
				return _y;
			}
			set
			{
				_y = value;
			}
		}

		public double Width
		{
			get
			{
				return _width;
			}
			set
			{
				_width = value;
			}
		}

		public double Height
		{
			get
			{
				return _height;
			}
			set
			{
				_height = value;
			}
		}

		public Rect() {
			initRect(0, 0, 0, 0);
		}

		public Rect(int x, int y, int width, int height) {
			initRect((double)x, (double)y, (double)width, (double)height);
		}

		public Rect(double x, double y, double width, double height)
		{
			initRect(x, y, width, height);
		}

		private void initRect(double x, double y, double width, double height) {
			_x = x;
			_y = y;
			_width = width;
			_height = height;
		}

	}
}
