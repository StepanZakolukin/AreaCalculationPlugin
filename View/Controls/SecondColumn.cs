﻿using AreaCalculationPlugin.Calculator;

namespace AreaCalculationPlugin.View.Controls;

internal class SecondColumn : Container
{
    private readonly TextBox NumberOfDecimalPlaces;
    private readonly List<DropDownListForSettingRoomParameters> RoomParameters = new();

    private RectangularRoundedButton ButtonSettingCoefficient = new()
    {
        Margin = new Padding(0, 0, 5, 10),
        Text = "Настройка коэффициента"
    };

    public RectangularRoundedButton ButtonCalculate = new(ColorTranslator.FromHtml("#EFE650"))
    {
        Text = "Посчитать",
        Margin = new Padding(0, 0, 5, 0),
        Enabled = false
    };

    public SecondColumn() : base()
    {
        NumberOfDecimalPlaces = new()
        {
            Dock = DockStyle.Fill,
            BackColor = ColorTranslator.FromHtml("#F5F6F8"),
            ForeColor = ColorTranslator.FromHtml("#515254"),
            Font = new Font(AreaOfPremises.DefaultFont.FontFamily, 11, FontStyle.Bold, GraphicsUnit.Pixel),
            TextAlign = HorizontalAlignment.Center,
            Text = PluginManager.ParameterCorrector.NumberOfDecimalPlaces.ToString()
        };

        ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        for (var i = 0; i < 7; i++)
            RowStyles.Add(new RowStyle(SizeType.Absolute, 49));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 68));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 49));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 23));
        RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        for (var i = 0; i < 2; i++)
            RowStyles.Add(new RowStyle(SizeType.Absolute, 38 - i * 10));

        FillSecondColumn();
        ButtonSettingCoefficient.Click += RunCoefficientSettingForm;
        NumberOfDecimalPlaces.TextChanged += UpdateRoundingRules;
    }

    private void UnlockButtonCalculate(object? sender, EventArgs args)
    {
        if (RoomParameters.All(param => param.List.SelectedItem != null) &&
            NumberOfDecimalPlaces.BackColor != Color.Red)
        {
            ButtonCalculate.Enabled = true;
        }
        else ButtonCalculate.Enabled= false;
    }

    private void UpdateRoundingRules(object? sender, EventArgs e)
    {
        if (int.TryParse(NumberOfDecimalPlaces.Text, out var number) && number >= 0)
        {
            PluginManager.ParameterCorrector.NumberOfDecimalPlaces = number;
            NumberOfDecimalPlaces.BackColor = ColorTranslator.FromHtml("#F5F6F8");
        }
        else NumberOfDecimalPlaces.BackColor = Color.Red;
    }

    private void RunCoefficientSettingForm(object? sender, EventArgs e)
    {
        var formForSettingCoefficient = new SettingСoefficient();
        formForSettingCoefficient.ShowDialog();
    }

    private void FillSecondColumn()
    {
        foreach (var cell in Enumerable.Range(0, 9)
            .Select(num => new DropDownListForSettingRoomParameters((RoomParameter)num)))
        {
            Controls.Add(cell);
            RoomParameters.Add(cell);
            cell.AddRange(RoomData.SharedParameters);
            cell.List.SelectedIndexChanged += UnlockButtonCalculate;
        }

        RoomParameters
            .Where(param => param.RoomParameter == RoomParameter.RoomType)
            .First().SelectedIndexChanged += UpdateRoomType;

        Controls.Add(CreateRoundingControls());
        Controls.Add(new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(0),
            Margin = new Padding(0)
        });
        foreach (var cell in СreateAKeypadForTheSecondColumn())
            Controls.Add(cell);
    }

    private void UpdateRoomType(object? sender, EventArgs e)
    {
        if (sender is DropDownListForSettingRoomParameters dropDownList && dropDownList.RoomParameter == RoomParameter.RoomType)
            RoomData.RoomTypeParameter = dropDownList.List.SelectedItem.ToString();
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
        cell1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 164));

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
}
