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
			scoreLabel.Text = "0";
			panel.AddChildLast(scoreLabel);
			uiScene.RootWidget.AddChildLast(panel);
			UISystem.SetScene(uiScene);
			
			//Create the background.
			background = new Background(gameScene);
			
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
			player.Update(0.0f, x);
		}
	}
}
