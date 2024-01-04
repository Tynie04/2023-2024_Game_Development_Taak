using GameDevProject.BackGround.TileTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject.BackGround
{
	public class TileFactory
	{
		private Texture2D[,] _tileTextures;
		private int _tileWidth;
		private int _tileHeight;

		public TileFactory(Texture2D[,] tileTextures, int tileWidth, int tileHeight)
		{
			_tileTextures = tileTextures;
			_tileWidth = tileWidth;
			_tileHeight = tileHeight;
		}

		public SolidTile CreateSolidTile(int tileType, Vector2 position)
		{
			int tilesetIndex = tileType / _tileTextures.GetLength(1);
			int localTileIndex = tileType % _tileTextures.GetLength(1);

			// Calculate row and col based on localTileIndex
			int tilesPerRow = _tileTextures.GetLength(1);
			int row = localTileIndex / tilesPerRow;
			int col = localTileIndex % tilesPerRow;

			Rectangle sourceRect = new Rectangle(col * _tileWidth, row * _tileHeight, _tileWidth, _tileHeight);

			SolidTile newTile;

			switch (tileType)
			{
				case 0: // ground
				case 1: // GrassTile
				case 2: // PlatformLeft
				case 3: // PlatformRight
					newTile = new SolidTile(true); // Make these tiles solid
					newTile.Bounds = new Rectangle((int)position.X, (int)position.Y, _tileWidth, _tileHeight); // Set correct width and height
					break;
				case 4: // Sky
					newTile = new SolidTile(false); // Sky tile is not solid
					break;
				// Add more cases as needed for different tile types
				default:
					newTile = new SolidTile(true); // Default to solid
					newTile.Bounds = new Rectangle((int)position.X, (int)position.Y, _tileWidth, _tileHeight); // Set correct width and height
					break;
			}

			// Set properties common to all tiles
			newTile.Texture = _tileTextures[tilesetIndex, localTileIndex];
			newTile.Position = position;
			newTile.SourceRectangle = sourceRect;

			return newTile;
		}
	}
}