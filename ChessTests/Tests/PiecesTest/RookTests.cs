using ChessTable;
using ChessTests.Pieces;
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
            
            board.AddPiece(rooksCoords, new Rook(currentPlayer));

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
     
            board.AddPiece(rookCoords, new Bishop(currentPlayer));
            board.AddPiece(obstacleCoords, new Pawn(PieceColor.Black));

            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            var rook = board.FindPieceWhoNeedsToBeMoved(move);

            Assert.False(rook);

        }

        [Theory]
        [InlineData("f5", "Rxd5", PieceColor.Black)]
        public void MoveRookWithCapture(string rookCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);

            board.AddPiece(rookCoords, new Rook(currentPlayer));
            board.AddPiece("d5", new Pawn(PieceColor.White));

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
           
            board.AddPiece("b5", new Rook(PieceColor.Black));
            board.AddPiece("f5", new Rook(PieceColor.Black));

            var moveAN = "Rbd5";

            Piece rook = board.PlayMove(moveAN, PieceColor.Black);

            Assert.Equal(rook, board.CellAt("d5").Piece);
            Assert.Null(board.CellAt("b5").Piece);
        }


    }
}
