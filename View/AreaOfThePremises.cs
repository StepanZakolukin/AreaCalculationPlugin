using AreaCalculationPlugin.Model;
using AreaCalculationPlugin.View.Controls;
using AreaCalculationPlugin.View.Extensions;
using System.Drawing.Text;

namespace AreaCalculationPlugin.View;

public partial class AreaOfThePremises : Form
{
    private Container firstColumn;
    private Container secondColumn;

    private readonly TreeView ListOfPremises;
    private readonly TextBox NumberOfDecimalPlaces;
    private readonly List<DropdownListForGrouping> groupingParameters;
    private readonly CoefficientsInfo[] DefaultAreaCoefficients;

    private readonly string[] HeadersOfGroupingControls = ["Группировать", "Затем по", "Затем по"];
    private RectangularRoundedButton SelectAllButton = new()
    {
        Margin = new Padding(0, 0, 5, 5),
        Text = "Выбрать все"
    };
    private RectangularRoundedButton ButtonRevealEverything = new()
    {
        Margin = new Padding(0, 5, 5, 0), 
        Text = "Раскрыть все" 
    };
    private RectangularRoundedButton ButtonThrowOff = new() 
    {
        Margin = new Padding(5, 0, 0, 5),
        Text = "Сбросить"
    };
    private RectangularRoundedButton ButtonHideEverything = new() 
    {
        Margin = new Padding(5, 5, 0, 0),
        Text = "Спрятать все" 
    };
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

    public static PrivateFontCollection PluginFontCollection = new();

    static AreaOfThePremises()
    {
        PluginFontCollection.AddFontFile("Resources/Inter.ttf");
    }

    public AreaOfThePremises(CoefficientsInfo[] defaultAreaCoefficients)
    {
        DefaultAreaCoefficients = defaultAreaCoefficients;

        InitializeComponent();
        BackColor = ColorTranslator.FromHtml("#F5F6F8");
        NotClientPartOfForm.CustomWindow(ColorTranslator.FromHtml("#F5F6F8"), Handle);
        Text = "Площади помещений";
        Size = new Size(766, 672);
        Padding = new Padding(30, 22, 30, 20);
        FormBorderStyle = FormBorderStyle.Sizable;

        NumberOfDecimalPlaces = new()
        {
            Dock = DockStyle.Fill,
            BackColor = ColorTranslator.FromHtml("#F5F6F8"),
            ForeColor = ColorTranslator.FromHtml("#515254"),
            Font = new Font(PluginFontCollection.Families.First(), 11, FontStyle.Bold, GraphicsUnit.Pixel),
        };

        NumberOfDecimalPlaces.Text = NumberOfDecimalPlaces.Font.Name;

        groupingParameters = [];
        ListOfPremises = new TreeView();
        Controls.Add(CreatAGridOfElements());

        SubscribeToEvents();
        ChangeMarginsOfMainColumns(null, null);
    }

    private Container CreatAGridOfElements()
    {
        var table = new Container();

        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        firstColumn = CreateFirstColumn();
        table.Controls.Add(firstColumn, 0, 0);
        FillInTheFirstColumn(firstColumn);

        secondColumn = CreateSecondColumn();
        table.Controls.Add(secondColumn, 1, 0);
        FillSecondColumn(secondColumn);
        table.Paint += MainTableOnBackgroundPaint;

        return table;
    }

    #region Обработчики событий
    private void SubscribeToEvents()
    {
        SizeChanged += ChangeMarginsOfMainColumns;
        ButtonSettingCoefficient.Click += RunCoefficientSettingForm;
        ButtonHideEverything.Click += HideEverything_Click;
        ButtonRevealEverything.Click += RevealEverything_Click;
        ListOfPremises.AfterCheck += Node_AfterCheck;
        SelectAllButton.Click += SelectAll_Click; ;
        ButtonThrowOff.Click += ThrowOff_Click;
    }

    private void SelectAll_Click(object? sender, EventArgs e)
    {
        ListOfPremises.MarkAll(true);
    }

    private void ThrowOff_Click(object? sender, EventArgs e)
    {
        ListOfPremises.MarkAll(false);
    }

    private void RevealEverything_Click(object? sender, EventArgs e)
    {
        ListOfPremises.ExpandAll();
    }

    private void HideEverything_Click(object? sender, EventArgs e)
    {
        ListOfPremises.CollapseAll();
    }

