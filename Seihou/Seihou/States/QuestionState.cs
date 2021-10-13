﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Seihou
{
	
	class QuestionState : State
	{
		private readonly double _score;
		private readonly Settings.Difficulty _difficulty;

		Vector2 buttonSize = new Vector2(400, 60);
        readonly List<Control> controls;

		public QuestionState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm,double score, Settings.Difficulty difficulty) : base(sm, cm,sb,gdm)
		{
			_difficulty = difficulty;
			_score = score;

			controls = new List<Control>
			{
				new Button(new Vector2(Global.screenWidth/2,Global.screenHeight/2-80),buttonSize,sb,Yes,"Yes please", 0, Button.Align.center),
				new Button(new Vector2(Global.screenWidth/2,Global.screenHeight/2+80),buttonSize,sb,No,"NO TAKE ME TO MENU", 1, Button.Align.center),
			};
		}

		private void Yes(object sender)
		{
			var data = new HighscoreState.PlayData
			{
				Score = _score,
				Difficulty = _difficulty,
			};

			sm.ChangeState(new HighscoreState(sm, cm, sb, gdm, data));
		}

		private void No(object sender)
		{
			sm.ChangeState(new MenuState(sm, cm, sb, gdm));
		}

		public override void Draw(GameTime gt)
		{
			var txt = "Do you want to save your PATHETIC score?";
			sb.DrawString(ResourceManager.fonts["DefaultFont"],txt,new Vector2(Global.screenWidth/2,100),Color.White,0,new Vector2(ResourceManager.fonts["DefaultFont"].MeasureString(txt).X/2,0),1,SpriteEffects.None,0);
			foreach (var c in controls) c.Draw(gt);
		}

		public override void Update(GameTime gt)
		{
			Cursor.Moved();
			Global.buttonCount = controls.Count;
			Button.ButtonKeyControl(gt);
			foreach (var c in controls) c.Update(gt);
		}
	}
	
}
