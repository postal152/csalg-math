using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PrintingTest.geom;

namespace PrintingTest.printing.onepage
{
	public abstract class BaseBlock:Rect
	{

		public BaseBlock(uint x, uint y, uint width, uint height): base(x, y, width, height){}
		public BaseBlock(Rect rect):base(rect.X, rect.Y, rect.Width, rect.Height) {}
		public BaseBlock(Rectangle rect) : base(rect.X, rect.Y, rect.Width, rect.Height) { }

		public virtual void PrintBlock(Graphics gr, Rect rootRect) {
			Rectangle rectToPrint = getRectangleStruct();
			rectToPrint.X += (int)rootRect.X;
			rectToPrint.Y += (int)rootRect.Y;

			gr.DrawRectangle(new Pen(Color.Black), rectToPrint);
		}

		public virtual void DebugPrint(Graphics gr, Rect rootRect) {
			Rectangle rectToPrint = getRectangleStruct();
			rectToPrint.X += (int)rootRect.X;
			rectToPrint.Y += (int)rootRect.Y;

			gr.DrawRectangle(new Pen(Color.Black), rectToPrint);
		}

		public Rectangle getRectangleStruct()
		{
			return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
		}

		public RectangleF getRectangleFStruct() {
			return new RectangleF((float)X, (float)Y, (float)Width, (float)Height);
		}

	}
}
