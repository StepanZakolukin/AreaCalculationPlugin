using AreaCalculationPlugin.View.Controls;

namespace AreaCalculationPlugin.View;

internal class SettingСoefficient : Form
{
    private MyButton saveButton = new(ColorTranslator.FromHtml("#EFE650")) { Text = "Сохранить" };

    private Dictionary<string, double> coefficientsInfo = new()
    {
        ["Жилые помещения квартиры"] = 1,
        ["Нежилые помещения квартиры"] = 1,
        ["Лоджии"] = 0.5,
        ["Балконы, Террасы"] = 0.3,
        ["Нежилые помещения, Общественные (МОП)"] = 1,
        ["Офисы"] = 1,
        ["Теплые лоджии"] = 1
    };

    public SettingСoefficient()
    {
        InitializeComponent();

        var table = CreateTableOfControls();
        FillTheControlTable(table);

        Controls.Add(table);
    }

    private void FillTheControlTable(Container mainTable)
    {
        var row = 0;
        mainTable.Controls.Add(CreateCoefficientAdjustmentTable(), 0, row++);
        mainTable.Controls.Add(new Panel { Margin = new Padding(0), Padding = new Padding(0) }, 0, row++);

        var cellForButton = new Container(ColorTranslator.FromHtml("#F5F6F8"));
        cellForButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        cellForButton.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 165));
        cellForButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        cellForButton.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        cellForButton.Controls.Add(new Panel(), 0, 0);
        cellForButton.Controls.Add(saveButton, 1, 0);
        cellForButton.Controls.Add(new Panel(), 2, 0);

        mainTable.Controls.Add(cellForButton, 0, row++);
    }

    private Container CreateCoefficientAdjustmentTable()
    {
        var mainTable = new Container(ColorTranslator.FromHtml("#F5F6F8"))
        {
            Padding = new Padding(6, 6, 6, 14)
        };
        mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        mainTable.Paint += MainTableOnBackgroundPaint;

        var row = 0;
        var column = 0;
        foreach (var coefficientName in coefficientsInfo.Keys)
        {
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            var currentCell = new AreaCoefficient(
                coefficientName,
                new Padding(0, 6, 0, 4),
                coefficientsInfo[coefficientName]);

            mainTable.Controls.Add(currentCell, column, row++);
            currentCell.CoefficientHasBeenChanged += (bool correct) => saveButton.Enabled = correct;
        }

        return mainTable;
    }

    private void MainTableOnBackgroundPaint(object? sender, PaintEventArgs e)
    {
        var table = sender as Container;
        var graphics = e.Graphics;

        graphics.FillRoundedRectangle(Color.White,
            ColorTranslator.FromHtml("#EEEEEE"),
            borderSize: 1,
            new Rectangle(
                new Point(0, 0),
                new Size(table.Width, table.Height)),
            radius: 10);

        graphics.Dispose();
    }

    private Container CreateTableOfControls()
    {
        var table = new Container(ColorTranslator.FromHtml("#F5F6F8"));

        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 300));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        return table;
    }

    private void InitializeComponent()
    {
        ResumeLayout(false);
        AutoScaleMode = AutoScaleMode.None;
        BackColor = ColorTranslator.FromHtml("#F5F6F8");
        NotClientPartOfForm.CustomWindow(ColorTranslator.FromHtml("#F5F6F8"), Handle);

        Size = new Size(570, 420);
        Text = "Настройка коэффициента";
        Padding = new Padding(25, 13, 25, 17);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
    }
}
