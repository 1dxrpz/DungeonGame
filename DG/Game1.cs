using DG.Engine;
using DG.Scripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DG
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}
		Main mainmenu;
		GameScript gameScript;
		protected override void Initialize()
		{
			mainmenu = new Main();
			gameScript = new GameScript();
			Utils.GraphicsDevice = _graphics.GraphicsDevice;
			Utils.Content = Content;
			_graphics.PreferredBackBufferHeight = 1080;
			_graphics.PreferredBackBufferWidth = 1920;
			AppWindow.Update(
				_graphics.PreferredBackBufferHeight,
				_graphics.PreferredBackBufferWidth
			);
			mainmenu.IsEnabled = EnabledState.Disabled;
			Utils.Scripts.Add(mainmenu);
			Utils.Scripts.Add(gameScript);
			foreach (var item in Utils.Scripts)
				item.Initialize();
			Settings.IsUpdated = true;
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			Utils.SpriteBatch = _spriteBatch;
		}

		protected override void Update(GameTime gameTime)
		{
			if (Settings.IsUpdated)
			{
				switch (Settings.WindowType)
				{
					case WindowType.Default:
						break;
					case WindowType.Borderless:
						Window.IsBorderless = true;
						break;
					case WindowType.Fullscreen:
						_graphics.ToggleFullScreen();
						break;
				}
				_graphics.ApplyChanges();
				Settings.IsUpdated = false;
			}
			Time.Update(gameTime);
			foreach (var item in Utils.Scripts)
				if (item.IsEnabled == EnabledState.Enabled ||
					item.IsEnabled == EnabledState.Default)
					item.Update();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
			foreach (var item in Utils.Scripts)
				if (item.IsEnabled == EnabledState.Enabled ||
					item.IsEnabled == EnabledState.Default)
					item.Draw();
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
