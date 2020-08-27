using ChessTests.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ChessTests
{
    public class Queen : Piece, ICheckOpponentKing
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

        public override bool ValidateMovement(IBoard board, IMove move)
        {
            move.DestinationCell.CheckDestinationCellAvailability(move.PieceColor);

            IEnumerable<Piece> findQueens = board.FindPieces(move, QueenOrientation);

            var piece = board.FoundedPiece(move, findQueens);

            if (piece != null) move.PiecePosition = piece.DestinationCell;

            return piece != null ? true : false;
        }

        public  bool CheckForOpponentKingOnSpecificRoutes(IBoard board,Move move)
        {
            return board.FindPieces(move, QueenOrientation).Count() != 0 ? true : false;
        }
    }
}
