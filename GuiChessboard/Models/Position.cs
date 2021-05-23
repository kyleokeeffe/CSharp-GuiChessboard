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
        public static System.Windows.Controls.Border ConvertPositionToSquare(Grid gridBoard,Position position)
        {
            string name = $"cell{position.X}{position.Y}";
            System.Windows.Controls.Border thisRect = (System.Windows.Controls.Border)gridBoard.FindName(name);

            return thisRect;
        }
    }
}
