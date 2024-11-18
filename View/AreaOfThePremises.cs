using System.Runtime.InteropServices;

namespace AreaCalculationPlugin.View;

public partial class AreaOfThePremises : Form
{
    private TableLayoutPanel GridOfElements;
    private Button SelectAllButton = new() { Text = "Выбрать все" };
    private Button ButtonRevealEverything = new() { Text = "Раскрыть все" };
    private Button ButtonThrowOff = new() { Text = "Сбросить" };
    private Button ButtonHideEverything = new() { Text = "Спрятать все" };
    private Button ButtonSettingCoefficient = new() { Text = "Настройка коэффициента" };
    private Button ButtonCalculate = new() { Text = "Посчитать" };
    
    public AreaOfThePremises()
    {
        InitializeComponent();
        BackColor = ColorTranslator.FromHtml("#F5F6F8");
        CustomWindow(ColorTranslator.FromHtml("#F5F6F8"), Handle);
        Text = "Площади помещений";
        Size = new Size(766, 672);
        Padding = new Padding(30, 22, 30, 20);
        FormBorderStyle = FormBorderStyle.Sizable;

        GridOfElements = CreatAGridOfElements();
    }

    TableLayoutPanel CreatAGridOfElements()
    {
        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(0),
            Padding = new Padding(10)
        };

        table.Paint += TableOnBorderPaint;
        Controls.Add(table);
        
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        
        table.Controls.Add(CreateFirstColumn(), 0, 0);
        table.Controls.Add(CreateSecondColumn(), 1, 0);
        
