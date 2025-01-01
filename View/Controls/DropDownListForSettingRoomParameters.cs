using AreaCalculationPlugin.Calculator;

namespace AreaCalculationPlugin.View.Controls;

public class DropDownListForSettingRoomParameters : DropdownList
{
    public RoomParameter RoomParameter { get; init; }
    public event Action<object?, EventArgs> SelectedIndexChanged;
    public DropDownListForSettingRoomParameters(RoomParameter roomParameter) 
        : base(RoomParameterConverter.Convert(roomParameter))
    {
        RoomParameter = roomParameter;
        List.SelectedIndexChanged += UpdateParameter;
        Margin = new Padding(left: 0, top: 0, right: 0, bottom: 4);
    }

    private void UpdateParameter(object? sender, EventArgs args)
    {
        SelectedIndexChanged?.Invoke(this, args);
        PluginManager.ParameterCorrector.ProjectionOfRoomParameterNames[RoomParameter] = List.SelectedItem.ToString();
    }
}
