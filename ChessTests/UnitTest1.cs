using ChessTable;
using System;
using System.Linq;
using Xunit;

namespace ChessTests
{
    public class UnitTest1
    {
        [Fact]
        public void ReadFromFileTest()
        {
            var listOfMoves = ReadFromFile.ProcessFile("chess-moves.txt");
            string move1 = listOfMoves[0].BlackMoves;
            string move2 = listOfMoves[0].WhiteMoves;

            var square = ReadFromFile.ConvertChessCoordonatesToArrayIndexes(move1);

            Assert.NotNull(square);
            Assert.Null(listOfMoves);
        }
        [Fact]
        public void Test1()
        {
            var board = new Board();
            
            Assert.Null(board);
        }
    }
}
