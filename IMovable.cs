﻿using GameDevProject.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
	internal interface IMovable
	{
		Vector2 Position { get; set; }
		Vector2 Speed { get; set; }
		IInputReader InputReader { get; set; }
		void UpdatePosition(Vector2 newPosition);
		GravityComponent Gravity { get; set; }
	}
}
