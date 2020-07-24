using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ChessTests.Tests.PiecesTest
{
    public class KnightTests
    {
        [Theory]
        [InlineData("d5", "Ne7", PieceColor.Black)]
       // [InlineData("f5", "Rf3", PieceColor.Black)]
        public void FindAndMoveKnightWithNoObstacles(string knightCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(knightCoords, new Knight(currentPlayer));
        

            //Act
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, currentPlayer);

            var knight = board.PlayMove(moveAN, currentPlayer);

            //Assert
            Assert.Equal(knight, board.CellAt(move.Coordinate).Piece);
            Assert.IsType<Knight>(knight);
            Assert.Null(board.CellAt("d5").Piece);
        }

        //TO DO test for move with obstacle
        //TO DO for move with capture
        //TO DO for ambiguous moves
    }
}
