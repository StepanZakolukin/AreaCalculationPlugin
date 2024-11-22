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
        //Application.Run(new Setting�oefficient());
    }

    private static CoefficientsInfo[] GetRoomCoefficients()
    {
        var coefficients = new Dictionary<string, double>
        {
            ["����� ��������� ��������"] = 1,
            ["������� ��������� ��������"] = 1,
            ["������"] = 0.5,
            ["�������, �������"] = 0.3,
            ["������� ���������, ������������ (���)"] = 1,
            ["�����"] = 1,
            ["������ ������"] = 1
        };

        return coefficients
            .Select(pair => new CoefficientsInfo(pair.Key, pair.Value))
            .ToArray();
    }
}