using ChessTable;
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
        //lista piese capturate
        public void Play(List<string> listOfMoves)
        {
            //initialize board
            board = new Board();

            foreach (var moveAN in listOfMoves)
            {
                NextTurn(currentPlayer, moveAN);
                //Console.ReadLine();
                IsGameOver = board.GetWin;
                //TO DO daca mutarea sah mad invalida-> mesaj
                MovesCounter++;
                if (IsGameOver)
                {
                    //Console.WriteLine("game over");
                    break;
                }
            } 
        }

        private void NextTurn(PieceColor player, string moveAN)
        {
            board.PlayMove(moveAN, player);
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, player);
            if (move.IsCheckMate)
            {
                Winner = move.Color;
                //Console.WriteLine(Winner + " won!");
            }
            
            if (currentPlayer == PieceColor.White)
            {
                //TO DO override la tostring for move
                //Console.WriteLine("Black player " + move.PieceName + " moves to " + move.Coordinates);
                currentPlayer = PieceColor.Black;
            }
            else
            {
                currentPlayer = PieceColor.White;
                //Console.WriteLine("White player " + move.PieceName + " moves to " + move.Coordinates);
            }

        }
    }
}
