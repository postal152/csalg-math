using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csalgs.io
{
	public interface ISeriable
	{
		byte[] GetChunk();
		byte[] Write();
	}
}
