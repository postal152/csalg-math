using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrintingTest.geom;
using System.Drawing;

namespace PrintingTest.printing.onepage
{
	public class ControlBlock:BaseBlock
	{
		private Control _control;
		public ControlBlock(Control control, Rect rect)
			: base(rect)
		{
			_control = control;
		}

		public override void PrintBlock(System.Drawing.Graphics gr, Rect rootRect)
		{
			Rectangle rectToPrint = getRectangleStruct();
			rectToPrint.X += (int)rootRect.X;
			rectToPrint.Y += (int)rootRect.Y;

			int old_w = _control.Width;
			int old_h = _control.Height;



			Bitmap bitmap = new Bitmap((int)Width, (int)Height);
			_control.Width = (int)Width;
			_control.Height = (int)Height;

			_control.DrawToBitmap(bitmap, new Rectangle(0, 0, (int)Width, (int)Height));
			_control.Width = old_w;
			_control.Height = old_h;

			gr.DrawImage(bitmap, new Point(rectToPrint.X, rectToPrint.Y));

			//chart1.DrawToBitmap(test, new Rectangle(0, 0, chart1.Width, chart1.Height));

			//base.PrintBlock(gr, rootRect);
		}

	}
}
