using System;

public static class UserInterface
{
    public static int ReadInt(string text, int min, int max)
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

    public static Money ReadMoney(string title)
    {
        Console.WriteLine(title);
        int rubles = ReadInt("Рубли: ", 0, 1000000);
        int kopeks = ReadInt("Копейки: ", 0, 99);
        return new Money(rubles, kopeks);
    }

    public static void PrintMenu(string title, params string[] items)
    {
        Console.WriteLine();
        Console.WriteLine(title);
        foreach (string item in items)
            Console.WriteLine(item);
    }

    public static void PrintMessage(string text)
    {
        Console.WriteLine(text);
    }
}
