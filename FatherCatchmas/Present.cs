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
		
		private static Vector2		min, max, min2, max2;
		private static Bounds2		preBox, preBox2;
		private static bool			hasCollided;
		
		const float SPEED = 2.0f;
		
		//Public functions.
		public Present (Scene scene, int seed)
		{
			textureInfo  = new TextureInfo("/Application/textures/present.png");
			
			//Create sprite
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			sprite.Position = new Vector2(GetRandomNumber(0, Director.Instance.GL.Context.GetViewport().Width), GetRandomNumber (Director.Instance.GL.Context.GetViewport().Height, Director.Instance.GL.Context.GetViewport().Height*2));
			
			hasCollided = false;
			
			//Add to the current scene.
			scene.AddChild(sprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime, float x)
		{			
			//Make the presents fall
			sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y-SPEED);
			
			//Reset the position once the ground is hit
			if(sprite.Position.Y <0)
				ResetPosition();
		
			//Assign bounding box values
			min.X		= sprite.Position.X;
			min.Y		= sprite.Position.Y;
			max.X		= sprite.Position.X + (textureInfo.TextureSizef.X);
			max.Y		= sprite.Position.Y + (textureInfo.TextureSizef.Y);
			preBox.Min 	= min;			
			preBox.Max 	= max;
			
			min2.X		= sprite.Position.X;
			min2.Y		= sprite.Position.Y + (textureInfo.TextureSizef.Y);
			max2.X		= sprite.Position.X + (textureInfo.TextureSizef.X);
			max2.Y		= sprite.Position.Y + (textureInfo.TextureSizef.Y);;
			preBox2.Min 	= min2;			
			preBox2.Max 	= max2;
		}
		
		//Function to get random number
		private static readonly Random getrandom = new Random();
		private static readonly object syncLock = new object();
		public static int GetRandomNumber(int min, int max)
		{
    		lock(syncLock)
			{ // synchronize
        		return getrandom .Next(min, max);
    		}
		}
		
		private float RandomPosition(int seed)
		{
			//Create a random float
			Random rnd = new Random(); 
			
        	float randomPos = (float)rnd.Next(Director.Instance.GL.Context.GetViewport().Width);	
		
			return randomPos;
		}
	
		public void ResetPosition()
		{
			sprite.Position = new Vector2(sprite.Position.X, Director.Instance.GL.Context.GetViewport().Height+RandomPosition(1));
		}
		
		public void IsColliing()
		{	
			hasCollided = true;
		}
		
		public void IsntColliing()
		{	
			hasCollided = false;
		}
		
		public Vector2 Pos()
		{
			Vector2 pos = new Vector2(sprite.Position.X, sprite.Position.Y);
			return pos;
		}
		
		public Bounds2 GetBox()
		{	
			return preBox;
		}
		
		public Bounds2 GetBox2()
		{	
			return preBox2;
		}
	}
}

