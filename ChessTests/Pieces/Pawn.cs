using ChessTable;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ChessTests
{
    public class Pawn : Piece
    {
        public Pawn(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Pawn;
        }


        public override bool PieceCanMove(Board board, Cell start, Cell end)
        {
            throw new NotImplementedException();
        }

        public static Piece ValidateMovementAndReturnPiece (Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);
    
            if (playerColor == PieceColor.White && !move.IsCapture)
            {
                 return FindPawn(destinationCell, Orientation.Down, playerColor);
            }

            if (playerColor == PieceColor.Black && !move.IsCapture)
            {
                return FindPawn(destinationCell, Orientation.Up, playerColor);
            }

            if (playerColor == PieceColor.White && move.IsCapture)
            {
                return FindPawnWhoCaptures(destinationCell, Orientation.Down, playerColor, move);
            }

            if (playerColor == PieceColor.Black && move.IsCapture)
            {
                return FindPawnWhoCaptures(destinationCell, Orientation.Up, playerColor, move);
            }

            throw new InvalidOperationException("Illegal move!");
        }

        private static Piece FindPawnWhoCaptures(Cell destinationCell, Orientation orientation, PieceColor playerColor, Move move)
        {
            var cell = destinationCell.Look(orientation, move.Y);
            if(cell.HasPiece() && cell.HasPiece() && cell.BelongsTo(playerColor))
            {
                return cell.Piece;
            }
            
            throw new InvalidOperationException("Illegal move!");
        }

        private static Piece FindPawn(Cell destinationCell,Orientation orientation, PieceColor playerColor)
        {

            if (destinationCell.HasPiece())
            {
                throw new InvalidOperationException("Invalid Move");
            }

            var cell = destinationCell.Look(orientation);

            if (cell.HasPawn() && cell.BelongsTo(playerColor))
            {
                return cell.Piece;
            }
            if (cell.HasPiece())
            {
                throw new InvalidOperationException("Invalid Move");

            }

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
    }
}
