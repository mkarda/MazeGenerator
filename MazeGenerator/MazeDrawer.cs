using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeGenerator
{
    class MazeDrawer
    {
        readonly Canvas _mainCanvas;
        private readonly GraphicFactory _graphicFactory;
        private double _cellsize;


        public MazeDrawer(Canvas mainCanvas, GraphicFactory graphicFactory)
        {
            _mainCanvas = mainCanvas;
            _graphicFactory = graphicFactory;
        }


        public void DrawTheMaze(List<List<Cell>> maze)
        {
            var mainCanvasHeight = _mainCanvas.Height;
            _cellsize = mainCanvasHeight / maze.Count;

            AddBorder(mainCanvasHeight);

            for (int i = 0; i < maze.Count; i++)
            {
                for (int j = 0; j < maze[i].Count; j++)
                {
                    Cell cell = maze[i][j];

                    if (cell.RightWall == 1)
                        DrawRightWall(_cellsize, i, j);

                    if (cell.BottomWall == 1)
                        DrawBottomLine(_cellsize, i, j);
                }
            }
            CreateStartAndStop(_cellsize, mainCanvasHeight);
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
            _mainCanvas.Children.Add(rect);
        }

        private void CreateStartAndStop(double cellsize, double mainCanvasHeight)
        {
            Ellipse startEl = _graphicFactory.CreateEllipse(cellsize, Colors.OrangeRed, 0, 0);
            _mainCanvas.Children.Add(startEl);

            Ellipse stopEl = _graphicFactory.CreateEllipse(cellsize, Colors.CornflowerBlue, mainCanvasHeight - cellsize, mainCanvasHeight - cellsize);
            _mainCanvas.Children.Add(stopEl);
        }

    }
}
