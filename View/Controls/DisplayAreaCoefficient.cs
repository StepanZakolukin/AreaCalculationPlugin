using AreaCalculationPlugin.Calculator;
using AreaCalculationPlugin.View.Extensions;
using System.Resources;


namespace AreaCalculationPlugin.View.Controls;

internal class DisplayAreaCoefficient : Container
{
    private int fontSize;
    public int FontSize 
    { 
        get
        {
            return fontSize;
        }
        set
        {
            if (value == 0)
                throw new ArgumentException();
            fontSize = value;
        }
    }

    public StringFormat Format { get; set; }

    private const double Delta = 0.1;

    private readonly Heading Title;
    private readonly RoundButton ButtonPlus;
    private readonly RoundButton ButtonMinus;
    private readonly Panel DisplayForCoefficient;
    private readonly RoomCategory RoomCategory;

    private readonly static Image PlusImage;
    private readonly static Image MinusImage;

    static DisplayAreaCoefficient()
    {
        var rm = new ResourceManager(typeof(SettingСoefficient));
        MinusImage = (Bitmap)new ImageConverter().ConvertFrom(rm.GetObject("ButtonMinus"));
        PlusImage = (Bitmap)new ImageConverter().ConvertFrom(rm.GetObject("ButtonPlus"));
    }

    public DisplayAreaCoefficient(Padding padding, RoomCategory roomCategory, int fontSz = 16)
        : base(Color.White)
    {
        Padding = padding;
        fontSize = fontSz;
        RoomCategory = roomCategory;
        ForeColor = ColorTranslator.FromHtml("#515254");
        Format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        Title = new($"{RoomCategoryConverter.Convert(roomCategory)}:", fontSize: FontSize, FontStyle.Bold)
        {
            Margin = new Padding(7, 0, 1, 0)
        };

        DisplayForCoefficient = new Panel()
        {
            Margin = new Padding(left: 2, top: 0, right: 2, bottom: 2),
            Dock = DockStyle.Fill,
            Padding = new Padding(0)
        };
        DisplayForCoefficient.Paint += DrawBackground;

        ButtonPlus = new(margin: new Padding(2, 0, 2, 1));
        CreateAndCustomizeButtons(ButtonPlus, PlusImage);

        ButtonMinus = new(margin: ButtonPlus.Margin);
        CreateAndCustomizeButtons(ButtonMinus, MinusImage);

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
            Math.Round(PluginManager.ParameterCorrector.AreaCoefficients[RoomCategory], 1).ToString().Replace(',', '.'),
            Font,
            new SolidBrush(ForeColor),
            new Rectangle(Point.Empty, DisplayForCoefficient.Size),
            Format);
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
        Controls.Add(Title, column++, row);

        Margin = new Padding(0, 0, 0, 0);

        BackColor = Color.Transparent;
        Font = new Font(AreaOfPremises.DefaultFont.FontFamily, FontSize, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    private void SubscribeToEvents()
    {
        ButtonMinus.Click += DecreaseValue;
        ButtonPlus.Click += IncreaseValue;
    }

    private void IncreaseValue(object? sender, EventArgs e)
    {
        if (PluginManager.ParameterCorrector.AreaCoefficients[RoomCategory] + Delta <= 1)
            PluginManager.ParameterCorrector.AreaCoefficients[RoomCategory] += Delta;

        Invalidate();
    }

    private void DecreaseValue(object? sender, EventArgs e)
    {
        if (PluginManager.ParameterCorrector.AreaCoefficients[RoomCategory] - Delta > 0.0000001)
            PluginManager.ParameterCorrector.AreaCoefficients[RoomCategory] -= Delta;

        Invalidate();
    }
}