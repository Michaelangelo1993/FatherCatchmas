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
		private static Bounds2		box, topBox;
		private static float 		rotation;
		
		private float speed = 2.0f;
		
		//Public functions.
		public Present (Scene scene, int seed)
		{
			textureInfo  = new TextureInfo("/Application/textures/present.png");
			
			//Create sprite
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			sprite.Position = new Vector2(GetRandomNumber(0, Director.Instance.GL.Context.GetViewport().Width), GetRandomNumber (Director.Instance.GL.Context.GetViewport().Height, Director.Instance.GL.Context.GetViewport().Height*2));
			
			rotation = 0.0f;
			
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
			sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y-speed);
			
			//Reset the position once the ground is hit
			if(sprite.Position.Y <0)
			{
				ResetPosition();
				AppMain.UpdateLives();
			}
		
			//Assign bounding box values - size of the present
			min.X		= sprite.Position.X;
			min.Y		= sprite.Position.Y;
			max.X		= sprite.Position.X + (textureInfo.TextureSizef.X);
			max.Y		= sprite.Position.Y + (textureInfo.TextureSizef.Y);
			box.Min 	= min;			
			box.Max 	= max;
			
			//Assign bounding box values - size of top of the present
			min.X		= sprite.Position.X;
			min.Y		= sprite.Position.Y + (textureInfo.TextureSizef.Y);
			max.X		= sprite.Position.X + (textureInfo.TextureSizef.X);
			max.Y		= sprite.Position.Y + (textureInfo.TextureSizef.Y);
			topBox.Min 	= min;			
			topBox.Max 	= max;	
			
			UpdateSpeed ();
			
			sprite.Rotate(rotation);
		}
		
		public void UpdateSpeed()
		{
			int currentScore = AppMain.GetScore();
			if(currentScore<10)
			{
				speed = 1.0f;
			}
			else if (currentScore<20)
			{
				speed = 1.5f;
			}
			else if (currentScore<30)
			{
				speed = 2.0f;
			}
			else if (currentScore<40)
			{
				speed = 2.5f;
			}
			else if (currentScore<50)
			{
				speed = 3.0f;
			}
			else if (currentScore<60)
			{
				speed = 3.0f;
			}
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
			
        	float randomPos = (float)rnd.Next(Director.Instance.GL.Context.GetViewport().Width - (int)(textureInfo.TextureSizef.X * 1.5));	
			
			if (randomPos < (textureInfo.TextureSizef.X * 1.5f))
				randomPos = (textureInfo.TextureSizef.X * 1.5f);
			
			return randomPos;
		}
	
		public void ResetPosition()
		{
			sprite.Position = new Vector2(sprite.Position.X, Director.Instance.GL.Context.GetViewport().Height+RandomPosition(1));
		}
		
		public void SetXPos(float x)
		{
			sprite.Position = new Vector2(x + (textureInfo.TextureSizef.X/2), sprite.Position.Y);
		}
		
		public float GetXPos()
		{
			return sprite.Position.X;
		}
		
		public void SetRotation(float r)
		{
			rotation = r;
		}
		
		public Bounds2 GetBox()
		{	
			return box;
		}
		
		public Bounds2 GetTopBox()
		{	
			return topBox;
		}
	}
}

