using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeMattersGame
{

	public class CollisionManager : GameComponent
	{
		public Player player;
		public List<ICollideableObject> level;
		private Rectangle levelRect;

		public CollisionManager(Game game, Player player, List<ICollideableObject> level) : base(game)
		{
			this.player = player;
			this.level = level;

		}

		public override void Update(GameTime gameTime)
		{
			Rectangle playerRect = player.GetBounds();

			foreach (var item in level)
			{
				levelRect = item.GetBounds();
				if (playerRect.Intersects(levelRect))
				{
					if (playerRect.Bottom >= levelRect.Top)
					{
						player.position.Y = levelRect.Top - playerRect.Height;
						player.downCollision = true;
					}
					else if (playerRect.Right >= levelRect.Left)
					{
						player.rightCollision = true;
						player.position.X = levelRect.Left + playerRect.Width;
					}
					else if (playerRect.Left <= levelRect.Right)
					{
						player.leftCollision = true;
						player.position.X = levelRect.Right;
					}
				}
				if (!playerRect.Intersects(levelRect))
				{
					player.ResetCollision();
				}
			}
			base.Update(gameTime);
		}

	}
}
