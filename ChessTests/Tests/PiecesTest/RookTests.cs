using ChessTable;
using ChessTests.Pieces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ChessTests.Tests.PiecesTest
{
    public class RookTests
    {
        [Theory]
        [InlineData("a4", "Re4", PieceColor.White)]
        [InlineData("f5", "Rf3", PieceColor.Black)]
        public void FindMoveRookWithNoObstacles(string rooksCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(rooksCoords, new Rook(currentPlayer));

            //Act
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);
   
            var rook = board.PlayMove(moveAN, currentPlayer);

            //Assert
            Assert.Equal(rook, board.CellAt(move.Coordinate).Piece);
            Assert.IsType<Rook>(rook);
        }

        [Theory]
        [InlineData("f5", "d5", "Rb5", PieceColor.Black)]
        public void FindRookOnAllRoutesWithObstacleShouldReturnNull(string rookCoords, string obstacleCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(rookCoords, new Bishop(currentPlayer));
            action.AddPiece(obstacleCoords, new Pawn(PieceColor.Black));

            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            var rook = board.FindPieceWhoNeedsToBeMoved(move, currentPlayer);

            Assert.Null(rook);

        }

        [Theory]
        [InlineData("f5", "Rxd5", PieceColor.Black)]
        public void MoveRookWithCapture(string rookCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(rookCoords, new Rook(currentPlayer));
            action.AddPiece("d5", new Pawn(PieceColor.White));

            //Act
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);
           
            var rook = board.PlayMove(moveAN, currentPlayer);

            //Assert
            Assert.Equal(rook, board.CellAt(move.Coordinate).Piece);
            Assert.Null(board.CellAt("f5").Piece);
            Assert.IsType<Rook>(rook);
        }

        [Fact]
        public void FindPieceWhoNeedsToBeMovedWithTwoBlackRooksShouldReturnTheRightRook()
        {
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece("b5", new Rook(PieceColor.Black));
            action.AddPiece("f5", new Rook(PieceColor.Black));

            var moveAN = "Rbd5";

            Piece rook = board.PlayMove(moveAN, PieceColor.Black);

            Assert.Equal(rook, board.CellAt("d5").Piece);
            Assert.Null(board.CellAt("b5").Piece);
        }


    }
}
