using System;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main()
    {
        int[,] matrix = new int[0, 0];
        int[][] jaggedArray = new int[0][];
        string text = "";
        int choice;

        Console.WriteLine("Лаба 5. Вариант 17");

        do
        {
            PrintMenu("Главное меню",
                "1. Двумерный массив",
                "2. Рваный массив",
                "3. Строка",
                "0. Выход");
            choice = ReadInt("Выберите пункт: ", 0, 3);

            switch (choice)
            {
                case 1:
                    WorkWithMatrix(ref matrix);
                    break;
                case 2:
                    WorkWithJaggedArray(ref jaggedArray);
                    break;
                case 3:
                    WorkWithText(ref text);
                    break;
            }
        }
        while (choice != 0);
    }

    private static void WorkWithMatrix(ref int[,] matrix)
    {
        int choice;
        int deletedCount;

        do
        {
            PrintMenu("Двумерный массив",
                "1. Сформировать вручную",
                "2. Сформировать с помощью датчика случайных чисел",
                "3. Вывести массив",
                "4. Удалить строки, в которых есть ноль",
                "0. Назад");
            choice = ReadInt("Выберите пункт: ", 0, 4);

            switch (choice)
            {
                case 1:
                    matrix = CreateMatrix(false);
                    PrintArray(matrix);
                    break;
                case 2:
                    matrix = CreateMatrix(true);
                    PrintArray(matrix);
                    break;
                case 3:
                    PrintArray(matrix);
                    break;
                case 4:
                    if (matrix.Length == 0)
                    {
                        Console.WriteLine("Ошибка: массив пустой, удалять нечего.");
                        break;
                    }
                    matrix = DeleteRowsWithZero(matrix, out deletedCount);
                    Console.WriteLine("Удалено строк: " + deletedCount);
                    PrintArray(matrix);
                    break;
            }
        }
        while (choice != 0);
    }

    private static void WorkWithJaggedArray(ref int[][] jaggedArray)
    {
        int choice;

        do
        {
            PrintMenu("Рваный массив",
                "1. Сформировать вручную",
                "2. Сформировать с помощью датчика случайных чисел",
                "3. Вывести массив",
                "4. Добавить K строк в конец массива",
                "0. Назад");
            choice = ReadInt("Выберите пункт: ", 0, 4);

            switch (choice)
            {
                case 1:
                    jaggedArray = CreateRows(ReadInt("Количество строк: ", 1, 100), false);
                    PrintArray(jaggedArray);
                    break;
                case 2:
                    jaggedArray = CreateRows(ReadInt("Количество строк: ", 1, 100), true);
                    PrintArray(jaggedArray);
                    break;
                case 3:
                    PrintArray(jaggedArray);
                    break;
                case 4:
                    int count = ReadInt("Сколько строк добавить (K): ", 1, 100);
                    int fillChoice = ReadInt("Как заполнить новые строки (1 - вручную, 2 - случайно): ", 1, 2);
                    int[][] newRows = CreateRows(count, fillChoice == 2);
                    jaggedArray = AddRowsToEnd(jaggedArray, newRows);
                    PrintArray(jaggedArray);
                    break;
            }
        }
        while (choice != 0);
    }

    private static void WorkWithText(ref string text)
    {
        string[] testLines =
        {
            "В лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была.",
            "Мама  мыла   раму!  Рама: чистая; мама рада?",
            "Привет."
        };
        int choice;

        do
        {
            PrintMenu("Строка",
                "1. Ввести строку с клавиатуры",
                "2. Выбрать строку из тестовых",
                "3. Вывести строку",
                "4. Сдвинуть каждое слово влево на его номер в строке",
                "0. Назад");
            choice = ReadInt("Выберите пункт: ", 0, 4);

            switch (choice)
            {
                case 1:
                    text = ReadText();
                    PrintText(text);
                    break;
                case 2:
                    PrintTestLines(testLines);
                    int number = ReadInt("Номер строки: ", 1, testLines.Length);
                    text = testLines[number - 1];
                    PrintText(text);
                    break;
                case 3:
                    PrintText(text);
                    break;
                case 4:
                    if (text.Length == 0)
                    {
                        Console.WriteLine("Ошибка: строка пустая, обрабатывать нечего.");
                        break;
                    }
                    PrintText(text, "Исходная строка: ");
                    PrintText(ShiftWords(text), "Результат: ");
                    break;
            }
        }
        while (choice != 0);
    }

    private static int[,] CreateMatrix(bool isRandom)
    {
        int rows = ReadInt("Количество строк: ", 1, 100);
        int columns = ReadInt("Количество столбцов: ", 1, 100);
        if (isRandom)
            return CreateRandomMatrix(rows, columns);
        return ReadMatrix(rows, columns);
    }

    private static int[,] CreateRandomMatrix(int rows, int columns)
    {
        Random random = new Random();
        int[,] matrix = new int[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
                matrix[i, j] = random.Next(0, 10);
        }
        return matrix;
    }

    private static bool HasZero(int[,] matrix, int row)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            if (matrix[row, j] == 0)
                return true;
        }
        return false;
    }

    private static int[,] DeleteRowsWithZero(int[,] matrix, out int deletedCount)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);

        deletedCount = 0;
        for (int i = 0; i < rows; i++)
        {
            if (HasZero(matrix, i))
                deletedCount++;
        }

        int[,] result = new int[rows - deletedCount, columns];
        int newRow = 0;
        for (int i = 0; i < rows; i++)
        {
            if (HasZero(matrix, i))
                continue;
            for (int j = 0; j < columns; j++)
                result[newRow, j] = matrix[i, j];
            newRow++;
        }
        return result;
    }

    private static int[][] CreateRows(int count, bool isRandom)
    {
        int[][] rows = new int[count][];
        for (int i = 0; i < count; i++)
        {
            int length = ReadInt($"Длина строки {i + 1}: ", 1, 100);
            if (isRandom)
                rows[i] = CreateRandomRow(length);
            else
                rows[i] = ReadRow(length);
        }
        return rows;
    }

    private static int[] CreateRandomRow(int length)
    {
        Random random = new Random();
        int[] row = new int[length];
        for (int i = 0; i < length; i++)
            row[i] = random.Next(0, 10);
        return row;
    }

    private static int[][] AddRowsToEnd(int[][] jaggedArray, int[][] newRows)
    {
        int[][] result = new int[jaggedArray.Length + newRows.Length][];
        Array.Copy(jaggedArray, result, jaggedArray.Length);
        Array.Copy(newRows, 0, result, jaggedArray.Length, newRows.Length);
        return result;
    }

    private static string ShiftWords(string text)
    {
        MatchCollection words = Regex.Matches(text, @"[^\s,;:.!?]+");
        string result = "";
        int position = 0;
        int wordNumber = 1;

        foreach (Match word in words)
        {
            result += text.Substring(position, word.Index - position);
            result += ShiftWord(word.Value, wordNumber);
            position = word.Index + word.Length;
            wordNumber++;
        }
        result += text.Substring(position);
        return result;
    }

    private static string ShiftWord(string word, int shift)
    {
        shift = shift % word.Length;
        return word.Substring(shift) + word.Substring(0, shift);
    }

    private static bool IsTextCorrect(string line)
    {
        return Regex.IsMatch(line, @"[.!?]$");
    }

    private static int ReadInt(string text, int min = int.MinValue, int max = int.MaxValue)
    {
        int number = 0;
        bool isCorrect = false;

        do
        {
            Console.Write(text);
            try
            {
                number = Convert.ToInt32(Console.ReadLine());
                if (number < min || number > max)
                    Console.WriteLine($"Ошибка: нужно число от {min} до {max}.");
                else
                    isCorrect = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: нужно ввести целое число.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: слишком большое число.");
            }
        }
        while (!isCorrect);

        return number;
    }

    private static int[,] ReadMatrix(int rows, int columns)
    {
        int[,] matrix = new int[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
                matrix[i, j] = ReadInt($"Элемент [{i + 1}, {j + 1}]: ");
        }
        return matrix;
    }

    private static int[] ReadRow(int length)
    {
        int[] row = new int[length];
        for (int i = 0; i < length; i++)
            row[i] = ReadInt($"Элемент {i + 1}: ");
        return row;
    }

    private static string ReadText()
    {
        Console.WriteLine("Введите строку:");
        string? line = Console.ReadLine();
        while (line == null || !IsTextCorrect(line.Trim()))
        {
            Console.WriteLine("Ошибка: строка не должна быть пустой и должна заканчиваться знаком . ! или ?");
            line = Console.ReadLine();
        }
        return line.Trim();
    }

    private static void PrintMenu(string title, params string[] items)
    {
        Console.WriteLine();
        Console.WriteLine(title);
        foreach (string item in items)
            Console.WriteLine(item);
    }

    private static void PrintArray(int[,] matrix)
    {
        if (matrix.Length == 0)
        {
            Console.WriteLine("Массив пустой.");
            return;
        }

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
                Console.Write(matrix[i, j] + "\t");
            Console.WriteLine();
        }
    }

    private static void PrintArray(int[][] jaggedArray)
    {
        if (jaggedArray.Length == 0)
        {
            Console.WriteLine("Массив пустой.");
            return;
        }

        for (int i = 0; i < jaggedArray.Length; i++)
        {
            foreach (int number in jaggedArray[i])
                Console.Write(number + "\t");
            Console.WriteLine();
        }
    }

    private static void PrintTestLines(string[] lines)
    {
        for (int i = 0; i < lines.Length; i++)
            Console.WriteLine($"{i + 1}. {lines[i]}");
    }

    private static void PrintText(string text, string title = "Строка: ")
    {
        if (text.Length == 0)
        {
            Console.WriteLine("Строка пустая.");
            return;
        }
        Console.WriteLine(title + text);
    }
}
