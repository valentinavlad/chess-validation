using System;

namespace ChessTests
{
    public class Cell
    {
        private Piece piece;
        private Cell[,] cells;
        public void SetCells(Cell[,] cells) 
        {
            this.cells = cells;
        }
        //make readonly
        public int X { get; set; }
        public int Y { get; set; }

        public Cell(Piece piece)
        {
            Piece = piece;
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
        
        public Cell LookDown()
        {
            int i = X + 1;
            //TO DO Verify if the coords are on the table
            return cells[i, Y];
        }

        public Cell LookUp()
        {
            int i = X - 1;
            //TO DO Verify if the coords are on the table
            return cells[i, Y];
        }

        public Cell LookDownRight()
        {
            int i = X + 1;
            int j = Y + 1;
            //TO DO Verify if the coords are on the table
            return cells[i, j];
        }

        public Cell LookDownLeft()
        {
            int i = X + 1;
            int j = Y - 1;
            //TO DO Verify if the coords are on the table
            return cells[i, j];
        }

        public Cell LookUpLeft()
        {
            int i = X - 1;
            int j = Y - 1;
            //TO DO Verify if the coords are on the table
            return cells[i, j];
        }
        
        public Cell LookUpRight()
        {
            int i = X - 1;
            int j = Y + 1;
            //TO DO Verify if the coords are on the table
            return cells[i, j];
        }
        
        public Cell Look(Orientation orientation, int fileY = -1)
        {

            if (fileY > -1)
            {
                if(Orientation.Down == orientation)
                {
                    var cellRight = LookDownRight();
                    var cellLeft =  LookDownLeft();
                    return fileY == cellRight.Y ? cellRight : cellLeft;
                }
                if (Orientation.Up == orientation)
                {
                    var cellRight = LookUpRight();
                    var cellLeft = LookUpLeft();
                    return fileY == cellRight.Y ? cellRight : cellLeft;
                }
            }
            else
            {
                if (Orientation.Up == orientation) return LookUp();
                if (Orientation.Down == orientation) return LookDown();
            }
            return null;
        }

    

        public bool HasPawn()
        {
            return HasPiece() && Piece.Name == PieceName.Pawn;
        }

        public bool HasPiece()
        {
            return Piece != null;
        }

        public bool BelongsTo(PieceColor playerColor)
        {
            return HasPiece() && Piece.pieceColor == playerColor;
        }
    }
}