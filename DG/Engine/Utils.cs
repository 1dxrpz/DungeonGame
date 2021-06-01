using DG.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace DG.Engine
{
	static public class Utils
	{
		public static int ItemID = 0;
		public static List<Drawable> Scripts = new List<Drawable>();
		public static GraphicsDevice GraphicsDevice;
		public static ContentManager Content;
		public static SpriteBatch SpriteBatch;
		public static Texture2D CreateDefaultTexture(Color color)
		{
			Color[] data = new Color[1] { color };
			Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);
			texture.SetData(data);
			return texture;
		}
	}
	static public class AppWindow
	{
		private static int sw = 0;
		private static int sh = 0;
		public static int Width { get => sw; }
		public static int Height { get => sh; }
		public static void Update(int w, int h) { sw = w; sh = h; }
	}
	static public class Time
	{
		static private float dt = 0;
		static public float DeltaTime { get => dt; }
		static public void Update(GameTime t) => dt = (float)t.ElapsedGameTime.TotalSeconds;
	}
	static public class Settings
	{
		static public WindowType WindowType = WindowType.Borderless;
		static public bool IsUpdated = false;
	}
	static public class Scene
	{
		static public List<Item> Items = new List<Item>();
	}
}
