using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace FatherCatchmas
{
	public class AppMain
	{
		private static Sce.PlayStation.HighLevel.GameEngine2D.Scene 	gameScene;
		private static Sce.PlayStation.HighLevel.UI.Scene 				uiScene;
		private static Sce.PlayStation.HighLevel.UI.Label				scoreLabel;
		private static Sce.PlayStation.HighLevel.UI.Label				livesLabel;
		
		private static Player 		player;
		private static Background	background;
		private static Present[]	presents;
		
		public static int score;
		public static int lives;
		
		const int NUMPRESENTS = 9;
		
		public static void Main (string[] args)
		{
			Initialize ();

			bool quitGame = false;
			while (!quitGame) 
			{
				if(lives>0)
					Update ();
				
				Director.Instance.Update();
				Director.Instance.Render();
				UISystem.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
			
			//Call dispose methods
			foreach(Present present in presents)
			{
				present.Dispose();
			}	
			
			Director.Terminate ();
			
		}

		public static void Initialize ()
		{
			//Set up director and UISystem.
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			
			//Reset score&lives
			score = 0;
			lives = 10;
			
			//Set the ui scene.
			uiScene = new Sce.PlayStation.HighLevel.UI.Scene();
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
			
			//Score label
			scoreLabel = new Sce.PlayStation.HighLevel.UI.Label();
			scoreLabel.HorizontalAlignment = HorizontalAlignment.Center;
			scoreLabel.VerticalAlignment = VerticalAlignment.Top;
			scoreLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - scoreLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - scoreLabel.Height/2);
			scoreLabel.Text = "Score: " + score;
			panel.AddChildLast(scoreLabel);
			
			//Lives label
			livesLabel = new Sce.PlayStation.HighLevel.UI.Label();
			livesLabel.HorizontalAlignment = HorizontalAlignment.Right;
			livesLabel.VerticalAlignment = VerticalAlignment.Top;
			livesLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/4 - livesLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - livesLabel.Height/2);
			livesLabel.Text = "Lives: " + lives;
			panel.AddChildLast(livesLabel);
			
			uiScene.RootWidget.AddChildLast(panel);
			UISystem.SetScene(uiScene);
			
			
			
			//Create the background.
			background = new Background(gameScene);
			
			//Create presents
			presents = new Present[NUMPRESENTS];
	
			for (int i=0; i<NUMPRESENTS; i++)
			{
				presents[i] = new Present(gameScene, i);	
			}
			
			
			//Create the player
			player = new Player(gameScene);
			
			//Run the scene.
			Director.Instance.RunWithScene(gameScene, true);
		}

		public static void Update ()
		{
			
			// Query gamepad for current state
			var gamePadData = GamePad.GetData(0);
			
			//Determine whether the player tapped the screen
			var touches = Touch.GetData(0);
			var x = Input2.Touch00.Pos.X;
			
			//Background animation
			background.Update(0.0f);
			
			//Update the presents
			foreach(Present present in presents)
			{
				present.Update(0.0f);
				isColliding(present);
			}	
			
			player.Update(0.0f, x);
			
			

		}
		
		public static void UpdateLives()
		{
			lives--;
			livesLabel.Text = "Lives: " + lives;
		}
		
		public static int GetScore()
		{
			return score;
		}
		
		public static void isColliding(Present present)
		{
			if (player.GetBox().Overlaps(present.GetBox()))
			{
				score++;
				present.ResetPosition();
			}
			scoreLabel.Text = "Score: " + score;
		}
	}
}
