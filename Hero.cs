using GameDevProject.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDevProject
{
	internal class Hero : IGameObject, IMovable
	{
		private Texture2D texture;
		Animation animation;
		private MovementManager movementManager;

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
			animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 8, 8, 2, 4);
			((IMovable)this).Position = new Vector2(0, 0);
			((IMovable)this).Speed = new Vector2(2, 2);
			((IMovable)this).InputReader = inputReader;


		}


		public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, ((IMovable)this).Position, animation.CurrentFrame.SourceRectangle, Color.White);

		}

		public void Update(GameTime gameTime)
		{
			Move2();

			animation.Update(gameTime);

		}

		/*private void MoveWithMouse() //Werkt niet zoals zou moeten: sprite wordt niet getekend op scherm. zonder Normalisatie werkt dit wel
        {
            MouseState state = Mouse.GetState();
            Vector2 mouseVector = new Vector2(state.X, state.Y);

            var richting = mouseVector - positie;
            richting.Normalize();
            richting = Vector2.Multiply(richting, 0.1f);
            snelheid += richting;
            snelheid = Limit(snelheid, 10);
            positie += snelheid;


        }*/

		/*        private void Move()

				{
					positie += snelheid;
					if (positie.X > 800 -32
						|| positie.X < 0)
					{
						snelheid.X *= -1;
					}
					if (positie.Y > 480-32
						|| positie.Y < 0)
					{
						snelheid.Y *= -1;
					}


				}*/
		private void Move2()
		{
			movementManager.Move(this);
		}
	}
}
