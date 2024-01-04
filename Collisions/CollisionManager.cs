using GameDevProject.BackGround;
using GameDevProject.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Collisions
{
	namespace GameDevProject.Collisions
	{
		public class CollisionManager
		{
			internal bool CheckTileCollisions(Hero hero, SolidTile[,] tiles)
			{
				bool collisionDetected = false;

				for (int y = 0; y < tiles.GetLength(0); y++)
				{
					for (int x = 0; x < tiles.GetLength(1); x++)
					{
						SolidTile tile = tiles[y, x];

						// Skip null tiles (assuming they represent sky tiles)
						if (tile == null)
						{
							continue;
						}


						// Check collision with the hero
						if (hero.Bounds.Intersects(tile.Bounds))
						{
							
							
							
									hero.Position = new Vector2(hero.Position.X, tile.Bounds.Top - hero.Bounds.Height);
									hero.Speed = new Vector2(hero.Speed.X, 0); // Stop falling
									hero.Gravity.Reset(); // Reset gravity component
							
							 
							// Handle other types of tiles if needed

							collisionDetected = true;
						}
					}
				}

				return collisionDetected;
			}
		}
	}
}

