using ChessTable;
using ChessTests.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Pieces
{
    public class King : Piece, ICheckOpponentKing
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

        public override bool ValidateMovement(Move move)
        {
            move.DestinationCell.CheckDestinationCellAvailability(move.Color);

            if (move.IsQueenCastling) return IsQueenCastling(move.DestinationCell, move.Color, move);

            if (move.IsKingCastling) return IsKingCastling(move.DestinationCell, move.Color, move);

            foreach (var orientation in KingOrientation)
            {
                var currentCell = move.DestinationCell;

                //there is no piece on the cells
                currentCell = currentCell.Look(orientation);

                //Search looks out of board
                if (currentCell == null) continue;

                if (currentCell.Piece == null) continue;

                if (currentCell.Piece.Name == PieceName.King && move.Color == currentCell.Piece.pieceColor)
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
                }
            }
            return false;
        }

        public bool CheckForOpponentKingOnSpecificRoutes(Move move)
        {
            return boardAction.FindPieces(move, PieceName.King, KingOrientation).Count != 0 ? true : false;
        }
    }
}
