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
		TileMap TileMap;
		public override void Initialize()
		{
			TileMap = new TileMap();
			TileMap.Texture = Utils.Content.Load<Texture2D>("tilemap");
			TileMap.AddTile('0', new Vector2(1, 1));
			TileMap.AddTile('1', new Vector2(1, 4));
			TileMap.SetTiles('0', new Vector2(0, 0));

			for (int a = 0; a < 10; a++)
			{
				TileMap.SetTiles('0', new Vector2(a + 5, 4));
			}

			for (int y = 0; y < 10; y++)
			{
				for (int x = 0; x < 10; x++)
				{
					TileMap.SetTiles('1', new Vector2(y + 5, x + 5));
				}
			}
		}
		public override void Update()
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
			{
				Camera.Position.X += Time.DeltaTime * 120f;
			}
		}
		public override void Draw()
		{
			TileMap.Draw();
		}
	}
}
