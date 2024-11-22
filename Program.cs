using AreaCalculationPlugin.Model;
using AreaCalculationPlugin.View;

namespace AreaCalculationPlugin;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        var defaultAreaCoefficients = GetRoomCoefficients();
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.

        ApplicationConfiguration.Initialize();
        Application.Run(new AreaOfThePremises(defaultAreaCoefficients));
        //Application.Run(new SettingСoefficient());
    }

    private static CoefficientsInfo[] GetRoomCoefficients()
    {
        var coefficients = new Dictionary<string, double>
        {
            ["Жилые помещения квартиры"] = 1,
            ["Нежилые помещения квартиры"] = 1,
            ["Лоджии"] = 0.5,
            ["Балконы, Террасы"] = 0.3,
            ["Нежилые помещения, Общественные (МОП)"] = 1,
            ["Офисы"] = 1,
            ["Теплые лоджии"] = 1
        };

        return coefficients
            .Select(pair => new CoefficientsInfo(pair.Key, pair.Value))
            .ToArray();
    }
}