        return table;
    }

    private void TableOnBorderPaint(object? sender, PaintEventArgs e)
    {
        var borderSize = 3;
        var table = sender as TableLayoutPanel;

        e.Graphics.CreateARoundedRectangle(pen: new Pen(ColorTranslator.FromHtml("#EEEEEE"), borderSize),
            size: new Size(table.Size.Width - 2 * borderSize, table.Size.Height - 2 * borderSize),
            point: new Point(borderSize, borderSize),
            radius: 10);
    }

    #region Первая колонка
    private TableLayoutPanel CreateFirstColumn()
    {
        var firstColumn = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(0),
            Padding = new Padding(0),
        };

        for (var i = 0; i < 3; i++)
            firstColumn.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        firstColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        FillInTheFirstColumn(firstColumn);

        return firstColumn;
    }

    private void FillInTheFirstColumn(TableLayoutPanel table)
    {
        foreach (var cell in CreateGroupingСontrols(new[] { "Группировать", "Затем по", "Затем по"}))
            table.Controls.Add(cell);

        table.Controls.Add(CreateContainer());
    }

    private TableLayoutPanel CreateContainer()
    {
        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(0),
            Margin = new Padding(0),
        };

        table.RowStyles.Add(new RowStyle(SizeType.Percent, 78.82F));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 21.18F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        table.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = Color.White });

        table.Controls.Add(СreateAKeypadForTheFirstColumn(new[,]
        {
            { SelectAllButton, ButtonRevealEverything } ,
            { ButtonThrowOff, ButtonHideEverything }
        }));

        return table;
    }

    IEnumerable<TableLayoutPanel> CreateGroupingСontrols(string[] titles)
    {
        for (var row = 0; row < 3; row++)
        {
            var cell = new TableLayoutPanel { Dock = DockStyle.Fill, BackColor = Color.White };
            
            cell.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            cell.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            cell.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            var list = new ComboBox { Dock = DockStyle.Fill, BackColor = Color.White };
            cell.Controls.Add(new Label { Dock = DockStyle.Fill, Text = titles[row] }, 0, 0);
            cell.Controls.Add(list, 0, 1);

            yield return cell;
        }
    }

    TableLayoutPanel СreateAKeypadForTheFirstColumn(Button[,] buttons)
    {
        var table = new TableLayoutPanel { Dock = DockStyle.Fill, BackColor = Color.White };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

        for (var row = 0; row < 2; row++)
        {
            for (var column = 0; column < 2; column++)
            {
                buttons[row, column].Dock = DockStyle.Fill;
                table.Controls.Add(buttons[row, column], row, column);
            }
        }
        
        return table;
    }
    #endregion

    #region Вторая колонка
    private TableLayoutPanel CreateSecondColumn()
    {
        var secondColumn = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(0),
            Margin = new Padding(0)
        };

        secondColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        for (var i = 0; i < 6; i++)
            secondColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 7.5F));
        secondColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 10.72F));
        secondColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 7.5F));
        secondColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 3.57F));
        secondColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 10.84F));
        for (var i = 0; i < 2; i++)
            secondColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
        
        FillSecondColumn(secondColumn);

        return secondColumn;
    }

    void FillSecondColumn(TableLayoutPanel table)
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

        foreach (var cell in CreateAParameterControlPanel(titles))
            table.Controls.Add(cell);
        table.Controls.Add(CreateRoundingControls());
        table.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = Color.White });  
        foreach (var cell in СreateAKeypadForTheSecondColumn())
            table.Controls.Add(cell);
    }

    IEnumerable<TableLayoutPanel> CreateAParameterControlPanel(string[] titles)
    {
        for (var row = 0; row < 8; row++)
        {
            var table = new TableLayoutPanel { Dock = DockStyle.Fill, BackColor = Color.White };
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            
            table.Controls.Add(new Label { Dock = DockStyle.Fill, Text = titles[row] }, 0, 0);

            var list = new ComboBox { Dock = DockStyle.Fill, BackColor = Color.White };
            table.Controls.Add(list, 0, 1);

            yield return table;
        }
    }

    TableLayoutPanel CreateRoundingControls()
    {
        var table = new TableLayoutPanel { Dock = DockStyle.Fill, BackColor = Color.White };
        
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.69F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.83F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.21F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.23F));

        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        table.Controls.Add(new Panel { Dock = DockStyle.Fill }, 0, 0);
        table.Controls.Add(new Label { Dock = DockStyle.Fill, Text = "Знаков после запятой" }, 1, 0);
        table.Controls.Add(new TextBox { Dock = DockStyle.Fill, BackColor = Color.White }, 2, 0);
        table.Controls.Add(new Panel { Dock = DockStyle.Fill }, 3, 0);

        return table;
    }

    private IEnumerable<TableLayoutPanel> СreateAKeypadForTheSecondColumn()
    {
        var cell1 = new TableLayoutPanel { Dock = DockStyle.Fill, BackColor = Color.White };
        cell1.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        cell1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.42F));
        cell1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.57F));
        cell1.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = Color.White }, 0, 0);
        ButtonSettingCoefficient.Dock = DockStyle.Fill;
        cell1.Controls.Add(ButtonSettingCoefficient, 1, 0);

        yield return cell1;
        
        var cell2 = new TableLayoutPanel { Dock = DockStyle.Fill, BackColor = Color.White };
        cell2.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        cell2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.57F));
        cell2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.42F));
        cell2.Controls.Add(new Panel { Dock = DockStyle.Fill, BackColor = Color.White }, 0, 0);
        ButtonCalculate.Dock = DockStyle.Fill;
        cell2.Controls.Add(ButtonCalculate, 1, 0);

        yield return cell2;
    }
    #endregion

    #region Меням цвет вехней рамки формы
    private string ToBgr(Color c) => $"{c.B:X2}{c.G:X2}{c.R:X2}";
    
    [DllImport("DwmApi")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
    
    const int DWWMA_CAPTION_COLOR = 35;
    public void CustomWindow(Color captionColor, IntPtr handle)
    {
        IntPtr hWnd = handle;
        //Change caption color
        int[] caption = new int[] { int.Parse(ToBgr(captionColor), System.Globalization.NumberStyles.HexNumber) };
        DwmSetWindowAttribute(hWnd, DWWMA_CAPTION_COLOR, caption, 4);
    }
    #endregion
}