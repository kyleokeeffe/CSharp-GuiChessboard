using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using GuiChessboard.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GuiChessboard.Models
{
    public class AllInOne
    {

        //gonna make a bishop
        //make one single piece with all its attributes, then if there are things in common with other peices, group them after. but make one functional piece 
        public int XPos { get; set; }
        public int YPos { get; set; }

        public AllInOne(int xPos, int yPos)
        {
            this.XPos = xPos;
            this.YPos = yPos;

        }


        public bool MovePiece(int newXPos, int newYPos)//bishop
        {
            bool legalMove;
            
           
            //2,1 -> to 5,4
            /*can move in four directions xpos+moveDistance;yPos+moveDistance
            forwardLeft = xPos-distance
             
            legal moves for bishop is 
                check if selected square is in on a legal move path for the piece 

               */
            //is difference between old x and new x the same as difference between old y and new y



                if(Math.Abs(XPos-newXPos)==Math.Abs(YPos-newYPos))
                    legalMove = true;

                else
                    legalMove = false;

                     MessageBox.Show($"{legalMove}");

            return legalMove;

        }

        public void MovePieceRook(int newXPos, int newYPos)
        {
            bool legalMove;


            //2,1 -> to 5,4
            /*can move in four directions xpos+moveDistance;yPos+moveDistance
            forwardLeft = xPos-distance
             
            legal moves for bishop is 
                check if selected square is in on a legal move path for the piece 

               */
            //is difference between old x and new x the same as difference between old y and new y



            if (Math.Abs(XPos - newXPos) == Math.Abs(YPos - newYPos))
                legalMove = true;

            else
                legalMove = false;

            MessageBox.Show($"{legalMove}");

        }

        //(xPos+n&&yPos+n||xPos-n&&yPos+n||xPos+n&&yPos-n||xPos-n&&yPos-n)






    }

    public enum MoveDirection
    {

    }
}
