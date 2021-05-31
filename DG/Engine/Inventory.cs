using DG.Scripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Engine
{
	public class Item : Drawable
	{
		static private bool DraggingObject = false;
		static private List<Item> OverlappingObjects = new List<Item>();
		static private bool KeyPressed = false;

		public string Name = "";
		public int id = 0;
		public Vector2 Position = Vector2.Zero;
		public Texture2D Texture;
		public bool IsVisible = true;
		public bool InInventory = false;
		public Point Size = new Point(32, 32);
		public int InvIndex = 0;
		public bool IsHover
		{
			get => Mouse.GetState().Position.X > Position.X &&
				Mouse.GetState().Position.X < Position.X + Size.X &&
				Mouse.GetState().Position.Y < Position.Y + Size.Y &&
				Mouse.GetState().Position.Y > Position.Y;
		}
		public bool IsOverlapping(Rectangle rect)
		{
			return !Rectangle.Intersect(new Rectangle(Position.ToPoint(), Size), rect).IsEmpty;
		}
		public Item(string n, Vector2 p, Texture2D t)
		{
			Name = n;
			Position = p;
			Texture = t;
			id = Utils.ItemID;
			Utils.ItemID++;
		}
		public bool IsDragging = false;
		public bool IsDropped = false;
		private Vector2 DraggingOffset = Vector2.Zero;
		Inventory pli = GameScript.PlayerInventory;

		Random r = new Random();
		public float floating = 0;
		bool init = true;
		public override void Update()
		{
			if (init)
			{
				floating = (float)r.NextDouble() * 100f;
				init = false;
			}
			floating = floating > 360 ? 0 : floating + 1;
			if (!InInventory)
			{
				Position.Y += MathF.Sin(floating / 10) * .25f;
			}
			if (IsHover && Mouse.GetState().LeftButton == ButtonState.Pressed && !DraggingObject && IsVisible && InInventory)
			{
				DraggingOffset = Mouse.GetState().Position.ToVector2() - Position;
				DraggingObject = true;
				IsDragging = true;
			}
			IsDropped = false;
			if (Mouse.GetState().LeftButton == ButtonState.Released && IsDragging)
			{
				DraggingObject = false;
				IsDragging = false;
				IsDropped = true;
			}
			if (IsDragging)
				Position = Mouse.GetState().Position.ToVector2() - DraggingOffset;
			if (IsOverlapping(new Rectangle(GameScript.Player.Position.ToPoint(), GameScript.Player.Size)))
			{
				if (!OverlappingObjects.Contains(this))
					OverlappingObjects.Add(this);
				if (!Item.KeyPressed && Keyboard.GetState().IsKeyDown(Keys.E))
				{
					Item.KeyPressed = true;
					for (int i = 0; i < OverlappingObjects.Count; i++)
						if (pli.Items.Count < pli.Size.X * pli.Size.Y)
						{
							GameScript.PlayerInventory.Add(OverlappingObjects[i]);
							OverlappingObjects[i].InInventory = true;
						}
				}
			} else
			{
				if (OverlappingObjects.Contains(this))
					OverlappingObjects.Remove(this);
			}
			if (Keyboard.GetState().IsKeyUp(Keys.E) && Item.KeyPressed)
				KeyPressed = false;
		}
		private Texture2D Tooltip = Utils.CreateDefaultTexture(new Color(34, 35, 36));
		SpriteFont font = Utils.Content.Load<SpriteFont>("font");
		public override void Draw()
		{
			if (IsVisible)
			{
				if (!InInventory)
					Utils.SpriteBatch.Draw(Texture, new Rectangle((Position - Camera.Position).ToPoint(), Size), Color.White);
				else if (IsHover && !IsDragging)
				{
					Utils.SpriteBatch.Draw(Tooltip, new Rectangle(Mouse.GetState().Position - new Point(-20, 10), new Point(150, 24)), Color.White);
					Utils.SpriteBatch.DrawString(font, Name, Mouse.GetState().Position.ToVector2() - new Vector2(-25, 5), Color.Gold);
				}
			}
		}
	}
	public class Inventory : Drawable
	{
		public List<Item> Items = new List<Item>();
		public Vector2 Position = new Vector2(100, 100);
		public Texture2D Texture = Utils.CreateDefaultTexture(Color.White);
		public bool Open = false;
		public Point Size = new Point(10, 10);
		public Point FrameSize = new Point(32, 32);
		public Point offset = new Point(2, 2);
		public bool IsHover
		{
			get => Mouse.GetState().Position.X > Position.X &&
				Mouse.GetState().Position.X < Position.X + Size.X * (FrameSize.X + offset.X) &&
				Mouse.GetState().Position.Y < Position.Y + Size.Y * (FrameSize.Y + offset.Y) &&
				Mouse.GetState().Position.Y > Position.Y;
		}
		public override void Update()
		{
			for (int i = 0; i < Items.Count; i++)
			{
				Items[i].IsVisible = Open;
				if (Items[i].IsDropped)
				{
					if (!IsHover)
					{
						Items[i].Position = GameScript.Player.Position;
						Items[i].InInventory = false;
						Items[i].InvIndex = 0;
						Items.RemoveAt(i);
					} else
					{
						Point temp_pos = (Mouse.GetState().Position - Position.ToPoint()) / (FrameSize + offset);
						if (Items.FindIndex(v => v.InvIndex == (temp_pos.X) + temp_pos.Y * Size.X) == -1)
						{
							Items[i].InvIndex = ((temp_pos.X) + temp_pos.Y * Size.X);
						}
						Items[i].Position = Position + new Vector2(
								(Items[i].InvIndex) % Size.X * (FrameSize.X + offset.X),
								((Items[i].InvIndex) / Size.X) * (FrameSize.Y + offset.Y));
					}
				}
			}
		}
		public void Add(Item item)
		{
			item.InInventory = true;
			for (int i = 0; i < Size.X * Size.Y; i++)
			{
				if (Items.FindIndex(v => v.InvIndex == item.InvIndex) != -1)
					item.InvIndex++;
				else
					break;
			}
			item.Position = Position + new Vector2(
				(item.InvIndex) % Size.X * (FrameSize.X + offset.X),
				((item.InvIndex) / Size.X) * (FrameSize.Y + offset.Y));
			Items.Add(item);
		}
		public override void Draw()
		{
			if (Open)
			{
				for (int y = 0; y < Size.Y; y++)
				{
					for (int x = 0; x < Size.X; x++)
					{
						Utils.SpriteBatch.Draw(Texture,
						new Rectangle(Position.ToPoint() + new Point(x, y) * (FrameSize + offset), FrameSize
						), Color.White);
					}
				}
				for (int i = 0; i < Items.Count; i++)
				{
					Utils.SpriteBatch.Draw(Items[i].Texture, new Rectangle((Items[i].Position).ToPoint(), Items[i].Size), Color.White);
				}
			}
		}
	}
}