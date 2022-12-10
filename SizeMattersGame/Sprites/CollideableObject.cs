using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SizeMattersGame.Sprites
{
	public class CollideableObject : DrawableGameComponent, ICollideableObject
	{

        public Game1 _game;
        public Texture2D tex;
		public Vector2 Position;
		public Vector2 Velocity;

        public bool ShowRectangle { get; set; } = true;

        protected Texture2D _rectangleTexture;

        public CollideableObject(Game game) : base(game)
        {
        }

        public CollideableObject(Game game, Texture2D texture) : base(game)
        {
            tex = texture;
        }



        public virtual Rectangle GetBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, tex.Width, tex.Height);
        }

        public bool CollidingBottom(CollideableObject sprite)
        {
            Rectangle rectangle = this.GetBounds();
            Rectangle spriteBox = sprite.GetBounds();
            if (rectangle.Bottom + this.Velocity.Y > spriteBox.Top &&
                rectangle.Top < spriteBox.Top &&
                rectangle.Right > spriteBox.Left &&
                rectangle.Left < spriteBox.Right)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CollidingTop(CollideableObject sprite)
        {
            Rectangle rectangle = this.GetBounds();
            Rectangle spriteBox = sprite.GetBounds();
            if (rectangle.Top + this.Velocity.Y < spriteBox.Bottom &&
                rectangle.Bottom > spriteBox.Bottom &&
                rectangle.Right > spriteBox.Left &&
                rectangle.Left < spriteBox.Right)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CollidingLeft(CollideableObject sprite)
        {
            Rectangle rectangle = this.GetBounds();
            Rectangle spriteBox = sprite.GetBounds();
            if (rectangle.Right + this.Velocity.X > spriteBox.Left &&
                rectangle.Left < spriteBox.Left &&
                rectangle.Bottom - 5 > spriteBox.Top &&
                rectangle.Top < spriteBox.Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CollidingRight(CollideableObject sprite)
        {
            Rectangle rectangle = this.GetBounds();
            Rectangle spriteBox = sprite.GetBounds();
            if (rectangle.Left + this.Velocity.X < spriteBox.Right &&
                rectangle.Right > spriteBox.Right &&
                rectangle.Bottom - 5 > spriteBox.Top &&
                rectangle.Top < spriteBox.Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Update(GameTime gameTime, List<CollideableObject> collideables)
        {
        }

        /// <summary>
        /// stuff for debugging
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="texture"></param>
        public void SetRectangleTexture(GraphicsDevice graphicsDevice, Rectangle texture)
        {
            var colours = new List<Color>();

            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    if (y == 0 || // On the top
                        x == 0 || // On the left
                        y == texture.Height - 1 || // on the bottom
                        x == texture.Width - 1) // on the right
                    {
                        colours.Add(new Color(255, 255, 255, 255)); // white
                    }
                    else
                    {
                        colours.Add(new Color(0, 0, 0, 0)); // transparent 
                    }
                }
            }

            _rectangleTexture = new Texture2D(graphicsDevice, texture.Width, texture.Height);
            _rectangleTexture.SetData<Color>(colours.ToArray());
        }


    }
}
