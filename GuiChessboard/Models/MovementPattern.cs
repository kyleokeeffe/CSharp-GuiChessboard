using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Shapes;
using GuiChessboard.Models;
using System.Windows.Controls;

namespace GuiChessboard.Models
{ 
    public delegate Position InnerPattern(int x, int y, int directionModifier, int index);
   //public delegate Position InnerPatternLimited(int x, int y, int directionModifier, int index, int distanceLimit);

    public class MovementPattern
    {
        private static bool distanceLimitReached=false;
        public ArrayList MoveFunctions { get; set; }
        private MovementPattern(ArrayList moveFunctions)
        {
            this.MoveFunctions = moveFunctions;
        }

        public static ArrayList CreatePattern(Grid gridBoard, Piece pieceClicked)
        {
            Dictionary<System.Windows.Controls.Border, Piece> availableEmptySquares = new Dictionary<System.Windows.Controls.Border, Piece>();
            Dictionary<System.Windows.Controls.Border, PieceTake> availableOccupiedSquares = new Dictionary<System.Windows.Controls.Border, PieceTake>();
            ArrayList patternSquares = new ArrayList();

            int directionModifier = (int)pieceClicked.Color;
            int x = pieceClicked.XPos;
            int y = pieceClicked.YPos;

            for (int i = 0; i < pieceClicked.MovePattern.MoveFunctions.Count; i++)
            {
                Position thisPosition;
                int j = 0;
                bool keepGoing = true;
                do
                {
                    InnerPattern moveFunctionDel = (InnerPattern)pieceClicked.MovePattern.MoveFunctions[i];
                    Piece intersectingPiece;
                    System.Windows.Controls.Border thisSquare;
                    //get distance limit
                    //check piece, move function, piece location, 
                    thisPosition = moveFunctionDel(x, y, directionModifier, (j + 1));

                    intersectingPiece = MainWindow.CheckSquareForPiece(thisPosition);
                    thisSquare = Position.GetPositionSquare(gridBoard,thisPosition);
                    

                    //could do if distance limit reached equals true then dont do the following
                    if (thisPosition.X > 0 && thisPosition.X < 9 && thisPosition.Y < 9 && thisPosition.Y > 0)
                    {
                        if (intersectingPiece == null)
                            availableEmptySquares.Add(thisSquare, pieceClicked);
                        else
                        {
                            availableOccupiedSquares.Add(thisSquare, new PieceTake(pieceClicked, intersectingPiece));
                            break;
                        }
                        j++;
                    }
                    else
                        keepGoing = false;
                    if (distanceLimitReached == true)
                        keepGoing = false;
                }
                while (keepGoing);
                distanceLimitReached = false;
            }
            patternSquares.Add(availableEmptySquares);
            patternSquares.Add(availableOccupiedSquares);
            return patternSquares;
        }

        public static MovementPattern NullPattern(int x, int y, PieceColour color)
        {
            return new MovementPattern(new ArrayList());
        }

        public static MovementPattern KingPattern(int x, int y, PieceColour color)
        {
            return new MovementPattern(new ArrayList()); 
        }
              
        public static MovementPattern QueenPattern(int x, int y, PieceColour color)
        {
            int directionModifier = (int)color;
            ArrayList pieceDirections = new ArrayList();

            InnerPattern queenForward, queenBack, queenLeft, queenRight, queenForwardLeft, queenForwardRight, queenBackLeft, queenBackRight;
            queenForward = new InnerPattern(Forward);
            queenBack = new InnerPattern(Back);
            queenLeft = new InnerPattern(Left); 
            queenRight = new InnerPattern(Right);
            queenForwardLeft = new InnerPattern(ForwardLeft);
            queenForwardRight = new InnerPattern(ForwardRight);
            queenBackLeft = new InnerPattern(BackLeft);
            queenBackRight = new InnerPattern(BackRight);

            pieceDirections.Add(queenForward);
            pieceDirections.Add(queenBack);
            pieceDirections.Add(queenLeft);
            pieceDirections.Add(queenRight);
            pieceDirections.Add(queenForwardLeft);
            pieceDirections.Add(queenForwardRight);
            pieceDirections.Add(queenBackLeft);
            pieceDirections.Add(queenBackRight);

            return new MovementPattern(pieceDirections);
        }
              
        public static MovementPattern BishopPattern(int x, int y, PieceColour color)
        {
            int directionModifier = (int)color;
            ArrayList pieceDirections = new ArrayList();

            InnerPattern bishopForwardLeft, bishopForwardRight,bishopBackLeft, bishopBackRight;
            bishopForwardLeft = new InnerPattern(ForwardLeft);
            bishopForwardRight = new InnerPattern(ForwardRight);
            bishopBackLeft = new InnerPattern(BackLeft);
            bishopBackRight = new InnerPattern(BackRight);

            pieceDirections.Add(bishopForwardLeft);
            pieceDirections.Add(bishopForwardRight);
            pieceDirections.Add(bishopBackLeft);
            pieceDirections.Add(bishopBackRight);

            return new MovementPattern(pieceDirections);
        }

