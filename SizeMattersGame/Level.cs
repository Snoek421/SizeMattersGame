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
    public class Level
    {
        public Level(Texture2D tex)
        {
            this.Texture = tex;
        }
        public Texture2D Texture { get; set; }

        public List<Vector2> level1 { get; private set; }

        public void CreateLevel1()
        {
            level1 = new List<Vector2>();
            for (int j = 1; j < 11; j++)
            {
                for (int i = 0; i < 14; i++)
                {
                    if (j != 1 && j != 10)
                    {
                        if (i != 0 && i != 13)
                        {
                            continue;
                        }
                    }
                    level1.Add(new Vector2(0 + Texture.Width * i, Shared.stage.Y - Texture.Height * j));
                }
            }
        }
    }
}
