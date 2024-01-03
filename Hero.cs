using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
    internal class Hero : IGameObject, IMovable
	{
		private Texture2D texture;
		Animation animation;
		private MovementManager movementManager;
		Animation right = new Animation();
		Animation left = new Animation();
		Animation neutralRight = new Animation();
		Animation neutralLeft = new Animation();
		direction d = Hero.direction.right;
		GraphicsDevice g;
		

		private Vector2 Limit(Vector2 v, float max)
		{
			if (v.Length() > max)
			{
				var ratio = max / v.Length();
				v.X *= ratio;
				v.Y *= ratio;
			}
			return v;
		}

		public Vector2 Position { get; set; }
		public Vector2 Speed { get; set; }
		IInputReader IMovable.InputReader { get; set; }

		public Hero(Texture2D texture, IInputReader inputReader)
		{
			movementManager = new MovementManager();
			this.texture = texture;
			animation = new Animation();

			((IMovable)this).Position = new Vector2(0, 0);
			((IMovable)this).Speed = new Vector2(5, 5);
			((IMovable)this).InputReader = inputReader;

			neutralRight.GetFramesFromTextureProperties(texture.Width, texture.Height, 8, 8, 7, 0);
			neutralLeft.GetFramesFromTextureProperties(texture.Width, texture.Height, 8, 8, 7, 1);
			right.GetFramesFromTextureProperties(texture.Width, texture.Height, 8, 8, 2, 4);
			left.GetFramesFromTextureProperties(texture.Width, texture.Height, 8, 8, 2, 5);

			animation = neutralRight;

			
		}


		public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, ((IMovable)this).Position, animation.CurrentFrame.SourceRectangle, Color.White);

		}

		public void Update(GameTime gameTime)
		{

			
			Move2();
			
			if (Keyboard.GetState().IsKeyDown(Keys.N))
			{
				//for debugging purposes
			}



		   animation.Update(gameTime);

		}

	
		private void Move2()
		{
			

			Animate(Keyboard.GetState().GetPressedKeys());
			movementManager.Move(this);
			
		}
		private void Animate(Keys[] state)
		{
			Debug.WriteLine(Position);
			
			if (state.Contains(Keys.Right) || state.Contains(Keys.D)) {

				animation = right;
				d = direction.right;
				return;
			}
			if (state.Contains(Keys.Left) || state.Contains(Keys.Q))
			{
				animation = left;
				d = direction.left;
				return;
			}
			else if (d == direction.right)
			{
				animation = neutralRight;
				return;
			}
			else if (d == direction.left)
			{
				animation = neutralLeft;
				return;
			}

		}
		private enum direction
		{
			left,
			right
		}

	}
}
