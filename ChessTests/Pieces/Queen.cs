using ChessTable;
using System.Collections.Generic;

namespace ChessTests
{
    public class Queen : Piece
    {
        private readonly List<Orientation> QueenOrientation = new List<Orientation>()
        {
                Orientation.Up,  Orientation.DownLeft, Orientation.UpRight,
                Orientation.Right, Orientation.DownRight,
                Orientation.Down,
                Orientation.Left,  Orientation.UpLeft

        };

        public Queen(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Queen;
        }

        public override bool ValidateMovement(Move move, PieceColor playerColor)
        {
            CheckDestinationCellAvailability(playerColor, move.DestinationCell);

            List<Piece> findQueens = boardAction.FindPieces(playerColor, move.DestinationCell, QueenOrientation, PieceName.Queen);

            var piece = boardAction.FoundedPiece(move, findQueens);

            if (piece != null) move.PiecePosition = piece.CurrentPosition;

            return piece != null ? true : false;
        }

        public override bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            return boardAction.FindKing(currentPosition, playerColor, QueenOrientation) != null ? true : false;
        }
    }
}
