using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku
{
    public class Board
    {
        public Board()
        {
            State = BoardState.InProgress;
        }
        public Board(Board Source)
        {
            Array.Copy(Source.data, data, data.Length);
            State = Source.State;
        }

        public Board Put(int Column, int Row, Stone Color)
        {
            if (Color == Stone.Empty)
                throw new ArgumentException("Stone cannot be Empty");

            var result = new Board(this);
            result.data[Column, Row] = Color;
            result.checkState(Column, Row);
            return result;
        }

        public Stone this[int Column, int Row]
        {
            get
            {
                return data[Column, Row];
            }
        }

        public BoardState State
        {
            get;
            private set;
        }

        private void checkState(int Column, int Row)
        {
            var which = this[Column, Row];

            int d19 = crawl(Column, Row, which, -1, -1) + crawl(Column, Row, which, 1, 1);
            int d28 = crawl(Column, Row, which, 0, -1) + crawl(Column, Row, which, 0, 1);
            int d37 = crawl(Column, Row, which, -1, 1) + crawl(Column, Row, which, 1, -1);
            int d46 = crawl(Column, Row, which, -1, 0) + crawl(Column, Row, which, 1, 0);

            if ((d19>5) || (d28>5) || (d37>5) || (d46>5))
            {
                switch (which)
                {
                    case Stone.Black: State = BoardState.BlackWins; break;
                    case Stone.White: State = BoardState.WhiteWins; break;
                }
            }
        }

        private int crawl(int column, int row, Stone color, int dc, int dr)
        {
            int result = 0;
            
            while ((column >=0) && (column<15) && (row>=0) && (row<15) && (this[column,row]==color))
            {
                column += dc;
                row += dr;
                result++;
            }

            return result;
        }

        private Stone[,] data = new Stone[15, 15];
    }
}
