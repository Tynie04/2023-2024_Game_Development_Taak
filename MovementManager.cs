using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject
{
	internal class MovementManager
	{
		public void Move(IMovable movable)
		{
			Vector2 direction = movable.InputReader.ReadInput();
			var afstand = direction * movable.Speed;
			//var toekomstigePositie = movable.Position + afstand;
			//movable.Position = toekomstigePositie;
			movable.Position += afstand;
		}
	}
}
