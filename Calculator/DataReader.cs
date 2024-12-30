using BIMStructureMgd.ObjectProperties;
using Teigha.DatabaseServices;

namespace AreaCalculationPlugin.Calculator;

internal class DataReader
{
    public static List<RoomData> GetRoomData()
    {
        var result = new List<RoomData>();
        var drawingDatabase = HostMgd.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;

        using var trans = drawingDatabase.TransactionManager.StartTransaction();
        var blockTable = (BlockTable)trans.GetObject(drawingDatabase.BlockTableId, OpenMode.ForRead);
        var tableRecords = (BlockTableRecord)trans.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForRead);

        foreach (var id in tableRecords)
        {
            if (id.GetObject(OpenMode.ForRead) is not IParametricObject room) continue;
            var elementData = room.GetElementData();
            if (elementData.Parameters.Any(parameter => parameter.Name == "AEC_ROOM_AREA"))
                result.Add(new RoomData(elementData));
        }

        return result;
    }
}
