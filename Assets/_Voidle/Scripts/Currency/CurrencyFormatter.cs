using UnityEngine;

public static class CurrencyFormatter
{
    private static readonly string[] _suffixes = { "", "Ъ", "Ь", "С", "в", "Ът", "Ъэ", "бѕ", "бя", "Ю", "Э" };

    public static string Format(long amount)
    {
        if (amount == 0) return "0";

        int suffixIndex = 0;
        double formattedAmount = amount;

        while (formattedAmount >= 1000 && suffixIndex < _suffixes.Length - 1)
        {
            formattedAmount /= 1000;
            suffixIndex++;
        }

        return formattedAmount.ToString("0.##") + _suffixes[suffixIndex];
    }
}
