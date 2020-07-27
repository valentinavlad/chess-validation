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

        private static void CheckDestinationCellHasPiece(Cell destinationCell)
        {
            if (destinationCell.HasPiece())
            {
                throw new InvalidOperationException("Invalid Move");
            }
        }

        public bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor, Move move)
        {

            Cell currentCell = null;
            if (playerColor == PieceColor.White && move.IsCapture) 
            {
                List<Orientation> orientations = WhitePawnOrientation();
                foreach (var orientation in orientations)
                {
                    currentCell = currentPosition;
                    while (true)
                    {
                        currentCell = currentCell.Look(orientation);
                        if (currentCell.Piece.Name == PieceName.King && playerColor != currentCell.Piece.pieceColor)
                        {
                            //we find the king, which is in check
                            return true;
                        }
                        break;
                    }
                }

            }
            if (playerColor == PieceColor.Black && move.IsCapture)
            {
                List<Orientation> orientations = BlackPawnOrientation();
                foreach (var orientation in orientations)
                {
                    currentCell = currentPosition;
                    while (true)
                    {
                        currentCell = currentCell.Look(orientation);
                        if (currentCell.Piece.Name == PieceName.King && playerColor != currentCell.Piece.pieceColor)
                        {
                            //we find the king, which is in check
                            return true;
                        }
                        break;
                    }
                }

            }

            return false;
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
