using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests
{
    public class Game
    {
        private Board board;
        public PieceColor currentPlayer = PieceColor.White;

        public void Play()
        {
            //initialize board
            board = new Board();

            //create listOfMoves -> ReadFromFile

            //NextTurn()

            //loop listMoves -> while are elem in list
            // NextTurn(player=white, cooordonates)

            // currentPlayer = black
            // NextTurn(player=black, cooordonates)
        }

        private void NextTurn(PieceColor player, string coordonates)
        {
            //citesc mutarea din lista si returnez Cell si Piece(e5)

            //coordonates: string =e5

            //cellDestination = transform coordonates into a Cell -> function in Board

            //piece = FindPieceWhoNeedsToBeMoved(cellDestination)

            //Move(piece, cellDestination)
        }
    }
}
