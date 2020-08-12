using System.Collections.Generic;
using System.Linq;

namespace ChessTests.Helpers
{
    internal class BoardAction
    {
        readonly List<Orientation> orientations = new List<Orientation>
        {
            Orientation.Up,  Orientation.DownLeft,
            Orientation.UpRight, Orientation.Right,
            Orientation.DownRight, Orientation.Down,
            Orientation.Left,  Orientation.UpLeft
        };
        internal List<Piece> FindPieces(Move move, List<Orientation> orientations, PieceName pieceName)
        {
  
            var findPieces = new List<Piece>();
            foreach (var orientation in orientations)
            {
                var currentCell = move.DestinationCell;
                while (true)
                {
                    //there is no piece on the cells
                    currentCell = currentCell.Look(orientation);

                    //Search looks out of board
                    if (currentCell == null) break;

                    if (currentCell.Piece == null) continue;

                    if (currentCell.Piece.Name == pieceName && move.Color == currentCell.Piece.pieceColor)
                    {
                        findPieces.Add(currentCell.Piece);
                    }
                    break;
                }
            }
            return findPieces;
        }

        internal Piece FoundedPiece(Move move, List<Piece> findPieces)
        {
            if (findPieces.Count == 1)
            {
                return findPieces.First();
            }
            else if (findPieces.Count > 1)
            {
                return findPieces.Find(q => q.CurrentPosition.Y == move.Y);
            }
            return null;
        }

        public Piece FindKing(Cell checkMatePosition, PieceColor playerColor, List<Orientation> orient = null)
        {
            foreach (var orientation in orientations)
            {
                var currentCell = checkMatePosition;
                while (true)
                {
                    //there is no piece on the cells
                    currentCell = currentCell.Look(orientation);

                    //Search looks out of board
                    if (currentCell == null) break;

                    if (currentCell.Piece == null) continue;

                    if (currentCell.Piece.Name == PieceName.King && playerColor != currentCell.Piece.pieceColor)
                    {
                        return currentCell.Piece;
                    }
                    //there is an obstacle in the way, must throw exception or return
                    break;
                }
            }
            return null;
        }
    }
}
