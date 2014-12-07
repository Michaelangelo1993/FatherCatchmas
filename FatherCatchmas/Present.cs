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
		
		public Vector2 Pos()
		{
			Vector2 pos = new Vector2(sprite.Position.X, sprite.Position.Y);
			return pos;
		}
		
		public Bounds2 GetBox()
		{	
			return preBox;
		}
	}
}

