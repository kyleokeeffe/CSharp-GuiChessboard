using System;
using System.Collections.Generic;
using System.Text;

namespace GuiChessboard.Models
{
    public class PieceTake
    {
        public Piece OriginatingPiece { get; set; }
        public Piece DestinationPiece { get; set; }

        public PieceTake(Piece originatingPiece, Piece destinationPiece)
        {
            this.OriginatingPiece = originatingPiece;
            this.DestinationPiece = destinationPiece;
        }
    }
}
