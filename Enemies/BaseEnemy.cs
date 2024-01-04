using GameDevProject.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Enemies
{
	internal class BaseEnemy : IMovable, IGameObject
	{
		public Vector2 Position { get; set; }
		public Vector2 Speed { get; set; }
		public IInputReader InputReader { get; set; }
		public GravityComponent Gravity { get; set; }
		public int Health { get; set; }
		public int Damage { get; set; }
		public virtual Rectangle Bounds { get; set; }


		public BaseEnemy(Vector2 initialPosition, IInputReader inputReader, GravityComponent gravity)
		{
			Position = initialPosition;
			InputReader = inputReader;
			Gravity = gravity;
			Health = 100; // Set an initial health value
			Damage = 10;  // Set an initial damage value
						  // Initialize other properties as needed
			Bounds = new Rectangle(0, 0, 50, 50);
		}

		public void UpdatePosition(Vector2 newPosition)
		{
			Position = newPosition;
			// Additional logic if needed
		}

		public virtual void Update(GameTime gameTime)
		{
			// Common enemy logic
			// ...

			// Apply gravity
			GravityProcessor.ApplyGravity(this, (float)gameTime.ElapsedGameTime.TotalSeconds);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			// Draw the enemy on the screen
		}

		// Implement the Bounds property required by the collision manager
		
	}
}
