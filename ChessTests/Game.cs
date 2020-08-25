using ChessTable;
using ChessTests.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ChessTests
{
    public class Game
    {
        private Board board;
        public PieceColor currentPlayer = PieceColor.White;
        public int MovesCounter { get; set; }
        public PieceColor Winner { get; set; }
        public bool IsGameOver { get; set; }

        public void Play(List<string> listOfMoves)
        {
            //initialize board
            board = new Board();

            foreach (var moveAN in listOfMoves)
            {
                var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);
                Console.WriteLine(currentPlayer + " player " + move.PieceName + " moves to " + move.Coordinates);
                NextTurn(currentPlayer, moveAN);

                IsGameOver = board.GetWin;
                
                MovesCounter++;
                if (IsGameOver)
                {
                    Console.WriteLine("game over");
                    break;
                }
                
            } 
        }
   
        private void NextTurn(PieceColor player, string moveAN)
        {
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, player);
            if (move.IsKingCastling || move.IsQueenCastling)  
            {
                Console.WriteLine("Performing castling.");
                return;
            }
            var piece = board.PlayMove(moveAN, player);
            if (move.IsCheck)
            {
                //verify if king is actually in check
                Console.WriteLine(currentPlayer + " puts opponent king in check!");
            }
            if (move.IsCheckMate)
            {
                Winner = move.Color;
                Console.WriteLine(Winner + " won!");
            }

            currentPlayer = currentPlayer == PieceColor.White ? PieceColor.Black : PieceColor.White;

        }
    }
}
