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
    { //mainly for blocks and the player, to allow the player to have collision with them

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
            tex = texture; //texture is needed for getting the bounding rectangle
        }



        public virtual Rectangle GetBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, tex.Width, tex.Height);
        }

        public bool CollidingBottom(CollideableObject sprite)
        {
            Rectangle rectangle = this.GetBounds(); //player
            Rectangle spriteBox = sprite.GetBounds(); //block
            if (rectangle.Bottom + this.Velocity.Y > spriteBox.Top && //if player's bottom will intersect with the Block's top with the current velocity
                rectangle.Top < spriteBox.Top && 
                rectangle.Right > spriteBox.Left &&
                rectangle.Left < spriteBox.Right) //and the player is above the block
            {
                return true; //return true, meaning the player will collide with the block while travelling downwards
            }
            else
            {
                return false; //else return false, meaning 
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
    }
}
