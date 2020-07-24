using ChessTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTests
{
    public class Queen : Piece
    {
        public Queen(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Queen;
        }

        public static Piece ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);
            if (destinationCell.HasPiece() && destinationCell.Piece.pieceColor == playerColor)
            {
                throw new InvalidOperationException("Invalid Move");
            }
            var orientations = new List<Orientation>()
            {
                Orientation.Up,  Orientation.DownLeft, Orientation.UpRight,
                Orientation.Right, Orientation.DownRight,
                Orientation.Down, 
                Orientation.Left,  Orientation.UpLeft
            };

            var findQueens = new List<Piece>();
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


                    if (currentCell.Piece.Name == PieceName.Queen && playerColor == currentCell.Piece.pieceColor)
                    {
                        findQueens.Add(currentCell.Piece);
                    }

                    //there is an obstacle in the way, must throw exception or return
                    break;
                }
            }


            if (findQueens.Count == 1)
            {
                return findQueens.First();
            }

            else if(findQueens.Count > 1)
            {
                return findQueens.Find(q => q.CurrentPosition.Y == move.Y);
            }
          
            return null;
        }
    }
}
