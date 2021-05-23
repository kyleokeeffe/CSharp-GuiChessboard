using System;
using System.Collections.Generic;
using System.Text;
using GuiChessboard.Models;
using System.Windows.Controls;

namespace GuiChessboard.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public static System.Windows.Controls.Border GetPositionSquare(Grid gridBoard,Position position)
        {
            string name = $"cell{position.X}{position.Y}";
            System.Windows.Controls.Border thisRect = (System.Windows.Controls.Border)gridBoard.FindName(name);

            return thisRect;
        }
        public static System.Windows.Controls.Label GetPositionLabel(Grid gridBoard, Position position)
        {
            string name = $"lblCell{position.X}{position.Y}";
            System.Windows.Controls.Label thisLabel = (System.Windows.Controls.Label)gridBoard.FindName(name);
            return thisLabel;
        }
    }
}
