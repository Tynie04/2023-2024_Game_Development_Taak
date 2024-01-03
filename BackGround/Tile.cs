using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.BackGround
{
	public class Tile 
	{
		public Texture2D Texture { get; set; }
		public Rectangle SourceRectangle { get; set; }
		public Vector2 Position { get; set; }

		public async void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				Texture,
				Position,
				SourceRectangle,
				Color.White

			);
		}
	}
}
