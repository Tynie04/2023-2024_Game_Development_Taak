using Accessibility;
using GameDevProject.BackGround;
using GameDevProject.BackGround.TileTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System.Diagnostics;

namespace GameDevProject
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private Texture2D _heroTexture;
		private Hero hero;
		private Texture2D[,] _tileset;
		private int _tileWidth;
		private int _tileHeight;
		private TileFactory tileFactory;
		private int levelHeight = 14;
		private int levelWidth = 24;
		private int[,] level =
		{
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 2, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 2, 1, 1, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 2, 1, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

		}
			; // Your terrain data
		private Texture2D terrainTexture; // Cached terrain texture
		private bool levelMade = false;
		private bool highlightBoundingBoxes = false;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			_graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			_graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			_graphics.ApplyChanges();
		}

		protected override void Initialize()
		{
			base.Initialize();
			hero = new Hero(_heroTexture, new KeyboardReader());
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_heroTexture = Content.Load<Texture2D>("squirrel sprite sheet_scaled_4x_pngcrushed");

			Texture2D[,] tileTextures = new Texture2D[,]
			{
				{ Content.Load<Texture2D>("ground_scaled_5x_pngcrushed") },
				{ Content.Load<Texture2D>("grass_scaled_5x_pngcrushed")},
				{ Content.Load<Texture2D>("platformLeft_scaled_5x_pngcrushed")},
				{ Content.Load<Texture2D>("platformRight_scaled_5x_pngcrushed")},
				{ Content.Load<Texture2D>("background_blue") }
                // Add more tile types as needed
            };

			_tileWidth = 80;
			_tileHeight = 80;

			_tileset = tileTextures;

			tileFactory = new TileFactory(_tileset, _tileWidth, _tileHeight);

			for (int i = 0; i < tileTextures.GetLength(0); i++)
			{
				for (int j = 0; j < tileTextures.GetLength(1); j++)
				{
					Debug.WriteLine($"Loaded texture at index ({i}, {j}): {_tileset[i, j].Name}");
				}
			}

			// Generate the terrain texture during initialization
			GenerateTerrainTexture();
		}

		private void GenerateTerrainTexture()
		{
			RenderTarget2D renderTarget = new RenderTarget2D(
				GraphicsDevice,
				levelWidth * _tileWidth,
				levelHeight * _tileHeight
			);

			GraphicsDevice.SetRenderTarget(renderTarget);
			GraphicsDevice.Clear(Color.Transparent);

			_spriteBatch.Begin();

			for (int y = 0; y < levelHeight; y++)
			{
				for (int x = 0; x < levelWidth; x++)
				{
					int tileIndex = level[y, x];
					Vector2 position = new Vector2(x * _tileWidth, y * _tileHeight);
                    BackGround.Tile tile = tileFactory.CreateTile(tileIndex, position);
					tile.Draw(_spriteBatch);
				}
			}

			_spriteBatch.End();

			GraphicsDevice.SetRenderTarget(null);

			terrainTexture = renderTarget;
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			hero.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Green);

			_spriteBatch.Begin();

			// Draw the cached terrain texture
			_spriteBatch.Draw(terrainTexture, Vector2.Zero, Color.White);

			// Draw the hero on top of the tilemap
			hero.Draw(_spriteBatch);


			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
