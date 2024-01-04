using GameDevProject.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Enemies.Ghoul
{
	internal class GhoulEnemy : BaseEnemy
	{
		private bool isMovingLeft = true;
		public Texture2D Texture { get; set; }
		Animation animation = new Animation();
		Animation neutralRight = new Animation();

		public float moveDistance { get; set; } // Property to store the move distance
		private Vector2 initialPosition;

		public override Rectangle Bounds
		{
			get
			{
				// Provide custom logic to calculate the bounding box for GhoulEnemy
				return new Rectangle((int)Position.X, (int)Position.Y, (1024 / 8) - 25, (640 / 5) );
			}
		}

		// Default constructor with a default initial position and move distance
		public GhoulEnemy(Texture2D texture)
			: this(new Vector2(800, 80), texture, 1000) // Default initial position (800, 80) and move distance of 1000 units
		{
			this.Texture = texture;
			neutralRight.GetFramesFromTextureProperties(texture.Width, texture.Height, 8, 5, 3, 0);
			animation = neutralRight;

			Gravity = new GravityComponent();
		}

		// Constructor with the option to specify the initial position and move distance
		public GhoulEnemy(Vector2 initialPosition, Texture2D texture, float moveDistance)
			: base(initialPosition, null, null)  // You can pass default values for inputReader and gravity
		{
			this.Texture = texture;
			Health = 50;  // Example: Set GhoulEnemy's health
			Damage = 15;  // Example: Set GhoulEnemy's damage
			Gravity = new GravityComponent();
			neutralRight.GetFramesFromTextureProperties(texture.Width, texture.Height, 8, 5, 7, 1);
			animation = neutralRight;
			this.moveDistance = moveDistance;
			this.initialPosition = initialPosition;
		}

		private void UpdateBounds()
		{
			Bounds = new Rectangle((int)Position.X, (int)Position.Y, (1024 / 8) - 25, (640 / 5) - 25);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			// Automatic left and right movement
			if (isMovingLeft)
			{
				Speed = new Vector2(-2.0f, 0);
				Position += Speed;
			}
			else
			{
				Speed = new Vector2(2.0f, 0);
				Position += Speed;
			}

			// Toggle direction when reaching the specified distance
			if (isMovingLeft && Position.X < (initialPosition.X - moveDistance / 2)
				|| !isMovingLeft && Position.X > (initialPosition.X + moveDistance / 2))
			{
				isMovingLeft = !isMovingLeft;
			}

			GravityProcessor.ApplyGravity(this, (float)gameTime.ElapsedGameTime.TotalSeconds);
			animation.Update(gameTime);

			UpdateBounds();

			// Implement additional GhoulEnemy-specific logic if needed
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			// Draw the GhoulEnemy on the screen using its texture
			spriteBatch.Draw(Texture, ((IMovable)this).Position, animation.CurrentFrame.SourceRectangle, Color.White);

			// Debugging
			if (Keyboard.GetState().IsKeyDown(Keys.B))
			{
				// Draw the bounding box (for visualization purposes)
				Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
				pixel.SetData(new[] { Color.Red });

				spriteBatch.Draw(pixel, Bounds, Color.Black);
			}
		}
	}
}
