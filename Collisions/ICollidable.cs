using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Collisions
{
	public interface ICollidable
	{
		Rectangle Bounds { get; } // Represents the collision bounds of an entity
		void HandleCollision(ICollidable other); // Define how an entity responds to collisions
	}
}
