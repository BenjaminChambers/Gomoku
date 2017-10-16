using System;
using System.Collections.Generic;
using System.Linq;
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

        public Board Shift(Coordinate Delta)
        {
            var result = new Board();

            for (int c = Math.Max(0, -Delta.Column); c < Math.Min(15, 15 - Delta.Column); c++)
            {
                for (int r = Math.Max(0, -Delta.Row); r < Math.Min(15, 15 - Delta.Row); r++)
                {
                    result.data[c + Delta.Column, r + Delta.Row] = this[c, r];
                }
            }

            return result;
        }

        public (Coordinate Minimum, Coordinate Maximum) GetBounds()
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

            return (new Coordinate(minC, minR), new Coordinate(maxC, maxR));
        }

        public Board Put(Coordinate Location, Stone Color)
        {
            if (Color == Stone.Empty)
                throw new ArgumentException("Stone cannot be Empty");

            var result = new Board(this);
            result.data[Location.Column, Location.Row] = Color;
            result.CheckState(Location.Column, Location.Row);
            return result;
        }
        public Board Put(IEnumerable<(Coordinate Where, Stone Color)> Stones)
        {
            if (Stones.Where(x => x.Color == Stone.Empty).Any())
                throw new ArgumentException("Stone cannot be Empty");

            var result = new Board(this);
            foreach (var item in Stones)
            {
                result.data[item.Where.Column, item.Where.Row] = item.Color;
                result.CheckState(item.Where.Column, item.Where.Row);
            }
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

            int d19 = Crawl(new Coordinate(Column, Row), new Coordinate(-1, -1), which) + Crawl(new Coordinate(Column, Row), new Coordinate(1, 1), which);
            int d28 = Crawl(new Coordinate(Column, Row), new Coordinate(0, -1), which) + Crawl(new Coordinate(Column, Row), new Coordinate(0, 1), which);
            int d37 = Crawl(new Coordinate(Column, Row), new Coordinate(-1, 1), which) + Crawl(new Coordinate(Column, Row), new Coordinate(1, -1), which);
            int d46 = Crawl(new Coordinate(Column, Row), new Coordinate(-1, 0), which) + Crawl(new Coordinate(Column, Row), new Coordinate(1, 0), which);

            if ((d19 > 5) || (d28 > 5) || (d37 > 5) || (d46 > 5))
            {
                switch (which)
                {
                    case Stone.Black: State = BoardState.BlackWins; break;
                    case Stone.White: State = BoardState.WhiteWins; break;
                }
            }
        }

        private int Crawl(Coordinate Location, Coordinate Delta, Stone color)
        {
            int result = 0;

            while ((Location.Column >= 0) && (Location.Column < 15) && (Location.Row >= 0) && (Location.Row < 15) && (this[Location.Column, Location.Row] == color))
            {
                Location += Delta;
                result++;
            }

            return result;
        }

        private Stone[,] data = new Stone[15, 15];
    }
}
