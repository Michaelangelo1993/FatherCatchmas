using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace FatherCatchmas
{
	public class Background
	{	
		//Private variables.
		private SpriteUV 	sprite;
		private TextureInfo	textureInfo;
		private SpriteUV 	snowSprite;
		private TextureInfo	textureInfo2;
		private SpriteUV	snowSprite2;
		private float		width;
		private float		height2;
		
		//Public functions.
		public Background (Scene scene)
		{
			sprite	= new SpriteUV();
			
			textureInfo  	= new TextureInfo("/Application/textures/backgroundnosnow.png");
			
			snowSprite	= new SpriteUV();
			snowSprite2 = new SpriteUV();
			
			textureInfo2  	= new TextureInfo("/Application/textures/snow3.png");
			
			sprite 			= new SpriteUV(textureInfo);
			sprite.Quad.S 	= textureInfo.TextureSizef;
			
			snowSprite 			= new SpriteUV(textureInfo2);
			snowSprite.Quad.S 	= textureInfo2.TextureSizef;
			
			snowSprite2 			= new SpriteUV(textureInfo2);
			snowSprite2.Quad.S 	= textureInfo2.TextureSizef;
			
			//Get sprite bounds.
			Bounds2 b = sprite.Quad.Bounds2();
			width     = b.Point10.X;
			Bounds2 b2 = snowSprite.Quad.Bounds2();
			height2     = b2.Point11.Y;
			
			//Position background.
			sprite.Position = new Vector2(0.0f, 0.0f);
			snowSprite.Position = new Vector2(0.0f, 0.0f);
			snowSprite2.Position = new Vector2(0.0f, height2);
			
			//Add to the current scene.
			scene.AddChild(sprite);
			scene.AddChild (snowSprite);
			scene.AddChild (snowSprite2);
		}	
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{			
			snowSprite.Position = new Vector2(snowSprite.Position.X, snowSprite.Position.Y-5.0f);
			snowSprite2.Position = new Vector2(snowSprite2.Position.X, snowSprite2.Position.Y-5.0f);
			if(snowSprite.Position.Y < -height2)
				snowSprite.Position = new Vector2(0.0f, height2);
			if(snowSprite2.Position.Y < -height2)
				snowSprite2.Position = new Vector2(0.0f, height2);
			
		}
	}
}

