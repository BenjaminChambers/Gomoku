using System;
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
        Dictionary<Gomoku.Stone, BitmapImage> ImageCache = new Dictionary<Gomoku.Stone, BitmapImage>();
        Gomoku.Board board;
        bool White = false;

        Button[,] Cells;

        public MainWindow()
        {
            InitializeComponent();

            ImageCache[Gomoku.Stone.Black] = new BitmapImage(new Uri("/Media/Black.png", UriKind.RelativeOrAbsolute));
            ImageCache[Gomoku.Stone.White] = new BitmapImage(new Uri("/Media/White.png", UriKind.RelativeOrAbsolute));
            ImageCache[Gomoku.Stone.Empty] = new BitmapImage(new Uri("/Media/Empty.png", UriKind.RelativeOrAbsolute));

            for (int i = 0; i < 15; i++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
                GameGrid.RowDefinitions.Add(new RowDefinition());
            }

            Cells = new Button[15, 15];

            for (int r = 0; r < 15; r++)
            {
                for (int c = 0; c < 15; c++)
                {
                    var img = new Image() { Source = ImageCache[Gomoku.Stone.Empty] };

                    Cells[r, c] = new Button()
                    {
                        Name = string.Format("Cell{0:00}{1:00}", r, c),
                        Content = img,
                        Width = 40,
                        Height = 40
                    };
                    Cells[r, c].Click += OnPlayCell;
                    Cells[r, c].Style = (Style)Resources["CellStyle"];

                    Grid.SetRow(Cells[r, c], r);
                    Grid.SetColumn(Cells[r, c], c);
                    GameGrid.Children.Add(Cells[r, c]);
                }
            }

            NewGame();
        }

        private void NewGame()
        {
            board = new Gomoku.Board();
            for (int r = 0; r < 15; r++)
            {
                for (int c = 0; c < 15; c++)
                {
                    ((Image)Cells[r, c].Content).Source = ImageCache[Gomoku.Stone.Empty];
                }
            }
            WhiteMoves.Children.Clear();
            BlackMoves.Children.Clear();
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
                        board = board.Put(Col, Row, Gomoku.Stone.White);
                        ((Image)b.Content).Source = ImageCache[Gomoku.Stone.White];
                        WhiteMoves.Children.Add(new TextBlock() { Text = string.Format("{0},{1}", Col, Row) });
                    }
                    else
                    {
                        board = board.Put(Col, Row, Gomoku.Stone.Black);
                        ((Image)b.Content).Source = ImageCache[Gomoku.Stone.Black];
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
    }
}
