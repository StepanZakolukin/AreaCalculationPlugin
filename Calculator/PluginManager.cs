using AreaCalculationPlugin.View;
using System.Resources;
using Teigha.Runtime;

namespace AreaCalculationPlugin.Calculator;

public class PluginManager
{
    public static RoomParameterCorrector ParameterCorrector;

    static PluginManager()
    {
        var rooms = DataReader.GetRoomData();
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
        ParameterCorrector = new RoomParameterCorrector(rooms, 2, defaultAreaCoefficients, projectionOfRoomParameterNames);
    }


    [CommandMethod("AreaCalc")]
    public static void Start()
    {
        var mainForm = new AreaOfPremises();
        RoomData.ResetParameters();
        /*mainForm.ChoseRooms += parameterCorrector.PerformCalculations;*/

        HostMgd.ApplicationServices.Application.ShowModalDialog(mainForm);
    }
}
