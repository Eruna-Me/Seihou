using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Seihou
{
    public class HighscoreState : State
    {
		readonly string conStr = $"Connection Timeout=30;Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\\GameData.mdf;Integrated Security=True;";

		double playedScore;
		readonly string playedMode;

		string selectedMode = "";

		List<Control> controls = new List<Control>();
  
        Textbox textBox1;
		ScoreDisplay scoreDisplay;

        public HighscoreState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm,double score,string mode) : base(sm, cm, sb, gdm)
        {
			playedMode = mode;
			playedScore = score;
			selectedMode = playedMode;

			scoreDisplay = new ScoreDisplay(new Vector2(500, 100), new Vector2(650, 395), this.sb, conStr)
			{
				background = new Color(0, 0, 0)
			};
			scoreDisplay.SetModeFilter(playedMode);

			textBox1 = new Textbox(new Vector2(100, 550), sb,OnSubmit);

			var enumVals = Enum.GetValues(typeof(Settings.Difficulty));
			for (int i = 0; i < enumVals.Length; i++)
				controls.Add(new Button(new Vector2(200,200 + i * 50), new Vector2(200, 50), sb, FilterButtonPressed,Enum.GetName(typeof(Settings.Difficulty),enumVals.GetValue(i)), i,Button.Align.center));

			controls.Add(new Button(new Vector2(80, 690), new Vector2(100, 25), sb, GoHome, "Back", enumVals.Length, Button.Align.center));

			controls.Add(scoreDisplay);
            controls.Add(textBox1);
        }

		private void FilterButtonPressed(object sender)
		{
			selectedMode = ((Button)sender).text;
			scoreDisplay.SetModeFilter(((Button)sender).text);
		}

        public override void Draw(GameTime gt)
        {
			sb.DrawString(ResourceManager.fonts["DefaultFont"], $"Your score: {playedScore}\nOn {playedMode}", new Vector2(10,10), Color.White);
			sb.DrawString(ResourceManager.fonts["DefaultFont"], $"Highscores: {selectedMode}", new Vector2(scoreDisplay.pos.X, 50), Color.White);
            foreach (var c in controls) c.Draw(gt);     
        }
        
        public override void Update(GameTime gt)
        {
			Cursor.Moved();
			Global.buttonCount = controls.Count - 2;
			Button.ButtonKeyControl(gt);
			foreach (var c in controls) c.Update(gt);
        }

		public void GoHome(object sender)
		{
			sm.ChangeState(new MenuState(sm, cm, sb, gdm));
		}

		public void OnSubmit(object sender)
		{
			string name = textBox1.text;

			scoreDisplay.InsertInto(playedScore.ToString(), name, playedMode);
		}

        public override void OnStart()
        {
        }
        
        public override void OnExit()
        {
        }
    }  
}
