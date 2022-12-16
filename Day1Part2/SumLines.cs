namespace Day1Part2;

internal static class SumLines
{
    private const int TOTAL_COUNT = 3;
    private const int MAX_INDEX = TOTAL_COUNT - 1;

    public static int UseBytesRecurse()
    {
        Span<int> totals = stackalloc int[TOTAL_COUNT];
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
                    _ = Insert(subtotal, ref totals, 0);
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

        int sum = totals[0];
        for (var i = 1; i < TOTAL_COUNT; i++)
        {
            sum += totals[i];
        }
        return sum;

        static int Insert(int value, ref Span<int> collection, int index)
        {
            if (value > collection[index])
            {
                var previousValue = collection[index];
                if (index < MAX_INDEX)
                {
                    collection[index] = Insert(value, ref collection, index + 1);
                }
                else
                {
                    collection[index] = value;
                }
                return previousValue;
            }
            else
            {
                return value;
            }
        }
    }

    public static int UseBytesLoop()
    {
        Span<int> totals = stackalloc int[TOTAL_COUNT];
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
                    Insert(subtotal, ref totals);
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

        int sum = totals[0];
        for (var i = 1; i < TOTAL_COUNT; i++)
        {
            sum += totals[i];
        }
        return sum;

        static void Insert(int value, ref Span<int> collection)
        {
            int index = 0;
            while (value > collection[index])
            {
                if (index > 0)
                {
                    collection[index - 1] = collection[index];
                }
                collection[index] = value;
                index++;
                if (index == TOTAL_COUNT) { break; }
            }
        }
    }
}
