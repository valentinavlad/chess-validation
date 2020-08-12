using ChessTable;
using ChessTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessTests
{
    public class Knight : Piece
    {
        private List<KnightOrientation> KnightOrientation =  new List<KnightOrientation>()
        {
            ChessTests.KnightOrientation.DownLeftDown, ChessTests.KnightOrientation.DownLeftUp, ChessTests.KnightOrientation.DownRightDown,
            ChessTests.KnightOrientation.DownRightUp, ChessTests.KnightOrientation.UpLeftDown, ChessTests.KnightOrientation.UpLeftUp,
            ChessTests.KnightOrientation.UpRightDown, ChessTests.KnightOrientation.UpRightUp
        };
        
        public Knight(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Knight;
        }


        public override bool ValidateMovement(Move move, PieceColor playerColor)
        {
            CheckDestinationCellAvailability(playerColor, move.DestinationCell);

            List<Piece> findKnights = FindPieces(playerColor, move.DestinationCell);

            var piece = boardAction.FoundedPiece(move, findKnights);
            if (piece != null) move.PiecePosition = piece.CurrentPosition;
            return piece != null ? true : false;
        }


        public override bool CheckForOpponentKingOnSpecificRoutes(Cell currentPosition, PieceColor playerColor)
        {
            foreach (var orientation in KnightOrientation)
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

        private List<Piece> FindPieces(PieceColor playerColor, Cell destinationCell)
        {
            var findKnight = new List<Piece>();

            foreach (var orientation in KnightOrientation)
            {
                var currentCell = destinationCell;

                currentCell = currentCell.LookLShape(orientation);
                if (currentCell == null || currentCell.Piece == null) continue;

                if (currentCell.Piece.Name == PieceName.Knight && playerColor == currentCell.Piece.pieceColor)
                {
                    findKnight.Add(currentCell.Piece);
                }
            }

            return findKnight;
        }
    }
}
