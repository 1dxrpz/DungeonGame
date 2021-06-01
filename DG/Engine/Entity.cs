using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.Engine
{
	public class Entity : Drawable
	{
		public Vector2 Position = Vector2.Zero;
		public Texture2D Texture = Utils.CreateDefaultTexture(Color.Red);
		public Point Size = new Point(64, 64);
		public Point Scale = new Point(1, 1);
		public Vector2 Velocity = new Vector2(100, 100);
		public Vector2 Speed = new Vector2(100, 100);
		public int FrameCount = 0;
		public Point FrameSize = new Point(32, 32);
		public int AnimationSpeed = 20;
		public IsMirrored Mirrored = IsMirrored.Default;
		public bool DropShadow = false;
		public bool IsVisible = true;
		private Vector2 prevPosition = Vector2.Zero;

		public CollisionMask CollisionMask = new CollisionMask(new Point(64, 64));

		private bool isMoving = false;
		public bool IsMoving
		{
			get
			{
				return isMoving;
			}
		}
		public bool OnStopped { get; set; }
		public bool OnMoved
		{
			get;
		}
		public int Frame
		{
			get => frame;
			set
			{
				AnimationStartPos.X = value;
				frame = value;
			}
		}

		private int frame = 0;
		private Texture2D Shadow = Utils.Content.Load<Texture2D>("Entity/Shadow");
		private Point AnimationStartPos = new Point(0, 0);

		public Entity()
		{
			Size = new Point(Texture.Width, Texture.Height);
			FrameSize = Size;
		}
		public Entity(Texture2D texture)
		{
			Texture = texture;
			Size = new Point(Texture.Width, Texture.Height);
			FrameSize = Size;
		}
		public void ChangeAnimation(string animation, int framecount, int framespeed = 10)
		{
			Texture = Utils.Content.Load<Texture2D>(animation);
			FrameCount = framecount;
			AnimationSpeed = framespeed;
			framefactor = 0;
			Frame = 0;
		}
		public bool IsOverlapping(Entity entity)
		{
			return Vector2.Distance(Position, entity.Position) < 100 ? !Rectangle.Intersect(new Rectangle(Position.ToPoint(), Size), new Rectangle(entity.Position.ToPoint(), entity.Size)).IsEmpty : false;
		}
		public void SpawnItem(string item, Vector2 Position)
		{
			Scene.Items.Add(new Item(item, Position, Utils.Content.Load<Texture2D>("Items/" + item)));
		}
		private int framefactor = 0;
		public override void Animate()
		{
			if (framefactor == AnimationSpeed)
			{
				Frame = frame >= FrameCount - 1 ? 0 : frame + 1;
				framefactor = 0;
			}
			framefactor++;
		}
		public override void Draw()
		{
			if (IsVisible)
			{
				isMoving = prevPosition != Position;
				prevPosition = Position;
				if (DropShadow)
				{
					Utils.SpriteBatch.Draw(Shadow,
						new Rectangle((Position - Camera.Position + new Vector2(Size.X / 2, Size.Y + 8) * 2).ToPoint(), Size),
						new Color(255, 255, 255, 25)
						);
				}
				Utils.SpriteBatch.Draw(Texture,
					new Rectangle((Position - Camera.Position).ToPoint(), Size * Scale),
					new Rectangle(AnimationStartPos * FrameSize, FrameSize),
					Color.White,
					0f,
					Vector2.Zero,
					Mirrored == IsMirrored.Mirrored ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
					0
				);
			}
		}
		public override void Update()
		{
			
		}
	}
}
