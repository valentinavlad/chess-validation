using System.Collections.Generic;

namespace ChessTests.Pieces
{
    public class Rook : Piece
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
            List<Piece> findRooks = boardAction.FindPieces(move, RookOrientation, PieceName.Rook);
            
            var piece = boardAction.FoundedPiece(move, findRooks);
            if (piece != null) move.PiecePosition = piece.CurrentPosition;
            return piece != null ? true : false;
        }

        public override bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            return boardAction.FindKing(currentPosition, playerColor, RookOrientation) != null ? true : false;
        }

  
    }
}
