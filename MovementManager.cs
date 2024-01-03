using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProject
{
    internal class MovementManager
    {
        Vector2 direction;
		int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
		public void Move(IMovable movable)
        {
            direction = movable.InputReader.ReadInput();


            //Debug.WriteLine(w);
            var afstand = direction * movable.Speed;
            //var toekomstigePositie = movable.Position + afstand;
            //movable.Position = toekomstigePositie;
            if (movable.Position.X >= 0)
            {
                movable.Position += afstand;
            }
            if (movable.Position.X < 5 || movable.Position.X > w - 150)
            {
                movable.Position -= afstand;
            }
            
            
        }
    }
}
