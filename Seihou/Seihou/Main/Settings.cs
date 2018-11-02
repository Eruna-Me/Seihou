using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


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

		readonly static string connectionString = $"Connection Timeout=30;Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\\GameData.mdf;Integrated Security=True;";
		static readonly Dictionary<string, string> data = new Dictionary<string, string>();

		public static string Get(string setting) => data[setting];
		public static int GetInt(string setting) => Convert.ToInt32(data[setting]);
		public static bool GetBool(string setting) => Convert.ToBoolean(data[setting]);
		public static Keys GetKey(string setting) => (Keys)Convert.ToInt32(data[setting]);
		public static Difficulty GetDifficulty() => difficulty;
		public static void Set(string setting, string value) => data[setting] = value;
		public static void SetDifficulty(Difficulty value) => difficulty = value;

		public static void ImportSettings()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					data.Clear();
					con.Open();

					var c = new SqlCommand($"SELECT * FROM Settings");
					c.Connection = con;
					c.CommandTimeout = 1;

					using (SqlDataReader reader = c.ExecuteReader())
					{
						while (reader.Read())
						{
							data.Add($"{reader[0]}",$"{reader[1]}");
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("!!! [WARNING] COULD NOT IMPORT SETTINGS");
			}
		}

		public static void ExportSettings()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					var deleteAll = new SqlCommand("DELETE FROM Settings");
					deleteAll.Connection = con;
					deleteAll.CommandTimeout = 1;
					deleteAll.ExecuteNonQuery();

					foreach (var kp in data)
					{
						var c = new SqlCommand($"INSERT INTO Settings ([Setting],[Value]) VALUES('{kp.Key}','{kp.Value}')");
						c.Connection = con;
						c.CommandTimeout = 1;
						c.ExecuteNonQuery();
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("!!! [WARNING] COULD NOT EXPORT SETTINGS");
			}
		}
	}
}
