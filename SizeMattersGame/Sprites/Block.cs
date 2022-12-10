using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeMattersGame.Sprites
{
    public class Block : CollideableObject
    {
        public SpriteBatch spriteBatch { get; set; }
        public Game1 g1 {get; set;}


        public Block(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position) : base(game)
        {
            this.g1 = (Game1)game;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            Enabled = true;
            Visible = true;
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Position, Color.White);
            if (ShowRectangle)
            {
                this.SetRectangleTexture(g1.GraphicsDevice, this.GetBounds());
                if (_rectangleTexture != null)
                {
                    spriteBatch.Draw(_rectangleTexture, Position, Color.Red);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
