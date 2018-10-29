using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Data.SqlClient;

namespace Seihou
{
    public class HighscoreState : State
    {
		readonly string conStr = $"Connection Timeout=30;Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\\GameData.mdf;Integrated Security=True;";

		double playedScore;
		string playedMode;

		List<Control> controls = new List<Control>();
  
        Textbox textBox1;
		ScoreDisplay scoreDisplay;

        public HighscoreState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm,double score,string mode) : base(sm, cm, sb, gdm)
        {
			this.playedMode = mode;
			this.playedScore = score;

            scoreDisplay = new ScoreDisplay(new Vector2(500, 100), new Vector2(650,395),this.sb,conStr);
            scoreDisplay.background = new Color(40, 40, 40);
			scoreDisplay.SetModeFilter("Infinite");
			textBox1 = new Textbox(new Vector2(100, 550), sb);

			controls.Add(new Button(new Vector2(200, 200), new Vector2(200, 50), sb, FilterButtonPressed, "easy",   Button.Align.center));
			controls.Add(new Button(new Vector2(200, 250), new Vector2(200, 50), sb, FilterButtonPressed, "hard",   Button.Align.center));
			controls.Add(new Button(new Vector2(200, 300), new Vector2(200, 50), sb, FilterButtonPressed, "normal", Button.Align.center));
			controls.Add(new Button(new Vector2(200, 350), new Vector2(200, 50), sb, FilterButtonPressed, "usagi",  Button.Align.center));
			controls.Add(new Button(new Vector2(200, 400), new Vector2(200, 50), sb, FilterButtonPressed, "infinite", Button.Align.center));

			controls.Add(scoreDisplay);
            controls.Add(textBox1);
        }

		private void FilterButtonPressed(object sender)
		{
			scoreDisplay.SetModeFilter(((Button)sender).text);
		}

        public override void Draw(GameTime gt)
        {
            sb.DrawString(ResourceManager.fonts["DefaultFont"], "Highscores", new Vector2(scoreDisplay.pos.X, 50), Color.White);
            foreach (var c in controls) c.Draw(gt);     
        }
        
        public override void Update(GameTime gt)
        {
            foreach (var c in controls) c.Update(gt);
        }
        
        public override void OnStart()
        {
        }
        
        public override void OnExit()
        {
        }
    }  
}
