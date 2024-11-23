namespace AreaCalculationPlugin.View.Controls;

public class DropdownListForGrouping : DropdownList
{
    public DropdownListForGrouping(string name) : base(name)
    {
        title.Margin = new Padding(0, 2, 0, 0);
        Margin = new Padding(left: 0, top: 0, right: 0, bottom: 8);
    }
}
