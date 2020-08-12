using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Pieces
{
    public class King : Piece
    {
        public List<Orientation> KingOrientation = new List<Orientation>()
        {
            Orientation.Up, Orientation.UpLeft, Orientation.Left,
            Orientation.DownLeft,  Orientation.Down, Orientation.DownRight,

            Orientation.Right, Orientation.UpRight

        };
        
        public King(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.King;
        }

        public override bool ValidateMovement(Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);

            CheckDestinationCellAvailability(playerColor, destinationCell);

            if (move.IsQueenCastling) return IsQueenCastling(destinationCell, playerColor, move);

            if (move.IsKingCastling) return IsKingCastling(destinationCell, playerColor, move);

            foreach (var orientation in KingOrientation)
            {
                var currentCell = destinationCell;

                //there is no piece on the cells
                currentCell = currentCell.Look(orientation);

                //Search looks out of board
                if (currentCell == null) continue;

                if (currentCell.Piece == null) continue;

                if (currentCell.Piece.Name == PieceName.King && playerColor == currentCell.Piece.pieceColor)
                {
                    move.PiecePosition = currentCell.Piece.CurrentPosition;
                    return true;
                }

                break;
            }

            return false;
        }

        private bool IsQueenCastling(Cell destinationCell, PieceColor playerColor, Move move)
        {
            var currentCell = destinationCell;
            for (int i = currentCell.Y + 1; i <= 4; i++)
            {
                currentCell = currentCell.Look(Orientation.Right);
                if (currentCell.Piece != null && currentCell.Y != 4) throw new InvalidOperationException("Invalid move!");
                if (currentCell.Y == 4 && currentCell.Piece.Name == PieceName.King && playerColor == currentCell.Piece.pieceColor)
                {
                    move.PiecePosition = currentCell.Piece.CurrentPosition;
                    return true;
                }
            }
            return false;
        }
        private bool IsKingCastling(Cell destinationCell, PieceColor playerColor, Move move)
        {
            var currentCell = destinationCell;
            for (int i = currentCell.Y - 1; i >= 4; i--)
            {
                currentCell = currentCell.Look(Orientation.Left);
                if (currentCell.Piece != null && currentCell.Y != 4) throw new InvalidOperationException("Invalid move!");
                if (currentCell.Y == 4 && currentCell.Piece.Name == PieceName.King && playerColor == currentCell.Piece.pieceColor)
                {
                    move.PiecePosition = currentCell.Piece.CurrentPosition;
                    return true;
                    //return currentCell.Piece;
                }
            }
            return false;
        }
        public override bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            throw new NotImplementedException();
        }
    }
}
