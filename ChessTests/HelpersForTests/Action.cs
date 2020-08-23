using ChessTable;
using System;

namespace ChessTests.Helpers
{
    internal class Action
    {
        private readonly Board board;

        public Action(Board board)
        {
            this.board = board;
        }

        internal Piece AddPiece(string coordsAN, Piece piece)
        {
            var coordinates = MoveNotationCoordinatesConverter.ConvertChessCoordinatesToArrayIndexes(coordsAN);
            return AddPiece(coordinates, piece);
        }

        internal Piece AddPiece(Coordinate coordinates, Piece piece)
        {
            var cell = board.CellAt(coordinates);
            cell.Piece = piece;
            return cell.Piece;
        }

    }
}
