using ChessTable;
using System;
using Xunit;

namespace ChessTests
{
    public class UnitTest1
    {
        [Fact]
        public void ReadFromFileTest()
        {
            var listOfMoves = ReadFromFile.ProcessFile("chess-moves.txt");

            string move = "Be5";
            var res = ReadFromFile.Test(move);
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
