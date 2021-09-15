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

		public static string Get(string setting) => data["test"][setting];
		public static int GetInt(string setting) => Convert.ToInt32(data["test"][setting]);
		public static bool GetBool(string setting) => Convert.ToBoolean(data["test"][setting]);
		public static Keys GetKey(string setting) => (Keys)Convert.ToInt32(data["test"][setting]);
		public static Difficulty GetDifficulty() => difficulty;
		public static void Set(string setting, string value) => data["test"][setting] = value;
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
