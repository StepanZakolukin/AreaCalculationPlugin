namespace AreaCalculationPlugin.View.Controls;

internal class AreaCoefficient : Container
{
    public readonly Heading Name;
    public int FontSize { get; private set; }
    private const double Delta = 0.1;
    public double Coefficient { get; private set; }

    public readonly Panel DisplayForCoefficient;
    public readonly MyButton ButtonPlus;
    public readonly MyButton ButtonMinus;

    public AreaCoefficient(string name, Padding padding, double coefficient = 1, int fontSize = 16) : base(Color.White)
    {
        Padding = padding;
        FontSize = fontSize;
        Coefficient = coefficient;

        Name = new(name, fontSize: FontSize, FontStyle.Bold) { Margin = new Padding(7, 0, 1, 0) };
        DisplayForCoefficient = new Panel()
        {
            Margin = new Padding(left: 2, top: 0, right: 2, bottom: 2),
            Dock = DockStyle.Fill,
            Padding = new Padding(0)
        };
        DisplayForCoefficient.Paint += DrawBackground;

        ButtonPlus = new(margin: new Padding(2, 0, 2, 1));
        CreateAndCustomizeButtons(ButtonPlus, Image.FromFile("../../../Resources/ButtonPlus.png"));

        ButtonMinus = new(margin: ButtonPlus.Margin);
        CreateAndCustomizeButtons(ButtonMinus, Image.FromFile("../../../Resources/ButtonMinus.png"));

        InitializeComponent();
        SubscribeToEvents();
    }

    private void DrawBackground(object? sender, PaintEventArgs e)
    {
        var control = sender as Control;
        var graphics = e.Graphics;

        graphics.FillRoundedRectangle(
            ColorTranslator.FromHtml("#F5F6F8"),
            ColorTranslator.FromHtml("#EEEEEE"),
            borderSize: 1,
            new Rectangle(
                new Point(0, 0),
                new Size(control.Width, control.Height)),
            radius: 10);

        PrintText(graphics);

        graphics.Dispose();
    }

    private void PrintText(Graphics graphics)
    {
        graphics.DrawString(
            Coefficient.ToString().Replace(',', '.'),
            new Font("Inter", FontSize, FontStyle.Bold, GraphicsUnit.Pixel),
            new SolidBrush(ColorTranslator.FromHtml("#515254")),
            new Rectangle(Point.Empty, DisplayForCoefficient.Size),
            new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
    }

    private void CreateAndCustomizeButtons(Button button, Image backgroundImage)
    {
        button.BackgroundImage = backgroundImage;
        button.BackgroundImageLayout = ImageLayout.Zoom;
        button.BackColor = Color.Transparent;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseDownBackColor = BackColor;
        button.FlatAppearance.MouseOverBackColor = BackColor;
    }

    public AreaCoefficient(string name, double coefficient = 1) : base(Color.White)
    {
        if (coefficient > 1 || coefficient <= 1)
            throw new ArgumentException("Значение должно удовлетворять требованиям: 0 < coefficient <= 1");

        Name = new(name, fontSize: 16, FontStyle.Bold);
        DisplayForCoefficient = new()
        {
            Text = coefficient.ToString(),
            Font = new Font("Inter", 16, FontStyle.Bold, GraphicsUnit.Pixel),
        };
        ButtonPlus = new();
        ButtonMinus = new();

        InitializeComponent();
        SubscribeToEvents();
    }

    private void InitializeComponent()
    {
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
        ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var row = 0;
        var column = 0;
        Controls.Add(ButtonPlus, column++, row);
        Controls.Add(DisplayForCoefficient, column++, row);
        Controls.Add(ButtonMinus, column++, row);
        Controls.Add(Name, column++, row);

        Margin = new Padding(0, 0, 0, 0);

        BackColor = Color.Transparent;
    }

    private void SubscribeToEvents()
    {
        ButtonMinus.Click += DecreaseValue;
        ButtonPlus.Click += IncreaseValue;
    }

    private void IncreaseValue(object? sender, EventArgs e)
    {
        if (Coefficient + Delta <= 1) Coefficient = Math.Round(Coefficient + Delta, 1);
        Invalidate();
    }

    private void DecreaseValue(object? sender, EventArgs e)
    {
        if (Coefficient - Delta > 0.0000001) Coefficient = Math.Round(Coefficient - Delta, 1);
        Invalidate();
    }
}
