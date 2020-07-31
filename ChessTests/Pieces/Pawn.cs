
using ChessTable;
using ChessTests.Helpers;
using System;
using System.Collections.Generic;

namespace ChessTests
{
    public class Pawn : Piece
    {
        public Pawn(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Pawn;
        }

        public static Piece ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);

            if (playerColor == PieceColor.White)
            {
                return !move.IsCapture ? FindPawn(destinationCell, Orientation.Down, playerColor)
                    : FindPawnWhoCaptures(destinationCell, Orientation.Down, playerColor, move);
            }

            if (playerColor == PieceColor.Black)
            {
                return !move.IsCapture ? FindPawn(destinationCell, Orientation.Up, playerColor)
                    : FindPawnWhoCaptures(destinationCell, Orientation.Up, playerColor, move);
            }

            throw new InvalidOperationException("Illegal move!");
        }

        private static Piece FindPawnWhoCaptures(Cell destinationCell, Orientation orientation, PieceColor playerColor, Move move)
        {
            try
            {
                var cell = destinationCell.Look(orientation, move.Y);
                if (cell.HasPiece() && cell.HasPiece() && cell.BelongsTo(playerColor))
                {
                    return cell.Piece;
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Illegal move!");
            }

            return null;
        }

        private static Piece FindPawn(Cell destinationCell, Orientation orientation, PieceColor playerColor)
        {
            CheckDestinationCellHasPiece(destinationCell);
            var cell = destinationCell.Look(orientation);

            if (cell.HasPawn() && cell.BelongsTo(playerColor))
            {
                return cell.Piece;
            }

            CheckDestinationCellHasPiece(cell);
            //check for two cell movements
            cell = cell.Look(orientation);

            if (cell.HasPawn() && cell.BelongsTo(playerColor))
            {
                var piece = cell.Piece;
                if (piece.IsOnInitialPosition() == false)
                {
                    throw new InvalidOperationException("Pawn is in an invalid state!");
                }
                return piece;
            }

            throw new InvalidOperationException("Pawn is in an invalid state!");
        }

        public bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor, Move move)
        {
            var orientations = playerColor == PieceColor.White ? WhitePawnOrientation() : BlackPawnOrientation();
            return move.IsCapture ? BoardAction.CheckForOpponentKingOnSpecificRoutes(currentPosition, playerColor, orientations) : false;
        }

        private static List<Orientation> WhitePawnOrientation()
        {
            return new List<Orientation>()
            {
                Orientation.UpLeft, Orientation.UpRight
            };
        }

        private static List<Orientation> BlackPawnOrientation()
        {
            return new List<Orientation>()
            {
                Orientation.DownLeft, Orientation.DownRight
            };
        }
    }
}