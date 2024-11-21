using AreaCalculationPlugin.View.Controls;

namespace AreaCalculationPlugin.View;

public partial class AreaOfThePremises : Form
{
    private Container firstColumn;
    private Container secondColumn;
    private readonly TreeView ListOfPremises;
    private readonly List<DropdownListForGrouping> groupingParameters;

    private readonly string[] HeadersOfGroupingControls = ["Группировать", "Затем по", "Затем по"];
    private MyButton SelectAllButton = new(margin: new Padding(4, 0, 10, 6)) { Text = "Выбрать все" };
    private MyButton ButtonRevealEverything = new(margin: new Padding(4, 0, 10, 6)) { Text = "Раскрыть все" };
    private MyButton ButtonThrowOff = new(margin: new Padding(0, 0, 4, 6)) { Text = "Сбросить" };
    private MyButton ButtonHideEverything = new(margin: new Padding(0, 0, 4, 6)) { Text = "Спрятать все" };
    private MyButton ButtonSettingCoefficient = new(margin: new Padding(0, 0, 5, 10)) { Text = "Настройка коэффициента" };
    private MyButton ButtonCalculate = new(margin: new Padding(0, 0, 5, 0)) { Text = "Посчитать" };

    public AreaOfThePremises()
    {
        InitializeComponent();
        BackColor = ColorTranslator.FromHtml("#F5F6F8");
        NotClientPartOfForm.CustomWindow(ColorTranslator.FromHtml("#F5F6F8"), Handle);
        Text = "Площади помещений";
        Size = new Size(766, 672);
        Padding = new Padding(30, 22, 30, 20);
        FormBorderStyle = FormBorderStyle.Sizable;

        groupingParameters = [];
        ListOfPremises = new TreeView();
        Fill();
        Controls.Add(CreatAGridOfElements());

        SizeChanged += ChangeMarginsOfMainColumns;
        ChangeMarginsOfMainColumns(null, null);
    }

    private void ChangeMarginsOfMainColumns(object? sender, EventArgs e)
    {
        var margin = (int)(16.02 / 768.96 * firstColumn.Height);

        firstColumn.Margin = new Padding(margin);
        secondColumn.Margin = firstColumn.Margin;
    }

    Container CreatAGridOfElements()
    {
        var table = new Container();

        table.Paint += TableOnBorderPaint;

        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        firstColumn = CreateFirstColumn();
        table.Controls.Add(firstColumn, 0, 0);
        FillInTheFirstColumn(firstColumn);

        secondColumn = CreateSecondColumn();
        table.Controls.Add(secondColumn, 1, 0);
        FillSecondColumn(secondColumn);

        return table;
    }

    private void TableOnBorderPaint(object? sender, PaintEventArgs e)
    {
        var borderSize = 3;
        var table = sender as Container;

        e.Graphics.CreateARoundedRectangle(pen: new Pen(ColorTranslator.FromHtml("#EEEEEE"), borderSize),
            size: new Size(table.Size.Width - 2 * borderSize, table.Size.Height - 2 * borderSize),
            point: new Point(borderSize, borderSize),
            radius: 10);
    }

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
        var table = new Container
        {
            BackColor = ColorTranslator.FromHtml("#F5F6F8"),
            Padding = new Padding(0, 3, 0, 0),
            Margin = new Padding(0, 4, 0, 0)
        };

        table.RowStyles.Add(new RowStyle(SizeType.Percent, 78.82F));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 21.18F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        table.Controls.Add(ListOfPremises);
        ListOfPremises.Dock = DockStyle.Fill;
        ListOfPremises.BackColor = Color.White;
        ListOfPremises.Padding = new Padding(0);
        ListOfPremises.Margin = new Padding(6, 0, 6, 5);

        table.Controls.Add(СreateAKeypadForTheFirstColumn(new[,]
        {
            { SelectAllButton, ButtonRevealEverything } ,
            { ButtonThrowOff, ButtonHideEverything }
        }));

        return table;
    }

    public void Fill()
    {
        ListOfPremises.CheckBoxes = true;
        ListOfPremises.ShowLines = false;
        ListOfPremises.Scrollable = true;
        ListOfPremises.BorderStyle = BorderStyle.None;

        for (var i = 0; i < 10; i++)
        {
            ListOfPremises.Nodes.Add("Родитель" + i.ToString());
            ListOfPremises.Nodes[i].Nodes.Add("Ребенок 1");
            ListOfPremises.Nodes[i].Nodes.Add("Ребенок 2");
        }
    }

    Container СreateAKeypadForTheFirstColumn(MyButton[,] buttons)
    {
        var table = new Container { BackColor = ColorTranslator.FromHtml("#F5F6F8") };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

        for (var row = 0; row < 2; row++)
        {
            for (var column = 0; column < 2; column++)
            {
                table.Controls.Add(buttons[row, column], row, column);
            }
        }

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

    void FillSecondColumn(Container table)
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

        foreach (var cell in titles.Select(title => new DropdownList(title)))
            table.Controls.Add(cell);
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
        var table = new Container { BackColor = Color.White };

        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.71F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 151));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.29F));

        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        table.Controls.Add(new Panel { Dock = DockStyle.Fill }, 0, 0);
        table.Controls.Add(new Heading { Text = "Знаков после запятой" }, 1, 0);
        table.Controls.Add(new TextBox
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            ForeColor = ColorTranslator.FromHtml("#515254"),
            Font = new Font("Inter", 11, FontStyle.Bold, GraphicsUnit.Pixel),
        }, 2, 0);
        table.Controls.Add(new Panel { Dock = DockStyle.Fill }, 3, 0);

        return table;
    }

    private IEnumerable<Container> СreateAKeypadForTheSecondColumn()
    {
        var cell1 = new Container { BackColor = Color.White };
        cell1.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        cell1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        cell1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));

        cell1.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = Color.White }, 0, 0);
        cell1.Controls.Add(ButtonSettingCoefficient, 1, 0);

        yield return cell1;

        var cell2 = new Container { BackColor = Color.White };
        cell2.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        cell2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        cell2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
        cell2.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = Color.White }, 0, 0);
        cell2.Controls.Add(ButtonCalculate, 1, 0);

        yield return cell2;
    }
    #endregion
}