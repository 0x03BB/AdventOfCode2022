namespace Day1;

internal static class SumLines
{
    public static int UseStrings()
    {
        int total = 0;
        int subtotal = 0;

        foreach (var line in File.ReadLines("input.txt"))
        {
            if (line == string.Empty)
            {
                total = Math.Max(total, subtotal);
                subtotal = 0;
            }
            else
            {
                subtotal += int.Parse(line);
            }
        }
        total = Math.Max(total, subtotal);

        return total;
    }

    public static int UseBytes()
    {
        int total = 0;
        int subtotal = 0;

        using var stream = File.OpenRead("input.txt");

        Span<char> buffer = stackalloc char[10];
        int input = 0;
        int length = 0;
        while ((input = stream.ReadByte()) != -1)
        {
            if (input == 10) // LF
            {
                if (length == 0)
                {
                    total = Math.Max(total, subtotal);
                    subtotal = 0;
                }
                else
                {
                    subtotal += int.Parse(buffer[..length]);
                    length = 0;
                }
            }
            else
            {
                buffer[length] = (char)input;
                length++;
            }
        }

        return total;
    }
}
