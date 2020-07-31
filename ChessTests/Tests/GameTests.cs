using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ChessTests.Tests
{
    public class GameTests
    {
        [Fact]
        public void Test()
        {
            var listOfMoves = new List<string>(){"e4","e5","g3" };
            var game = new Game();
            game.Play(listOfMoves);

            Assert.Equal(listOfMoves.Count, game.MovesCounter);
        }

        [Fact]
        public void PlayGameWithPawnsAndQueenMovement()
        {
            var listOfMoves = new List<string>() 
            {
                "d4", "e5", "dxe5", "f5","Qd5", "Qh4", "Qxd7", "c5" 
            };
            var game = new Game();
            game.Play(listOfMoves);

            Assert.Equal(listOfMoves.Count, game.MovesCounter);
        }

        [Fact]
        public void FinalTest()
        {
            var listOfMoves = ReadFromFile.ProcessFile("chess-moves2.txt");

            var game = new Game();
            game.Play(listOfMoves);

            Assert.Equal(listOfMoves.Count, game.MovesCounter);

            //assert daca jocul s-a gatat
            Assert.True(game.IsGameOver);

            //assert cu cine a castigat
            Assert.Equal(PieceColor.White, game.Winner);
        }
    }
}
