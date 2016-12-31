﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    class Generator
    {
        private List<List<Cell>> _maze;
        private Random _random;
        private int _height;
        public List<List<Cell>> GenerateMaze(int height)
        {
            _random = new Random();
            _height = height;


            _maze = GenerateDefaultMaze(_height);


            int startX = _random.Next(_height);
            int startY = _random.Next(_height);


            WalkMaze(startX, startY);

            return _maze;
        }

        private void WalkMaze(int x, int y)
        {
            Cell startCell = _maze[x][y];
            startCell.Visited = true;


                List<int[]> neighbours = new List<int[]>() { new[] { x - 1, y }, new[] { x + 1, y }, new[] { x, y - 1 }, new[] { x, y + 1 } };
            neighbours = Shuffle(neighbours);

            foreach (int[] neighbour in neighbours)
            {
                if (neighbour[0] >= _height || neighbour[1] >= _height || neighbour[0] < 0 || neighbour[1] < 0 ||
                    _maze[neighbour[0]][neighbour[1]].Visited) continue;

                Cell neigbourCell = _maze[neighbour[0]][neighbour[1]];

                if (neighbour[0] < x) // w gore
                    neigbourCell.BottomWall = 0;

                if (neighbour[0] > x) // w dol
                    startCell.BottomWall = 0;

                if (neighbour[1] > y)  // w prawo
                    startCell.RightWall = 0;

                if (neighbour[1] < y) // w lewo
                    neigbourCell.RightWall = 0;

                WalkMaze(neighbour[0], neighbour[1]);
            }
        }

        private List<int[]> Shuffle(List<int[]> list)
        {
            if (list.Count <= 1) return list;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                int[] tmp = list[i];
                int randomIndex = _random.Next(i + 1);

                list[i] = list[randomIndex];
                list[randomIndex] = tmp;
            }
            return list;
        }


        private static List<List<Cell>> GenerateDefaultMaze(int height)
        {
            List<List<Cell>> maze = new List<List<Cell>>();
            for (int i = 0; i < height; i++)
            {
                List<Cell> row = new List<Cell>();
                for (int j = 0; j < height; j++)
                {
                    Cell cell = new Cell(false, 1, 1);
                    row.Add(cell);
                }
                maze.Add(row);
            }
            return maze;
        }
    }
}
