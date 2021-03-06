﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gomoku
{
    public class Board : IEnumerable<Stone>
    {
        #region Constructors
        public Board()
        {
            State = BoardState.InProgress;
        }
        public Board(Board Source)
        {
            Array.Copy(Source.data, data, data.Length);
            State = Source.State;
        }
        #endregion

        #region Board modification
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

        public Board FlipHorizontal()
        {
            var result = new Board();

            for (int c = 0; c < 15; c++)
            {
                for (int r = 0; r < 15; r++)
                {
                    result.data[c, r] = data[14 - c, r];
                }
            }

            return result;
        }

        public Board FlipVertical()
        {
            var result = new Board();

            for (int c = 0; c < 15; c++)
            {
                for (int r = 0; r < 15; r++)
                {
                    result.data[c, r] = data[c, 14-r];
                }
            }

            return result;
        }

        public Board RotateCounterClockwise()
        {
            var result = new Board();

            for (int c=0; c<15; c++)
            {
                for (int r=0; r<15; r++)
                {
                    var dst = new Coordinate(c, r).RotateCCW();
                    result.data[dst.Column, dst.Row] = this[c, r];
                }
            }

            return result;
        }

        public Board RotateClockwise()
        {
            var result = new Board();

            for (int c=0; c<15; c++)
            {
                for (int r=0; r<15; r++)
                {
                    var dst = new Coordinate(c, r).RotateCW();
                    result.data[dst.Column, dst.Row] = this[c, r];
                }
            }

            return result;
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
        #endregion

        #region Board Information
        public Stone this[int Column, int Row]
        {
            get
            {
                return data[Column, Row];
            }
        }
        public Stone this[Coordinate Where]
        {
            get
            {
                return data[Where.Column, Where.Row];
            }
        }

        public BoardState State
        {
            get;
            private set;
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

        public List<Coordinate> GetThreats(Stone Aggressor)
        {
            var result = new List<Coordinate>();

            for (int c=0; c<15; c++)
            {
                for (int r=0; r<15; r++)
                {
                    if (this[c,r] == Stone.Empty)
                    {
                        int d19 = Crawl(new Coordinate(c, r), new Coordinate(-1, -1), Aggressor) + Crawl(new Coordinate(c, r), new Coordinate(1, 1), Aggressor);
                        int d28 = Crawl(new Coordinate(c, r), new Coordinate(0, -1), Aggressor) + Crawl(new Coordinate(c, r), new Coordinate(0, 1), Aggressor);
                        int d37 = Crawl(new Coordinate(c, r), new Coordinate(-1, 1), Aggressor) + Crawl(new Coordinate(c, r), new Coordinate(1, -1), Aggressor);
                        int d46 = Crawl(new Coordinate(c, r), new Coordinate(-1, 0), Aggressor) + Crawl(new Coordinate(c, r), new Coordinate(1, 0), Aggressor);

                        if ((d19 >= 4) || (d28 >= 4) || (d37 >= 4) || (d46 >= 4))
                            result.Add(new Coordinate(c, r));
                    }
                }
            }

            return result;
        }
        #endregion

        #region Private
        private void CheckState(int Column, int Row)
        {
            var which = this[Column, Row];

            int d19 = Crawl(new Coordinate(Column, Row), new Coordinate(-1, -1), which) + Crawl(new Coordinate(Column, Row), new Coordinate(1, 1), which);
            int d28 = Crawl(new Coordinate(Column, Row), new Coordinate(0, -1), which) + Crawl(new Coordinate(Column, Row), new Coordinate(0, 1), which);
            int d37 = Crawl(new Coordinate(Column, Row), new Coordinate(-1, 1), which) + Crawl(new Coordinate(Column, Row), new Coordinate(1, -1), which);
            int d46 = Crawl(new Coordinate(Column, Row), new Coordinate(-1, 0), which) + Crawl(new Coordinate(Column, Row), new Coordinate(1, 0), which);

            if ((d19 >= 4) || (d28 >= 4) || (d37 >= 4) || (d46 >= 4))
            {
                switch (which)
                {
                    case Stone.Black: State = BoardState.BlackWins; break;
                    case Stone.White: State = BoardState.WhiteWins; break;
                }
            } else
            {
                var foundEmpty = false;
                foreach (var cell in data)
                {
                    if (cell == Stone.Empty)
                        foundEmpty = true;
                }
                if (foundEmpty)
                    State = BoardState.InProgress;
                else
                    State = BoardState.Tie;
            }
        }

        private int Crawl(Coordinate Location, Coordinate Delta, Stone color)
        {
            int result = 0;

            Location += Delta;

            while ((Location.Column >= 0) && (Location.Column < 15) && (Location.Row >= 0) && (Location.Row < 15) && (this[Location.Column, Location.Row] == color))
            {
                Location += Delta;
                result++;
            }

            return result;
        }

        private Stone[,] data = new Stone[15, 15];
        #endregion


        #region Enumeration
        public IEnumerator<Stone> GetEnumerator()
        {
            for (int i = 0; i < 225; i++)
            {
                yield return this[new Coordinate(i)];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < 225; i++)
            {
                yield return this[new Coordinate(i)];
            }
        }
        #endregion
    }
}
