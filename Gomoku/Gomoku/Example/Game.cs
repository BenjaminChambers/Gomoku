using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku.Example
{
    public class Game
    {
        public Game() { }

        public Board CurrentBoard { get; private set; }
        public IReadOnlyList<(Stone Color, Coordinate Where, Board Result)> GetHistory() { return History; }

        public void PlayMove(Coordinate Location)
        {
            if (CurrentBoard.State == BoardState.InProgress)
            {
                Stone player = (History.Count % 2 == 0) ? Stone.Black : Stone.White;
                CurrentBoard = CurrentBoard.Put(Location, player);
                History.Add((player, Location, CurrentBoard));
            }
        }

        List<(Stone Color, Coordinate Where, Board Result)> History = new List<(Stone Color, Coordinate Where, Board Result)>();
    }
}
