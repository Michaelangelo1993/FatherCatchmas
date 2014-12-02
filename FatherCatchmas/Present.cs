using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace FatherCatchmas
{
	public class Present
	{
		
		//Private variables.
		private SpriteUV 	sprite;
		private TextureInfo	textureInfo;
		
		private static Vector2		min, max;
		private static Bounds2		preBox;
		
		//Public functions.
		public Present (Scene scene, int seed)
		{
			textureInfo  = new TextureInfo("/Application/textures/present.png");
			
			//Create sprite
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			sprite.Position = new Vector2(RandomPosition(seed), Director.Instance.GL.Context.GetViewport().Height+RandomPosition(seed));

			
			//viewHeight = Director.Instance.GL.Context.GetViewport().Height;

			
			//Add to the current scene.
			scene.AddChild(sprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{			
			//Make the presents fall
			sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y-2f);
			
			//Reset the position once the ground is hit
			if(sprite.Position.Y <0)
				sprite.Position = new Vector2(sprite.Position.X, Director.Instance.GL.Context.GetViewport().Height+RandomPosition(1));
			
			//Assign bounding box values
			min.X		= sprite.Position.X;
			min.Y		= sprite.Position.Y;
			max.X		= sprite.Position.X + (textureInfo.TextureSizef.X);
			max.Y		= sprite.Position.Y + (textureInfo.TextureSizef.Y);
			preBox.Min 	= min;			
			preBox.Max 	= max;
		}
		
		private float RandomPosition(int seed)
		{
			//Create a random float
			Random rnd = new Random(seed); 
			float randomPos = (float)rnd.Next(0, Director.Instance.GL.Context.GetViewport().Width);
		
			return randomPos;
		}
		
		public bool HasCollidedWith(Bounds2 box)
		{			
			//Collision calculations go here
			
			return false;
		}
	}
}

