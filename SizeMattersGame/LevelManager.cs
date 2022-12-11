using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SizeMattersGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeMattersGame
{
    public class LevelManager
    {
        public LevelManager(Texture2D tex)
        {
            this.Texture = tex;
        }

        public Texture2D Texture { get; set; }

        public List<Vector2> borders { get; private set; }

        public List<Vector2> Level1 { get; private set; }
        public List<Vector2> Level1Objects { get; private set; }


        public List<Vector2> Level2 { get; private set; }
        public List<Vector2> Level2Objects { get; private set; }

        public void CreateLevels()
        {
            if (borders == null)
            {
                borders = new List<Vector2>();
            }

            for (int j = 1; j < 10; j++)
            {
                for (int i = 0; i < 14; i++)
                {
                    if (j != 1 && j != 8)
                    {
                        if (i != 0 && i != 13)
                        {
                            continue;
                        }
                    }
                    borders.Add(new Vector2(0 + Texture.Width * i, Shared.stage.Y - Texture.Height * j));
                }
            }

            //level 1 contents blocks
            if (Level1 == null)
            {
                Level1 = new List<Vector2>();
                Level1Objects = new List<Vector2>();
            }
            else
            {
                Level1.Clear();
            }
            Level1.Add(new Vector2(0 + Texture.Width * 3, Shared.stage.Y - Texture.Height * 3));
            Level1.Add(new Vector2(0 + Texture.Width * 4, Shared.stage.Y - Texture.Height * 3));
            Level1.Add(new Vector2(0 + Texture.Width * 7, Shared.stage.Y - Texture.Height * 5));
            Level1.Add(new Vector2(0 + Texture.Width * 8, Shared.stage.Y - Texture.Height * 5));
            //door
            Level1Objects.Add(new Vector2(Shared.stage.X - Texture.Width * 2, Shared.stage.Y - 108));
            //button
            Level1Objects.Add(new Vector2(0 + Texture.Width * 8 + 15, Shared.stage.Y - Texture.Height* 6 + 10));

            //level 2 contents blocks
            if (Level2 == null)
            {
                Level2 = new List<Vector2>();
                Level2Objects = new List<Vector2>();
            }
            else
            {
                Level2.Clear();
            }
            for (int i = 5; i < 10; i++)
            {
                for (int j = 2; j <= 5; j++)
                {
                    if (j == 2 && i > 5)
                    {
                        //Level2.Add(new Vector2(0 + Texture.Width * i, Shared.stage.Y - Texture.Height * j));
                        continue;
                    }
                    else if (j == 3 && i > 6)
                    {
                        Level2.Add(new Vector2(0 + Texture.Width * i, Shared.stage.Y - Texture.Height * j));
                    }
                    else if(j == 4 && i >7)
                    {
                        Level2.Add(new Vector2(0 + Texture.Width * i, Shared.stage.Y - Texture.Height * j));
                    }
                    else if(j == 5  && i >8)
                    {
                        Level2.Add(new Vector2(0 + Texture.Width * i, Shared.stage.Y - Texture.Height * j));
                    }
                }
            }
            //door
            Level2Objects.Add(new Vector2(Shared.stage.X - Texture.Width * 2, Shared.stage.Y - 108));

            //button
            Level2Objects.Add(new Vector2(Shared.stage.X - Texture.Width * 2, Shared.stage.Y + 15 - Texture.Height * 6));

            //battery
            Level2Objects.Add(new Vector2(Shared.stage.X - 240, 0 + Texture.Height * 3));
        }

    }
}

