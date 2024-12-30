namespace AreaCalculationPlugin.View.Controls;

public class DropdownList : Container
{
    protected readonly Heading title;
    public string Title { get { return title.Text; } }
    
    public ComboBox List { get; set; }

    public DropdownList(string name) : base(Color.White)
    {
        title = new Heading
        {
            Text = name,
            //Margin = new Padding(0, 0, 0, 4)
        };
        List = new ComboBox();
        ConfigureList(List);

        ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        RowStyles.Add(new RowStyle(SizeType.Absolute, 20));

        Controls.Add(title);
        Controls.Add(List);
    }

    private void ConfigureList(ComboBox comboBox)
    {
        comboBox.Dock = DockStyle.Fill;
        comboBox.Margin = new Padding(0);
        comboBox.Padding = new Padding(0);
        comboBox.BackColor = ColorTranslator.FromHtml("#F5F6F8");
        comboBox.ForeColor = ColorTranslator.FromHtml("#515254");
        comboBox.Font = new Font(AreaOfPremises.DefaultFont.FontFamily, 13, FontStyle.Bold, GraphicsUnit.Pixel);
        comboBox.FlatStyle = FlatStyle.Flat;
    }

    public void Add(string listItem)
    {
        List.Items.Add(listItem);
    }

    public void AddRange(IEnumerable<string> listItems)
    {
        foreach (var item in listItems)
            List.Items.Add(item);
    }
}
