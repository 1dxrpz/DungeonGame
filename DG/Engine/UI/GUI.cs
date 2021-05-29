using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Engine.UI
{
	public class GUI : Drawable
	{
		public Texture2D Texture = Utils.CreateDefaultTexture(Color.White);
		public Point MousePos = Mouse.GetState().Position;
		public Rectangle Bounds = new Rectangle(0, 0, 10, 10);
		public override void Update()
		{
			MousePos = Mouse.GetState().Position;
		}
		public override void Draw()
		{
			Utils.SpriteBatch.Draw(Texture, Bounds, Color.White);
		}
	}
}
