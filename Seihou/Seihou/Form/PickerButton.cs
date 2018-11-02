using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
	class PickerButton : Control
	{
		int selected = 0;
		readonly string[] answers;
		public string question;
		Button button;

		public PickerButton(Vector2 pos, Vector2 size, SpriteBatch sb, string question, string startsWith,params string[] answers) : base(sb)
		{
			for (int i = 0; i < answers.Length; ++i) selected = answers[i] == startsWith ? i : selected;
			this.question = question;
			this.answers = answers;
			button = new Button(pos, size, sb, OnClicked, question, Button.Align.left);
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
			button.text = $"{question}:  {answers[selected]}";
			button.Update(gt);
		}
	}
}
