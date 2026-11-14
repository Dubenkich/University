using System;

public class Money
{
    private int rubles;
    private int kopeks;
    private static int count = 0;

    public int Rubles
    {
        get => rubles;
        set
        {
            if (value < 0)
                throw new ArgumentException("Рублей не может быть меньше 0.");
            rubles = value;
        }
    }

    public int Kopeks
    {
        get => kopeks;
        set
        {
            if (value < 0 || value > 99)
                throw new ArgumentException("Копеек должно быть от 0 до 99.");
            kopeks = value;
        }
    }

    public static int Count => count;

    public Money()
    {
        Rubles = 0;
        Kopeks = 0;
        count++;
    }

    public Money(int rubles, int kopeks)
    {
        Rubles = rubles;
        Kopeks = kopeks;
        count++;
    }

    // Часть 1. Статическая функция: добавление копеек
    public static Money AddKopeks(Money money, int kopeksToAdd)
    {
        return CreateFromKopeks(money.GetTotalKopeks() + kopeksToAdd);
    }

    // Часть 1. Метод класса: добавление копеек
    public Money AddKopeks(int kopeksToAdd)
    {
        return AddKopeks(this, kopeksToAdd);
    }

    public static Money SubtractKopeks(Money money, int kopeksToSubtract)
    {
        return CreateFromKopeks(money.GetTotalKopeks() - kopeksToSubtract);
    }

    public int GetTotalKopeks()
    {
        return rubles * 100 + kopeks;
    }

    public override string ToString()
    {
        return $"{rubles} руб. {kopeks} коп.";
    }

    // Часть 2. Унарные операции
    public static Money operator ++(Money money)
    {
        return AddKopeks(money, 1);
    }

    public static Money operator --(Money money)
    {
        return SubtractKopeks(money, 1);
    }

    // Часть 2. Операции приведения типа
    public static explicit operator int(Money money)
    {
        return money.rubles;
    }

    public static implicit operator double(Money money)
    {
        return money.kopeks / 100.0;
    }

    // Часть 2. Бинарные операции с целым числом (копейки)
    public static Money operator +(Money money, int kopeksToAdd)
    {
        return AddKopeks(money, kopeksToAdd);
    }

    public static Money operator +(int kopeksToAdd, Money money)
    {
        return AddKopeks(money, kopeksToAdd);
    }

    public static Money operator -(Money money, int kopeksToSubtract)
    {
        return SubtractKopeks(money, kopeksToSubtract);
    }

    public static Money operator -(int kopeksToSubtract, Money money)
    {
        return SubtractKopeks(money, kopeksToSubtract);
    }

    // Часть 3. Сравнение сумм нужно для поиска минимального значения
    public static bool operator <(Money left, Money right)
    {
        return left.GetTotalKopeks() < right.GetTotalKopeks();
    }

    public static bool operator >(Money left, Money right)
    {
        return left.GetTotalKopeks() > right.GetTotalKopeks();
    }

    // Сумма в копейках раскладывается на рубли и копейки.
    // Отрицательной суммы быть не может, поэтому она заменяется нулём.
    private static Money CreateFromKopeks(int totalKopeks)
    {
        if (totalKopeks < 0)
            totalKopeks = 0;
        return new Money(totalKopeks / 100, totalKopeks % 100);
    }
}
