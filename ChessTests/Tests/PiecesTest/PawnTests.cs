using ChessTable;
using ChessTests.Pieces;
using System;
using Xunit;

namespace ChessTests.Tests.PiecesTest
{
    public class PawnTests
    {
        [Fact]
        public void WhitePawnCannotMoveBackwords()
        {
            var board = new Board();
            var pawn = board.CellAt("e2").Piece;
            var cell = board.CellAt("e4");
            var move = MoveNotationConverter.ParseMoveNotation("e4", PieceColor.White);
            move.MovePiece(pawn, cell);

            Action exception = () => board.FindPieceWhoNeedsToBeMoved("e3", PieceColor.White);
            Assert.Throws<InvalidOperationException>(exception);
        }

        [Fact]
        public void MoveWhitePawnASquareForward()
        {
            var board = new Board();
            var pawn = board.CellAt("e2").Piece;

            var move = MoveNotationConverter.ParseMoveNotation("e4", PieceColor.White);
            move.MovePiece(pawn, board.CellAt("e4"));

            Assert.Equal(pawn, board.FindPieceWhoNeedsToBeMoved("e5", PieceColor.White));
        }

        [Theory]
        //white pawn captures
        [InlineData("d2", "d1Q", PieceColor.Black)]
        [InlineData("d7", "d8Q", PieceColor.White)]
        public void PawnPromotesToQueen(string pawnCoord, string moveAN, PieceColor pieceColor)
        {
            //promote a pawn with no capture to Queen
            var board = new Board(false);
            var action = new Helpers.Action(board);
            var pawnBlack = action.AddPiece(pawnCoord, new Pawn(pieceColor));
            var cell = board.CellAt(pawnCoord);
            Assert.Equal(pawnBlack, cell.Piece);     
           
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, pieceColor);
            var pawn = board.PlayMove(moveAN, pieceColor);
            Assert.Null(pawnBlack.CurrentPosition);
            Assert.IsType<Queen>(board.CellAt(move.Coordinate).Piece);

        }

        [Theory]
        //white pawn captures
        [InlineData("d2", "c1", "dxc1Q", PieceColor.Black)]
        [InlineData("d3", "c2", "dxc2Q", PieceColor.Black)]
        //  [InlineData("d7", "d8Q", PieceColor.White)]
        public void PawnCapturesAndPromotesToQueen(string attackerCoords, string opponentCoords, string moveAN, PieceColor attackerColor)
        {
            //promote a pawn with no capture to Queen
            //Arrange
            var board = new Board(false);
            var opponentColor = attackerColor == PieceColor.Black ? PieceColor.White : PieceColor.Black;
            var action = new Helpers.Action(board);
            action.AddPiece(attackerCoords, new Pawn(attackerColor));
            action.AddPiece(opponentCoords, new Rook(opponentColor));

            //Act
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, attackerColor);
            //caut piesa care face capturarea
            board.FindPieceWhoNeedsToBeMoved(move, attackerColor);


            //determina celula destinatie
            var cellDestination = board.TransformCoordonatesIntoCell(move.Coordinate);

            //cauta piesa oponenta
            var opponent = cellDestination.Piece;

            board.PlayMove(moveAN, attackerColor);

            Assert.Null(opponent.CurrentPosition);
            Assert.IsType<Queen>(board.CellAt(move.Coordinate).Piece);

        }

        [Theory]
        //white pawn captures
        [InlineData("d3", "c4", "dxc4", false, PieceColor.White)]
        [InlineData("b3", "c4", "bxc4", false, PieceColor.White)]
        [InlineData("e6", "c4", "dxc4", true, PieceColor.White)]
        //black pawn captures
        [InlineData("d3", "c4", "cxd3", false, PieceColor.Black)]
        [InlineData("d3", "e4", "exd3", false, PieceColor.Black)]
        [InlineData("d3", "e6", "cxd3", true, PieceColor.Black)]
        public void PawnCapture(string whitePawnCoordinates, string blackPawnCoordinates, string moveAN, bool expectsException, PieceColor currentPlayer)
        {
            var board = new Board(false);
            var action = new Helpers.Action(board);
            var pawnBlack = action.AddPiece(blackPawnCoordinates, new Pawn(PieceColor.Black));
            var pawnWhite = action.AddPiece(whitePawnCoordinates, new Pawn(PieceColor.White));

            Assert.Equal(pawnWhite, board.CellAt(whitePawnCoordinates).Piece);
            Assert.Equal(pawnBlack, board.CellAt(blackPawnCoordinates).Piece);


            Piece attackerPawn = null;
            //Assert.Equal(3, move.Y);
            if (!expectsException)
            {
                //cauta piesa alba care face mutarea
                attackerPawn = board.PlayMove(moveAN, currentPlayer);

                if (currentPlayer == PieceColor.White)
                {
                    //verific pozitia pionului alb, sa fie pe celula c4
                    Assert.Equal(attackerPawn, board.CellAt(blackPawnCoordinates).Piece);

                    //verific ca pionul negru nu mai exista pe board
                    Assert.Null(pawnBlack.CurrentPosition);
                }
                else
                {
                    //verific pozitia pionului alb, sa fie pe celula c4
                    Assert.Equal(attackerPawn, board.CellAt(whitePawnCoordinates).Piece);

                    //verific ca pionul alb nu mai exista pe board
                    Assert.Null(pawnWhite.CurrentPosition);
                }
            }
            else
            {
              
                Assert.Null(board.FindPieceWhoNeedsToBeMoved(moveAN, currentPlayer));
            }
        }

        [Fact]
        public void FindWhitePawnWhoNeedsToBeMovedTest()
        {
            var board = new Board();
            var player = PieceColor.White;
            var move = MoveNotationConverter.ParseMoveNotation("e3", player);
            var piece = board.FindPieceWhoNeedsToBeMoved(move, player);

            Assert.NotNull(piece);
            Assert.IsType<Pawn>(piece);

            var piece2 = board.FindPieceWhoNeedsToBeMoved("e4", player);

            Assert.NotNull(piece2);
            Assert.IsType<Pawn>(piece2);
            Assert.Equal(PieceColor.White, piece2.pieceColor);
        }

        [Fact]
        public void FindBlackPawnWhoNeedsToBeMovedTest()
        {
            var board = new Board();
            var player = PieceColor.Black;
            var piece = board.FindPieceWhoNeedsToBeMoved("e6", player);

            Assert.NotNull(piece);
            Assert.IsType<Pawn>(piece);

            var piece2 = board.FindPieceWhoNeedsToBeMoved("e5", player);

            Assert.NotNull(piece2);
            Assert.IsType<Pawn>(piece2);
            Assert.Equal(PieceColor.Black, piece2.pieceColor);
        }

        [Theory]

        [InlineData("d2", "d1", "d4")]
        [InlineData("d3", "d1", "d4")]
        public void CheckPawnMovementWithObstacleShouldThrowException(string attackerCoords,
                   string opponentCoords, string moveAN)
        {
            var board = new Board(false);
            var action = new Helpers.Action(board);
            action.AddPiece(attackerCoords, new Pawn(PieceColor.Black));
            action.AddPiece(opponentCoords, new Queen(PieceColor.White));

            var move = MoveNotationConverter.ParseMoveNotation(moveAN, PieceColor.White);

            Action exception = () => board.FindPieceWhoNeedsToBeMoved(move, PieceColor.White);
            Assert.Throws<InvalidOperationException>(exception);
        }
    }
}
