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

        private gameObject door;

        private gameObject button;

        ActionScene scene;
        public InteractableObjectCollision(Game game, Player player, gameObject door, gameObject button, ActionScene scene) : base(game)
        {
            this.player = player;
            this.door = door;
            this.button = button;
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
                scene.stageCleared = true;
            }



            base.Update(gameTime);
        }
    }
}
