using ChessTable;
using ChessTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessTests
{
    public class Knight : Piece
    {
        public Knight(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Knight;
        }

        public static Piece ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);

            CheckDestinationCellAvailability(playerColor, destinationCell);

            List<KnightOrientation> orientations = KnightOrientation();

            List<Piece> findKnights = FindPieces(playerColor, destinationCell, orientations);

            return BoardAction.FoundedPiece(move, findKnights);
        }

        public bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            List<KnightOrientation> orientations = KnightOrientation();
            foreach (var orientation in orientations)
            {
                var currentCell = currentPosition;
                while (true)
                {
                    //there is no piece on the cells
                    currentCell = currentCell.LookLShape(orientation);

                    //Search looks out of board
                    if (currentCell == null) break;

                    if (currentCell.Piece == null) continue;

                    if (currentCell.Piece.Name == PieceName.King && playerColor != currentCell.Piece.pieceColor)
                    {
                        //we find the king, which is in check
                        return true;
                    }

                    //there is an obstacle in the way, must throw exception or return
                    break;
                }

            }
            return false;
        }

        private static List<Piece> FindPieces(PieceColor playerColor, Cell destinationCell, List<KnightOrientation> orientations)
        {
            var findKnight = new List<Piece>();

            foreach (var orientation in orientations)
            {
                //var loop = true;
                var currentCell = destinationCell;
                while (true)
                {
                    //there is no piece on the cells
                    currentCell = currentCell.LookLShape(orientation);

                    //Search looks out of board
                    if (currentCell == null || currentCell.Piece == null) break;

                    //if (currentCell.Piece == null) continue;


                    if (currentCell.Piece.Name == PieceName.Knight && playerColor == currentCell.Piece.pieceColor)
                    {
                        findKnight.Add(currentCell.Piece);
                    }

                    //there is an obstacle in the way, must throw exception or return
                    break;
                }
            }

            return findKnight;
        }

        private static List<KnightOrientation> KnightOrientation()
        {
            return new List<KnightOrientation>()
            {
                ChessTests.KnightOrientation.DownLeftDown, ChessTests.KnightOrientation.DownLeftUp, ChessTests.KnightOrientation.DownRightDown,
                ChessTests.KnightOrientation.DownRightUp, ChessTests.KnightOrientation.UpLeftDown, ChessTests.KnightOrientation.UpLeftUp,
                ChessTests.KnightOrientation.UpRightDown, ChessTests.KnightOrientation.UpRightUp
            };
        }
        private static void CheckDestinationCellAvailability(PieceColor playerColor, Cell destinationCell)
        {
            if (destinationCell.BelongsTo(playerColor))
            {
                throw new InvalidOperationException("Invalid Move");
            }
        }

        public override bool ValidateMovementAndReturnPiece(Board board, Move move, PieceColor playerColor, out Piece piece)
        {
            throw new NotImplementedException();
        }
    }
}
