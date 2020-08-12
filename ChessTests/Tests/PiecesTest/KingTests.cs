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
            var action = new Helpers.Action(board);
            action.AddPiece(kingCoords, new King(currentPlayer));
            
            //Act
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            var king = board.PlayMove(moveAN, currentPlayer);

            //Assert
            Assert.Equal(king, action.CellAt(move.Coordinate).Piece);
            Assert.IsType<King>(king);
            Assert.Null(action.CellAt("d2").Piece);
        }

        [Theory]
        [InlineData("e6", "d5", "Kd5", PieceColor.Black)]
        public void FindKingOnAllRoutesWithObstacleShouldThrowError(string kingCoords, string obstacleCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(kingCoords, new King(currentPlayer));
            action.AddPiece(obstacleCoords, new Pawn(PieceColor.Black));


            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

          
            Action exception = () => board.FindPieceWhoNeedsToBeMoved(move);

            Assert.Throws<InvalidOperationException>(exception);

        }

        [Theory]
        [InlineData("e6", "Kxd5", PieceColor.Black)]
        public void FindKingOnAllRoutesWithCapture(string kingCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(kingCoords, new King(currentPlayer));
            action.AddPiece("d5", new Pawn(PieceColor.White));

            //Act
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            var king = board.PlayMove(moveAN, currentPlayer);

            //Assert
            Assert.Equal(king, action.CellAt(move.Coordinate).Piece);
            Assert.Null(action.CellAt("e6").Piece);
            Assert.IsType<King>(king);

        }

        [Fact]
        public void WhiteQueenCastling()
        {
            var board = new Board();
            var action = new Helpers.Action(board);
            board.PlayMove("d4", PieceColor.White);
            board.PlayMove("e6", PieceColor.Black);

            board.PlayMove("Bf4", PieceColor.White);
            board.PlayMove("g6", PieceColor.Black);

            board.PlayMove("Nc3", PieceColor.White);
            board.PlayMove("b6", PieceColor.Black);

            board.PlayMove("Qd3", PieceColor.White);
            board.PlayMove("b5", PieceColor.Black);

            board.PlayMove("0-0-0", PieceColor.White);

            Assert.IsType<King>(action.CellAt("c1").Piece);
            Assert.IsType<Rook>(action.CellAt("d1").Piece);
            Assert.Null(action.CellAt("a1").Piece);
            Assert.Null(action.CellAt("e1").Piece);
        }

        [Fact]
        public void WhiteKingCastling()
        {
            var board = new Board();
            var action = new Helpers.Action(board);

            board.PlayMove("e3", PieceColor.White);
            board.PlayMove("e5", PieceColor.Black);

            board.PlayMove("Bd3", PieceColor.White);
            board.PlayMove("f5", PieceColor.Black);

            board.PlayMove("Nh3", PieceColor.White);
            board.PlayMove("g5", PieceColor.Black);

            board.PlayMove("0-0", PieceColor.White);
            board.PlayMove("d6", PieceColor.Black);


            Assert.IsType<King>(action.CellAt("g1").Piece);
            Assert.IsType<Rook>(action.CellAt("f1").Piece);
            Assert.Null(action.CellAt("e1").Piece);
            Assert.Null(action.CellAt("h1").Piece);
        } 

        [Fact]
        public void BlackQueenCastling()
        {
            var board = new Board();
            var action = new Helpers.Action(board);
            board.PlayMove("c4", PieceColor.White);
            board.PlayMove("d6", PieceColor.Black);

            board.PlayMove("d3", PieceColor.White);
            board.PlayMove("Bf5", PieceColor.Black);

            board.PlayMove("e4", PieceColor.White);
            board.PlayMove("Nc6", PieceColor.Black);

            board.PlayMove("b3", PieceColor.White);
            board.PlayMove("Qd7", PieceColor.Black);

            board.PlayMove("f3", PieceColor.White);
            board.PlayMove("0-0-0", PieceColor.Black);


            Assert.IsType<King>(action.CellAt("c8").Piece);
            Assert.IsType<Rook>(action.CellAt("d8").Piece);
            Assert.Null(action.CellAt("a8").Piece);
            Assert.Null(action.CellAt("e8").Piece);
        }

        [Fact]
        public void WhiteKingCastlingPiecesMovedShouldThrowException()
        {
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece("a1", new Rook(PieceColor.White));
            action.AddPiece("e1", new King(PieceColor.White));

            var move = MoveNotationConverter.ParseMoveNotation("0-0", PieceColor.White);

          
            Action exception = () => board.PlayMove("0-0", PieceColor.White);

            Assert.Throws<InvalidOperationException>(exception);
        }

        [Fact]
        public void WhiteKingCastlingWithObstacle()
        {
            var board = new Board();


            board.PlayMove("d4", PieceColor.White);
            board.PlayMove("e6", PieceColor.Black);

            board.PlayMove("Bf4", PieceColor.White);
            board.PlayMove("g6", PieceColor.Black);

            board.PlayMove("b3", PieceColor.White);
            board.PlayMove("b6", PieceColor.Black);

            board.PlayMove("Qd3", PieceColor.White);
            board.PlayMove("b5", PieceColor.Black);

        

            Action exception = () => board.PlayMove("0-0", PieceColor.White);

            Assert.Throws<InvalidOperationException>(exception);
        }
    }
}
