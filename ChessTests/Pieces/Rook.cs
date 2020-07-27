using ChessTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTests.Pieces
{
    public class Rook : Piece
    {

        public Rook(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Rook;
        }

        public static Piece ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);

            CheckDestinationCellAvailability(playerColor, destinationCell);

            List<Orientation> orientations = RookOrientation();

            List<Piece> findRooks = FindPieces(playerColor, destinationCell, orientations);

            if (findRooks.Count == 1)
            {
                return findRooks.First();
            }

            else if (findRooks.Count > 1)
            {
                return findRooks.Find(q => q.CurrentPosition.Y == move.Y);
            }

            return null;
        }

        public bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            List<Orientation> orientations = RookOrientation();
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

        private static List<Piece> FindPieces(PieceColor playerColor, Cell destinationCell, List<Orientation> orientations)
        {
            var findRooks = new List<Piece>();
            foreach (var orientation in orientations)
            {
                //var loop = true;
                var currentCell = destinationCell;
                while (true)
                {
                    //there is no piece on the cells
                    currentCell = currentCell.Look(orientation);

                    //Search looks out of board
                    if (currentCell == null) break;

                    if (currentCell.Piece == null) continue;


                    if (currentCell.Piece.Name == PieceName.Rook && playerColor == currentCell.Piece.pieceColor)
                    {
                        findRooks.Add(currentCell.Piece);
                    }

                    //there is an obstacle in the way, must throw exception or return
                    break;
                }
            }

            return findRooks;
        }
        private static List<Orientation> RookOrientation()
        {
            return new List<Orientation>()
            {
                Orientation.Up,    Orientation.Down,
                Orientation.Right,   Orientation.Left
            };
        }


    }
}
