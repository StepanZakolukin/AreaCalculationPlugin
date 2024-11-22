namespace AreaCalculationPlugin.View.Controls;

public class Heading : Label
{
    public Heading(int fontSize = 14, FontStyle fontStyle = FontStyle.Regular)
    {
        InitializeComponent(fontSize, fontStyle);
    }

    public Heading(string text, int fontSize = 14, FontStyle fontStyle = FontStyle.Regular)
    {
        Text = text;
        InitializeComponent(fontSize, fontStyle);
    }

    private void InitializeComponent(int fontSize, FontStyle fontStyle)
    {
        Font = new Font("Inter", fontSize, fontStyle, GraphicsUnit.Pixel);
        Dock = DockStyle.Fill;
        Margin = new Padding(0);
        Padding = new Padding(0);
        ForeColor = ColorTranslator.FromHtml("#515254");
        TextAlign = ContentAlignment.MiddleLeft;
    }
}
