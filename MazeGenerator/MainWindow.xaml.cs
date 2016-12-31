using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Canvas _mainCanvas;
        private readonly Generator _generator;
        private readonly MazeSolver _mazeSolver;
        private List<List<Cell>> _maze;
        private double _cellsize;

        public MainWindow()
        {
            InitializeComponent();
            _mazeSolver = new MazeSolver();


            

            _mainCanvas = MainCanvas;

            _generator = new Generator();


        }

        private void DoIt(int size)
        {
            MainCanvas.Children.Clear();

            _maze = _generator.GenerateMaze(size);


            var mainCanvasHeight = MainCanvas.Height;

            _cellsize = mainCanvasHeight / _maze.Count;


            AddBorder(mainCanvasHeight);

            for (int i = 0; i < _maze.Count; i++)
            {
                for (int j = 0; j < _maze[i].Count; j++)
                {
                    Cell c = _maze[i][j];

                    if (c.RightWall == 1)
                        DrawRightWall(_cellsize, i, j);

                    if (c.BottomWall == 1)
                        DrawBottomLine(_cellsize, i, j);
                }
            }

            CreateStartAndStop(_cellsize, mainCanvasHeight);
            SolveButton.IsEnabled = true;

        }

        private void CreateStartAndStop(double cellsize, double mainCanvasHeight)
        {
            Ellipse startEl = new Ellipse();
            startEl.Height = cellsize;
            startEl.Width = cellsize;
            startEl.Fill = new SolidColorBrush(Colors.OrangeRed);
            Canvas.SetLeft(startEl, 0);
            Canvas.SetTop(startEl, 0);

            Ellipse stopEl = new Ellipse();
            stopEl.Height = cellsize;
            stopEl.Width = cellsize;
            stopEl.Fill = new SolidColorBrush(Colors.CornflowerBlue);
            Canvas.SetLeft(stopEl, mainCanvasHeight-cellsize);
            Canvas.SetTop(stopEl, mainCanvasHeight - cellsize);

            MainCanvas.Children.Add(startEl);
            MainCanvas.Children.Add(stopEl);
        }

        private static void DrawBottomLine(double cellsize, int j, int i)
        {
            Color col = Colors.Black;

            Line line = new Line() {StrokeThickness = 1, Stroke = new SolidColorBrush(col)};
            line.Y1 = cellsize + j*cellsize;
            line.Y2 = cellsize + j*cellsize;
            line.X1 = i*cellsize;
            line.X2 = cellsize + i*cellsize;
            _mainCanvas.Children.Add(line);
        }

        private static void DrawRightWall(double cellsize, int i, int j)
        {
            Color col = Colors.Black;

            Line line = new Line() { StrokeThickness = 1, Stroke = new SolidColorBrush(col) };
            line.X1 = cellsize + j * cellsize;
            line.X2 = cellsize + j * cellsize;
            line.Y1 = i * cellsize;
            line.Y2 = cellsize + i * cellsize;
            _mainCanvas.Children.Add(line);
        }

        private void AddBorder(double mainCanvasHeight)
        {
            Rectangle r = new Rectangle();
            r.Height = mainCanvasHeight;
            r.Width = mainCanvasHeight;
            r.Stroke = new SolidColorBrush(Colors.Black);
            r.StrokeThickness = 1;
            MainCanvas.Children.Add(r);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DoIt(int.Parse(SizeTextBox.Text));
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            List<int[]> solution = _mazeSolver.SolveMaze(_maze);
            string sol = "";
            foreach (var intse in solution)
            {
                sol += $"  {intse[0]},{intse[1]}  ";
            }


//            List<int[]> solution = new List<int[]>() {new []{0,0}, new []{1,0}, new []{2,0}, new []{2,1}};

            Polyline polyline = new Polyline();
            polyline.Stroke = new SolidColorBrush(Colors.GreenYellow);
            polyline.StrokeThickness = 4;

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
