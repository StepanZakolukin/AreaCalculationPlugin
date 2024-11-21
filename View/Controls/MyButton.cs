namespace AreaCalculationPlugin.View.Controls;

public class MyButton : Button
{
    public MyButton()
    {
        Margin = new Padding(0);
        InitializeComponent();
    }

    public MyButton(Padding margin)
    {
        Margin = margin;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        ForeColor = ColorTranslator.FromHtml("#515254");
        Font = new Font("Inter", 11, FontStyle.Bold, GraphicsUnit.Pixel);
        Padding = new Padding(0);
        Dock = DockStyle.Fill;
    }
}
