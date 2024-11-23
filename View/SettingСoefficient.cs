using AreaCalculationPlugin.Model;
using AreaCalculationPlugin.View.Controls;
using AreaCalculationPlugin.View.Extensions;

namespace AreaCalculationPlugin.View;

public partial class SettingСoefficient : Form
{
    private RectangularRoundedButton saveButton = new(ColorTranslator.FromHtml("#EFE650")) { Text = "Сохранить" };

    private readonly CoefficientsInfo[] CoefficientsInfo;

    public SettingСoefficient(CoefficientsInfo[] coefficientsInfo)
    {
        CoefficientsInfo = coefficientsInfo;

        InitializeComponent();
        InitializeControls();

        var table = CreateTableOfControls();
        FillTheControlTable(table);

        Controls.Add(table);
    }

    private void InitializeControls()
    {
        ResumeLayout(false);
        BackColor = ColorTranslator.FromHtml("#F5F6F8");
        NotClientPartOfForm.CustomWindow(ColorTranslator.FromHtml("#F5F6F8"), Handle);

        Size = new Size(570, 420);
        Text = "Настройка коэффициента";
        Padding = new Padding(25, 13, 25, 17);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        saveButton.Click += CloseForm;
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
        foreach (var coefficient in CoefficientsInfo)
        {
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            mainTable.Controls.Add(
                new DisplayAreaCoefficient(
                    new Padding(0, 6, 0, 4),
                    coefficient),
                column, row++);
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

    private void CloseForm(object? sender, EventArgs e)
    {
        Close();
    }
}

