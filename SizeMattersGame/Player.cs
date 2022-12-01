using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace newGameProto
{
    public class Player : DrawableGameComponent
    {
        private SpriteBatch playerBatch;
        public Texture2D tex;

        private Vector2 position;
        private int x;
        private int y;

        private float rotation;
        private float scale = 4f;

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

        public Player(Game game, SpriteBatch playerBatch, Texture2D tex, Vector2 position) : base(game)
        {
            this.playerBatch = playerBatch;
            this.tex = tex;
            this.position = position;

            x = (int)position.X;
            y = (int)position.Y;

            this.origin = new Vector2(tex.Width / 2, tex.Height / 2);
            this.playerBox = new Rectangle(x, y, tex.Width, tex.Height);
            this.rotation = 0;
            CreateFrames();

            this.game = game;
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
            this.Enabled = false;
            this.Visible = false;
        }
        private void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        public void restart()
        {
            frameIndex = 0;
            delayCounter = 0;
            show();
        }

        bool pressed = false;
        buttonState oldState;     
        bool formChange = false;

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position = new Vector2(x, y);
                y -= 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position = new Vector2(x, y);
                y += 10;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (true)
                {

                }
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
                position = new Vector2(x, y);
                x -= 5;
                oldState = buttonState.leftPressed;

            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Left) && oldState == buttonState.leftPressed)
            {
                restart();
                pressed = false;
                oldState = buttonState.leftReleased;
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
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
                position = new Vector2(x, y);
                x += 5;
                oldState = buttonState.rightPressed;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Right) && oldState == buttonState.rightPressed)
            {
                restart();
                pressed = false;
                oldState = buttonState.rightReleased;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                restart();
                pressed = false;
                scale = 4f;
                //tex = game.Content.Load<Texture2D>("images/SizeLargeText");
                //playerBox = new Rectangle(x, y, tex.Width, tex.Height);               
                //rotation = (float)Math.PI;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
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

            if (Keyboard.GetState().IsKeyDown(Keys.A))
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

            if(Keyboard.GetState().IsKeyDown(Keys.D))
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

            if (pressed == false && Keyboard.GetState().IsKeyUp(Keys.Left)
                && Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down) 
                && Keyboard.GetState().IsKeyUp(Keys.Right))
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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0)
            {
                playerBatch.Begin();
                playerBatch.Draw(tex, position, frames[frameIndex], Color.White, rotation, origin, scale, SpriteEffects.None, 1);
                playerBatch.End();
            }      

            base.Draw(gameTime);
        }
    }
}
