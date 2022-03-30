using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Seihou
{
    class SettingsState : State
    {
        private readonly FormHost host = new();

        static Vector2 ButtonStart = new(30, 100);
        static Vector2 ButtonSize = new(500, 50);
        static Vector2 spacing = new(0, ButtonSize.Y);

        public SettingsState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
            int i = 0;
            void Block(bool block) => host.BlockUserInput = block;

            //host.AddControl(new PickerButton(spacing * i + ButtonStart, ButtonSize, sb, "Starting Lives", "startingLives", Settings.Get("game", "startingLives"), i++, "1", "2", "3", "4", "5"));
            //host.AddControl(new PickerButton(spacing * i + ButtonStart, ButtonSize, sb, "Starting Bombs", "startingBombs", Settings.Get("game", "startingBombs"), i++, "0", "1", "2", "3"));
            host.AddControl(new PickerButton(spacing * i + ButtonStart, ButtonSize, sb, "Simple Graphics", "simpleGraphics", Settings.Get("graphics", "simpleGraphics"), i++, "True", "False"));
            host.AddControl(new KeyPicker(Block, spacing * i + ButtonStart, ButtonSize, sb, "Move Up", "upKey", Settings.GetKey("upKey"), i++));
            host.AddControl(new KeyPicker(Block, spacing * i + ButtonStart, ButtonSize, sb, "Move Down", "downKey", Settings.GetKey("downKey"), i++));
            host.AddControl(new KeyPicker(Block, spacing * i + ButtonStart, ButtonSize, sb, "Move Left", "leftKey", Settings.GetKey("leftKey"), i++));
            host.AddControl(new KeyPicker(Block, spacing * i + ButtonStart, ButtonSize, sb, "Move Right", "rightKey", Settings.GetKey("rightKey"), i++));
            host.AddControl(new KeyPicker(Block, spacing * i + ButtonStart, ButtonSize, sb, "Precision Mode", "slowKey", Settings.GetKey("slowKey"), i++));
            host.AddControl(new KeyPicker(Block, spacing * i + ButtonStart, ButtonSize, sb, "Fire", "shootKey", Settings.GetKey("shootKey"), i++));
            host.AddControl(new KeyPicker(Block, spacing * i + ButtonStart, ButtonSize, sb, "Bomb", "bombKey", Settings.GetKey("bombKey"), i++));

            foreach(var control in host.Controls)
            {
                if (control is Button button)
                    button.Align = TextAlign.Left;
            }


            host.AddControl(new Button(sb, OnExitScreen)
            {
                Position = new Vector2(300, 670),
                Size = ButtonSize,
                Text = "Save & Exit",
                TabIndex = i++,
            });
        }

        public void OnExitScreen()
        {
            foreach (object b in host.Controls)
            {
                if (b is PickerButton pkb)
                {
                    Settings.Set("controls", pkb.question, pkb.GetAnswer());
                    continue;
                }

                if (b is KeyPicker kp)
                {
                    Settings.SetKey(kp.KeyName, (Keys)int.Parse(kp.GetKey()));
                    continue;
                }
            }

            Settings.ExportSettings();
            sm.ChangeState(new MenuState(sm, cm, sb, gdm));
        }

        public override void Draw(GameTime gt)
        {
            sb.Draw(ResourceManager.textures["Logo"], new Vector2(Global.screenWidth / 2, 100), Color.White);

            sb.DrawString(ResourceManager.fonts["DefaultFont"], "Settings menu", new Vector2(10, 10), Color.White);

            if (host.BlockUserInput)
            {
                sb.DrawString(ResourceManager.fonts["DefaultFont"], "Press the key you wish to use.", new Vector2(670, 550), Color.BlueViolet);
            }

            host.Draw(gt);
        }

        public override void Update(GameTime gt) => host.Update(gt);
    }
}
