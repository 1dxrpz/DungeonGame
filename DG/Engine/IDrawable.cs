using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Engine
{
	public interface IDrawable
	{
		public void Initialize();
		public void Update();
		public void Draw();
	}
}
