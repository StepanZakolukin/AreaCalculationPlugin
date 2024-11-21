namespace AreaCalculationPlugin.View;

public static class GraphicsExtensions
{
    public static void CreateRoundedRectangle(this Graphics graphics, Pen pen, Rectangle rectangle, int radius)
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

        DrawRoundedCorners(graphics, pen, rectangle, radius);
    }

    private static void DrawRoundedCorners(Graphics graphics, Pen pen, Rectangle rectangle, int radius)
    {
        var diameterOfCircle = 2 * radius;
        var rectangles = CreateSetOfRectanglesToRoundCorners(diameterOfCircle, rectangle);
        
        for (var i = 0; i < 4; i++)
            graphics.DrawArc(pen, rectangles[i], i * 90, 94);
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
