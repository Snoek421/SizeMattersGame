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

        //player entity
        private Player player;
        private Vector2 playerStartPos = new Vector2(0 + 65, Shared.stage.Y / 2);
        private const double INIT_RESET_TIMER = 2;
        private double resetMessageTimer = 0;

        //lists for loading levels and calculating collision
        private List<CollideableObject> _collideables;
        private List<CollideableObject> _borders;
        private List<CollideableObject> _level1;
        private List<CollideableObject> _level2;
        private LevelManager levelManager;
        public int currentLevel = 1;

        //stuff for score
        private const double INIT_TIMER = 10;
        private double _timer = INIT_TIMER;
        private int secondTimer;
        private int previousSecond;
        private SpriteFont regularFont, highlightFont;
        private Vector2 timerPosition = new Vector2(70, 70);
        private Vector2 scorePosition = new Vector2(Shared.stage.X - 300, 70);
        private Vector2 centerScreen = new Vector2(Shared.stage.X / 2 - 200, Shared.stage.Y / 2 - 100);

        //stuff to store the scores for different levels
        private int firstLevelScore = 0;
        private int secondLevelScore = 0;


        //added elements
        private gameObject door;
        private gameObject button;
        private gameObject battery;
        public int batteriesCollected = 0;

        InteractableObjectCollision collisionManager;

        //stage transition control
        public bool stageCompleted = false;
        private bool scoreScreen = false;


        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;
            spriteBatch = g._spriteBatch;

            //lists for loading level layouts and managing collision
            _collideables = new List<CollideableObject>();
            _borders = new List<CollideableObject>();
            _level1 = new List<CollideableObject>();
            _level2 = new List<CollideableObject>();

            //ready fonts for displaying congratulations and scores
            SpriteFont regular = g.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont hilight = g.Content.Load<SpriteFont>("fonts/highlightFont");
            regularFont = regular;
            highlightFont = hilight;

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
            //load level 1
            foreach (var levelBlock in levelManager.Level1)
            {
                Block block = new Block(game, spriteBatch, blockTex, levelBlock);
                _level1.Add(block);
                this.components.Add(block);
                _collideables.Add(block);
            }
            //load level 2 into a list for future loading into components
            foreach (var levelBlock in levelManager.Level2)
            {
                Block block = new Block(game, spriteBatch, blockTex, levelBlock);
                _level2.Add(block);
            }




            //door
            Texture2D objectTex = game.Content.Load<Texture2D>("images/ObjectSheet");
            door = new gameObject(game, spriteBatch, objectTex, levelManager.Level1Objects[0], 1);
            components.Add(door);
            //button
            button = new gameObject(game, spriteBatch, objectTex, levelManager.Level1Objects[1], 5);
            components.Add(button);

            //battery
            //start the first battery out of bounds, to let there be a battery in level 2 but not in level 1
            battery = new gameObject(game, spriteBatch, objectTex, new Vector2(-100, -100), 0);

            //collision manager for objects
            collisionManager = new InteractableObjectCollision(game, player, door, button, battery, this);
            components.Add(collisionManager);

        }

        /// <summary>
        /// Clears the components list and the collideables list
        /// </summary>
        private void clearLevel()
        {
            this.components.Clear();
            this._collideables.Clear();
        }

        /// <summary>
        /// Adds only the border blocks to the current screen
        /// </summary>
        private void addBorders()
        {
            foreach (var block in _borders)
            {
                this.components.Add(block);
                this._collideables.Add(block);
            }
        }

        /// <summary>
        /// Adds and resets the main core things required for each level. The button, door, border blocks, player, InteractableObjectCollision, and set up the collideables list
        /// </summary>
        private void addMainComponents()
        {
            _timer = INIT_TIMER;
            player.Score = 0;
            batteriesCollected = 0;
            stageCompleted = false;
            this.button.isActive = false;
            this.door.isActive = false;
            this.battery.isActive = false;
            this.battery.Visible = true;
            this.components.Add(player);
            this.components.Add(collisionManager);
            this._collideables.Add(player);
            addBorders();
        }

        /// <summary>
        /// Clears and resets all required variables, then loads the first level to be played
        /// </summary>
        public void LoadLevel1()
        {
            stageCompleted = false;
            clearLevel();
            addMainComponents();
            foreach (var levelBlock in _level1)
            {
                this.components.Add(levelBlock);
                this._collideables.Add(levelBlock);
            }
            collisionManager.resetFirstTime();
            scoreScreen = false;
            firstLevelScore = 0;
            secondLevelScore = 0;
            player.Position = playerStartPos;
            door.position = levelManager.Level1Objects[0];
            button.position = levelManager.Level1Objects[1];
            collisionManager.door = door;
            collisionManager.button = button;
            this.components.Add(door);
            this.components.Add(button);
        }


        /// <summary>
        /// Clears and resets all required variables, then loads the second level to be played
        /// </summary>
        private void LoadLevel2()
        {
            clearLevel();
            addMainComponents();
            foreach (var levelBlock in _level2)
            {
                this.components.Add(levelBlock);
                this._collideables.Add(levelBlock);
            }
            collisionManager.resetFirstTime();
            scoreScreen = false;
            player.Position = playerStartPos;
            door.position = levelManager.Level2Objects[0];
            button.position = levelManager.Level2Objects[1];
            battery.position = levelManager.Level2Objects[2];
            collisionManager.door = door;
            collisionManager.button = button;
            collisionManager.battery = battery;
            this.components.Add(door);
            this.components.Add(button);
            this.components.Add(battery);
        }

        protected override void LoadContent()
        {


        }

        public override void Update(GameTime gameTime)
        {
            
            bool levelChanged = false; //bool for only allowing one level change at a time
            foreach (var collideable in _collideables) //run update for player and block collision logic
            {
                collideable.Update(gameTime, _collideables);
            }

            //if score screen is active, set player score to correct number and commit that score to memory
            if (scoreScreen)
            {
                player.Score = secondTimer * 200 + batteriesCollected * 1500;
                if (currentLevel == 1)
                {
                    firstLevelScore = player.Score;
                }
                if (currentLevel == 2)
                {
                    secondLevelScore = player.Score;
                }
            }
            else //else, run the timer to be displayed on screen and reset the level when timer runs out
            {
                _timer -= gameTime.ElapsedGameTime.TotalSeconds;
                secondTimer = (int)Math.Round(_timer);
                resetMessageTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            //if level was finished and player is on score screen...
            if (stageCompleted == true && scoreScreen == true) //wait for user to press enter before progressing to next level, so user can see score screen
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter)) //when user pushes enter, set score screen to false, change stage completed to false, reset player position...
                {
                    scoreScreen = false;
                    stageCompleted = false;
                    if (currentLevel == 1 && levelChanged == false) //and progress level by one based on what level is active
                    {
                        LoadLevel2();
                        currentLevel = 2;
                        levelChanged = true;
                    }
                    if (currentLevel == 2 && levelChanged == false) //if second level is active, clear level and add border blocks and player
                    {
                        clearLevel();
                        addMainComponents();
                        // then set conditions so congrats text is shown
                        currentLevel = 3;
                        levelChanged = true;
                        scoreScreen = true;
                    }
                }
            }

            //if stage is completed and score screen not up, set score screen to be shown in empty level with only borders
            if (stageCompleted == true)
            {
                if (scoreScreen == false)
                {
                    clearLevel();
                    addBorders();
                    scoreScreen = true;
                }
            }

            //if timer runs out, reload level
            if (secondTimer <= 0)
            {
                switch (currentLevel)
                {
                    case 1:
                        LoadLevel1();
                        resetMessageTimer = INIT_RESET_TIMER;
                        break;
                    case 2:
                        LoadLevel2();
                        resetMessageTimer = INIT_RESET_TIMER;
                        break;
                    default:
                        break;
                }
            }

            base.Update(gameTime);
        }



        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (var collideable in _collideables) //run draw function of all collideable entities
            {
                collideable.Draw(gameTime);
            }

            if (resetMessageTimer > 0)
            {
                spriteBatch.DrawString(highlightFont, "Time Up!", new Vector2(Shared.stage.X / 2 - 100, Shared.stage.Y /2), Color.Red);
            }
            if (scoreScreen && currentLevel != 3) //if score screen should be shown and the current level isn't 3 (the congrats screen) then show score for the level
            {
                string scoreScreenMessage = $"Your score was: {player.Score} \nTimer: {secondTimer} x 200 = {secondTimer * 200}\nBatteries: {batteriesCollected} x 1500\n\nPress enter to go to the next level";
                spriteBatch.DrawString(regularFont, scoreScreenMessage, centerScreen, Color.Maroon);

            }
            else if (scoreScreen && currentLevel == 3) //if score screen should be shown but the current level is the congrats screen then show congrats message with both level's scores
            {
                string scoreScreenMessage = $"Congratulations, you completed the currently existing levels!\n" +
                    $"Your score on the first level was {firstLevelScore}!\n" +
                    $"Your score on the second level was {secondLevelScore}!\n\n" +
                    $"Press escape to go back to the main menu.";
                spriteBatch.DrawString(regularFont, scoreScreenMessage, new Vector2(75, 100), Color.DarkRed);
            }
            else //otherwise, draw the score and timer
            {
                if (secondTimer != previousSecond) //if the timer is different from its previous value, then draw the new value
                {
                    spriteBatch.DrawString(highlightFont, $"Time: {secondTimer}", timerPosition, Color.Black);
                    spriteBatch.DrawString(highlightFont, $"Score: {player.Score}", scorePosition, Color.Black);
                }
                else //otherwise, draw the previous value until the second has fully passed and the next second is active
                {
                    spriteBatch.DrawString(highlightFont, $"Time: {previousSecond}", timerPosition, Color.Black);
                    spriteBatch.DrawString(highlightFont, $"Score: {player.Score}", scorePosition, Color.Black);
                }
            }


            base.Draw(gameTime);
            spriteBatch.End();
            previousSecond = secondTimer; //take current second based timer value and put it into the previous second variable to compare against
        }
    }
}
