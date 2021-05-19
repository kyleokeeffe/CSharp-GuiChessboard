using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Collections;
using GuiChessboard.Models;



namespace GuiChessboard.Models
{
    
    public class Piece
    {
        public delegate MovementPattern MyDelegate(int xPos, int yPos, PieceColour color);


       
        public PieceType Name { get; set; }
        public PieceColour Color { get; set; }
        //position
        
        public Rectangle CurrentLocation { get; set; }
        public int XPos
        {
            get
            {
                return Convert.ToInt32(CurrentLocation.Name.Substring(4, 1));
            }
            set
            {
          
            }
        }
        public int YPos
        {
            get
            {
                return Convert.ToInt32(CurrentLocation.Name.Substring(5, 1));
            }
            set
            {

            }
        }
        public MovementPattern MovePattern 
        {
            get
            {

                MyDelegate thisPiecePattern;

                
                switch (this.Name)
                {
                    case PieceType.King:
                        thisPiecePattern= new MyDelegate(MovementPattern.KingPattern);
                        break;
                    case PieceType.Queen:
                        thisPiecePattern = new MyDelegate(MovementPattern.QueenPattern);
                        break;
                    case PieceType.Bishop:
                        thisPiecePattern = new MyDelegate(MovementPattern.BishopPattern);
                        break;
                    case PieceType.Knight:
                        thisPiecePattern = new MyDelegate(MovementPattern.KnightPattern);
                        break;
                    case PieceType.Rook:
                        thisPiecePattern = new MyDelegate(MovementPattern.RookPattern);
                        break;
                    case PieceType.Pawn:
                        thisPiecePattern = new MyDelegate(MovementPattern.PawnPattern);
                        break;
                    default:
                        thisPiecePattern = new MyDelegate(MovementPattern.NullPattern);
                        break;
                }
                return thisPiecePattern(this.XPos, this.YPos, this.Color);
                
            }
            set
            {

            }
        }


        public Piece(PieceColour color,PieceType name, Rectangle currentLocation)
        {
            this.Name = name;
            this.Color = color;
            this.CurrentLocation = currentLocation;




        }
    

        public override string ToString()
        {
            return $"{this.Name}, {this.Color},{string.Join(", ", this.MovePattern.MoveFunctions.ToString())}";
        }
    }
    /*public class Rook : Piece
    {

        public Rook(List<DirectionDistance> movementPattern, Color color, PieceType name = PieceType.Rook) : base(movementPattern,color,name)
        {
            
            

        }
        public override string ToString()
        {
            return $"{this.Name}, {this.Color},{string.Join(", ", this.MovementPattern)}";
        }



    }
    public class Bishop : Piece
    {
    public Bishop(List<DirectionDistance> movementPattern, Color color, PieceType name = PieceType.Bishop) : base(movementPattern,color,name)
        {
            
            

        }
        public override string ToString()
        {
            return $"{this.Name}, {this.Color},{string.Join(", ", this.MovementPattern)}";
        }

    }
    public class Knight : Piece
    {
        public Knight(List<DirectionDistance> movementPattern, Color color, PieceType name = PieceType.Knight) : base(movementPattern, color, name)
        {



        }
        public override string ToString()
        {
            return $"{this.Name}, {this.Color},{string.Join(", ", this.MovementPattern)}";
        }

    }
    public class Pawn : Piece
    {
        public Pawn(List<DirectionDistance> movementPattern, Color color, PieceType name = PieceType.Pawn) : base(movementPattern, color, name)
        {



        }
        public override string ToString()
        {
            return $"{this.Name}, {this.Color},{string.Join(", ", this.MovementPattern)}";
        }

    }
    public class King : Piece
    {
        public King(List<DirectionDistance> movementPattern, Color color, PieceType name = PieceType.King) : base(movementPattern, color, name)
        {



        }
        public override string ToString()
        {
            return $"{this.Name}, {this.Color},{string.Join(", ", this.MovementPattern)}";
        }

    }
    public class Queen : Piece
    {
        public Queen(List<DirectionDistance> movementPattern, Color color, PieceType name = PieceType.Queen) : base(movementPattern, color, name)
        {



        }
        public override string ToString()
        {
            return $"{this.Name}, {this.Color},{string.Join(", ", this.MovementPattern)}";
        }

    }

    */
}
