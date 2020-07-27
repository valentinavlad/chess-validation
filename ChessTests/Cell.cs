namespace ChessTests
{
    public class Cell
    {
        private Piece piece;
        private Cell[,] cells;
        //public void SetCells(Cell[,] cells) 
        //{
        //    this.cells = cells;
        //}
        // readonly
        public int X { get;}
        public int Y { get; }

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
        
        public Cell LookDown()
        {
            int i = X + 1;
            //TO DO Verify if the coords are on the table
            if (i >= 8) return null;
            return cells[i, Y];
        }

        public Cell LookUp()
        {
            int i = X - 1;
            //TO DO Verify if the coords are on the table
            if (i < 0) return null;
            return cells[i, Y];
        }

        public Cell LookDownRight()
        {
            int i = X + 1;
            int j = Y + 1;
            //TO DO Verify if the coords are on the table
            if (i >= 8 || j >= 8) return null;
            return cells[i, j];
        }

        public Cell LookDownLeft()
        {
            int i = X + 1;
            int j = Y - 1;
            //TO DO Verify if the coords are on the table
            if (i >= 8 || j < 0) return null;
            return cells[i, j];
        }

        public Cell LookUpLeft()
        {
            int i = X - 1;
            int j = Y - 1;
            //TO DO Verify if the coords are on the table
            if (i < 0 || j < 0) return null;
            return cells[i, j];
        }
        
        public Cell LookUpRight()
        {
            int i = X - 1;
            int j = Y + 1;
            //TO DO Verify if the coords are on the table
            if (i < 0 || j >= 8) return null;
            return cells[i, j];
        }

        private Cell LookRight()
        {
            int j = Y + 1;

            if (j >= 8) return null;
            return cells[X, j];
        }

        private Cell LookLeft()
        {
            int j = Y - 1;
            if (j < 0) return null;
            return cells[X, j];
        }

        //knight looks
        private Cell LShapeLookUpLeftDown()
        {
            int i = X - 1;
            int j = Y - 2;
            if (i < 0 || j < 0) return null;
            return cells[i, j];
        }
        private Cell LShapeLookUpLeftUp()
        {
            int i = X - 2;
            int j = Y - 1;
            if (i < 0 || j < 0) return null;
            return cells[i, j];
        }
        private Cell LShapeLookUpRightUp()
        {
            int i = X - 2;
            int j = Y + 1;
            if (i < 0 || j >= 8) return null;
            return cells[i, j];
        }
        private Cell LShapeLookUpRightDown()
        {
            int i = X - 1;
            int j = Y + 2;
            if (i < 0 || j >= 8) return null;
            return cells[i, j];
        }
        private Cell LShapeLookDownLeftUp()
        {
            int i = X + 1;
            int j = Y - 2;
            if (i >= 8 || j < 0) return null;
            return cells[i, j];
        }
        private Cell LShapeLookDownLeftDown()
        {
            int i = X + 2;
            int j = Y - 1;
            if (i >= 8 || j < 0) return null;
            return cells[i, j];
        }
        private Cell LShapeLookDownRightUp()
        {
            int i = X + 1;
            int j = Y + 2;
            if (i >= 8 || j >= 8) return null;
            return cells[i, j];
        }
        private Cell LShapeLookDownLRightDown()
        {
            int i = X + 2;
            int j = Y + 1;
            if (i >= 8 || j >= 8) return null;
            return cells[i, j];
        }
  
        public Cell LookLShape(KnightOrientation orientation)
        {
            if (KnightOrientation.DownLeftDown == orientation) return LShapeLookDownLeftDown();
            if (KnightOrientation.DownLeftUp == orientation) return LShapeLookDownLeftUp();
            if (KnightOrientation.DownRightDown == orientation) return LShapeLookDownLRightDown();
            if (KnightOrientation.DownRightUp == orientation) return LShapeLookDownRightUp();
            if (KnightOrientation.UpLeftDown == orientation) return LShapeLookUpLeftDown();
            if (KnightOrientation.UpLeftUp == orientation) return LShapeLookUpLeftUp();
            if (KnightOrientation.UpRightDown == orientation) return LShapeLookUpRightDown();
            if (KnightOrientation.UpRightUp == orientation) return LShapeLookUpRightUp();
            return null;
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
                if (Orientation.DownRight == orientation) return LookDownRight();
                if (Orientation.DownLeft == orientation) return LookDownLeft();
                if (Orientation.Left == orientation) return LookLeft();
                if (Orientation.Right == orientation) return LookRight();
                if (Orientation.UpLeft == orientation) return LookUpLeft();
                if (Orientation.UpRight == orientation) return LookUpRight();
           
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