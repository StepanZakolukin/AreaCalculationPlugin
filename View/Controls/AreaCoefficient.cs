using System.Globalization;

namespace AreaCalculationPlugin.View.Controls;

internal class AreaCoefficient : Container
{
    public readonly Heading Name;
    public int FontSize { get; private set; }
    public readonly TextBox Coefficient;
    public readonly MyButton ButtonPlus;
    public readonly MyButton ButtonMinus;
    private const double Delta = 0.1;
    public event Action<bool> CoefficientHasBeenChanged;

    public AreaCoefficient(string name, Padding padding, double coefficient = 1, int fontSize = 16) : base(Color.White)
    {
        Padding = padding;
        FontSize = fontSize;
        Name = new(name, fontSize: FontSize, FontStyle.Bold)
        {
            Margin = new Padding(7, 0, 1, 0)
        };
        Coefficient = new()
        {
            Text = coefficient.ToString(),
            Font = new Font( "Inter", FontSize, FontStyle.Bold, GraphicsUnit.Pixel),
            Margin = new Padding(left: 2, top: 0, right: 2, bottom: 2)
        };
        ButtonPlus = new(margin: new Padding(2, 0, 2, 1));
        CreateAndCustomizeButtons(ButtonPlus, Image.FromFile("../../../Resources/ButtonPlus.png"));

        ButtonMinus = new(margin: ButtonPlus.Margin);
        CreateAndCustomizeButtons(ButtonMinus, Image.FromFile("../../../Resources/ButtonMinus.png"));

        InitializeComponent();
        SubscribeToEvents();
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
        Name = new(name, fontSize: 16, FontStyle.Bold);
        Coefficient = new()
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
        Controls.Add(Coefficient, column++, row);
        Controls.Add(ButtonMinus, column++, row);
        Controls.Add(Name, column++, row);

        Margin = new Padding(0, 0, 0, 0);

        BackColor = Color.Transparent;
    }

    private void SubscribeToEvents()
    {
        ButtonMinus.Click += DecreaseValue;
        ButtonPlus.Click += IncreaseValue;
        Coefficient.TextChanged += DisplayStatusOfTheValueChange;

        CoefficientHasBeenChanged += (correct) => ButtonPlus.Enabled = correct;
        CoefficientHasBeenChanged += (correct) => ButtonMinus.Enabled = correct;
    }

    private void DisplayStatusOfTheValueChange(object? sender, EventArgs e)
    {
        CoefficientHasBeenChanged(double.TryParse(Coefficient.Text,
            new CultureInfo("en-US"),
            out double result) && result > 0 && result <= 1);
    }

    private void IncreaseValue(object? sender, EventArgs e)
    {
        var text = Coefficient.Text.Replace('.', ',');

        if (double.TryParse(text, out double number) && number + Delta <= 1)
            Coefficient.Text = $"{Math.Round(number + Delta, 1)}".Replace(',', '.');
    }

    private void DecreaseValue(object? sender, EventArgs e)
    {
        var text = Coefficient.Text.Replace('.', ',');

        if (double.TryParse(text, out double number) && number - Delta > 0.0000001)
            Coefficient.Text = $"{Math.Round(number - Delta, 1)}".Replace(',', '.');
    }
}
