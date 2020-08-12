using ChessTable;
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
            var action = new Helpers.Action(board);
            action.AddPiece(queenCoords, new Queen(currentPlayer));

            //find queen on diagonal right-down
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            //var queen = board.FindPieceWhoNeedsToBeMoved(move, PieceColor.White);
            var queen = board.PlayMove(moveAN, PieceColor.White);

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
            var action = new Helpers.Action(board);
            action.AddPiece(queenCoords, new Queen(currentPlayer));
            action.AddPiece(obstacleCoords, new Pawn(currentPlayer));

            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);
            var queen = board.FindPieceWhoNeedsToBeMoved(move, currentPlayer);

            Assert.False(queen);

        }

        [Theory]
        //find queen on diagonal left-down, no obstacles
        [InlineData("d1", "Qf3", PieceColor.White)]
        public void MoveWhiteQueen(string queenCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(queenCoords, new Queen(currentPlayer));

            var queen = board.PlayMove(moveAN, currentPlayer);

            Assert.Equal(queen, action.CellAt("f3").Piece);
        }

        [Theory]
        //find queen on diagonal left-down, no obstacles
        [InlineData("f5", "d5", "Qxd5", PieceColor.White)]
        public void WhiteQueenCapture(string queenCoords, string opponentCoords, string moveAN, PieceColor currentPlayer)
        {
            //Arange
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(queenCoords, new Queen(currentPlayer));
            action.AddPiece(opponentCoords, new Bishop(PieceColor.Black));

            Piece queen = board.PlayMove(moveAN, currentPlayer);

            Assert.Equal(queen, action.CellAt("d5").Piece);
        }

        //ambiguous moves regarding the white queen
        [Fact]
        public void FindPieceWhoNeedsToBeMovedWithTwoWhiteQueensShouldReturnTheRightQueen()
        {
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece("g4", new Queen(PieceColor.White));
            action.AddPiece("c6", new Queen(PieceColor.White));
            action.AddPiece("d3", new Pawn(PieceColor.White));
            var moveAN = "Qce4";
          
            Piece queen = board.PlayMove(moveAN, PieceColor.White);

            Assert.Equal(queen, action.CellAt("e4").Piece);
        }

    }
}
