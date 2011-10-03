using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using csalg_math.geom;
namespace csalg_math.printing.onepage
{
	class OnePageReport:Rect
	{

		private List<BaseBlock> _blockList;

		public OnePageReport(uint x, uint y, uint width, uint height)
			: base(x, y, width, height)
		{
			_blockList = new List<BaseBlock>();
			
		}

		public void AddBlock(BaseBlock block) {
			if (block == null) throw new NullReferenceException();

			_blockList.Add(block);

		}

		public void Print(Graphics gr) {
			//gr.DrawRectangle(new Pen(Color.Black), (float)X, (float)Y, (float)Width, (float)Height);
			foreach (BaseBlock block in _blockList) {
				block.PrintBlock(gr, this);
			}
		}

		public void DebugPrint(Graphics gr)
		{
			gr.DrawRectangle(new Pen(Color.Black), (float)X, (float)Y, (float)Width, (float)Height);
			foreach (BaseBlock block in _blockList)
			{
				block.DebugPrint(gr ,this);
			}
		}

	}
}
