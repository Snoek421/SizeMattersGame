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

    public class CollisionManager : GameComponent
	{
		public Player player;
		public List<CollideableObject> interactables;

		public CollisionManager(Game game, Player player, List<CollideableObject> interactables) : base(game)
		{
			this.player = player;
			this.interactables = interactables;
		}

		public override void Update(GameTime gameTime)
		{
			Rectangle playerRect = player.GetBounds();


			base.Update(gameTime);
		}

	}
}
