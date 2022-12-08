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
        private TopCollisionManager topCollisionManager;
        private SideCollisionManager sideCollisionManager;
        private List<ICollideableObject> levelBlocks = new List<ICollideableObject>();



        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;
            spriteBatch = g._spriteBatch;

            //music 
            Song bgmSong = g.Content.Load<Song>("sound/bgmSong"); //downloaded from https://freemusicarchive.org/music/eggy-toast/game-music/7mp3/

            //player character
            Texture2D playerTex = game.Content.Load<Texture2D>("images/SizeSpriteSheet");
            SoundEffect jumpSound = game.Content.Load<SoundEffect>("sound/jumpSound");
            Vector2 playerPosition = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
            player = new Player(game, spriteBatch, playerTex, playerPosition, jumpSound);
            components.Add(player);

            //block
            Texture2D blockTex = game.Content.Load<Texture2D>("images/testblock");
            Vector2 block1Pos = new Vector2(Shared.stage.X / 2 - blockTex.Width, Shared.stage.Y - blockTex.Height - 30);
            Block block = new Block(game, spriteBatch, blockTex, block1Pos);
            components.Add(block);
            levelBlocks.Add(block);

            sideCollisionManager = new SideCollisionManager(game, player, levelBlocks);
            topCollisionManager = new TopCollisionManager(game, player, levelBlocks);
            components.Add(sideCollisionManager);
            components.Add(topCollisionManager);

            if (Enabled)
            {
                //play bgm
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(bgmSong);
            }
        }

        protected override void LoadContent()
        {


        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
