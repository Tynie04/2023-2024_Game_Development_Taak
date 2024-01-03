using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameDevProject
{
    internal class KeyboardReader : IInputReader
    {


        public Vector2 ReadInput()
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;
            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))
            {
                direction.X -= 1;
            }
            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                direction.X += 1;
            }
            return direction;

        }
        public bool IsDsestinationInput => false;

		
	}
}
