using ChessTests.Directions;
using ChessTests.Pieces;
using System;

namespace ChessTests
{
    public class Cell
    {
        protected internal Cell[,] cells;
        private Piece piece;

        public Cell()
        {
        }
        public Cell(int x, int y, Piece piece = null, Cell[,] cells = null)
        {
            X = x;
            Y = y;
            Piece = piece;
            if (piece != null)
            {
                piece.InitialPosition = this;
            }
            this.cells = cells;
        }

 
        //needed for Initialize board with no pieces
        public Cell(int x, int y, Cell[,] cells = null)
        {
            X = x;
            Y = y;
            this.cells = cells;
        }

        public Piece Piece
        {
            get
            {
                return piece;
            }
            set
            {
                piece = value;
                if (piece != null)
                {
                    piece.CurrentPosition = this;
                }
            }
        }

        public int X { get; }
        public int Y { get; }
        public bool BelongsTo(PieceColor playerColor)
        {
            return HasPiece() && Piece.pieceColor == playerColor;
        }

        public bool HasPawn()
        {
            return HasPiece() && Piece.Name == PieceName.Pawn;
        }

        public bool HasPiece()
        {
            return Piece != null;
        }

        public Cell Look(Orientation orientation, int fileY = -1)
        {
            var look = new Look(X, Y, cells);
            if (fileY > -1)
            {
                if (Orientation.Down == orientation)
                {
                    var cellRight = look.LookDownRight();
                    var cellLeft = look.LookDownLeft();
                    return fileY == cellRight.Y ? cellRight : cellLeft;
                }
                if (Orientation.Up == orientation)
                {
                    var cellRight = look.LookUpRight();
                    var cellLeft = look.LookUpLeft();
                    return fileY == cellRight.Y ? cellRight : cellLeft;
                }
            }
            else
            {
                if (Orientation.Up == orientation) return look.LookUp();
                if (Orientation.Down == orientation) return look.LookDown();
                if (Orientation.DownRight == orientation) return look.LookDownRight();
                if (Orientation.DownLeft == orientation) return look.LookDownLeft();
                if (Orientation.Left == orientation) return look.LookLeft();
                if (Orientation.Right == orientation) return look.LookRight();
                if (Orientation.UpLeft == orientation) return look.LookUpLeft();
                if (Orientation.UpRight == orientation) return look.LookUpRight();

            }
            return null;
        }

        public Cell LookLShape(KnightOrientation orientation)
        {
            var knightLook = new KnightLook(X, Y, cells);
            if (KnightOrientation.DownLeftDown == orientation) return knightLook.LShapeLookDownLeftDown();
            if (KnightOrientation.DownLeftUp == orientation) return knightLook.LShapeLookDownLeftUp();
            if (KnightOrientation.DownRightDown == orientation) return knightLook.LShapeLookDownLRightDown();
            if (KnightOrientation.DownRightUp == orientation) return knightLook.LShapeLookDownRightUp();
            if (KnightOrientation.UpLeftDown == orientation) return knightLook.LShapeLookUpLeftDown();
            if (KnightOrientation.UpLeftUp == orientation) return knightLook.LShapeLookUpLeftUp();
            if (KnightOrientation.UpRightDown == orientation) return knightLook.LShapeLookUpRightDown();
            if (KnightOrientation.UpRightUp == orientation) return knightLook.LShapeLookUpRightUp();
            return null;
        }
        internal void CheckDestinationCellAvailability(PieceColor playerColor)
        {
            if (BelongsTo(playerColor))
            {
                throw new InvalidOperationException("Invalid Move");
            }
        }
    }
}