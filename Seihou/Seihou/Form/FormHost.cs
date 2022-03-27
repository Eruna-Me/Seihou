using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seihou
{
    internal class FormHost
    {
        public IReadOnlyList<Control> Controls => controls;
        public int DefaultTabIndex { get; set; } = 0;
        public bool BlockUserInput { set => input.Block = value; get => input.Block; }
        public bool BlockKeyboardUserInput { set => input.BlockKeyboard = value; get => input.BlockKeyboard; }

        private readonly FormInputHandler input = new();
        private readonly List<Control> controls = new();

        private int tabIndex = -1;

        public FormHost()
        {
            input.OnPressed += InputOnPressed;
            input.OnReleased += InputOnReleased;
            input.OnTabIndexChanged += InputOnTabIndexChanged;
            input.OnKeyboardPress += InputOnKeyboardPress;
        }

        public void ResetTabIndex()
        {
            tabIndex = -1;
            ForAll(c => c.SetTabIndex(tabIndex));
        }

        public void AddControl(Control control) => controls.Add(control);
        public void Draw(GameTime gt) => ForAll(c => c.Draw(gt));

        public void Update(GameTime gt)
        {
            input.Update();
            ForAll(c => c.Update(gt));
            ForAll(c => c.Update(gt));
            ForAll(c => c.MouseMove(input.LastPosition));
        }

        private void InputOnKeyboardPress() => ForAll(c => c.OnKeyboardPress());
        private void InputOnReleased() => ForAll(c => c.Release());
        private void InputOnPressed() => ForAll(c => c.Press());

        private void InputOnTabIndexChanged(int change)
        {
            if (tabIndex == -1)
            {
                tabIndex = DefaultTabIndex;
            }
            else
            {
                var max = GetMaxTabIndex();
                tabIndex += change;
                if (tabIndex > max) tabIndex = 0;
                if (tabIndex < 0) tabIndex = max;
            }

            ForAll(c => c.SetTabIndex(tabIndex));
        }

        private int GetMaxTabIndex()
        {
            return controls
                .Where(c => c.TabIndex != -1)
                .Max(c => c.TabIndex);
        }

        private void ForAll(Action<Control> action)
        {
            foreach(var control in controls)
            {
                action(control);
            }
        }
    }
}
