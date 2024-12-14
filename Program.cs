using AreaCalculationPlugin.Model;
using AreaCalculationPlugin.View;
using System.Resources;

namespace AreaCalculationPlugin;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Start()
    {
        var defaultAreaCoefficients = GetRoomCoefficients();
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.

        var mainForm = new AreaOfThePremises(defaultAreaCoefficients);
        mainForm.Show();
    }

    private static CoefficientsInfo[] GetRoomCoefficients()
    {
        var rm = new ResourceManager(typeof(AreaOfThePremises));

        return rm.GetString("Coefficients").Split("\r\n")
            .Select(line => line.Split(": "))
            .Select(array => new CoefficientsInfo(array.First(), double.Parse(array.Last())))
            .ToArray();
    }
}