        public static MovementPattern KnightPattern(int x, int y, PieceColour color)
        {
            return new MovementPattern(new ArrayList());
        }

        public static MovementPattern RookPattern(int x, int y, PieceColour color)
        {
            int directionModifier = (int)color;
            ArrayList pieceDirections = new ArrayList();

            InnerPattern rookForward, rookBack, rookLeft, rookRight;
            rookForward = new InnerPattern(Forward);
            rookBack = new InnerPattern(Back);
            rookLeft = new InnerPattern(Left);
            rookRight = new InnerPattern(Right);

            pieceDirections.Add(rookForward);
            pieceDirections.Add(rookBack);
            pieceDirections.Add(rookLeft);
            pieceDirections.Add(rookRight);

            return new MovementPattern(pieceDirections);
        }

        public static MovementPattern PawnPattern(int x, int y, PieceColour color)
        {
            int directionModifier = (int)color;

            InnerPattern pawnForward;
            ArrayList pieceDirections = new ArrayList();

            if ((y == 1 && color == PieceColour.Black) || (y == 8 && color == PieceColour.White))
                pawnForward = new InnerPattern(ForwardTwo);
            else
                pawnForward = new InnerPattern(ForwardOne);

            pieceDirections.Add(pawnForward);

            if (MainWindow.CheckSquareForPiece(new Position(x + 1, y + (1 * directionModifier))) != null) 
                pieceDirections.Add(new InnerPattern(ForwardRightOne));
            
            if(MainWindow.CheckSquareForPiece(new Position(x - 1, y + (1 * directionModifier))) != null)
                pieceDirections.Add(new InnerPattern(ForwardLeftOne));


            return new MovementPattern(pieceDirections);
        }
        


        public static Position Forward(int x, int y, int directionModifier, int index)
        {
            
            
            
            int newX, newY;

            newX = x;
            newY = y + (index * directionModifier);

            return new Position(newX, newY);
        }

        public static Position ForwardOne(int x, int y, int directionModifier, int index)
        {

            


            int newX, newY;

            newX = x;
            newY = y + (index * directionModifier);
            distanceLimitReached = true;
            return new Position(newX, newY);
        }

        public static Position ForwardTwo(int x, int y, int directionModifier, int index)
        {

            


            int newX, newY;

            newX = x;
            newY = y + (index * directionModifier);
           if(index==1)
                distanceLimitReached = true;
            return new Position(newX, newY);
        }



        public static Position ForwardLeft(int x, int y, int directionModifier, int index)
        {
            int newX, newY;

            newX = x - (index * directionModifier);
            newY = y + (index * directionModifier);

            return new Position(newX, newY);
        }
        public static Position ForwardLeftOne(int x, int y, int directionModifier, int index)
        {
            int newX, newY;

            newX = x - (index * directionModifier);
            newY = y + (index * directionModifier);
            distanceLimitReached = true;
            return new Position(newX, newY);
        }

        public static Position ForwardRight(int x, int y, int directionModifier, int index)
        {
            int newX, newY;

            newX = x + (index * directionModifier);
            newY = y + (index * directionModifier);

            return new Position(newX, newY);
        }

        public static Position ForwardRightOne(int x, int y, int directionModifier, int index)
        {
            int newX, newY;

            newX = x + (index * directionModifier);
            newY = y + (index * directionModifier);
            distanceLimitReached = true;
            return new Position(newX, newY);
        }

        public static Position Back(int x, int y, int directionModifier, int index)
        {
            int newX, newY;

            newX = x;
            newY = y - (index * directionModifier);

            return new Position(newX, newY);
        }

        public static Position BackLeft(int x, int y, int directionModifier, int index)
        {
            int newX, newY;

            newX = x - (index * directionModifier);
            newY = y - (index * directionModifier);

            return new Position(newX, newY);
        }

        public static Position BackRight(int x, int y, int directionModifier, int index)
        {
            int newX, newY;

            newX = x + (index * directionModifier);
            newY = y - (index * directionModifier);

            return new Position(newX, newY);
        }

        public static Position Left(int x, int y, int directionModifier, int index)
        {
            int newX, newY;

            newX = x - (index * directionModifier);
            newY = y;

            return new Position(newX, newY);
        }

        public static Position Right(int x, int y, int directionModifier, int index)
        {

            int newX, newY;

            newX = x + (index * directionModifier);
            newY = y;

            return new Position(newX, newY);
        }
    }
}
