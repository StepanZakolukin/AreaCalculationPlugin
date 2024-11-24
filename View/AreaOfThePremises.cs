using AreaCalculationPlugin.Model;
using AreaCalculationPlugin.View.Controls;
using AreaCalculationPlugin.View.Extensions;
using System.Drawing.Text;

namespace AreaCalculationPlugin.View;

public partial class AreaOfThePremises : Form
{
    private Container firstColumn;
    private Container secondColumn;

    public static PrivateFontCollection PluginFontCollection = new();

    static AreaOfThePremises()
    {
        PluginFontCollection.AddFontFile("Resources/Inter.ttf");
    }

    public AreaOfThePremises(CoefficientsInfo[] defaultAreaCoefficients)
    {
        InitializeComponent();
        BackColor = ColorTranslator.FromHtml("#F5F6F8");
        NotClientPartOfForm.CustomWindow(ColorTranslator.FromHtml("#F5F6F8"), Handle);
        Text = "Площади помещений";
        Size = new Size(766, 672);
        Padding = new Padding(30, 22, 30, 20);
        FormBorderStyle = FormBorderStyle.Sizable;

        Controls.Add(CreatAGridOfElements(defaultAreaCoefficients));

        SizeChanged += ChangeMarginsOfMainColumns;
        ChangeMarginsOfMainColumns(null, null);
    }

    private Container CreatAGridOfElements(CoefficientsInfo[] defaultAreaCoefficients)
    {
        var table = new Container();

        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        firstColumn = new FirstColumn();
        table.Controls.Add(firstColumn, 0, 0);

        secondColumn = new SecondColumn(defaultAreaCoefficients);
        table.Controls.Add(secondColumn, 1, 0);
        
        table.Paint += MainTableOnBackgroundPaint;

        return table;
    }

    #region Обработчики событий
    private void ChangeMarginsOfMainColumns(object? sender, EventArgs e)
    {
        var margin = (int)(16.02 / 768.96 * firstColumn.Height);

        firstColumn.Margin = new Padding(margin);
        secondColumn.Margin = firstColumn.Margin;
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
}