using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintingTest.geom;
using System.Drawing;
namespace PrintingTest.printing.onepage
{
	public class TextBlock:BaseBlock
	{
		private string _text;
		public TextBlock(string text, Rect rect)
			: base(rect)
		{
			_text = text;
		}

		public override void PrintBlock(Graphics gr, Rect rootRect)
		{
			Console.WriteLine("TextBlock!!!!!!!!!!!!!!");
			Rectangle rectToPrint = getRectangleStruct();
			rectToPrint.X += (int)rootRect.X;
			rectToPrint.Y += (int)rootRect.Y;

			gr.DrawString(_text, new Font("Tahoma", 18), new SolidBrush(Color.Black), rectToPrint.X, rectToPrint.Y);
		}
	}
}
