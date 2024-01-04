using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Physics
{
	public class GravityComponent
	{
		public float GravityForce { get; set; } = 150f;
		public bool IsAffectedByGravity { get; set; } = true;
	}

}
