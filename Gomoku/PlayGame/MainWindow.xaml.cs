﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlayGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, BitmapImage> ImageCache = new Dictionary<string, BitmapImage>();
        Gomoku.Board board;
        bool White = false;

        Button[,] Cells;

        public MainWindow()
        {
            InitializeComponent();

            ImageCache["Black"] = new BitmapImage(new Uri("/Media/Black.png", UriKind.RelativeOrAbsolute));
            ImageCache["White"] = new BitmapImage(new Uri("/Media/White.png", UriKind.RelativeOrAbsolute));
            ImageCache["Empty"] = new BitmapImage(new Uri("/Media/Empty.png", UriKind.RelativeOrAbsolute));
            ImageCache["WhiteThreat"] = new BitmapImage(new Uri("/Media/WhiteThreat.png", UriKind.RelativeOrAbsolute));
            ImageCache["BlackThreat"] = new BitmapImage(new Uri("/Media/BlackThreat.png", UriKind.RelativeOrAbsolute));

            for (int i = 0; i < 15; i++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
                GameGrid.RowDefinitions.Add(new RowDefinition());

                ThreatGrid.ColumnDefinitions.Add(new ColumnDefinition());
                ThreatGrid.RowDefinitions.Add(new RowDefinition());
            }

            Cells = new Button[15, 15];

            for (int r = 0; r < 15; r++)
            {
                for (int c = 0; c < 15; c++)
                {
                    var img = new Image() { Source = ImageCache["Empty"] };

                    Cells[c, r] = new Button()
                    {
                        Name = string.Format("Cell{0:00}{1:00}", c, r),
                        Content = img,
                        Width = 40,
                        Height = 40
                    };
                    Cells[c, r].Click += OnPlayCell;
                    Cells[c, r].Style = (Style)Resources["CellStyle"];

                    Grid.SetRow(Cells[c,r], r);
                    Grid.SetColumn(Cells[c, r], c);
                    GameGrid.Children.Add(Cells[c, r]);
                }
            }

            NewGame();
        }

        private void NewGame()
        {
            board = new Gomoku.Board();
            RedrawAll();
            WhiteMoves.Children.Clear();
            BlackMoves.Children.Clear();
            White = false;
        }

        private void RedrawAll()
        {
            for (int r = 0; r < 15; r++)
            {
                for (int c = 0; c < 15; c++)
                {
                    ((Image)Cells[c,r].Content).Source = ImageCache[board[c,r].ToString()];
                }
            }

            ThreatGrid.Children.Clear();
        }

        private void OnPlayCell(object sender, RoutedEventArgs e)
        {
            if (board.State == Gomoku.BoardState.InProgress)
            {
                var b = (Button)sender;

                int Col = int.Parse(b.Name.Substring(4, 2));
                int Row = int.Parse(b.Name.Substring(6, 2));

                if (board[Col, Row] == Gomoku.Stone.Empty)
                {
                    if (White)
                    {
                        board = board.Put(new Gomoku.Coordinate(Col, Row), Gomoku.Stone.White);
                        ((Image)b.Content).Source = ImageCache["White"];
                        WhiteMoves.Children.Add(new TextBlock() { Text = string.Format("{0},{1}", Col, Row) });
                    }
                    else
                    {
                        board = board.Put(new Gomoku.Coordinate(Col, Row), Gomoku.Stone.Black);
                        ((Image)b.Content).Source = ImageCache["Black"];
                        BlackMoves.Children.Add(new TextBlock() { Text = string.Format("{0},{1}", Col, Row) });
                    }
                    White = !White;

                    switch (board.State)
                    {
                        case Gomoku.BoardState.BlackWins: BlackMoves.Children.Add(new TextBlock() { Text = "Black wins!" }); break;
                        case Gomoku.BoardState.WhiteWins: WhiteMoves.Children.Add(new TextBlock() { Text = "White wins!" }); break;
                        case Gomoku.BoardState.Tie:
                            var tb = new TextBlock() { Text = "Tie!" };
                            BlackMoves.Children.Add(tb);
                            WhiteMoves.Children.Add(tb);
                            break;
                    }
                }
            }
        }

        private void OnNewGame(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void OnShiftUp(object sender, RoutedEventArgs e)
        {
            board = board.Shift(new Gomoku.Coordinate(0, -1));
            RedrawAll();
        }

        private void OnShiftLeft(object sender, RoutedEventArgs e)
        {
            board = board.Shift(new Gomoku.Coordinate(-1, 0));
            RedrawAll();
        }

        private void OnShiftRight(object sender, RoutedEventArgs e)
        {
            board = board.Shift(new Gomoku.Coordinate(1, 0));
            RedrawAll();
        }

        private void OnShiftDown(object sender, RoutedEventArgs e)
        {
            board = board.Shift(new Gomoku.Coordinate(0, 1));
            RedrawAll();
        }

        private void OnCenter(object sender, RoutedEventArgs e)
        {
            var bounds = board.GetBounds();
            var right = 14 - bounds.Maximum.Column;
            var bottom = 14 - bounds.Maximum.Row;

            var avgC = (bounds.Minimum.Column + right) / 2;
            var avgR = (bounds.Minimum.Row + bottom) / 2;

            var shiftC = avgC - bounds.Minimum.Column;
            var shiftR = avgR - bounds.Minimum.Row;

            board = board.Shift(new Gomoku.Coordinate(shiftC, shiftR));
            RedrawAll();
        }

        private void OnUpperLeft(object sender, RoutedEventArgs e)
        {
            var bounds = board.GetBounds();
            board = board.Shift(new Gomoku.Coordinate(-bounds.Minimum.Column, -bounds.Minimum.Row));
            RedrawAll();
        }

        private void OnUpperRight(object sender, RoutedEventArgs e)
        {
            var bounds = board.GetBounds();
            board = board.Shift(new Gomoku.Coordinate(14 -bounds.Maximum.Column, -bounds.Minimum.Row));
            RedrawAll();
        }

        private void OnLowerLeft(object sender, RoutedEventArgs e)
        {
            var bounds = board.GetBounds();
            board = board.Shift(new Gomoku.Coordinate(-bounds.Minimum.Column, 14-bounds.Maximum.Row));
            RedrawAll();
        }

        private void OnLowerRight(object sender, RoutedEventArgs e)
        {
            var bounds = board.GetBounds();
            board = board.Shift(new Gomoku.Coordinate(14 -bounds.Maximum.Column, 14 - bounds.Maximum.Row));
            RedrawAll();
        }

        private void OnFindThreats(object sender, RoutedEventArgs e)
        {
            ThreatGrid.Children.Clear();

            foreach (var item in board.GetThreats(Gomoku.Stone.White))
            {
                var img = new Image() { Source = ImageCache["WhiteThreat"] };
                Grid.SetColumn(img, item.Column);
                Grid.SetRow(img, item.Row);
                ThreatGrid.Children.Add(img);
            }

            foreach (var item in board.GetThreats(Gomoku.Stone.Black))
            {
                var img = new Image() { Source = ImageCache["BlackThreat"] };
                Grid.SetColumn(img, item.Column);
                Grid.SetRow(img, item.Row);
                ThreatGrid.Children.Add(img);
            }
        }

        private void OnFlipHorizontal(object sender, RoutedEventArgs e)
        {
            board = board.FlipHorizontal();
            RedrawAll();
        }

        private void OnFlipVertical(object sender, RoutedEventArgs e)
        {
            board = board.FlipVertical();
            RedrawAll();
        }

        private void OnRotateCW(object sender, RoutedEventArgs e)
        {
            board = board.RotateClockwise();
            RedrawAll();
        }

        private void OnRotateCCW(object sender, RoutedEventArgs e)
        {
            board = board.RotateCounterClockwise();
            RedrawAll();
        }
    }
}
