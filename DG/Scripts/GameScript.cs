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
		Texture2D Vignette;
		Random random;
		
		SpriteFont font;
		Vector2 WSize;

		Inventory PlayerInventory;

		Item SilverIngot;
		public override void Initialize()
		{
			random = new Random();
			Vignette = Utils.Content.Load<Texture2D>("vignette");
			font = Utils.Content.Load<SpriteFont>("font");
			PlayerInventory = new Inventory();

			Player = new Player();
			Player.Velocity = new Vector2(500, 500);
			Player.Size = new Point(32, 32);

			Player.Position = new Vector2(500, 500);
			SilverIngot = new Item("Silver Ingot", Player.Position, Utils.Content.Load<Texture2D>("Items/SilverIngot"));

			WSize = new Vector2(AppWindow.Width, AppWindow.Height);
		}
		bool invopen = false;
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

			Player.Update();
			Camera.Position = Vector2.Lerp(Camera.Position, Player.Position - WSize / 2, .1f);
			
		}
		Vector2 res = Vector2.Zero;
		public override void Draw()
		{
			Player.Draw();
			SilverIngot.Draw();
			//Utils.SpriteBatch.Draw(Vignette, new Rectangle(Point.Zero, WSize.ToPoint()), new Color(255, 255, 255, 255));
			PlayerInventory.Draw();
			Utils.SpriteBatch.DrawString(font, Vector2.Floor(Player.Position).ToString(), new Vector2(10, 30), Color.Gold);
		}
	}
}