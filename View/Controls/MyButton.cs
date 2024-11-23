namespace AreaCalculationPlugin.View.Controls;

public class MyButton : Button
{
    public MyButton()
    {
        Margin = new Padding(0);
        InitializeComponent();
    }

    public MyButton(Color backColor)
    {
        Margin = new Padding(0);
        InitializeComponent();
        BackColor = backColor;
    }

    public MyButton(Padding margin)
    {
        Margin = margin;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        ForeColor = ColorTranslator.FromHtml("#515254");
        BackColor = ColorTranslator.FromHtml("#EEEEEE");

        Font = new Font(AreaOfThePremises.PluginFontCollection.Families.First(), 11, FontStyle.Bold, GraphicsUnit.Pixel);
        Padding = new Padding(0);
        Dock = DockStyle.Fill;
        FlatAppearance.BorderSize = 1;
    }
}
