using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameDevProject.Collisions;
using GameDevProject.Physics;

namespace GameDevProject
{
	internal class KeyboardReader : IInputReader
	{
		private bool isJumpKeyPressed = false;

		public Vector2 ReadInput()
		{
			KeyboardState state = Keyboard.GetState();
			Vector2 direction = Vector2.Zero;

			if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))
			{
				direction.X -= 1;
			}
			if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
			{
				direction.X += 1;
			}

			if (state.IsKeyDown(Keys.Space) && !isJumpKeyPressed)
			{
				direction.Y -= 1;
				isJumpKeyPressed = true;
				
			}
			else if (state.IsKeyUp(Keys.Space))
			{
				isJumpKeyPressed = false;
			}

			return direction;
		}

		public bool IsDsestinationInput => false;

		private const float jumpForce = 20f; // Adjust this value based on your game's physics
	}

}

