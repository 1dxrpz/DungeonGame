using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Engine
{
	public class CollisionMask : Drawable
	{
		public Vector2 Position = Vector2.Zero;
		public Vector2 Offset = Vector2.Zero;
		public Point Size;
		public Vector2 Velocity = Vector2.Zero;
		
		public CollisionMask(Point s) => Size = s;
		public bool CollidingLeft(Entity obj)
		{
			return obj.Position.X + obj.Size.X >= Position.X;
		}

		public override void Update()
		{
			
		}
		public override void Draw()
		{
			
		}
	}
}
