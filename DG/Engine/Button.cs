using DG.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Engine
{
	public class Button : GUI
	{
		public bool IsHover
		{
			get => MousePos.X >= Bounds.X && MousePos.X <= Bounds.X + Bounds.Width &&
				MousePos.Y >= Bounds.Y && MousePos.Y <= Bounds.Y + Bounds.Height;
		}
		public bool IsPressed
		{
			get => IsHover && Mouse.GetState().LeftButton == ButtonState.Pressed;
		}
		private bool ClickState = false;
		public bool OnClick
		{
			get
			{
				ClickState = IsPressed ? ClickState ? false : true : false;
				return ClickState;
			}
		}
		
		
	}
}
