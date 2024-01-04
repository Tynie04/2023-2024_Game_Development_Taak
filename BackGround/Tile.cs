using GameDevProject.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.BackGround
{
	public class Tile : ICollidable
	{
		public Texture2D Texture { get; set; }
		public Rectangle SourceRectangle { get; set; }
		public Vector2 Position { get; set; }
		public Rectangle Bounds { get; set; }


		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				Texture,
				Position,
				SourceRectangle,
				Color.White

			);
		}

		public void HandleCollision(ICollidable other)
		{
			// Define how the tile responds to collisions with other ICollidable entities
			// For example, you might want to handle collisions differently based on the type of tile
		}
	}
}
