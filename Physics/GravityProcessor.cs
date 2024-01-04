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
				// Update the entity's position based on gravity force
				Vector2 newPosition = new Vector2(entity.Position.X, entity.Position.Y + entity.Gravity.Gravity * deltaTime);
				entity.UpdatePosition(newPosition);
			}

			// Other gravity-related logic specific to the entity...
		}
	}

}
