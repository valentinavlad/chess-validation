namespace ChessTests.Directions
{
    public class Look : Cell
    {
        public Look(int x, int y, Cell[,] cells = null) : base(x, y, cells)
        {
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
    }
}
