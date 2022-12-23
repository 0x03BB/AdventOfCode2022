using System.Runtime.Intrinsics.X86;
using System.Runtime.Intrinsics;

namespace Day2;

internal static class SumLines
{
    private static readonly int[] scoresMap = new int[9] { 4, 8, 3, 1, 5, 9, 7, 2, 6 };
    private static readonly Vector256<byte> scoresVectorMap = Vector256.Create(
        4, 8, 3, 0, 1, 5, 9, 0, 7, 2, 6, 0, 0, 0, 0, 0,
        4, 8, 3, 0, 1, 5, 9, 0, 7, 2, 6, 0, 0, 0, 0, (byte)0);

    //O1, s,P1, n,O2, s,P2, n
    //O3, s,P3, n,O4, s,P4, n
    //O5, s,P5, n,O6, s,P6, n
    //O7, s,P7, n,O8, s,P8, n
    //
    //O1,P1,  ,  ,O2,P2,  ,
    //  ,  ,O3,P3,  ,  ,O4,P4
    //O5,P5,  ,  ,O6,P6,  ,
    //  ,  ,O7,P7,  ,  ,O8,P8
    private static readonly Vector256<byte> inputShuffleLowIndicies = Vector256.Create(
        0, 2, 4, 6, 8, 10, 12, 14, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80,
        0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0, 2, 4, 6, 8, 10, 12, 14);
    private static readonly Vector256<byte> inputShuffleHighIndicies = Vector256.Create(
        0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0, 2, 4, 6, 8, 10, 12, 14,
        0, 2, 4, 6, 8, 10, 12, 14, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80);
    private static readonly Vector256<byte> offsets = Vector256.Create(
        65, 88, 65, 88, 65, 88, 65, 88, 65, 88, 65, 88, 65, 88, 65, 88,
        65, 88, 65, 88, 65, 88, 65, 88, 65, 88, 65, 88, 65, 88, 65, (byte)88); // 65 = A, 88 = x

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
            score += scoresMap[opponent + player];
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
            score1 += scoresMap[opponent1 + player1];

            opponent2 = (buffer[4] - 65) * 3; // A
            player2 = buffer[6] - 88; // X
            score2 += scoresMap[opponent2 + player2];

            opponent3 = (buffer[8] - 65) * 3; // A
            player3 = buffer[10] - 88; // X
            score3 += scoresMap[opponent3 + player3];

            opponent4 = (buffer[12] - 65) * 3; // A
            player4 = buffer[14] - 88; // X
            score4 += scoresMap[opponent4 + player4];
        }

        if (read == 12) { goto read12; }
        else if (read == 8) { goto read8; }
        else if (read == 4) { goto read4; }
        else { goto read0; }

    read12:
        opponent3 = (buffer[8] - 65) * 3; // A
        player3 = buffer[10] - 88; // X
        score3 += scoresMap[opponent3 + player3];
    read8:
        opponent2 = (buffer[4] - 65) * 3; // A
        player2 = buffer[6] - 88; // X
        score2 += scoresMap[opponent2 + player2];
    read4:
        opponent1 = (buffer[0] - 65) * 3; // A
        player1 = buffer[2] - 88; // X
        score1 += scoresMap[opponent1 + player1];
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
            score1 += scoresMap[opponent1 + player1];

            opponent2 = (buffer[4] - 65) * 3; // A
            player2 = buffer[6] - 88; // X
            score2 += scoresMap[opponent2 + player2];

            opponent3 = (buffer[8] - 65) * 3; // A
            player3 = buffer[10] - 88; // X
            score3 += scoresMap[opponent3 + player3];

            opponent4 = (buffer[12] - 65) * 3; // A
            player4 = buffer[14] - 88; // X
            score4 += scoresMap[opponent4 + player4];

            opponent5 = (buffer[16] - 65) * 3; // A
            player5 = buffer[18] - 88; // X
            score5 += scoresMap[opponent5 + player5];

            opponent6 = (buffer[20] - 65) * 3; // A
            player6 = buffer[22] - 88; // X
            score6 += scoresMap[opponent6 + player6];

            opponent7 = (buffer[24] - 65) * 3; // A
            player7 = buffer[26] - 88; // X
            score7 += scoresMap[opponent7 + player7];

            opponent8 = (buffer[28] - 65) * 3; // A
            player8 = buffer[30] - 88; // X
            score8 += scoresMap[opponent8 + player8];
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
        score7 += scoresMap[opponent7 + player7];
    read24:
        opponent6 = (buffer[20] - 65) * 3; // A
        player6 = buffer[22] - 88; // X
        score6 += scoresMap[opponent6 + player6];
    read20:
        opponent5 = (buffer[16] - 65) * 3; // A
        player5 = buffer[18] - 88; // X
        score5 += scoresMap[opponent5 + player5];
    read16:
        opponent4 = (buffer[12] - 65) * 3; // A
        player4 = buffer[14] - 88; // X
        score4 += scoresMap[opponent4 + player4];
    read12:
        opponent3 = (buffer[8] - 65) * 3; // A
        player3 = buffer[10] - 88; // X
        score3 += scoresMap[opponent3 + player3];
    read8:
        opponent2 = (buffer[4] - 65) * 3; // A
        player2 = buffer[6] - 88; // X
        score2 += scoresMap[opponent2 + player2];
    read4:
        opponent1 = (buffer[0] - 65) * 3; // A
        player1 = buffer[2] - 88; // X
        score1 += scoresMap[opponent1 + player1];
    read0:

        return score1 + score2 + score3 + score4 + score5 + score6 + score7 + score8;
    }

    public static int UseIntrinsics()
    {
        using var stream = File.OpenRead("input.txt");

        var zero = Vector256<byte>.Zero;
        var one = Vector256.Create<sbyte>(1);
        var accumlator = Vector256<ushort>.Zero;

        Span<byte> buffer = stackalloc byte[64];
        int read;
        while ((read = stream.Read(buffer)) == 64)
        {
            var first = Vector256.Create<byte>(buffer);
            var firstInt = Vector256.Shuffle(first, inputShuffleLowIndicies).AsUInt32();
            var second = Vector256.Create<byte>(buffer.Slice(32, 32));
            var secondInt = Vector256.Shuffle(second, inputShuffleHighIndicies).AsUInt32();
            var whole = Avx2.Blend(firstInt, secondInt, 0x3c).AsByte();

            whole = Avx2.Subtract(whole, offsets);
            //opponent = Avx2.Add(opponent, opponent); // There is no multiply or left shift for byte vectors.
            //opponent = Avx2.Add(opponent, opponent); // This is equivilent to multiply by 4 or left shift by 2.

            var indices = Avx2.MultiplyAddAdjacent(whole, one).AsByte();
            var scores = Avx2.Shuffle(scoresVectorMap, indices).AsUInt16();
            
            accumlator = Avx2.Add(accumlator, scores);
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
            score += scoresMap[opponentScalar + playerScalar];
        }

        return score;
    }
}
