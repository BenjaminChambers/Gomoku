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

        public Board Shift(int dX, int dY)
        {
            var result = new Board();

            for (int c=Math.Max(0, -dX); c<Math.Min(15,15-dX); c++)
            {
                for (int r=Math.Max(0,-dY); r<Math.Min(15,15-dY); r++)
                {
                    result.data[c+dX, r+dY] = this[c, r];
                }
            }

            return result;
        }

        public (int MinC, int MaxC, int MinR, int MaxR) GetWhiteSpace()
        {
            int minC = 15, maxC = -1;
            int minR = 15, maxR = -1;

            for (int c = 0; c < 15; c++)
            {
                for (int r = 0; r < 15; r++)
                {
                    if (this[c, r] != Stone.Empty)
                    {
                        minC = Math.Min(c, minC);
                        maxC = Math.Max(c, maxC);
                        minR = Math.Min(r, minR);
                        maxR = Math.Max(r, maxR);
                    }
                }
            }

            return (minC, maxC, minR, maxR);
        }

        public Board Put(int Column, int Row, Stone Color)
        {
            if (Color == Stone.Empty)
                throw new ArgumentException("Stone cannot be Empty");

            var result = new Board(this);
            result.data[Column, Row] = Color;
            result.CheckState(Column, Row);
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

        private void CheckState(int Column, int Row)
        {
            var which = this[Column, Row];

            int d19 = Crawl(Column, Row, which, -1, -1) + Crawl(Column, Row, which, 1, 1);
            int d28 = Crawl(Column, Row, which, 0, -1) + Crawl(Column, Row, which, 0, 1);
            int d37 = Crawl(Column, Row, which, -1, 1) + Crawl(Column, Row, which, 1, -1);
            int d46 = Crawl(Column, Row, which, -1, 0) + Crawl(Column, Row, which, 1, 0);

            if ((d19>5) || (d28>5) || (d37>5) || (d46>5))
            {
                switch (which)
                {
                    case Stone.Black: State = BoardState.BlackWins; break;
                    case Stone.White: State = BoardState.WhiteWins; break;
                }
            }
        }

        private int Crawl(int column, int row, Stone color, int dc, int dr)
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
