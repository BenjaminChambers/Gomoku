using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku.Example
{
    public class Game
    {
        public Game()
        {
            CurrentBoard = new Board();
        }

        public Board CurrentBoard { get; private set; }
        public Stone CurrentPlayer { get { return History.Count % 2 == 0 ? Stone.Black : Stone.White; } }
        public IReadOnlyList<(Stone Color, Coordinate Where, Board Result)> GetHistory() { return History; }

        public void PlayMove(Coordinate Location)
        {
            if (CurrentBoard.State == BoardState.InProgress)
            {
                CurrentBoard = CurrentBoard.Put(Location, CurrentPlayer);
                History.Add((CurrentPlayer, Location, CurrentBoard));
            }
        }

        List<(Stone Color, Coordinate Where, Board Result)> History = new List<(Stone Color, Coordinate Where, Board Result)>();
    }
}
