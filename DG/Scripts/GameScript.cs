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
		static public Entity Player;
		static public Inventory PlayerInventory;

		Texture2D Vignette;
		Random random;
		
		SpriteFont font;
		Vector2 WSize;

		public override void Initialize()
		{
			random = new Random();
			Vignette = Utils.Content.Load<Texture2D>("vignette");
			font = Utils.Content.Load<SpriteFont>("font");
			PlayerInventory = new Inventory();

			Player = new Player();
			Player.Speed = new Vector2(300, 300);
			//Player.Scale = new Point(2, 2);

			Player.Position = new Vector2(500, 500);
			Player.Texture = Utils.Content.Load<Texture2D>("Entity/Player/PlayerIdle");

			Player.Size = new Point(50, 37);
			Player.Scale = new Point(3, 3);
			Player.FrameSize = new Point(50, 37);
			Player.FrameCount = 5;
			Player.AnimationSpeed = 8;
			Player.DropShadow = true;
			WSize = new Vector2(AppWindow.Width, AppWindow.Height);
		}
		bool invopen = false;
		Rectangle pl = new Rectangle();
		private bool mtemp = false;
		public override void Update()
		{
			if (Player.IsMoving && !mtemp)
			{
				Player.ChangeAnimation("Entity/Player/PlayerRun", 5);
				mtemp = true;
			}
			if (!Player.IsMoving && mtemp)
			{
				Player.ChangeAnimation("Entity/Player/PlayerIdle", 4);
				mtemp = false;
			}
			
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Environment.Exit(0);
			if (Keyboard.GetState().IsKeyDown(Keys.Tab) && !invopen && !PlayerInventory.Open)
			{
				Player.SpawnItem("Diamond", Player.Position);
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
			Player.Animate();
			PlayerInventory.Update();

			Scene.Items.ForEach(v => v.Update());
			Camera.Position = Vector2.Lerp(Camera.Position, Player.Position - WSize / 2, .1f);
			
		}
		static public string test = "";
		public override void Draw()
		{
			Player.Draw();
			Utils.SpriteBatch.Draw(Vignette, new Rectangle(Point.Zero, WSize.ToPoint()), new Color(255, 255, 255, 130));
			PlayerInventory.Draw();
			Scene.Items.ForEach(v => v.Draw());
			//Utils.SpriteBatch.DrawString(font, Location.Wall.GetTile(Vector2.Floor(Player.Position / new Vector2(64, 64))).ToString(), new Vector2(10, 30), Color.Gold);
			//Utils.SpriteBatch.DrawString(font, test, new Vector2(10, 100), Color.Gold);
		}
	}
}