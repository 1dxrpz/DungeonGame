using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Engine
{
	public class Player : Entity
	{
		public Vector2 Velocity = new Vector2(100, 100);
		public override void Update()
		{
			if (Keyboard.GetState().IsKeyDown(Keys.D))
				Position.X += Velocity.X * Time.DeltaTime;
			if (Keyboard.GetState().IsKeyDown(Keys.A))
				Position.X -= Velocity.X * Time.DeltaTime;
			if (Keyboard.GetState().IsKeyDown(Keys.S))
				Position.Y += Velocity.Y * Time.DeltaTime;
			if (Keyboard.GetState().IsKeyDown(Keys.W))
				Position.Y -= Velocity.Y * Time.DeltaTime;
		}
	}
}
