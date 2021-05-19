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
using GuiChessboard.Models;
using System.Collections;
using System.Diagnostics;
using System.Drawing;

namespace GuiChessboard
{
    //testing Git Push
    public partial class MainWindow : Window
    {
        public delegate void EventCreator(object obj, MouseButtonEventArgs e);
        
        ArrayList emptyBoardColors = new ArrayList();
        static List<Piece> piecesList = new List<Piece>();
        protected Dictionary<System.Windows.Shapes.Rectangle, Piece> availableEmptySquares;
        protected Dictionary<System.Windows.Shapes.Rectangle, PieceTake> availableOccupiedSquares;
        
        public MainWindow()
        {
            InitializeComponent();
       
            emptyBoardColors =  SaveEmptyBoardColors();

            Piece bishop1 = new Piece(PieceColour.Black, PieceType.Bishop, cell43);
            Piece bishop2 = new Piece(PieceColour.White, PieceType.Bishop, cell65);
            Piece rook1 = new Piece(PieceColour.Black, PieceType.Rook, cell55);
            Piece queen1 = new Piece(PieceColour.Black, PieceType.Queen, cell33);

            piecesList.Add(bishop1);
            piecesList.Add(bishop2);
            piecesList.Add(rook1);
            piecesList.Add(queen1);

            lblMobile.Content = "hello";
            lblMobile.Margin = new Thickness(0, 0, 100,100);

            grdBoard.Loaded += PaintPieces;

            grdBoard.MouseDown += IdentifyClick;
        }

        public void IdentifyClick(object obj, MouseButtonEventArgs e)
        {
            var pieceClicked = piecesList.Find(piece => piece.CurrentLocation == (System.Windows.Shapes.Rectangle)e.Source);

            if (pieceClicked != null)
                PaintPieceMovePattern(pieceClicked);
            else if (availableEmptySquares == null)
            {
                PaintEmptyBoardColors();
                PaintPieces();
            }
            else if (availableEmptySquares.ContainsKey((System.Windows.Shapes.Rectangle)e.Source) == false)
            {
                foreach (System.Windows.Shapes.Rectangle emptySquare in availableEmptySquares.Keys)
                    emptySquare.MouseLeftButtonDown -= EmptyMoveSquareClicked;

                foreach (var pieceSquare in availableOccupiedSquares.Keys)
                    pieceSquare.MouseLeftButtonDown -= OccupiedMoveSquareClicked;

                PaintEmptyBoardColors();
                PaintPieces();
            }
        }

        public void PaintPieceMovePattern(Piece pieceClicked)
        {
            PaintEmptyBoardColors();
            PaintPieces();
            
            int directionModifier = (int)pieceClicked.Color;
            int x = pieceClicked.XPos;
            int y = pieceClicked.YPos;

            //if the new click is on another piece, remove the event listeners for the previous piece clicked
            if (availableEmptySquares != null)
            {
                foreach (System.Windows.Shapes.Rectangle emptySquare in availableEmptySquares.Keys)
                    emptySquare.MouseLeftButtonDown -= EmptyMoveSquareClicked;
            }
            
            if (availableOccupiedSquares != null)
            {
                foreach (var pieceSquare in availableOccupiedSquares.Keys)
                    pieceSquare.MouseLeftButtonDown -= OccupiedMoveSquareClicked;
            }


            //if this method has not been run before, initialize move square arrays
            availableEmptySquares = new Dictionary<System.Windows.Shapes.Rectangle, Piece>();
            availableOccupiedSquares = new Dictionary<System.Windows.Shapes.Rectangle, PieceTake>();

            ArrayList patternSquares= MovementPattern.CreatePattern(grdBoard,pieceClicked);
            availableEmptySquares = (Dictionary<System.Windows.Shapes.Rectangle, Piece>)patternSquares[0];
            availableOccupiedSquares = (Dictionary<System.Windows.Shapes.Rectangle, PieceTake>)patternSquares[1];

            MovementPattern.CreatePattern(grdBoard,pieceClicked);

            /*

            //fill move square arrays
            for (int i = 0; i < pieceClicked.MovePattern.MoveFunctions.Count; i++)
            {
                Position thisPosition;
                int j = 0;
                bool keepGoing = true;
                do
                {
                    
                    InnerPattern thing = (InnerPattern)pieceClicked.MovePattern.MoveFunctions[i];
                    thisPosition = thing(x, y, directionModifier, (j + 1));
                    Piece intersectingPiece;
                    intersectingPiece = CheckSquareForPiece(thisPosition);
                    ConvertPositionToSquare(thisPosition);
                    if (thisPosition.X > 0 && thisPosition.X < 9 && thisPosition.Y < 9 && thisPosition.Y > 0)//why? to allow the do while to test the first runthrough - shoudl be using a while, but testing valueis made inside while - probably away to do this //didnt work
                    {
                        if (intersectingPiece == null)
                            availableEmptySquares.Add(ConvertPositionToSquare(thisPosition), pieceClicked);
                        else
                        {
                            availableOccupiedSquares.Add(ConvertPositionToSquare(thisPosition), new PieceTake(pieceClicked, intersectingPiece));
                            break;
                        }
                        j++;
                    }
                    else
                        keepGoing = false;
                }
                while (keepGoing);
               // while (thisPosition.X > 1 && thisPosition.X < 8 && thisPosition.Y < 8 && thisPosition.Y > 1);
            }
            */

            //paint squares
            if (availableEmptySquares != null)
            {
                foreach (System.Windows.Shapes.Rectangle emptySquare in availableEmptySquares.Keys)
                {
                    emptySquare.Fill = System.Windows.Media.Brushes.Green;
                    emptySquare.MouseLeftButtonDown += EmptyMoveSquareClicked;  
                }
            }

            if (availableOccupiedSquares != null)
            {
                 foreach (var pieceSquare in availableOccupiedSquares.Keys)
                    {
                        pieceSquare.Fill = System.Windows.Media.Brushes.Orange;
                        pieceSquare.MouseLeftButtonDown += OccupiedMoveSquareClicked;
                    }
            }
        }

