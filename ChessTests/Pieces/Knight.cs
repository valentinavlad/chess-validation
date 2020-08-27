using ChessTests.Interfaces;
using System.Collections.Generic;

namespace ChessTests
{
    public class Knight : Piece, ICheckOpponentKing
    {
        private readonly List<KnightOrientation> KnightOrientation =  new List<KnightOrientation>()
        {
            ChessTests.KnightOrientation.DownLeftDown, ChessTests.KnightOrientation.DownLeftUp, ChessTests.KnightOrientation.DownRightDown,
            ChessTests.KnightOrientation.DownRightUp, ChessTests.KnightOrientation.UpLeftDown, ChessTests.KnightOrientation.UpLeftUp,
            ChessTests.KnightOrientation.UpRightDown, ChessTests.KnightOrientation.UpRightUp
        };
        
        public Knight(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Knight;
        }

        public override bool ValidateMovement(IBoard board, IMove move)
        {
            move.DestinationCell.CheckDestinationCellAvailability(move.PieceColor);

            List<Piece> findKnights = FindPieces(move.PieceColor, move.DestinationCell);

            var piece = board.FoundedPiece(move, findKnights);
            if (piece != null) move.PiecePosition = piece.DestinationCell;
            return piece != null ? true : false;
        }

        public bool CheckForOpponentKingOnSpecificRoutes(IBoard board, Move move)
        {
            foreach (var orientation in KnightOrientation)
            {
                var currentCell = move.DestinationCell;
                while (true)
                {
                    //there is no piece on the cells
                    currentCell = currentCell.LookLShape(orientation);

                    //Search looks out of board
                    if (currentCell == null) break;

                    if (currentCell.Piece == null) continue;

                    if (currentCell.Piece.Name == PieceName.King && move.PieceColor != currentCell.Piece.PieceColor)
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

                if (currentCell.Piece.Name == PieceName.Knight && playerColor == currentCell.Piece.PieceColor)
                {
                    findKnight.Add(currentCell.Piece);
                }
            }

            return findKnight;
        }
    }
}
