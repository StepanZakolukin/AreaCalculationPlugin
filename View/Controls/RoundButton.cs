namespace AreaCalculationPlugin.View.Controls;

public class RoundButton : Button
{
    public RoundButton()
    {
        Margin = new Padding(0);
        InitializeComponent();
    }

    public RoundButton(Color backColor)
    {
        Margin = new Padding(0);
        InitializeComponent();
        BackColor = backColor;
    }

    public RoundButton(Padding margin)
    {
        Margin = margin;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        ForeColor = ColorTranslator.FromHtml("#515254");
        BackColor = ColorTranslator.FromHtml("#EEEEEE");

        Font = new Font(AreaOfThePremises.DefaultFont.FontFamily, 11, FontStyle.Bold, GraphicsUnit.Pixel);
        Padding = new Padding(0);
        Dock = DockStyle.Fill;
        FlatAppearance.BorderSize = 1;
    }
}
