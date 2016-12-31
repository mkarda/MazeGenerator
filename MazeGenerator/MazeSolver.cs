using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class MazeSolver
    {
        public MazeSolver()
        {

        }


        private int _height;
        private List<List<Cell>> _maze;
        private List<int[]> _solutionPath;

        public List<int[]> SolveMaze(List<List<Cell>> maze)
        {
            _height = maze.Count;
            _maze = maze;
            _solutionPath = new List<int[]> { new[] { 0, 0 } };
            doNextStep = false;
            finished = false;
            WalkTheMaze(0, 0);
            return _solutionPath;
        }

        private bool doNextStep = false;
        private bool finished = false;

        private void WalkTheMaze(int x, int y)
        {
            Cell startCell = _maze[x][y];
            startCell.Visited = false;

            List<int[]> neighbours = new List<int[]>() { new[] { x - 1, y }, new[] { x + 1, y }, new[] { x, y - 1 }, new[] { x, y + 1 } };


            foreach (int[] neighbour in neighbours)
            {
                if (finished)
                {
                    break;
                }

                if (neighbour[0] >= _height || neighbour[1] >= _height || neighbour[0] < 0 || neighbour[1] < 0 ||
                _maze[neighbour[0]][neighbour[1]].Visited == false) continue;


                Cell neigbourCell = _maze[neighbour[0]][neighbour[1]];

                if (neighbour[0] < x && neigbourCell.BottomWall == 0)
                {
                    _solutionPath.Add(new[] { neighbour[0], neighbour[1] });
                    doNextStep = true;
                }

                if (neighbour[0] > x && startCell.BottomWall == 0)
                {
                    _solutionPath.Add(new[] { neighbour[0], neighbour[1] });
                    doNextStep = true;
                }

                if (neighbour[1] > y && startCell.RightWall == 0)
                {
                    _solutionPath.Add(new[] { neighbour[0], neighbour[1] });
                    doNextStep = true;
                }

                if (neighbour[1] < y && neigbourCell.RightWall == 0)
                {
                    _solutionPath.Add(new[] { neighbour[0], neighbour[1] });
                    doNextStep = true;
                }

                if (neighbour[0] == _height - 1 && neighbour[1] == _height - 1 && doNextStep == true)
                {
                    finished = true;
                }

                if (doNextStep == true)
                {



                    doNextStep = false;
                    WalkTheMaze(neighbour[0], neighbour[1]);
                    if (finished == false)
                    {
                        _solutionPath.RemoveAt(_solutionPath.Count - 1);
                    }
                }

            }
        }
    }
}
