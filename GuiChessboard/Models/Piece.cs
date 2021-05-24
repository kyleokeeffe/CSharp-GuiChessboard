using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Collections;
using GuiChessboard.Models;
using System.Windows.Controls;
using System.Windows;




namespace GuiChessboard.Models
{
    public class Piece
    {
      
        public delegate MovementPattern MyDelegate(int xPos, int yPos, PieceColour color);

   

        public PieceType Name { get; set; }
        public PieceColour Color { get; set; }
        
        public System.Windows.Controls.Border CurrentLocation { get; set; }
        public System.Windows.Controls.Label CurrentLabel { 
            get 
            {
                System.Windows.Controls.Label thisLabel = new Label();
                 string name = $"lbl{CurrentLocation.Name}";
                
              
               
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        thisLabel = (System.Windows.Controls.Label)(window as MainWindow).grdBoard.FindName(name); 
                    }
                }
                //https://stackoverflow.com/questions/13644114/how-can-i-access-a-control-in-wpf-from-another-class-or-window

            return thisLabel;

            }
            set 
            {
            } 
        }
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

        public Piece(PieceColour color,PieceType name, System.Windows.Controls.Border currentLocation)
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
}
