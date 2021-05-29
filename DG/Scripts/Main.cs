using DG.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace DG.Scripts
{
	public class Main : Drawable
	{
		Button b;
		public override void Initialize()
		{
			b = new Button();
			b.Bounds = new Rectangle(10, 10, 100, 100);
		}
		public override void Update()
		{
			b.Update();
			if (b.OnClick)
				Environment.Exit(0);
		}
		public override void Draw()
		{
			b.Draw();
		}
	}
}
