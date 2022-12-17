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
            opponent = (buffer[0] - 65) * 3; // A
            player = buffer[2] - 88; // X
            score += map[opponent + player];
        }

        return score;
    }

    public static int UseUnrolled()
    {
        using var stream = File.OpenRead("input.txt");

        int score1, score2, score3, score4;
        score1 = score2 = score3 = score4 = 0;
        int opponent1, opponent2, opponent3, opponent4;
        int player1, player2, player3, player4;
        Span<byte> buffer = stackalloc byte[16];
        int read;
        while ((read = stream.Read(buffer)) == 16)
        {
            opponent1 = (buffer[0] - 65) * 3; // A
            player1 = buffer[2] - 88; // X
            score1 += map[opponent1 + player1];

            opponent2 = (buffer[4] - 65) * 3; // A
            player2 = buffer[6] - 88; // X
            score2 += map[opponent2 + player2];

            opponent3 = (buffer[8] - 65) * 3; // A
            player3 = buffer[10] - 88; // X
            score3 += map[opponent3 + player3];

            opponent4 = (buffer[12] - 65) * 3; // A
            player4 = buffer[14] - 88; // X
            score4 += map[opponent4 + player4];
        }

        if (read == 12) { goto read12; }
        else if (read == 8) { goto read8; }
        else if (read == 4) { goto read4; }
        else { goto read0; }

    read12:
        opponent3 = (buffer[8] - 65) * 3; // A
        player3 = buffer[10] - 88; // X
        score3 += map[opponent3 + player3];
    read8:
        opponent2 = (buffer[4] - 65) * 3; // A
        player2 = buffer[6] - 88; // X
        score2 += map[opponent2 + player2];
    read4:
        opponent1 = (buffer[0] - 65) * 3; // A
        player1 = buffer[2] - 88; // X
        score1 += map[opponent1 + player1];
    read0:

        return score1 + score2 + score3 + score4;
    }
}
