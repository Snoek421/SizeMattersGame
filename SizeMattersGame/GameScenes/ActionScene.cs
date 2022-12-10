using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SizeMattersGame.Sprites;
using static System.Net.Mime.MediaTypeNames;


namespace SizeMattersGame.GameScenes
{
    public class ActionScene : GameScene
    {
        private Game1 g;
        private SpriteBatch spriteBatch;

        private Player player;
        private CollisionManager CollisionManager;
        private List<CollideableObject> levelBlocks;
        private List<CollideableObject> _collideables;
        private Level level1;



        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;
            spriteBatch = g._spriteBatch;


            _collideables = new List<CollideableObject>();

            //player character
            Texture2D playerTex = game.Content.Load<Texture2D>("images/SizeSpriteSheet");
            SoundEffect jumpSound = game.Content.Load<SoundEffect>("sound/jumpSound");
            Vector2 playerPosition = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
            player = new Player(game, spriteBatch, playerTex, playerPosition, jumpSound);
            components.Add(player);
            _collideables.Add(player);

            //blocks for level
            Texture2D blockTex = game.Content.Load<Texture2D>("images/testblock");
            Vector2 block1Pos = new Vector2(Shared.stage.X / 2 - blockTex.Width, Shared.stage.Y - blockTex.Height - 50);
            Level level = new Level(blockTex);
            level.CreateLevel1();
            levelBlocks = new List<CollideableObject>();
            foreach (var levelBlock in level.level1)
            {
                Block block = new Block(game, spriteBatch, blockTex, levelBlock);
                components.Add(block);
                _collideables.Add(block);
                levelBlocks.Add(block);
            }


            //CollisionManager = new CollisionManager(game, player, list of buttons and batteries and door);
            //components.Add(CollisionManager);



        }

        protected override void LoadContent()
        {


        }

        public override void Update(GameTime gameTime)
        {
            foreach (var collideable in _collideables)
            {
                collideable.Update(gameTime, _collideables);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (var collideable in _collideables)
            {
                collideable.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
