using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SizeMattersGame.Sprites
{
    public class Player : CollideableObject
    {
        private Game1 g1;
        private SpriteBatch playerBatch;

        //variable for score
        public int Score { get; set; }

        //sound
        private SoundEffect jumpSound; //downloaded from freesound.org https://freesound.org/s/369515/
        private const float VOLUME = 0.15f;
        private const float PITCH = 0.05f;
        private const float PAN = 0.5f;


        //variables to control jump
        private const int JUMP_VELOCITY = 4; //allows for a bit more of a floaty jump
        private bool hasJumped = false; //controls the flow of jump logic
        private int jumpTimer = 0;
        private const int MAX_JUMP_TIME = 15; //variables to control jump height
        private const int JUMP_STRENGTH = 7; //initial position change on jump to make it feel better
        private const float GRAVITY = 0.4f;
        private const int SPEED = 4; //speed const for movement logic
        
        //variables to control
        private const float SCALE = 3f;
        private Vector2 origin;

        //varibles for the animation
        private Vector2 dimension = new Vector2(18, 18);
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delayCounter;
        private const int ROWS = 7;
        private const int COLS = 5;

        //variable for the "form" Change
        private bool formChange = false;

        //variables to smooth controls and animation
        public enum buttonState
        {
            rightPressed,
            leftPressed,
            rightReleased,
            leftReleased
            
        }
        bool pressed = false;
        buttonState oldState;
        bool transformation = false;
        string form = "";


        public Player(Game game, SpriteBatch playerBatch, Texture2D tex, Vector2 position, SoundEffect jump) : base(game)
        {
            this.playerBatch = playerBatch;
            this.tex = tex;
            this.Position = position;

            origin = new Vector2(2, 1);
            CreateFrames();

            this.g1 = (Game1)game;
            jumpSound = jump;
        }

        /// <summary>
        /// Creates the frames which are used for the animation
        /// </summary>
        private void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

        /// <summary>
        /// Restarts the values for the delaycounter and the animation frame
        /// </summary>
        public void restart()
        {
            frameIndex = 0;
            delayCounter = 0;
        }

        private int getSpriteSize()
        {
            float size = tex.Width / COLS * SCALE;
            int simpleSize = (int)Math.Round(size) - 7;
            return simpleSize;
        }

        public override void Update(GameTime gameTime, List<CollideableObject> collideables)
        {
            KeyboardState ks = Keyboard.GetState();

            //jump logic
            if (ks.IsKeyDown(Keys.Space))
            {
                if (hasJumped == false)
                {
                    jumpSound.Play(VOLUME, PITCH, PAN);
                }
                jumpTimer++;
                if (jumpTimer >= MAX_JUMP_TIME)
                {
                    Velocity.Y += GRAVITY * 1;
                }
                if (jumpTimer < MAX_JUMP_TIME)
                {
                    Position.Y -= JUMP_STRENGTH;
                    Velocity.Y = -JUMP_VELOCITY;
                    hasJumped = true;
                }
            }

            if (ks.IsKeyDown(Keys.Left) && ks.IsKeyUp(Keys.Right))
            {
                if (formChange == false) //if form is changed, don't animate
                {
                    if (pressed == false)//if left key is pressed then prepare to start animation
                    {
                        frameIndex = 8;
                        delayCounter = 0;
                        pressed = true;
                    }
                    else if (pressed == true)
                    {
                        delayCounter++;
                        if (delayCounter > 7)
                        {
                            if (frameIndex < 10)
                            {
                                frameIndex++;
                                delayCounter = 0;
                            }
                        }
                    }

                }
                Velocity.X = -SPEED; //Allows the player's horizontal movement to be independent of the vertical movement
                oldState = buttonState.leftPressed;

            }            
            else if (ks.IsKeyUp(Keys.Left) && oldState == buttonState.leftPressed)
            {
                if (formChange == false)
                {
                    restart();
                }
                pressed = false;
                oldState = buttonState.leftReleased;
                Velocity.X = 0;

            }
            else if (ks.IsKeyUp(Keys.Left) && ks.IsKeyDown(Keys.Right) && oldState == buttonState.leftPressed)
            {
                if (formChange == false)
                {
                    restart();
                }
                pressed = false;
                oldState = buttonState.rightPressed;
                Velocity.X = 0;
            }


            if (ks.IsKeyDown(Keys.Right) && ks.IsKeyUp(Keys.Left))
            {
                if (formChange == false)
                {
                    if (pressed == false)
                    {
                        frameIndex = 5;
                        delayCounter = 0;
                        pressed = true;
                    }
                    else if (pressed == true)
                    {
                        delayCounter++;
                        if (delayCounter > 7)
                        {
                            if (frameIndex < 7)
                            {
                                frameIndex++;
                                delayCounter = 0;
                            }
                        }
                    }
                }                
                Velocity.X = SPEED; //Allows the player's horizontal movement to be independent of the vertical movement
                oldState = buttonState.rightPressed;
            }
            else if (ks.IsKeyUp(Keys.Right) && oldState == buttonState.rightPressed)
            {
                if (formChange == false)
                {
                    restart();
                }
                pressed = false;
                oldState = buttonState.rightReleased;
                Velocity.X = 0;
            }            

            if (ks.IsKeyDown(Keys.W))
            {
                restart();
                formChange = false;
                pressed = false;
                //tex = game.Content.Load<Texture2D>("images/SizeLargeText");
                //playerBox = new Rectangle(x, y, tex.Width, tex.Height);               
                //rotation = (float)Math.PI;
            }

            if (ks.IsKeyDown(Keys.S))
            {
                form = "small";
                transformation = true;
            }

            if (ks.IsKeyDown(Keys.A))
            {
                form = "thin";
                transformation = true;
            }

            if (ks.IsKeyDown(Keys.D))
            {
                form = "thick";
                transformation = true;
            }

            if (transformation == true && form == "small")
            {
                formChange = true;

                if (pressed == false)
                {
                    frameIndex = 11;
                    pressed = true;
                }
                else if (pressed == true)
                {
                    delayCounter++;

                    if (delayCounter > 2)
                    {
                        if (frameIndex < 28)
                        {
                            frameIndex++;
                            
                            if (frameIndex == 15)
                            {
                                frameIndex = 24;
                            }

                            if (frameIndex == 28)
                            {
                                pressed = false;
                                transformation = false;
                            }
                        }

                        delayCounter = 0;
                    }
                }
            }
            else if (transformation == true && form == "thin")
            {
                formChange = true;

                if (pressed == false)
                {
                    frameIndex = 11;
                    delayCounter = 0;
                    pressed = true;
                }
                else if (pressed == true)
                {
                    delayCounter++;
                    if (delayCounter > 2)
                    {
                        if (frameIndex < 23)
                        {
                            frameIndex++;
                            delayCounter = 0;
                            if (frameIndex == 15)
                            {
                                frameIndex = 20;
                            }

                            if (frameIndex == 23)
                            {
                                pressed = false;
                                transformation = false;
                            }
                        }

                    }
                }
            }
            else if (transformation == true && form == "thick")
            {
                formChange = true;

                if (pressed == false)
                {
                    frameIndex = 11;
                    delayCounter = 0;
                    pressed = true;
                }
                else if (pressed == true)
                {
                    delayCounter++;
                    if (delayCounter > 2)
                    {
                        if (frameIndex < 19)
                        {
                            frameIndex++;
                            delayCounter = 0;
                        }

                        if (frameIndex == 19)
                        {
                            pressed = false;
                            transformation = false;
                        }

                    }
                }

            }

            if (pressed == false && ks.IsKeyUp(Keys.Left)
                && ks.IsKeyUp(Keys.Up) && ks.IsKeyUp(Keys.Down)
                && ks.IsKeyUp(Keys.Right) && formChange == false)
            {
                delayCounter++;
                if (delayCounter > 7)
                {
                    frameIndex++;
                    delayCounter = 0;
                    if (frameIndex > 4)
                    {
                        frameIndex = 0;
                        //hide();
                        restart();
                    }
                }
            }

            bool onGround = false;
            foreach (var collideable in collideables)
            {
                if (collideable == this)
                {
                    continue;
                }
                if (this.Velocity.Y < 0 && this.CollidingTop(collideable))
                {
                    this.Velocity.Y = 0;
                    jumpTimer = MAX_JUMP_TIME;
                }
                if (this.Velocity.Y >= 0 && this.CollidingBottom(collideable))
                {
                    int distance = collideable.GetBounds().Top - this.GetBounds().Bottom;

                    Velocity.Y = 0;

                    jumpTimer = 0;
                    hasJumped = false;
                    onGround = true;
                }
                if (this.Velocity.X > 0 && this.CollidingLeft(collideable))
                {
                    this.Velocity.X = 0;
                }
                if (this.Velocity.X < 0 && this.CollidingRight(collideable))
                {
                    this.Velocity.X = 0;
                }
            }
            if (!onGround)
            {
                Velocity.Y += GRAVITY * 1;
            }


            Position += Velocity;
            base.Update(gameTime);
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, getSpriteSize() - 5, getSpriteSize());
        }

        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0)
            {
                
                playerBatch.Draw(tex, Position, frames[frameIndex], Color.White, 0, origin, SCALE, SpriteEffects.None, 1);
                
            }

            base.Draw(gameTime);
        }

    }
}
