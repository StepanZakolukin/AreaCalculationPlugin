using AreaCalculationPlugin.Calculator;

namespace AreaCalculationPlugin.View.Controls;

public class DropDownListForSettingRoomParameters : DropdownList
{
    private readonly RoomParameter RoomParameter;
    public DropDownListForSettingRoomParameters(RoomParameter roomParameter) 
        : base(RoomParameterConverter.Convert(roomParameter))
    {
        RoomParameter = roomParameter;
        List.SelectedIndexChanged += UpdateParameter;
    }

    private void UpdateParameter(object? sender, EventArgs args)
    {
        PluginManager.ParameterCorrector.ProjectionOfRoomParameterNames[RoomParameter] = List.SelectedItem.ToString();
    }
}
