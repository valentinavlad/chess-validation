using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Pieces
{
    public class King : Piece
    {

        public King(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.King;
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
                Orientation.Up, Orientation.UpLeft, Orientation.Left,  
                Orientation.DownLeft,  Orientation.Down, Orientation.DownRight,
                
                Orientation.Right, Orientation.UpRight

            };


            if (move.IsKingCastling)
            {
                var currentCell = destinationCell;
                for (int i = currentCell.Y + 1; i <= 4; i++)
                {
                    currentCell = currentCell.Look(Orientation.Right);
                    if (currentCell.Piece != null && currentCell.Y != 4) throw new InvalidOperationException("Invalid move!"); 
                    if (currentCell.Y == 4 &&  currentCell.Piece.Name == PieceName.King && playerColor == currentCell.Piece.pieceColor)
                    {
                        return currentCell.Piece;
                    }
                }

            }

            if (move.IsQueenCastling)
            {
                var currentCell = destinationCell;
                for (int i = currentCell.Y - 1; i >= 4; i--)
                {
                    //TO DO if king is in other square error
                    currentCell = currentCell.Look(Orientation.Left);
                    if (currentCell.Piece != null && currentCell.Y != 4) throw new InvalidOperationException("Invalid move!");
                    if (currentCell.Y == 4 && currentCell.Piece.Name == PieceName.King && playerColor == currentCell.Piece.pieceColor)
                    {
                        return currentCell.Piece;
                    }
                }

            }

            foreach (var orientation in orientations)
            {
                var currentCell = destinationCell;
    
                    //there is no piece on the cells
                    currentCell = currentCell.Look(orientation);

                    //Search looks out of board
                    if (currentCell == null) continue;

                    if (currentCell.Piece == null) continue;


                    if (currentCell.Piece.Name == PieceName.King && playerColor == currentCell.Piece.pieceColor)
                    {
                        return currentCell.Piece;
                    }

                    //there is an obstacle in the way, must throw exception or return
                    break;
                
            }

            return null;
        }
    }
}
