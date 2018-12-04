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
		static float spacing = ButtonSize.Y;

		public SettingsState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
		{
			Vector2 iPos = ButtonStart;
			buttons.Add(new PickerButton(iPos,ButtonSize, sb, "Starting Lives" ,"startingLives" , Settings.Get("startingLives"), "1", "2", "3", "4", "5"));
			iPos += new Vector2(0, spacing);
			buttons.Add(new PickerButton(iPos, ButtonSize, sb, "Starting Bombs" , "startingBombs", Settings.Get("startingBombs"),"0", "1", "2", "3"));
			iPos += new Vector2(0, spacing);
			buttons.Add(new PickerButton(iPos, ButtonSize, sb, "Simple Graphics", "simpleGraphics", Settings.Get("simpleGraphics"), "True", "False"));
			iPos += new Vector2(0, spacing*2);
			buttons.Add(new KeyPicker(iPos, ButtonSize, sb, "Move Up", "upKey", Settings.GetKey("upKey")));
			iPos += new Vector2(0, spacing);
			buttons.Add(new KeyPicker(iPos, ButtonSize, sb, "Move Down", "downKey", Settings.GetKey("downKey")));
			iPos += new Vector2(0, spacing);
			buttons.Add(new KeyPicker(iPos, ButtonSize, sb, "Move Left", "leftKey", Settings.GetKey("leftKey")));
			iPos += new Vector2(0, spacing);
			buttons.Add(new KeyPicker(iPos, ButtonSize, sb, "Move Right", "rightKey", Settings.GetKey("rightKey")));
			iPos += new Vector2(0, spacing);
			buttons.Add(new KeyPicker(iPos, ButtonSize, sb, "Precision Mode", "slowKey", Settings.GetKey("slowKey")));
			iPos += new Vector2(0, spacing);
			buttons.Add(new KeyPicker(iPos, ButtonSize, sb, "Fire", "shootKey", Settings.GetKey("shootKey")));
			iPos += new Vector2(0, spacing);
			buttons.Add(new KeyPicker(iPos, ButtonSize, sb, "Bomb", "bombKey", Settings.GetKey("bombKey")));

			buttons.Add(new Button(new Vector2(300, 670), ButtonSize, sb,OnExit, "Save & Exit"));
		}

		public void OnExit(object Sender)
		{
			foreach (object b in buttons)
			{
				if (b is PickerButton)
				{
					var pkb = (PickerButton)b;
					Settings.Set(pkb.question, pkb.GetAnswer());
					continue;
				}

				if (b is KeyPicker)
				{
					var kp = (KeyPicker)b;
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
