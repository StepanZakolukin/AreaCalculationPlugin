using System.Runtime.InteropServices;
using System.Drawing;

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
        Size = new Size(660, 589);
        FormBorderStyle = FormBorderStyle.Sizable;
        BackColor = ColorTranslator.FromHtml("#F5F6F8");
        Text = "Площади помещений";
        CustomWindow(ColorTranslator.FromHtml("#F5F6F8"), Handle);
        Padding = new Padding(30, 22, 30, 20);

        GridOfElements = CreatAGridOfElements();

        //Paint += AreaOfThePremises_Paint;
    }

    private void AreaOfThePremises_Paint(object? sender, PaintEventArgs e)
    {
        e.Graphics.CreateARoundedRectangle(new Pen(Color.Red), new Size(500, 500), new Point(10, 10), 50);
    }

    TableLayoutPanel CreatAGridOfElements()
    {
        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(0),
            Padding = new Padding(10)
        };

        table.Paint += TableOnCellPaint;
        Controls.Add(table);
        
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        
        for (var i = 0; i < 3; i++)
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 12));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 52));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 12));
        
        FillInTheFirstColumn(table);
        FillInTheSecondColumn(table);
        
        return table;
    }

    private void TableOnCellPaint(object? sender, PaintEventArgs e)
    {
        var table = sender as TableLayoutPanel;
        var borderSize = 3;
        e.Graphics.CreateARoundedRectangle(new Pen(Color.Red, 3), new Size(table.Size.Width - 2 * borderSize - 1, table.Size.Height - 2 * borderSize - 1),
            new Point(1, 1), 10);
    }

    #region Первая колонка
    void FillInTheFirstColumn(TableLayoutPanel table)
    {
        var index = 0;
        foreach (var cell in CreateGroupingСontrols(new[] { "Группировать", "Затем по", "Затем по"}))
            table.Controls.Add(cell, 0, index++);
        
        table.Controls.Add(new Panel { Dock = DockStyle.Fill }, 0, index++);

        table.Controls.Add(СreateAKeypadForTheFirstColumn(new[,] 
        {
            { SelectAllButton, ButtonRevealEverything } ,
            { ButtonThrowOff, ButtonHideEverything }
        }), 0, index++);
    }

    IEnumerable<TableLayoutPanel> CreateGroupingСontrols(string[] titles)
    {
        for (var row = 0; row < 3; row++)
        {
            var cell = new TableLayoutPanel { Dock = DockStyle.Fill };
            
            cell.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            cell.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            cell.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            var list = new ComboBox { Dock = DockStyle.Fill };
            cell.Controls.Add(new Label { Dock = DockStyle.Fill, Text = titles[row] }, 0, 0);
            cell.Controls.Add(list, 0, 1);

            yield return cell;
        }
    }

    TableLayoutPanel СreateAKeypadForTheFirstColumn(Button[,] buttons)
    {
        var table = new TableLayoutPanel { Dock = DockStyle.Fill };
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
    void FillInTheSecondColumn(TableLayoutPanel table)
    {
        var index = 0;
        foreach (var cell in CreateAParameterControlPanel(new[,]
                 {
                     { "Параметр номера квартиры", "Номер"},
                     { "Параметр типа помещения", "Коэф." }
                 }))
        {
            table.Controls.Add(cell, 1, index++);
        }
        table.Controls.Add(CreateRoundingControls(), 0, index++);
        table.Controls.Add(new Panel { Dock = DockStyle.Fill }, 1, index++);  
        table.Controls.Add(СreateAKeypadForTheSecondColumn(), 1, index++);  
    }

    IEnumerable<TableLayoutPanel> CreateAParameterControlPanel(string[,] titles)
    {
        for (var row = 0; row < 2; row++)
        {
            var table = new TableLayoutPanel { Dock = DockStyle.Fill };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 77.77F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.22F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            
            table.Controls.Add(new Label { Dock = DockStyle.Fill, Text = titles[row, 0] }, 0, 0);
            table.Controls.Add(new Label { Dock = DockStyle.Fill, Text = titles[row, 1] }, 1, 0);

            var list = new ComboBox { Dock = DockStyle.Fill };
            table.Controls.Add(list, 0, 1);
            table.Controls.Add(new TextBox { Dock = DockStyle.Fill }, 1, 1);

            yield return table;
        }
    }

    TableLayoutPanel CreateRoundingControls()
    {
        var table = new TableLayoutPanel { Dock = DockStyle.Fill };
        
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 59.63F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.22F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.15F));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        
        table.Controls.Add(new Label { Dock = DockStyle.Fill, Text = "Знаков после запятой" }, 0, 0);
        table.Controls.Add(new TextBox { Dock = DockStyle.Fill }, 1, 0);
        table.Controls.Add(new Panel { Dock = DockStyle.Fill }, 2, 0);
        for (var column = 0; column < 3; column++)
            table.Controls.Add(new Panel { Dock = DockStyle.Fill }, column, 1);
        
        return table;
    }

    TableLayoutPanel СreateAKeypadForTheSecondColumn()
    {
        var table = new TableLayoutPanel { Dock = DockStyle.Fill };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        
        var cell1 = new TableLayoutPanel { Dock = DockStyle.Fill };
        cell1.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        cell1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.42F));
        cell1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.57F));
        cell1.Controls.Add(new Panel { Dock = DockStyle.Fill }, 0, 0);
        ButtonSettingCoefficient.Dock = DockStyle.Fill;
        cell1.Controls.Add(ButtonSettingCoefficient, 1, 0);
        
        var cell2 = new TableLayoutPanel { Dock = DockStyle.Fill };
        cell2.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        cell2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.57F));
        cell2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.42F));
        cell2.Controls.Add(new Panel { Dock = DockStyle.Fill }, 0, 0);
        ButtonCalculate.Dock = DockStyle.Fill;
        cell2.Controls.Add(ButtonCalculate, 1, 0);
        
        table.Controls.Add(cell1, 0, 0);
        table.Controls.Add(cell2, 0, 1);
        
        return table;
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