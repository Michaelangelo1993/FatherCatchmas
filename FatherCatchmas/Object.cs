using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace FlappyBird
{
	public class Object
	{
		const int numPresents = 10;
		
		//Private variables.
		private SpriteUV[] 	sprites;
		private TextureInfo	textureInfo;
		private float		width;
		private float		height;
		
		//Public functions.
		public Object (float startX, Scene scene)
		{
			textureInfo     = new TextureInfo("/Application/textures/present.png");
			
			for(int i=0; i<numPresents; i++)
			{
				sprites	= new SpriteUV[i];;
			}
			
			
			for(int i=0; i<numPresents; i++)
			{
				scene.AddChild(sprites[i]);
			}
			
			
			//Get sprite bounds.
			Bounds2 b = sprites[0].Quad.Bounds2();
			width  = b.Point10.X;
			height = b.Point01.Y;
			
			//Position pipes.
			sprites[0].Position = new Vector2(startX,
			                              Director.Instance.GL.Context.GetViewport().Height*RandomPosition());
			
			//sprites[1].Position = new Vector2(startX, sprites[0].Position.Y-height-kGap);
		}
		
		public void Dispose()
		{
			//textureInfoTop.Dispose();
			//textureInfoBottom.Dispose();
		}
		
		public void Update(float deltaTime)
		{			
				
		}
		
		private float RandomPosition()
		{
			Random rand = new Random();
			float randomPosition = (float)rand.NextDouble();
			randomPosition += 0.45f;
			
			if(randomPosition > 1.0f)
				randomPosition = 0.9f;
		
			return randomPosition;
		}
		
		public bool HasCollidedWith(SpriteUV sprite)
		{
			return false;
		}
	}
}

