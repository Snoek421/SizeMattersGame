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
        private SpriteBatch playerBatch;

        //sound
        private SoundEffect jumpSound; //downloaded from freesound.org https://freesound.org/s/369515/
        private const float VOLUME = 0.15f;
        private const float PITCH = 0.05f;
        private const float PAN = 0.5f;

        private int x;
        private int y;


        private const int JUMP_VELOCITY = 4; //allows for a bit more of a floaty jump
        private bool hasJumped = false; //controls the flow of jump logic
        private int jumpTimer = 0;
        private const int MAX_JUMP_TIME = 15; //variables to control jump height
        private const int JUMP_STRENGTH = 7; //initial position change on jump to make it feel better
        private const float GRAVITY = 0.4f;
        private const int SPEED = 5; //speed const for movement logic
        private const float SCALE = 3f;

        private float rotation;

        public bool leftCollision = false;
        public bool rightCollision = false;
        public bool upCollision = false;
        public bool downCollision = false;

        private Vector2 origin;
        public Rectangle playerBox;

        private Game game;

        private Vector2 dimension = new Vector2(18, 18);
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        private const int ROWS = 7;
        private const int COLS = 5;

        private bool change = false;

        public enum buttonState
        {
            leftPressed,
            rightPressed,
            leftReleased,
            rightReleased
        }

        public Player(Game game, SpriteBatch playerBatch, Texture2D tex, Vector2 position, SoundEffect jump) : base(game)
        {
            this.playerBatch = playerBatch;
            this.tex = tex;
            this.Position = position;

            x = (int)position.X;
            y = (int)position.Y;

            origin = new Vector2(tex.Width / ROWS, tex.Height / COLS);   //(tex.Width / 2, tex.Height / 2);
            //playerBox = new Rectangle(x, y, tex.Width / ROWS, tex.Height / COLS);
            //rotation = 0;
            CreateFrames();

            this.game = game;
            jumpSound = jump;
        }

        public void ResetCollision()
        {
            leftCollision = false;
            rightCollision = false;
            upCollision = false;
            downCollision = false;
        }

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

        private void hide()
        {
            Enabled = false;
            Visible = false;
        }
        private void show()
        {
            Enabled = true;
            Visible = true;
        }

        public void restart()
        {
            frameIndex = 0;
            delayCounter = 0;
            show();
        }

        private void move()
        {
            KeyboardState ks = Keyboard.GetState();
            if(ks.IsKeyDown(Keys.Left))
            {
                base.Velocity.X = -SPEED;
            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                Velocity.X = SPEED;
            }

        }

        bool pressed = false;
        buttonState oldState;
        KeyboardState oldKsState; //was trying to use this to prevent bouncing after landing
        bool formChange = false;

        public override void Update(GameTime gameTime, List<CollideableObject> collideables)
        {
            KeyboardState ks = Keyboard.GetState();


            if (ks.IsKeyDown(Keys.Space))
            {
                if (oldKsState.IsKeyUp(Keys.Space) && hasJumped == false)
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


            //if (hasJumped == true)
            //{
            //	velocity.Y += GRAVITY * 1;
            //}
            //if (position.Y >= 0 + playerBox.Height || downCollision == true)
            //{
            //	hasJumped = false;
            //}
            //if (hasJumped == false || downCollision == true)
            //{
            //	velocity.Y = 0;
            //	jumpTimer = 0;
            //}
            //if (downCollision == false)
            //{
            //	velocity.Y += GRAVITY * 1;
            //}
            if (Position.Y >= Shared.stage.Y - playerBox.Height)
            {
                Velocity.Y = 0;
                jumpTimer = 0;
                hasJumped = false;
            }
            if (Position.Y <= Shared.stage.Y - playerBox.Height && hasJumped == true)
            {
                Velocity.Y += GRAVITY * 1;
            }


            if (ks.IsKeyDown(Keys.Left) )
            {
                if (pressed == false)
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
                Velocity.X = -SPEED; //Allows the player's horizontal movement to be independent of the vertical movement
                oldState = buttonState.leftPressed;

            }
            else if (ks.IsKeyUp(Keys.Left) && oldState == buttonState.leftPressed)
            {
                restart();
                pressed = false;
                oldState = buttonState.leftReleased;
                Velocity.X = 0;

            }

            if (ks.IsKeyDown(Keys.Right) && rightCollision != true)
            {
                leftCollision = false;
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
                Velocity.X = SPEED; //Allows the player's horizontal movement to be independent of the vertical movement
                oldState = buttonState.rightPressed;
            }
            else if (ks.IsKeyUp(Keys.Right) && oldState == buttonState.rightPressed)
            {
                restart();
                pressed = false;
                oldState = buttonState.rightReleased;
                Velocity.X = 0;
            }

            if (ks.IsKeyDown(Keys.W))
            {
                restart();
                pressed = false;
                //tex = game.Content.Load<Texture2D>("images/SizeLargeText");
                //playerBox = new Rectangle(x, y, tex.Width, tex.Height);               
                //rotation = (float)Math.PI;
            }

            if (ks.IsKeyDown(Keys.S))
            {
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
                        if (frameIndex < 28)
                        {
                            frameIndex++;
                            delayCounter = 0;
                            if (frameIndex == 15)
                            {
                                frameIndex = 24;
                            }

                            if (frameIndex == 28)
                            {
                                //frameIndex = 0;
                                //scale = 1f;
                                //position = new Vector2(x, y);
                            }
                        }

                    }


                }
            }

            if (ks.IsKeyDown(Keys.A))
            {
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
                        }

                    }
                }
            }

            if (ks.IsKeyDown(Keys.D))
            {
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

                    }
                }
            }

            if (pressed == false && ks.IsKeyUp(Keys.Left)
                && ks.IsKeyUp(Keys.Up) && ks.IsKeyUp(Keys.Down)
                && ks.IsKeyUp(Keys.Right))
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

            foreach (var collideable in collideables)
            {
                if (collideable == this)
                {
                    continue;
                }

                if (this.Velocity.Y > 0 && this.CollidingBottom(collideable))
                {
                    this.Velocity.Y = 0;
                }
                if (this.Velocity.Y < 0 && this.CollidingTop(collideable))
                {
                    this.Velocity.Y = 0;
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


            Position += Velocity;
            oldKsState = ks;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0)
            {
                playerBatch.Begin();
                playerBatch.Draw(tex, Position, frames[frameIndex], Color.White, rotation, origin, SCALE, SpriteEffects.None, 1);
                playerBatch.End();
            }

            base.Draw(gameTime);
        }

    }
}
