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

        private readonly FormInputHandler input = new();
        private readonly List<Control> controls = new();

        private int tabindex = -1;

        public FormHost()
        {
            input.OnPressed += InputOnPressed;
            input.OnReleased += InputOnReleased;
            input.OnTabIndexChanged += InputOnTabIndexChanged;
            input.OnKeyboardPress += InputOnKeyboardPress;
        }

        private void InputOnKeyboardPress() => ForAll(c => c.OnKeyboardPress());
        private void InputOnReleased() => ForAll(c => c.Release());
        private void InputOnPressed() => ForAll(c => c.Press());

        private void InputOnTabIndexChanged(int change)
        {
            if (tabindex == -1)
            {
                tabindex = DefaultTabIndex;
            }
            else
            {
                var max = GetMaxTabIndex();
                tabindex += change;
                if (tabindex > max) tabindex = 0;
                if (tabindex < 0) tabindex = max;
            }

            ForAll(c => c.SetTabIndex(tabindex));
        }

        public void AddControl(Control control) => controls.Add(control);

        public void Draw(GameTime gt) => ForAll(c => c.Draw(gt));
        public void SetTabIndex(int index) => ForAll(c => c.SetTabIndex(index));
        public void Update(GameTime gt)
        {
            input.Update();
            ForAll(c => c.Update(gt));
            ForAll(c => c.Update(gt));

            ForAll(c => c.MouseMove(input.LastPosition));
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
