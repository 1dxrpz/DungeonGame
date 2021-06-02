using DG.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Scripts
{
	public class Location : Drawable
	{
		TileMap Grass;
		static public TileMap Wall;
		TileMap Struct;
		Random random;

		Entity Portal;

		public void InitGrass()
		{
			Grass = new TileMap();
			Grass.Texture = Utils.Content.Load<Texture2D>("TGrass");
			Grass.AddTile(0, new Vector2(0, 0));
			Grass.AddTile(1, new Vector2(1, 0));
			Grass.AddTile(2, new Vector2(2, 0));
			Grass.AddTile(3, new Vector2(3, 0));
			Grass.AddTile(4, new Vector2(0, 1));
			Grass.AddTile(5, new Vector2(1, 1));
			Grass.AddTile(6, new Vector2(2, 1));
			Grass.AddTile(7, new Vector2(3, 1));
			Grass.AddTile(8, new Vector2(0, 2));
			Grass.AddTile(9, new Vector2(1, 2));
			Grass.AddTile(10, new Vector2(2, 2));
			Grass.AddTile(11, new Vector2(3, 2));
			Grass.AddTile(12, new Vector2(4, 2));
		}
		public void InitWall()
		{
			Wall = new TileMap();
			Wall.Texture = Utils.Content.Load<Texture2D>("TWall");
			Wall.AddTile(0, new Vector2(1, 1));
			Wall.AddTile(1, new Vector2(2, 1));
			Wall.AddTile(2, new Vector2(3, 1));
			Wall.AddTile(3, new Vector2(1, 2));
			Wall.AddTile(4, new Vector2(3, 2));
			Wall.AddTile(5, new Vector2(1, 3));
			Wall.AddTile(6, new Vector2(2, 3));
			Wall.AddTile(7, new Vector2(3, 3));
			Wall.AddTile(8, new Vector2(1, 4));
			Wall.AddTile(9, new Vector2(2, 4));
			Wall.AddTile(10, new Vector2(3, 4));
			Wall.AddTile(11, new Vector2(2, 2));
		}
		public void InitStruct()
		{
			Struct = new TileMap();
			Struct.Texture = Utils.Content.Load<Texture2D>("TStruct");
			Struct.AddTile(0, new Vector2(1, 1 + 4));
			Struct.AddTile(1, new Vector2(2, 1 + 4));
			Struct.AddTile(2, new Vector2(1, 2 + 4));
			Struct.AddTile(3, new Vector2(2, 2 + 4));
			Struct.AddTile(4, new Vector2(1, 3 + 4));
			Struct.AddTile(5, new Vector2(2, 3 + 4));
		}
		public void DrawPortal(int x, int y)
		{
			Portal.Position = new Vector2(x, y);
		}
		public void DrawWall(Rectangle rect, bool stairs = true)
		{
			for (int y = rect.Y; y < rect.Y + rect.Height; y++)
				for (int x = rect.X; x <= rect.X + rect.Width; x++)
				{
					Wall.SetTiles(11, new Vector2(x, y));
					Grass.SetTiles(random.Next(13), new Vector2(x, y));
				}
			Wall.SetTiles(0, new Vector2(rect.X, rect.Y));
			for (int i = rect.X + 1; i < rect.Width + rect.X; i++)
				Wall.SetTiles(1, new Vector2(i, rect.Y));
			Wall.SetTiles(2, new Vector2(rect.X + rect.Width, rect.Y));
			for (int i = rect.Y + 1; i < rect.Height + rect.Y; i++)
				Wall.SetTiles(3, new Vector2(rect.X, i));
			for (int i = rect.Y + 1; i < rect.Height + rect.Y; i++)
				Wall.SetTiles(4, new Vector2(rect.X + rect.Width, i));
			Wall.SetTiles(2, new Vector2(rect.X + rect.Width, rect.Y));
			Wall.SetTiles(5, new Vector2(rect.X, rect.Y + rect.Height));
			for (int i = rect.X + 1; i < rect.X + rect.Width; i++)
				Wall.SetTiles(6, new Vector2(i, rect.Y + rect.Height));
			Wall.SetTiles(7, new Vector2(rect.X + rect.Width, rect.Y + rect.Height));
			Wall.SetTiles(8, new Vector2(rect.X, rect.Y + rect.Height + 1));
			for (int i = rect.X + 1; i < rect.X + rect.Width; i++)
				Wall.SetTiles(9, new Vector2(i, rect.Y + rect.Height + 1));
			Wall.SetTiles(10, new Vector2(rect.X + rect.Width, rect.Y + rect.Height + 1));
		}
		public void DrawStaircase(int x, int y)
		{
			Struct.SetTiles(0, new Vector2(x, y));
			Struct.SetTiles(1, new Vector2(x + 1, y));
			Struct.SetTiles(2, new Vector2(x, y + 1));
			Struct.SetTiles(3, new Vector2(x + 1, y + 1));
			Struct.SetTiles(4, new Vector2(x, y + 2));
			Struct.SetTiles(5, new Vector2(x + 1, y + 2));
		}

		static private int LID = 0;
		static public int LocationID
		{
			get => LID;
			set
			{
				LevelChanged = true;
				LID = value;
			}
		}
		static public bool LevelChanged = true;

		public override void Initialize()
		{
			random = new Random();
			InitGrass();
			InitWall();
			InitStruct();
			Portal = new Entity(Utils.Content.Load<Texture2D>("Props/Portal"));
			Portal.Scale = new Point(2);
			Portal.OnFloor = true;
		}
		public override void Update()
		{
			
			if (LevelChanged)
			{
				LevelChanged = false;
				switch (LocationID)
				{
					case 0:
						DrawWall(new Rectangle(5, 5, 20, 20), false);
						DrawWall(new Rectangle(12, 3, 5, 5));
						DrawPortal(870, 300);
						DrawStaircase(14, 8);
						break;
					case 1:
						DrawWall(new Rectangle(4, 5, 10, 10), false);
						DrawWall(new Rectangle(5, 4, 5, 5));
						DrawPortal(420, 350);
						DrawStaircase(7, 9);
						break;
				}
			}
		}
		public override void Draw()
		{
			Grass.Draw();
			Wall.Draw();
			Struct.Draw();
			Portal.Draw();
		}
	}
}