        public void EmptySquareClicked(object obj, MouseButtonEventArgs e)
        {
            System.Windows.Shapes.Rectangle thisSquare = (System.Windows.Shapes.Rectangle)obj;
            Piece originatingPiece = availableEmptySquares.GetValueOrDefault(thisSquare);

            originatingPiece.CurrentLocation = thisSquare;

            PaintEmptyBoardColors();
            PaintPieces();
        }

        public void EmptyMoveSquareClicked(object obj, MouseButtonEventArgs e)
        {
            System.Windows.Shapes.Rectangle thisSquare = (System.Windows.Shapes.Rectangle)obj;
            Piece originatingPiece = availableEmptySquares.GetValueOrDefault(thisSquare);

            originatingPiece.CurrentLocation = thisSquare;

            PaintEmptyBoardColors();
            PaintPieces();
        }

        public void OccupiedMoveSquareClicked(object obj, MouseButtonEventArgs e)
        {
            System.Windows.Shapes.Rectangle thisSquare = (System.Windows.Shapes.Rectangle)obj;
            PieceTake thisPiecetake = availableOccupiedSquares.GetValueOrDefault(thisSquare);
           
            thisPiecetake.OriginatingPiece.CurrentLocation = thisSquare;

            piecesList.Remove(thisPiecetake.DestinationPiece);
            PaintEmptyBoardColors();
            PaintPieces();
         
            MessageBox.Show($"{thisPiecetake.OriginatingPiece.Color} {thisPiecetake.OriginatingPiece.Name} takes {thisPiecetake.DestinationPiece.Color} {thisPiecetake.DestinationPiece.Name}");
        }

        public static Piece CheckSquareForPiece(Position position)
        {
            //check if theres a piece on it
            Piece thisPiece;
            try
            {
                thisPiece = piecesList.Find(x => x.XPos == position.X && x.YPos == position.Y);
            }
            catch (Exception)
            {
                thisPiece = null;
            }
            return thisPiece;
        }
        
        public void PaintPieces(object obj, EventArgs e)
        { 
            for(int i = 0; i< piecesList.Count; i++)
            {
                var thisColor = piecesList[i].Color.ToString();
                SolidColorBrush colorConvertor = (SolidColorBrush)new BrushConverter().ConvertFromString(thisColor);
               
                piecesList[i].CurrentLocation.Fill = colorConvertor;
            }
        
        }

        public void PaintPieces()
        {
            for (int i = 0; i < piecesList.Count; i++)
            {
                var thisColor = piecesList[i].Color.ToString();
                SolidColorBrush colorConvertor = (SolidColorBrush)new BrushConverter().ConvertFromString(thisColor);

                piecesList[i].CurrentLocation.Fill = colorConvertor;
                
                //Trying to figure out how to print label in same square as piece
                // piecesList[i].CurrentLocation.Fill=
                //var thisPieceLocation = piecesList[i].CurrentLocation.RenderedGeometry.Bounds;
                //lblMobile.Content = piecesList[i].Name.ToString().Substring(0, 2);
                //lblMobile.Margin = new Thickness(thisPieceLocation.Left, thisPieceLocation.Top, thisPieceLocation.Right, thisPieceLocation.Bottom);
            }

        }

        public ArrayList SaveEmptyBoardColors()
        {
            ArrayList emptyBoardColors = new ArrayList();
            foreach (var thing in grdBoard.Children)
            {
                if (thing.GetType() == typeof(System.Windows.Shapes.Rectangle))
                {
                    System.Windows.Shapes.Rectangle thisThing = (System.Windows.Shapes.Rectangle)thing;
                    emptyBoardColors.Add(thisThing.Fill);
                }
                else
                    emptyBoardColors.Add(thing);
            }
            return emptyBoardColors;
        }
            
        public void PaintEmptyBoardColors()
        {
            for(int i =0;i< grdBoard.Children.Count; i++)
            {
                var previousColor = emptyBoardColors[i];

                if (previousColor.GetType() == typeof(System.Windows.Media.SolidColorBrush))
                {
                    System.Windows.Shapes.Rectangle thing = (System.Windows.Shapes.Rectangle)grdBoard.Children[i];
                    thing.Fill = (System.Windows.Media.Brush)previousColor;
                }
            }
        }
    }
} 
