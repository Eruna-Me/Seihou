using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Xna.Framework.Input;
using IniParser;
using IniParser.Model;


namespace Seihou
{
	static class Settings
	{ 
		public enum Difficulty
		{
			easy,
			normal,
			hard,
			usagi
		}

		static Difficulty difficulty;

		static readonly IniData data = new IniData();

		public static string Get(string section, string setting) => data[section][setting];
		public static int GetInt(string section, string setting) => Convert.ToInt32(data[section][setting]);
		public static bool GetBool(string section, string setting) => Convert.ToBoolean(data[section][setting]);
		public static Difficulty GetDifficulty() => difficulty;
		public static void Set(string section, string setting, string value) => data[section][setting] = value;
		public static Keys GetKey(string setting) => Enum.Parse<Keys>(data["controls"][setting]);
		public static void SetKey(string setting, Keys key) => data["controls"][setting] = key.ToString();
		public static void SetDifficulty(Difficulty value) => difficulty = value;

		public static void ImportSettings()
		{
			var parser = new FileIniDataParser();
			data.Merge(parser.ReadFile("Configuration.ini"));
		}

		public static void ExportSettings()
		{
			var parser = new FileIniDataParser();
			parser.WriteFile("Configuration.ini", data);
		}
	}
}
