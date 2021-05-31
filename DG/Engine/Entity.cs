using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Engine
{
	public class Entity : Drawable
	{
		public Vector2 Position = Vector2.Zero;
		public Texture2D Texture = Utils.CreateDefaultTexture(Color.Red);
		public Point Size = new Point(64, 64);
		public Rectangle Bounds = new Rectangle();
		public override void Initialize()
		{
			Texture = Utils.CreateDefaultTexture(Color.Red);
			Bounds = new Rectangle(Position.ToPoint(), Size);
		}
		public override void Draw()
		{
			Utils.SpriteBatch.Draw(Texture,
				new Rectangle((Position - Camera.Position).ToPoint(), Size),
				Color.White
			);
		}
	}
}
