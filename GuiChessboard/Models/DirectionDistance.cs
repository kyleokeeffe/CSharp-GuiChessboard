using System;
using System.Collections.Generic;
using System.Text;

namespace GuiChessboard.Models
{
    public class DirectionDistance
    {
        public Direction Direction { get; set; }
        public DistanceLimit DistanceLimit { get; set; }
        
        public DirectionDistance(Direction direction, DistanceLimit distanceLimit)
        {
            this.Direction = direction;
            this.DistanceLimit = distanceLimit;
        }

    

    }





    public enum Direction
    {
        forward,
        back,
        left,
        right,
        forwardLeft,
        forwardRight,
        backLeft,
        backRight
    }
    public enum DistanceLimit
    {
        one,
        two,
        full
    }
}
