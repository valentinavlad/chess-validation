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
        public int MovesCounter { get; set; }
        public PieceColor Winner { get; set; }
        //lista piese capturate
        public void Play(List<string> listOfMoves)
        {
            //initialize board
            board = new Board();

            //create listOfMoves -> ReadFromFile
               
            foreach (var moveAN in listOfMoves)
            {
                NextTurn(currentPlayer, moveAN);
                MovesCounter++;
                if (currentPlayer == PieceColor.White)
                {
                    currentPlayer = PieceColor.Black;
                }
                else
                {
                    currentPlayer = PieceColor.White;
                }
                
       
            }
            //loop listMoves -> while are elem in list
            // NextTurn(player=white, cooordonates)

            // currentPlayer = black
            // NextTurn(player=black, cooordonates)
        }

        private void NextTurn(PieceColor player, string moveAN)
        {
            //citesc mutarea din lista si returnez Cell si Piece(e5)

            //coordonates: string =e5

            //cellDestination = transform coordonates into a Cell -> function in Board
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, player);
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);

            //piece = FindPieceWhoNeedsToBeMoved(cellDestination)
            var piece = board.FindPieceWhoNeedsToBeMoved(move, player);

            //Move(piece, cellDestination)
            board.Move(piece, destinationCell);

            //moveAn imi spune ce mutare am sa fac

            //after move check if is promotion
        }
    }
}
