using System.Collections.Generic;
using System.Linq;

namespace ChessTests.Helpers
{
    internal class BoardAction
    {
        internal List<Piece> FindPieces(Move move,PieceName pieceName, List<Orientation> orientations)
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

                    if (currentCell.Piece.Name == pieceName && move.Color == currentCell.Piece.PieceColor)
                    {
                        findPieces.Add(currentCell.Piece);
                    }
                    if (currentCell.Piece.Name == PieceName.King && move.Color != currentCell.Piece.PieceColor)
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
            IEnumerable<Piece> list = findPieces.Where(x => x.Name == move.PieceName);
            if (list.Count() == 1)
            {
                return list.First();
            }
            else if (list.Count() > 1)
            {
                return list.Where(q => q.CurrentPosition.Y == move.Y).First();
            }
            return null;
        }
    }
}
