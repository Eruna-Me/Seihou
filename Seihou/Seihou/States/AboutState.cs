using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Seihou
{
	class AboutState : State
	{
		private Button button;

		public AboutState (StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
			button = new Button(new Vector2(150,300), new Vector2(300, 50), sb, OnClickedExit, "Back", 0, Button.Align.center);
		}

		private void OnClickedExit(object sender)
		{
			sm.ChangeState(new MenuState(sm, cm, sb, gdm));
		}

		public override void Draw(GameTime gt)
		{
			button.Draw(gt);
			var f = ResourceManager.fonts["DefaultFont"];
			var text = "SEIHOU\nMade in C# \n\nThis game was made by \nDennis & Hidde\nBob5";
			sb.DrawString(f,text, new Vector2(Global.screenWidth / 2, 200), Color.White, 0, f.MeasureString(text)/2, 1, SpriteEffects.None, 0);
        }

		public override void Update(GameTime gt)
		{
			Global.buttonCount = 1;
			Button.ButtonKeyControl(gt);
			button.Update(gt);
		}
	}
}
