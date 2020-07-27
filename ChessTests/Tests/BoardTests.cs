using ChessTable;
using ChessTests.Pieces;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ChessTests
{
    public class BoardTests
    {
        [Fact]
        public void InitialBoardShouldHavePiecesOnPLace()
        {
            var board = new Board();
            var rook = board.cells[0, 0].Piece;

            Assert.NotNull(rook);
            Assert.IsType<Rook>(rook);
            Assert.Equal(PieceColor.Black, rook.pieceColor);

            var rookWhite = board.cells[7, 7].Piece;

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

            var pawn = board.cells[6, 0].Piece;
          
            Assert.True(pawn.IsOnInitialPosition());

            //to do move
            var cell = board.CellAt("a4");
            board.Move(pawn, cell);
            Assert.False(pawn.IsOnInitialPosition());
        }

        [Fact]
        public void Move()
        {
            var board = new Board();
            var pawn = board.cells[6, 4].Piece;
            var cell = board.CellAt("e4");
            board.Move(pawn, cell);

            Assert.Equal(pawn, cell.Piece);
            Assert.Null(board.cells[6, 4].Piece);
        }

        [Fact]
        public void FindPieceWhoNeedsToBeMovedShouldThrowAnException()
        {
            var board = new Board();
            var pawn = board.CellAt("e2").Piece;
            var cell = board.CellAt("e3");

            board.Move(pawn, cell);
            Action exception = () => board.FindPieceWhoNeedsToBeMoved("e5", PieceColor.White);
            Assert.Throws<InvalidOperationException>(exception);
        }

        [Fact]
        public void Test()
        {
            var board = new Board(false);
            board.AddPiece("c3", new Knight(PieceColor.Black));
            board.AddPiece("c2", new Pawn(PieceColor.White));
            var moveAN = "c3";
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, PieceColor.White);

            Action exception = () => board.FindPieceWhoNeedsToBeMoved(move, PieceColor.White);
            Assert.Throws<InvalidOperationException>(exception);
        }

        //[Fact]
        //public void Tset()
        //{
        //    var board = new Board();


        //    board.PlayMove("e4", PieceColor.White);
        //    board.PlayMove("e5", PieceColor.Black);

        //    board.PlayMove("Nf3", PieceColor.White);
        //    board.PlayMove("Nc6", PieceColor.Black);

        //    board.PlayMove("Bb5", PieceColor.White);
        //    board.PlayMove("Nf6", PieceColor.Black);

        //    board.PlayMove("Nc3", PieceColor.White);
        //    board.PlayMove("Bc5", PieceColor.Black);

        //    board.PlayMove("0-0", PieceColor.White);
        //    board.PlayMove("d5", PieceColor.Black);

        //    board.PlayMove("exd5", PieceColor.White);
        //    board.PlayMove("Nxd5", PieceColor.Black);//xx

        //    board.PlayMove("Nxd5", PieceColor.White);
        //    board.PlayMove("Qxd5", PieceColor.Black);

        //    board.PlayMove("Bxc6+", PieceColor.White);
        //    board.PlayMove("bxc6", PieceColor.Black);

        //    board.PlayMove("c3", PieceColor.White);
        //    board.PlayMove("0-0", PieceColor.Black);

        //    board.PlayMove("Ng5", PieceColor.White);
        //    board.PlayMove("e4", PieceColor.Black);

        //    board.PlayMove("d4", PieceColor.White);
        //    board.PlayMove("exd3", PieceColor.Black);

        //    board.PlayMove("Qf3", PieceColor.White);
        //    board.PlayMove("d2", PieceColor.Black);

        //    board.PlayMove("Qxd5", PieceColor.White);
        //    board.PlayMove("dxc1Q", PieceColor.Black);

        //    board.PlayMove("Raxc1", PieceColor.White);
        //    board.PlayMove("cxd5", PieceColor.Black);

        //    board.PlayMove("Kh1", PieceColor.White);
        //    board.PlayMove("Bb7", PieceColor.Black);

        //    board.PlayMove("f4", PieceColor.White);
        //    board.PlayMove("Rfe8", PieceColor.Black);

        //    board.PlayMove("Nh3", PieceColor.White);
        //    board.PlayMove("Rad8", PieceColor.Black);

        //    board.PlayMove("g3", PieceColor.White);
        //    board.PlayMove("Be3", PieceColor.Black);

        //    board.PlayMove("Rcd1", PieceColor.White);
        //    board.PlayMove("f6", PieceColor.Black);

        //    board.PlayMove("Rfe1", PieceColor.White);
        //    board.PlayMove("d4++", PieceColor.Black);


        //    //Assert.IsType<King>(board.CellAt("c1").Piece);
        //    //Assert.IsType<Rook>(board.CellAt("d1").Piece);
        //    //Assert.Null(board.CellAt("a1").Piece);
        //    //Assert.Null(board.CellAt("e1").Piece);
        //}

        [Fact]
        public void WhitePawnPutsKingInCheck()
        {
            var board = new Board(false);

            board.AddPiece("e6", new Pawn(PieceColor.White));
            board.AddPiece("d7", new Pawn(PieceColor.Black));
            board.AddPiece("e8", new King(PieceColor.Black));

            board.PlayMove("exd7+", PieceColor.White);
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation("exd7+", PieceColor.White);
            Assert.True(move.IsCheck);

        }


        [Fact]
        public void BlackKingCheckMate()
        {
            var board = new Board(false);

            board.AddPiece("d1", new Rook(PieceColor.White));
            board.AddPiece("g5", new Bishop(PieceColor.White));

            board.AddPiece("f8", new Bishop(PieceColor.Black));
            board.AddPiece("f7", new Pawn(PieceColor.Black));
            board.AddPiece("e6", new Queen(PieceColor.Black));
            board.AddPiece("e8", new King(PieceColor.Black));

            board.PlayMove("Rd8++", PieceColor.White);
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation("exd7+", PieceColor.White);
            Assert.True(move.IsCheckMate);

        }
    }
}
