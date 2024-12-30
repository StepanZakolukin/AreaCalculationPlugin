namespace AreaCalculationPlugin.Calculator;

public class RoomCategoryConverter
{
    private static readonly Dictionary<string, RoomCategory> FromStringToRoomCategory = new()
    {
        { "Жилые помещения квартиры", RoomCategory.ResidentialRoom},
        { "Нежилые помещения квартиры", RoomCategory.NonResidentialRoom},
        { "Лоджии:", RoomCategory.Loggia },
        { "Балконы, Террасы", RoomCategory.Balcony },
        { "Нежилые помещения, Общественные (МОП)", RoomCategory.CommonAreas },
        { "Офисы", RoomCategory.Office },
        { "Теплые лоджии", RoomCategory.WarmLoggia },
        { "Данные не найдены", RoomCategory.Invalid }
    };

    private static readonly Dictionary<RoomCategory, string> FromRoomCategoryToString = new()
    {
        { RoomCategory.ResidentialRoom, "Жилые помещения квартиры" },
        { RoomCategory.NonResidentialRoom, "Нежилые помещения квартиры" },
        { RoomCategory.Loggia, "Лоджии" },
        { RoomCategory.Balcony, "Балконы, Террасы" },
        { RoomCategory.CommonAreas, "Нежилые помещения, Общественные (МОП)"},
        { RoomCategory.Office, "Офисы" },
        { RoomCategory.WarmLoggia, "Теплые лоджии" },
        { RoomCategory.Invalid, "Данные не найдены" }
    };

    public static string Convert(RoomCategory roomCategory) => FromRoomCategoryToString[roomCategory];
    public static RoomCategory Convert(string nameRoomCategory) => FromStringToRoomCategory[nameRoomCategory];
}
