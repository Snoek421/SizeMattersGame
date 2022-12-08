using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeMattersGame
{
	public class Block : DrawableGameComponent, ICollideableObject
	{
		public SpriteBatch spriteBatch { get; set; }
		public Texture2D tex { get; set; }
		public Vector2 position { get; set; }


		public Block(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position) : base(game)
		{
			this.spriteBatch = spriteBatch;
			this.tex = tex;
			this.position = position;
		}

		public override void Update(GameTime gameTime)
		{
			this.Enabled = true;
			this.Visible = true;
			base.Update(gameTime);
		}


		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(tex, position, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}

		public Rectangle GetBounds()
		{
			return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
		}
	}
}
