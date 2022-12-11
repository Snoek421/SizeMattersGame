using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SizeMattersGame.GameScenes;
using SizeMattersGame.Sprites;

namespace SizeMattersGame
{
    public class InteractableObjectCollision : GameComponent
    {
        private Player player;

        public gameObject door;

        public gameObject button;

        private gameObject battery;

        private int firstTime = 0;

        ActionScene scene;
        public InteractableObjectCollision(Game game, Player player, gameObject door, gameObject button, gameObject battery, ActionScene scene) : base(game)
        {
            this.player = player;
            this.door = door;
            this.button = button;
            this.battery = battery;
            this.scene = scene;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = player.GetBounds();

            if (playerRect.Intersects(button.objectRect))
            {
                button.isActive = true;
                door.isActive = true;
            }

            if (playerRect.Intersects(door.objectRect) && door.isActive == true)
            {
                scene.stageCompleted = true;
                firstTime = 0;
            }

            if (playerRect.Intersects(battery.objectRect) && firstTime == 0)
            {
                battery.isActive = true;
                battery.Visible = false;
                player.Score += 1500;
                scene.batteries++;
                firstTime++;
            }



            base.Update(gameTime);
        }
    }
}
