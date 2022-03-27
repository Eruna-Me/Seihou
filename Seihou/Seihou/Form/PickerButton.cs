using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Seihou
{
    internal class PickerButton : Button
    {
        int selected = 0;
        readonly string[] answers;
        public string question;
        readonly string text;

        public PickerButton(Vector2 pos, Vector2 size, SpriteBatch sb, string text, string question, string startsWith, int index, params string[] answers) : 
            this(sb, text, question, startsWith, answers)
        {
            TabIndex = index;
            Position = pos;
            Size = size;
        }

        public PickerButton(SpriteBatch sb, string text, string question, string startsWith, params string[] answers) : base(sb)
        {
            for (int i = 0; i < answers.Length; ++i) selected = answers[i] == startsWith ? i : selected;
            this.question = question;
            this.answers = answers;
            this.text = text;
            this.sb = sb;

            OnReleased += (_,_) => OnClicked();
        }

        private void OnClicked()
        {
            ++selected;
            if (selected >= answers.Length)
                selected = 0;
        }

        public string GetAnswer() => answers[selected];

        public override void Update(GameTime gt)
        {
            Text = $"{text}:  {answers[selected]}";

            base.Update(gt);
        }
    }
}
