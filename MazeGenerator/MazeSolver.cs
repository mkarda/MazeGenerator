using System.Collections.Generic;

namespace MazeGenerator
{
    public class MazeSolver
    {
        private int _height;
        private List<List<Cell>> _maze;
        private List<int[]> _solutionPath;

        public List<int[]> SolveMaze(List<List<Cell>> maze)
        {
            _height = maze.Count;
            _maze = maze;
            _solutionPath = new List<int[]> { new[] { 0, 0 } };
            _doNextStep = false;
            _finished = false;
            WalkTheMaze(0, 0);
            return _solutionPath;
        }

        private bool _doNextStep;
        private bool _finished;

        private void WalkTheMaze(int x, int y)
        {
            Cell startCell = _maze[x][y];
            startCell.Visited = false;

            List<int[]> neighbours = new List<int[]> { new[] { x - 1, y }, new[] { x + 1, y }, new[] { x, y - 1 }, new[] { x, y + 1 } };


            foreach (int[] neighbour in neighbours)
            {
                if (_finished) break;

                if (neighbour[0] >= _height || neighbour[1] >= _height || neighbour[0] < 0 || neighbour[1] < 0 ||
                _maze[neighbour[0]][neighbour[1]].Visited == false) continue;


                Cell neigbourCell = _maze[neighbour[0]][neighbour[1]];

                if (neighbour[0] < x && neigbourCell.BottomWall == 0)
                    AddPointtoSolution(neighbour);

                if (neighbour[0] > x && startCell.BottomWall == 0)
                    AddPointtoSolution(neighbour);

                if (neighbour[1] > y && startCell.RightWall == 0)
                    AddPointtoSolution(neighbour);

                if (neighbour[1] < y && neigbourCell.RightWall == 0)
                    AddPointtoSolution(neighbour);


                if (neighbour[0] == _height - 1 && neighbour[1] == _height - 1 && _doNextStep)
                {
                    _finished = true;
                }

                if (!_doNextStep) continue;

                _doNextStep = false;
                WalkTheMaze(neighbour[0], neighbour[1]);
                if (_finished == false)
                {
                    _solutionPath.RemoveAt(_solutionPath.Count - 1);
                }
            }
        }

        private void AddPointtoSolution(int[] neighbour)
        {
            _solutionPath.Add(new[] { neighbour[0], neighbour[1] });
            _doNextStep = true;
        }
    }
}
