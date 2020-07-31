namespace ChessTests.Directions
{
    public class KnightLook : Cell
    {
        public KnightLook(int x, int y, Cell[,] cells = null) : base(x,  y, cells)
        {
        }
        internal Cell LShapeLookUpLeftDown()
        {
            int i = X - 1;
            int j = Y - 2;
            if (i < 0 || j < 0) return null;
            return cells[i, j];
        }
        internal Cell LShapeLookUpLeftUp()
        {
            int i = X - 2;
            int j = Y - 1;
            if (i < 0 || j < 0) return null;
            return cells[i, j];
        }
        internal Cell LShapeLookUpRightUp()
        {
            int i = X - 2;
            int j = Y + 1;
            if (i < 0 || j >= 8) return null;
            return cells[i, j];
        }
        internal Cell LShapeLookUpRightDown()
        {
            int i = X - 1;
            int j = Y + 2;
            if (i < 0 || j >= 8) return null;
            return cells[i, j];
        }
        internal Cell LShapeLookDownLeftUp()
        {
            int i = X + 1;
            int j = Y - 2;
            if (i >= 8 || j < 0) return null;
            return cells[i, j];
        }
        internal Cell LShapeLookDownLeftDown()
        {
            int i = X + 2;
            int j = Y - 1;
            if (i >= 8 || j < 0) return null;
            return cells[i, j];
        }
        internal Cell LShapeLookDownRightUp()
        {
            int i = X + 1;
            int j = Y + 2;
            if (i >= 8 || j >= 8) return null;
            return cells[i, j];
        }
        internal Cell LShapeLookDownLRightDown()
        {
            int i = X + 2;
            int j = Y + 1;
            if (i >= 8 || j >= 8) return null;
            return cells[i, j];
        }
    }
}
