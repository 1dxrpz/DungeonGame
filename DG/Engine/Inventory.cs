using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Engine
{
	public class Item : Drawable
	{
		public int Amount = 0;
		public string Name = "";
		public int id = 0;
		public Vector2 Position = Vector2.Zero;
		public Texture2D Texture;
		public bool IsVisible = true;
		public bool InInventory = false;
		public Point Size = new Point(32, 32);
		public Item(string n, Vector2 p, Texture2D t)
		{
			Name = n;
			Position = p;
			Texture = t;
			id = Utils.ItemID;
			Utils.ItemID++;
		}
		public override void Draw()
		{
			if (IsVisible)
				Utils.SpriteBatch.Draw(Texture, new Rectangle(InInventory ? Position.ToPoint() : (Position - Camera.Position).ToPoint(), Size), Color.White);
		}
	}
	public class Inventory : Drawable
	{
		List<Item> Items = new List<Item>();
		public Vector2 Position = Vector2.Zero;
		public Texture2D Texture = Utils.CreateDefaultTexture(Color.White);
		public bool Open = false;
		public override void Draw()
		{
			if (Open)
			{
				Utils.SpriteBatch.Draw(Texture, new Rectangle(Position.ToPoint(), new Point(100, 100)), Color.White);
			}
		}
	}
}