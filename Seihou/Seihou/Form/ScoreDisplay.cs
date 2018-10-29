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
    class ScoreDisplay : Control
    {
		string filterMode = "infinite";

		List<Score> scores = new List<Score>();

		public Vector2 pos;
		public Vector2 size;
		const string font = "DefaultFont";
		const int maxLength = 10;
        const int ySpacing = 40;

		readonly string connectionString;

		string[] displayText;

		public ScoreDisplay(Vector2 pos,Vector2 size,SpriteBatch sb, string connectionString) : base(sb)
        {
			this.connectionString = connectionString;
			this.size = size;
			this.pos = pos;

			RefreshAll();
		}

		public void InsertInto(string score,string name,string mode)
		{
			//Load data from MDF
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					var c = new SqlCommand($"INSERT INTO [Highscores] (Score,Name,Mode) VALUES ({score},'{name}','{mode}')");
					c.Connection = con; 
					c.CommandTimeout = 1;

					using (SqlDataReader reader = c.ExecuteReader())
					{
						while (reader.Read())
						{
							scores.Add(new Score()
							{
								score = reader[0].ToString(),
								name = reader[1].ToString(),
								mode = reader[2].ToString()
							});
						}
					}
				}
			}
			catch (Exception e)
			{
				Debugging.Write(this,"Could not insert score : " + e.Message);
			}
			RefreshAll();
		}

		public void RefreshAll()
		{
			//Load data from MDF
			scores.Clear();
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					var c = new SqlCommand("SELECT * FROM [Highscores] ORDER BY [score] DESC");
					c.Connection = con;
					c.CommandTimeout = 1;

					using (SqlDataReader reader = c.ExecuteReader())
					{
						while (reader.Read())
						{
							scores.Add(new Score()
							{
								score = reader[0].ToString(),
								name = reader[1].ToString(),
								mode = reader[2].ToString()
							});
						}
					}
				}
			}
			catch (Exception e)
			{
				Debugging.Write(this, e.Message);
				scores.Add(new Score() { name = "Error." });
			}

			UpdateDisplayText();
		}

        public override void Draw(GameTime gt)
        {
			MonoGame.Primitives2D.FillRectangle(sb, pos, size, background, 0);

			for (int i = 0; i < displayText.Length; i++)
			{
				sb.DrawString(ResourceManager.fonts[font], displayText[i] ?? "Empty", new Vector2(pos.X, pos.Y + (i * ySpacing)), textColor);
			}			
        }

        public override void Update(GameTime gt)
        {

        }

		public void SetModeFilter(string mode)
		{
			filterMode = mode;
			UpdateDisplayText();
		}

		private void UpdateDisplayText()
		{
			displayText = new string[maxLength]; 
			int index = 0;
			foreach (Score s in scores)
			{
				if (filterMode == s.mode) 
				{
					displayText[index] = Clip($"#{index+1}",4) + s.ToString();

					index++;
					if (index >= maxLength) break;
				}
			}
		}

		private class Score
		{
			public string name;
			public string mode;
			public string score;

			public override string ToString()
			{
				return $"{Clip(name, 20)} | {Clip(score, 20)}";
			}
		}

		private static string Clip(string str, int length)
		{
			if (str.Length > length)
				return str.Substring(0, length);

			if (str.Length < length)
				return str + new string(' ', length - str.Length);

			return str;
		}
	}
}
