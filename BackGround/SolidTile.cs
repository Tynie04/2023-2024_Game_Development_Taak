using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.BackGround
{
	public class SolidTile : Tile
	{
		public bool IsSolid { get; private set; }

		public SolidTile(bool isSolid)
		{
			IsSolid = isSolid;
		}
	}
}
