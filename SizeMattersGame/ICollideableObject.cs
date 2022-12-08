using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeMattersGame
{
	public interface ICollideableObject
	{
		Rectangle GetBounds();
	}
}
