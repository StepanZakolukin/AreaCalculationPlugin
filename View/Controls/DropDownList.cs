namespace AreaCalculationPlugin.View.Controls;

public class DropdownList : Container
{
    protected readonly Heading title;
    public string Title { get { return title.Text; } }
    
    private ComboBox Items { get; set; }

    public DropdownList(string name)
    {
        title = new Heading
        {
            Text = name,
            Margin = new Padding(0, 0, 0, 4)
        };
        Items = new ComboBox();
        ConfigureList(Items);


        ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 20));

        Controls.Add(title);
        Controls.Add(Items);
    }

    private void ConfigureList(ComboBox comboBox)
    {
        comboBox.Dock = DockStyle.Fill;
        comboBox.Margin = new Padding(0);
        comboBox.Padding = new Padding(0);
        comboBox.BackColor = ColorTranslator.FromHtml("#F5F6F8");
        comboBox.ForeColor = ColorTranslator.FromHtml("#515254");
        comboBox.Font = new Font("Inter", 13, FontStyle.Bold, GraphicsUnit.Pixel);
        comboBox.FlatStyle = FlatStyle.Flat; //Элемент плоский
    }

    public void Add(Control control)
    {
        Items.Items.Add(control);
    }
}
