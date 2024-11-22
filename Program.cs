using AreaCalculationPlugin.Model;
using AreaCalculationPlugin.View;
using System.Globalization;
using System.Resources;
using System.Text;

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
        //Application.Run(new SettingÑoefficient());
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