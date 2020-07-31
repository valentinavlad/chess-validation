using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTests.Helpers
{
    internal  class BoardAction
    {
        internal static List<Piece> FindPieces(PieceColor playerColor, Cell destinationCell, List<Orientation> orientations, PieceName pieceName)
        {
            var findPieces = new List<Piece>();
            foreach (var orientation in orientations)
            {
                var currentCell = destinationCell;
                while (true)
                {
                    //there is no piece on the cells
                    currentCell = currentCell.Look(orientation);

                    //Search looks out of board
                    if (currentCell == null) break;

                    if (currentCell.Piece == null) continue;


                    if (currentCell.Piece.Name == pieceName && playerColor == currentCell.Piece.pieceColor)
                    {
                        findPieces.Add(currentCell.Piece);
                    }

                    //there is an obstacle in the way, must throw exception or return
                    break;
                }
            }

            return findPieces;
        }

        internal static Piece FoundedPiece(Move move, List<Piece> findPieces)
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
   

        internal static bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor, List<Orientation> orientations)
        {
            foreach (var orientation in orientations)
            {
                var currentCell = currentPosition;
                while (true)
                {
                    //there is no piece on the cells
                    currentCell = currentCell.Look(orientation);

                    //Search looks out of board
                    if (currentCell == null) break;

                    if (currentCell.Piece == null) continue;

                    if (currentCell.Piece.Name == PieceName.King && playerColor != currentCell.Piece.pieceColor)
                    {
                        //we find the king, which is in check
                        return true;
                    }

                    //there is an obstacle in the way, must throw exception or return
                    break;
                }

            }
            return false;
        }

        public static Piece FindKing(Cell checkMatePosition, PieceColor playerColor)
        {
            List<Orientation> orientations = new List<Orientation>
            {
                Orientation.Up,  Orientation.DownLeft,
                Orientation.UpRight, Orientation.Right,
                Orientation.DownRight, Orientation.Down,
                Orientation.Left,  Orientation.UpLeft
            };
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
