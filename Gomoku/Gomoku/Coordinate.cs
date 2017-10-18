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

        public int Index
        {
            get { return Row * 15 + Column; }
        }

        static public Coordinate operator+(Coordinate A, Coordinate B)
        {
            return new Coordinate(A.Column + B.Column, A.Row + B.Row);
        }
        static public Coordinate operator-(Coordinate A, Coordinate B)
        {
            return new Coordinate(A.Column - B.Column, A.Row - B.Row);
        }

        public readonly int Column;
        public readonly int Row;
    }
}
