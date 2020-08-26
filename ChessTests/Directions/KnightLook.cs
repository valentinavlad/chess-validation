namespace ChessTests.Directions
{
    public class KnightLook : Cell
    {
        public KnightLook(int x, int y, Cell[,] cells = null) : base(x,  y, cells)
        {
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
    }
}