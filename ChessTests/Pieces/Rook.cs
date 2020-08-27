using ChessTests.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ChessTests.Pieces
{
    public class Rook : Piece, ICheckOpponentKing
    {
        private List<Orientation> RookOrientation = new List<Orientation>()
        {
            Orientation.Up, Orientation.Down,
            Orientation.Right, Orientation.Left
        };
        public Rook(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Rook;
        }

        public override bool ValidateMovement(IBoard board, IMove move)
        {
            move.DestinationCell.CheckDestinationCellAvailability(move.PieceColor);

            IEnumerable<Piece> findRooks = board.FindPieces(move, RookOrientation);
            
            var piece = board.FoundedPiece(move, findRooks);
            
            if (piece != null) move.PiecePosition = piece.DestinationCell;
            
            return piece != null ? true : false;
        }

        public bool CheckForOpponentKingOnSpecificRoutes(IBoard board,Move move)
        {
            return board.FindPieces(move, RookOrientation).Count() != 0 ? true : false;     
        }

  
    }
}
