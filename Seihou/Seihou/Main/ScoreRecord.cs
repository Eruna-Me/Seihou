namespace Seihou
{ 
	internal record ScoreRecord
	{
		public double Score { get; init; }
		public string Name { get; init; }
		public Settings.Difficulty Difficulty { get; init; }
	}
}
