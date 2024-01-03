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

		public Tile CreateTile(int tileType, Vector2 position)
		{
			int tilesetIndex = tileType / _tileTextures.GetLength(1);
			int localTileIndex = tileType % _tileTextures.GetLength(1);

			// Calculate row and col based on localTileIndex
			int tilesPerRow = _tileTextures.GetLength(1);
			int row = localTileIndex / tilesPerRow;
			int col = localTileIndex % tilesPerRow;

			Rectangle sourceRect = new Rectangle(col * _tileWidth, row * _tileHeight, _tileWidth, _tileHeight);

			Tile newTile;

			// Use a switch statement to create the appropriate subclass based on tileType
			switch (tileType)
			{
				case 0: // GrassTile
					newTile = new GrassTile();
					break;
				case 1: // GroundTile
					newTile = new GroundTile();
					break;
				case 2: // PlatformTile
					newTile = new PlatformTile();
					break;
				// Add more cases as needed for different tile types
				default:
					newTile = new Tile();
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