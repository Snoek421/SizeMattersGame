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
        public Texture2D tex;
		public Vector2 Position;
		public Vector2 Velocity;

        public CollideableObject(Game game) : base(game)
        {
        }

        public CollideableObject(Game game, Texture2D texture) : base(game)
        {
            tex = texture;
        }



        public Rectangle GetBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, tex.Width, tex.Height);
        }

        public bool CollidingTop(CollideableObject sprite)
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

        public bool CollidingBottom(CollideableObject sprite)
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
                rectangle.Bottom > spriteBox.Top &&
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
                rectangle.Bottom > spriteBox.Top &&
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
    }
}
