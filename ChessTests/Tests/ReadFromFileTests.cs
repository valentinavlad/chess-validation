using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace ChessTests.Tests
{
    public class ReadFromFileTests
    {
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

            var pieceTypeKnight = MoveNotationConverter.ConvertPieceInitialFromMoveToPieceName(move3);
            var pieceTypePawn = MoveNotationConverter.ConvertPieceInitialFromMoveToPieceName(move1);
            var pieceTypeBishop = MoveNotationConverter.ConvertPieceInitialFromMoveToPieceName(move4);

            Assert.Equal(PieceName.Bishop, pieceTypeBishop);
            Assert.Equal(PieceName.Pawn, pieceTypePawn);
            Assert.Equal(PieceName.Knight, pieceTypeKnight);
        }

        [Fact]
        public void CheckRegexForKingCastlingShouldReturnNewPiece()
        {
            var notation = "0-0";
            var color = PieceColor.White;
            var move = MoveNotationConverter.ParseMoveNotation(notation, color);


            Assert.True(move.IsKingCastling);
            Assert.False(move.IsQueenCastling);
        }
        [Fact]
        public void CheckRegexForMovePawnToPromotionShouldReturnNewPiece()
        {
            var notation = "e8Q++";
            var color = PieceColor.White;
            var move = MoveNotationConverter.ParseMoveNotation(notation, color);

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
            var move = MoveNotationConverter.ParseMoveNotation(notation, color);

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
            var move = MoveNotationConverter.ParseMoveNotation(notation, color);

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
            var move = MoveNotationConverter.ParseMoveNotation(notation, color);

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
