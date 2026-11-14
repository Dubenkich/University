using System;

public class MoneyArray
{
    private Money[] arr;
    private static int count = 0;

    public int Length => arr.Length;

    public static int Count => count;

    public MoneyArray()
    {
        arr = new Money[0];
        count++;
    }

    // Заполнение случайными суммами: от 0 до 100 рублей и от 0 до 99 копеек
    public MoneyArray(int length, Random random)
    {
        arr = new Money[length];
        for (int i = 0; i < length; i++)
            arr[i] = new Money(random.Next(0, 101), random.Next(0, 100));
        count++;
    }

    // Заполнение суммами, которые пользователь вводит с клавиатуры
    public MoneyArray(int length)
    {
        arr = new Money[length];
        for (int i = 0; i < length; i++)
            arr[i] = UserInterface.ReadMoney($"Элемент [{i}]");
        count++;
    }

    public Money this[int index]
    {
        get
        {
            CheckIndex(index);
            return arr[index];
        }
        set
        {
            CheckIndex(index);
            arr[index] = value;
        }
    }

    public void Show()
    {
        if (arr.Length == 0)
        {
            UserInterface.PrintMessage("Массив пустой.");
            return;
        }

        for (int i = 0; i < arr.Length; i++)
            UserInterface.PrintMessage($"[{i}] {arr[i]}");
    }

    private void CheckIndex(int index)
    {
        if (index < 0 || index >= arr.Length)
            throw new ArgumentException("Индекс за пределами массива.");
    }
}
