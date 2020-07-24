using ChessTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTests
{
    public class Knight : Piece
    {
   

        public Knight(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Knight;
        }

        public static Piece ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);
            if (destinationCell.HasPiece() && destinationCell.Piece.pieceColor == playerColor)
            {
                throw new InvalidOperationException("Invalid Move");
            }
            var orientations = new List<KnightOrientation>()
            {
                KnightOrientation.DownLeftDown, KnightOrientation.DownLeftUp, KnightOrientation.DownRightDown,
                KnightOrientation.DownRightUp, KnightOrientation.UpLeftDown, KnightOrientation.UpLeftUp,
                KnightOrientation.UpRightDown, KnightOrientation.UpRightUp
            };

            var findKnight = new List<Piece>();

            foreach (var orientation in orientations)
            {
                //var loop = true;
                var currentCell = destinationCell;
                while (true)
                {
                    //there is no piece on the cells
                    currentCell = currentCell.LookLShape(orientation);

                    //Search looks out of board
                    if (currentCell == null) break;

                    if (currentCell.Piece == null) continue;


                    if (currentCell.Piece.Name == PieceName.Knight && playerColor == currentCell.Piece.pieceColor)
                    {
                        findKnight.Add(currentCell.Piece);
                    }

                    //there is an obstacle in the way, must throw exception or return
                    break;
                }
            }


            if (findKnight.Count == 1)
            {
                return findKnight.First();
            }

            else if (findKnight.Count > 1)
            {
                return findKnight.Find(q => q.CurrentPosition.Y == move.Y);
            }

            return null;
        }
    }
}
