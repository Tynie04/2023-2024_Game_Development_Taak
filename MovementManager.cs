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
		Vector2 direction;
		public void Move(IMovable movable)
		{
			direction = movable.InputReader.ReadInput();



			var afstand = direction * movable.Speed;
			//var toekomstigePositie = movable.Position + afstand;
			//movable.Position = toekomstigePositie;
			movable.Position += afstand;
		}
		public float GetDirectionX()
		{
			return direction.X;
		}
		public Vector2 GetDirection()
		{
			return direction;
		}
	}
}
