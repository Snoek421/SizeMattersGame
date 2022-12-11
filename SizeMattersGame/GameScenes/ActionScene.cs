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
        private List<CollideableObject> _collideables;
        private List<CollideableObject> _borders;
        private List<CollideableObject> _level1;
        private List<CollideableObject> _level2;
        private LevelManager levelManager;
        private int currentLevel = 1;
        private SpriteFont regularFont, highlightFont;
        private Vector2 messagePosition;


        //added elements
        private gameObject door;
        private gameObject button;

        InteractableObjectCollision test;
        public bool stageCleared = false;


        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;
            spriteBatch = g._spriteBatch;

            _collideables = new List<CollideableObject>();
            _borders = new List<CollideableObject>();
            _level1 = new List<CollideableObject>();
            _level2 = new List<CollideableObject>();

            //ready fonts
            SpriteFont regular = g.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont hilight = g.Content.Load<SpriteFont>("fonts/highlightFont");
            regularFont = regular;
            highlightFont = hilight;
            messagePosition = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);

            //player character
            Texture2D playerTex = game.Content.Load<Texture2D>("images/SizeSpriteSheet");
            SoundEffect jumpSound = game.Content.Load<SoundEffect>("sound/jumpSound");
            Vector2 playerPosition = new Vector2(0 + 65, Shared.stage.Y / 2);
            player = new Player(game, spriteBatch, playerTex, playerPosition, jumpSound);
            this.components.Add(player);
            _collideables.Add(player);

            //blocks for level
            Texture2D blockTex = game.Content.Load<Texture2D>("images/testblock"); //load block texture
            levelManager = new LevelManager(blockTex); //create level instance to control level position lists
            levelManager.CreateLevels();
            foreach (var borderBlock in levelManager.borders)
            {
                Block block = new Block(game, spriteBatch, blockTex, borderBlock);
                _borders.Add(block);
                this.components.Add(block);
                _collideables.Add(block);
            }

            foreach (var levelBlock in levelManager.Level1)
            {
                Block block = new Block(game, spriteBatch, blockTex, levelBlock);
                _level1.Add(block);
                this.components.Add(block);
                _collideables.Add(block);
            }

            foreach (var levelBlock in levelManager.Level2)
            {
                Block block = new Block(game, spriteBatch, blockTex, levelBlock);
                _level2.Add(block);
            }


            Texture2D objectTex = game.Content.Load<Texture2D>("images/ObjectSheet");
            Vector2 objPos = new Vector2(Shared.stage.X - 51, Shared.stage.Y - 108);
            door = new gameObject(game, spriteBatch, objectTex, objPos, 1);
            components.Add(door);

            objectTex = game.Content.Load<Texture2D>("images/ObjectSheet");
            objPos = new Vector2(Shared.stage.X - 200, Shared.stage.Y - 108);
            button = new gameObject(game, spriteBatch, objectTex, objPos, 5);
            components.Add(button);

            test = new InteractableObjectCollision(game, player, door, button, this);
            components.Add(test);

            //CollisionManager = new CollisionManager(game, player, list of buttons and batteries and door);
            //components.Add(CollisionManager);
        }

        private void clearLevel()
        {
            this.components.Clear();
            this._collideables.Clear();
        }

        private void addMainComponents()
        {
            stageCleared = false;
            this.button.isActive = false;
            this.door.isActive = false;
            this.components.Add(player);
            this.components.Add(button);
            this.components.Add(door);
            this.components.Add(test);
            this._collideables.Add(player);
            foreach (var block in _borders)
            {
                this.components.Add(block);
                this._collideables.Add(block);
            }
        }

        protected override void LoadContent()
        {


        }

        public override void Update(GameTime gameTime)
        {
            bool levelChanged = false;
            foreach (var collideable in _collideables)
            {
                collideable.Update(gameTime, _collideables);
            }
            base.Update(gameTime);



            if (stageCleared == true)
            {
                player.Position = new Vector2(0 + 65, Shared.stage.Y / 2);
                if (currentLevel == 1 && levelChanged == false)
                {
                    clearLevel();
                    addMainComponents();
                    foreach (var levelBlock in _level2)
                    {
                        this.components.Add(levelBlock);
                        this._collideables.Add(levelBlock);
                    }
                    currentLevel = 2;
                    levelChanged = true;
                }
                if (currentLevel == 2 && levelChanged == false)
                {
                    clearLevel();
                    addMainComponents();
                    levelChanged = true;
                }


            }

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (var collideable in _collideables)
            {
                collideable.Draw(gameTime);
            }

            if (currentLevel == 3)
            {
               
            }
            base.Draw(gameTime);
        }
    }
}
