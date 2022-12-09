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
    public class StartScene : GameScene
    {

        //menu component needs to be created
        public MenuComponent menu { get; set; }
        private Game1 g;
        private SpriteBatch spriteBatch;
        private string[] menuItems = { "Start game", "Help", "High score", "About", "Quit" };

        public StartScene(Game game) : base(game)
        {
            g = (Game1)game;
            spriteBatch = g._spriteBatch;
            SpriteFont regular = g.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont hilight = g.Content.Load<SpriteFont>("fonts/highlightFont");
            menu = new MenuComponent(game, spriteBatch, regular, hilight, menuItems);
            components.Add(menu);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
