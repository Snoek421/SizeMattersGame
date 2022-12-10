using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SizeMattersGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeMattersGame
{

    public class TopCollisionManager : GameComponent
	{
		public Player player;
		public List<ICollideableObject> level;
		private Rectangle levelRect;

		public TopCollisionManager(Game game, Player player, List<ICollideableObject> level) : base(game)
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
				if (playerRect.Intersects(levelRect) && playerRect.Bottom >= levelRect.Top && playerRect.Top < levelRect.Bottom)
				{
					player.Position.Y = levelRect.Top - playerRect.Height;
					player.downCollision = true;
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
