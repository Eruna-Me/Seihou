using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    class QuestionState : State
	{
		private readonly double _score;
		private readonly Settings.Difficulty _difficulty;

		Vector2 buttonSize = new(400, 60);
		readonly FormHost host = new();

		public QuestionState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm,double score, Settings.Difficulty difficulty) : base(sm, cm,sb,gdm)
		{
			_difficulty = difficulty;
			_score = score;

			host.AddControl(new Button(sb, No)
			{
				TabIndex = 0,
				Position = new Vector2(Global.screenWidth / 2 - buttonSize.X / 2, Global.screenHeight / 2 - 80),
				Size = buttonSize,
				Text = "No"
			});

			host.AddControl(new Button(sb, Yes)
			{
				TabIndex = 1,
				Position = new Vector2(Global.screenWidth / 2 - buttonSize.X / 2, Global.screenHeight / 2 + 80),
				Size = buttonSize,
				Text = "Yes"
			});
		}

		private void Yes()
		{
			var data = new HighscoreState.PlayData
			{
				Score = _score,
				Difficulty = _difficulty,
			};

			sm.ChangeState(new HighscoreState(sm, cm, sb, gdm, data));
		}

		private void No()
		{
			sm.ChangeState(new MenuState(sm, cm, sb, gdm));
		}

		public override void Draw(GameTime gt)
		{
			var txt = "Do you want to save your PATHETIC score?";
			sb.DrawString(ResourceManager.fonts["DefaultFont"],txt,new Vector2(Global.screenWidth/2,100),Color.White,0,new Vector2(ResourceManager.fonts["DefaultFont"].MeasureString(txt).X/2,0),1,SpriteEffects.None,0);
			host.Draw(gt);
		}

		public override void Update(GameTime gt)
		{
			host.Update(gt);
		}
	}
	
}
