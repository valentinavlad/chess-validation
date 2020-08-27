using ChessTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
namespace ChessTests.Tests.PiecesTest
{
    public class BishopTests
    {
        [Theory]
        //find bishop on diagonal right-down, no obstacles
        [InlineData("c8", "Bg4", PieceColor.Black)]
        [InlineData("d1", "Bf3", PieceColor.White)]
        public void MoveBishop(string bishopCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(bishopCoords, new Bishop(currentPlayer));

            //Act
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            var bishop = board.PlayMove(moveAN, currentPlayer);

            //Assert
            Assert.Equal(bishop, board.CellAt(move.Coordinate).Piece);
            Assert.IsType<Bishop>(bishop);
        }

        [Theory]
        [InlineData("c5", "d4", "Be3", PieceColor.Black)]
        public void FindWhiteBishopOnAllRoutesWithObstacleShouldReturnNull(string bishopCoords, string obstacleCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(bishopCoords, new Bishop(currentPlayer));
            board.AddPiece(obstacleCoords, new Pawn(PieceColor.White));

            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            var bishop = board.FindPieceWhoNeedsToBeMoved(move);

            Assert.False(bishop);

        }

        [Theory]
        //find bishop on diagonal right-down, no obstacles

        [InlineData("f4", "Bxd6", PieceColor.White)]
        public void MoveBishopWithCapture(string bishopCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(bishopCoords, new Bishop(currentPlayer));
            board.AddPiece("d6", new Pawn(PieceColor.Black));

            //Act
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);
            var list = board.Pieces;
            var bishop = board.PlayMove(moveAN, currentPlayer);
            int x = list.Count();
            //Assert
            Assert.Equal(bishop, board.CellAt(move.Coordinate).Piece);
            Assert.IsType<Bishop>(bishop);
        }

        [Theory]

        [InlineData("c5", "d6", "Bce7", PieceColor.Black)]
        public void MoveBishopWithObstacleShouldReturnNull(string bishopCoords, string obstacleCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(bishopCoords, new Bishop(currentPlayer));
            board.AddPiece(obstacleCoords, new Bishop(PieceColor.White));

            var bishop = board.FindPieceWhoNeedsToBeMoved(moveAN, currentPlayer);

            //Assert
            Assert.False(bishop);

        }

        //ambiguous moves regarding the black bishop
        [Fact]
        public void FindPieceWhoNeedsToBeMovedWithTwoBlackBishopsShouldReturnTheRightBishop()
        {
            var board = new Board(false);

            board.AddPiece("a7", new Bishop(PieceColor.Black));
            board.AddPiece("d6", new Bishop(PieceColor.Black));

            var moveAN = "Bdb8";

            Piece bishop = board.PlayMove(moveAN, PieceColor.Black);

            Assert.Equal(bishop, board.CellAt("b8").Piece);
        }
    }
}
