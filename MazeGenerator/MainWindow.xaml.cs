using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeGenerator
{
    public partial class MainWindow : Window
    {
        private static Canvas _mainCanvas;
        private readonly Generator _mazeGenerator;
        private readonly MazeSolver _mazeSolver;
        private List<List<Cell>> _maze;
        private double _cellsize;
        private readonly GraphicFactory _graphicFactory;
        private readonly MazeDrawer _mazeDrawer;

        public MainWindow()
        {
            InitializeComponent();
            _mainCanvas = MainCanvas;
            _mazeGenerator = new Generator();
            _mazeSolver = new MazeSolver();
            _graphicFactory = new GraphicFactory();
            _mazeDrawer = new MazeDrawer(_mainCanvas, _graphicFactory);
        }

        private void DoIt(int size)
        {
            MainCanvas.Children.Clear();

            _maze = _mazeGenerator.GenerateMaze(size);

            _cellsize = MainCanvas.Height / _maze.Count;
            _mazeDrawer.DrawTheMaze(_maze);

            SolveButton.IsEnabled = true;

        }

        private void CreateStartAndStop(double cellsize, double mainCanvasHeight)
        {
            Ellipse startEl = _graphicFactory.CreateEllipse(cellsize, Colors.OrangeRed, 0, 0);
            MainCanvas.Children.Add(startEl);
            
            Ellipse stopEl = _graphicFactory.CreateEllipse(cellsize, Colors.CornflowerBlue, mainCanvasHeight - cellsize, mainCanvasHeight - cellsize);
            MainCanvas.Children.Add(stopEl);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DoIt(int.Parse(SizeTextBox.Text));
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            List<int[]> solution = _mazeSolver.SolveMaze(_maze);

            Polyline polyline = _graphicFactory.CreatePolyline();
            foreach (int[] step in solution)
            {
                double x = (step[1] * _cellsize) + _cellsize / 2;
                double y = (step[0] * _cellsize) + _cellsize / 2;

                polyline.Points.Add(new Point(x, y));
            }
            _mainCanvas.Children.Add(polyline);
        }
    }


    public class Cell
    {
        public Cell(bool visited, int wall, int bottomWall)
        {
            Visited = visited;
            RightWall = wall;
            BottomWall = bottomWall;
        }

        public bool Visited { get; set; }
        public int RightWall { get; set; }
        public int BottomWall { get; set; }
    }
}
