using System.Collections.Immutable;
using System.Globalization;

namespace AreaCalculationPlugin.Calculator;

public class RoomParameterCorrector
{
    private int numberOfDecimalPlaces;
    public int NumberOfDecimalPlaces 
    {
        get => numberOfDecimalPlaces;
        set
        {
            if (value > 0) numberOfDecimalPlaces = value;
        }
    }
    public ImmutableArray<RoomData> Rooms { get; private set; }
    public Dictionary<RoomCategory, double> AreaCoefficients { get; private set; }
    public Dictionary<RoomParameter, string> ProjectionOfRoomParameterNames { get; private set; }

    public RoomParameterCorrector(
        IEnumerable<RoomData> rooms,
        int numberOfDecimalPlaces,
        Dictionary<RoomCategory, double> areaCoefficients,
        Dictionary<RoomParameter, string> projectionOfRoomParameterNames)
    {
        Rooms = rooms.ToImmutableArray();
        NumberOfDecimalPlaces = numberOfDecimalPlaces;
        AreaCoefficients = areaCoefficients;
        ProjectionOfRoomParameterNames = projectionOfRoomParameterNames;
    }

    public void PerformCalculations(IEnumerable<RoomData> rooms)
    {
        var editor = HostMgd.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        ResetParametersThatNeedToBeCalculated(rooms);

        foreach (var room in rooms)
        {
            var apartments = rooms.Where(r => r.ApartmentNumber == room.ApartmentNumber);

            var parameterValue = AreaCoefficients[room.Type].ToString(CultureInfo.InvariantCulture);
            room.ChangeParameter(ProjectionOfRoomParameterNames[RoomParameter.AreaCoefficient], parameterValue);

            parameterValue = (room.Area * AreaCoefficients[room.Type]).ToString($"N{NumberOfDecimalPlaces}", CultureInfo.InvariantCulture);
            room.ChangeParameter(ProjectionOfRoomParameterNames[RoomParameter.AreaWithCoefficient], parameterValue);

            if (ProjectionOfRoomParameterNames[RoomParameter.NumberOfRooms] != null && room.Type == RoomCategory.ResidentialRoom)
            {
                foreach (var e in apartments)
                {
                    var param = e.Parameters[ProjectionOfRoomParameterNames[RoomParameter.NumberOfRooms]];
                    param.Value = (int.Parse(param.Value) + 1).ToString(CultureInfo.InvariantCulture);
                }
            }

            if (room.Type == RoomCategory.ResidentialRoom || room.Type == RoomCategory.NonResidentialRoom)
                CalculateArea(ProjectionOfRoomParameterNames[RoomParameter.AreaOfApartment], apartments, room.Area * AreaCoefficients[room.Type]);
            if (room.Type == RoomCategory.ResidentialRoom)
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

    private void ResetParametersThatNeedToBeCalculated(IEnumerable<RoomData> rooms)
    {
        foreach (var room in rooms)
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
