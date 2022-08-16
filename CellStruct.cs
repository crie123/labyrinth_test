using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    public struct CellStruct
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool _isCell { get; set; }
        public bool _isVisited { get; set; }

        public CellStruct(int x, int y, bool isCell = true, bool isVisited = false)
        {
            X = x;
            Y = y;
            _isCell = isCell;
            _isVisited = isVisited;
        }             
    }

