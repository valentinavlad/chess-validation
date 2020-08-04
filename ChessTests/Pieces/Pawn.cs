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

        public override bool ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor, out Piece piece)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);

            if (playerColor == PieceColor.White)
            {
                piece = GetPawn(destinationCell, playerColor, Orientation.Down, move);
                return true;
            }

            if (playerColor == PieceColor.Black)
            {
                piece = GetPawn(destinationCell, playerColor, Orientation.Up, move);
                return true;
            }

            throw new InvalidOperationException("Illegal move!");
        }

        public bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor, Move move)
        {
            var orientations = playerColor == PieceColor.White ? WhitePawnOrientation() : BlackPawnOrientation();
            return move.IsCapture ? BoardAction.CheckForOpponentKingOnSpecificRoutes(currentPosition, playerColor, orientations) : false;
        }

        private Piece GetPawn(Cell destinationCell, PieceColor playerColor, Orientation orientation, Move move)
        {
            return !move.IsCapture ? FindPawn(destinationCell, orientation, playerColor)
                   : FindPawnWhoCaptures(destinationCell, orientation, playerColor, move);
        }
        private Pawn FindPawnWhoCaptures(Cell destinationCell, Orientation orientation, PieceColor playerColor, Move move)
        {
            try
            {
                var cell = destinationCell.Look(orientation, move.Y);
                if (cell.HasPiece() && cell.HasPiece() && cell.BelongsTo(playerColor))
                {
                    return (Pawn)cell.Piece;
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Illegal move!");
            }

            return null;
        }

        private Pawn FindPawn(Cell destinationCell, Orientation orientation, PieceColor playerColor)
        {
            CheckDestinationCellHasPiece(destinationCell);
            var cell = destinationCell.Look(orientation);

            if (cell.HasPawn() && cell.BelongsTo(playerColor))
            {
                return (Pawn)cell.Piece;
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
                return (Pawn)piece;
            }

            throw new InvalidOperationException("Pawn is in an invalid state!");
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

        private void CheckDestinationCellHasPiece(Cell destinationCell)
        {
            if (destinationCell.HasPiece())
            {
                throw new InvalidOperationException("Invalid Move");
            }
        }
    }
}