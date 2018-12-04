using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
	class PickerButton : Control
	{
		int selected = 0;
		readonly string[] answers;
		public string question;
		Button button;
		readonly string text;

		public PickerButton(Vector2 pos, Vector2 size, SpriteBatch sb, string text, string question, string startsWith, int index, params string[] answers) : base(sb)
		{
			for (int i = 0; i < answers.Length; ++i) selected = answers[i] == startsWith ? i : selected;
			this.question = question;
			this.answers = answers;
			this.text = text;
			button = new Button(pos, size, sb, OnClicked, question, index, Button.Align.left);
			this.sb = sb;
		}

		private void OnClicked(object sender)
		{
			++selected;
			if (selected >= answers.Length)
				selected = 0;
		}

		public string GetAnswer() => answers[selected];

		public override void Draw(GameTime gt)
		{
			button.Draw(gt);
		}

		public override void Update(GameTime gt)
		{
			button.text = $"{text}:  {answers[selected]}";
			button.Update(gt);
		}
	}
}
