using ChessTable;
using ChessTests.Pieces;
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
        public void FindAndMoveKnightWithNoObstacles(string knightCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(knightCoords, new Knight(currentPlayer));
        

            //Act
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            var knight = board.PlayMove(moveAN, currentPlayer);

            //Assert
            Assert.Equal(knight, action.CellAt(move.Coordinate).Piece);
            Assert.IsType<Knight>(knight);
            Assert.Null(action.CellAt("d5").Piece);
        }

        //TO DO test for move with obstacle
        [Theory]
        [InlineData("d5", "e7", "Ne7", PieceColor.Black)]
        public void FindAndMoveKnightWithObstaclesShouldThrowError(string knightCoords,string obstacleCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(knightCoords, new Knight(currentPlayer));
            action.AddPiece(obstacleCoords, new Bishop(currentPlayer));

            //Act
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            //Assert
            Action exception = () => board.FindPieceWhoNeedsToBeMoved(moveAN, currentPlayer);

            Assert.Throws<InvalidOperationException>(exception);

        }

        //TO DO for move with capture
        [Theory]
        [InlineData("d5", "e7", "Nb4", PieceColor.Black)]
        public void FindAndMoveKnightWithCapture(string knightCoords, string obstacleCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(knightCoords, new Knight(currentPlayer));
            action.AddPiece(obstacleCoords, new Bishop(currentPlayer));
            action.AddPiece("b4", new Rook(PieceColor.White));

            //Act
          
            Piece knight = board.PlayMove(moveAN, currentPlayer);

            //Assert
            Assert.Equal(knight, action.CellAt("b4").Piece);
        }

        //TO DO for ambiguous moves
        [Theory]
        [InlineData("d5", "a6", "Naxb4", PieceColor.Black)]
        public void FindAndMoveKnightWithAmbiguousMove(string knightCoords, string knightCoords2, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(knightCoords, new Knight(currentPlayer));
            action.AddPiece(knightCoords2, new Knight(currentPlayer));
            action.AddPiece("b4", new Rook(PieceColor.White));

            //Act
            Piece knight = board.PlayMove(moveAN, currentPlayer);
          
            //Assert
             Assert.Equal(knight, action.CellAt("b4").Piece);
        }

        [Theory]
        [InlineData("d5", "a6", "Naxb4", PieceColor.Black)]
        public void FindAndMoveKnightWithAmbiguousMove2(string knightCoords, string knightCoords2, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(knightCoords, new Knight(currentPlayer));
            var knight1 = action.AddPiece(knightCoords2, new Knight(currentPlayer));
            action.AddPiece("b4", new Rook(PieceColor.White));

            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);
            //Assert
            Assert.Equal(knight1.CurrentPosition.Y, move.Y);

        }
    }
}
