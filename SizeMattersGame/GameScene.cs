using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate
{
	public abstract class GameScene : DrawableGameComponent
	{
		//private Game1 g;
		//private SpriteBatch spriteBatch;

		public List<GameComponent> components { get; set; }
		public virtual void show()
		{
			this.Visible = true;
			this.Enabled = true;
		}

		public virtual void hide()
		{
			this.Visible = false;
			this.Enabled = false;
		}

		public GameScene(Game game) : base(game)
		{
			components = new List<GameComponent>();
			hide();
		}

		public override void Update(GameTime gameTime)
		{
			foreach (GameComponent component in components)
			{
				if (component.Enabled)
				{
					component.Update(gameTime);
				}
			}
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			foreach (GameComponent item in components)
			{

				if (item is DrawableGameComponent)
				{
					DrawableGameComponent comp = (DrawableGameComponent)item;
					if (comp.Visible)
					{
						comp.Draw(gameTime);
					}
				}
			}
			base.Draw(gameTime);
		}
	}
}
