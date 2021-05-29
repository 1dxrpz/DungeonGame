using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Engine
{
	public class Drawable : IDrawable
	{
		public EnabledState IsEnabled = EnabledState.Default;
		public virtual void Draw() { }

		public virtual void Initialize() { }

		public virtual void Update() { }
	}
}
