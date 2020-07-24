using ChessTable;
using ChessTests.Pieces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ChessTests.Tests.PiecesTest
{
    public class KingTests
    {
        [Theory]
        [InlineData("d2", "Kc1", PieceColor.Black)]
       // [InlineData("f5", "Rf3", PieceColor.Black)]
        public void FindAndMoveKingWithNoObstacles(string kingCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(kingCoords, new King(currentPlayer));
            
            //Act
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, currentPlayer);

            var king = board.PlayMove(moveAN, currentPlayer);

            //Assert
            Assert.Equal(king, board.CellAt(move.Coordinate).Piece);
            Assert.IsType<King>(king);
            Assert.Null(board.CellAt("d2").Piece);
        }

        [Theory]
        [InlineData("e6", "d5", "Kd5", PieceColor.Black)]
        public void FindKingOnAllRoutesWithObstacleShouldThrowError(string kingCoords, string obstacleCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(kingCoords, new King(currentPlayer));
            board.AddPiece(obstacleCoords, new Pawn(PieceColor.Black));


            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, currentPlayer);

          
            Action exception = () => board.FindPieceWhoNeedsToBeMoved(move, currentPlayer);

            Assert.Throws<InvalidOperationException>(exception);

        }

        [Theory]
        [InlineData("e6", "Kxd5", PieceColor.Black)]
        public void FindKingOnAllRoutesWithCapture(string kingCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(kingCoords, new King(currentPlayer));
            board.AddPiece("d5", new Pawn(PieceColor.White));

            //Act
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, currentPlayer);


            var king = board.PlayMove(moveAN, currentPlayer);

            //Assert
            Assert.Equal(king, board.CellAt(move.Coordinate).Piece);
            Assert.Null(board.CellAt("e6").Piece);
            Assert.IsType<King>(king);

        }
    }
}
