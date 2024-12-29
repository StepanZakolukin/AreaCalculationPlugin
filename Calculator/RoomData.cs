using BIMStructureMgd.ObjectProperties;
using System.Globalization;

namespace AreaCalculationPlugin.Calculator;

public class RoomData
{
    public RoomType Type
    {
        get
        {
            if (RoomTypeParameter is not null && int.TryParse(Parameters[RoomTypeParameter].Value, out var digit))
                return (RoomType)(digit - 1);
            return RoomType.Invalid;
        }
    }
    public string ApartmentNumber
    {
        get
        {
            if (ApartmentNumberParameter is null) return string.Empty;
            return Parameters[ApartmentNumberParameter].Value;
        }
    }
    public static string? RoomTypeParameter { get; set; }
    public static string? ApartmentNumberParameter { get; set; }
    public double Area { get; private set; }

    public static HashSet<string> SharedParameters { get; private set; } = new HashSet<string>();

    public Dictionary<string, Parameter> Parameters { get; private set; } = new Dictionary<string, Parameter>();

    /*public double AreaWithCoefficient => Area * MainForm.Multiplicators[Type];*/


    public RoomData(ElementData data)
    {
        var currentParameters = new HashSet<string>();

        foreach (var parametr in data.Parameters)
        {
            currentParameters.Add(parametr.Name);
            Parameters[parametr.Name] = parametr;
        }

        Area = double.Parse(Parameters["AEC_ROOM_AREA"].Value, CultureInfo.InvariantCulture);

        if (SharedParameters.Count == 0)
            SharedParameters = currentParameters;
        SharedParameters.IntersectWith(currentParameters);
    }

    public static void ResetParameters()
    {
        SharedParameters = new HashSet<string>();
    }

    public void ChangeParameter(string? parameterName, string parameterValue)
    {
        if (parameterName is null) return;
        Parameters[parameterName].Value = parameterValue;
    }
}
