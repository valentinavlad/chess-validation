using ChessTable;
using ChessTests.Pieces;
using System;
using Xunit;

namespace ChessTests
{
    public class BoardTests
    {
        [Fact]
        public void InitialBoardShouldHavePiecesOnPLace()
        {
            var board = new Board();     
            var rook = board.CellAt("a8").Piece;

            Assert.NotNull(rook);
            Assert.IsType<Rook>(rook);
            Assert.Equal(PieceColor.Black, rook.pieceColor);

            var rookWhite = board.CellAt("h1").Piece;

            Assert.NotNull(rookWhite);
            Assert.IsType<Rook>(rookWhite);
            Assert.Equal(PieceColor.White, rookWhite.pieceColor);
        }

        [Fact]
        public void InitializeBoardShoudReturnPiecesInInitialPosition()
        {
            var board = new Board();

            var cell = board.CellAt("e4");

            var pawnOnInitialPos = board.CellAt("e2");

            Assert.Null(cell.Piece);
            Assert.NotNull(pawnOnInitialPos.Piece);
        }

        [Fact]
        public void CoordonateXOutOfBoundShouldThrowError()
        {
            var board = new Board();

            Action exception = () => board.CellAt("a9");
            Assert.Throws<IndexOutOfRangeException>(exception);
        }

        [Fact]
        public void CoordonatYsOutOfBoundShouldThrowError()
        {
            var board = new Board();

            Action exception = () => board.CellAt("i8");
            Assert.Throws<IndexOutOfRangeException>(exception);
        }

        [Fact]
        public void IsOnInitialPositionShouldReturnTRue()
        {
            var board = new Board();

            var pawn = board.CellAt("a2").Piece;

            Assert.True(pawn.IsOnInitialPosition());

            //to do move
            var cell = board.CellAt("a4");
            var move = MoveNotationConverter.ParseMoveNotation("a4", PieceColor.White);
            move.MovePiece(pawn, cell);
            Assert.False(pawn.IsOnInitialPosition());
        }

        [Fact]
        public void Move()
        {
            var board = new Board();
            var pawn = board.CellAt("c2").Piece;
            var cell = board.CellAt("e4");
            var move = MoveNotationConverter.ParseMoveNotation("a4", PieceColor.White);
            move.MovePiece(pawn, cell);

            Assert.Equal(pawn, cell.Piece);
            Assert.Null(board.CellAt("c2").Piece);
        }

        [Fact]
        public void FindPieceWhoNeedsToBeMovedShouldThrowAnException()
        {
            var board = new Board();
            var pawn = board.CellAt("e2").Piece;
            var cell = board.CellAt("e3");
            var move = MoveNotationConverter.ParseMoveNotation("a4", PieceColor.White);
            move.MovePiece(pawn, cell);

            Action exception = () => board.FindPieceWhoNeedsToBeMoved("e5", PieceColor.White);
            Assert.Throws<InvalidOperationException>(exception);
        }

        [Fact]
        public void Test()
        {
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece("c3", new Knight(PieceColor.Black));
            action.AddPiece("c2", new Pawn(PieceColor.White));
            var moveAN = "c3";
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, PieceColor.White);

            Action exception = () => board.FindPieceWhoNeedsToBeMoved(move, PieceColor.White);
            Assert.Throws<InvalidOperationException>(exception);
        }

        [Fact]
        public void ChessMovesTXT2()
        {
            var board = new Board();


            board.PlayMove("e4", PieceColor.White);
            board.PlayMove("e5", PieceColor.Black);

            board.PlayMove("Nf3", PieceColor.White);
            board.PlayMove("d6", PieceColor.Black);

            board.PlayMove("d4", PieceColor.White);
            board.PlayMove("Bg4", PieceColor.Black);

            board.PlayMove("dxe5", PieceColor.White);
            board.PlayMove("Bxf3", PieceColor.Black);

            board.PlayMove("Qxf3", PieceColor.White);
            board.PlayMove("dxe5", PieceColor.Black);

            board.PlayMove("Bc4", PieceColor.White);
            board.PlayMove("Nf6", PieceColor.Black);//xx

            board.PlayMove("Qb3", PieceColor.White);
            board.PlayMove("Qe7", PieceColor.Black);

            board.PlayMove("Nc3", PieceColor.White);
            board.PlayMove("c6", PieceColor.Black);

            board.PlayMove("Bg5", PieceColor.White);
            board.PlayMove("b5", PieceColor.Black);

            board.PlayMove("Nxb5", PieceColor.White);
            board.PlayMove("cxb5", PieceColor.Black);

            board.PlayMove("Bxb5+", PieceColor.White);
            board.PlayMove("Nbd7", PieceColor.Black);

            board.PlayMove("0-0-0", PieceColor.White);
            board.PlayMove("Rd8", PieceColor.Black);

            board.PlayMove("Rxd7", PieceColor.White);
            board.PlayMove("Rxd7", PieceColor.Black);

            board.PlayMove("Rd1", PieceColor.White);
            board.PlayMove("Qe6", PieceColor.Black);

            board.PlayMove("Bxd7+", PieceColor.White);
            board.PlayMove("Nxd7", PieceColor.Black);

            board.PlayMove("Qb8+", PieceColor.White);
            board.PlayMove("Nxb8", PieceColor.Black);

            board.PlayMove("Rd8++", PieceColor.White);

            Assert.True(board.GetWin);
        }


        [Fact]
        public void WhitePawnPutsKingInCheck()
        {
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece("e6", new Pawn(PieceColor.White));
            action.AddPiece("d7", new Pawn(PieceColor.Black));
            action.AddPiece("e8", new King(PieceColor.Black));

            board.PlayMove("exd7+", PieceColor.White);
            var move = MoveNotationConverter.ParseMoveNotation("exd7+", PieceColor.White);
            Assert.True(move.IsCheck);

        }
        [Fact]
        public void ChessMovesTXT3()
        {
            var board = new Board();


            board.PlayMove("e4", PieceColor.White);
            board.PlayMove("e5", PieceColor.Black);

            board.PlayMove("c4", PieceColor.White);
            board.PlayMove("d5", PieceColor.Black);

            board.PlayMove("exd5", PieceColor.White);
            board.PlayMove("Bb4", PieceColor.Black);

            board.PlayMove("Bd3", PieceColor.White);
            board.PlayMove("c5", PieceColor.Black);

            board.PlayMove("d6", PieceColor.White);
            board.PlayMove("Qh4", PieceColor.Black);

            board.PlayMove("Nf3", PieceColor.White);
            board.PlayMove("Bxd2+", PieceColor.Black);

            board.PlayMove("Qxd2", PieceColor.White);
            board.PlayMove("a6", PieceColor.Black);

            board.PlayMove("Nxh4", PieceColor.White);
            board.PlayMove("Nf6", PieceColor.Black);

            board.PlayMove("d7+", PieceColor.White);
            board.PlayMove("0-0", PieceColor.Black);

            board.PlayMove("d8Q", PieceColor.White);
            board.PlayMove("a5", PieceColor.Black);


            void exception() => board.PlayMove("Qxf8++", PieceColor.White);
            Assert.Throws<InvalidOperationException>(exception);
        }

    }
}
