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

            var cell = ConvertAMoveIntoACellInstance.ConvertChessCoordonatesToArrayIndexes(move1);

            var board = new Board();
            //add a piece on table

            board.AddPieceInCell(cell);


            Assert.NotNull(cell);
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
