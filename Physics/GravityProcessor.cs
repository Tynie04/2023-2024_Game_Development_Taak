using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Physics
{
	internal static class GravityProcessor
	{
		public static void ApplyGravity(IMovable entity, float deltaTime)
		{
			if (entity.Gravity.IsAffectedByGravity)
			{
				// Update the entity's velocity based on gravity force
				entity.Gravity.Velocity += new Vector2(0, entity.Gravity.Gravity) * deltaTime;

				// Update the entity's position based on velocity
				Vector2 newPosition = new Vector2(entity.Position.X, entity.Position.Y + entity.Gravity.Velocity.Y * deltaTime);
				entity.UpdatePosition(newPosition);
			}
		}

		public static void ApplyJumpForce(IMovable entity, float jumpForce, float deltaTime)
		{
			// Update the entity's velocity based on upward force (jump)
			entity.Gravity.Velocity = new Vector2(entity.Gravity.Velocity.X, -jumpForce);

			// Update the entity's position based on velocity
			Vector2 newPosition = new Vector2(entity.Position.X, entity.Position.Y + entity.Gravity.Velocity.Y * deltaTime);
			entity.UpdatePosition(newPosition);
		}
	}
}