    private void Node_AfterCheck(object sender, TreeViewEventArgs e)
    {
        e.Node.CheckAllChildNodes(e.Node.Checked);
    }

    private void RunCoefficientSettingForm(object? sender, EventArgs e)
    {
        var formForSettingCoefficient = new SettingСoefficient(DefaultAreaCoefficients);
        formForSettingCoefficient.Show();
    }

    private void ChangeMarginsOfMainColumns(object? sender, EventArgs e)
    {
        var margin = (int)(16.02 / 768.96 * firstColumn.Height);

        firstColumn.Margin = new Padding(margin);
        secondColumn.Margin = firstColumn.Margin;
    }

    private void DrawBackgroundOfControlUnit(object? sender, PaintEventArgs e)
    {
        var table = sender as Container;
        var graphics = e.Graphics;

        graphics.FillRoundedRectangle(brush: new SolidBrush(ColorTranslator.FromHtml("#F5F6F8")),
            new Rectangle(0, 0, table.Size.Width,
            table.Size.Height),
            radius: 10);

        graphics.Dispose();
    }

    private void MainTableOnBackgroundPaint(object? sender, PaintEventArgs e)
    {
        var table = sender as Container;
        var graphics = e.Graphics;

        graphics.FillRoundedRectangle(Color.White,
            ColorTranslator.FromHtml("#EEEEEE"),
            borderSize: 3,
            new Rectangle(
                new Point(0, 0),
                table.Size),
            radius: 10);

        graphics.Dispose();
    }
    #endregion

    #region Первая колонка
    private Container CreateFirstColumn()
    {
        var column = new Container();

        for (var i = 0; i < 3; i++)
            column.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        column.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        return column;
    }

    private void FillInTheFirstColumn(Container table)
    {
        foreach (var dropdownList in HeadersOfGroupingControls
            .Select(title => new DropdownListForGrouping(title)))
        {
            table.Controls.Add(dropdownList);
            groupingParameters.Add(dropdownList);
        }

        table.Controls.Add(CreateContainer());
    }

    private Container CreateContainer()
    {
        var table = new Container()
        {
            Padding = new Padding(4, 4, 4, 6),
            Margin = new Padding(0, 4, 0, 0)
        };
        table.Paint += DrawBackgroundOfControlUnit;

        table.RowStyles.Add(new RowStyle(SizeType.Percent, 78.82F));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 21.18F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        table.Controls.Add(ListOfPremises);
        ListOfPremises.Dock = DockStyle.Fill;
        ListOfPremises.BackColor = Color.White;
        ListOfPremises.Padding = new Padding(0);
        ListOfPremises.Margin = new Padding(5, 5, 5, 5);

        table.Controls.Add(СreateAKeypadForTheFirstColumn(new[,]
        {
            { SelectAllButton, ButtonRevealEverything } ,
            { ButtonThrowOff, ButtonHideEverything }
        }));

        return table;
    }

    private Container СreateAKeypadForTheFirstColumn(RectangularRoundedButton[,] buttons)
    {
        var table = new Container();
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

        for (var row = 0; row < 2; row++)
            for (var column = 0; column < 2; column++)
                table.Controls.Add(buttons[row, column], row, column);

        return table;
    }
    #endregion

    #region Вторая колонка
    private Container CreateSecondColumn()
    {
        var column = new Container();

        column.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        for (var i = 0; i < 6; i++)
            column.RowStyles.Add(new RowStyle(SizeType.Absolute, 49));
        column.RowStyles.Add(new RowStyle(SizeType.Absolute, 68));
        column.RowStyles.Add(new RowStyle(SizeType.Absolute, 49));
        column.RowStyles.Add(new RowStyle(SizeType.Absolute, 23));
        column.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        for (var i = 0; i < 2; i++)
            column.RowStyles.Add(new RowStyle(SizeType.Absolute, 38 - i * 10));

        return column;
    }

    private void FillSecondColumn(Container table)
    {
        var titles = new[] {
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
            .Select(title => new DropdownList(title) { Margin = new Padding(left: 0, top: 0, right: 0, bottom: 4)}))
        {
            table.Controls.Add(cell);
        }
        table.Controls.Add(CreateRoundingControls());
        table.Controls.Add(new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(0),
            Margin = new Padding(0)
        });
        foreach (var cell in СreateAKeypadForTheSecondColumn())
            table.Controls.Add(cell);
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
        cell1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));

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
    #endregion
}