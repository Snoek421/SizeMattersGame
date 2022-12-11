using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using static System.Net.Mime.MediaTypeNames;

namespace SizeMattersGame
{
    public class gameObject : DrawableGameComponent
    {
        private Game game;

        private SpriteBatch objectBatch;
        public Texture2D objectTexture;
        private Vector2 origin;

        public Vector2 position;
        private int x;
        private int y;

        public Rectangle objectRect { get
            {
                return new Rectangle((int)position.X, (int)position.Y, objectTexture.Width / ROWS, objectTexture.Height / COLS);
            }
        }

        private Vector2 dimension = new Vector2(18, 18);
        private List<Rectangle> sprites;
        private int objectSprite;
        private int ObjectIndex = 0;
        private const int ROWS = 2;
        private const int COLS = 4;

        private int baseSprite;
        private int count;
        public bool isActive;

        public gameObject(Game game, SpriteBatch playerBatch, Texture2D tex, Vector2 position, int sprite) : base(game)
        {
            this.objectBatch = playerBatch;
            this.objectTexture = tex;
            this.position = position;

            this.x = (int)position.X;
            this.y = (int)position.Y;

            origin = new Vector2(2, 1);
            //objectRect = new Rectangle(x, y, tex.Width / ROWS, tex.Height / COLS);

            this.baseSprite = sprite;
            objectSprite = sprite;

            CreateFrames();

            this.game = game;
        }

        private void CreateFrames()
        {
            sprites = new List<Rectangle>();
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    sprites.Add(r);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (isActive == true && count == 0)
            {
                count++;
                objectSprite++;
            }

            if (isActive == false)
            {
                objectSprite = baseSprite;
                count = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            objectBatch.Draw(objectTexture, position, sprites[objectSprite], Color.White, 0, origin, 3f, SpriteEffects.None, 1);

            base.Draw(gameTime);
        }
    }
}
