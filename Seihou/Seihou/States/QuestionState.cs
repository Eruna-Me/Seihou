using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Seihou
{
	
	class QuestionState : State
	{
		readonly double score;
		readonly string mode;

		Vector2 buttonSize = new Vector2(400, 60);
		Control[] controls;

		public QuestionState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm,double score,string mode) : base(sm, cm,sb,gdm)
		{
			this.score = score;
			this.mode = mode;

			controls = new Control[2]
			{
				new Button(new Vector2(Global.screenWidth/2,Global.screenHeight/2-80),buttonSize,sb,Yes,"Yes please", 0, Button.Align.center),
				new Button(new Vector2(Global.screenWidth/2,Global.screenHeight/2+80),buttonSize,sb,No,"NO TAKE ME TO MENU", 1, Button.Align.center),
			};
		}

		private void Yes(object sender)
		{
			sm.ChangeState(new HighscoreState(sm, cm, sb, gdm, score, mode));
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
			Global.buttonCount = controls.Length;
			Button.ButtonKeyControl(gt);
			foreach (var c in controls) c.Update(gt);
		}
	}
	
}
