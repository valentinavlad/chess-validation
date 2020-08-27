using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IBoard
    {
        public Piece FoundedPiece(IMove move, IEnumerable<Piece> findPieces);
        public IEnumerable<Piece> FindPieces(IMove move, IEnumerable<Orientation> orientations);
        public Cell CellAt(Coordinate coordinates);
    }
}
