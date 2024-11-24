using AreaCalculationPlugin.View.Extensions;


namespace AreaCalculationPlugin.View.Controls;

public class RectangularRoundedButton : Panel
{
    public Color BackgroundColor { get; set; }
    public Color BorderColor { get; set; }
    public int BorderSize { get; set; }
    public int Radius { get; set; }

    public RectangularRoundedButton(Color backgroundColor)
    {
        InitializeComponent();
        BackgroundColor = backgroundColor;
    }

    public RectangularRoundedButton()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Radius = 10;
        BorderSize = 1;
        BackColor = Color.Transparent;
        ForeColor = ColorTranslator.FromHtml("#515254");
        BorderColor = ColorTranslator.FromHtml("#515254");
        BackgroundColor = ColorTranslator.FromHtml("#EEEEEE");

        Dock = DockStyle.Fill;
        Padding = new Padding(0);
        Margin = new Padding(0);
        Font = new Font(AreaOfThePremises.DefaultFont.FontFamily, 11, FontStyle.Bold, GraphicsUnit.Pixel);

        Paint += Background_Paint;
    }

    private void Background_Paint(object? sender, PaintEventArgs e)
    {
        var graphics = e.Graphics;

        graphics.FillRoundedRectangle(
            BackgroundColor,
            BorderColor,
            BorderSize,
            new Rectangle(Point.Empty, Size),
            Radius);

        PrintText(graphics);

        graphics.Dispose();
    }

    private void PrintText(Graphics graphics)
    {
        graphics.DrawString(Text, Font,
            new SolidBrush(ForeColor),
            new Rectangle(Point.Empty, Size),
            new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
    }
}
