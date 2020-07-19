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
        public void FindWhitePawnWhoNeedsToBeMovedTest()
        {
            var board = new Board();
            var cell = board.CellAt("e3");
            var player = PieceColor.White;
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation("e3", player);
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
        public void PromoteWhitePawnToQueen()
        {
            //promote a pawn with no capture to Queen
            var board = new Board(false);
            //  var pawnBlack = board.CellAt("e2").Piece;
            var pawnBlack = board.AddPiece("d2", new Pawn(PieceColor.Black));
            var cell = board.CellAt("d2");
            Assert.Equal(pawnBlack, cell.Piece);

            var moveAN = "d1Q";
            //pe celula destinatie dupa mutarea pionului pe d1, ar trebui sa fie regina neagra

            //caut piesa 
            //am gasit piesa
            var piece = board.FindPieceWhoNeedsToBeMoved(moveAN, PieceColor.Black);

            //determin celula destinatie
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, PieceColor.Black);
            var cellDestination = board.TransformCoordonatesIntoCell(move.Coordinate);

            //fac mutarea pe celula destinatie
            board.Move(piece, cellDestination);

            //fac promovarea pionului la regina
            //resetez pozitia curenta a pionului
            //adaug o noua regina
            var newQueen = board.PromotePawn(move, piece);

            Assert.Null(pawnBlack.CurrentPosition);
            Assert.IsType<Queen>(board.CellAt("d1").Piece);

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
        public void WhitePawnCaptureBlackPawn(string whitePawnCoordinates, string blackPawnCoordinates, string moveAN, bool expectsException, PieceColor currentPlayer)
        {
            var board = new Board(false);
            var pawnBlack = board.AddPiece(blackPawnCoordinates, new Pawn(PieceColor.Black));
            var pawnWhite = board.AddPiece(whitePawnCoordinates, new Pawn(PieceColor.White));

            Assert.Equal(pawnWhite, board.CellAt(whitePawnCoordinates).Piece);
            Assert.Equal(pawnBlack, board.CellAt(blackPawnCoordinates).Piece);

            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, currentPlayer);
            Piece attackerPawn = null;
            //Assert.Equal(3, move.Y);
            if (!expectsException)
            {
                //cauta piesa alba care face mutarea
                attackerPawn = board.FindPieceWhoNeedsToBeMoved(moveAN, currentPlayer);
                //determina celula destinatie
                var cellDestination = board.TransformCoordonatesIntoCell(move.Coordinate);

                //cauta daca exista pion pe celula destinatie
                // capturez pionul negru de pe diagonala
                //resetez pozitia curenta a pionului negru
                board.CapturePiece(attackerPawn, cellDestination);

                //mut pionul pe celula destinatie
                board.Move(attackerPawn, cellDestination);

                if(currentPlayer == PieceColor.White)
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
                //daca nu exista piesa, arunc exceptie
                Action exception = () => board.FindPieceWhoNeedsToBeMoved(moveAN, currentPlayer);
                Assert.Throws<InvalidOperationException>(exception);
            }
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
