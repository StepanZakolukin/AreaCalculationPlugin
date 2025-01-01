using AreaCalculationPlugin.View;
using System.Collections.Immutable;
using Teigha.Runtime;

namespace AreaCalculationPlugin.Calculator;

public class PluginManager
{
    public static ImmutableArray<RoomData> Rooms { get; private set; }
    public static RoomParameterCorrector ParameterCorrector { get; private set; }

    [CommandMethod("AreaCalc")]
    public static void Start()
    {
        RoomData.ResetParameters();
        Rooms = DataReader.GetRoomData().ToImmutableArray(); ;
        var defaultAreaCoefficients = new Dictionary<RoomCategory, double>()
        {
            { RoomCategory.ResidentialRoom, 1 },
            { RoomCategory.NonResidentialRoom, 1 },
            { RoomCategory.Loggia, 0.5 },
            { RoomCategory.Balcony, 0.3 },
            { RoomCategory.CommonAreas, 1},
            { RoomCategory.Office, 1 },
            { RoomCategory.WarmLoggia, 1 }
        };
        var projectionOfRoomParameterNames = Enumerable.Range(0, 9)
            .ToDictionary(num => (RoomParameter)num, num => string.Empty);
        ParameterCorrector = new RoomParameterCorrector(2, defaultAreaCoefficients, projectionOfRoomParameterNames);

        var mainForm = new AreaOfPremises();
        mainForm.ChoseRooms += ParameterCorrector.PerformCalculations;

        HostMgd.ApplicationServices.Application.ShowModalDialog(mainForm);
    }
}
