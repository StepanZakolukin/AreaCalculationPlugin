using BIMStructureMgd.ObjectProperties;
using System.Globalization;

namespace AreaCalculationPlugin.Calculator;

public class RoomData
{
    public RoomCategory Type
    {
        get
        {
            if (RoomTypeParameter is not null && int.TryParse(Parameters[RoomTypeParameter].Value, out var digit))
                return (RoomCategory)(digit - 1);
            return RoomCategory.Invalid;
        }
    }
    
    public static string? RoomTypeParameter { get; set; }
    public double Area { get; private set; }

    public static HashSet<string> SharedParameters { get; private set; } = new HashSet<string>();

    private Dictionary<string, Parameter> Parameters { get; set; } = new Dictionary<string, Parameter>();

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

    public string GetParameterValue(string? parameterName)
    {
        if (parameterName is null) throw new ArgumentNullException(nameof(parameterName));
        return Parameters[parameterName].Value;
    }
}
