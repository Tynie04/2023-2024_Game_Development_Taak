using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Physics
{
	public interface IGravityAffected
	{
		GravityComponent Gravity { get; set; }
		Vector2 Position { get; } 
	}

}
