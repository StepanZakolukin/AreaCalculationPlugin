using AreaCalculationPlugin.Calculator;
using AreaCalculationPlugin.View.Extensions;
using Teigha.DatabaseServices;
using static Multicad.DatabaseServices.McDbObject;

namespace AreaCalculationPlugin.View.Controls;

internal class FirstColumn : Container
{
    public TreeView ListOfPremises { get; private set; }
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
            Font = new Font(AreaOfPremises.DefaultFont.FontFamily, 11, FontStyle.Bold, GraphicsUnit.Pixel),
            ItemHeight = 21
        };

        groupingParameters = new();
        GroupByParameters(Array.Empty<string>());

        FillInTheFirstColumn();
        SubscribeToEvents();
    }

    public void UpdateTreeView(object? sender, EventArgs args)
    {
        var list = groupingParameters
            .Where(param => param.List.SelectedIndex != -1)
            .Select(parameter => parameter.List.SelectedItem.ToString())
            .ToArray();

        GroupByParameters(list);
    }

    public void GroupByParameters(string[] parameters)
    {
        ListOfPremises.Nodes.Clear();

        if (parameters.Length > 0)
            FillInWithGrouping(parameters);
        else
            FillInWithoutGrouping();
    }

    private void FillInWithoutGrouping()
    {
        foreach (var room in PluginManager.Rooms)
            ListOfPremises.AddNode(room.GetParameterValue("AEC_ROOM_NAME"), room);
    }

    private void FillInWithGrouping(string[] parameters)
    {
        var rootDict = new Dictionary<string, TreeNode>();

        foreach (var room in PluginManager.Rooms)
        {
            if (!rootDict.ContainsKey(room.GetParameterValue(parameters[0])))
            {
                var paramValue = room.GetParameterValue(parameters[0]);
                rootDict[paramValue] = new TreeNode(paramValue);
                ListOfPremises.Nodes.Add(rootDict[paramValue]);
            }

            var node = rootDict[room.GetParameterValue(parameters[0])];

            for (int i = 1; i < parameters.Length; i++)
            {
                node.Tag ??= new Dictionary<string, TreeNode>();
                var dict = (Dictionary<string, TreeNode>)node.Tag;

                if (!dict.ContainsKey(room.GetParameterValue(parameters[i])))
                {
                    dict[room.GetParameterValue(parameters[i])] = new TreeNode(room.GetParameterValue(parameters[i]));
                    node.Nodes.Add(dict[room.GetParameterValue(parameters[i])]);
                }

                node = dict[room.GetParameterValue(parameters[i])];
            }

            ListOfPremises.AddNode(room.GetParameterValue("AEC_ROOM_NAME"), room);
        }
    }

    public HashSet<RoomData> GetCheckedNodes(TreeNodeCollection nodes)
    {
        var result = new HashSet<RoomData>();

        foreach (TreeNode node in nodes)
        {
            if (node.Checked)
            {
                if (node.Tag is RoomData roomData)
                    result.Add(roomData);

                if (node.Nodes.Count > 0)
                    result.UnionWith(GetCheckedNodes(node.Nodes));
            }
        }

        return result;
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

    private void Node_AfterCheck(object? sender, TreeViewEventArgs e)
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
            dropdownList.AddRange(RoomData.SharedParameters);
            dropdownList.List.SelectedIndexChanged += UpdateTreeView;
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