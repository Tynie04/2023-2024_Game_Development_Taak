﻿using Accessibility;
using GameDevProject.BackGround;
using GameDevProject.BackGround.TileTypes;
using GameDevProject.Collisions;
using GameDevProject.Enemies.Ghoul;
using GameDevProject.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace GameDevProject
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private Texture2D _heroTexture;
		private Texture2D _ghoulTexture;
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
			{4, 4, 4, 2, 5, 5, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 2, 5, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

		}
			; // Your terrain data
		private Texture2D terrainTexture; // Cached terrain texture
		private bool levelMade = false;
		private CollisionManager collisionManager;
		private SolidTile[,] tiles;
		private GhoulEnemy ghoul;

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
			ghoul = new GhoulEnemy(new Vector2(800, 850),_ghoulTexture, 200);
		}
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_heroTexture = Content.Load<Texture2D>("squirrel sprite sheet_scaled_4x_pngcrushed");
			_ghoulTexture = Content.Load<Texture2D>("Ghoul Sprite Sheet_scaled_4x_pngcrushed");

			Texture2D[,] tileTextures = new Texture2D[,]
			{
				{ Content.Load<Texture2D>("ground_scaled_5x_pngcrushed") },
				{ Content.Load<Texture2D>("grass_scaled_5x_pngcrushed")},
				{ Content.Load<Texture2D>("platformLeft_scaled_5x_pngcrushed")},
				{ Content.Load<Texture2D>("platformRight_scaled_5x_pngcrushed")},
				{ Content.Load<Texture2D>("background_blue") },
				{ Content.Load<Texture2D>("PlatformMiddle_scaled_5x_pngcrushed") }
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

			// Generate the terrain first
			tiles = GenerateTerrain();

			// Generate the terrain texture during initialization
			GenerateTerrainTexture();
			collisionManager = new CollisionManager();
		}

		private SolidTile[,] GenerateTerrain()
		{
			SolidTile[,] tiles = new SolidTile[levelHeight, levelWidth];

			for (int y = 0; y < levelHeight; y++)
			{
				for (int x = 0; x < levelWidth; x++)
				{
					int tileIndex = level[y, x];
					Vector2 position = new Vector2(x * _tileWidth, y * _tileHeight);

					// Create the solid tile
					SolidTile tile = tileFactory.CreateSolidTile(tileIndex, position);

					// Use a default non-solid tile for sky tiles
					if (tileIndex != 4)  // Assuming 4 is the index for sky tiles
					{
						tiles[y, x] = tile;
					}
					else
					{
						tiles[y, x] = new SolidTile(false);  // Sky tile is not solid
					}
				}
			}

			return tiles;
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
                    BackGround.Tile tile = tileFactory.CreateSolidTile(tileIndex, position);
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

			// Step 1: Apply Gravity
			GravityProcessor.ApplyGravity(hero, (float)gameTime.ElapsedGameTime.TotalSeconds);

			// Step 2: Check collisions with tiles
			bool tileCollision = collisionManager.CheckTileCollisions(hero, tiles);
			bool tileCollision2 = collisionManager.CheckEnemyCollisions(ghoul, tiles);

			// Step 3: Update hero's position based on collision result and gravity
			if (!tileCollision )
			{
				hero.Position += hero.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}
			if (tileCollision2)
			{
				ghoul.Position += ghoul.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}
			hero.Update(gameTime);
			ghoul.Update(gameTime);

			// Check collisions with tiles
			

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

			ghoul.Draw(_spriteBatch);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
