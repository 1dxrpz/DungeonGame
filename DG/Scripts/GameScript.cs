using DG.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Scripts
{
	public class GameScript : Drawable
	{
		static public Player Player;
		static public Inventory PlayerInventory;

		Texture2D Vignette;
		Random random;
		
		SpriteFont font;
		Vector2 WSize;


		List<Item> SilverIngot = new List<Item>();
		public override void Initialize()
		{
			random = new Random();
			Vignette = Utils.Content.Load<Texture2D>("vignette");
			font = Utils.Content.Load<SpriteFont>("font");
			PlayerInventory = new Inventory();

			Player = new Player();
			Player.Velocity = new Vector2(300, 300);
			Player.Size = new Point(32, 32);

			Player.Position = new Vector2(500, 500);
			for (int i = 0; i < 20; i++)
			{
				//SilverIngot.Add(new Item("Silver Ingot", Player.Position + new Vector2(i * 33, 0), Utils.Content.Load<Texture2D>("Items/SilverIngot")));
				SilverIngot.Add(new Item("Diamond", Player.Position + new Vector2(i * 33, 64), Utils.Content.Load<Texture2D>("Items/Diamond")));
				SilverIngot.Add(new Item("Emerald", Player.Position + new Vector2(i * 33, 128), Utils.Content.Load<Texture2D>("Items/Emerald")));
				//SilverIngot.Add(new Item("Ruby", Player.Position + new Vector2(i * 33, 196), Utils.Content.Load<Texture2D>("Items/Ruby")));
				SilverIngot.Add(new Item("Obsidian", Player.Position + new Vector2(i * 33, 232), Utils.Content.Load<Texture2D>("Items/Obsidian")));
				//SilverIngot.Add(new Item("Sapphire", Player.Position + new Vector2(i * 33, 264), Utils.Content.Load<Texture2D>("Items/Sapphire")));
				//SilverIngot.Add(new Item("Coal", Player.Position + new Vector2(i * 33, 300), Utils.Content.Load<Texture2D>("Items/Coal")));
			}

			WSize = new Vector2(AppWindow.Width, AppWindow.Height);
		}
		bool invopen = false;
		Rectangle pl = new Rectangle();
		public override void Update()
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Environment.Exit(0);
			if (Keyboard.GetState().IsKeyDown(Keys.Tab) && !invopen && !PlayerInventory.Open)
			{
				invopen = true;
				PlayerInventory.Open = true;
			}
			
			if (Keyboard.GetState().IsKeyDown(Keys.Tab) && !invopen && PlayerInventory.Open)
			{
				PlayerInventory.Open = false;
				invopen = true;
			}
			if (Keyboard.GetState().IsKeyUp(Keys.Tab))
				invopen = false;

			pl.X = (int)Player.Position.X;
			pl.Y = (int)Player.Position.Y;
			pl.Width = Player.Size.X;
			pl.Height = Player.Size.Y;

			Player.Update();
			PlayerInventory.Update();
			SilverIngot.ForEach(v => v.Update());
			Camera.Position = Vector2.Lerp(Camera.Position, Player.Position - WSize / 2, .1f);
			
		}
		static public string test = "";
		public override void Draw()
		{
			Player.Draw();
			Utils.SpriteBatch.Draw(Vignette, new Rectangle(Point.Zero, WSize.ToPoint()), new Color(255, 255, 255, 130));
			PlayerInventory.Draw();
			SilverIngot.ForEach(v => v.Draw());
			//Utils.SpriteBatch.DrawString(font, Location.Wall.GetTile(Vector2.Floor(Player.Position / new Vector2(64, 64))).ToString(), new Vector2(10, 30), Color.Gold);
			//Utils.SpriteBatch.DrawString(font, test, new Vector2(10, 100), Color.Gold);
		}
	}
}