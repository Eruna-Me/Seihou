using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Seihou
{
	internal class ScoreRepository
	{
		private List<ScoreRecord> _scores = new();

		private const string SAVE_FILE = "Highscores.score";

		public IOrderedEnumerable<ScoreRecord> GetScoreRecords() => _scores.OrderByDescending(s => s.Score);

		public IOrderedEnumerable<ScoreRecord> GetScoreRecords(Settings.Difficulty difficulty) => _scores
			.Where(s => s.Difficulty == difficulty)
			.OrderByDescending(s => s.Score);

		public void Write(ScoreRecord record)
		{
			_scores.Add(record);
		}
		
		public void Save()
		{
			var json = string.Concat(JsonConvert.SerializeObject(_scores).Reverse());
			var fileStream = File.OpenWrite(SAVE_FILE);

			var algoritm = GetAes();
			ICryptoTransform encryptor = algoritm.CreateEncryptor();

			using CryptoStream cryptoStream = new(fileStream, encryptor, CryptoStreamMode.Write);
			using StreamWriter writer = new(cryptoStream);
			writer.Write(json);
			writer.Flush();
		}

		public void Load()
		{
			if (File.Exists(SAVE_FILE))
			{
				var fileStream = File.OpenRead(SAVE_FILE);

				var algoritm = GetAes();
				ICryptoTransform decryptor = algoritm.CreateDecryptor();

				using CryptoStream cryptoStream = new(fileStream, decryptor, CryptoStreamMode.Read);
				using StreamReader reader = new(cryptoStream);

				var json = string.Concat(reader.ReadToEnd().Reverse());

				_scores = JsonConvert.DeserializeObject<List<ScoreRecord>>(json).ToList();
			}
		}

		private static Aes GetAes()
		{
			var aes = Aes.Create();
			aes.Padding = PaddingMode.Zeros;
			aes.Key =																																																																																																																																																																																																																																																																						                                                                       BuildKey(32, $"{Environment.MachineName} {Environment.UserName} BOB5 SECURE DATA SYSTEMS");
			aes.IV =																																																																																																																																																																																																																																																																						                                                                       BuildKey(16, "BOB5"); //Nani?
			return aes;
		}

		private static byte[] BuildKey(int size, string text)
		{
			string key = text;
			Rfc2898DeriveBytes rfc = new(key, Encoding.UTF8.GetBytes("Security, string"));
			return rfc.GetBytes(size);
		}
	}
}
