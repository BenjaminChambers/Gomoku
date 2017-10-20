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
        public Coordinate(int Index)
        {
            Row = Index / 15;
            Column = Index % 15;
        }

        public int Index
        {
            get { return Row * 15 + Column; }
        }

        public Coordinate RotateCW()
            => new Coordinate(14 - Row, Column);
        public Coordinate RotateCCW()
            => new Coordinate(Row, 14 - Column);
        public Coordinate FlipVertical()
            => new Coordinate(Column, 14 - Row);
        public Coordinate FlipHorizontal()
            => new Coordinate(14 - Column, Row);

        static public Coordinate operator +(Coordinate A, Coordinate B)
        {
            return new Coordinate(A.Column + B.Column, A.Row + B.Row);
        }
        static public Coordinate operator -(Coordinate A, Coordinate B)
        {
            return new Coordinate(A.Column - B.Column, A.Row - B.Row);
        }

        public readonly int Column;
        public readonly int Row;
    }
}
