namespace PathMaximumProbability
{
	internal class Program
	{
		static void Main(string[] args)
		{
			PathMaximumProbability pathMaximumProbability = new();
            Console.WriteLine(pathMaximumProbability.MaxProbability(3, new int[][] { new int[] { 0, 1 }, new int[] { 1, 2 }, new int[] { 0, 2 } }, new double[] { 0.5, 0.5, 0.2 }, 0, 2));
			Console.WriteLine(pathMaximumProbability.MaxProbability(3, new int[][] { new int[] { 0, 1 }, new int[] { 1, 2 }, new int[] { 0, 2 } }, new double[] { 0.5, 0.5, 0.3 }, 0, 2));
			Console.WriteLine(pathMaximumProbability.MaxProbability(3, new int[][] { new int[] { 0, 1 } }, new double[] { 0.5 }, 0, 2));
			Console.WriteLine(pathMaximumProbability.MaxProbability(5, new int[][] {
				new int[] { 1, 4 }, new int[] { 2, 4 }, new int[] { 0, 4 }, new int[] { 0, 3 }, new int[] { 0, 2 }, new int[] { 2, 3 }
			}, new double[] { 0.37, 0.17, 0.93, 0.23, 0.39, 0.04 }, 3, 4));
		}
	}
}