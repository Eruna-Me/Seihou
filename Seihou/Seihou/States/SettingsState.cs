using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections;

namespace Seihou
{
	class SettingsState : State
	{
		List<Control> buttons = new List<Control>();

		static Vector2 ButtonStart = new Vector2(400,100);
		static Vector2 ButtonSize = new Vector2(500, 50);
		static Vector2 spacing = new Vector2(0, ButtonSize.Y);

		public SettingsState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
		{
			int i = 0;
			buttons.Add(new PickerButton(spacing * i + ButtonStart,ButtonSize, sb, "Starting Lives" ,"startingLives" , Settings.Get("startingLives"), i++,"1", "2", "3", "4", "5"));
			buttons.Add(new PickerButton(spacing * i + ButtonStart, ButtonSize, sb, "Starting Bombs" , "startingBombs", Settings.Get("startingBombs"), i++, "0", "1", "2", "3"));
			buttons.Add(new PickerButton(spacing * i + ButtonStart, ButtonSize, sb, "Simple Graphics", "simpleGraphics", Settings.Get("simpleGraphics"), i++, "True", "False"));
			buttons.Add(new KeyPicker(spacing * i + ButtonStart, ButtonSize, sb, "Move Up", "upKey", Settings.GetKey("upKey"), i++));
			buttons.Add(new KeyPicker(spacing * i + ButtonStart, ButtonSize, sb, "Move Down", "downKey", Settings.GetKey("downKey"), i++));
			buttons.Add(new KeyPicker(spacing * i + ButtonStart, ButtonSize, sb, "Move Left", "leftKey", Settings.GetKey("leftKey"), i++));
			buttons.Add(new KeyPicker(spacing * i + ButtonStart, ButtonSize, sb, "Move Right", "rightKey", Settings.GetKey("rightKey"), i++));
			buttons.Add(new KeyPicker(spacing * i + ButtonStart, ButtonSize, sb, "Precision Mode", "slowKey", Settings.GetKey("slowKey"), i++));
			buttons.Add(new KeyPicker(spacing * i + ButtonStart, ButtonSize, sb, "Fire", "shootKey", Settings.GetKey("shootKey"), i++));
			buttons.Add(new KeyPicker(spacing * i + ButtonStart, ButtonSize, sb, "Bomb", "bombKey", Settings.GetKey("bombKey"), i++));

			buttons.Add(new Button(new Vector2(300, 670), ButtonSize, sb,OnExit, "Save & Exit", i++));
		}

		public void OnExit(object Sender)
		{
			foreach (object b in buttons)
			{
				if (b is PickerButton pkb)
				{
					Settings.Set(pkb.question, pkb.GetAnswer());
					continue;
				}

				if (b is KeyPicker kp)
				{
					Settings.Set(kp.keyName, kp.GetKey());
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

			sb.DrawString(ResourceManager.fonts["DefaultFont"], "Hold your mouse on \nthe selected key setting \nand press the key you wish to use.", new Vector2(670,500), Color.White);

			foreach (var b in buttons) b.Draw(gt) ;
		}

		public override void Update(GameTime gt)
		{
			Global.buttonCount = buttons.Count;
			Button.ButtonKeyControl(gt);
			foreach (var b in buttons) b.Update(gt);
		}
	}
}
