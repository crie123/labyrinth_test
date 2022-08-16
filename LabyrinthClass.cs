using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace labyrinth
{
    public class LabyrinthClass
    {
            public readonly CellStruct[,] _cells;
            public int _width;
            public int _height;
            public Stack<CellStruct> _path = new Stack<CellStruct>();
            public List<CellStruct> _neighbours = new List<CellStruct>();
            public List<CellStruct> _visited = new List<CellStruct>();
            public List<CellStruct> _solve = new List<CellStruct>();
            public Random rnd = new Random();
            public CellStruct start;
            public CellStruct finish;

            public LabyrinthClass(int width, int height)
            {
                start = new CellStruct(1, 1, true, true);
                finish = new CellStruct(width - 3, height - 3, true, true);


                _width = width;
                _height = height;
                _cells = new CellStruct[width, height];
                for (var i = 0; i < width; i++)
                    for (var j = 0; j < height; j++)
                        if ((i % 2 != 0 && j % 2 != 0) && (i < _width - 1 && j < _height - 1))
                        {
                            _cells[i, j] = new CellStruct(i, j);
                        }
                        else
                        {

                            _cells[i, j] = new CellStruct(i, j, false, false);
                        }
                _path.Push(start);
                _cells[start.X, start.Y] = start;
            }
        public void LabyrinthCreation()
        {
            _cells[start.X, start.Y] = start;
            while (_path.Count != 0) //пока в стеке есть клетки ищем соседей и строим путь
            {
                _neighbours.Clear();
                NeighboursLook(_path.Peek());
                if (_neighbours.Count != 0)
                {
                    CellStruct nextCell = ChooseNeighbour(_neighbours);
                    RemoveWall(_path.Peek(), nextCell);
                    nextCell._isVisited = true; //делаем текущую клетку посещенной
                    _cells[nextCell.X, nextCell.Y]._isVisited = true; //и в общем массиве
                    _path.Push(nextCell); //затем добавляем её в стек
                }
                else
                {
                    _path.Pop();
                }
            }
        }
        private void NeighboursLook(CellStruct localcell)
        {
            int x = localcell.X;
            int y = localcell.Y;
            const int distance = 2;
            CellStruct[] possibleNeighbours = new[] //все соседи
            {
                new CellStruct(x, y - distance), // Up
                new CellStruct(x + distance, y), // Right
                new CellStruct(x, y + distance), // Down
                new CellStruct(x - distance, y) // Left
            };
            for (int i = 0; i < 4; i++) //все 4 направления
            {
                CellStruct curNeighbour = possibleNeighbours[i];
                if (curNeighbour.X > 0 && curNeighbour.X < _width && curNeighbour.Y > 0 && curNeighbour.Y < _height)
                {//если сосед не выходит за стенки
                    if (_cells[curNeighbour.X, curNeighbour.Y]._isCell && !_cells[curNeighbour.X, curNeighbour.Y]._isVisited)
                    { //и является клеткой и непосещен
                        _neighbours.Add(curNeighbour);
                    }//добавляем соседа в лист соседей
                }
            }
        }
        private void NeighboursSolve(CellStruct localcell)
        {
            int x = localcell.X;
            int y = localcell.Y;
            const int distance = 1;
            CellStruct[] possibleNeighbours = new[] //все соседи
            {
                new CellStruct(x, y - distance), // Up
                new CellStruct(x + distance, y), // Right
                new CellStruct(x, y + distance), // Down
                new CellStruct(x - distance, y) // Left
            };
            for (int i = 0; i < 4; i++) //все 4 направления
            {
                CellStruct curNeighbour = possibleNeighbours[i];
                if (curNeighbour.X > 0 && curNeighbour.X < _width && curNeighbour.Y > 0 && curNeighbour.Y < _height)
                {//если сосед не выходит за стенки
                    if (_cells[curNeighbour.X, curNeighbour.Y]._isCell && !_cells[curNeighbour.X, curNeighbour.Y]._isVisited)
                    { //и является клеткой и непосещен
                        _neighbours.Add(curNeighbour);
                    }//добавляем соседа в лист соседей
                }
            }
        }
        private CellStruct ChooseNeighbour(List<CellStruct> neighbours) //выбор случайного соседа
        {
            int r = rnd.Next(neighbours.Count);
            return neighbours[r];
        }
        private void RemoveWall(CellStruct first, CellStruct second)
        {
            int xDiff = second.X - first.X;
            int yDiff = second.Y - first.Y;
            int addX = (xDiff != 0) ? xDiff / Math.Abs(xDiff) : 0; //направление удаления стены
            int addY = (yDiff != 0) ? yDiff / Math.Abs(yDiff) : 0;
            //коорды удаленной стены
            _cells[first.X + addX, first.Y + addY]._isCell = true; //обращаем стену в клетку
            _cells[first.X + addX, first.Y + addY]._isVisited = true; //и делаем ее посещенной
            second._isVisited = true; //делаем клетку посещенной
            _cells[second.X, second.Y] = second;
        }
        public void SolveLabyrinth() 
        {
            bool flag = false; //достиг финиша
            foreach (CellStruct cell in _cells)
            {
                if (_cells[cell.X, cell.Y]._isCell == true)
                {
                    _cells[cell.X, cell.Y]._isVisited = false;
                }
            }
            _path.Clear();
            _path.Push(start);

            while(_path.Count != 0)
            {
                if(_path.Peek().X == finish.X && _path.Peek().Y == finish.Y)
                {
                    flag = true;
                }
                if (!flag)
                {
                    _neighbours.Clear();
                    NeighboursSolve(_path.Peek());
                    if(_neighbours.Count != 0)
                    {
                        CellStruct nextcell = ChooseNeighbour(_neighbours);
                        nextcell._isVisited = true;
                        _cells[nextcell.X, nextcell.Y]._isVisited = true;
                        _path.Push(nextcell);
                        _visited.Add(_path.Peek());
                    }
                    else
                    {
                        _path.Pop();
                    }
                }
                else
                {
                    _solve.Add(_path.Peek());
                    _path.Pop();
                }
            }
        }

    }
    
}
