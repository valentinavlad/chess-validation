using ChessTests.Interfaces;
using System.Collections.Generic;

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

        public override bool ValidateMovement(Move move)
        {
            move.DestinationCell.CheckDestinationCellAvailability(move.Color);

            List<Piece> findRooks = boardAction.FindPieces(move, PieceName.Rook, RookOrientation);
            
            var piece = boardAction.FoundedPiece(move, findRooks);
            
            if (piece != null) move.PiecePosition = piece.CurrentPosition;
            
            return piece != null ? true : false;
        }

        public  bool CheckForOpponentKingOnSpecificRoutes(Move move)
        {
            return boardAction.FindPieces(move, PieceName.King, RookOrientation).Count != 0 ? true : false;     
        }

  
    }
}
