using ChessTable;
using ChessTests.Pieces;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ChessTests
{
    public class UnitTest1
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
        public void ReadFromFileShoudReturnListOfMoves()
        {
            var listOfMoves = ReadFromFile.ProcessFile("chess-moves.txt");
            
            string move1 = listOfMoves[0];
            string move2 = listOfMoves[1];
            string move3 = listOfMoves[2];

            Assert.Equal("e4", move1);
            Assert.Equal("e5", move2);
            Assert.Equal("Nf3", move3);
        }

        [Fact]
        public void ConvertPieceInitialFromMoveToPieceNameShouldReturnPieceName()
        {
            var listOfMoves = ReadFromFile.ProcessFile("chess-moves.txt");

            string move1 = listOfMoves[0];
            string move3 = listOfMoves[2];
            string move4 = listOfMoves[4];

            var pieceTypeKnight = ConvertAMoveIntoACellInstance.ConvertPieceInitialFromMoveToPieceName(move3);
            var pieceTypePawn = ConvertAMoveIntoACellInstance.ConvertPieceInitialFromMoveToPieceName(move1);
            var pieceTypeBishop = ConvertAMoveIntoACellInstance.ConvertPieceInitialFromMoveToPieceName(move4);

            Assert.Equal(PieceName.Bishop, pieceTypeBishop);
            Assert.Equal(PieceName.Pawn, pieceTypePawn);
            Assert.Equal(PieceName.Knight , pieceTypeKnight);
        }

        [Fact]
        public void InitializeBoardShoudReturnPiecesInInitialPosition()
        {
            var board = new Board();

            var cell = board.TransformCoordonatesIntoCell("e4");

            var pawnOnInitialPos = board.TransformCoordonatesIntoCell("e2");
            
            Assert.Null(cell.Piece);
            Assert.NotNull(pawnOnInitialPos.Piece);
        }

        [Fact]
        public void CoordonateXOutOfBoundShouldThrowError()
        {
            var board = new Board();

            Action exception = () => board.TransformCoordonatesIntoCell("08");
            Assert.Throws<IndexOutOfRangeException>(exception);
        }


        [Fact]
        public void CoordonatYsOutOfBoundShouldThrowError()
        {
            var board = new Board();

            Action exception = () => board.TransformCoordonatesIntoCell("80");
            Assert.Throws<IndexOutOfRangeException>(exception);
        }

        [Fact]
        public void FindWhitePawnWhoNeedsToBeMovedTest()
        {
            var board = new Board();
            var cell = board.TransformCoordonatesIntoCell("e3");
            var player = PieceColor.White;
            var piece = board.FindPieceWhoNeedsToBeMoved("e3", player);

            Assert.NotNull(piece);
            Assert.IsType<Pawn>(piece);


            var cell2 = board.TransformCoordonatesIntoCell("e4");
            var piece2 = board.FindPieceWhoNeedsToBeMoved("e4", player);

            Assert.NotNull(piece2);
            Assert.IsType<Pawn>(piece2);
            Assert.Equal(PieceColor.White, piece2.pieceColor);
        }

        [Fact]
        public void FindBlackPawnWhoNeedsToBeMovedTest()
        {
            var board = new Board();
            var cell = board.TransformCoordonatesIntoCell("e6");
            var player = PieceColor.Black;
            var piece = board.FindPieceWhoNeedsToBeMoved("e6", player);

            Assert.NotNull(piece);
            Assert.IsType<Pawn>(piece);

            var cell2 = board.TransformCoordonatesIntoCell("e5");
            var piece2 = board.FindPieceWhoNeedsToBeMoved("e5", player);

            Assert.NotNull(piece2);
            Assert.IsType<Pawn>(piece2);
            Assert.Equal(PieceColor.Black, piece2.pieceColor);
        }


        [Fact]
        public void IsOnInitialPositionShouldReturnTRue()
        {
            var board = new Board();

            var pawn = board.cells[6, 0].Piece;
          
            Assert.True(pawn.IsOnInitialPosition());

            //to do move
            var cell = board.TransformCoordonatesIntoCell("a4");
            board.Move(pawn, cell);
            Assert.False(pawn.IsOnInitialPosition());
        }

        [Fact]
        public void Move()
        {
            var board = new Board();
            var pawn = board.cells[6, 4].Piece;
            var cell = board.TransformCoordonatesIntoCell("e4");
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
        public void WhitePawnCannotMoveBackwords()
        {
            var board = new Board();
            var pawn = board.CellAt("e2").Piece;
            var cell = board.CellAt("e4");
            board.Move(pawn, cell);

            Action exception = () => board.FindPieceWhoNeedsToBeMoved("e3", PieceColor.White);
            Assert.Throws<InvalidOperationException>(exception);
        }


        [Fact]
        public void MoveWhitePawnASquareForward()
        {
            var board = new Board();
            var pawn = board.CellAt("e2").Piece;
            board.Move(pawn, board.CellAt("e4"));

            
            Assert.Equal(pawn, board.FindPieceWhoNeedsToBeMoved("e5", PieceColor.White));
   
        }

       
        [Fact]
        public void PromoteWhitePawn()
        {
            //promote a pawn with no capture
            var move = "d1Q";
            var board = new Board(false);
            //  var pawnBlack = board.CellAt("e2").Piece;
            var pawnBlack = board.AddPiece("d2", new Pawn(PieceColor.Black));
            var cell = board.CellAt("d2");

            Assert.Equal(pawnBlack, cell.Piece);
        }

        [Fact]
        public void CheckRegexForMovePawnToPromotionShouldReturnNewPiece()
        {
            var notation = "e8Q++";
            var color = PieceColor.White;
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(notation, color);

            Assert.NotNull(move.Coordinate);
            Assert.NotNull(move.Promotion);
            Assert.IsType<Queen>(move.Promotion);
            Assert.Equal(color, move.Promotion.pieceColor);
            Assert.False(move.IsCheck);
            Assert.True(move.IsCheckMate);
        }

        [Fact]
        public void CheckRegexForMoveBishopShouldReturn()
        {
            var notation = "Be5+";
            var color = PieceColor.White;
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(notation, color);

            Assert.NotNull(move.Coordinate);
            Assert.Null(move.Promotion);
           
            Assert.Equal(color, move.Color);
            Assert.Equal(PieceName.Bishop, move.PieceName);
            Assert.True(move.IsCheck);
            Assert.False(move.IsCheckMate);
        }

        [Fact]
        public void CheckRegexForMoveBishopWithCaptureShouldReturn()
        {
            var notation = "Bxe5";
            var color = PieceColor.White;
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(notation, color);

            Assert.NotNull(move.Coordinate);
            Assert.Null(move.Promotion);

            Assert.Equal(color, move.Color);
            Assert.Equal(PieceName.Bishop, move.PieceName);
            Assert.False(move.IsCheck);
            Assert.False(move.IsCheckMate);
            Assert.True(move.IsCapture);
        }

        [Fact]
        public void CheckRegexForMovePawnWithCaptureAndPromotionShouldReturnNewPiece()
        {
            var notation = "exd8Q+";
            var color = PieceColor.White;
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(notation, color);

            Assert.NotNull(move.Coordinate);
            Assert.NotNull(move.Promotion);
            Assert.IsType<Queen>(move.Promotion);
            Assert.Equal(color, move.Promotion.pieceColor);
            Assert.True(move.IsCheck);
            Assert.False(move.IsCheckMate);
            Assert.True(move.IsCapture);
        }
    }
}
