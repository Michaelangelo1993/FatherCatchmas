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
		
		private static Player 		player;
		private static Background	background;
		private static Present[]	presents;
		
		public static int score;
		const int NUMPRESENTS = 9;
		
		public static void Main (string[] args)
		{
			Initialize ();

			bool quitGame = false;
			while (!quitGame) 
			{
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
			
			//Set the ui scene.
			uiScene = new Sce.PlayStation.HighLevel.UI.Scene();
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
			scoreLabel = new Sce.PlayStation.HighLevel.UI.Label();
			scoreLabel.HorizontalAlignment = HorizontalAlignment.Center;
			scoreLabel.VerticalAlignment = VerticalAlignment.Top;
			scoreLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - scoreLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - scoreLabel.Height/2);
			scoreLabel.Text = "Score: " + score;
			panel.AddChildLast(scoreLabel);
			uiScene.RootWidget.AddChildLast(panel);
			UISystem.SetScene(uiScene);
			
			//Reset score
			score = 0;
			
			//Create the background.
			background = new Background(gameScene);
			
			//Create the player
			player = new Player(gameScene);
			
			
			//Create presents
			presents = new Present[NUMPRESENTS];
	
			for (int i=0; i<NUMPRESENTS; i++)
			{
				presents[i] = new Present(gameScene, i);	
			}
			
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
			player.Update(0.0f, x);
			
			
			//Update the presents
			foreach(Present present in presents)
			{
				present.Update(0.0f);
			}	
			
			
			//Temporary collision 
			bool collected = false;
			Bounds2 playerBox = player.GetBox ();
			
			foreach(Present present in presents)
			{
				collected = present.HasCollidedWith(playerBox);
				if (collected==true)
					score++;
			}	
			
			//Score test			
			scoreLabel.Text = "" + score;
			
		}
	}
}
