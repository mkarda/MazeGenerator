using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeGenerator
{
    class GraphicFactory
    {
        public Polyline CreatePolyline()
        {
            Polyline polyline = new Polyline
            {
                Stroke = new SolidColorBrush(Colors.GreenYellow),
                StrokeThickness = 4
            };

            return polyline;
        }

        public Rectangle CreateBorder(double height)
        {
            Rectangle r = new Rectangle
            {
                Height = height,
                Width = height,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1
            };

            return r;
        }

        public Ellipse CreateEllipse(double size, Color color, double left,  double top)
        {
            Ellipse ellipse = new Ellipse
            {
                Height = size,
                Width = size,
                Fill = new SolidColorBrush(color)
            };
            Canvas.SetLeft(ellipse, left);
            Canvas.SetTop(ellipse, top);

            return ellipse;
        }

        public Line CreateLine()
        {
            Color col = Colors.Black;

            Line line = new Line() { StrokeThickness = 1, Stroke = new SolidColorBrush(col) };

            return line;
        }
    }
}
