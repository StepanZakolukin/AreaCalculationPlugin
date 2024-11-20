namespace AreaCalculationPlugin.View.Controls;

public class Heading : Label
{
    public Heading()
    {
        Font = new Font("Inter", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        Dock = DockStyle.Fill;
        Margin = new Padding(0);
        Padding = new Padding(0);
        ForeColor = ColorTranslator.FromHtml("#515254");
    }
}
