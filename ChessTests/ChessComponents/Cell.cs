using ChessTests.Directions;
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
            return HasPiece() && Piece.PieceColor == playerColor;
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
        public Cell LookDown()
        {
            int i = X + 1;
            if (i >= 8) return null;
            return cells[i, Y];
        }

        public Cell LookUp()
        {
            int i = X - 1;
            if (i < 0) return null;
            return cells[i, Y];
        }

        public Cell LookDownRight()
        {
            int i = X + 1;
            int j = Y + 1;
            if (i >= 8 || j >= 8) return null;
            return cells[i, j];
        }

        public Cell LookDownLeft()
        {
            int i = X + 1;
            int j = Y - 1;
            if (i >= 8 || j < 0) return null;
            return cells[i, j];
        }

        public Cell LookUpLeft()
        {
            int i = X - 1;
            int j = Y - 1;
            if (i < 0 || j < 0) return null;
            return cells[i, j];
        }

        public Cell LookUpRight()
        {
            int i = X - 1;
            int j = Y + 1;
            if (i < 0 || j >= 8) return null;
            return cells[i, j];
        }

        public Cell LookRight()
        {
            int j = Y + 1;

            if (j >= 8) return null;
            return cells[X, j];
        }

        public Cell LookLeft()
        {
            int j = Y - 1;
            if (j < 0) return null;
            return cells[X, j];
        }
        public Cell LShapeLookUpLeftDown()
        {
            int i = X - 1;
            int j = Y - 2;
            if (i < 0 || j < 0) return null;
            return cells[i, j];
        }
        public Cell LShapeLookUpLeftUp()
        {
            int i = X - 2;
            int j = Y - 1;
            if (i < 0 || j < 0) return null;
            return cells[i, j];
        }
        public Cell LShapeLookUpRightUp()
        {
            int i = X - 2;
            int j = Y + 1;
            if (i < 0 || j >= 8) return null;
            return cells[i, j];
        }
        public Cell LShapeLookUpRightDown()
        {
            int i = X - 1;
            int j = Y + 2;
            if (i < 0 || j >= 8) return null;
            return cells[i, j];
        }
        public Cell LShapeLookDownLeftUp()
        {
            int i = X + 1;
            int j = Y - 2;
            if (i >= 8 || j < 0) return null;
            return cells[i, j];
        }
        public Cell LShapeLookDownLeftDown()
        {
            int i = X + 2;
            int j = Y - 1;
            if (i >= 8 || j < 0) return null;
            return cells[i, j];
        }
        public Cell LShapeLookDownRightUp()
        {
            int i = X + 1;
            int j = Y + 2;
            if (i >= 8 || j >= 8) return null;
            return cells[i, j];
        }
        public Cell LShapeLookDownLRightDown()
        {
            int i = X + 2;
            int j = Y + 1;
            if (i >= 8 || j >= 8) return null;
            return cells[i, j];
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