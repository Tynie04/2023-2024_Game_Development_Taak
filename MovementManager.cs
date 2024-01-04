using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using GameDevProject.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject
{
	internal class MovementManager
	{
		private Vector2 direction;
		private int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

		public void Move(IMovable movable, GameTime gameTime)
		{
			// Read input for movement
			direction = movable.InputReader.ReadInput();

			// Handle movement based on the direction
			var afstand = direction * movable.Speed;

			if (movable.Position.X >= 0)
			{
				movable.Position += afstand;
			}
			if (movable.Position.X < 5 || movable.Position.X > w - 150)
			{
				movable.Position -= afstand;
			}

			// Handle jumping logic
			if (direction.Y < 0)
			{
				GravityProcessor.ApplyJumpForce(movable, jumpForce, (float)gameTime.ElapsedGameTime.TotalSeconds);
			}
			else
			{
				GravityProcessor.ApplyGravity(movable, (float)gameTime.ElapsedGameTime.TotalSeconds);
			}
		}

		private const float jumpForce = 325f; // Adjust this value based on your game's physics
	}
}
