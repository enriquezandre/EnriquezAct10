class Program
{
    public static void Main(string[] args)
    {
        int choice = 1;

        while (choice != 0)
        {
            DisplayMenu();
            choice = GetInput();

            switch (choice)
            {
                case 1:
                    HandleOddEvenParityCheck();
                    break;
                case 2:
                    HandleFindBcc();
                    break;
                case 3:
                    HandleCharacterBitErrorCheck();
                    break;
                case 4:
                    HandleBlockCharacterBits();
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose a valid option.");
                    break;
            }
        }
    }
    static void DisplayMenu()
    {
        Console.WriteLine("Choose an option:");
        Console.WriteLine("[1] Checker for ODD or EVEN Parity");
        Console.WriteLine("[2] Find BCC ");
        Console.WriteLine("[3] Find error for Character Bits and BCC ");
        Console.WriteLine("[4] Input a block of character bits and find its BCC through LRC, EVEN-set or ODD-set Parity bits and the Parity bit of the BCC");
        Console.WriteLine("[0] Exit");
    }

    static void HandleOddEvenParityCheck()
    {
        Console.WriteLine("How many columns?");
        int row = GetInput();
        int[] input = GetOneDInputs(row);

        Console.WriteLine("ODD or EVEN?");
        string parity = Console.ReadLine()!;
        Console.WriteLine(OddOrEvenParity(input, parity) ? "No error" : "Error!");
    }

    static void HandleFindBcc()
    {
        Console.WriteLine("How many rows?");
        int row = GetInput();
        Console.WriteLine("How many columns?");
        int column = GetInput();
        int[,] input = GetTwoDInputs(row, column);
        int[] bcc = FindBcc(input);
        Console.Write("BCC: ");
        foreach (int n in bcc)
        {
            Console.Write(n + " ");
        }
        Console.WriteLine(" ");
    }
    
    static void HandleCharacterBitErrorCheck()
    {
        Console.WriteLine("How many rows of character bits?");
        int row = GetInput();
        Console.WriteLine("How many columns of character bits?");
        int column = GetInput();
        int[,] charBitsInput = GetTwoDInputs(row, column);

        Console.WriteLine("BCC Inputs: ");
        int[] bccInput = GetOneDInputs(row);
        Console.WriteLine(IsBccError(charBitsInput, bccInput) ? "Inputted BCC is correct" : "Inputted BCC does not match actual BCC");
    }

    static void HandleBlockCharacterBits()
    {
        Console.WriteLine("How many rows?");
        int row = GetInput();
        Console.WriteLine("How many columns?");
        int column = GetInput();
        int[,] input = GetTwoDInputs(row, column);
        int[] bcc = FindBcc(input);
        Console.Write("BCC: ");
        foreach (int n in bcc)
        {
            Console.Write(n + " ");
        }
        Console.WriteLine(" ");
        int[] evenParity = GetEvenParity(input);
        int[] oddParity = GetOddParity(evenParity);
        int evenBcc = GetEvenBCCParity(evenParity);
        int oddBcc = GetOddBCCParity(oddParity);
        Console.WriteLine("Even Parity: ");
        for (int i = 0; i < evenParity.Length; i++)
        {
            Console.Write(evenParity[i] + " ");
        }
        Console.WriteLine(" ");
        Console.WriteLine("Odd Parity: ");
        for (int i = 0; i < oddParity.Length; i++)
        {
            Console.Write(oddParity[i] + " ");
        }
        Console.WriteLine(" ");
        Console.Write("BCC Parity: " + evenBcc);
        Console.WriteLine(" ");
    }

    static bool OddOrEvenParity(int[] num, string parity)
    {
        int count = 0;
        for (int i = 0; i < num.Length; i++)
        {
            if (num[i] == 1)
            {
                count++;
            }
        }
        if (parity.Equals("EVEN"))
        {
            return count % 2 == 0;
        }
        else if (parity.Equals("ODD"))
        {
            return count % 2 != 0;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter 'ODD' or 'EVEN'.");
            return false;
        }
    }

    static int[] FindBcc(int[,] num)
    {
        List<int> list = new List<int>();
        int count = 0;
        for (int i = 0; i < num.GetLength(1); i++)
        {
            for (int j = 0; j < num.GetLength(0); j++)
            {
                if (num[j, i] == 1)
                {
                    count++;
                }
            }
            if (count % 2 == 0)
            {
                list.Add(0);
            }
            else
            {
                list.Add(1);
            }
            count = 0;
        }
        return list.ToArray(); ;
    }

    static bool IsBccError(int[,] arr, int[] num)
    {
        int[] bcc = FindBcc(arr);
        return bcc.SequenceEqual(num);
    }

    static int[] GetEvenParity(int[,] num)
    {
        List<int> list = new List<int>();
        int count = 0;
        for (int i = 0; i < num.GetLength(0); i++)
        {
            for (int j = 0; j < num.GetLength(1); j++)
            {
                if (num[i, j] == 1)
                {
                    count++;
                }
            }
            if (count % 2 == 0)
            {
                list.Add(0);
            }
            else
            {
                list.Add(1);
            }
            count = 0;
        }
        return list.ToArray();
    }

    static int[] GetOddParity(int[] num)
    {
        List<int> list = new List<int>();
        foreach (int n in num)
        {
            if (n == 0)
                list.Add(1);
            else
                list.Add(0);
        }
        return list.ToArray();
    }

    static int GetEvenBCCParity(int[] num)
    {
        int count = 0;
        for (int i = 0; i < num.Length; i++)
        {
            if (num[i] == 1)
            {
                count++;
            }
        }
        if (count % 2 == 0)
        {
            return 0;
        }
        return 1;
    }

    static int GetOddBCCParity(int[] num)
    {
        int count = 0;
        for (int i = 0; i < num.Length; i++)
        {
            if (num[i] == 1)
            {
                count++;
            }
        }
        if (count % 2 == 0)
        {
            return 1;
        }
        return 0;
    }

    static int[,] GetTwoDInputs(int row, int column)
    {
        int[,] input = new int[row, column];

        for (int i = 0; i < row; i++)
        {
            while (true)
            {
                Console.Write($"Enter values for row {i} of 0s and 1s, length should be {column}): ");
                string? line = Console.ReadLine();
                if (IsValidInput(line, column))
                {
                    for (int j = 0; j < column; j++)
                    {
                        input[i, j] = line[j] - '0'; // Convert char to int (0 or 1)
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter values of only 0s and 1s with the correct length.");
                }
            }
        }

        return input;
    }

    static int[] GetOneDInputs(int columns)
    {
        int[] input = new int[columns];

        while (true)
        {
            Console.Write($"Enter values of 0s and 1s, length should be {columns}): ");
            string? line = Console.ReadLine();
            if (IsValidInput(line, columns))
            {
                for (int j = 0; j < columns; j++)
                {
                    input[j] = line[j] - '0'; // Convert char to int (0 or 1)
                }
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter values of only 0s and 1s with the correct length.");
            }
        }

        return input;
    }

    static bool IsValidInput(string? input, int expectedLength)
    {
        return input != null && input.Length == expectedLength && input.All(c => c == '0' || c == '1');
    }
    static int GetInput()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
            }
        }
    }
}