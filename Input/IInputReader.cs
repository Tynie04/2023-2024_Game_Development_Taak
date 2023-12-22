using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject.Input
{
	internal interface IInputReader
	{
		Vector2 ReadInput();
		public bool IsDsestinationInput { get; }
	}
}
