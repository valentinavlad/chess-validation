using ChessTests.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ChessTests
{
    public class Bishop : Piece, ICheckOpponentKing
    {
        private readonly List<Orientation> BishopOrientation = new List<Orientation>()
        {
            Orientation.UpLeft, Orientation.DownLeft,
            Orientation.UpRight, Orientation.DownRight,
        };

        public Bishop(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Bishop;
        }

        public bool CheckForOpponentKingOnSpecificRoutes(IBoard board, Move move)
        {
            return board.FindPieces(move,  BishopOrientation).Count() != 0 ? true : false;  
        }

        public override bool ValidateMovement(IBoard board, IMove move)
        {
            move.DestinationCell.CheckDestinationCellAvailability(move.PieceColor);

            IEnumerable<Piece> findBishops = board.FindPieces(move, BishopOrientation);

            var piece = board.FoundedPiece(move, findBishops);

            if (piece != null) move.PiecePosition = piece.DestinationCell;

            return piece != null ? true : false;
        }
    }
}