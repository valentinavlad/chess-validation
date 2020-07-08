using ChessTable;
using System;
using Xunit;

namespace ChessTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var board = new Board();
            Assert.Null(board);
        }
    }
}
