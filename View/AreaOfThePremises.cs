using AreaCalculationPlugin.Calculator;
using AreaCalculationPlugin.View.Controls;
using AreaCalculationPlugin.View.Extensions;

namespace AreaCalculationPlugin.View;

public partial class AreaOfPremises : Form
{
    private FirstColumn firstColumn;
    private SecondColumn secondColumn;
    public event Action<IEnumerable<RoomData>> ChoseRooms;

    public static new readonly Font DefaultFont;
    static AreaOfPremises()
    {
        //var pluginFontCollection = new PrivateFontCollection();
        //pluginFontCollection.AddFontFile("Resources/Inter.ttf");
        //DefaultFont = new Font(pluginFontCollection.Families[0], 14, FontStyle.Regular, GraphicsUnit.Pixel);
        DefaultFont = new Font("Inter", 14, FontStyle.Regular, GraphicsUnit.Pixel);
    }

    public AreaOfPremises()
    {
        InitializeComponent();

        Text = "Площади помещений";
        FormBorderStyle = FormBorderStyle.Sizable;

        Controls.Add(CreatAGridOfElements());

        Padding = new Padding(30, 22, 30, 20);
        SizeChanged += ChangeMarginsOfMainColumns;
        Size = new Size(766, 672);

        BackColor = ColorTranslator.FromHtml("#F5F6F8");
        NotClientPartOfForm.CustomWindow(ColorTranslator.FromHtml("#F5F6F8"), Handle);
    }

    private Container CreatAGridOfElements()
    {
        var table = new Container();

        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        firstColumn = new FirstColumn();
        table.Controls.Add(firstColumn, 0, 0);

        secondColumn = new SecondColumn();
        secondColumn.ButtonCalculate.Click += ChangeParametersOfPremises;
        table.Controls.Add(secondColumn, 1, 0);
        
        table.Paint += MainTableOnBackgroundPaint;

        return table;
    }

    #region Обработчики событий
    private void ChangeParametersOfPremises(object? sender, EventArgs args)
    {
        var rooms = firstColumn.GetCheckedNodes(firstColumn.ListOfPremises.Nodes);
        if (ChoseRooms is not null) ChoseRooms(rooms);
    }

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