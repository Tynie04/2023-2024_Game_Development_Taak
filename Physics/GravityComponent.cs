using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Physics
{
	public class GravityComponent
	{
		public Vector2 Velocity { get; set; }
		public float Gravity { get; private set; } = 200f;
		public bool IsAffectedByGravity { get; set; } = true;

		public bool IsFalling => Velocity.Y > 0;

		public GravityComponent()
		{
			Velocity = Vector2.Zero;
		}

		public void ApplyGravity(float deltaTime)
		{
			if (IsAffectedByGravity)
			{
				Velocity += new Vector2(0, Gravity) * deltaTime;
			}
		}

		public void Reset()
		{
			Velocity = Vector2.Zero;
		}
	}

}
