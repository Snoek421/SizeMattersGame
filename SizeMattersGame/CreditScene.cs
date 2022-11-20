using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTemplate
{
	public class CreditScene : GameScene
	{
		private SpriteBatch spritebatch;
		private Texture2D tex;
		private Game1 g;

		public CreditScene(Game game) : base(game)
		{
			g = (Game1)game;
			this.spritebatch = g._spriteBatch;
			this.tex = g.Content.Load<Texture2D>("images/credits");
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			spritebatch.Begin();
			spritebatch.Draw(tex, Vector2.Zero, Color.White);
			spritebatch.End();
			base.Draw(gameTime);
		}
	}
}
