using ChessTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTests
{
    public class Bishop : Piece
    {

        public Bishop(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Bishop;
        }
        public static Piece ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);

            CheckDestinationCellAvailability(playerColor, destinationCell);

            List<Orientation> orientations = BishopOrientation();

            List<Piece> findBishops = FindPieces(playerColor, destinationCell, orientations);

            if (findBishops.Count == 1)
            {
                return findBishops.First();
            }

            else if (findBishops.Count > 1)
            {
                return findBishops.Find(q => q.CurrentPosition.Y == move.Y);
            }

            return null;
        }

        private static List<Piece> FindPieces(PieceColor playerColor, Cell destinationCell, List<Orientation> orientations)
        {
            var findBishops = new List<Piece>();

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


                    if (currentCell.Piece.Name == PieceName.Bishop && playerColor == currentCell.Piece.pieceColor)
                    {
                        findBishops.Add(currentCell.Piece);
                    }

                    //there is an obstacle in the way, must throw exception or return
                    break;
                }
            }

            return findBishops;
        }

        private static List<Orientation> BishopOrientation()
        {
            return new List<Orientation>()
            {
                Orientation.UpLeft, Orientation.DownLeft,
                Orientation.UpRight, Orientation.DownRight,
            };
        }

        public bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            List<Orientation> orientations = BishopOrientation();
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
    }
}