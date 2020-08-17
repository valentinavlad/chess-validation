using System.Collections.Generic;

namespace ChessTests
{
    public class Queen : Piece
    {
        private readonly List<Orientation> QueenOrientation = new List<Orientation>()
        {
                Orientation.Up,  Orientation.DownLeft, Orientation.UpRight,
                Orientation.Right, Orientation.DownRight, Orientation.Down,
                Orientation.Left,  Orientation.UpLeft
        };

        public Queen(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Queen;
        }

        public override bool ValidateMovement(Move move)
        {
            move.DestinationCell.CheckDestinationCellAvailability(move.Color);

            List<Piece> findQueens = boardAction.FindPieces(move, PieceName.Queen, QueenOrientation);

            var piece = boardAction.FoundedPiece(move, findQueens);

            if (piece != null) move.PiecePosition = piece.CurrentPosition;

            return piece != null ? true : false;
        }

        public override bool CheckForOpponentKingOnSpecificRoutes(Move move)
        {
           // return boardAction.FindKing(currentPosition, playerColor, QueenOrientation) != null ? true : false;
            return boardAction.FindPieces(move, PieceName.King, QueenOrientation).Count != 0 ? true : false;
        }
    }
}
