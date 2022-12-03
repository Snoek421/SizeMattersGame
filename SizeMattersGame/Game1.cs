﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design;

namespace SizeMattersGame
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		public SpriteBatch _spriteBatch;
		
		//declare all scenes here
		private StartScene startScene;
		private HelpScene helpScene;
		private ActionScene actionScene;
		private CreditScene creditScene;


		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here

			//create all scenes here
			startScene = new StartScene(this);
			this.Components.Add(startScene);
			startScene.show();
			helpScene = new HelpScene(this);
			this.Components.Add(helpScene);
			//helpScene.show();
			actionScene = new ActionScene(this);
			this.Components.Add(actionScene);
			//actionScene.show();
			creditScene = new CreditScene(this);
			this.Components.Add(creditScene);
		}

		
		/// <summary>
		/// Goes through the components list and hides each gamescene component
		/// </summary>
		private void hideAllScenes()
		{
			foreach (GameComponent item in Components)
			{
				if (item is GameScene)
				{
					GameScene gs = (GameScene)item;
					gs.hide();
				}
			}
		}

		protected override void Update(GameTime gameTime)
		{
			int selectedIndex = 0;
			KeyboardState ks = Keyboard.GetState();
			if (startScene.Enabled)
			{
				selectedIndex = startScene.menu.selectedIndex;
				if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					actionScene.show();
				}
				if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					helpScene.show();
				}
				if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					creditScene.show();
				}
				if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
				{
					Exit();
				}
			}
			if (actionScene.Enabled || helpScene.Enabled || creditScene.Enabled)
			{
				if (ks.IsKeyDown(Keys.Escape))
				{
					hideAllScenes();
					startScene.show();
				}
			}

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}