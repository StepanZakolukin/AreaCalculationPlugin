namespace AreaCalculationPlugin.View.Extensions;

public static class GraphicsExtensions
{
    public static void FillRoundedRectangle(this Graphics graphics, Color backColor, Color borderColor,
        int borderSize, Rectangle rectangle, int radius)
    {
        graphics.FillRoundedRectangle(brush: new SolidBrush(backColor),
            new Rectangle(borderSize, borderSize, rectangle.Size.Width - 2 * borderSize,
            rectangle.Size.Height - 2 * borderSize),
            radius: radius);

        graphics.RoundedRectangle(pen: new Pen(borderColor, borderSize),
            new Rectangle(borderSize, borderSize, rectangle.Size.Width - 2 * borderSize,
            rectangle.Size.Height - 2 * borderSize),
            radius: radius);
    }

    public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle rectangle, int radius)
    {
        graphics.DrawCornersForFillRoundedRectangle(brush, radius, rectangle);

        graphics.FillRectangle(brush,
            new Rectangle(
                new Point(
                    x: rectangle.Location.X + radius,
                    y: rectangle.Location.Y),
                new Size(
                    width: rectangle.Size.Width - 2 * radius,
                    height: rectangle.Size.Height)));

        graphics.FillRectangle(brush,
            new Rectangle(
                new Point(
                    x: rectangle.Location.X,
                    y: rectangle.Location.Y + radius),
                new Size(
                    width: rectangle.Size.Width,
                    height: rectangle.Size.Height - 2 * radius)));
    }

    private static void DrawCornersForFillRoundedRectangle(
        this Graphics graphics, Brush brush,
        int radius, Rectangle rectangle)
    {
        var cornerRectangles = CreateSetOfRectanglesToRoundCorners(radius * 2, rectangle);

        foreach (var rect in cornerRectangles)
            graphics.FillEllipse(brush, rect);
    }

    public static void RoundedRectangle(this Graphics graphics, Pen pen, Rectangle rectangle, int radius)
    {
        // рисует верхнюю и нижнюю сторону прямоугольника
        for (var i = 0; i < 2; i++)
        {
            var y = rectangle.Location.Y + rectangle.Size.Height * i;

            graphics.DrawLine(pen,
                new Point(rectangle.Location.X + radius, y),
                new Point(rectangle.Location.X - radius + rectangle.Size.Width, y));
        }

        // рисует левую и правую сторону прямоугольника
        for (var i = 0; i < 2; i++)
        {
            var x = rectangle.Location.X + rectangle.Size.Width * i;

            graphics.DrawLine(pen,
                new Point(x, rectangle.Location.Y + radius),
                new Point(x, rectangle.Location.Y + rectangle.Size.Height - radius));
        }

        graphics.DrawRoundedCorners(pen, rectangle, radius);
    }

    private static void DrawRoundedCorners(this Graphics graphics, Pen pen, Rectangle rectangle, int radius)
    {
        var diameterOfCircle = 2 * radius;
        var cornerRectangles = CreateSetOfRectanglesToRoundCorners(diameterOfCircle, rectangle);

        for (var i = 0; i < 4; i++)
            graphics.DrawArc(pen, cornerRectangles[i], i * 90 - 2, 94);
    }

    private static List<Rectangle> CreateSetOfRectanglesToRoundCorners(int diameterOfCircle, Rectangle rectangle)
    {
        var sizeOfBoundingRect = new Size(diameterOfCircle, diameterOfCircle);
        var YValueForRightSide = rectangle.Location.Y + rectangle.Size.Height - diameterOfCircle;
        var XValueForBottomSide = rectangle.Location.X - diameterOfCircle + rectangle.Size.Width;

        return
        [
            new Rectangle(new Point(XValueForBottomSide, YValueForRightSide), sizeOfBoundingRect),
            new Rectangle(new Point(rectangle.Location.X, YValueForRightSide), sizeOfBoundingRect),
            new Rectangle(rectangle.Location, sizeOfBoundingRect),
            new Rectangle(new Point(XValueForBottomSide, rectangle.Location.Y), sizeOfBoundingRect),
        ];
        throw new NotImplementedException();
    }
}
