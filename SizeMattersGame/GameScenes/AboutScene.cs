using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SizeMattersGame.GameScenes
{
    public class AboutScene : GameScene
    {
        private SpriteBatch spritebatch;
        private Texture2D tex;
        private Game1 g;

        public AboutScene(Game game) : base(game)
        {
            g = (Game1)game;
            spritebatch = g._spriteBatch;
            tex = g.Content.Load<Texture2D>("images/credits");
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
