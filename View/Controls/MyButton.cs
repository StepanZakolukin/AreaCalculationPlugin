namespace AreaCalculationPlugin.View.Controls;

public class MyButton : Button
{
    public MyButton(Padding margin)
    {
        ForeColor = ColorTranslator.FromHtml("#515254");
        Font = new Font("Inter", 11, FontStyle.Bold, GraphicsUnit.Pixel);
        Padding = new Padding(0);
        Margin = margin;
        Dock = DockStyle.Fill;
    }
}
