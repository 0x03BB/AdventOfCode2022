using System.Runtime.Intrinsics.X86;
using System.Runtime.Intrinsics;

namespace Day2;

internal static class SumLines
{
    private static readonly int[] map = new int[9] { 4, 8, 3, 1, 5, 9, 7, 2, 6 };
    private static readonly Vector256<byte> mapVector = Vector256.Create(
        4, 8, 3, 0, 1, 5, 9, 0, 7, 2, 6, 0, 0, 0, 0, 0,
        4, 8, 3, 0, 1, 5, 9, 0, 7, 2, 6, 0, 0, 0, 0, (byte)0);

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

    public static int UseUnrolled4()
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

    public static int UseUnrolled8()
    {
        using var stream = File.OpenRead("input.txt");

        int score1, score2, score3, score4, score5, score6, score7, score8;
        score1 = score2 = score3 = score4 = score5 = score6 = score7 = score8 = 0;
        int opponent1, opponent2, opponent3, opponent4, opponent5, opponent6, opponent7, opponent8;
        int player1, player2, player3, player4, player5, player6, player7, player8;
        Span<byte> buffer = stackalloc byte[32];
        int read;
        while ((read = stream.Read(buffer)) == 32)
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

            opponent5 = (buffer[16] - 65) * 3; // A
            player5 = buffer[18] - 88; // X
            score5 += map[opponent5 + player5];

            opponent6 = (buffer[20] - 65) * 3; // A
            player6 = buffer[22] - 88; // X
            score6 += map[opponent6 + player6];

            opponent7 = (buffer[24] - 65) * 3; // A
            player7 = buffer[26] - 88; // X
            score7 += map[opponent7 + player7];

            opponent8 = (buffer[28] - 65) * 3; // A
            player8 = buffer[30] - 88; // X
            score8 += map[opponent8 + player8];
        }

        if (read == 28) { goto read28; }
        else if (read == 24) { goto read24; }
        else if (read == 20) { goto read20; }
        else if (read == 16) { goto read16; }
        else if (read == 12) { goto read12; }
        else if (read == 8) { goto read8; }
        else if (read == 4) { goto read4; }
        else { goto read0; }

    read28:
        opponent7 = (buffer[24] - 65) * 3; // A
        player7 = buffer[26] - 88; // X
        score7 += map[opponent7 + player7];
    read24:
        opponent6 = (buffer[20] - 65) * 3; // A
        player6 = buffer[22] - 88; // X
        score6 += map[opponent6 + player6];
    read20:
        opponent5 = (buffer[16] - 65) * 3; // A
        player5 = buffer[18] - 88; // X
        score5 += map[opponent5 + player5];
    read16:
        opponent4 = (buffer[12] - 65) * 3; // A
        player4 = buffer[14] - 88; // X
        score4 += map[opponent4 + player4];
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

        return score1 + score2 + score3 + score4 + score5 + score6 + score7 + score8;
    }

    public static int UseIntrinsics()
    {
        using var stream = File.OpenRead("input.txt");

        var zero = Vector256<byte>.Zero;
        var accumlator = Vector256<ushort>.Zero;

        var opponentOffset = Vector256.Create((byte)65); // A
        var playerOffset = Vector256.Create((byte)88); // X

        Span<byte> buffer = stackalloc byte[128];
        int read;
        while ((read = stream.Read(buffer)) == 128)
        {
            var opponent = Vector256.Create(
                buffer[0], buffer[4], buffer[8], buffer[12], buffer[16], buffer[20], buffer[24], buffer[28],
                buffer[32], buffer[36], buffer[40], buffer[44], buffer[48], buffer[52], buffer[56], buffer[60],
                buffer[64], buffer[68], buffer[72], buffer[76], buffer[80], buffer[84], buffer[88], buffer[92],
                buffer[96], buffer[100], buffer[104], buffer[108], buffer[112], buffer[116], buffer[120], buffer[124]);
            var player = Vector256.Create(
                buffer[2], buffer[6], buffer[10], buffer[14], buffer[18], buffer[22], buffer[26], buffer[30],
                buffer[34], buffer[38], buffer[42], buffer[46], buffer[50], buffer[54], buffer[58], buffer[62],
                buffer[66], buffer[70], buffer[74], buffer[78], buffer[82], buffer[86], buffer[90], buffer[94],
                buffer[98], buffer[102], buffer[106], buffer[110], buffer[114], buffer[118], buffer[122], buffer[126]);

            opponent = Avx2.Subtract(opponent, opponentOffset);
            opponent = Avx2.Add(opponent, opponent); // There is no multiply or left shift for byte vectors.
            opponent = Avx2.Add(opponent, opponent); // This is equivilent to multiply by 4 or left shift by 2.
            player = Avx2.Subtract(player, playerOffset);

            var indices = Avx2.Add(opponent, player);
            var scores = Avx2.Shuffle(mapVector, indices);

            // Unpack to ushorts and add to accumulator.
            var lowScores = Avx2.UnpackLow(scores, zero).AsUInt16();
            accumlator = Avx2.Add(accumlator, lowScores);
            var highScores = Avx2.UnpackHigh(scores, zero).AsUInt16();
            accumlator = Avx2.Add(accumlator, highScores);
        }

        // Add accumulator values together.
        Span<ushort> scoresArray = stackalloc ushort[16];
        accumlator.CopyTo(scoresArray);
        int score = 0;
        for (var i = 0; i < 16; i++)
        {
            score += scoresArray[i];
        }

        // Process remaining lines sequentially.
        int opponentScalar;
        int playerScalar;
        for (var i = 0; i < read; i += 4)
        {
            opponentScalar = (buffer[i] - 65) * 3; // A
            playerScalar = buffer[i + 2] - 88; // X
            score += map[opponentScalar + playerScalar];
        }

        return score;
    }
}
