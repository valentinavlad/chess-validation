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

            var pieceTypeKnight = ConvertAMoveIntoACellInstance.ConvertPieceInitialFromMoveToPieceName(move3);
            var pieceTypePawn = ConvertAMoveIntoACellInstance.ConvertPieceInitialFromMoveToPieceName(move1);
            var pieceTypeBishop = ConvertAMoveIntoACellInstance.ConvertPieceInitialFromMoveToPieceName(move4);

            Assert.Equal(PieceName.Bishop, pieceTypeBishop);
            Assert.Equal(PieceName.Pawn, pieceTypePawn);
            Assert.Equal(PieceName.Knight, pieceTypeKnight);
        }
    }
}
