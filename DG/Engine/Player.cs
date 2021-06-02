using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Engine
{
	public class Player : Entity
	{
		public override void Update()
		{
			if (Keyboard.GetState().IsKeyDown(Keys.D))
			{
				Velocity.X = Speed.X;
				Mirrored = IsMirrored.NotMirrored;
			} else
			if (Keyboard.GetState().IsKeyDown(Keys.A))
			{
				Velocity.X = -Speed.X;
				Mirrored = IsMirrored.Mirrored;
			} else
				Velocity.X = 0;
			if (Keyboard.GetState().IsKeyDown(Keys.S))
			{
				Velocity.Y = Speed.Y;
			} else
			if (Keyboard.GetState().IsKeyDown(Keys.W))
			{
				Velocity.Y = -Speed.Y;
			} else
				Velocity.Y = 0;
		}
	}
}
