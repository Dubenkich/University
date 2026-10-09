using System;

internal class Program
{
    private static void Main()
    {
        double a = 0.1;
        double b = 1;
        int n = 10;
        int k = 9;
        double epsilon = 0.0001;
        double step = (b - a) / k;

        PrintHeader(n, epsilon);

        for (int i = 0; i <= k; i++)
        {
            double x = a + i * step;
            double sn = CalculateSumN(x, n);
            double se = CalculateSumE(x, epsilon);
            double y = CalculateFunction(x);
            PrintResult(x, sn, se, y);
        }
    }

    private static double CalculateSumN(double x, int n)
    {
        double sum = 1;
        double term = 1;

        for (int i = 1; i <= n; i++)
        {
            term = CalculateNextTerm(term, x, i);
            sum += term;
        }

        return sum;
    }

    private static double CalculateSumE(double x, double epsilon)
    {
        double sum = 1;
        double term = 1;
        int i = 1;

        do
        {
            term = CalculateNextTerm(term, x, i);
            sum += term;
            i++;
        }
        while (Math.Abs(term) >= epsilon);

        return sum;
    }

    private static double CalculateNextTerm(double term, double x, int index)
    {
        return term * x * x / ((2 * index - 1) * (2 * index));
    }

    private static double CalculateFunction(double x)
    {
        return (Math.Exp(x) + Math.Exp(-x)) / 2;
    }

    private static void PrintHeader(int n, double epsilon)
    {
        Console.WriteLine("Вариант 17");
        Console.WriteLine("Вычисление функции y = (e^x + e^(-x)) / 2");
        Console.WriteLine($"n = {n}, точность = {epsilon}");
        Console.WriteLine();
    }

    private static void PrintResult(double x, double sn, double se, double y)
    {
        Console.WriteLine($"X={x:F1} SN={sn:F12} SE={se:F12} Y={y:F12}");
    }
}
