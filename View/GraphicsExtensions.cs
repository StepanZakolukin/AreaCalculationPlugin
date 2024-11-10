namespace AreaCalculationPlugin.View;

public static class GraphicsExtensions
{
    public static void CreateARoundedRectangle(this Graphics graphics, Pen pen, Size size, Point point, int radius)
    {
        for (var i = 0; i < 2; i++)
        {
            graphics.DrawLine(pen, new Point(point.X + radius, point.Y + size.Height * i),
                new Point(point.X - radius + size.Width, point.Y + size.Height * i));
        }

        for (var i = 0; i < 2; i++)
        {
            graphics.DrawLine(pen, new Point(point.X + size.Width * i, point.Y + radius),
                new Point(point.X + size.Width * i, point.Y + size.Height - radius));
        }

        graphics.DrawArc(pen, new Rectangle(point, new Size(2 * radius, 2 * radius)), 180, 90);
        graphics.DrawArc(pen, new Rectangle(new Point(point.X - 2 * radius + size.Width, point.Y),
            new Size(2 * radius, 2 * radius)), 270, 90);
        graphics.DrawArc(pen, new Rectangle(new Point(point.X - 2 * radius + size.Width, point.Y + size.Height - 2 * radius),
            new Size(2 * radius, 2 * radius)), 0, 90);
        graphics.DrawArc(pen, new Rectangle(new Point(point.X + radius - radius, point.Y + size.Height - 2 * radius),
            new Size(2 * radius, 2 * radius)), 90, 90);
    }
}
