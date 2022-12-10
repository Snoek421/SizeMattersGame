using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SizeMattersGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeMattersGame
{
    public class SideCollisionManager : GameComponent
	{
		public Player player;
		public List<ICollideableObject> level;
		private Rectangle levelRect;
		public SideCollisionManager(Game game, Player player, List<ICollideableObject> level) : base(game)
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
				if (playerRect.Intersects(levelRect) && playerRect.Right >= levelRect.Left && playerRect.Top > levelRect.Top)
				{
					player.rightCollision = true;
					player.Position.X = levelRect.Left + playerRect.Width;
				}
				else if (playerRect.Intersects(levelRect) && playerRect.Left <= levelRect.Right && playerRect.Top > levelRect.Top)
				{
					player.leftCollision = true;
					player.Position.X = levelRect.Right;
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
