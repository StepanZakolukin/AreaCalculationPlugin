using AreaCalculationPlugin.View.Controls;

namespace AreaCalculationPlugin.View;

internal class AreaCoefficient : Container
{
    public readonly Heading Name;
    public readonly TextBox Coefficient;
    public readonly MyButton ButtonToIncreaseValue;
    public readonly MyButton DecreaseValueButton;
    private const double Delta = 0.1;
    public event Action<bool> CoefficientHasBeenChanged;

    public AreaCoefficient(string name, double coefficient = 1) : base()
    {
        Name = new(name);
        Coefficient = new() { Text = coefficient.ToString() };
        ButtonToIncreaseValue = new();
        DecreaseValueButton = new();

        InitializeComponent();
        SubscribeToEvents();
    }

    private void InitializeComponent()
    {
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 21));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
        ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 21));
        ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var row = 0;
        var column = 0;
        Controls.Add(ButtonToIncreaseValue, column++, row);
        Controls.Add(Coefficient, column++, row);
        Controls.Add(DecreaseValueButton, column++, row);
        Controls.Add(Name, column++, row);

        Margin = new Padding(0, 0, 0, 4);
    }

    private void SubscribeToEvents()
    {
        DecreaseValueButton.Click += DecreaseValue;
        ButtonToIncreaseValue.Click += IncreaseValue;
        Coefficient.TextChanged += DisplayStatusOfTheValueChange;

        CoefficientHasBeenChanged += (bool correct) => ButtonToIncreaseValue.Enabled = correct;
        CoefficientHasBeenChanged += (bool correct) => DecreaseValueButton.Enabled = correct;
    }

    private void DisplayStatusOfTheValueChange(object? sender, EventArgs e)
    {
        CoefficientHasBeenChanged(double.TryParse(Coefficient.Text, out double result) && result > 0);
    }

    private void IncreaseValue(object? sender, EventArgs e)
    {
        var number = double.Parse(Coefficient.Text);
        if (number + Delta <= 1) Coefficient.Text = $"{Math.Round(number + Delta, 1)}";
    }

    private void DecreaseValue(object? sender, EventArgs e)
    {
        var number = double.Parse(Coefficient.Text);
        if (number - Delta > 0.0000001) Coefficient.Text = $"{Math.Round(number - Delta, 1)}";
    }
}
