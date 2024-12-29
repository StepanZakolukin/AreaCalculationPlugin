using System.Globalization;

namespace AreaCalculationPlugin.Calculator;

internal class RoomParameterCorrector
{
    private readonly RoomData[] Rooms;
    private readonly int NumberOfDecimalPlaces;
    private readonly Dictionary<RoomType, double> AreaCoefficients;
    private readonly Dictionary<RoomParameter, string> ProjectionOfRoomParameterNames;

    public RoomParameterCorrector(
        RoomData[] rooms,
        int numberOfDecimalPlaces,
        Dictionary<RoomType, double> areaCoefficients,
        Dictionary<RoomParameter, string> projectionOfRoomParameterNames)
    {
        Rooms = rooms;
        NumberOfDecimalPlaces = numberOfDecimalPlaces;
        AreaCoefficients = areaCoefficients;
        ProjectionOfRoomParameterNames = projectionOfRoomParameterNames;
    }

    public void PerformCalculations()
    {
        var editor = HostMgd.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        ResetParametersThatNeedToBeCalculated();

        foreach (var room in Rooms)
        {
            var apartments = Rooms.Where(r => r.ApartmentNumber == room.ApartmentNumber);

            var parameterValue = AreaCoefficients[room.Type].ToString(CultureInfo.InvariantCulture);
            room.ChangeParameter(ProjectionOfRoomParameterNames[RoomParameter.AreaCoefficient], parameterValue);

            parameterValue = (room.Area * AreaCoefficients[room.Type]).ToString($"N{NumberOfDecimalPlaces}", CultureInfo.InvariantCulture);
            room.ChangeParameter(ProjectionOfRoomParameterNames[RoomParameter.AreaWithCoefficient], parameterValue);

            if (ProjectionOfRoomParameterNames[RoomParameter.NumberOfRooms] != null && room.Type == RoomType.ResidentialRoom)
            {
                foreach (var e in apartments)
                {
                    var param = e.Parameters[ProjectionOfRoomParameterNames[RoomParameter.NumberOfRooms]];
                    param.Value = (int.Parse(param.Value) + 1).ToString(CultureInfo.InvariantCulture);
                }
            }

            if (room.Type == RoomType.ResidentialRoom || room.Type == RoomType.NonResidentialRoom)
                CalculateArea(ProjectionOfRoomParameterNames[RoomParameter.AreaOfApartment], apartments, room.Area * AreaCoefficients[room.Type]);
            if (room.Type == RoomType.ResidentialRoom)
                CalculateArea(ProjectionOfRoomParameterNames[RoomParameter.AreaOfApartmentIsResidential], apartments, room.Area * AreaCoefficients[room.Type]);
            CalculateArea(ProjectionOfRoomParameterNames[RoomParameter.AreaOfApartmentWithLoggiaAndBalcony], apartments, room.Area);
            CalculateArea(ProjectionOfRoomParameterNames[RoomParameter.TotalAreaOfApartment], apartments, room.Area * AreaCoefficients[room.Type]);
        }

        editor.WriteMessage("Рассчеты выполнены");
    }

    private void CalculateArea(string? parameterName, IEnumerable<RoomData> rooms, double areaOfCrrentRoom)
    {
        if (parameterName == null) return;

        foreach (var room in rooms)
        {
            var param = room.Parameters[parameterName];
            param.Value = (double.Parse(param.Value ?? "0.0", CultureInfo.InvariantCulture) + areaOfCrrentRoom)
                .ToString($"N{NumberOfDecimalPlaces}", CultureInfo.InvariantCulture);
        }
    }

    private void ResetParametersThatNeedToBeCalculated()
    {
        foreach (var room in Rooms)
        {
            room.ChangeParameter(ProjectionOfRoomParameterNames[RoomParameter.NumberOfRooms], "0");
            room.ChangeParameter(ProjectionOfRoomParameterNames[RoomParameter.AreaCoefficient], "0.0");
            room.ChangeParameter(ProjectionOfRoomParameterNames[RoomParameter.AreaOfApartment], "0.0");
            room.ChangeParameter(ProjectionOfRoomParameterNames[RoomParameter.AreaWithCoefficient], "0.0");
            room.ChangeParameter(ProjectionOfRoomParameterNames[RoomParameter.TotalAreaOfApartment], "0.0");
            room.ChangeParameter(ProjectionOfRoomParameterNames[RoomParameter.AreaOfApartmentIsResidential], "0.0");
            room.ChangeParameter(ProjectionOfRoomParameterNames[RoomParameter.AreaOfApartmentWithLoggiaAndBalcony], "0.0");
        }
    }
}
