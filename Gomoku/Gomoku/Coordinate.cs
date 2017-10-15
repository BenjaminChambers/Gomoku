using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku
{
    public struct Coordinate
    {
        public Coordinate(int Column, int Row)
        {
            this.Column = Column;
            this.Row = Row;
        }
        public int Column;
        public int Row;
    }
}
