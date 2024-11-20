namespace AreaCalculationPlugin.View.Controls;

public class DropDownList : ComboBox
{
    public DropDownList()
    {
        Dock = DockStyle.Fill;
        Margin = new Padding(0);
        Padding = new Padding(0);

        BackColor = ColorTranslator.FromHtml("#F5F6F8");
        ForeColor = ColorTranslator.FromHtml("#515254");

        Font = new Font("Inter", 13, FontStyle.Bold, GraphicsUnit.Pixel);
        FlatStyle = FlatStyle.Flat; //Элемент плоский
    }
}
