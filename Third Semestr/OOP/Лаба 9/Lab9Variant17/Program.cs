using System;

internal class Program
{
    private static void Main()
    {
        int choice;

        UserInterface.PrintMessage("Лаба 9. Вариант 17 (в задании вариант 2, класс Money)");

        do
        {
            UserInterface.PrintMenu("Главное меню",
                "1. Часть 1. Класс Money",
                "2. Часть 2. Перегруженные операции",
                "3. Часть 3. Коллекция MoneyArray",
                "0. Выход");
            choice = UserInterface.ReadInt("Выберите пункт: ", 0, 3);

            switch (choice)
            {
                case 1:
                    DoPart1();
                    break;
                case 2:
                    DoPart2();
                    break;
                case 3:
                    DoPart3();
                    break;
            }
        }
        while (choice != 0);
    }

    private static void DoPart1()
    {
        UserInterface.PrintMessage("\nЧасть 1");

        Money emptyMoney = new Money();
        UserInterface.PrintMessage("Конструктор без параметров: " + emptyMoney);

        Money fixedMoney = new Money(5, 50);
        UserInterface.PrintMessage("Конструктор с параметрами (5, 50): " + fixedMoney);

        Money money = UserInterface.ReadMoney("Введите сумму:");
        UserInterface.PrintMessage($"Рубли: {money.Rubles}, копейки: {money.Kopeks}");

        int kopeksToAdd = UserInterface.ReadInt("Сколько копеек добавить: ", 0, 1000000);
        Money staticResult = Money.AddKopeks(money, kopeksToAdd);
        Money methodResult = money.AddKopeks(kopeksToAdd);
        UserInterface.PrintMessage("Статическая функция: " + staticResult);
        UserInterface.PrintMessage("Метод класса: " + methodResult);

        UserInterface.PrintMessage("Создано объектов Money: " + Money.Count);
    }

    private static void DoPart2()
    {
        Money money = new Money();
        int choice;

        do
        {
            UserInterface.PrintMenu($"Часть 2. Текущая сумма: {money} Создано объектов Money: {Money.Count}",
                "1. Ввести сумму",
                "2. ++ (добавить копейку)",
                "3. -- (вычесть копейку)",
                "4. Явное приведение к int (рубли)",
                "5. Неявное приведение к double (копейки)",
                "6. Сумма + число",
                "7. Число + сумма",
                "8. Сумма - число",
                "9. Число - сумма",
                "0. Назад");
            choice = UserInterface.ReadInt("Выберите пункт: ", 0, 9);

            switch (choice)
            {
                case 1:
                    money = UserInterface.ReadMoney("Введите сумму:");
                    break;
                case 2:
                    money++;
                    UserInterface.PrintMessage("Результат: " + money);
                    break;
                case 3:
                    money--;
                    UserInterface.PrintMessage("Результат: " + money);
                    break;
                case 4:
                    int rubles = (int)money;
                    UserInterface.PrintMessage("(int) = " + rubles);
                    break;
                case 5:
                    double kopeksPart = money;
                    UserInterface.PrintMessage("double = " + kopeksPart);
                    break;
                case 6:
                    int addRight = ReadKopeks();
                    UserInterface.PrintMessage($"{money} + {addRight} = {money + addRight}");
                    break;
                case 7:
                    int addLeft = ReadKopeks();
                    UserInterface.PrintMessage($"{addLeft} + {money} = {addLeft + money}");
                    break;
                case 8:
                    int subtractRight = ReadKopeks();
                    UserInterface.PrintMessage($"{money} - {subtractRight} = {money - subtractRight}");
                    break;
                case 9:
                    int subtractLeft = ReadKopeks();
                    UserInterface.PrintMessage($"{subtractLeft} - {money} = {subtractLeft - money}");
                    break;
            }
        }
        while (choice != 0);
    }

    private static void DoPart3()
    {
        MoneyArray array = new MoneyArray();
        Random random = new Random();
        int choice;

        do
        {
            UserInterface.PrintMenu($"Часть 3. Создано объектов MoneyArray: {MoneyArray.Count}, Money: {Money.Count}",
                "1. Создать пустой массив",
                "2. Создать массив со случайными значениями",
                "3. Создать массив с клавиатуры",
                "4. Вывести массив",
                "5. Получить элемент по индексу",
                "6. Изменить элемент по индексу",
                "7. Найти минимальное значение",
                "0. Назад");
            choice = UserInterface.ReadInt("Выберите пункт: ", 0, 7);

            switch (choice)
            {
                case 1:
                    array = new MoneyArray();
                    array.Show();
                    break;
                case 2:
                    array = new MoneyArray(ReadLength(), random);
                    array.Show();
                    break;
                case 3:
                    array = new MoneyArray(ReadLength());
                    array.Show();
                    break;
                case 4:
                    array.Show();
                    break;
                case 5:
                    ShowElement(array);
                    break;
                case 6:
                    ChangeElement(array);
                    break;
                case 7:
                    if (array.Length == 0)
                    {
                        UserInterface.PrintMessage("Ошибка: массив пустой, искать нечего.");
                        break;
                    }
                    UserInterface.PrintMessage("Минимальное значение: " + FindMin(array));
                    break;
            }
        }
        while (choice != 0);
    }

    // Часть 3. Задание варианта: минимальное значение в массиве
    private static Money FindMin(MoneyArray array)
    {
        Money min = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] < min)
                min = array[i];
        }
        return min;
    }

    private static void ShowElement(MoneyArray array)
    {
        int index = UserInterface.ReadInt("Индекс: ", int.MinValue, int.MaxValue);
        try
        {
            UserInterface.PrintMessage($"[{index}] {array[index]}");
        }
        catch (ArgumentException exception)
        {
            UserInterface.PrintMessage("Ошибка: " + exception.Message);
        }
    }

    private static void ChangeElement(MoneyArray array)
    {
        int index = UserInterface.ReadInt("Индекс: ", int.MinValue, int.MaxValue);
        try
        {
            Money oldValue = array[index];
            array[index] = UserInterface.ReadMoney("Новое значение:");
            UserInterface.PrintMessage($"[{index}] {oldValue} -> {array[index]}");
        }
        catch (ArgumentException exception)
        {
            UserInterface.PrintMessage("Ошибка: " + exception.Message);
        }
    }

    private static int ReadKopeks()
    {
        return UserInterface.ReadInt("Число (копейки): ", 0, 1000000);
    }

    private static int ReadLength()
    {
        return UserInterface.ReadInt("Количество элементов: ", 1, 100);
    }
}
