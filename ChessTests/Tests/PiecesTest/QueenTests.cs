using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ChessTests.Tests.PiecesTest
{
    public class QueenTests
    {
        [Theory]
        //find queen on diagonal left-down, no obstacles
        [InlineData("d1", "Qf3", PieceColor.White)]

        //find queen on diagonal right-down, no obstacles
        [InlineData("h1", "Qf3", PieceColor.White)]

        //find queen on diagonal right-up, no obstacles
        [InlineData("h5", "Qf3", PieceColor.White)]

        //find queen on diagonal left-up, no obstacles
        [InlineData("c6", "Qf3", PieceColor.White)]

        //find queen horizintal left, no obstacles
        [InlineData("b3", "Qf3", PieceColor.White)]

        //find queen horizintal right, no obstacles
        [InlineData("h3", "Qf3", PieceColor.White)]

        //find queen vertical up, no obstacles
        [InlineData("f6", "Qf3", PieceColor.White)]

        //find queen vertical down no obstacles
        [InlineData("f1", "Qf3", PieceColor.White)]
        public void FindWhiteQueenOnAllRoutesNoObstacleShouldReturnQueen(string queenCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(queenCoords, new Queen(currentPlayer));

            //find queen on diagonal right-down
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, currentPlayer);

            var queen = board.FindPieceWhoNeedsToBeMoved(move, PieceColor.White);

            Assert.IsType<Queen>(queen);
        }

        [Theory]
        //find queen on diagonal left-down, with obstacles
        [InlineData("d1", "e2", "Qf3", PieceColor.White)]

        //find queen on diagonal right-down, with obstacles
        [InlineData("h1", "g2", "Qf3", PieceColor.White)]

        //find queen on diagonal right-up, with obstacles
        [InlineData("h5", "g4", "Qf3", PieceColor.White)]

        //find queen on diagonal left-up, with obstacles
        [InlineData("c6", "d5", "Qf3", PieceColor.White)]

        //find queen horizintal left, with obstacles
        [InlineData("b3", "d3", "Qf3", PieceColor.White)]

        //find queen horizintal right, with obstacles
        [InlineData("h3", "g3", "Qf3", PieceColor.White)]

        //find queen vertical up, with obstacles
        [InlineData("f6", "f5", "Qf3", PieceColor.White)]

        //find queen vertical down with obstacles
        [InlineData("f1", "f2", "Qf3", PieceColor.White)]
        public void FindWhiteQueenOnAllRoutesWithObstacleShouldReturnNull(string queenCoords, string obstacleCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(queenCoords, new Queen(currentPlayer));
            board.AddPiece(obstacleCoords, new Pawn(currentPlayer));


            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, currentPlayer);

            var queen = board.FindPieceWhoNeedsToBeMoved(move, currentPlayer);

            Assert.Null(queen);

        }

        [Theory]
        //find queen on diagonal left-down, no obstacles
        [InlineData("d1", "Qf3", PieceColor.White)]
        public void MoveWhiteQueen(string queenCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(queenCoords, new Queen(currentPlayer));

            //find queen on diagonal right-down
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, currentPlayer);
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);

            var queen = board.PlayMove(moveAN, currentPlayer);


            Assert.Equal(queen, board.CellAt("f3").Piece);
        }
        //TO DO test queen move with capture

        [Theory]
        //find queen on diagonal left-down, no obstacles
        [InlineData("f5", "d5", "Qxd5", PieceColor.White)]
        public void WhiteQueenCapture(string queenCoords, string opponentCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(queenCoords, new Queen(currentPlayer));
            board.AddPiece(opponentCoords, new Bishop(PieceColor.Black));

            Piece queen = board.PlayMove(moveAN, currentPlayer);

            Assert.Equal(queen, board.CellAt("d5").Piece);
        }

        //ambiguous moves regarding the white queen
        [Fact]
        public void FindPieceWhoNeedsToBeMovedWithTwoWhiteQueensShouldReturnTheRightQueen()
        {
            var board = new Board(false);
            board.AddPiece("g4", new Queen(PieceColor.White));
            board.AddPiece("c6", new Queen(PieceColor.White));
            board.AddPiece("d3", new Pawn(PieceColor.White));
            var moveAN = "Qce4";
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, PieceColor.White);

            Piece queen = board.PlayMove(moveAN, PieceColor.White);

            Assert.Equal(queen, board.CellAt("e4").Piece);
        }

    }
}
