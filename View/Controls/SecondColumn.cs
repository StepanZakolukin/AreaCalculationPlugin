using AreaCalculationPlugin.Model;

namespace AreaCalculationPlugin.View.Controls;

internal class SecondColumn : Container
{
    private readonly TextBox NumberOfDecimalPlaces;

    private readonly CoefficientsInfo[] DefaultAreaCoefficients;

    private RectangularRoundedButton ButtonSettingCoefficient = new()
    {
        Margin = new Padding(0, 0, 5, 10),
        Text = "Настройка коэффициента"
    };

    private RectangularRoundedButton ButtonCalculate = new(ColorTranslator.FromHtml("#EFE650"))
    {
        Text = "Посчитать",
        Margin = new Padding(0, 0, 5, 0),
    };

    public SecondColumn(CoefficientsInfo[] defaultAreaCoefficients)
    {
        DefaultAreaCoefficients = defaultAreaCoefficients;
        NumberOfDecimalPlaces = new()
        {
            Dock = DockStyle.Fill,
            BackColor = ColorTranslator.FromHtml("#F5F6F8"),
            ForeColor = ColorTranslator.FromHtml("#515254"),
            Font = new Font(AreaOfThePremises.PluginFontCollection.Families.First(), 11, FontStyle.Bold, GraphicsUnit.Pixel),
            TextAlign = HorizontalAlignment.Center,
            Text = "0"
        };

        ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        for (var i = 0; i < 6; i++)
            RowStyles.Add(new RowStyle(SizeType.Absolute, 49));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 68));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 49));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 23));
        RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        for (var i = 0; i < 2; i++)
            RowStyles.Add(new RowStyle(SizeType.Absolute, 38 - i * 10));

        FillSecondColumn();
        ButtonSettingCoefficient.Click += RunCoefficientSettingForm;
    }

    private void RunCoefficientSettingForm(object? sender, EventArgs e)
    {
        var formForSettingCoefficient = new SettingСoefficient(DefaultAreaCoefficients);
        formForSettingCoefficient.Show();
    }

    private void FillSecondColumn()
    {
        var titles = new[]
        {
            "Параметр номера квартиры",
            "Параметр типа помещения",
            "Коэффициент площади",
            "Площадь с коэффициентом",
            "Площадь квартиры жилая",
            "Площадь квартиры",
            "Площадь квартиры с лоджией и балконом\r\nбез коэф.",
            "Число комнат"
        };

        foreach (var cell in titles
            .Select(title => new DropdownList(title) { Margin = new Padding(left: 0, top: 0, right: 0, bottom: 4) }))
        {
            Controls.Add(cell);
        }
        Controls.Add(CreateRoundingControls());
        Controls.Add(new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(0),
            Margin = new Padding(0)
        });
        foreach (var cell in СreateAKeypadForTheSecondColumn())
            Controls.Add(cell);
    }

    Container CreateRoundingControls()
    {
        var table = new Container();
        table.Margin = new Padding(left: 0, top: 4, right: 0, bottom: 0);

        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.71F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 191));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.29F));

        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        table.Controls.Add(new Panel { Dock = DockStyle.Fill }, 0, 0);
        table.Controls.Add(new Heading("Знаков после запятой")
        {
            Margin = new Padding(left: 0, top: 0, right: 20, bottom: 0)
        }, 1, 0);

        table.Controls.Add(NumberOfDecimalPlaces, 2, 0);
        table.Controls.Add(new Panel { Dock = DockStyle.Fill }, 3, 0);

        return table;
    }

    private IEnumerable<Container> СreateAKeypadForTheSecondColumn()
    {
        var cell1 = new Container();
        cell1.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        cell1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        cell1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 164));

        cell1.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = Color.White }, 0, 0);
        cell1.Controls.Add(ButtonSettingCoefficient, 1, 0);

        yield return cell1;

        var cell2 = new Container();
        cell2.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        cell2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        cell2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
        cell2.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = Color.White }, 0, 0);
        cell2.Controls.Add(ButtonCalculate, 1, 0);

        yield return cell2;
    }
}
