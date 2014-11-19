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
		private float		width;
		
		//Public functions.
		public Background (Scene scene)
		{
			sprite	= new SpriteUV();
			
			textureInfo  	= new TextureInfo("/Application/textures/background.png");
			
			sprite 			= new SpriteUV(textureInfo);
			sprite.Quad.S 	= textureInfo.TextureSizef;
			
			//Get sprite bounds.
			Bounds2 b = sprite.Quad.Bounds2();
			width     = b.Point10.X;
			
			//Position pipes.
			sprite.Position = new Vector2(0.0f, 0.0f);
			
			//Add to the current scene.
			scene.AddChild(sprite);
		}	
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{			
			
		}
	}
}

