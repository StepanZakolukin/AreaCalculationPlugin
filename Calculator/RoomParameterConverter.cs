namespace AreaCalculationPlugin.Calculator;

public static class RoomParameterConverter
{
    private static readonly Dictionary<string, RoomParameter> FromStringToRoomParameter = new()
    {
        {"Параметр номера квартиры", RoomParameter.ApartmentNumber },
        {"Параметр типа помещения", RoomParameter.RoomType },
        {"Коэффициент площади", RoomParameter.AreaCoefficient },
        {"Площадь с коэффициентом", RoomParameter.AreaWithCoefficient },
        {"Площадь квартиры жилая", RoomParameter.AreaOfApartmentIsResidential },
        {"Площадь квартиры с лоджией и балконом\r\nбез коэф.", RoomParameter.AreaOfApartmentWithLoggiaAndBalcony },
        {"Площадь квартиры", RoomParameter.AreaOfApartment},
        {"Площадь квартиры общая", RoomParameter.TotalAreaOfApartment},
        { "Число комнат", RoomParameter.NumberOfRooms }
    };

    private static readonly Dictionary<RoomParameter, string> FromRoomParameterToString = new()
    {
        { RoomParameter.ApartmentNumber, "Параметр номера квартиры" },
        { RoomParameter.RoomType, "Параметр типа помещения" },
        { RoomParameter.AreaCoefficient, "Коэффициент площади" },
        { RoomParameter.AreaWithCoefficient, "Площадь с коэффициентом" },
        { RoomParameter.AreaOfApartmentIsResidential, "Площадь квартиры жилая" },
        { RoomParameter.AreaOfApartmentWithLoggiaAndBalcony, "Площадь квартиры с лоджией и балконом\r\nбез коэф." },
        { RoomParameter.AreaOfApartment, "Площадь квартиры" },
        { RoomParameter.TotalAreaOfApartment, "Площадь квартиры общая" },
        { RoomParameter.NumberOfRooms, "Число комнат" }
    };

    public static string Convert(RoomParameter roomCategory) => FromRoomParameterToString[roomCategory];
    public static RoomParameter Convert(string nameRoomCategory) => FromStringToRoomParameter[nameRoomCategory];
}
