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

        public MainWindow()
        {
            InitializeComponent();
            _mazeGenerator = new Generator();
            _mazeSolver = new MazeSolver();
            _graphicFactory = new GraphicFactory();


            _mainCanvas = MainCanvas;
        }

        private void DoIt(int size)
        {
            MainCanvas.Children.Clear();

            _maze = _mazeGenerator.GenerateMaze(size);


            var mainCanvasHeight = MainCanvas.Height;

            _cellsize = mainCanvasHeight / _maze.Count;


            AddBorder(mainCanvasHeight);

            DrawTheMAze();

            CreateStartAndStop(_cellsize, mainCanvasHeight);
            SolveButton.IsEnabled = true;

        }

        private void DrawTheMAze()
        {
            for (int i = 0; i < _maze.Count; i++)
            {
                for (int j = 0; j < _maze[i].Count; j++)
                {
                    Cell cell = _maze[i][j];

                    if (cell.RightWall == 1)
                        DrawRightWall(_cellsize, i, j);

                    if (cell.BottomWall == 1)
                        DrawBottomLine(_cellsize, i, j);
                }
            }
        }

        private void CreateStartAndStop(double cellsize, double mainCanvasHeight)
        {
            Ellipse startEl = _graphicFactory.CreateEllipse(cellsize, Colors.OrangeRed, 0, 0);
            MainCanvas.Children.Add(startEl);
            
            Ellipse stopEl = _graphicFactory.CreateEllipse(cellsize, Colors.CornflowerBlue, mainCanvasHeight - cellsize, mainCanvasHeight - cellsize);
            MainCanvas.Children.Add(stopEl);
        }

        private void DrawBottomLine(double cellsize, int j, int i)
        {
            Line line = _graphicFactory.CreateLine();

            line.Y1 = cellsize + j * cellsize;
            line.Y2 = cellsize + j * cellsize;
            line.X1 = i * cellsize;
            line.X2 = cellsize + i * cellsize;
            _mainCanvas.Children.Add(line);
        }

        private void DrawRightWall(double cellsize, int i, int j)
        {
            Line line = _graphicFactory.CreateLine();

            line.X1 = cellsize + j * cellsize;
            line.X2 = cellsize + j * cellsize;
            line.Y1 = i * cellsize;
            line.Y2 = cellsize + i * cellsize;
            _mainCanvas.Children.Add(line);
        }

        private void AddBorder(double mainCanvasHeight)
        {
            Rectangle rect = _graphicFactory.CreateBorder(mainCanvasHeight);
            MainCanvas.Children.Add(rect);
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
