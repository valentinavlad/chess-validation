using ChessTable;
using ChessTests.GameAction;
using ChessTests.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTests.Pieces
{
    public class King : Piece, ICheckOpponentKing
    {
        private readonly CastlingHelpers castling = new CastlingHelpers();
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

        public override bool ValidateMovement(IBoard board, IMove move)
        {
            move.DestinationCell.CheckDestinationCellAvailability(move.PieceColor);
            if (move.IsQueenCastling) return IsQueenCastling(move.DestinationCell, move.PieceColor, move);

            if (move.IsKingCastling) return IsKingCastling(move.DestinationCell, move.PieceColor, move);

            foreach (var orientation in KingOrientation)
            {
                var currentCell = move.DestinationCell;

                //there is no piece on the cells
                currentCell = currentCell.Look(orientation);

                //Search looks out of board
                if (currentCell == null) continue;

                if (currentCell.Piece == null) continue;

                if (currentCell.Piece.Name == PieceName.King && move.PieceColor == currentCell.Piece.PieceColor)
                {
                    move.PiecePosition = currentCell.Piece.DestinationCell;
                    return true;
                }

                break;
            }

            return false;
        }

        public bool CheckForOpponentKingOnSpecificRoutes(IBoard board, Move move)
        {
            return board.FindPieces(move, KingOrientation).Count() != 0 ? true : false;
        }

        public bool Castling(Move move)
        {
            if (move.IsQueenCastling)
            {             
                return IsQueenCastling(move.DestinationCell, move.PieceColor, move);
            }

            if (move.IsKingCastling) 
            {
                return IsKingCastling(move.DestinationCell, move.PieceColor, move);
            }
            
            return false;
        }

        //check if king is able to perform castling
        private bool IsQueenCastling(Cell destinationCell, PieceColor playerColor, IMove move)
        {
            var currentCell = destinationCell;
            for (int i = currentCell.Y + 1; i <= 4; i++)
            {
                currentCell = currentCell.Look(Orientation.Right);
                if (currentCell.Piece != null && currentCell.Y != 4) throw new InvalidOperationException("Invalid move!");
                if (currentCell.Y == 4 && currentCell.Piece.Name == PieceName.King && playerColor == currentCell.Piece.PieceColor)
                {
                    move.PiecePosition = currentCell.Piece.DestinationCell;
                    return true;
                }
            }
            return false;
        }
        private bool IsKingCastling(Cell destinationCell, PieceColor playerColor, IMove move)
        {
            var currentCell = destinationCell;
            for (int i = currentCell.Y - 1; i >= 4; i--)
            {
                currentCell = currentCell.Look(Orientation.Left);
                if (currentCell.Piece != null && currentCell.Y != 4) throw new InvalidOperationException("Invalid move!");
                if (currentCell.Y == 4 && currentCell.Piece.Name == PieceName.King && playerColor == currentCell.Piece.PieceColor)
                {
                    move.PiecePosition = currentCell.Piece.DestinationCell;
                    return true;
                }
            }
            return false;
        }
    }
}
