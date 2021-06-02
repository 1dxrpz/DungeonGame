using DG.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DG.Scripts
{
	public class GameScript : Drawable
	{
		static public Entity Player;
		static public Inventory PlayerInventory;
		Entity skeleton;
		Entity empty;

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

			Player = new Player
			{
				Speed = new Vector2(300, 300),
				Position = new Vector2(500, 500),
				Texture = Utils.Content.Load<Texture2D>("Entity/Player/PlayerIdle"),
				Size = new Point(50, 37),
				Scale = new Point(3, 3),
				FrameSize = new Point(50, 37),
				FrameCount = 5,
				AnimationSpeed = 8,
				DropShadow = true
			};

			skeleton = new Enemy
			{
				Speed = new Vector2(200, 200),
				Texture = Utils.Content.Load<Texture2D>("Entity/Skeleton/SkeletonIdle"),
				FrameCount = 11,
				AnimationSpeed = 10,
				FrameSize = new Point(24, 32),
				Size = new Point(24, 32),
				Scale = new Point(3, 3),
				Position = new Vector2(500, 500),
				DropShadow = true
			};
			empty = new Entity();
			/*
			skeleton.ChangeAnimation("Entity/Skeleton/SkeletonHit", 8, 10);
			skeleton.Size = new Point(30, 32);
			skeleton.FrameSize = new Point(30, 32);
			*/
			/*
			skeleton.ChangeAnimation("Entity/Skeleton/SkeletonAttack", 18, 10);
			skeleton.Size = new Point(43, 37);
			skeleton.FrameSize = new Point(43, 37);
			*/

			Player.SpawnItem("Diamond", Player.Position);

			Player.Position = skeleton.Position;
			WSize = new Vector2(AppWindow.Width, AppWindow.Height);

		}
		bool invopen = false;
		private bool mtemp = false;
		private bool stemp = false;
		public override void Update()
		{
			/*
			if (Vector2.Distance(Player.Position, skeleton.Position) > 60)
			{
				if (!stemp)
				{
					skeleton.ChangeAnimation("Entity/Skeleton/SkeletonWalk", 13, 5);
					skeleton.Size = new Point(22, 33);
					skeleton.FrameSize = new Point(22, 33);
					stemp = true;
				}
				var angle = MathF.Atan2(skeleton.Position.X - Player.Position.X, skeleton.Position.Y - Player.Position.Y);
				skeleton.Position -= new Vector2(MathF.Sin(angle), MathF.Cos(angle)) * 100 * Time.DeltaTime;
			} else
			{
				if (stemp)
				{
					skeleton.ChangeAnimation("Entity/Skeleton/SkeletonAttack", 18, 10);
					skeleton.Size = new Point(43, 37);
					skeleton.FrameSize = new Point(43, 37);
					stemp = false;
				}
			}
			*/
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
			if (Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				
			}
			
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

			
			foreach (Entity item in Utils.Entities)
			{
				item.Update();
				item.Animate();
			}
			PlayerInventory.Update();
			
			Scene.Items.ForEach(v => v.Update());
			Camera.Position = Vector2.Lerp(Camera.Position, Player.Position - WSize / 2 + (Player.Size * Player.Scale).ToVector2() / 2, .1f);
		}
		static public string test = "";
		public override void Draw()
		{
			
			Utils.SpriteBatch.Draw(Vignette, new Rectangle(Point.Zero, WSize.ToPoint()), new Color(255, 255, 255, 130));
			Scene.Items.ForEach(v => v.Draw());
			PlayerInventory.Draw();
			var temp = Utils.Entities.OrderBy(v => v.Position.Y + v.Size.Y * v.Scale.Y).Reverse().OrderBy(v => v.OnFloor).Reverse();
			foreach (Entity item in temp)
			{
				item.Draw();
			}
			//Utils.SpriteBatch.DrawString(font, Location.Wall.GetTile(Vector2.Floor(Player.Position / new Vector2(64, 64))).ToString(), new Vector2(10, 30), Color.Gold);
			//Utils.SpriteBatch.DrawString(font, test, new Vector2(10, 100), Color.Gold);
		}
	}
}