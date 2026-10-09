using System;
using System.Globalization;
using System.Text;

class Program
{
    static void Main()
    {
     
        Console.OutputEncoding = Encoding.UTF8;

        Console.Write("Введіть обсяг води (м³): ");
        string input = (Console.ReadLine() ?? "").Replace(',', '.');

        //  перевірка коректності введення (TryParse + додатне число)
        if (!double.TryParse(input, CultureInfo.InvariantCulture, out double v) || v < 0)
        {
            Console.WriteLine("Помилка: обсяг має бути додатним числом.");
            return;
        }

        //  додаткова величина для охоронної умови when
        Console.Write("Чи є у вас пільга (так/ні): ");
       
        bool hasPrivilege = (Console.ReadLine() ?? "").Trim().ToLower() == "так";

        //  визначення тарифної групи та ставки за Варіантом 3
        double rate;
        string group;

        if (v <= 5.0)
        {
            rate = 12.0;
            group = "До 5 м³ (12 грн/м³)";
        }
        else if (v <= 15.0)
        {
            rate = 15.0;
            group = "Від 5 до 15 м³ (15 грн/м³)";
        }
        else
        {
            rate = 19.0;
            group = "Понад 15 м³ (19 грн/м³)";
        }

        double sum = v * rate;

        //  вираз switch із реляційними зразками, and, when та _
        string category = sum switch
        {
            < 60.0 => "Малий рахунок",
            >= 60.0 and <= 225.0 when hasPrivilege => "Середній рахунок (пільговий)",
            >= 60.0 and <= 225.0 => "Середній рахунок",
            _ => "Великий рахунок"
        };

        // Форматоване виведення результатів
        Console.WriteLine("\n--- Підсумок ---");
        Console.WriteLine($"Тарифна група:  {group}");
        Console.WriteLine($"Сума до сплати: {sum:F2} грн");
        Console.WriteLine($"Категорія:      {category}");
    }
}
