namespace AreaCalculationPlugin.View.Controls;

public class Container : TableLayoutPanel
{
    public Container()
    {
        InitializeComponent();
    }

    public Container(Color backColor)
    {
        BackColor = backColor;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Dock = DockStyle.Fill;
        Margin = new Padding(0);
        Padding = new Padding(0);
        BackColor = Color.Transparent;
    }
}
