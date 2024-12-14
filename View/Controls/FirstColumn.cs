using AreaCalculationPlugin.View.Extensions;

namespace AreaCalculationPlugin.View.Controls;

internal class FirstColumn : Container
{
    private readonly TreeView ListOfPremises;
    private readonly List<DropdownListForGrouping> groupingParameters;
    private static readonly string[] HeadersOfGroupingControls = new[] { "Группировать", "Затем по", "Затем по" };

    private readonly RectangularRoundedButton SelectAllButton = new()
    {
        Margin = new Padding(0, 0, 5, 5),
        Text = "Выбрать все"
    };
    private readonly RectangularRoundedButton ButtonRevealEverything = new()
    {
        Margin = new Padding(0, 5, 5, 0),
        Text = "Раскрыть все"
    };
    private readonly RectangularRoundedButton ButtonThrowOff = new()
    {
        Margin = new Padding(5, 0, 0, 5),
        Text = "Сбросить"
    };
    private readonly RectangularRoundedButton ButtonHideEverything = new()
    {
        Margin = new Padding(5, 5, 0, 0),
        Text = "Спрятать все"
    };

    public FirstColumn() : base()
    {
        for (var i = 0; i < 3; i++)
            RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        ListOfPremises = new TreeView
        {
            BorderStyle = BorderStyle.None,
            CheckBoxes = true,
            ShowLines = false,
            Scrollable = true,
            Font = new Font(AreaOfPremises.DefaultFont.FontFamily, 11, FontStyle.Bold, GraphicsUnit.Pixel)
        };

        groupingParameters = new();

        FillInTheFirstColumn();
        SubscribeToEvents();

        FillInTheTestData();
    }

    public void FillInTheTestData()
    {
        for (var i = 0; i < 10; i++)
        {
            ListOfPremises.Nodes.Add(i.ToString());
            ListOfPremises.Nodes[i].Nodes.Add(i.ToString());
            ListOfPremises.Nodes[i].Nodes.Add($"{i + 1}");
        }
    }
    private void SubscribeToEvents()
    {
        ButtonHideEverything.Click += HideEverything_Click;
        ButtonRevealEverything.Click += RevealEverything_Click;
        ListOfPremises.AfterCheck += Node_AfterCheck;
        SelectAllButton.Click += SelectAll_Click; ;
        ButtonThrowOff.Click += ThrowOff_Click;
    }

    #region Обработчики событий

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
    #endregion

    private void FillInTheFirstColumn()
    {
        foreach (var dropdownList in HeadersOfGroupingControls
            .Select(title => new DropdownListForGrouping(title)))
        {
            Controls.Add(dropdownList);
            groupingParameters.Add(dropdownList);
        }

        Controls.Add(CreateContainer());
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
}