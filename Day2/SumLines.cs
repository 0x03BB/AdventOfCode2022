namespace Day2;

internal static class SumLines
{
    private static readonly int[] map = new int[9] { 4, 8, 3, 1, 5, 9, 7, 2, 6 };

    public static int UseSequential()
    {
        using var stream = File.OpenRead("input.txt");

        int score = 0;
        int opponent;
        int player;
        Span<byte> buffer = stackalloc byte[4];
        while (stream.Read(buffer) == 4)
        {
            opponent = buffer[0] - 65; // A
            opponent = (opponent << 1) + opponent;
            player = buffer[2] - 88; // X
            score += map[opponent + player];
        }
        
        return score;
    }
}
