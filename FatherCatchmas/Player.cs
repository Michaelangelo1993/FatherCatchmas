using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace FatherCatchmas
{
	public class Player
	{
		//Private variables.
		private static SpriteUV 	sackSprite;
		private static SpriteUV		santaSprite;
		private static TextureInfo	textureInfoSack;
		private static TextureInfo	textureInfoSanta;
		private static Vector2		min, max;
		private static Bounds2		box, leftBox, rightBox;
		private static float		xPos;
		
		//Accessors.
		//public SpriteUV Sprite { get{return sprite;} }
		
		//Public functions.
		public Player (Scene scene)
		{
			textureInfoSack  = new TextureInfo("/Application/textures/sack.png");
			textureInfoSanta  = new TextureInfo("/Application/textures/santaSprite.png");
			
			sackSprite	 		= new SpriteUV();
			sackSprite 			= new SpriteUV(textureInfoSack);	
			sackSprite.Quad.S 	= textureInfoSack.TextureSizef;
			sackSprite.Position = new Vector2((Director.Instance.GL.Context.GetViewport().Width / 2) - (textureInfoSack.TextureSizef.X / 2),0.0f);
			
			santaSprite	 		= new SpriteUV();
			santaSprite 		= new SpriteUV(textureInfoSanta);	
			santaSprite.Quad.S 	= textureInfoSanta.TextureSizef;
			santaSprite.Position = new Vector2(sackSprite.Position.X,sackSprite.Position.Y);
			
			//Add to the current scene.
			scene.AddChild(santaSprite);
			scene.AddChild(sackSprite);
		}
		
		public void Dispose()
		{
			textureInfoSack.Dispose();
			textureInfoSanta.Dispose();
		}
		
		public void Update(float deltaTime, float x)
		{				
			xPos = (x * (Director.Instance.GL.Context.GetViewport().Width / 2))
					+ (Director.Instance.GL.Context.GetViewport().Width / 2)
					- (textureInfoSack.TextureSizef.X / 2);
			
			sackSprite.Position = new Vector2(xPos,sackSprite.Position.Y);
			santaSprite.Position = new Vector2(sackSprite.Position.X,sackSprite.Position.Y);
			
			if(sackSprite.Position.X < 0)
			{
				sackSprite.Position = new Vector2(0.0f, 0.0f);
				santaSprite.Position = new Vector2(sackSprite.Position.X,sackSprite.Position.Y);
			}
			else if(sackSprite.Position.X > (Director.Instance.GL.Context.GetViewport().Width - textureInfoSack.TextureSizef.X))
			{
				sackSprite.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width - textureInfoSack.TextureSizef.X, 0.0f);
				santaSprite.Position = new Vector2(sackSprite.Position.X,sackSprite.Position.Y);
			}
			
			min.X			= sackSprite.Position.X;
			min.Y			= sackSprite.Position.Y;
			max.X			= sackSprite.Position.X + (textureInfoSack.TextureSizef.X);
			max.Y			= sackSprite.Position.Y + (textureInfoSack.TextureSizef.Y);
			box.Min 		= min;			
			box.Max 		= max;
			
			min.X			= sackSprite.Position.X - 1;
			min.Y			= sackSprite.Position.Y;
			max.X			= sackSprite.Position.X - 1;
			max.Y			= sackSprite.Position.Y + (textureInfoSack.TextureSizef.Y) + 5;
			leftBox.Min 	= min;			
			leftBox.Max 	= max;
			
			min.X			= sackSprite.Position.X + (textureInfoSack.TextureSizef.X) + 1;
			min.Y			= sackSprite.Position.Y;
			max.X			= sackSprite.Position.X + (textureInfoSack.TextureSizef.X) + 1;
			max.Y			= sackSprite.Position.Y + (textureInfoSack.TextureSizef.Y) + 5;
			rightBox.Min 	= min;			
			rightBox.Max 	= max;
		}	
		
		public void Tapped()
		{
			
		}
		
		public float GetXPos()
		{
			return xPos;
		}
		
		public Bounds2 GetBox()
		{	
			return box;
		}
		
		public Bounds2 GetLeftBox()
		{	
			return leftBox;
		}
		
		public Bounds2 GetRightBox()
		{	
			return rightBox;
		}
	}
}


