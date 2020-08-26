using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface IBoard
    {
        public Piece FoundedPiece(Move move, IEnumerable<Piece> findPieces);
        public IEnumerable<Piece> FindPieces(Move move, IEnumerable<Orientation> orientations);
        public Cell CellAt(Coordinate coordinates);
    }
}
